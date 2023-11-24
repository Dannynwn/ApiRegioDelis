namespace RegioDelis.Models.DB;

public class Menu
{
    public int IDProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public decimal Precio { get; set; }
}
