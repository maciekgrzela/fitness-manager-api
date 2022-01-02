using System;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Address;
using FitnessManager.BusinessLogic.Address.Interfaces;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Contact;
using FitnessManager.BusinessLogic.Contact.Interfaces;
using FitnessManager.BusinessLogic.Customer;
using FitnessManager.BusinessLogic.Customer.Interfaces;
using FitnessManager.BusinessLogic.FitnessClass;
using FitnessManager.BusinessLogic.FitnessClass.Interfaces;
using FitnessManager.BusinessLogic.FitnessClub;
using FitnessManager.BusinessLogic.FitnessClub.Interfaces;
using FitnessManager.BusinessLogic.FitnessClubNetwork;
using FitnessManager.BusinessLogic.FitnessClubNetwork.Interfaces;
using FitnessManager.BusinessLogic.Hall;
using FitnessManager.BusinessLogic.Hall.Interfaces;
using FitnessManager.BusinessLogic.Instructor;
using FitnessManager.BusinessLogic.Instructor.Interfaces;
using FitnessManager.BusinessLogic.Membership;
using FitnessManager.BusinessLogic.SportsEquipment;
using FitnessManager.BusinessLogic.SportsEquipment.Interfaces;
using FitnessManager.BusinessLogic.Subscription;
using FitnessManager.BusinessLogic.Subscription.Interfaces;
using FitnessManager.BusinessLogic.User;
using FitnessManager.BusinessLogic.User.Interfaces;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.User;
using FitnessManager.Infrastructure.Accessors;
using FitnessManager.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FitnessManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DevSql"));
            });
            
            ConfigureServices(services);
        }
        
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ProdSql"));
            });
            
            ConfigureServices(services);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1"  });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("Pagination")
                        .WithExposedHeaders("WWW-Authenticate")
                        .WithOrigins(Configuration.GetSection("ClientAppUrl").Value)
                        .AllowCredentials();
                });
            });
            
            var builder = services.AddIdentityCore<UserEntity>();
            builder.AddRoles<IdentityRole>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();

            var identityBuilder = new IdentityBuilder(builder.UserType, builder.RoleType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<UserEntity>>();

            var key = SecurityKeyGenerator.Instance.GetKey();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateActor = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWebTokenGenerator, WebTokenGenerator>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFitnessClassService, FitnessClassService>();
            services.AddScoped<IFitnessClubService, FitnessClubService>();
            services.AddScoped<IFitnessClubNetworkService, FitnessClubNetworkService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ISportsEquipmentService, SportsEquipmentService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddAutoMapper(typeof(BaseEntity).Assembly, typeof(UserDto).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessManager.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}