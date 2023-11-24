using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RegioDelis.Models;

using System.Data;
using System.Data.SqlClient;

namespace RegioDelis.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly string cadenaSQL;

        public SucursalController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Sucursal> Lista = new List<Sucursal>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL)) {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_sucursal", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new Sucursal()
                            {
                                IDSucursal = Convert.ToInt32(reader["IDSucursal"]),
                                NombreSucursal = reader["NombreSucursal"].ToString(),
                                Ubicacion = reader["Ubicacion"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                CorreoSucursal = reader["CorreoSucursal"].ToString(),
                                HoraApertura = TimeSpan.Parse(reader["HoraApertura"].ToString()),
                                HoraCierre = TimeSpan.Parse(reader["HoraCierre"].ToString()),
                                DiasDisponibles = reader["DiasDisponibles"].ToString()

                            });
                        }
                    }

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Lista });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = Lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{IDSucursal:int}")]
        public IActionResult Obtener(int IDSucursal)
        {
            List<Sucursal> Lista = new List<Sucursal>();
            Sucursal sucursal = new Sucursal();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_sucursal", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new Sucursal()
                            {
                                IDSucursal = Convert.ToInt32(reader["IDSucursal"]),
                                NombreSucursal = reader["NombreSucursal"].ToString(),
                                Ubicacion = reader["Ubicacion"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                CorreoSucursal = reader["CorreoSucursal"].ToString(),
                                HoraApertura = TimeSpan.Parse(reader["HoraApertura"].ToString()),
                                HoraCierre = TimeSpan.Parse(reader["HoraCierre"].ToString()),
                                DiasDisponibles = reader["DiasDisponibles"].ToString()
                            });
                        }
                    }
                }

                sucursal = Lista.Where(item => item.IDSucursal == IDSucursal).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = sucursal });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = sucursal });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Sucursal objeto)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_sucursal", conexion);
                    cmd.Parameters.AddWithValue("NombreSucursal", objeto.NombreSucursal);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("PhoneNumber", objeto.PhoneNumber);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion);
                    cmd.Parameters.AddWithValue("CorreoSucursal", objeto.CorreoSucursal);
                    cmd.Parameters.AddWithValue("HoraApertura", objeto.HoraApertura);
                    cmd.Parameters.AddWithValue("HoraCierre", objeto.HoraCierre);
                    cmd.Parameters.AddWithValue("DiasDisponibles", objeto.DiasDisponibles);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                  
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message});
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Sucursal objeto)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_sucursal", conexion);
                    cmd.Parameters.AddWithValue("IDSucursal", objeto.IDSucursal == 0 ? DBNull.Value : objeto.IDSucursal);
                    cmd.Parameters.AddWithValue("NombreSucursal", objeto.NombreSucursal is null ? DBNull.Value : objeto.NombreSucursal);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion is null ? DBNull.Value : objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("PhoneNumber", objeto.PhoneNumber is null ? DBNull.Value : objeto.PhoneNumber);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion is null ? DBNull.Value : objeto.Descripcion);
                    cmd.Parameters.AddWithValue("CorreoSucursal", objeto.CorreoSucursal is null ? DBNull.Value : objeto.CorreoSucursal);
                    cmd.Parameters.AddWithValue("HoraApertura", objeto.HoraApertura);
                    cmd.Parameters.AddWithValue("HoraCierre", objeto.HoraCierre);
                    cmd.Parameters.AddWithValue("DiasDisponibles", objeto.DiasDisponibles is null ? DBNull.Value : objeto.DiasDisponibles);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IDSucursal:int}")]
        public IActionResult Eliminar(int IDSucursal)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_sucursal", conexion);
                    cmd.Parameters.AddWithValue("IDSucursal", IDSucursal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


    }
}
