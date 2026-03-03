using Microsoft.Data.Sqlite;
using CRUD.Domain;
using CRUD.Interfaces;

namespace CRUD.Storage;

public class ProductSqliteStorage : IProductStorage
{
    private readonly string _connectionString;

    public ProductSqliteStorage(IConfiguration configuration)
    {
        _connectionString =
        configuration["Storage:ConnectionString"]
        ?? throw new InvalidOperationException(
            "Storage:ConnectionString is not configured."
        );

    CreateTable();
    }

    private void CreateTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        CREATE TABLE IF NOT EXISTS Products (
            Id TEXT PRIMARY KEY,
            Name TEXT NOT NULL,
            Price REAL NOT NULL,
            Stock INTEGER NOT NULL
        );
        ";

        command.ExecuteNonQuery();
    }

    public bool Create(Product product)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        INSERT INTO Products (Id, Name, Price, Stock)
        VALUES ($id, $name, $price, $stock);
        ";

        command.Parameters.AddWithValue("$id", product.Id);
        command.Parameters.AddWithValue("$name", product.Name);
        command.Parameters.AddWithValue("$price", product.Price);
        command.Parameters.AddWithValue("$stock", product.Stock);

        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Product> GetAll()
    {
        var products = new List<Product>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, Price, Stock FROM Products;";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetDecimal(2),
                reader.GetInt32(3)
            ));
        }

        return products;
    }

    public Product? GetById(string id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT Id, Name, Price, Stock
        FROM Products
        WHERE Id = $id;
        ";

        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Product(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetDecimal(2),
            reader.GetInt32(3)
        );
    }

    public bool Update(Product product)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        UPDATE Products
        SET Name = $name,
            Price = $price,
            Stock = $stock
        WHERE Id = $id;
        ";

        command.Parameters.AddWithValue("$id", product.Id);
        command.Parameters.AddWithValue("$name", product.Name);
        command.Parameters.AddWithValue("$price", product.Price);
        command.Parameters.AddWithValue("$stock", product.Stock);

        var rows = command.ExecuteNonQuery();

        return rows > 0;
    }

    public bool Delete(string id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        DELETE FROM Products
        WHERE Id = $id;
        ";

        command.Parameters.AddWithValue("$id", id);

        var rows = command.ExecuteNonQuery();

        return rows > 0;
    }
}