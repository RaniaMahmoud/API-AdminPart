using E_commerceAPI.Model;
using E_commerceAPI.Repository.AddressRepository;
using E_commerceAPI.Repository.BrancheRepository;
using E_commerceAPI.Repository.CategoryRepository;
using E_commerceAPI.Repository.PhoneRepository;
using E_commerceAPI.Repository.ProductRepository;
using E_commerceAPI.Repository.StoreRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using E_commerceAPI.Repository.CardRepository;
using E_commerceAPI.Repository.OrderRepository;
using E_commerceAPI.Repository.AdminRepo;

namespace E_commerceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddCors(options =>
            {
                options.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E_commerceAPI", Version = "v1" });
            });
            services.AddDbContext<ContextDB>(optios =>
            {
                optios.UseSqlServer(Configuration.GetConnectionString("E-CommerceAPI"));
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBrancheRepository, BrancheRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ContextDB>();

            //services.AddIdentity<AppUser, UserManager<AppUser>>()
            //    .AddEntityFrameworkStores<ContextDB>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme =
                        JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = Configuration["JWT:ValidationAud"],
                    ValidIssuer = Configuration["JWT:ValidationISS"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]))
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E_commerceAPI v1"));
            }
            app.UseCors("Cors");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"assets")),
                RequestPath = new PathString("/assets")
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
