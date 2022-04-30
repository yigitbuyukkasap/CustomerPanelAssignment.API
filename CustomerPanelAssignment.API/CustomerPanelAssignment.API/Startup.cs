using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IO;

namespace CustomerPanelAssignment.API
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

            // cors
            services.AddCors((options) =>
            {
                options.AddPolicy("angularApplication", (builder) =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithExposedHeaders("*");
                });
            });

            services.AddControllers();

            //fluent valid
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());


            // Adding DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("CustomerPanelDb")));

            //Adding Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            // Auth JWT TOKEN CONF
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //AutoMapper
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerPanelAssignment.API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme { 
                    Description = "Bearer auth. ornek : \"bearer {token}\" ",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerPanelAssignment.API v1"));
            }
            app.UseCors("angularApplication");

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions { 
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Resources")),
                RequestPath = "/Resources"
            });;

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
