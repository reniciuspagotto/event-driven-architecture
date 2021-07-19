using EventDrivenArchitectureExample.Data.Context;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Product.Handler
{
    public class ProductHandler : IProductHandler
    {
        protected readonly DataContext _dataContext;

        public ProductHandler(DataContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<Data.Entities.Product> Create(Data.Entities.Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();

            return product;
        }
    }
}
