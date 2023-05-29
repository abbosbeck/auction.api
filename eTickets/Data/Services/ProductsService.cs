using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _appDbContext;
        public ProductsService(AppDbContext context) : base(context) 
        {
            _appDbContext = context;
        }

        public async Task UpdateProdcutPriceAsync(int id, Product entity)
        {
            var oldProduct = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            oldProduct.Price = entity.Price;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
