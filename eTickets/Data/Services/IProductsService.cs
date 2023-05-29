using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task UpdateProdcutPriceAsync(int id, Product entity);
    }
}
