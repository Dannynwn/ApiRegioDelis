namespace RegioDelis.Models.DB;

public class Rol
{
    public int IDRol { get; set; }

    public string Rol1 { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
