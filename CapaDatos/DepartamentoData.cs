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

        public async Task<Departamento> Obtener(int id)
        {
            Departamento departamento = new Departamento();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerDepartamento", conexion);
                cmd.Parameters.AddWithValue("@IdDepartamento", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        departamento = new Departamento
                        {
                            IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                            Nombre = reader["Nombre"].ToString()
                        };
                    }
                }
            }

            return departamento;
        }

        public async Task<bool> Crear(Departamento objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_crearDepartamento", conexion);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conexion.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Editar(Departamento objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_editarDepartamento", conexion);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.IdDepartamento);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conexion.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarDepartamento", conexion);
                cmd.Parameters.AddWithValue("@IdDepartamento", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conexion.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }
    }
}
