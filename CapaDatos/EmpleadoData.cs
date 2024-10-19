using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class EmpleadoData
    {
        private readonly ConnectionStrings conexiones;

        public EmpleadoData(IOptions<ConnectionStrings> options) { //para recibir las conexiones
        // Esto ya es inyeccion de dependencias
        conexiones = options.Value;
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync()) {
                    while (reader.Read()) {
                        lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                            Departamento = new Departamento { 
                             IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                             Nombre = reader["Nombre"].ToString()
                            }
                        });
                    }
                }
            }
            return lista;
        }
    }
}
