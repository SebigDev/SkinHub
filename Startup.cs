using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkinHubApp.Data;
using SkinHubApp.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SkinHubApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

             services.AddDbContext<SkinHubAppDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("SkinHubAppDbContextConn")));

            services.AddTransient<IGenderTypeServices, GenderTypeServices>();
            services.AddTransient<IColorTypeServices, ColorTypeServices>();
            services.AddTransient<IProductTypeServices, ProductTypeServices>();
            services.AddTransient<IProductListTypeServices, ProductListTypeServices>();
            services.AddTransient<IPostServices, PostServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IReplyServices, ReplyServices>();


             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SkinHubApp API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                 app.UseCors(builder => builder.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowCredentials());

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
             // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkinHubApp API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
