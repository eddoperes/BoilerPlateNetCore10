//using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services;
using BoilerPlateNetCore10.Domain.Interfaces;
using BoilerPlateNetCore10.Infra.Data.Context;
using BoilerPlateNetCore10.Infra.Data.Repository;

namespace BoilerPlateNetCore10.Infra.IoC
{
    public static class DependencyInjectionAPI
    {

        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));           

            //services.AddApiVersioning();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();

            //services.AddScoped<IFileService, FileService>();
            //services.AddScoped<ILoginService, LoginService>();
            
            //services.AddAutoMapper(cfg => { cfg.AddProfile<DomainToDTOMappingProfile>(); });

            return services;
        }




    }
}
