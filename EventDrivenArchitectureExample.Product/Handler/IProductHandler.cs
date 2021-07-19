using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Product.Handler
{
    public interface IProductHandler
    {
        Task<Data.Entities.Product> Create(Data.Entities.Product product);
    }
}
