using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dapper;

public class StockController : Controller
{
    private readonly ControlStockContext context;

    public StockController()
    {
        context = new ControlStockContext();
    }

    // Acción para mostrar la página principal
    public ActionResult Index()
    {
        // Obtener las categorías y el stock desde la base de datos
        IEnumerable<CategoriaModel> categorias = context.GetCategorias();
        IEnumerable<StockModel> stock = context.GetStock();

        // Pasar los datos a la vista
        ViewBag.Categorias = categorias;
        return View(stock);
    }

    // Acción para agregar un producto
    [HttpPost]
    public ActionResult AgregarProducto(StockModel producto)
    {
        // Insertar el producto en la base de datos
        context.InsertProducto(producto);

        // Redirigir al índice
        return RedirectToAction("Index");
    }

    // Acción para mostrar la página de filtrado
    public ActionResult Filtrar()
    {
        return View();
    }

    // Acción para realizar el filtrado
    [HttpPost]
    public ActionResult Filtrar(decimal presupuesto)
    {
        // Obtener las categorías y el stock desde la base de datos
        IEnumerable<CategoriaModel> categorias = context.GetCategorias();
        IEnumerable<StockModel> stock = context.GetStock();

        // Crear una lista para almacenar los productos filtrados
        var productosFiltrados = new List<StockModel>();

        // Crear una matriz para almacenar los resultados de la programación dinámica
        decimal[,] dp = new decimal[stock.Count() + 1, (int)presupuesto + 1];

        // Calcular los resultados de la programación dinámica
        for (int i = 0; i <= stock.Count(); i++)
        {
            for (int j = 0; j <= (int)presupuesto; j++)
            {
                if (i == 0 || j == 0)
                    dp[i, j] = 0;
                else if (stock.ElementAt(i - 1).Precio <= j)
                    dp[i, j] = Math.Max(stock.ElementAt(i - 1).Precio + dp[i - 1, j - (int)stock.ElementAt(i - 1).Precio], dp[i - 1, j]);
                else
                    dp[i, j] = dp[i - 1, j];
            }
        }

        // Recorrer los resultados de la programación dinámica para obtener los productos filtrados
        int res = (int)presupuesto;
        for (int i = stock.Count(); i > 0 && res > 0; i--)
        {
            if (dp[i, res] != dp[i - 1, res])
            {
                var producto = stock.ElementAt(i - 1);
                productosFiltrados.Add(producto);
                res -= (int)producto.Precio;
            }
        }

        // Calcular la diferencia entre el presupuesto y el precio total de los productos filtrados
        ViewBag.DiferenciaPresupuesto = presupuesto - productosFiltrados.Sum(p => p.Precio);

        // Pasar los productos filtrados a la vista
        return View("FiltrarResultados", productosFiltrados);
    }

    // Acción para eliminar un producto
    public ActionResult Eliminar(int id)
    {
        // Eliminar el producto de la base de datos
        context.EliminarProducto(id);

        // Redirigir al índice
        return RedirectToAction("Index");
    }

    // Acción para mostrar la página de edición de un producto
    public ActionResult Editar(int id)
    {
        // Obtener el producto desde la base de datos
        StockModel producto = context.GetProducto(id);

        // Pasar el producto a la vista
        return View(producto);
    }

    // Acción para actualizar un producto
    [HttpPost]
    public ActionResult ActualizarProducto(StockModel producto)
    {
        // Actualizar el producto en la base de datos
        context.ActualizarProducto(producto);

        // Redirigir al índice
        return RedirectToAction("Index");
    }
}