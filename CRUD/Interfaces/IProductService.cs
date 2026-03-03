using CRUD.Domain;

namespace CRUD.Interfaces
{
    public interface IProductService
    {
        bool Create(string id, string name, decimal price, int stock);
        Product? GetById(string id);
        IEnumerable<Product> GetAll();
        bool Update(string id, string name, decimal price, int stock);
        bool Delete(string id);
    }
}