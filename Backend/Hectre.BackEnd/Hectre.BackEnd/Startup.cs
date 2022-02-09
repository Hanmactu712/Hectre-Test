using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Hectre.BackEnd.Common;
using Hectre.BackEnd.Data;
using Hectre.BackEnd.GraphQl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Hectre.BackEnd
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
            services.AddDbContext<HectreDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("DemoMySqlConnection")));

            services.AddSingleton<ISchema, HectreSchema>();

            services.AddRepositoryServices();

            services.AddScoped<DataContext>();

            services.AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = true;
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate =
                        ctx => logger.LogError("{Error} occurred", ctx.OriginalException);
                }).AddDataLoader()
                .AddSystemTextJson()
                .AddAllGraphType();

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(setting => setting.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                option.AddPolicy("MyCors", options =>
                {
                    options.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:44342/", "https://localhost:5001/").AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hectre.BackEnd", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hectre.BackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyCors");

            app.UseAuthorization();

            app.UseGraphQLPlayground(new PlaygroundOptions()
            {
                SchemaPollingEnabled = false,
                GraphQLEndPoint = "/graphql"
            }, "/playground");
            app.UseGraphQL<ISchema>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
