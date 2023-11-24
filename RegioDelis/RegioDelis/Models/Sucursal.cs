namespace RegioDelis.Models
{
    public class Sucursal
    {
        public int IDSucursal { get; set; }

        public string NombreSucursal { get; set; } = null!;

        public string Ubicacion { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string CorreoSucursal { get; set; } = null!;

        public TimeSpan HoraApertura { get; set; }

        public TimeSpan HoraCierre { get; set; }

        public string DiasDisponibles { get; set; } = null!;
    }
}
