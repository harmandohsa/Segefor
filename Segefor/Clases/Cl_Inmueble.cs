using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Inmueble
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        

        public DataSet Inmueble_GetAll(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmuebles_GetAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Inmueble_GetAll_Manejo(int UsuarioId, int AsignacionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmuebles_GetAll_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Integer).Value = AsignacionId;
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


        public DataSet Propietarios_GetAll(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Propietarios_GetAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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

        public bool Existe_Propiedad_Usuario(int UsuarioId, string Direccion, string Aldea, string Finca, int MunicipioId, int TipoDoc_Propiedad, DateTime Fec_Emision, int No_Finca, int No_Folio, int No_Libro, string De, string No_Certificado, string Municipalidad, string No_Escritura, int TituloId, string Notario, int OptArea, int AreaId, decimal Area)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmueble_Repetido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 200).Value = Direccion;
                cmd.Parameters.Add("@Aldea", OleDbType.VarChar, 200).Value = Aldea;
                cmd.Parameters.Add("@Finca", OleDbType.VarChar, 200).Value = Finca;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@TipoDoc_Propiedad", OleDbType.Integer).Value = TipoDoc_Propiedad;
                cmd.Parameters.Add("@Fec_Emision", OleDbType.Date).Value = Fec_Emision;
                if (TipoDoc_Propiedad == 1)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = No_Finca;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = No_Folio;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = No_Libro;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = De;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 2)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = No_Certificado;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = Municipalidad;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 3)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = No_Escritura;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = TituloId;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = Notario;
                }
                
                cmd.Parameters.Add("@OptArea", OleDbType.Integer).Value = OptArea;
                if (OptArea == 1)
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = AreaId;
                else
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = DBNull.Value;
                cmd.Parameters.Add("@Area", OleDbType.Decimal).Value = Area;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,800).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        public bool Existe_Propiedad_OtroUsuario(int UsuarioId, string Direccion, string Aldea, string Finca, int MunicipioId, int TipoDoc_Propiedad, DateTime Fec_Emision, int No_Finca, int No_Folio, int No_Libro, string De, string No_Certificado, string Municipalidad, string No_Escritura, int TituloId, string Notario, int OptArea, int AreaId, decimal Area)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmueble_Repetido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 200).Value = Direccion;
                cmd.Parameters.Add("@Aldea", OleDbType.VarChar, 200).Value = Aldea;
                cmd.Parameters.Add("@Finca", OleDbType.VarChar, 200).Value = Finca;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@TipoDoc_Propiedad", OleDbType.Integer).Value = TipoDoc_Propiedad;
                cmd.Parameters.Add("@Fec_Emision", OleDbType.Date).Value = Fec_Emision;
                if (TipoDoc_Propiedad == 1)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = No_Finca;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = No_Folio;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = No_Libro;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = De;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 2)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = No_Certificado;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = Municipalidad;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 3)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipalidad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = No_Escritura;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = TituloId;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = Notario;
                }

                cmd.Parameters.Add("@OptArea", OleDbType.Integer).Value = OptArea;
                if (OptArea == 1)
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = AreaId;
                else
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = DBNull.Value;
                cmd.Parameters.Add("@Area", OleDbType.Decimal).Value = Area;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 800).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        public void Inserta_InmuebleT(int InmuebleId, int UsuarioId, string Direccion, string Aldea, string Finca, int MunicipioId, int TipoDoc_Propiedad, DateTime Fec_Emision, int No_Finca, string No_Folio, int No_Libro, string De, string No_Certificado, string Municipalidad, string No_Escritura, int TituloId, string Notario, int OptArea, int AreaProtegidaId, double Area, double Gtm_Norte, double Gtm_Oeste, string Col_Norte, string Col_Sur, string Col_Este, string Col_Oeste)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmueble_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 200).Value = Direccion;
                cmd.Parameters.Add("@Aldea", OleDbType.VarChar, 200).Value = Aldea;
                cmd.Parameters.Add("@Finca", OleDbType.VarChar, 200).Value = Finca;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@TipoDoc_Propiedad", OleDbType.Integer).Value = TipoDoc_Propiedad;
                cmd.Parameters.Add("@Fec_Emision", OleDbType.Date).Value = Fec_Emision;
                if (TipoDoc_Propiedad == 1)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = No_Finca;
                    cmd.Parameters.Add("@NoFolio", OleDbType.VarChar, 100).Value = No_Folio;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = No_Libro;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = De;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipaldad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 2)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = No_Certificado;
                    cmd.Parameters.Add("@Municipaldad", OleDbType.VarChar, 200).Value = Municipalidad;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 3)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Municipaldad", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = No_Escritura;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = TituloId;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = Notario;
                }
                cmd.Parameters.Add("@OptArea", OleDbType.Integer).Value = OptArea;
                if (OptArea == 1)
                    cmd.Parameters.Add("@AreaProtegidaId", OleDbType.Integer).Value = AreaProtegidaId;
                else
                    cmd.Parameters.Add("@AreaProtegidaId", OleDbType.Integer).Value = DBNull.Value;
                cmd.Parameters.Add("@Area", OleDbType.Double).Value = Area;
                cmd.Parameters.Add("@Gtm_Norte", OleDbType.Double).Value = Gtm_Norte;
                cmd.Parameters.Add("@Gtm_Oeste", OleDbType.Double).Value = Gtm_Oeste;
                cmd.Parameters.Add("@Col_Norte", OleDbType.VarChar, 200).Value = Col_Norte;
                cmd.Parameters.Add("@Col_Sur", OleDbType.VarChar, 200).Value = Col_Sur;
                cmd.Parameters.Add("@Col_Este", OleDbType.VarChar, 200).Value = Col_Este;
                cmd.Parameters.Add("@Col_Oeste", OleDbType.VarChar, 200).Value = Col_Oeste;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        

        public void Inserta_Inmueble_Propietario(int PersonaId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Propietario_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Actualiza_Archivo(int InmuebleId, byte[] Archivo, string ContentType, string Nombre_Archivo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_InsertaFile_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@ContentType", OleDbType.VarChar, 200).Value = ContentType;
                cmd.Parameters.Add("@Nombre_Archivo", OleDbType.VarChar, 200).Value = Nombre_Archivo;
                cmd.Parameters.AddWithValue("@File", OleDbType.VarBinary).Value = Archivo;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Actualiza_Inmueble(int InmuebleId, int ValidaJur, int PersonaJuridicaId, string Direccion, string Aldea, string Finca, int MunicipioId, int TipoDoc_Propiedad, DateTime Fec_Emision, int No_Finca, int No_Folio, int No_Libro, string De, string No_Certificado, string No_Escritura, int TituloId, string Notario, int OptArea, double Area, int AreaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmueble_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@ValidaJur", OleDbType.Integer).Value = ValidaJur;
                if (ValidaJur == 1)
                    cmd.Parameters.Add("@PersonaJuridicaId", OleDbType.Integer).Value = PersonaJuridicaId;
                else
                    cmd.Parameters.Add("@PersonaJuridicaId", OleDbType.Integer).Value = DBNull.Value;
                cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 200).Value = Direccion;
                cmd.Parameters.Add("@Aldea", OleDbType.VarChar, 200).Value = Aldea;
                cmd.Parameters.Add("@Finca", OleDbType.VarChar, 200).Value = Finca;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@TipoDoc_Propiedad", OleDbType.Integer).Value = TipoDoc_Propiedad;
                cmd.Parameters.Add("@Fec_Emision", OleDbType.Date).Value = Fec_Emision;
                if (TipoDoc_Propiedad == 1)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = No_Finca;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = No_Folio;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = No_Libro;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = De;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 2)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = No_Certificado;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = DBNull.Value;
                }
                else if (TipoDoc_Propiedad == 3)
                {
                    cmd.Parameters.Add("@NoFinca", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFolio", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoLibro", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@De", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Certificacion", OleDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Escritura", OleDbType.VarChar, 200).Value = No_Escritura;
                    cmd.Parameters.Add("@TituloId", OleDbType.Integer).Value = TituloId;
                    cmd.Parameters.Add("@Notario", OleDbType.VarChar, 200).Value = Notario;
                }

                cmd.Parameters.Add("@OptArea", OleDbType.Integer).Value = OptArea;
                if (OptArea == 1)
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = AreaId;
                else
                    cmd.Parameters.Add("@AreaId", OleDbType.Integer).Value = DBNull.Value;
                cmd.Parameters.Add("@Area", OleDbType.Double).Value = Area;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public Int32 Max_Inmueble()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void Elimina_Inmueble(int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmueble_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet Inmueble_Get(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmuebles_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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

        public DataSet Archivo_Inmueble(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Archivo_Inmueble_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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

        public bool Existe_Archivo(int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Archivo_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                if (Convert.ToInt32(cmd.Parameters["@Resul"].Value) == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                cn.Close();
                return false;
            }
        }

        public DataSet Get_Region_Subregion_Inmueble(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Region_Subregion_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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

        public DataSet Get_Region_Subregion_MunicipioId(int MunicipioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Region_Subregion_MunicipioId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = MunicipioId;
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

        public DataSet obtener_puntos_poligonos_Inmueble(int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_obtener_puntos_poligonos_Inmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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

        public void Sp_Elimina_PoligonoFinca(int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Elimina_PoligonoFinca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
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