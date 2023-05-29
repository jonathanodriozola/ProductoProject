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

    public ActionResult Index()
    {
        IEnumerable<CategoriaModel> categorias = context.GetCategorias();
        IEnumerable<StockModel> stock = context.GetStock();
        ViewBag.Categorias = categorias;
        return View(stock);
    }

    [HttpPost]
    public ActionResult AgregarProducto(StockModel producto)
    {
        context.InsertProducto(producto);
        return RedirectToAction("Index");
    }
    public ActionResult Filtrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Filtrar(decimal presupuesto)
    {
        IEnumerable<CategoriaModel> categorias = context.GetCategorias();
        IEnumerable<StockModel> stock = context.GetStock();

        var productosFiltrados = new List<StockModel>();
        decimal[,] dp = new decimal[stock.Count() + 1, (int)presupuesto + 1];

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

        ViewBag.DiferenciaPresupuesto = presupuesto - productosFiltrados.Sum(p => p.Precio);

        return View("FiltrarResultados", productosFiltrados);
    }

    public ActionResult Eliminar(int id)
    {
        context.EliminarProducto(id);
        return RedirectToAction("Index");
    }


    public ActionResult Editar(int id)
    {
        StockModel producto = context.GetProducto(id);
        IEnumerable<CategoriaModel> categorias = context.GetCategorias();
        ViewBag.Categorias = categorias;
        return View(producto);
    }

    [HttpPost]
    public ActionResult GuardarEdicion(StockModel producto)
    {
        producto.Id = Convert.ToInt32(Request.Form["Id"]);
        context.ActualizarProducto(producto);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public ActionResult Vender(int[] productosSeleccionados)
    {
        if (productosSeleccionados != null && productosSeleccionados.Length > 0)
        {
            foreach (int productoId in productosSeleccionados)
            {
                context.EliminarProducto(productoId);
            }
        }
        return Json(new { success = true });
    }

}
