using CRUD.Domain;
using CRUD.Interfaces;

namespace CRUD.Services;

public class ProductService : IProductService
{
    private readonly IProductStorage _storage;

    public ProductService(IProductStorage storage)
    {
        _storage = storage;
    }

    public bool Create(string id, string name, decimal price, int stock)
    {
        var product = new Product(id, name, price, stock);
        return _storage.Create(product);
    }

    public IEnumerable<Product> GetAll()
        => _storage.GetAll();

    public Product? GetById(string id)
        => _storage.GetById(id);

    public bool Update(string id, string name, decimal price, int stock)
    {
        var product = new Product(id, name, price, stock);
        return _storage.Update(product);
    }

    public bool Delete(string id)
        => _storage.Delete(id);
}