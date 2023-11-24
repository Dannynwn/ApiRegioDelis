using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RegioDelis.Models;
using RegioDelis.Models.DB;
using System.Data;
using System.Data.SqlClient;

namespace RegioDelis.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly string cadenaSQL;

        public MenuController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Menu> Lista = new List<Menu>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_Menu", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new Menu()
                            {
                                IDProducto = Convert.ToInt32(reader["IDProducto"]),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                DescripcionProducto  = reader["DescripcionProducto"].ToString(),
                                Precio = Convert.ToInt32(reader["Precio"])

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
        [Route("Obtener/{IDProducto:int}")]
        public IActionResult Obtener(int IDProducto)
        {
            List<Menu> Lista = new List<Menu>();
            Menu Menu = new Menu();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_Menu", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new Menu()
                            {
                                IDProducto = Convert.ToInt32(reader["IDProducto"]),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                DescripcionProducto  = reader["DescripcionProducto"].ToString(),
                                Precio = Convert.ToInt32(reader["Precio"])
                            });
                        }
                    }
                }

                Menu = Lista.Where(item => item.IDProducto == IDProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Menu });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = Menu });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Menu objeto)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_Menu", conexion);
                    cmd.Parameters.AddWithValue("NombreProducto", objeto.NombreProducto);
                    cmd.Parameters.AddWithValue("DescripcionProducto", objeto.DescripcionProducto);
                    cmd.Parameters.AddWithValue("Precio", objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Menu objeto)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_Menu", conexion);
                    cmd.Parameters.AddWithValue("IDProducto", objeto.IDProducto == 0 ? DBNull.Value : objeto.IDProducto);
                    cmd.Parameters.AddWithValue("NombreProducto", objeto.NombreProducto is null ? DBNull.Value : objeto.NombreProducto);
                    cmd.Parameters.AddWithValue("DescripcionProducto", objeto.DescripcionProducto is null ? DBNull.Value : objeto.DescripcionProducto);
                    cmd.Parameters.AddWithValue("Precio", objeto.Precio == 0 ? DBNull.Value : objeto.Precio);
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
        [Route("Eliminar/{IDProducto:int}")]
        public IActionResult Eliminar(int IDProducto)
        {

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_Menu", conexion);
                    cmd.Parameters.AddWithValue("IDProducto", IDProducto);
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
