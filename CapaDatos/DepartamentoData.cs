using CapaEntidad;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DepartamentoData
    {
        private readonly ConnectionStrings conexiones;

        public DepartamentoData(IOptions<ConnectionStrings> options)
        {
            conexiones = options.Value;
        }

        public async Task<List<Departamento>> Lista(){
            List<Departamento> listaDepartamento = new List<Departamento>();
            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaDepartamentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync()) {
                    while (reader.Read()) {
                        listaDepartamento.Add(new Departamento()
                        {
                            IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                            Nombre = reader["Nombre"].ToString()
                        });
                    }
                }
            }
            return listaDepartamento;
        }
     }
}
