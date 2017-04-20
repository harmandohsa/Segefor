using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEGEFOR.Clases;
using System.Data.OleDb;
using System.Data;

namespace SEGEFOR.Clases
{
    public class Cl_Persona_Juridica
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();

        public DataSet Persona_Juridica_Get(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Juridica_GET", cn);
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

        

        public void Insertar_Persona_Juridica(int PersonaJuridicaId,int UsuarioId,int Tipo_Juridico_Id,string Nombre, DateTime Fec_Rep, string NoActa, int Numero, int Folio, int Libro, string Nit)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Juridica_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaJuridicaId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo_Juridico_Id", OleDbType.Integer).Value = Tipo_Juridico_Id;
                cmd.Parameters.Add("@Nombre", OleDbType.VarChar, 200).Value = Nombre;
                cmd.Parameters.Add("@Fec_Rep", OleDbType.Date).Value = Fec_Rep;
                if (NoActa == "")
                    cmd.Parameters.Add("@No_Acta", OleDbType.VarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@No_Acta", OleDbType.VarChar,200).Value = NoActa;
                cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = Numero;
                cmd.Parameters.Add("@Folio", OleDbType.Integer).Value = Folio;
                cmd.Parameters.Add("@Libro", OleDbType.Integer).Value = Libro;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 15).Value = Nit;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public Int32 Max_Persona_Juridica()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Persona_Juridica", cn);
                cmd.CommandType = CommandType.StoredProcedure;    
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
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

        public bool Existe_Juridco(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public bool Existe_NumeroJuridico(int Numero)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_NumeroJuridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = Numero;
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

        public DataSet Persona_Juridica_Get_CorrJur(int Persona_JuridicaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Juridica_GET_CorrJur", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Persona_JuridicaId", OleDbType.Integer).Value = Persona_JuridicaId;
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

        public void Persona_Juridica_Delete(int Persona_JuridicaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Juridica_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Persona_JuridicaId", OleDbType.Integer).Value = Persona_JuridicaId;
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Actualiza_Persona_Juridica(int PersonaJuridicaId, int Tipo_Juridico_Id, string Nombre, DateTime Fec_Rep, string NoActa, int Numero, int Folio, int Libro, string Nit)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Persona_Juridica_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Persona_JuridicaId", OleDbType.Integer).Value = PersonaJuridicaId;
                if (Tipo_Juridico_Id == 7)
                    cmd.Parameters.Add("@Tipo_Juridico_Id", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Tipo_Juridico_Id", OleDbType.Integer).Value = Tipo_Juridico_Id;
                cmd.Parameters.Add("@Nombre", OleDbType.VarChar, 200).Value = Nombre;
                cmd.Parameters.Add("@Fec_Rep", OleDbType.Date).Value = Fec_Rep;
                if (NoActa == "")
                    cmd.Parameters.Add("@No_Acta", OleDbType.VarChar,200).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@No_Acta", OleDbType.VarChar,200).Value = NoActa;
                cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = Numero;
                cmd.Parameters.Add("@Folio", OleDbType.Integer).Value = Folio;
                cmd.Parameters.Add("@Libro", OleDbType.Integer).Value = Libro;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 15).Value = Nit;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Empresa(int PersonaJuridicaId, int UsuarioId, string Nombre, int Numero, int Folio, int Libro, string Nit, int CategoriaEmpresaMercantil, string Objeto, int HorasTurno, int TurnoDia, int DiasYear, int EmplFijo, int EmplNoFijo, string Direccion, int MunicipioId, int TelUno, int TelDos, string Correo, int Categoria_EmpresaId, int StockMotoSierra)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Insert_Empresa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaJuridicaId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Nombre", OleDbType.VarChar, 200).Value = Nombre;
                cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = Numero;
                cmd.Parameters.Add("@Folio", OleDbType.Integer).Value = Folio;
                cmd.Parameters.Add("@Libro", OleDbType.Integer).Value = Libro;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 15).Value = Nit;
                cmd.Parameters.Add("@CategoriaEmpresaId", OleDbType.Integer).Value = Categoria_EmpresaId;
                cmd.Parameters.Add("@Objeto", OleDbType.VarChar, 300).Value = Objeto;
                cmd.Parameters.Add("@HorasTurno", OleDbType.Integer).Value = HorasTurno;
                cmd.Parameters.Add("@TurnoDia", OleDbType.Integer).Value = TurnoDia;
                cmd.Parameters.Add("@DiasYear", OleDbType.Integer).Value = DiasYear;
                cmd.Parameters.Add("@EmplFijo", OleDbType.Integer).Value = EmplFijo;
                cmd.Parameters.Add("@EmplNoFijo", OleDbType.Integer).Value = EmplNoFijo;
                cmd.Parameters.Add("@Direccion", OleDbType.VarChar, 300).Value = Direccion;
                cmd.Parameters.Add("@MunicipioId", OleDbType.Integer).Value = MunicipioId;
                cmd.Parameters.Add("@TelUno", OleDbType.Integer).Value = TelUno;
                if (TelDos == 0)
                    cmd.Parameters.Add("@TelDos", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TelDos", OleDbType.Integer).Value = TelDos;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 300).Value = Correo;
                cmd.Parameters.Add("@CategoriaEmpresaMercantilId", OleDbType.Integer).Value = CategoriaEmpresaMercantil;
                if (StockMotoSierra == 0)
                    cmd.Parameters.Add("@StockMotoSierra", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StockMotoSierra", OleDbType.Integer).Value = StockMotoSierra;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        

        public DataSet Get_Datos_Empresa(int EmpresaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Empresa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaJuridicaId", OleDbType.Integer).Value = EmpresaId;
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

        public DataSet Get_Propietarios_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Propietarios_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public bool Existe_Empresa(int Numero, int Folio, int Libro, int Categoria_EmpresaMercantilId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Empresa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = Numero;
                cmd.Parameters.Add("@Folio", OleDbType.Integer).Value = Folio;
                cmd.Parameters.Add("@Libro", OleDbType.Integer).Value = Libro;
                cmd.Parameters.Add("@CategoriaMercantilId", OleDbType.Integer).Value = Categoria_EmpresaMercantilId;
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

        public void Actualiza_DatosEmpresa(int PersonaJuridicaId, int HorasTurno, int EmplFijo, int EmplNoFijo, int DiasYear, int StockMotoSierra)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Actualiza_DatosEmpresa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaJuridicaId", OleDbType.Integer).Value = PersonaJuridicaId;
                cmd.Parameters.Add("@HorasTurno", OleDbType.Integer).Value = HorasTurno;
                cmd.Parameters.Add("@DiasYear", OleDbType.Integer).Value = DiasYear;
                cmd.Parameters.Add("@EmplFijo", OleDbType.Integer).Value = EmplFijo;
                cmd.Parameters.Add("@EmplNoFijo", OleDbType.Integer).Value = EmplNoFijo;
                cmd.Parameters.Add("@StockMotoSierra", OleDbType.Integer).Value = StockMotoSierra;
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