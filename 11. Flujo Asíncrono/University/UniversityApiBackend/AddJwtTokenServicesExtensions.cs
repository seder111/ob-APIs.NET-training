﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using UniversityApiBackend.Models.DataModels;
using Microsoft.IdentityModel.Tokens;

namespace UniversityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {

        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            var bindJwtSettings = new JwtSettings();

            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            Services.AddSingleton(bindJwtSettings);

            Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidIssuer = bindJwtSettings.ValidIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudience,
                        RequireExpirationTime = bindJwtSettings.RequiredExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidateLifeTime,
                        ClockSkew = TimeSpan.FromDays(1)

                    };
                });
        }
    }
}
