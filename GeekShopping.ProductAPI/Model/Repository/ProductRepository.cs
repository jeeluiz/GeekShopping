using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVO> Create(ProductVO vO)
        {
            Product product = _mapper.Map<Product>(vO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);

        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                Product products = await _context.Products.Where(p => p.id == id)
                    .FirstOrDefaultAsync();
                if (products == null) return false;
                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product products = await _context.Products.Where(p => p.id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(products);
        }

        public async Task<ProductVO> Update(ProductVO vO)
        {
            Product product = _mapper.Map<Product>(vO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
    }


}

