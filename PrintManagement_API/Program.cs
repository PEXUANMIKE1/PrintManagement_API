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

namespace PrintManagement_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constant.AppSettingKeys.DEFAULT_CONNECTION)));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            builder.Services.AddScoped<IBaseRepository<Permissions>, BaseRepository<Permissions>>();
            builder.Services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
            builder.Services.AddScoped<IBaseRepository<ConfirmEmail>, BaseRepository<ConfirmEmail>>();
            builder.Services.AddScoped<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDbContext, AppDbContext>();
            builder.Services.AddScoped<UserConverter>();
            builder.Services.AddCors();
            builder.Services.AddHttpContextAccessor();
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.MapControllers();

            app.Run();
        }
    }
}
