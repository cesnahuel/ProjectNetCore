using AutoMapper;
using CatalogData.Model;
using CatalogData.Repository.Concrete;
using CatalogData.Repository.Interface;
using CatalogApi.Domain;
using CatalogApi.Service.Concrete;
using CatalogApi.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using FluentValidation;
using CatalogApi.Validation;

namespace CatalogApi.Middlewares
{
    /// <summary>
    /// Contenedor de control de inversion, configura los servicios
    /// </summary>
    public static class IoC
    {
        public static IServiceCollection AddServicesApp(this IServiceCollection services)
        {
            //por cada request creo el servicio
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.Api", Version = "v1" });
            });
            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddDbContext<CatalogData.CatalogContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CatalogApi"))
            );
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ForMember(dest => dest.idProduct, opt => opt.MapFrom(src => src.IdProduct));
                cfg.CreateMap<ProductDTO, Product>().ForMember(dest => dest.IdProduct, opt => opt.MapFrom(src => src.idProduct));
                cfg.CreateMap<List<Product>, List<ProductDTO>>(MemberList.Destination);
                cfg.CreateMap<List<ProductDTO>, List<Product>>(MemberList.Destination);
                cfg.CreateMap<CategoryProductDTO, Category>();
                cfg.CreateMap<Category, CategoryProductDTO>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            var mapper = config.CreateMapper();
            //Creo el servicio la primera vez que lo utilizo
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<ProductDTO>, ProductValidator>();

            return services;

        }

    }
}
