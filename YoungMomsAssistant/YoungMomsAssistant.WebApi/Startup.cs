using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YoungMomsAssistant.Core.DbContexts;
using YoungMomsAssistant.Core.Domain.Babies;
using YoungMomsAssistant.Core.Domain.Users;
using YoungMomsAssistant.Core.Models.DbModels;
using YoungMomsAssistant.Core.Repositories;
using YoungMomsAssistant.WebApi.Configuration.Extensions;

namespace YoungMomsAssistant.WebApi {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCustomJwtAuth();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Baby>, BabyRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IBabyManager, BabyManager>();
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("YoungMomsAssistant.WebApi")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
