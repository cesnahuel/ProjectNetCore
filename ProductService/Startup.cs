using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessService.Service;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;

namespace BusinessService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Init Serilog configuration
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(BusinessData.Repository.Interface.IBaseRepository<>), typeof(BusinessData.Repository.Concrete.BaseRepository<>));
            services.AddScoped<IProductService, BusinessService.Service.ProductService>();
            services.AddScoped<BusinessData.Repository.Interface.IProductRepository, BusinessData.Repository.Concrete.ProductRepository>();

            services.AddScoped<IClientService, BusinessService.Service.ClientService>();
            services.AddScoped<BusinessData.Repository.Interface.IClientRepository, BusinessData.Repository.Concrete.ClientRepository>();

            var aa = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<BusinessData.BusinessContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BusinessService")));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Business API", Version = "V1" });
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BusinessData.Model.Client, Domain.ClientDTO>();
                cfg.CreateMap<Domain.ClientDTO, BusinessData.Model.Client>();
                cfg.CreateMap<IEnumerable<BusinessData.Model.Client>, IEnumerable<Domain.ClientDTO>>(MemberList.Destination);
                cfg.CreateMap<BusinessData.Model.Product, Domain.ProductDTO>();
                cfg.CreateMap<Domain.ProductDTO, BusinessData.Model.Product>();
                cfg.CreateMap<IEnumerable<BusinessData.Model.Product>, IEnumerable<Domain.ProductDTO>>(MemberList.Destination);
                cfg.CreateMap<BusinessData.Model.Category, Domain.CategoryDTO>();
                cfg.CreateMap<Domain.CategoryDTO, BusinessData.Model.Category>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });
            loggerFactory.AddSerilog();

        }
    }
}
