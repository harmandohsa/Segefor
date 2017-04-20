using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Regiones
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public DataSet Regiones_Mantenimiento()
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Regiones_Mantenimiento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public void Actualiza_Regional(int RegionId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actualiza_Regional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (RegionId == 0)
                    cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = RegionId;

                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet SubRegiones_Mantenimiento(int RegionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_SubRegiones_Mantenimiento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = RegionId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public void SP_Actualiza_SubRegional(int SubRegionId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Actualiza_SubRegional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (SubRegionId == 0)
                    cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Update_Estatus_SubRegion(int SubRegionId, int EstadoSubregionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Update_Estatus_SubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@EstadoSubregionId", OleDbType.Integer).Value = EstadoSubregionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insert_SubRegion(string No_SubRegion, string Sub_Region, string Nombre_SubRegion, int PersonaId, string Ubicacion, int RegionId, int MunicipioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_SubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@No_SubRegion", OleDbType.VarChar, 100).Value = No_SubRegion;
                cmd.Parameters.Add("@Sub_Region", OleDbType.VarChar, 300).Value = Sub_Region;
                cmd.Parameters.Add("@Nombre_SubRegion", OleDbType.VarChar, 300).Value = Nombre_SubRegion;
                if (PersonaId == 0)
                    cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                
                cmd.Parameters.Add("@Ubicacion", OleDbType.VarChar, 300).Value = Ubicacion;
                cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = RegionId;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet Get_MunicipioSubRegion(int SubRegionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_MunicipioSubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public void Insert_Municipio_SubRegion(int MunicipioId, int SubRegionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Municipio_SubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Update_Estatus_MunicipioSubRegion(int MunicipioId, int SubRegionId, int EstadoSubregionMunicipioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Update_Estatus_MunicipioSubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@Estado_SubRegionId", OleDbType.Integer).Value = EstadoSubregionMunicipioId;
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