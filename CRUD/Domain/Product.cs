namespace CRUD.Domain
{
    public class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product(string id, string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("O Id não pode estar vazio!");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome não pode estar vazio!");

            if (price < 0)
                throw new ArgumentException("O preço não pode ser negativo!");

            if (stock < 0)
                throw new ArgumentException("O estoque não pode ser negativo!");

            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void Update(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome não pode estar vazio!");

            if (price < 0)
                throw new ArgumentException("O preço não pode ser negativo!");

            if (stock < 0)
                throw new ArgumentException("O estoque não pode ser negativo!");

            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}