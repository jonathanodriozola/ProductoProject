using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

public class ControlStockContext
{
    private readonly string connectionString;

    public ControlStockContext()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ControlStockDB"].ConnectionString;
    }

    // Obtener todas las categorías desde la base de datos
    public IEnumerable<CategoriaModel> GetCategorias()
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Query<CategoriaModel>("SELECT * FROM Categorias").ToList();
        }
    }

    // Obtener todo el stock desde la base de datos
    public IEnumerable<StockModel> GetStock()
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Query<StockModel>("SELECT * FROM Stock").ToList();
        }
    }

    // Insertar un producto en la base de datos
    public void InsertProducto(StockModel producto)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Stock (Nombre, Precio, CategoriaId) VALUES (@Nombre, @Precio, @CategoriaId)";
            connection.Execute(query, producto);
        }
    }

    // Eliminar un producto de la base de datos
    public void EliminarProducto(int id)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Stock WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }
    }

    // Obtener un producto por su id desde la base de datos
    public StockModel GetProducto(int id)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Stock WHERE Id = @Id";
            return connection.QuerySingleOrDefault<StockModel>(query, new { Id = id });
        }
    }

    // Actualizar un producto en la base de datos
    public void ActualizarProducto(StockModel producto)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Stock SET Nombre = @Nombre, Precio = @Precio, CategoriaId = @CategoriaId WHERE Id = @Id";
            connection.Execute(query, producto);
        }
    }
}
