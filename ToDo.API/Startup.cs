using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ToDoAPI.DAL;
using ToDoListAPI.ToDoAPI.GraphQL;
using ToDoListAPI.ToDoAPI.Middleware;
using ToDoListAPI.ToDoBLL.Services;
using AutoMapper;
using ToDoListAPI.ToDoBLL.Handlers;
using ToDoAPI.Core.Utilities;

namespace ToDoListAPI.ToDoAPI
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
            services.AddCors();
            services.AddDbContextPool<ToDoContext>(optionsAction =>
            optionsAction.UseSqlServer(Configuration.GetConnectionString("ToDoContext"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.CommandTimeout(60);
                sqlOptions.EnableRetryOnFailure();
            })
            );

            services.AddControllers(p => p.RespectBrowserAcceptHeader = true).AddNewtonsoftJson();

            #region ConfigureSwagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ToDoList API",
                    Description = "ToDoList operation using swagger",
                    Version = "v1"
                });
                options.EnableAnnotations();
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion

            services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddTransient<CorrelationID, CorrelationID>();
            services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "ToDoList Api v1");
            });
            app.UsePlayground(new PlaygroundOptions
            {
                Path = "/playground"
            });
            app.UseMiddleware<CorrelationIdToResponseMiddleware>();
            app.UseHttpsRedirection();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });

        }
    }
}
