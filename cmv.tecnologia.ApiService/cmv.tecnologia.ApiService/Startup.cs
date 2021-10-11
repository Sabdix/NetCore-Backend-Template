using cmv.tecnologia.ApiService.Interceptores;
using cmv.tecnologia.DAL;
using cmv.tecnologia.Entidades;
using cmv.tecnologia.Entidades.Herramientas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cmv.tecnologia.ApiService {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
      services.AddSingleton(jwtTokenConfig);

      services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x => {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuer = true,
          ValidIssuer = jwtTokenConfig.Issuer,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
          ValidAudience = jwtTokenConfig.Audience,
          ValidateAudience = true,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.FromMinutes(1)
        };
      });

      services.AddSingleton<GeneradorTokens>();
      // Se agregan los DAOs
      services.AddScoped<LoginDao>();
      //services.AddScoped<ServiceDao>();

      services.AddHttpContextAccessor();

      services.AddControllers(config => {
        config.Filters.Add(new LogInterceptor(Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("Environment") + "ServiceLog")));
      });
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo {
          Title = "Api Service",
          Version = "v1",
          Description = "Api Descripcion.",
          Contact = new OpenApiContact {
            Name = "José Adrián Coria",
            Email = "jcoria@cajamorelia.com.mx"
          }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
          Description = @"Authorización JWT",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer",
          BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            new List<string>()
          }
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "cmv.tecnologia.ApiService v1");
          c.DocumentTitle = "Api Service";
#if TRUE
          // For Debug
          c.SwaggerEndpoint("/docs/v1/docs.json", "Catalogo Service v1");
          c.InjectStylesheet("/Resources/swagger-custom-styles.css");  //Added Code
#else
          // To deploy on IIS
          c.SwaggerEndpoint("/Dominio/swagger/v1/swagger.json", "Catalogo Service v1");
          c.InjectStylesheet("/Dominio/Resources/swagger-custom-styles.css");  //Added Code
#endif
        });
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
