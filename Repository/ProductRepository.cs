
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperDemo.Models;


namespace DapperDemo.Repository
{
  public class ProductRepository
  {
    private string connectionString;
    public ProductRepository()
    {
      connectionString = "Server=localhost;User ID=sa;Password=xxx;Database=DapperDemo;Integrated Security=false";
    }

    public IDbConnection Connection
    {
      get
      {
        return new SqlConnection(connectionString);
      }
    }

    public Product Add(Product prod)
    {
      using (IDbConnection dbConnection = Connection)
      {
        string query = @"INSERT INTO Products (Name, Quantity, Price)
                            OUTPUT INSERTED.*
                            VALUES (@Name, @Quantity, @Price)";
        dbConnection.Open();
        return dbConnection.Query<Product>(query, prod).FirstOrDefault();
      }
    }

    public IEnumerable<Product> GetAll()
    {
      using (IDbConnection dbConnection = Connection)
      {
        dbConnection.Open();
        return dbConnection.Query<Product>("SELECT * FROM Products");
      }
    }

    public Product GetById(int id)
    {
      using (IDbConnection dbConnection = Connection)
      {
        string query = @"SELECT * FROM Products
                                WHERE ProductId = @Id";
        dbConnection.Open();
        return dbConnection.Query<Product>(query, new { Id = id }).FirstOrDefault();
      }
    }

    public Product Delete(int id)
    {
      using (IDbConnection dbConnection = Connection)
      {
        string query = @"DELETE Products OUTPUT DELETED.*
                        FROM Products
                        WHERE ProductId = @Id";
        dbConnection.Open();
        return dbConnection.Query<Product>(query, new { Id = id }).FirstOrDefault();
      }
    }

    public Product Update(Product prod)
    {
      using (IDbConnection dbConnection = Connection)
      {
        string query = @"UPDATE Products SET Name = @Name,
                            Quantity = @Quantity, Price = @Price
                            OUTPUT INSERTED.*
                            WHERE ProductId = @ProductId";
        dbConnection.Open();
        return dbConnection.Query<Product>(query, prod).FirstOrDefault();
      }
    }
  }
}