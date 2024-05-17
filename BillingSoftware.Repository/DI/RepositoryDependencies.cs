using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Billing.Repository.Imp.DBContext;
using Billing.Repository.Imp.ProductsRepo;
using Billing.Repository.Imp.UserRepo;
using Billing.Repository.Contracts;
using BillingSoftware.Repository.Contracts;
using BillingSoftware.Repository.CommonRepo;
using BillingSoftware.Repository.SuppliersRepo;

namespace Billing.Repository.Imp.DI
{
    public static class RepositoryDependencies
    {
        public static void RegisterRepositoryDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<POSBillingDBContext>(options =>
                options.UseSqlite(configuration.GetSection("Sqlite:ConnectionString").Value));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductMeasurementRepository, ProductMeasurementRepository>();
            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
        }
    }
}
