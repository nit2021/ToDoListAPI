using BasicAuth.API;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToDoListAPI.DAL;
using ToDoListAPI.GraphQL;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI
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
            services.AddDbContextPool<ToDoContext>(optionsAction =>
            optionsAction.UseSqlServer(Configuration.GetConnectionString("ToDoContext"),
            sqlServerOptionsAction: sqlOptions=>
            {
                sqlOptions.CommandTimeout(20);
                sqlOptions.EnableRetryOnFailure();
            })
            );
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers().AddNewtonsoftJson();

            #region ConfigureSwagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ToDoList Demo API",
                    Description = "Demo for showing swagger",
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();
            //services.AddScoped<Query>();
            //services.AddGraphQL(c=>SchemaBuilder.New().AddServices(c).AddType<ToDoItemType>().AddQueryType<Query>().Create());
            services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
                });
                app.UsePlayground(new PlaygroundOptions
                {
                    //QueryPath = "/api",
                    Path = "/playground"
                });
            }
            //app.UseGraphQL();
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
