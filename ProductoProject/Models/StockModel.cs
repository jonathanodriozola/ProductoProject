using System;

public class StockModel
{
    public int Id { get; set; }
    public decimal Precio { get; set; }
    public string FechaCarga { get; set; }
    public int CategoriaId { get; set; }
    public string CategoriaNombre { get; set; }
}