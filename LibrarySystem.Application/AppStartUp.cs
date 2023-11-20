using FluentValidation;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Mapping;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Infrastructure.Data;
using LibrarySystem.Infrastructure.Repositories;
using LibrarySystem.Infrastructure.Repositories.Base;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //validitors
            services.AddTransient<IValidator<AuthorDto>, AuthorDtoValidator>();
            services.AddTransient<IValidator<BookDto>, BookDtoValidator>();
            services.AddTransient<IValidator<PublisherDto>, PublisherDtoValidator>();


            //auto mapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
