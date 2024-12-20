﻿using Microsoft.Extensions.Options;
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

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                            Departamento = new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                                Nombre = reader["Nombre"].ToString()
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<Empleado> Obtener(int id)
        {
            Empleado empleado = new Empleado();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", conexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        empleado = new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                            Departamento = new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                                Nombre = reader["Nombre"].ToString()
                            }
                        };
                    }
                }
            }

            return empleado;
        }


        public async Task<bool> Crear(Empleado objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", conexion);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.Departamento!.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
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

        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", conexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.Departamento!.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
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
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("@IdEmpleado",id);
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
