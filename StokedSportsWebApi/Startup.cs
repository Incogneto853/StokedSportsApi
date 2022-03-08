using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using StokedSportsIdenityService.Configuration;
using StokedSportsIdenityService.Data.DBContext;
using StokedSportsIdenityService.Data.Repositories;
using StokedSportsIdenityService;
using StokedSportsWebApi.GlobalUtilities;
using StokedSportsSpotService.Data.DBContext;
using StokedSportsSpotService;
using StokedSportsSpotService.Data.Repositories;
using StokedSportsGroupService.Data.DBContext;
using StokedSportsGroupService.Data.Repositories;
using StokedSportsGroupService;
using StokedSportsSessionService.Data.Repositories;
using StokedSportsSessionService;

namespace StokedSportsWebApi
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
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddDbContextPool<StokedIdentityContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("StokedIdentityDbConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("StokedIdentityDbConnection"))));

            services.AddDbContextPool<StokedSpotContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("StokedSpotDbConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("StokedSpotDbConnection"))));

            services.AddDbContextPool<StokedGroupContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("StokedGroupDbConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("StokedGroupDbConnection"))));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = false
                    };
                })
                .AddGoogle(g =>
                {
                    g.ClientId = Configuration["Google:ClientId"];
                    g.ClientSecret = Configuration["Google:ClientSecret"];
                    g.SaveTokens = true;
                });

            services.AddCors();

            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<ISpotRepository, SpotRepository>();
            services.AddScoped<ISpotService, SpotService>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();

            //services.AddScoped<ISessionRepository, SessionRepository>();
            //services.AddScoped<ISessionService, SessionService>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<StokedIdentityContext>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "StokedSportsWebApi", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stoked Sports v1"));
            }

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}