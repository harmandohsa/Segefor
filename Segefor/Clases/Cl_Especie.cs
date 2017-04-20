using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Especie
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public void Update_Estatus_Especie(int EspecieId, int Estatus_EspecieId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Update_Estatus_Especie", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EspecieId", OleDbType.Integer).Value = EspecieId;
                cmd.Parameters.Add("@Estatus_EspecieId", OleDbType.Integer).Value = Estatus_EspecieId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Update_Especie(int EspecieId, string Codigo, string Nombre_Cientifico, string Familia, string Genero, string Autores, string Nombre_Comun, string Sinonimos, string Nombre_Comercial)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Update_Especie", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EspecieId", OleDbType.Integer).Value = EspecieId;
                cmd.Parameters.Add("@Codigo", OleDbType.VarChar, 100).Value = Codigo;
                cmd.Parameters.Add("@Nombre_Cientifico", OleDbType.VarChar,300).Value = Nombre_Cientifico;
                cmd.Parameters.Add("@Familia", OleDbType.VarChar, 300).Value = Familia;
                cmd.Parameters.Add("@Genero", OleDbType.VarChar, 300).Value = Genero;
                cmd.Parameters.Add("@Autores", OleDbType.VarChar, 300).Value = Autores;
                cmd.Parameters.Add("@Nombre_Comun", OleDbType.VarChar, 300).Value = Nombre_Comun;
                cmd.Parameters.Add("@Sinonimos", OleDbType.VarChar, 300).Value = Sinonimos;
                cmd.Parameters.Add("@Nombre_Comercial", OleDbType.VarChar, 300).Value = Nombre_Comercial;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insert_Especie(string Codigo, string Nombre_Cientifico, string Familia, string Genero, string Autores, string Nombre_Comun, string Sinonimos, string Nombre_Comercial)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Especie", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo", OleDbType.VarChar, 100).Value = Codigo;
                cmd.Parameters.Add("@Nombre_Cientifico", OleDbType.VarChar, 300).Value = Nombre_Cientifico;
                cmd.Parameters.Add("@Familia", OleDbType.VarChar, 300).Value = Familia;
                cmd.Parameters.Add("@Genero", OleDbType.VarChar, 300).Value = Genero;
                cmd.Parameters.Add("@Autores", OleDbType.VarChar, 300).Value = Autores;
                cmd.Parameters.Add("@Nombre_Comun", OleDbType.VarChar, 300).Value = Nombre_Comun;
                cmd.Parameters.Add("@Sinonimos", OleDbType.VarChar, 300).Value = Sinonimos;
                cmd.Parameters.Add("@Nombre_Comercial", OleDbType.VarChar, 300).Value = Nombre_Comercial;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public int SP_Existe_Especie_Codigo(string Codigo_Especie)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Especie_Codigo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo_Especie", OleDbType.VarChar, 200).Value = Codigo_Especie;
                cmd.Parameters.Add("@Resul", OleDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public int SP_Existe_Especie_NombreCientifico(string Nombre_Cientifico)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Especie_NombreCientifico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre_Cientifico", OleDbType.VarChar, 200).Value = Nombre_Cientifico;
                cmd.Parameters.Add("@Resul", OleDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

    }
}