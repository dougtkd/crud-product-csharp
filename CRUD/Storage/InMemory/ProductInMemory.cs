using CRUD.Domain;
using CRUD.Interfaces;

namespace CRUD.Storage
{
    public class ProductInMemoryStorage : IProductStorage
    {
        private readonly Dictionary<string, Product> _products = new();

        public bool Create(Product product)
        {
            if (_products.ContainsKey(product.Id))
                return false;

            _products.Add(product.Id, product);
            return true;
        }

        public Product? GetById(string id)
        {
            if (_products.TryGetValue(id, out var product))
                return product;

            return null;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Values;
        }

        public bool Update(Product product)
        {
            if (!_products.ContainsKey(product.Id))
                return false;

            _products[product.Id] = product;
            return true;
        }

        public bool Delete(string id)
        {
            return _products.Remove(id);
        }
    }
}