
using Domain.Contracts;
using Domain.Entities.Identity_Modules;
using E_Commerces.CustomMiddleWares;
using E_Commerces.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using Persistance.Data.Seeding;
using Persistance.Repositories;
using Service;
using Service.Abstractions;
using Shared.ErrorModels;
using StackExchange.Redis;
using System.Text;

namespace E_Commerces
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Configure Services

            builder.Services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();


            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

             //-------------------------------------

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles ()));
            
            builder.Services.AddScoped<PictureUrlResolver>();
            builder.Services.AddHttpContextAccessor();

            //--------------------------------------

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });


            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {

               options.InvalidModelStateResponseFactory = (context) =>
               {
                   return APIResponseFactory.GenerteApiValidationResponse(context);
               };
            });

            builder.Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"])),
                   
                };
            });

            builder.Services.AddTransient<OrderItemPictureUrlResolver>();

            builder.Services.AddScoped<ICashRepository, CashRepository>();
            builder.Services.AddScoped<ICashService, CashService>();


            #region Angular
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();


                });
            });
            #endregion

            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Build
            var app = builder.Build();
            #endregion

            #region Services
            var scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeed();


            #endregion

            #region MiddleWare
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("AllowAll");

            app.UseAuthorization();



            app.MapControllers();
            #endregion



            app.Run();
        }
    }
}
