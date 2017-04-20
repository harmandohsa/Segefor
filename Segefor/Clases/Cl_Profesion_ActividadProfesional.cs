using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Profesion_ActividadProfesional
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();


        public DataSet Profesion_Actividad_Profesional_GetAll()
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Profesion_Actividad_Profesional_GetAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                DataSet ds = new DataSet();
                return ds;
            }
        }

        public void Actualiza_Profesion_Actividad_Profesional(int Tipo, int UsuarioId, int ProfesionId, int CategoriaId, int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actualiza_Profesion_ActividadProfesional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@ProfesionId", OleDbType.Integer).Value = ProfesionId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }
    }
}