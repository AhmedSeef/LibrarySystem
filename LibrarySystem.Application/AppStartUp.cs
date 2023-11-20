using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.Repositories.Base;
using LibrarySystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LibrarySystem.Application.Services;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Mapping;
using AutoMapper;

namespace LibrarySystem.Application
{
    public static class AppStartUp
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            //Inject repositries
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            //Inject Logic
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookService, BookService>();

            //auto mapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
