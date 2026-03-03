using CRUD.Domain;

namespace CRUD.Interfaces
{
    public interface IProductStorage
    {
        bool Create(Product product);

        Product? GetById(string id);

        IEnumerable<Product> GetAll();

        bool Update(Product product);

        bool Delete(string id);
    }
}