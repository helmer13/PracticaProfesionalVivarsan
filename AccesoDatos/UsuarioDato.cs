﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using AcessoDatos;

//using PracticaProfesionalVivarsan.
namespace AccesoDatos
{
  public  class UsuarioDato
    {
       public  static DataSet seleccionarUsuarios()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_VISTA_USUARIOS");
            comando.CommandType = CommandType.StoredProcedure;
           comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDUSUARIO", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "Usuario");
            return ds;

        }

        public static DataSet seleccionarUsuario(string idUsuario)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_VISTA_USUARIOS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "USUARIO");
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);

            DataSet ds = db.ExecuteReader(comando, "Usuario");
            return ds;

        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasenna"></param>
        /// <returns></returns>
        public static DataSet seleccionarUsuario(String usuario, String contrasenna)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INICIARSESION");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@USUARIO", usuario);
            comando.Parameters.AddWithValue("@CONTRASENA", contrasenna);

            DataSet ds = db.ExecuteReader(comando, "usuario");
            return ds;

        }
        public static void InsertarActualizarUsuario(string id, string nombre, string usuarioGeneral, string contrasena, string tipo,int idEmpresa)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_USUARIO");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@NOMBRE", nombre);
            comando.Parameters.AddWithValue("@USUARIO", usuarioGeneral);
            comando.Parameters.AddWithValue("@CONTRASENA", contrasena);
            comando.Parameters.AddWithValue("@TIPO", tipo);
            comando.Parameters.AddWithValue("@IDEMPRESA", Convert.ToInt32( idEmpresa));

            db.ExecuteNonQuery(comando);
        }

        public static void InsertarActualizarUsuarioPrincipal(string id, string nombre, string usuarioGeneral, string contrasena, int idEmpresa)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_ACTUALIZAR_USUARIOPRINCIPAL");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@NOMBRE", nombre);
            comando.Parameters.AddWithValue("@USUARIO", usuarioGeneral);
            comando.Parameters.AddWithValue("@CONTRASENA", contrasena);
            comando.Parameters.AddWithValue("@IDEMPRESA", Convert.ToInt32(idEmpresa));

            db.ExecuteNonQuery(comando);
        }

        public static DataTable ReporteBitacora(DateTime fechaInicio)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_BITACORA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHA", fechaInicio);

            DataSet ds = db.ExecuteReader(comando, "usuario");
            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }


    }
}
