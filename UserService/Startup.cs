using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using UserApi.Service;
using UserApi.Domain;
using FluentValidation.AspNetCore;
using FluentValidation;
using UserApi.Validation;
using UserData.Repository.Interface;
using UserData.Repository.Concrete;
using UserData.Models;

namespace UserApi
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
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);


            services.AddControllers();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //configuro el connection string de la base
            services.AddDbContext<UserContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("UserApi"))
           );

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "User API", Version = "V1" });
            });

            //Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TokenUser, TokenDto>()
                    .ForMember(dest => dest.auth_token, opt => opt.MapFrom(src => src.Token))
                    .ForMember(dest => dest.expire, opt => opt.MapFrom(src => src.EndDate));
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<AuthDto>, AuthValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
