using Microsoft.EntityFrameworkCore;
using System;
using PrintManagerment_API.Infrastructure.DataContexts;
using PrintManagerment_API.Application.Constants;
using PrintManagerment_API.Application.Handle.HandleEmail;
using PrintManagerment_API.Application.InterfaceServices;
using PrintManagerment_API.Doman.InterfaceRepositories;
using PrintManagerment_API.Doman.Entities;
using PrintManagerment_API.Application.ImplementServices;
using PrintManagerment_API.Application.Payload.Mapper;
using PrintManagerment_API.Infrastructure.ImplementRepository;
using PrintManagerment_API.Infrastructure.ImplementRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using PrintManagerment_API.Application.Payload.Mappers;
using Microsoft.Extensions.FileProviders;
using Org.BouncyCastle.Asn1.Cmp;
using StackExchange.Redis;
using Role = PrintManagerment_API.Doman.Entities.Role;

namespace PrintManagement_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //cấu hình redis

            //Lưu ý: tải Memurai  hoặc sủ dụng WSL 
            var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));


            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constant.AppSettingKeys.DEFAULT_CONNECTION)));
            builder.Services.AddScoped<IDbContext, AppDbContext>();

            //add scope Service
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IPrintJobsService, PrintJobService>();
            builder.Services.AddScoped<IBillService, BillService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IDesignService, DesignService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IResourcePropertyDetailService, ResourcePropertyDetailService>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            //add scope repository
            builder.Services.AddScoped<IBaseRepository<Design>, BaseRepository<Design>>();
            builder.Services.AddScoped<IBaseRepository<Delivery>, BaseRepository<Delivery>>();
            builder.Services.AddScoped<IBaseRepository<ShippingMethod>, BaseRepository<ShippingMethod>>();
            builder.Services.AddScoped<IBaseRepository<Bill>, BaseRepository<Bill>>();
            builder.Services.AddScoped<IBaseRepository<PrintJobs>, BaseRepository<PrintJobs>>();
            builder.Services.AddScoped<IBaseRepository<Notification>, BaseRepository<Notification>>();
            builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            builder.Services.AddScoped<IBaseRepository<Project>, BaseRepository<Project>>();
            builder.Services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
            builder.Services.AddScoped<IBaseRepository<Permissions>, BaseRepository<Permissions>>();
            builder.Services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
            builder.Services.AddScoped<IBaseRepository<ConfirmEmail>, BaseRepository<ConfirmEmail>>();
            builder.Services.AddScoped<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();
            builder.Services.AddScoped<IBaseRepository<Team>, BaseRepository<Team>>();
            builder.Services.AddScoped<IBaseRepository<ResourcePropertyDetail>, BaseRepository<ResourcePropertyDetail>>();
            builder.Services.AddScoped<IBaseRepository<ResourceProperty>, BaseRepository<ResourceProperty>>();
            builder.Services.AddScoped<IBaseRepository<Resources>, BaseRepository<Resources>>();
            builder.Services.AddScoped<IBaseRepository<ResourceForPrintJob>, BaseRepository<ResourceForPrintJob>>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //add scope converter
            builder.Services.AddScoped<UserConverter>();
            builder.Services.AddScoped<TeamConverter>();
            builder.Services.AddScoped<ProjectConverter>();
            builder.Services.AddScoped<DesignConverter>();
            builder.Services.AddScoped<EmployeeConverter>();
            builder.Services.AddScoped<CustomerConverter>();
            builder.Services.AddScoped<NotifyConverter>();
            builder.Services.AddScoped<BillConverter>();
            builder.Services.AddScoped<PrintJobConverter>();
            builder.Services.AddScoped<PropertyDetailConverter>();
            builder.Services.AddScoped<BillDeliveryConverter>();
            builder.Services.AddScoped<DeliveryConverter>();

            builder.Services.AddCors();
            builder.Services.AddHttpContextAccessor();
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Auth Api", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Vui lòng nhập token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Upload/Files")),
                RequestPath = "/Upload/Files"
            });
            app.MapControllers();

            app.Run();
        }
    }
}
