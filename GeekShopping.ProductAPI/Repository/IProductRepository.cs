using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Repository
{
    public class IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
