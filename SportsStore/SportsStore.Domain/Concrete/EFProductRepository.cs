using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return this.context.Products; }
        }
    }
}
