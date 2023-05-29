using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

public class ControlStockContext
{
    private readonly string connectionString;

    public ControlStockContext()
    {
        connectionString = ConfigurationManager.ConnectionStrings["ControlStockConnection"].ConnectionString;
    }

    public IDbConnection Connection => new SqlConnection(connectionString);

    public IEnumerable<CategoriaModel> GetCategorias()
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            return conn.Query<CategoriaModel>("SELECT * FROM Categorias");
        }
    }

    public IEnumerable<StockModel> GetStock()
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            return conn.Query<StockModel>("SELECT Stock.id, precio, CONVERT(varchar, fecha_carga, 23) AS FechaCarga, Categorias.nombre AS CategoriaNombre FROM Stock INNER JOIN Categorias ON Stock.categoria_id = Categorias.id");
        }
    }
    public void InsertProducto(StockModel producto)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            string query = "INSERT INTO Stock (precio, fecha_carga, categoria_id) VALUES (@Precio, @FechaCarga, @CategoriaId)";
            conn.Execute(query, producto);
        }
    }
    public void EliminarProducto(int id)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            string query = "DELETE FROM Stock WHERE id = @Id";
            conn.Execute(query, new { Id = id });
        }
    }
    public StockModel GetProducto(int id)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            return conn.QueryFirstOrDefault<StockModel>("SELECT Stock.id, precio, CONVERT(varchar, fecha_carga, 23) AS FechaCarga, Categorias.id AS CategoriaId, Categorias.nombre AS CategoriaNombre FROM Stock INNER JOIN Categorias ON Stock.categoria_id = Categorias.id WHERE Stock.id = @Id", new { Id = id });
        }
    }
    public void ActualizarProducto(StockModel producto)
    {
        using (IDbConnection conn = Connection)
        {
            conn.Open();
            string query = "UPDATE Stock SET precio = @Precio, fecha_carga = @FechaCarga, categoria_id = @CategoriaId WHERE id = @Id";
            conn.Execute(query, new { producto.Precio, producto.FechaCarga, producto.CategoriaId, producto.Id });
        }
    }
}
