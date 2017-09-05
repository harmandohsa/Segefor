using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using SEGEFOR.Data_Set;

namespace SEGEFOR.Clases
{
    public class Cl_Gestion_Registro
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        SqlConnection cnSql = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
        DataSet ds = new DataSet();
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona_Juridica ClEmpresa;
        Cl_Manejo ClManejo;

        public int MaxGestionId(int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
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

        public void Insertar_Gestion(int GestionId, string NUG, int PersonaId, int SubRegionId, int Tipo_UsuarioId, int ModuloId, int No_NUG)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@NUG", OleDbType.VarChar, 200).Value = NUG;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
                cmd.Parameters.Add("@ModuloId", OleDbType.Integer).Value = ModuloId;
                cmd.Parameters.Add("@No_NUG", OleDbType.Integer).Value = No_NUG;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Gestion_Plantacion(int GestionId, int SubCategoriaId, int CategoriaId, int ProcedenciaId, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_Plantacion_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                if (ProcedenciaId == 0)
                    cmd.Parameters.Add("@ProcedenciaId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ProcedenciaId", OleDbType.Integer).Value = ProcedenciaId;
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public void Gestion_BosqueNatural_Insert(int GestionId, int SubCategoriaId, int CategoriaId, int ConIncentivos, int ProcedenciaId, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId, int Tipo_BosqueId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_BosqueNatural_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@ConIncentivos", OleDbType.Integer).Value = ConIncentivos;
                cmd.Parameters.Add("@ProcedenciaId", OleDbType.Integer).Value = ProcedenciaId;
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo_BosqueId", OleDbType.Integer).Value = Tipo_BosqueId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public void Insert_Gestion_Entidad(int GestionId, int SubCategoriaId, int CategoriaId, string NombreEntidad, string Nit, string Objeto, string DireccionEntidad, int MunicipioIdEntidad, int TelefonoUno, int TelefonoDos, string CorreoEntidad, int Tipo_Persona, string NombreEmpresa, int TipoEntidadId, int CoberturaId, int Tipo_PropiedadId, string ActividadPrincipal, int NoFamAtendidas, int TamanoId, int Tipo_ProudccionId, int EtniaId, string Finalidad, int No_Integrantes, double TotBosqueNatural, double TotReforestacion, string NombreOficina, int YearCreacion, string CorreoOficina, int TelefonoOficina, string NombreEncargado, string ApellidoEncargado, string CorreoEncargado, int CelularEncargado, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Insert_Gestion_Entidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@NombreEntidad", OleDbType.VarChar, 500).Value = NombreEntidad;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 500).Value = Nit;
                cmd.Parameters.Add("@Objeto", OleDbType.VarChar, 500).Value = Objeto;
                cmd.Parameters.Add("@DireccionEntidad", OleDbType.VarChar, 500).Value = DireccionEntidad;
                cmd.Parameters.Add("@MunicipioIdEntidad", OleDbType.Integer).Value = MunicipioIdEntidad;
                cmd.Parameters.Add("@TelefonoUno", OleDbType.Integer).Value = TelefonoUno;
                if (TelefonoDos == 0)
                    cmd.Parameters.Add("@TelefonoDos", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TelefonoDos", OleDbType.Integer).Value = TelefonoDos;
                cmd.Parameters.Add("@CorreoEntidad", OleDbType.VarChar,300).Value = CorreoEntidad;
                cmd.Parameters.Add("@Tipo_PersonaId", OleDbType.Integer).Value = Tipo_Persona;
                cmd.Parameters.Add("@NomnbreEmpresa", OleDbType.VarChar, 300).Value = NombreEmpresa;
                if ((SubCategoriaId == 25) || (SubCategoriaId == 26))
                {
                    cmd.Parameters.Add("@Tipo_EntidadId", OleDbType.Integer).Value = TipoEntidadId;
                    cmd.Parameters.Add("@CoberturaId", OleDbType.Integer).Value = CoberturaId;
                    cmd.Parameters.Add("@Tipo_Propiedad", OleDbType.Integer).Value = Tipo_PropiedadId;
                }
                else
                {
                    cmd.Parameters.Add("@Tipo_EntidadId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@CoberturaId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Tipo_Propiedad", OleDbType.Integer).Value = DBNull.Value;
                }
                    
                if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27))
                    cmd.Parameters.Add("@ActividadPrincipal", OleDbType.VarChar, 300).Value = ActividadPrincipal;
                else
                    cmd.Parameters.Add("@ActividadPrincipal", OleDbType.VarChar, 300).Value = ActividadPrincipal;
                cmd.Parameters.Add("@NoFamAtendidas", OleDbType.Integer).Value = NoFamAtendidas;
                if (SubCategoriaId == 26)
                {
                    cmd.Parameters.Add("@TamanoId", OleDbType.Integer).Value = TamanoId;
                    cmd.Parameters.Add("@Tipo_ProduccionId", OleDbType.Integer).Value = Tipo_ProudccionId;
                }
                else
                {
                    cmd.Parameters.Add("@TamanoId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Tipo_ProduccionId", OleDbType.Integer).Value = DBNull.Value;
                }
                if (SubCategoriaId == 27)
                {
                    cmd.Parameters.Add("@EtniaId", OleDbType.Integer).Value = EtniaId;
                    cmd.Parameters.Add("@Finalidad", OleDbType.VarChar, 300).Value = Finalidad;
                    cmd.Parameters.Add("@No_Integrantes", OleDbType.Integer).Value = No_Integrantes;
                    cmd.Parameters.Add("@TotBosqueNatural", OleDbType.Double).Value = TotBosqueNatural;
                    cmd.Parameters.Add("@TotReforestacion", OleDbType.Double).Value = TotReforestacion;
                }
                else
                {
                    cmd.Parameters.Add("@EtniaId", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@Finalidad", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@No_Integrantes", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@TotBosqueNatural", OleDbType.Double).Value = DBNull.Value;
                    cmd.Parameters.Add("@TotReforestacion", OleDbType.Double).Value = DBNull.Value;
                }
                if (SubCategoriaId == 28)
                {
                    cmd.Parameters.Add("@NombreOficina", OleDbType.VarChar, 300).Value = NombreOficina;
                    cmd.Parameters.Add("@YearCreacion", OleDbType.Integer).Value = YearCreacion;
                    cmd.Parameters.Add("@CorreoOficina", OleDbType.VarChar, 300).Value = CorreoOficina;
                    cmd.Parameters.Add("@TelefonoOficina", OleDbType.Integer).Value = TelefonoOficina;
                    cmd.Parameters.Add("@NombreEncargado", OleDbType.VarChar, 300).Value = NombreEncargado;
                    cmd.Parameters.Add("@ApellidoEncargado", OleDbType.VarChar, 300).Value = ApellidoEncargado;
                    cmd.Parameters.Add("@CorreoEncargado", OleDbType.VarChar, 300).Value = CorreoEncargado;
                    cmd.Parameters.Add("@CelularEncargado", OleDbType.Integer).Value = CelularEncargado;
                }
                else
                {
                    cmd.Parameters.Add("@NombreOficina", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@YearCreacion", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@CorreoOficina", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@TelefonoOficina", OleDbType.Integer).Value = DBNull.Value;
                    cmd.Parameters.Add("@NombreEncargado", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@ApellidoEncargado", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@CorreoEncargado", OleDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@CelularEncargado", OleDbType.Integer).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public void Insertar_Gestion_SistemaAgro(int GestionId, int SubCategoriaId, int CategoriaId, int ConIncentivos, int ProcedenciaId, int CategoriaSAF, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_SistemaAgro_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@ConIncentivos", OleDbType.Integer).Value = ConIncentivos;
                if (ProcedenciaId == 0)
                    cmd.Parameters.Add("@ProcedenciaId", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ProcedenciaId", OleDbType.Integer).Value = ProcedenciaId;
                cmd.Parameters.Add("@CategoriaSAF", OleDbType.Integer).Value = CategoriaSAF;
                
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        public void Insertar_Gestion_Fuente_Semillera(int GestionId, int SubCategoriaId, int CategoriaId, int ProcedenciaId, int TipoBosqueId, XmlDocument especies, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId, int CategoriaFSId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Gestion_FuenteSemillera", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", SqlDbType.Int).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = CategoriaId;
                cmd.Parameters.Add("@ProcedenciaId", SqlDbType.Int).Value = ProcedenciaId;
                cmd.Parameters.Add("@TipoBosqueId", SqlDbType.Int).Value = TipoBosqueId;
                cmd.Parameters.Add("@Especies", SqlDbType.Xml).Value = especies.OuterXml.ToString();
                cmd.Parameters.Add("@Direccion_Notificacion", SqlDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", SqlDbType.Int).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = Telefono;
                cmd.Parameters.Add("@Celular", SqlDbType.Int).Value = Celular;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", SqlDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@CategoriaFSid", SqlDbType.Int).Value = CategoriaFSId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_Gestion_ArbolFrutal(int GestionId, int SubCategoriaId, int CategoriaId, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_Arbol_Frutal_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@Celular", OleDbType.Integer).Value = Celular;
                cmd.Parameters.Add("@Correo", OleDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insertar_Gestion_Profesional(int GestionId, int Telefono, int ProfesionId, int CategoriaProfesionId, string No_Colegiado, string No_Diploma, string Direccion_Notificacion, int Municipio_Notificacion, string Observaciones, string Nombre_Firma, int SubCategoriaId, int CategoriaId, string Aldea_Notificacion)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Gestion_Profesional_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", OleDbType.Integer).Value = Telefono;
                cmd.Parameters.Add("@ProfesionId", OleDbType.Integer).Value = ProfesionId;
                cmd.Parameters.Add("@CategoriaProfesionId", OleDbType.Integer).Value = CategoriaProfesionId;
                cmd.Parameters.Add("@No_Colegiado", OleDbType.VarChar, 200).Value = No_Colegiado;
                cmd.Parameters.Add("@No_Diploma", OleDbType.VarChar, 200).Value = No_Diploma;
                cmd.Parameters.Add("@Direccion_Notificacion", OleDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", OleDbType.Integer).Value = Municipio_Notificacion;
                cmd.Parameters.Add("@Observaciones", OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", OleDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@Aldea_Notificacion", OleDbType.VarChar, 200).Value = Aldea_Notificacion;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        


        public void Insertar_Gestion_Empresa(int GestionId, int Det_SubCategoria, int SubCategoriaId, int CategoriaId, int PersonaJuridicaId, string DireccionFuncionamiento, int MunicipioId_Funcionamiento, int FabricaProductos, int RegistroId,  XmlDocument ProductoImportacion, XmlDocument ProductosExportacion, int ActividadPrincialId, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId, int Tipo_Persona, string Razon_Social)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Gestion_Empresa", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                if (Det_SubCategoria == 0)
                    cmd.Parameters.Add("@Det_SubCategoriaId", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Det_SubCategoriaId", SqlDbType.Int).Value = Det_SubCategoria;
                cmd.Parameters.Add("@SubCategoriaId", SqlDbType.Int).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = CategoriaId;
                cmd.Parameters.Add("@PersonaJuridicaId", SqlDbType.Int).Value = PersonaJuridicaId;
                if (DireccionFuncionamiento == "")
                    cmd.Parameters.Add("@DireccionFuncionamiento", SqlDbType.VarChar, 300).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@DireccionFuncionamiento", SqlDbType.VarChar, 300).Value = Direccion_Notificacion;
                if (MunicipioId_Funcionamiento == 0)
                    cmd.Parameters.Add("@MunicipioIdFuncionamiento", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@MunicipioIdFuncionamiento", SqlDbType.Int).Value = MunicipioId_Funcionamiento;
                if (FabricaProductos == 0)
                    cmd.Parameters.Add("@FabricaProductos", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@FabricaProductos", SqlDbType.Int).Value = FabricaProductos;
                if (RegistroId == 0)
                    cmd.Parameters.Add("@RegistroId", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@RegistroId", SqlDbType.Int).Value = RegistroId;
                if (ProductoImportacion.HasChildNodes)
                    cmd.Parameters.Add("@ProductosImportacion", SqlDbType.Xml).Value = ProductoImportacion.OuterXml.ToString();
                else
                    cmd.Parameters.Add("@ProductosImportacion", SqlDbType.Xml).Value = DBNull.Value;
                if (ProductosExportacion.HasChildNodes)
                    cmd.Parameters.Add("@ProductosExportacion", SqlDbType.Xml).Value = ProductosExportacion.OuterXml.ToString();
                else
                    cmd.Parameters.Add("@ProductosExportacion", SqlDbType.Xml).Value = DBNull.Value;
                cmd.Parameters.Add("@ActividadPrincipalId", SqlDbType.Int).Value = ActividadPrincialId;
                cmd.Parameters.Add("@Direccion_Notificacion", SqlDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", SqlDbType.Int).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = Telefono;
                cmd.Parameters.Add("@Celular", SqlDbType.Int).Value = Celular;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", SqlDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo_Persona", SqlDbType.Int).Value = Tipo_Persona;
                cmd.Parameters.Add("@Razon_Social", SqlDbType.VarChar, 200).Value = Razon_Social;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_Gestion_MotoSierra(int GestionId, int SubCategoriaId, int CategoriaId, int PersonaJuridicaId, string Marca, string Modelo, double Cilindraje, double Potencia, string Serie, int TipoDocumentoId, string NoFactura, string Empresa, string Especifieque, string RazonSocial, int ActividadPrincialId, string Direccion_Notificacion, int Municipio_Notificacion, int Telefono, int Celular, string Correo, string Observaciones, string Nombre_Firma, int UsuarioId, int Tipo_Persona)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Gestion_MotoSierra", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@SubCategoriaId", SqlDbType.Int).Value = SubCategoriaId;
                cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = CategoriaId;
                
                if (SubCategoriaId == 11)
                {
                    cmd.Parameters.Add("@ActividadPrincipalId", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@PersonaJuridicaId", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Marca", SqlDbType.VarChar, 200).Value = Marca;
                    cmd.Parameters.Add("@Modelo", SqlDbType.VarChar,200).Value = Modelo;
                    cmd.Parameters.Add("@Cilindraje", SqlDbType.Float).Value = Cilindraje;
                    cmd.Parameters.Add("@Potencia", SqlDbType.Float).Value = Potencia;
                    cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 200).Value = Serie;
                    cmd.Parameters.Add("@TipoDocumentoId", SqlDbType.Int).Value = TipoDocumentoId;
                    if ((TipoDocumentoId == 1) || (TipoDocumentoId == 4))
                    {
                        cmd.Parameters.Add("@NoFactura", SqlDbType.VarChar, 200).Value = NoFactura;
                        cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 200).Value = Empresa;
                    }
                    else
                    {
                        cmd.Parameters.Add("@NoFactura", SqlDbType.VarChar, 200).Value = DBNull.Value;
                        cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 300).Value = DBNull.Value;
                    }
                    if (TipoDocumentoId == 5)
                        cmd.Parameters.Add("@Especifique", SqlDbType.VarChar, 200).Value = Especifieque;
                    else
                        cmd.Parameters.Add("@Especifique", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    

                }
                else
                {
                    cmd.Parameters.Add("@ActividadPrincipalId", SqlDbType.Int).Value = ActividadPrincialId;
                    cmd.Parameters.Add("@PersonaJuridicaId", SqlDbType.Int).Value = PersonaJuridicaId;
                    cmd.Parameters.Add("@Marca", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Modelo", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Cilindraje", SqlDbType.Float).Value = DBNull.Value;
                    cmd.Parameters.Add("@Potencia", SqlDbType.Float).Value = DBNull.Value;
                    cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@TipoDocumentoId", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@NoFactura", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 300).Value = DBNull.Value;
                    cmd.Parameters.Add("@Especifique", SqlDbType.VarChar, 200).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@Razon_Social", SqlDbType.VarChar, 200).Value = RazonSocial;
                
                cmd.Parameters.Add("@Direccion_Notificacion", SqlDbType.VarChar, 200).Value = Direccion_Notificacion;
                cmd.Parameters.Add("@MunicipioId_Notificacion", SqlDbType.Int).Value = Municipio_Notificacion;
                if (Telefono == 0)
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = Telefono;
                cmd.Parameters.Add("@Celular", SqlDbType.Int).Value = Celular;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 200).Value = Correo;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("@Nombre_Firma", SqlDbType.VarChar, 200).Value = Nombre_Firma;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo_Persona", SqlDbType.Int).Value = Tipo_Persona;
                
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Gestiones(int Tipo, int Tipo_UsuarioId, int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Gestiones", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Get_Gestiones_Usuario(int PersonaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Gestiones_Usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
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

        public string Get_Actividad_Registro(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Actividad_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "No Existe Actividad";
            }
        }

        public DataSet Formulario_Profesional(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Formulario_Profesional", cn);
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
                return ds;
            }
        }

        public int Get_Actividad_RegistroId(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Actividad_RegistroId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string Get_Identificacion_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Identificacion_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "Sin Identifiación";
            }
        }

        public void Insert_Solicitud_Completacion_Gestion(int GestionId,XmlDocument Requisitos, int UsuarioId, string Pendinetes)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Solicitud_Completacion_Gestion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Requisitos", SqlDbType.Xml).Value = Requisitos.OuterXml.ToString();
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Pendientes", SqlDbType.VarChar).Value = Pendinetes;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }




        public DataSet Solicitud_Completacion_Gestion(int Gestion_IncompletaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Solicitud_Completacion_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Gestion_IncompletaId", OleDbType.Integer).Value = Gestion_IncompletaId;
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

        public int Max_GestionInCompletaId()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_GestionInCompletaId", cn);
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

        public string Get_CompletaPropietarios(int Categoria, int GestionId, int ModuloId)
        {
            string AgraegadoSol = "";
            if (ModuloId == 3)
            {
                if ((Categoria == 2) || (Categoria == 3) || (Categoria == 4) || (Categoria == 6) || (Categoria == 1))
                {
                    if (CuantosPropietarios_GestionRegistro(1, GestionId) > 3)
                    {
                        bool YaloPuso = false;
                        DataSet TiposDocPropiedad = Get_TiposInmuebles(GestionId, ModuloId);
                        for (int i = 0; i < TiposDocPropiedad.Tables["Datos"].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 1)
                                AgraegadoSol = " y Copropietarios";
                            else if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 2)
                            {
                                if (AgraegadoSol == "")
                                    AgraegadoSol = " y Poseedores";
                                else
                                    AgraegadoSol = "Copropietarios y Poseedores";
                                YaloPuso = true;
                            }
                            else if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 3)
                            {
                                if (YaloPuso != true)
                                {
                                    if (AgraegadoSol == "")
                                        AgraegadoSol = "y Poseedores";
                                    else
                                        AgraegadoSol = "Copropietarios y Poseedores";
                                    YaloPuso = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (CuantosPropietarios_GestionRegistro(2, GestionId) > 3)
                    {
                        AgraegadoSol = "y Propietarios";
                    }
                }
            }
            else if (ModuloId == 2)
            {
                if (CuantosPropietarios_GestionManejo(GestionId) > 3)
                {
                    bool YaloPuso = false;
                    DataSet TiposDocPropiedad = Get_TiposInmuebles(GestionId, ModuloId);
                    for (int i = 0; i < TiposDocPropiedad.Tables["Datos"].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 1)
                            AgraegadoSol = " y Copropietarios";
                        else if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 2)
                        {
                            if (AgraegadoSol == "")
                                AgraegadoSol = " y Poseedores";
                            else
                                AgraegadoSol = "Copropietarios y Poseedores";
                            YaloPuso = true;
                        }
                        else if (Convert.ToInt32(TiposDocPropiedad.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"]) == 3)
                        {
                            if (YaloPuso != true)
                            {
                                if (AgraegadoSol == "")
                                    AgraegadoSol = "y Poseedores";
                                else
                                    AgraegadoSol = "Copropietarios y Poseedores";
                                YaloPuso = true;
                            }
                        }
                    }
                }
            }
            return AgraegadoSol;
        }

        public DataSet ImpresionGestionInCompleta(int GestionInCompletaId, int SubCategoriaId)
        {
            
            int Categoria = Get_CategoriaRNFId(SubCategoriaId);
            DataSet dsDatos = Solicitud_Completacion_Gestion(GestionInCompletaId);
            Ds_Gestiones Ds_GestionIncompleta = new Ds_Gestiones();
            Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Clear();
            DataRow row = Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].NewRow();
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["No_Completacion"] = dsDatos.Tables["DATOS"].Rows[0]["No_Completacion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha_Solicitud"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha_Solicitud"];
            //row["Solicitante"] = dsDatos.Tables["DATOS"].Rows[0]["Solicitante"];
            int GestionId = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["GestionId"]);
            int ModuloId = SP_Get_Modulo_Gestion(GestionId);
            if (ModuloId == 3)
                row["Solicitante"] = Get_Propietarios_Gestion_Registro(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["GestionId"]), Categoria);
            else if (ModuloId == 2)
                row["Solicitante"] = Get_Propietarios_Manejo(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["GestionId"]));
            row["Pendientes"] = dsDatos.Tables["DATOS"].Rows[0]["Pendientes"];
            row["Secretaria"] = dsDatos.Tables["DATOS"].Rows[0]["Secretaria"];
            row["NoSubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["NoSubRegion"];
            row["Puesto"] = dsDatos.Tables["DATOS"].Rows[0]["Puesto"];
            Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Rows.Add(row);
            
            dsDatos.Clear();
            
            string AgraegadoSol = Get_CompletaPropietarios(Categoria, GestionId, ModuloId);
            if (AgraegadoSol != "")
                Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Rows[0]["Solicitante"] + " " + AgraegadoSol + ".";
            else
                Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Solicitud_Completacion"].Rows[0]["Solicitante"] + ".";
            return Ds_GestionIncompleta;

        }

        public DataSet Solicitud_Completacion_Gestion_Historial(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Solicitud_Completacion_Gestion_Historial", cn);
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
                return ds;
            }
        }

        public string Get_Region_SubRegion_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Region_SubRegion_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Max_Adminision_Gestion()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Adminision_Gestion", cn);
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

        public void Insert_Aceptacion_Gestion(int GestionId, string No_Expediente, string Mensaje, string Requisitos_Presentados, int UsuarioId, int Correlativo_Anual_SubRegion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Aceptacion_Gestion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@No_Expediente", SqlDbType.VarChar, 50).Value = No_Expediente;
                cmd.Parameters.Add("@Mensaje", SqlDbType.Text).Value = Mensaje;
                cmd.Parameters.Add("@Requisitos_Presentados", SqlDbType.Text).Value = Requisitos_Presentados;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Correlativo_Anual_SubRegion", SqlDbType.Int).Value = Correlativo_Anual_SubRegion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_Resolucion_Aceptacion(int GestionId, string Asunto, string Cuerpo_Resolucion, string ConsiderandoUno, string ConsiderandoDos, int UsuarioId, int SubRegion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Resolucion_Admision", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Asunto", SqlDbType.Text).Value = Asunto;
                cmd.Parameters.Add("@Cuerpo_Resolucion", SqlDbType.Text).Value = Cuerpo_Resolucion;
                cmd.Parameters.Add("@ConsiderandoUno", SqlDbType.Text).Value = ConsiderandoUno;
                cmd.Parameters.Add("@ConsiderandoDos", SqlDbType.Text).Value = ConsiderandoDos;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Sub_Region", SqlDbType.Int).Value = SubRegion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public string Get_Nug(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Nug", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Get_No_Expediente(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_No_Expediente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }


        public string Get_Nombre_SubCategoriaRNF(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubCategoriaRNF", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,150).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Get_Nombre_CategoriaRNF(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_CategoriaRNF", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 150).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Get_CategoriaRNFId(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_CategoriaRNFId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Get_CategoriaManejoId(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_CategoriaManejoId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Get_Det_RegistroRnfId(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Det_RegistroRnfId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Max_No_Expediente(int SubRegionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_No_Expediente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
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


        public int Get_SubRegion_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubRegion_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public DataSet Genera_Impresion_Admision_Expediente(int Admision_GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Genera_Impresion_Admision_Expediente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Admision_GestionId", OleDbType.Integer).Value = Admision_GestionId;
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

        public void Manda_Gestion_Usuario(int GestionId, int TipoUsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Manda_Gestion_Usuario", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Tipo_UsuarioId", SqlDbType.Int).Value = TipoUsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Manda_Gestion_Usuario_Validacion(int GestionId, int TipoUsuarioId, int Origen)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Manda_Gestion_Usuario_Validacion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Tipo_UsuarioId", SqlDbType.Int).Value = TipoUsuarioId;
                cmd.Parameters.Add("@Origen", SqlDbType.Int).Value = Origen;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }


        public int SP_Get_Modulo_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Modulo_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@ModuloId", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@ModuloId"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        

        public DataSet ImpresionAdmisionExpediente(int AdmisionGestionId, int SubCategoria)
        {
            int Categoria = Get_CategoriaRNFId(SubCategoria);
            DataSet dsDatos = Genera_Impresion_Admision_Expediente(AdmisionGestionId);
            Ds_Gestiones Ds_GestionIncompleta = new Ds_Gestiones();
            Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Clear();
            DataRow row = Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].NewRow();
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["Sub_Region"];
            row["No_CAE"] = dsDatos.Tables["DATOS"].Rows[0]["No_Admision_Gestion"];
            row["No_Expediente"] = dsDatos.Tables["DATOS"].Rows[0]["No_Expediente"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["Mensaje"] = dsDatos.Tables["DATOS"].Rows[0]["Mensaje"];
            row["Doc_Presentada"] = dsDatos.Tables["DATOS"].Rows[0]["Requisitos_Presentados"];
            row["Secretaria"] = dsDatos.Tables["DATOS"].Rows[0]["Secretaria"];
            row["Puesto"] = dsDatos.Tables["DATOS"].Rows[0]["Puesto"];
            row["Nombre_SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre"];
            int GestionId = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["GestionId"]);
            int MOduloId = SP_Get_Modulo_Gestion(GestionId);
            if (MOduloId == 3)
                row["Solicitante"] = Get_Propietarios_Gestion_Registro(GestionId, Categoria);
            else if (MOduloId == 2)
                row["Solicitante"] = Get_Propietarios_Manejo(GestionId);
            Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Rows.Add(row);
            dsDatos.Clear();

            //Llenado Caratula
            DataSet dsDatosCaratula = Datos_Caratula(AdmisionGestionId);
            Ds_GestionIncompleta.Tables["Dt_Caratula"].Clear();
            DataRow rowCaratula = Ds_GestionIncompleta.Tables["Dt_Caratula"].NewRow();
            int CategoriaId = Convert.ToInt32(dsDatosCaratula.Tables["DATOS"].Rows[0]["CategoriaId"]);
            //int GestionId = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["GestionId"]);
            rowCaratula["NoExpediente"] = dsDatosCaratula.Tables["DATOS"].Rows[0]["No_Expediente"];
            rowCaratula["TipoSolicitud"] = dsDatosCaratula.Tables["DATOS"].Rows[0]["TipoSolicitud"];
            rowCaratula["Solicitante"] = Get_Propietarios_Gestion_Registro(GestionId, Categoria);
            rowCaratula["Representante"] = dsDatosCaratula.Tables["DATOS"].Rows[0]["Representante"];
            rowCaratula["Direccion"] = dsDatosCaratula.Tables["DATOS"].Rows[0]["Direccion_Notificacion"] + ", " + dsDatosCaratula.Tables["DATOS"].Rows[0]["Departamento"] + ", " + dsDatosCaratula.Tables["DATOS"].Rows[0]["Municipio"];
            Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows.Add(rowCaratula);
            dsDatosCaratula.Clear();

            int ModuloId = SP_Get_Modulo_Gestion(GestionId);
            string AgraegadoSol = Get_CompletaPropietarios(Categoria, GestionId, ModuloId);
            if (AgraegadoSol != "")
            {
                Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Rows[0]["Solicitante"] + " " + AgraegadoSol + ".";
                Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Solicitante"] + " " + AgraegadoSol + ".";
            }
            else
            {
                Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Admision_Gestion"].Rows[0]["Solicitante"] + ".";
                Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Solicitante"] = Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Solicitante"] + ".";
            }


            //DataSet Propietarios = Get_Propietrios_Caratula(GestionId, CategoriaId);
            //string PropietariosCaratula = "";
            //for (int i = 0; i < Propietarios.Tables["Datos"].Rows.Count; i++)
            //{
            //    if (i == 0)
            //        PropietariosCaratula = Propietarios.Tables["Datos"].Rows[i]["Propietarios"].ToString();
            //    else if (i == 1)
            //        PropietariosCaratula = PropietariosCaratula + ", " + Propietarios.Tables["Datos"].Rows[i]["Propietarios"].ToString();
            //    else if (i == 2)
            //    {
            //        PropietariosCaratula = PropietariosCaratula + " y otros.";
            //        break;
            //    }

            //}
            //Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Solicitante"] = PropietariosCaratula;
            //Propietarios.Clear();
            
            DataSet Representantes = Get_Representantes_Caratula(GestionId);
            string RepresentantesCaratula = "";
            for (int i = 0; i < Representantes.Tables["Datos"].Rows.Count; i++)
            {
                if (i == 0)
                    RepresentantesCaratula = Representantes.Tables["Datos"].Rows[i]["Representantes"].ToString();
                else if (i == 1)
                    RepresentantesCaratula = RepresentantesCaratula + ", " + Representantes.Tables["Datos"].Rows[i]["Representantes"].ToString();
                else if (i == 2)
                {
                    RepresentantesCaratula = RepresentantesCaratula + " y otros.";
                    break;
                }

            }
            Ds_GestionIncompleta.Tables["Dt_Caratula"].Rows[0]["Representante"] = RepresentantesCaratula;
            Representantes.Clear();

            return Ds_GestionIncompleta;

        }

        public void Insert_Admision_Gestion_Profesional(int GestionId, string No_Serie, int No_Factura, int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Admision_Gestion_Profesional", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@No_Serie", SqlDbType.VarChar, 50).Value = No_Serie;
                cmd.Parameters.Add("@No_Factura", SqlDbType.Int).Value = No_Factura;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Datos_Adicionales_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Adicionales_Gestion", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Adicionales_Gestion_Manejo(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Adicionales_Gestion_Manejo", cn);
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
                return ds;
            }
        }

        public DataSet Get_Juridicos_SubRegion(int SubRegionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Juridicos_SubRegion", cn);
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

        public DataSet Get_Tecnicos_SubRegion(int SubRegionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Tecnicos_SubRegion", cn);
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

        public DataSet Impresion_Providencia_Datos(int Tipo, int Id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Impresion_Providencia_Datos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
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

        public DataSet Get_Datos_Persona(int Tipo, int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Persona", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet ImpresionProvidenciaGestion(int Tipo, int Id, string Asunto, string Cuerpo, int UsuarioId, int UsuarioRecibeId)
        {

            if (Tipo == 1)
            {
                ClUtilitarios = new Cl_Utilitarios();
                string[] Items = Cuerpo.Split('|');
                DataSet dsDatos = Impresion_Providencia_Datos(Tipo, Id);
                string SubRegion = dsDatos.Tables["DATOS"].Rows[0]["Sub_Region"].ToString();
                string NoExpediente = dsDatos.Tables["DATOS"].Rows[0]["No_Expediente"].ToString();
                string Fecha = dsDatos.Tables["DATOS"].Rows[0]["Fecha"].ToString();
                dsDatos.Clear();
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                string PersonaElabora = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string Nombre_SubRegion = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                DataSet dsUsuarioRecibe = Get_Datos_Persona(2, UsuarioRecibeId);
                string Persona_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string Nombre_SubRegion_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuarioRecibe.Clear();
                Ds_Gestiones Ds_Providencia_Gestion = new Ds_Gestiones();
                Ds_Providencia_Gestion.Tables["Dt_Providencia"].Clear();
                
                for (int i = 0;i < Items.Length; i++)
                {
                    DataRow row = Ds_Providencia_Gestion.Tables["Dt_Providencia"].NewRow();
                    row["SubRegion"] = SubRegion;
                    row["No_Expediente"] = NoExpediente;
                    row["Fecha"] = Fecha;
                    row["Asunto"] = Asunto;
                    row["No_Providencia"] = "";
                    row["ItemCuerpo"] = Items[i];
                    row["Persona_Elabora"] = PersonaElabora;
                    row["Puesto_Elabora"] = PuestoElabora;
                    row["Nombre_SubRegion"] = Nombre_SubRegion;
                    row["Persona_Recibe"] = Persona_Recibe;
                    row["Puesto_Recibe"] = Puesto_Recibe;
                    row["Nombre_SubRegion_Recibe"] = Nombre_SubRegion_Recibe;
                    row["LetraRomana"] = ClUtilitarios.DevuelveRomano(i+1);
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows.Add(row);
                    
                }
                return Ds_Providencia_Gestion;
                
             
            }
            else
            {
                ClUtilitarios = new Cl_Utilitarios();
                DataSet dsDatos = Impresion_Providencia_Datos(Tipo, Id);
                Ds_Gestiones Ds_Providencia_Gestion = new Ds_Gestiones();
                int UsuarioElabora = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioId"]);
                int UsuarioRecibe = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Usuario_RecibeId"]);
                for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Providencia_Gestion.Tables["Dt_Providencia"].NewRow();
                    row["SubRegion"] = dsDatos.Tables["Datos"].Rows[i]["Sub_Region"];
                    row["No_Expediente"] = dsDatos.Tables["Datos"].Rows[i]["No_Expediente"];
                    row["Fecha"] = dsDatos.Tables["Datos"].Rows[i]["Fecha"];
                    row["Asunto"] = dsDatos.Tables["Datos"].Rows[i]["Asunto"];
                    row["No_Providencia"] = dsDatos.Tables["Datos"].Rows[i]["No_Providencia"];
                    row["ItemCuerpo"] = dsDatos.Tables["Datos"].Rows[i]["Punto"];
                    row["Persona_Elabora"] = "";
                    row["Puesto_Elabora"] = "";
                    row["Nombre_SubRegion"] = "";
                    row["Persona_Recibe"] = "";
                    row["Puesto_Recibe"] = "";
                    row["Nombre_SubRegion_Recibe"] = "";
                    row["LetraRomana"] = ClUtilitarios.DevuelveRomano(Convert.ToInt32(dsDatos.Tables["Datos"].Rows[i]["Orden"]));
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows.Add(row);
                }
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioElabora);
                string PersonaElabora = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string Nombre_SubRegion = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                DataSet dsUsuarioRecibe = Get_Datos_Persona(2, UsuarioRecibe);
                string Persona_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string Nombre_SubRegion_Recibe = dsUsuarioRecibe.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuarioRecibe.Clear();
                for (int j = 1; j < Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows.Count; j++)
                {
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Persona_Elabora"] = PersonaElabora;
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Puesto_Elabora"] = PuestoElabora;
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Nombre_SubRegion"] = Nombre_SubRegion;
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Persona_Recibe"] = Persona_Recibe;
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Puesto_Recibe"] = Puesto_Recibe;
                    Ds_Providencia_Gestion.Tables["Dt_Providencia"].Rows[j]["Nombre_SubRegion_Recibe"] = Nombre_SubRegion_Recibe;
                }
                    return Ds_Providencia_Gestion;
            }
        }

        public void Insert_Providencia_Exp(string SubRegion, int GestionId, string Asunto, XmlDocument Cuerpo, int UsuarioId, int UsuarioRecibeId, int SubRegionId, int Usuario_recibeTecnicoId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Providencia_Exp", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubRegion", SqlDbType.VarChar,4).Value = SubRegion;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Asunto", SqlDbType.Text).Value = Asunto;
                cmd.Parameters.Add("@Cuerpo", SqlDbType.Xml).Value = Cuerpo.OuterXml.ToString(); ;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Usuario_RecebieId", SqlDbType.Int).Value = UsuarioRecibeId;
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = SubRegionId;
                if (Usuario_recibeTecnicoId == 0)
                    cmd.Parameters.Add("@Usuario_recibeTecnicoId", SqlDbType.Int).Value =  DBNull.Value;
                else
                    cmd.Parameters.Add("@Usuario_recibeTecnicoId", SqlDbType.Int).Value = Usuario_recibeTecnicoId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public int Max_Providencia()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Providencia", cn);
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


        public int Max_ResolucionAdmision()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_ResolucionAdmision", cn);
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

        public int Get_No_Providencia(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_No_Providencia", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public int Get_Ditamen_JuridicoId(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Ditamen_JuridicoId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public int Get_ResolucionId(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_ResolucionId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public DataSet ImpresionDictamenJuridicoGestion(int Tipo, int Id, int UsuarioSubRegionalId, string No_Expediente, string Asunto, string Antecedente, string Considera, string Opinion, int UsuarioJuridicoId, string Solicitante, string Solicitud, string Registro, DataSet dsTemp, string IntroAnalisis)
        {
            if (Tipo == 1)
            {
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioSubRegionalId);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                dsUsuario.Clear();
                DataSet dsUsuarioJuridico = Get_Datos_Persona(2, UsuarioJuridicoId);
                string NomJuridico = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string No_Region = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Ubicacion = dsUsuario.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                dsUsuarioJuridico.Clear();
                Ds_Gestiones Ds_Dictamen_Juridico_Gestion = new Ds_Gestiones();
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Clear();
                DataRow row = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].NewRow();
                row["No_Dictamen"] = "";
                row["No_Region"] = No_Region;
                row["Fecha"] = DateTime.Now;
                row["Ubicacion_Region"] = Ubicacion;
                row["SubRegional"] = NomSubRegional;
                row["Puesto"] = Puesto;
                row["Sub_Region_SubRegional"] = SubRegion_SubRegional;
                row["NoExpediente"] = No_Expediente;
                row["Asunto"] = Asunto;
                row["Antecedente"] = Antecedente;
                row["Considera"] = Considera;
                row["Opinion"] = Opinion;
                row["Juridico"] = NomJuridico;
                row["Solicitante"] = Solicitante;
                row["Solicitud"] = Solicitud;
                row["Registro"] = Registro;
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows.Add(row);
                
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Articulo_Dictamen_Juridico"].Clear();
                for (int i = 0; i < dsTemp.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Count; i++)
                {
                   DataRow rowArticulo = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Articulo_Dictamen_Juridico"].NewRow();
                   rowArticulo["Articulo"] = dsTemp.Tables["Dt_Articulo_Dictamen_Juridico"].Rows[i]["Articulo"];
                   Ds_Dictamen_Juridico_Gestion.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Add(rowArticulo);
                }
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Analisis_Dictamen_Juridico"].Clear();
                for (int j = 0; j < dsTemp.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Count; j++)
                {
                    
                    DataRow rowAnalisis = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Analisis_Dictamen_Juridico"].NewRow();
                    rowAnalisis["IntroAnalisis"] = IntroAnalisis;
                    rowAnalisis["Anlisis"] = dsTemp.Tables["Dt_Analisis_Dictamen_Juridico"].Rows[j]["Analisis"];
                    rowAnalisis["No"] = j + 1;
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Add(rowAnalisis);
                }

                return Ds_Dictamen_Juridico_Gestion;
                
            }
            else if (Tipo == 2)
            {

                DataSet dsDatos = Datos_Impresion_Dictamen_Juridico(Id);
                Ds_Gestiones Ds_Dictamen_Juridico_Gestion = new Ds_Gestiones();
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Clear();
                DataRow row = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].NewRow();
                row["No_Dictamen"] = dsDatos.Tables["Datos"].Rows[0]["No_Dictamen"];
                row["No_Region"] = dsDatos.Tables["Datos"].Rows[0]["No_Region"];
                row["Fecha"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                row["Ubicacion_Region"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"];
                row["SubRegional"] = dsDatos.Tables["Datos"].Rows[0]["nombresubregionl"]; 
                row["Puesto"] = "";
                row["Sub_Region_SubRegional"] = "";
                row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                row["Asunto"] = dsDatos.Tables["Datos"].Rows[0]["Asunto"];
                row["Antecedente"] = dsDatos.Tables["Datos"].Rows[0]["Antecedente"];
                row["Considera"] = dsDatos.Tables["Datos"].Rows[0]["Considera"];
                row["Opinion"] = dsDatos.Tables["Datos"].Rows[0]["Opinion"];
                row["Juridico"] = dsDatos.Tables["Datos"].Rows[0]["nombrejuridico"];
                row["Solicitante"] = dsDatos.Tables["Datos"].Rows[0]["Solicitante"];
                row["Solicitud"] = Solicitud;
                row["Registro"] = Registro;
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows.Add(row);
                int ModuloId = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["ModuloId"]);
                int GestionId = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["GestionId"]);
                
                DataSet dsUsuario = Get_Datos_Persona(1, Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioSubregionalId"]));
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                dsUsuario.Clear();
                
                dsDatos.Clear();

                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Puesto"] = Puesto;
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Sub_Region_SubRegional"] = SubRegion_SubRegional;

                ClUtilitarios = new Cl_Utilitarios();
                ClManejo = new Cl_Manejo();
                
                if (ModuloId == 3)
                {
                    string ActividadRNF = Get_Actividad_Profesional(GestionId);
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Solicitud"] = "solicita inscripción en el Registro Nacional Forestal de " + ActividadRNF;
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Registro"] = ActividadRNF;
                    
                }
                else if (ModuloId == 2)
                {
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, ModuloId);
                    int CategoriaId = Get_CategoriaManejoId(SubCategoriaId);
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Solicitud"] = "solicita inscripción del Plan de Manejo Forestal " + Get_SubCategoriaManejo(SubCategoriaId);
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Registro"] = "";
                    Solicitante = "";
                    Solicitante = Get_Propietarios_Manejo(Convert.ToInt32(GestionId));
                    string AgraegadoSol = Get_CompletaPropietarios(CategoriaId, GestionId, ModuloId);
                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["Solicitante"] = Solicitante;
                }
                string[] ParteExpediente = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["NoExpediente"].ToString().Split('-');
                string No_ExpedienteLetras = "Número ";
                string Cod_SubCategoriaLetras = "";
                for (int i = 0; i < ParteExpediente.Length; i++)
                {
                    if (i == 0)
                        No_Expediente = ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                    else if (i == 1)
                        No_Expediente = No_Expediente + " guion " + ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                    else if (i == 2)
                    {
                        string[] Codigo_Subcategoria = ParteExpediente[i].Split('.');
                        for (int j = 0; j < Codigo_Subcategoria.Length; j++)
                        {
                            if (j == 0)
                                Cod_SubCategoriaLetras = ClUtilitarios.enletras(Codigo_Subcategoria[j]).ToLower();
                            else
                                Cod_SubCategoriaLetras = Cod_SubCategoriaLetras + " punto " + ClUtilitarios.enletras(Codigo_Subcategoria[j]).ToLower();
                        }
                        No_Expediente = No_Expediente + " guion " + Cod_SubCategoriaLetras;
                    }
                    else if (i == 3)
                        No_Expediente = No_Expediente + " guion " + ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                }
                Ds_Dictamen_Juridico_Gestion.Tables["Dt_Dictamen_Juridico"].Rows[0]["NoExpediente"] = No_Expediente;

                DataSet dsArticulo_Dictamen_Jur = Get_ArticulosXml_Dictamen_juridico(Id);
                for (int i = 0; i < dsArticulo_Dictamen_Jur.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowArticulo = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Articulo_Dictamen_Juridico"].NewRow();
                    rowArticulo["Articulo"] = dsArticulo_Dictamen_Jur.Tables["Datos"].Rows[i]["Articulo"];
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Add(rowArticulo);
                }
                dsArticulo_Dictamen_Jur.Clear();

                DataSet dsAnalisis_Dictamen_Jur = Get_AnalisisXml_Dictamen_juridico(Id);
                for (int j = 0; j < dsAnalisis_Dictamen_Jur.Tables["Datos"].Rows.Count; j++)
                {

                    DataRow rowAnalisis = Ds_Dictamen_Juridico_Gestion.Tables["Dt_Analisis_Dictamen_Juridico"].NewRow();
                    rowAnalisis["IntroAnalisis"] = dsAnalisis_Dictamen_Jur.Tables["Datos"].Rows[j]["IntroAnalisis"];
                    rowAnalisis["Anlisis"] = dsAnalisis_Dictamen_Jur.Tables["Datos"].Rows[j]["Analisis"];
                    rowAnalisis["No"] = Convert.ToInt32(dsAnalisis_Dictamen_Jur.Tables["Datos"].Rows[j]["Orden"]) + 1;
                    Ds_Dictamen_Juridico_Gestion.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Add(rowAnalisis);
                }
                dsAnalisis_Dictamen_Jur.Clear();
                
                return Ds_Dictamen_Juridico_Gestion;
            }
            else
                return ds;
                
        }



        public int Get_UsuarioSubRegional_Providencia(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_UsuarioSubRegional_Providencia", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public int Get_UsuarioSubRegional_Resolucion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_UsuarioSubRegional_Resolucion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public DataSet Get_Datos_Solicitante(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_Solicitante", cn);
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
                return ds;
            }
        }

        public DataSet Get_ArticulosXml_Dictamen_juridico(int Dictamen_Juridico_id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_ArticulosXml_Dictamen_juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dictamen_Juridico_id", OleDbType.Integer).Value = Dictamen_Juridico_id;
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

        public DataSet Get_AnalisisXml_Dictamen_juridico(int Dictamen_Juridico_id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_AnalisisXml_Dictamen_juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dictamen_Juridico_id", OleDbType.Integer).Value = Dictamen_Juridico_id;
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

        public void Insert_Dictamen_Juridico(int GestionId, string Asunto, string Antecedente, XmlDocument Articulo, string IntroAnalisis, XmlDocument Analisis, int ConsiderandoId, int OpinionId, int UsuarioId, int RegionId, XmlDocument Enmiendas)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Dictamen_Juridico", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Asunto", SqlDbType.VarChar, 50).Value = Asunto;
                cmd.Parameters.Add("@Antecedente", SqlDbType.VarChar, 50).Value = Antecedente;
                cmd.Parameters.Add("@Articulo", SqlDbType.Xml).Value = Articulo.OuterXml.ToString();
                cmd.Parameters.Add("@IntroAnalisis", SqlDbType.VarChar, 50).Value = IntroAnalisis;
                cmd.Parameters.Add("@Analisis", SqlDbType.Xml).Value = Analisis.OuterXml.ToString();
                cmd.Parameters.Add("@ConsiderandoId", SqlDbType.Int).Value = ConsiderandoId;
                cmd.Parameters.Add("@OpinionId", SqlDbType.Int).Value = OpinionId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@RegionId", SqlDbType.Int).Value = RegionId;
                if (Enmiendas.ToString() == "")
                    cmd.Parameters.Add("@Enmiendas", SqlDbType.Xml).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Enmiendas", SqlDbType.Xml).Value = Enmiendas.OuterXml.ToString();
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_Resolucion(int GestionId, int UsuarioId, int EstatusResolucionId, int SubRegionId, string No_Subregion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Resolucion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@EstatusResolucionId", SqlDbType.Int).Value = EstatusResolucionId;
                cmd.Parameters.Add("@SubRegion", SqlDbType.Int).Value = SubRegionId;
                cmd.Parameters.Add("@No_Subregion", SqlDbType.VarChar, 50).Value = No_Subregion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public int Max_Resolucion()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Resolucion", cn);
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

        public int Max_Dictamen_Juridico()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Dictamen_Juridico", cn);
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

        public int Max_Oficio_Dictamen_Juridico()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Oficio_Dictamen_Juridico", cn);
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

        public DataSet Datos_Impresion_Dictamen_Juridico(int DictamenJuridicoId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Impresion_Dictamen_Juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dictamen_Juridico_id", OleDbType.Integer).Value =DictamenJuridicoId;
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

        public string Get_Actividad_Profesional(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Actividad_Profesional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Tiene_Enmiendas_Dictamen_Juridico(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Tiene_Enmiendas_Dictamen_Juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public DataSet Datos_Impresion_Oficio_Enmiendas(int Tipo, int Id, int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Impresion_Oficio_Enmiendas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Datos_ConstanciaRRF_Registro(int Tipo, int Id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_ConstanciaRRF_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
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

        public DataSet Datos_Caratula(int AdmisionGestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Caratula", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Admision_GestionId", OleDbType.Integer).Value = AdmisionGestionId;
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

        

        public DataSet Datos_Oficio_Devolucion(int OficioDevolucion)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Oficio_Devolucion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OficioDevolucionId", OleDbType.Integer).Value = OficioDevolucion;
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

        public DataSet Datos_Impresion_Resolucion_Aprobacion(int Tipo, int Id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Impresion_Resolucion_Aprobacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
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

        public DataSet Datos_Impresion_Resolucion_Admision(int Tipo, int Id, int CategoriaId)  //Tipo 1VP  2Resolucion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Resolucion_Admision", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
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

        public DataSet ImpresionOficioEnmiendasJuridico(int Tipo, int Id, int UsuarioId)
        {
            if (Tipo == 1)
            {
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_Region = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                DataSet dsDatos = Datos_Impresion_Oficio_Enmiendas(Tipo,Id,UsuarioId);
                Ds_Gestiones Ds_Oficio_Enmienda_Gestion = new Ds_Gestiones();
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Clear();
                DataRow row = Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].NewRow();
                row["Oficio_No"] = "";
                row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"];
                row["FechaOficio"] = DateTime.Now;
                row["Solicitante"] = dsDatos.Tables["Datos"].Rows[0]["Solicitante"]; 
                row["No_Expediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                row["No_Dictamen_Juridico"] = dsDatos.Tables["Datos"].Rows[0]["No_Dictamen"];
                row["SubRegional"] = NomSubRegional;
                row["Puesto_SubRegional"] = Puesto;
                row["No_SubRegion"] = SubRegion_SubRegional;
                row["Nombre_SubRegion"] = Nombre_Region;
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows.Add(row);

                DataSet dsTemp = Get_EnmiendasXml_Dictamen_juridico(Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Dictamen_JuridicoId"]));
                
                Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].Clear();
                for (int i = 0; i < dsTemp.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowArticulo = Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].NewRow();
                    rowArticulo["Enmienda"] = dsTemp.Tables["Datos"].Rows[i]["Enmienda"];
                    Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].Rows.Add(rowArticulo);
                }
                dsDatos.Clear();
                dsTemp.Clear();
                return Ds_Oficio_Enmienda_Gestion;
            }
            else if (Tipo == 2)
            {
                DataSet dsDatos = Datos_Impresion_Oficio_Enmiendas(2, Id, UsuarioId);
                Ds_Gestiones Ds_Oficio_Enmienda_Gestion = new Ds_Gestiones();
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Clear();
                DataRow row = Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].NewRow();
                row["Oficio_No"] = dsDatos.Tables["Datos"].Rows[0]["No_Oficio"];
                row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"];
                row["FechaOficio"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                row["Solicitante"] = dsDatos.Tables["Datos"].Rows[0]["Solicitante"];
                row["No_Expediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                row["No_Dictamen_Juridico"] = dsDatos.Tables["Datos"].Rows[0]["No_Dictamen"];
                row["SubRegional"] = "";
                row["Puesto_SubRegional"] = "";
                row["No_SubRegion"] = "";
                row["Nombre_SubRegion"] = "";
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows.Add(row);
                int Dictamen_JuridicoId = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Dictamen_JuridicoId"]);
                int UsuarioIdSubregional = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioId"]);
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioIdSubregional);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_Region = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows[0]["SubRegional"] = NomSubRegional;
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows[0]["Puesto_SubRegional"] = Puesto;
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows[0]["No_SubRegion"] = SubRegion_SubRegional;
                Ds_Oficio_Enmienda_Gestion.Tables["Dt_Enmiendas_Juridicas"].Rows[0]["Nombre_SubRegion"] = Nombre_Region;
                DataSet dsTemp = Get_EnmiendasXml_Dictamen_juridico(Dictamen_JuridicoId);

                Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].Clear();
                for (int i = 0; i < dsTemp.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowArticulo = Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].NewRow();
                    rowArticulo["Enmienda"] = dsTemp.Tables["Datos"].Rows[i]["Enmienda"];
                    Ds_Oficio_Enmienda_Gestion.Tables["dt_Detalle_Enmiendas"].Rows.Add(rowArticulo);
                }
                dsTemp.Clear();
                dsDatos.Clear();

                return Ds_Oficio_Enmienda_Gestion;
            }
            else
                return ds;

        }

        public DataSet ImpresionResolucion_Admision(int Tipo, int Id, int UsuarioId, int CategoriaId)
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();

            string AgraegadoSol = "";
            string Solicitante = "";
            int ModuloId = 0;
            int IdGestion = 0;
            if (Tipo  == 1)
                ModuloId = SP_Get_Modulo_Gestion(Id);
            else
            {
                IdGestion = ClManejo.Get_GestionId_Resolucion_Admision_Expediente(Id);
                ModuloId = SP_Get_Modulo_Gestion(IdGestion);
            }
                
            if (ModuloId == 3)
            {
                Solicitante = Get_Propietarios_Gestion_Registro(Id, CategoriaId);
                
            }
            else if (ModuloId == 2)
                Solicitante = Get_Propietarios_Manejo(Id);
            if (Tipo == 1)
                AgraegadoSol = Get_CompletaPropietarios(CategoriaId, Id, ModuloId);
            else
            {
                IdGestion = ClManejo.Get_GestionId_Resolucion_Admision_Expediente(Id);
                AgraegadoSol = Get_CompletaPropietarios(CategoriaId, IdGestion, ModuloId);
            }

            if (AgraegadoSol != "")
                Solicitante = Solicitante + " " + AgraegadoSol + ".";
            else
                Solicitante = Solicitante + ".";


            if (ModuloId == 3)
            {
                if (Tipo == 1)
                {
                    if ((CategoriaId == 2) || (CategoriaId == 3) || (CategoriaId == 4) || (CategoriaId == 6) || (CategoriaId == 1))
                    {

                        string Fincas = GetDatosFinca_Gestion_Juntos(Id);
                        string Representantes = GetNombresRepresentantes_Gestion_Juntos(Id);
                        int TipoPersona = Get_Tipo_Persona_Fincas(2, Id);
                        string Propietarios = GetNombresPropietarios_Gestion_Juntos(Id, TipoPersona);
                        DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                        string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                        string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                        dsUsuario.Clear();
                        DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                        Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                        DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                        row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                        row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                        row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                        string Asunto = Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Asunto = Asunto + " representado por: " + Representantes;
                        if (CategoriaId == 2)
                            Asunto = Asunto + " Solicita (n): aprobación de registro de plantación Voluntaria en ";
                        else if (CategoriaId == 3)
                            Asunto = Asunto + " Solicita (n): aprobación de registro de plantación de Árboles frutales en ";
                        else if (CategoriaId == 4)
                            Asunto = Asunto + " Solicita (n): aprobación de registro de plantación de Sistemas Agroforestales en ";
                        else if (CategoriaId == 6)
                            Asunto = Asunto + " Solicita (n): aprobación de registro de plantación de Fuentes Semilleras y de Material Vegetativo en ";
                        Asunto = Asunto + Fincas;
                        row["Asunto"] = Asunto;
                        row["No_Resolucion"] = "-----";
                        string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Cuerpo_Resolucion = Cuerpo_Resolucion + " representado por: " + Representantes;
                        Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                        row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                        string ConsiderandoUno = "Que " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoUno = ConsiderandoUno + " representado por: " + Representantes;
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " en " + Fincas + ", quien ha cumplido con los requisitos de ley establecidos.";
                        row["ConsiderandoUno"] = ConsiderandoUno;
                        string ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoDos = ConsiderandoDos + " representado por: " + Representantes;
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        row["ConsiderandoDos"] = ConsiderandoDos;
                        row["SubRegional"] = NomSubRegional;
                        row["PuestoSubRegional"] = Puesto;
                        row["Solicitante"] = Solicitante;
                        row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                        row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                        dsDatos.Clear();
                        return Ds_Resolucion_Admision;
                    }
                    else if ((CategoriaId == 5) || (CategoriaId == 8))
                    {

                        string Representantes = GetNombresRepresentantes_Gestion_Juntos(Id);
                        DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                        string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                        string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();


                        dsUsuario.Clear();
                        DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                        int TipoPersona = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_PersonaId"]);
                        string Propietarios = GetNombresPropietarios_Gestion_Juntos_SinFinca(Id, TipoPersona, CategoriaId);

                        Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                        DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                        row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                        row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                        row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                        string Asunto = Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Asunto = Asunto + " Representado legalmente por: " + Representantes;
                        Asunto = Asunto + " Solicita (n): aprobación de inscripción de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"] + " en la subcategoría de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + "  ubicado en " + dsDatos.Tables["Datos"].Rows[0]["Direccion"] + " en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ".";
                        row["Asunto"] = Asunto;
                        row["No_Resolucion"] = "-----";
                        string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Cuerpo_Resolucion = Cuerpo_Resolucion + " representado legalmente por " + Representantes;
                        Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                        row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                        string ConsiderandoUno = "Que " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " ubicado en " + dsDatos.Tables["Datos"].Rows[0]["Direccion"] + " en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ", quien ha cumplido con los requisitos de ley establecidos.";
                        row["ConsiderandoUno"] = ConsiderandoUno;
                        string ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + Representantes;
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        row["ConsiderandoDos"] = ConsiderandoDos;
                        row["SubRegional"] = NomSubRegional;
                        row["PuestoSubRegional"] = Puesto;
                        row["Solicitante"] = Solicitante;
                        row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                        row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                        dsDatos.Clear();
                        return Ds_Resolucion_Admision;
                    }
                    else if (CategoriaId == 7)
                    {
                        DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                        string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                        string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                        dsUsuario.Clear();
                        DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                        Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                        DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                        row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                        row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                        row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                        string Asunto = dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        Asunto = Asunto + " solicita su INSCRIPCIÓN en el Registro nacional Forestal como : " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + ".";
                        //if (dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString() != "")
                        //Asunto = Asunto + " Representado legalmente por: " + dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString();
                        row["Asunto"] = Asunto;
                        row["No_Resolucion"] = "-----";
                        string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString() + ".";
                        //if (dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString() != "")
                        //Cuerpo_Resolucion = Cuerpo_Resolucion + " Representado legalmente por: " + dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString();
                        row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                        string ConsiderandoUno = "Que " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        //if (dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString() != "")
                        //ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString();
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para su inscripción en el Registro Nacional Forestal como:  " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + ", quien ha cumplido con los requisitos de ley establecidos.";
                        row["ConsiderandoUno"] = ConsiderandoUno;
                        string ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        //if (dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString() != "")
                        //ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + dsDatos.Tables["Datos"].Rows[0]["Representante"].ToString();
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        row["ConsiderandoDos"] = ConsiderandoDos;
                        row["SubRegional"] = NomSubRegional;
                        row["PuestoSubRegional"] = Puesto;
                        row["Solicitante"] = Solicitante;
                        row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                        row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                        dsDatos.Clear();
                        return Ds_Resolucion_Admision;
                    }
                    else if (CategoriaId == 9)
                    {

                        DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                        string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                        string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                        dsUsuario.Clear();
                        DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                        Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                        DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                        row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                        row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                        row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                        string Asunto = "";
                        int TipoPersona = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_PersonaId"]);
                        string Propietarios = GetNombresPropietarios_Gestion_Juntos_SinFinca(Id, TipoPersona, CategoriaId);

                        string Representantes = GetNombresRepresentantes_Gestion_Juntos(Id);
                        if (dsDatos.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "15")
                        {
                            Asunto = Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Asunto = Asunto + " Representado legalmente por: " + Representantes;
                            Asunto = Asunto + " Solicita: aprobación de inscripción de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"] + " en la subcategoría de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " de la empresa " + dsDatos.Tables["Datos"].Rows[0]["Nombre"] + ", ubicada en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ".";
                            row["Asunto"] = Asunto;
                            row["No_Resolucion"] = "-----";
                            string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante + ".";
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Cuerpo_Resolucion = Cuerpo_Resolucion + " Representado legalmente por: " + Representantes;
                            row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                            string ConsiderandoUno = "Que " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                            ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para su inscripción en el Registro Nacional Forestal como:  " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " de la empresa denominada " + dsDatos.Tables["Datos"].Rows[0]["Nombre"] + ", ubicada en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ". Ha cumplido con los requisitos de ley establecidos.";
                            row["ConsiderandoUno"] = ConsiderandoUno;
                        }
                        else if (dsDatos.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "11")
                        {
                            Asunto = Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Asunto = Asunto + " Representado legalmente por: " + Representantes;
                            Asunto = Asunto + " Solicita: aprobación de inscripción de una MOTOSIERRA, marca: " + dsDatos.Tables["Datos"].Rows[0]["Marca"] + ", modelo " + dsDatos.Tables["Datos"].Rows[0]["Modelo"] + " serie No. " + dsDatos.Tables["Datos"].Rows[0]["Serie"] + ".";
                            row["Asunto"] = Asunto;
                            row["No_Resolucion"] = "-----";
                            string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante + ".";
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Cuerpo_Resolucion = Cuerpo_Resolucion + " Representado legalmente por: " + Representantes;
                            row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                            string ConsiderandoUno = "Que " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                            ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para su inscripción de una MOTOSIERRA marca:  " + dsDatos.Tables["Datos"].Rows[0]["marca"] + ", modelo " + dsDatos.Tables["Datos"].Rows[0]["Modelo"] + ", serie " + dsDatos.Tables["Datos"].Rows[0]["Serie"] + ".";
                            row["ConsiderandoUno"] = ConsiderandoUno;
                        }



                        string ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + Representantes;
                        row["ConsiderandoDos"] = ConsiderandoDos;
                        row["SubRegional"] = NomSubRegional;
                        row["PuestoSubRegional"] = Puesto;
                        row["Solicitante"] = Solicitante;
                        row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                        row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                        Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                        dsDatos.Clear();
                        return Ds_Resolucion_Admision;
                    }

                    else
                    {
                        return ds;
                    }
                }
                else
                {
                    DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                    Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                    DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                    row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                    row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                    row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                    row["Asunto"] = dsDatos.Tables["Datos"].Rows[0]["Asunto"];
                    row["No_Resolucion"] = "-----";
                    row["Cuerpo_Resolucion"] = dsDatos.Tables["Datos"].Rows[0]["No_Resolucion"] + ".  " + dsDatos.Tables["Datos"].Rows[0]["Cuerpo_Resolucion"];
                    row["ConsiderandoUno"] = dsDatos.Tables["Datos"].Rows[0]["ConsiderandoUno"];
                    row["ConsiderandoDos"] = dsDatos.Tables["Datos"].Rows[0]["ConsiderandoDos"];
                    row["SubRegional"] = dsDatos.Tables["Datos"].Rows[0]["SubRegional"]; ;
                    row["PuestoSubRegional"] = dsDatos.Tables["Datos"].Rows[0]["nombre"]; ;
                    row["Solicitante"] = Solicitante;
                    row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                    row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                    dsDatos.Clear();
                    return Ds_Resolucion_Admision;
                }
            }
            else if (ModuloId == 2)
            {
                if (Tipo == 1)
                {
                    string Fincas = GetDatosFinca_Gestion_Juntos(Id);
                    string Representantes = GetNombresRepresentantes_Gestion_Juntos(Id);
                    int TipoPersona = Get_Tipo_Persona_Fincas(3, Id);
                    string Propietarios = GetNombresPropietarios_Gestion_Juntos(Id, TipoPersona);
                    DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                    string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                    string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                    dsUsuario.Clear();
                    DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                    Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                    DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                    row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                    row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                    row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                    string Asunto = Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        Asunto = Asunto + " representado por: " + Representantes;
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Id, 2, ModuloId);

                    Asunto = Asunto + " Solicita (n): aprobación de licencia de  " + Get_SubCategoriaManejo(SubCategoriaId) + " en";
                    Asunto = Asunto + Fincas;
                    row["Asunto"] = Asunto;
                    row["No_Resolucion"] = "-----";
                    string Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        Cuerpo_Resolucion = Cuerpo_Resolucion + " representado por: " + Representantes;
                    Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                    row["Cuerpo_Resolucion"] = Cuerpo_Resolucion;
                    string ConsiderandoUno = "Que " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        ConsiderandoUno = ConsiderandoUno + " representado por: " + Representantes;
                    ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para Manejo Forestal de " + dsDatos.Tables["Datos"].Rows[0]["SubCategoria"] + " en " + Fincas + ", quien ha cumplido con los requisitos de ley establecidos.";
                    row["ConsiderandoUno"] = ConsiderandoUno;
                    string ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        ConsiderandoDos = ConsiderandoDos + " representado por: " + Representantes;
                    ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                    row["ConsiderandoDos"] = ConsiderandoDos;
                    row["SubRegional"] = NomSubRegional;
                    row["PuestoSubRegional"] = Puesto;
                    row["Solicitante"] = Solicitante;
                    row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                    row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                    dsDatos.Clear();
                    return Ds_Resolucion_Admision;
                }
                else
                {
                    DataSet dsDatos = Datos_Impresion_Resolucion_Admision(Tipo, Id, CategoriaId);
                    Ds_Gestiones Ds_Resolucion_Admision = new Ds_Gestiones();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Clear();
                    DataRow row = Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].NewRow();
                    row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                    row["NoExpediente"] = dsDatos.Tables["Datos"].Rows[0]["No_Expediente"];
                    row["Fecha_Doc"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                    row["Asunto"] = dsDatos.Tables["Datos"].Rows[0]["Asunto"];
                    row["No_Resolucion"] = "-----";
                    row["Cuerpo_Resolucion"] = dsDatos.Tables["Datos"].Rows[0]["No_Resolucion"] + ".  " + dsDatos.Tables["Datos"].Rows[0]["Cuerpo_Resolucion"];
                    row["ConsiderandoUno"] = dsDatos.Tables["Datos"].Rows[0]["ConsiderandoUno"];
                    row["ConsiderandoDos"] = dsDatos.Tables["Datos"].Rows[0]["ConsiderandoDos"];
                    row["SubRegional"] = dsDatos.Tables["Datos"].Rows[0]["SubRegional"]; ;
                    row["PuestoSubRegional"] = dsDatos.Tables["Datos"].Rows[0]["nombre"]; ;
                    row["Solicitante"] = Solicitante;
                    row["Nombre_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                    row["Ubicacion_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString();
                    Ds_Resolucion_Admision.Tables["Dt_Resolucion_Admision"].Rows.Add(row);
                    dsDatos.Clear();
                    return Ds_Resolucion_Admision;
                }
            }
            else
                return ds;
            
        }

        public DataSet ImpresionResolucion_Aprobacion(int Tipo, int Id, int UsuarioId, int EstatusResolucionId)
        {
            ClUtilitarios = new Cl_Utilitarios();
            if (Tipo == 1)
            {
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioId);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_Region = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                DataSet dsDatos = Datos_Impresion_Resolucion_Aprobacion(Tipo, Id);
                Ds_Gestiones Ds_Resolucion_Aprobacion = new Ds_Gestiones();
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Clear();
                DataRow row = Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].NewRow();
                row["NoResolucion"] = "";
                row["No_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                string[] Ubicacion = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString().Split(',');
                row["Municipio"] = Ubicacion[0].Trim();
                row["Departamento"] = Ubicacion[1].Trim();
                row["Fecha"] = DateTime.Now;
                row["Solicitante"] = dsDatos.Tables["Datos"].Rows[0]["Solicitante"];
                row["DPI"] = dsDatos.Tables["Datos"].Rows[0]["DPI"];
                row["Categoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"];
                row["SubCategoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Subcategoria"];
                row["No_DictamenJuridico"] = dsDatos.Tables["Datos"].Rows[0]["No_Dictamen"];
                row["FechaDictamenJuridico"] = dsDatos.Tables["Datos"].Rows[0]["FecDictamen"];
                row["NombreJuridico"] = dsDatos.Tables["Datos"].Rows[0]["Juridico"];
                row["NombreRegion"] = dsDatos.Tables["Datos"].Rows[0]["Nombre"];
                row["NombreSubRegional"] = NomSubRegional;
                row["PuestoSubRegional"] = Puesto;
                row["NombreSubRegion"] = Nombre_Region;
                row["ModuloId"] = dsDatos.Tables["Datos"].Rows[0]["ModuloId"];
                row["DiaFecha"] = ClUtilitarios.enletras(DateTime.Now.Day.ToString());
                row["YearFecha"] = ClUtilitarios.enletras(DateTime.Now.Year.ToString());
                row["Estatus_ResolucionId"] = EstatusResolucionId;
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Rows.Add(row);
                dsDatos.Clear();
                return Ds_Resolucion_Aprobacion;
            }
            else if (Tipo == 2)
            {
                DataSet dsDatos = Datos_Impresion_Resolucion_Aprobacion(2, Id);
                Ds_Gestiones Ds_Resolucion_Aprobacion = new Ds_Gestiones();
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Clear();
                DataRow row = Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].NewRow();
                row["NoResolucion"] = dsDatos.Tables["Datos"].Rows[0]["No_Resolucion"];
                row["No_SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["Sub_Region"];
                string[] Ubicacion = dsDatos.Tables["Datos"].Rows[0]["Ubicacion"].ToString().Split(',');
                row["Municipio"] = Ubicacion[0].Trim();
                row["Departamento"] = Ubicacion[1].Trim();
                row["Fecha"] = dsDatos.Tables["Datos"].Rows[0]["FecResolucion"];
                row["Solicitante"] = dsDatos.Tables["Datos"].Rows[0]["Solicitante"];
                row["DPI"] = dsDatos.Tables["Datos"].Rows[0]["DPI"];
                row["Categoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"];
                row["SubCategoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Subcategoria"];
                row["No_DictamenJuridico"] = dsDatos.Tables["Datos"].Rows[0]["No_Dictamen"];
                row["FechaDictamenJuridico"] = dsDatos.Tables["Datos"].Rows[0]["FecDictamen"];
                row["NombreJuridico"] = dsDatos.Tables["Datos"].Rows[0]["Juridico"];
                row["NombreRegion"] = dsDatos.Tables["Datos"].Rows[0]["Nombre"];
                row["NombreSubRegional"] = "";
                row["PuestoSubRegional"] = "";
                row["NombreSubRegion"] = "";
                row["ModuloId"] = dsDatos.Tables["Datos"].Rows[0]["ModuloId"];
                row["DiaFecha"] = ClUtilitarios.enletras(dsDatos.Tables["Datos"].Rows[0]["dia"].ToString());
                row["YearFecha"] = ClUtilitarios.enletras(dsDatos.Tables["Datos"].Rows[0]["year"].ToString());
                row["Estatus_ResolucionId"] = dsDatos.Tables["Datos"].Rows[0]["Estatus_ResolucionId"];
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Rows.Add(row);
                int UsuarioIdSubregional = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioId"]);
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioIdSubregional);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_Region = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Rows[0]["NombreSubRegional"] = NomSubRegional;
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Rows[0]["PuestoSubRegional"] = Puesto;
                Ds_Resolucion_Aprobacion.Tables["Dt_Resolucion_Aprobacion"].Rows[0]["NombreSubRegion"] = Nombre_Region;
                
                dsDatos.Clear();

                return Ds_Resolucion_Aprobacion;
            }
            else
                return ds;

        }

        public DataSet Get_EnmiendasXml_Dictamen_juridico(int Dictamen_Juridico_id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_EnmiendasXml_Dictamen_juridico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dictamen_Juridico_id", OleDbType.Integer).Value = Dictamen_Juridico_id;
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

        public DataSet Get_MotivosXml_Oficio_Devolucion(int Oficio_DevolucionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_MotivosXml_Oficio_Devolucion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Oficio_DevolucionId", OleDbType.Integer).Value = Oficio_DevolucionId;
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

        public void Insert_Oficio_Enmiendas_Juridico(int GestionId, int SubRegionId, int UsuarioId, string SubRegion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Oficio_Enmiendas_Juridico", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = SubRegionId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@SubRegion", SqlDbType.VarChar, 4).Value = SubRegion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_Datos_Persona_Desde_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Datos_Person_Desde_Gestion", cn);
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
                return ds;
            }
        }

        public void Cambia_Estatus_Gestion(int GestionId, int EstatusId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Cambia_Estatus_Gestion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@EstatusId", SqlDbType.Int).Value = EstatusId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Inserta_Expediente_Resolucion(int ResolucionId, byte[] Archivo, string ContentType, string Nombre_Archivo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inserta_Expediente_Resolucion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ResolucionId", OleDbType.Integer).Value = ResolucionId;
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

        public DataSet ImpresionOficioDevolucion(int Tipo, int Id, int UsuarioSubRegionalId, DataSet dsTemp, string Dirigido, int UsuarioIdElebora, int TipoUsuarioId)
        {
            if (Tipo == 1)
            {
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioSubRegionalId);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                dsUsuario.Clear();
                DataSet dsUsuarioElabora = Get_Datos_Persona(2, UsuarioIdElebora);
                string NomElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string No_Region = "";
                string NombreRegionElabora = "";
                if (TipoUsuarioId == 4)
                {
                    No_Region = dsUsuarioElabora.Tables["Datos"].Rows[0]["No_Region"].ToString();
                    NombreRegionElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Region"].ToString();
                }
                else
                {
                    No_Region = dsUsuarioElabora.Tables["Datos"].Rows[0]["No_Region"].ToString();
                    NombreRegionElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["No_Region"].ToString();
                }
                dsUsuarioElabora.Clear();
                Ds_Gestiones Ds_Oficio_Devolucion = new Ds_Gestiones();
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Clear();
                DataRow row = Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].NewRow();
                row["Fecha"] = DateTime.Now;
                row["No_Oficio"] = "";
                row["SubRegional"] = NomSubRegional;
                row["Puesto_SubRegional"] = Puesto;
                row["No_SubRegion"] = SubRegion_SubRegional;
                row["Estimado"] = Dirigido;
                row["Regional"] = NomElabora;
                row["PuestoRegional"] = PuestoElabora;
                row["No_Region"] = No_Region;
                row["Nombre_Region"] = NombreRegionElabora;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows.Add(row);

                Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].Clear();
                for (int i = 0; i < dsTemp.Tables["Dt_Motivo_Oficio_Dev"].Rows.Count; i++)
                {
                    DataRow rowArticulo = Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].NewRow();
                    rowArticulo["Motivo"] = dsTemp.Tables["Dt_Motivo_Oficio_Dev"].Rows[i]["Motivo"];
                    Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].Rows.Add(rowArticulo);
                }

                return Ds_Oficio_Devolucion;

            }
            else if (Tipo == 2)
            {
                DataSet dsDatos = Datos_Oficio_Devolucion(Id);
                Ds_Gestiones Ds_Oficio_Devolucion = new Ds_Gestiones();
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Clear();
                DataRow row = Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].NewRow();
                row["Fecha"] = dsDatos.Tables["Datos"].Rows[0]["Fecha"];
                row["No_Oficio"] = dsDatos.Tables["Datos"].Rows[0]["No_Oficio"];
                row["SubRegional"] = "";
                row["Puesto_SubRegional"] = "";
                row["No_SubRegion"] = "";
                row["Estimado"] = dsDatos.Tables["Datos"].Rows[0]["Saludo"];
                row["Regional"] = "";
                row["PuestoRegional"] = "";
                row["No_Region"] = "";
                row["Nombre_Region"] = "";
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows.Add(row);

                int UsuarioIdSubregional = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsurioSubregional"]);
                int USuarioElaboro = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioElaboro"]);
                dsDatos.Clear();
                DataSet dsUsuario = Get_Datos_Persona(1, UsuarioIdSubregional);
                string NomSubRegional = dsUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string Puesto = dsUsuario.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string SubRegion_SubRegional = dsUsuario.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_Region = dsUsuario.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuario.Clear();
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["SubRegional"] = NomSubRegional;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["Puesto_SubRegional"] = Puesto;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["No_SubRegion"] = SubRegion_SubRegional;
                DataSet dsUsuarioElabora = Get_Datos_Persona(2, USuarioElaboro);
                string NombreElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Puesto"].ToString();
                string No_RegionElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["No_Region"].ToString();
                string Nombre_RegionElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Region"].ToString();
                dsUsuarioElabora.Clear();
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["Regional"] = NombreElabora;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["PuestoRegional"] = PuestoElabora;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["No_Region"] = No_RegionElabora;
                Ds_Oficio_Devolucion.Tables["Dt_Oficio_Dev"].Rows[0]["Nombre_Region"] = Nombre_RegionElabora;

                
                DataSet dsTempMotivos = Get_MotivosXml_Oficio_Devolucion(Id);

                Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].Clear();
                for (int i = 0; i < dsTempMotivos.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowArticulo = Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].NewRow();
                    rowArticulo["Motivo"] = dsTempMotivos.Tables["Datos"].Rows[i]["Motivo"];
                    Ds_Oficio_Devolucion.Tables["Dt_Det_Oficio_Dev"].Rows.Add(rowArticulo);
                }
                dsTempMotivos.Clear();

                return Ds_Oficio_Devolucion;
            }
            else
                return ds;

        }

        public DataSet ImpresionConstanciaRFF(int Tipo, int Id, int Usuarioid, int TipoUsuarioId, DateTime Fecha_Inscripcion, string Fecha_Actualizacion, DateTime Fecha_Vencimiento)
        {
            if (Tipo == 1)
            {
                DataSet dsUsuarioElabora = Get_Datos_Persona(2, Usuarioid);
                string NomElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Puesto"].ToString();
                dsUsuarioElabora.Clear();
                DataSet dsDatos = Datos_ConstanciaRRF_Registro(Tipo, Id);
                Ds_Gestiones Ds_Constancia_RRF = new Ds_Gestiones();
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Clear();
                DataRow row = Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].NewRow();
                row["SubCategoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"];
                row["Registro"] = "";
                row["Region"] = dsDatos.Tables["Datos"].Rows[0]["No_Region"];
                row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["No_SubRegion"];
                row["Nombre"] = dsDatos.Tables["Datos"].Rows[0]["Nombre"];
                row["DPI"] = dsDatos.Tables["Datos"].Rows[0]["DPI"];
                row["NIT"] = dsDatos.Tables["Datos"].Rows[0]["NIT"];
                row["Direccion"] = dsDatos.Tables["Datos"].Rows[0]["Direccion"] + ", " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", " + dsDatos.Tables["Datos"].Rows[0]["Departamento"];
                row["Categoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"];
                row["Profesion"] = dsDatos.Tables["Datos"].Rows[0]["Profesion"];
                row["No_Colegiado"] = dsDatos.Tables["Datos"].Rows[0]["No_Colegiado"];
                row["FechaInscripcion"] = Fecha_Inscripcion;
                row["FechaUltimaActualizacion"] = Fecha_Actualizacion;
                row["FechaVencimiento"] = Fecha_Vencimiento;
                row["Regional"] = NomElabora;
                row["Puesto"] = PuestoElabora;
                row["CategoriaProfesionId"] = dsDatos.Tables["Datos"].Rows[0]["CategoriaProfesionId"];
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Rows.Add(row);

                dsDatos.Clear();
                return Ds_Constancia_RRF;

            }
            else if (Tipo == 2)
            {
                
                DataSet dsDatos = Datos_ConstanciaRRF_Registro(Tipo, Id);
                Ds_Gestiones Ds_Constancia_RRF = new Ds_Gestiones();
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Clear();
                DataRow row = Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].NewRow();
                row["SubCategoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"];
                row["Registro"] = dsDatos.Tables["Datos"].Rows[0]["No_Registro"];
                row["Region"] = dsDatos.Tables["Datos"].Rows[0]["No_Region"];
                row["SubRegion"] = dsDatos.Tables["Datos"].Rows[0]["No_SubRegion"];
                row["Nombre"] = dsDatos.Tables["Datos"].Rows[0]["Nombre"];
                row["DPI"] = dsDatos.Tables["Datos"].Rows[0]["DPI"];
                row["NIT"] = dsDatos.Tables["Datos"].Rows[0]["NIT"];
                row["Direccion"] = dsDatos.Tables["Datos"].Rows[0]["Direccion"] + ", " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", " + dsDatos.Tables["Datos"].Rows[0]["Departamento"];
                row["Categoria"] = dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"];
                row["Profesion"] = dsDatos.Tables["Datos"].Rows[0]["Profesion"];
                row["No_Colegiado"] = dsDatos.Tables["Datos"].Rows[0]["No_Colegiado"];
                row["FechaInscripcion"] = dsDatos.Tables["Datos"].Rows[0]["FechaRegistro"];
                row["FechaVencimiento"] = dsDatos.Tables["Datos"].Rows[0]["Fecha_Vencimiento"];
                if (dsDatos.Tables["Datos"].Rows[0]["FechaUltimaActualizacion"].ToString() == "")
                    row["FechaUltimaActualizacion"] = "";
                else
                    row["FechaUltimaActualizacion"] = dsDatos.Tables["Datos"].Rows[0]["FechaUltimaActualizacion"];
                row["Regional"] = "";
                row["Puesto"] = "";
                row["CategoriaProfesionId"] = dsDatos.Tables["Datos"].Rows[0]["CategoriaProfesionId"];
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Rows.Add(row);

                int UsuarioId = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["UsuarioRegistroId"]);
                
                DataSet dsUsuarioElabora = Get_Datos_Persona(2, UsuarioId);
                string NomElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Nombre"].ToString();
                string PuestoElabora = dsUsuarioElabora.Tables["Datos"].Rows[0]["Puesto"].ToString();
                dsUsuarioElabora.Clear();
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Rows[0]["Regional"] = NomElabora;
                Ds_Constancia_RRF.Tables["Dt_ConstanciaRNF"].Rows[0]["Puesto"] = PuestoElabora;
                dsDatos.Clear();
                return Ds_Constancia_RRF;
            }
            else
                return ds;

        }

        public void Insert_Oficio_Devolucion(int GestionId, string Saludo,  XmlDocument Motivos, int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Oficio_Devolucion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Saludo", SqlDbType.VarChar, 100).Value = Saludo;
                cmd.Parameters.Add("@Motivos", SqlDbType.Xml).Value = Motivos.OuterXml.ToString();
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public int Max_Oficio_Devolucion()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Oficio_Devolucion", cn);
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

        public int Max_registroId()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_registroId", cn);
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

        public DataSet Get_Regional_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Regional_Gestion", cn);
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
                return ds;
            }
        }

        public DataSet GetPersona_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetPersona_Gestion", cn);
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
                return ds;
            }
        }

        public int Get_Vigencia_SubCategoria(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Vigencia_SubCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
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

        public void Insert_RegistroRNF(int GestionId, DateTime FechaRegistro, DateTime FechaVencimiento, int SubCategoria, int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_RegistroRNF", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@FechaRegitro", SqlDbType.Date).Value = FechaRegistro;
                cmd.Parameters.Add("@FechaVencimiento", SqlDbType.Date).Value = FechaVencimiento;
                cmd.Parameters.Add("@SubCategoria", SqlDbType.Int).Value = SubCategoria;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_SubRegional(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubRegional", cn);
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
                return ds;
            }
        }

        public DataSet Get_Gestiones_Historial(int Tipo_UsuarioId, int UsuarioId, string No_Expediente)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Gestiones_Historial", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo_UsuarioId", OleDbType.Integer).Value = Tipo_UsuarioId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@No_Expediente", OleDbType.VarChar, 200).Value = No_Expediente;
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

        public int Get_DocumentoId(int Tipo, int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_DocumentoId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public string Get_SubCategoriaManejo(int SubCategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubCategoriaManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public DataSet Datos_Formulario_Plantacion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Plantacion", cn);
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
                return ds;
            }
        }


        public DataSet Get_Datos_Formulario_BosqueNatural(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_BosqueNatural", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Formulario_SistemaAgroforestal(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_SistemaAgroforestal", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Formulario_FuenteSemillera(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_FuenteSemillera", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Formulario_Empresas(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Empresas", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Formulario_Entidad(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Entidad", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Formulario_MotoSierra(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_MotoSierra", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Formulario_Entidad_Institucion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Entidad_Institucion", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Formulario_Entidad_Organizacion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Entidad_Organizacion", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Formulario_Entidad_Asociacion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Entidad_Asociacion", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_Formulario_Entidad_Municipalidad(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_Entidad_Municipalidad", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Formulario_ArbolesFrutales(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Formulario_ArbolesFrutales", cn);
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
                return ds;
            }
        }

        public DataSet Datos_Inventario_Forestal(int GestionId, int Tipo) //1 PV, AB 2. SAF
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Inventario_Forestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
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


        public DataSet Datos_EspeciesArboreas(int GestionId) //1 PV, AB 2. SAF
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_LeeEspecieArborea", cn);
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
                return ds;
            }
        }


        public DataSet Productos_Importacion_Empresas(int GestionId) 
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Productos_Importacion_Empresas", cn);
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
                return ds;
            }
        }

        public DataSet Productos_Exportacion_Empresas(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Productos_Exportacion_Empresas", cn);
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
                return ds;
            }
        }

        public DataSet ImpresionFormularioPlantacionVoluntaria(int GestionId, int Tipo)  // Tipo 1, PV 2 Arboles Frutales
        {
            int TieneRepresentantes = TieneRepresentantes_Gestion(GestionId);
            int TipoPersona = Get_Tipo_Persona_Fincas(2, GestionId);
            int NoRequisito = 1;
            DataSet dsDatos;
            if (Tipo == 1)
                dsDatos = Datos_Formulario_Plantacion(GestionId);
            else
                dsDatos = Datos_Formulario_ArbolesFrutales(GestionId);
            Ds_Profesionales Ds_Formulario_Plantacion_Voluntaria = new Ds_Profesionales();
            Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Clear();
            DataRow row = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].NewRow();
            row["Requisitos"] = "Requisitos:\n " + NoRequisito + ") Certificación original o copia legalizada de dicha certificación que acredite la propiedad  del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud; ";
            NoRequisito++;
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del propietario; ";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Polígono georeferenciado a registrar, en coordenadas GTM.";
            NoRequisito++;
            if ((TieneRepresentantes > 0) || (TipoPersona == 2))
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del Representante Legal;";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["TipoPlantacion"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Subcategoria"];
            if (Tipo == 1)
                if (dsDatos.Tables["DATOS"].Rows[0]["Procedencia"].ToString() == "")
                    row["Procedencia"] = "--------------";
                else
                    row["Procedencia"] = dsDatos.Tables["DATOS"].Rows[0]["Procedencia"];
            else
                row["Procedencia"] = "";
            
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Telefono"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Celular"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["Tipo"] = Tipo;
            row["TotalForestal"] = Get_Sum_Area_Plantacion(GestionId, 2);
            if (TieneRepresentantes > 0)
                row["TieneRepresentantes"] = 1;
            else
                row["TieneRepresentantes"] = 0;
            Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows.Add(row);
            dsDatos.Clear();

            

            if (TieneRepresentantes > 0)
            {
                DataSet DatosRep = GetRepresentantes_Gestion(GestionId);
                Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < DatosRep.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = DatosRep.Tables["Datos"].Rows[i]["Nombres"];
                    rowRepresentantes["TipoId"] = DatosRep.Tables["Datos"].Rows[i]["TipoIdRepresentante"];
                    rowRepresentantes["Id"] = DatosRep.Tables["Datos"].Rows[i]["DocId"];
                    Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            
            return Ds_Formulario_Plantacion_Voluntaria;
        }

        public DataSet ImpresionFormularioSistemaAgroforestal(int GestionId) 
        {
            int TipoPersona = Get_Tipo_Persona_Fincas(2, GestionId);
            int NoRequisito = 1;
            int TieneRepresentantes = TieneRepresentantes_Gestion(GestionId);
            DataSet dsDatos = Datos_Formulario_SistemaAgroforestal(GestionId);
            Ds_Profesionales Ds_Formulario_SistemaAgroforestal = new Ds_Profesionales();
            Ds_Formulario_SistemaAgroforestal.Tables["Dt_Plantacion_SistemaAgroforestal"].Clear();
            DataRow row = Ds_Formulario_SistemaAgroforestal.Tables["Dt_Plantacion_SistemaAgroforestal"].NewRow();
            row["Requisitos"] = "Requisitos:\n " + NoRequisito + ") Certificación original o copia legalizada de dicha certificación que acredite la propiedad  del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud; ";
            NoRequisito++;
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del propietario; ";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Polígono georeferenciado a registrar, en coordenadas GTM.";
            NoRequisito++;
            if ((TieneRepresentantes > 0) || (TipoPersona == 2))
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del Representante Legal;";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["CategoriaSAF"] = dsDatos.Tables["DATOS"].Rows[0]["CategoriaSAF"];
            row["TipoPlantacion"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Subcategoria"];
            if (dsDatos.Tables["DATOS"].Rows[0]["Procedencia"].ToString() == "")
                row["Procedencia"] = "";
            else
                row["Procedencia"] = dsDatos.Tables["DATOS"].Rows[0]["Procedencia"];
            row["Incentivos"] = dsDatos.Tables["DATOS"].Rows[0]["Incentivos"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Telefono"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Celular"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TotalForestal"] = Get_Sum_Area_Plantacion(GestionId, 2);
            if (TieneRepresentantes > 0)
                row["TieneRepresentantes"] = 1;
            else
                row["TieneRepresentantes"] = 0;
            Ds_Formulario_SistemaAgroforestal.Tables["Dt_Plantacion_SistemaAgroforestal"].Rows.Add(row);
            dsDatos.Clear();

            if (TieneRepresentantes > 0)
            {
                DataSet DatosRep = GetRepresentantes_Gestion(GestionId);
                Ds_Formulario_SistemaAgroforestal.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < DatosRep.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_SistemaAgroforestal.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = DatosRep.Tables["Datos"].Rows[i]["Nombres"];
                    rowRepresentantes["TipoId"] = DatosRep.Tables["Datos"].Rows[i]["TipoIdRepresentante"];
                    rowRepresentantes["Id"] = DatosRep.Tables["Datos"].Rows[i]["DocId"];
                    Ds_Formulario_SistemaAgroforestal.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            return Ds_Formulario_SistemaAgroforestal;
        }

        public DataSet ImpresionFormularioBosqueNatural(int GestionId)  // Tipo 1, PV 2 Arboles Frutales
        {
            int TieneRepresentantes = TieneRepresentantes_Gestion(GestionId);
            int TipoPersona = Get_Tipo_Persona_Fincas(2, GestionId);
            int NoRequisito = 1;
            DataSet dsDatos;
            dsDatos = Get_Datos_Formulario_BosqueNatural(GestionId);
            int TipoBosque = 2;
            
            Ds_Profesionales Ds_Formulario_BosqueNatural = new Ds_Profesionales();
            Ds_Formulario_BosqueNatural.Tables["Dt_BosqueNatural"].Clear();
            DataRow row = Ds_Formulario_BosqueNatural.Tables["Dt_BosqueNatural"].NewRow();
            row["Requisitos"] = "Requisitos:\n " + NoRequisito + ") Certificación original o copia legalizada de dicha certificación que acredite la propiedad  del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud; ";
            NoRequisito++;
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del propietario; ";
                NoRequisito++;
            }
            if ((TieneRepresentantes > 0) || (TipoPersona == 2))
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del Representante Legal;";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
            NoRequisito++;
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Polígono georeferenciado a registrar, en coordenadas GTM. Es requisito de inscripción indispensable  de las plantaciones forestales tener como  mínimo un año de haber sido establecidas y para efectos de inscripción, se establecerán  parámetros técnicos de evaluación.";

            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["Procedencia"] = dsDatos.Tables["DATOS"].Rows[0]["Procedencia"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Telefono"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Celular"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TipoBosque"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_Bosque"];
            row["TotalForestal"] = Get_Sum_Area_Plantacion(GestionId, 2);
            row["TipoBosqueId"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_BosqueId"];
            if (dsDatos.Tables["DATOS"].Rows[0]["Tipo_BosqueId"].ToString() == "2")
                TipoBosque = 1;
            if (TieneRepresentantes > 0)
                row["TieneRepresentantes"] = 1;
            else
                row["TieneRepresentantes"] = 0;
            Ds_Formulario_BosqueNatural.Tables["Dt_BosqueNatural"].Rows.Add(row);
            dsDatos.Clear();

            if (TieneRepresentantes > 0)
            {
                DataSet DatosRep = GetRepresentantes_Gestion(GestionId);
                Ds_Formulario_BosqueNatural.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < DatosRep.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_BosqueNatural.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = DatosRep.Tables["Datos"].Rows[i]["Nombres"];
                    rowRepresentantes["TipoId"] = DatosRep.Tables["Datos"].Rows[i]["TipoIdRepresentante"];
                    rowRepresentantes["Id"] = DatosRep.Tables["Datos"].Rows[i]["DocId"];
                    Ds_Formulario_BosqueNatural.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }

            Ds_Formulario_BosqueNatural.Tables["Dt_Arboles_BosqueNatural"].Clear();

            DataSet DatosInventario = LeerXml_InventarioForestalBosqueNatural(GestionId, TipoBosque);

            for (int i = 0; i < DatosInventario.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowArboles = Ds_Formulario_BosqueNatural.Tables["Dt_Arboles_BosqueNatural"].NewRow();
                rowArboles["Rodal"] = DatosInventario.Tables["Datos"].Rows[i]["Rodal"];
                rowArboles["ClaseDesarrollo"] = DatosInventario.Tables["Datos"].Rows[i]["Clase_Desarrollo"];
                rowArboles["Nombre_Comun"] = DatosInventario.Tables["Datos"].Rows[i]["Nombre_Comun"];
                rowArboles["Nombre_Cientifico"] = DatosInventario.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                rowArboles["Densidad"] = DatosInventario.Tables["Datos"].Rows[i]["Densidad"];
                rowArboles["AreaBasal"] = DatosInventario.Tables["Datos"].Rows[i]["Area"];
                rowArboles["Edad"] = DatosInventario.Tables["Datos"].Rows[i]["Edad"];
                rowArboles["DAP"] = DatosInventario.Tables["Datos"].Rows[i]["Dap"];
                rowArboles["Altura"] = DatosInventario.Tables["Datos"].Rows[i]["Altura"];
                rowArboles["Estrato"] = DatosInventario.Tables["Datos"].Rows[i]["Estrato"];
                Ds_Formulario_BosqueNatural.Tables["Dt_Arboles_BosqueNatural"].Rows.Add(rowArboles);
            }

            return Ds_Formulario_BosqueNatural;
        }


        public DataSet ImpresionFormularioFuenteSemillera(int GestionId)
        {
            int TipoPersona = Get_Tipo_Persona_Fincas(2,GestionId);
            int NoRequisito = 1;
            int TieneRepresentantes = TieneRepresentantes_Gestion(GestionId);
            DataSet dsDatos = Datos_Formulario_FuenteSemillera(GestionId);
            Ds_Profesionales Ds_Formulario_FuenteSemillera = new Ds_Profesionales();
            Ds_Formulario_FuenteSemillera.Tables["Dt_Fueste_Semillera"].Clear();
            DataRow row = Ds_Formulario_FuenteSemillera.Tables["Dt_Fueste_Semillera"].NewRow();
            row["Requisitos"] = "Requisitos:\n " + NoRequisito + ") Certificación original o copia legalizada de dicha certificación que acredite la propiedad  del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud; ";
            NoRequisito++;
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del propietario; ";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Polígono georeferenciado a registrar, en coordenadas GTM.";
            NoRequisito++;
            if ((TieneRepresentantes > 0) || (TipoPersona == 2))
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del Representante Legal;";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["CategoriaFS"] = dsDatos.Tables["DATOS"].Rows[0]["CategoriaSF"];
            row["TipoPlantacion"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Subcategoria"];
            
            
            row["Procedencia"] = dsDatos.Tables["DATOS"].Rows[0]["Procedencia"];
            row["TipoBosque"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_Bosque"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Telefono"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Celular"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TotalForestal"] = Get_Sum_Area_Plantacion(GestionId, 2);
            row["TotalFuente"] = Get_Sum_Area_Fuente(GestionId, 2);
            if (TieneRepresentantes > 0)
                row["TieneRepresentantes"] = 1;
            else
                row["TieneRepresentantes"] = 0;
            Ds_Formulario_FuenteSemillera.Tables["Dt_Fueste_Semillera"].Rows.Add(row);
            dsDatos.Clear();
            if (TieneRepresentantes > 0)
            {
                DataSet DatosRep = GetRepresentantes_Gestion(GestionId);
                Ds_Formulario_FuenteSemillera.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < DatosRep.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_FuenteSemillera.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = DatosRep.Tables["Datos"].Rows[i]["Nombres"];
                    rowRepresentantes["TipoId"] = DatosRep.Tables["Datos"].Rows[i]["TipoIdRepresentante"];
                    rowRepresentantes["Id"] = DatosRep.Tables["Datos"].Rows[i]["DocId"];
                    Ds_Formulario_FuenteSemillera.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            Ds_Formulario_FuenteSemillera.Tables["Dt_Fueste_Semillera"].Rows[0]["EspeciesArb"] = EspeciesArborea(GestionId);

                
            return Ds_Formulario_FuenteSemillera;
        }

        public DataSet ImpresionFormularioEmpresas(int GestionId)
        {
            DataSet dsDatos = Datos_Formulario_Empresas(GestionId);
            Ds_Profesionales Ds_Formulario_Empresas = new Ds_Profesionales();
            Ds_Formulario_Empresas.Tables["Dt_Empresas"].Clear();
            DataRow row = Ds_Formulario_Empresas.Tables["Dt_Empresas"].NewRow();
            row["Requisitos"] = "Requisitos:\n1) Copia legalizada de la patente de comercio, con la especificación clara del objeto del negocio como actividad forestal; \n2) copia de constancia de inscripción en el Registro Tributario Unificado (RTU). Las sucursales deben contar con su propia patente de comercio;  ";
            if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0)))
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario.";
            if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) > 0)))
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario. \n4) Copia del documento personal de identificación del Representante Legal;  \n5) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
            if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) == 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) > 0)))
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del Representante Legal;  \n4) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];
            row["CategoriaEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_SubCategoria"];

            row["NombreComercial"] = dsDatos.Tables["DATOS"].Rows[0]["NomEmpresa"];
            row["NIT"] = dsDatos.Tables["DATOS"].Rows[0]["NIT"];
            row["NoRegMercantil"] = dsDatos.Tables["DATOS"].Rows[0]["Numero"];
            row["Folio"] = dsDatos.Tables["DATOS"].Rows[0]["Folio"];
            row["Libro"] = dsDatos.Tables["DATOS"].Rows[0]["Libro"];
            row["De"] = dsDatos.Tables["DATOS"].Rows[0]["CategoriaEmpresaMercantil"];
            row["Categoria"] = dsDatos.Tables["DATOS"].Rows[0]["Categoria_Empresa"];
            row["Objeto"] = dsDatos.Tables["DATOS"].Rows[0]["Objeto"];
            row["HorasTurno"] = dsDatos.Tables["DATOS"].Rows[0]["HorasTurno"];
            row["TurnoDia"] = dsDatos.Tables["DATOS"].Rows[0]["TurnoDia"];
            row["DiasYear"] = dsDatos.Tables["DATOS"].Rows[0]["DiasYear"];
            row["NoEmplFijo"] = dsDatos.Tables["DATOS"].Rows[0]["NoEmpleados_Fijo"];
            row["NoEmplNoFijo"] = dsDatos.Tables["DATOS"].Rows[0]["NoEmpleados_NoFijos"];
            row["DireccionEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["DireccionEmpresa"];
            row["DepEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["DepEmpresa"];
            row["MunEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["MunEmpresa"];
            row["TelefonoUno"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoUno"];
            if (dsDatos.Tables["DATOS"].Rows[0]["TelefonoDos"].ToString() == "")
                row["TelefonoDos"] = "";
            else
                row["TelefonoDos"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoDos"];
            row["CorreoEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["CorreoEmpresa"];
            row["TipoPersona"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_Persona"]; ;
            row["RazonSocial"] = dsDatos.Tables["DATOS"].Rows[0]["Razon_Social"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["TelNotifica"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CelularNotifica"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CorreoNotifica"];
            row["DireccionFuncionamiento"] = dsDatos.Tables["DATOS"].Rows[0]["DireccionFuncionamiento"];
            row["DepFuncionamiento"] = dsDatos.Tables["DATOS"].Rows[0]["DepFuncionamiento"];
            row["MunFuncionamiento"] = dsDatos.Tables["DATOS"].Rows[0]["MunFuncionamiento"];
            row["FabricaProductosForestales"] = dsDatos.Tables["DATOS"].Rows[0]["FabricaProductos"];
            if (dsDatos.Tables["DATOS"].Rows[0]["Codigo_Registro"].ToString() == "")
                row["IFRNF"] = "";
            else
                row["IFRNF"] = dsDatos.Tables["DATOS"].Rows[0]["Codigo_Registro"];
            row["ActividadPrincipal"] = dsDatos.Tables["DATOS"].Rows[0]["ActividadPrincipal"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TipoPersonaId"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"];
            if (dsDatos.Tables["DATOS"].Rows[0]["DireccionFuncionamiento"].ToString() == "")
                row["EsMovil"] = "0";
            else
                row["EsMovil"] = "1";
            if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "9")
                row["EsExportador"] = "1";
            else
                row["EsExportador"] = "0";
            if (dsDatos.Tables["DATOS"].Rows[0]["FabricaProductos"].ToString() == "Si")
                row["EsFabricante"] = "1";
            else
                row["EsFabricante"] = "0";
            if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0))
                row["TieneRepresentantes"] = "0";
            else
                row["TieneRepresentantes"] = "1";
            Ds_Formulario_Empresas.Tables["Dt_Empresas"].Rows.Add(row);
            int TipoPesona = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"]);
            int EmpresaId = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaJuridicaId"]);
            int SubCategoriaId = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"]);
            dsDatos.Clear();
            if (TipoPesona == 1)
            {
                
                ClEmpresa = new Cl_Persona_Juridica();
                DataSet dsPropietarios = ClEmpresa.Get_Propietarios_Gestion(GestionId);
                for (int i = 0; i < dsPropietarios.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowPropietario = Ds_Formulario_Empresas.Tables["Dt_Propietarios"].NewRow();
                    rowPropietario["Nombre"] = dsPropietarios.Tables["Datos"].Rows[i]["Nombres"] + " " + dsPropietarios.Tables["Datos"].Rows[i]["Apellidos"];
                    if (dsPropietarios.Tables["Datos"].Rows[i]["PaisId"].ToString() == "0")
                        rowPropietario["TipoId"] = "DPI";
                    else
                        rowPropietario["TipoId"] = "Pasporte";
                    rowPropietario["Id"] = dsPropietarios.Tables["Datos"].Rows[i]["Dpi"];
                    Ds_Formulario_Empresas.Tables["Dt_Propietarios"].Rows.Add(rowPropietario);
                }
                dsPropietarios.Clear();
            }

            DataSet dsRepresentantes = Get_Representantes_Gestion(GestionId);
            Ds_Formulario_Empresas.Tables["Dt_Representantes"].Clear();
            for (int i = 0; i < dsRepresentantes.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowPropietario = Ds_Formulario_Empresas.Tables["Dt_Representantes"].NewRow();
                rowPropietario["Nombres"] = dsRepresentantes.Tables["Datos"].Rows[i]["Nombres"] + " " + dsRepresentantes.Tables["Datos"].Rows[i]["Apellidos"];
                if (dsRepresentantes.Tables["Datos"].Rows[i]["PaisId"].ToString() == "0")
                    rowPropietario["TipoId"] = "DPI";
                else
                    rowPropietario["TipoId"] = "Pasporte";
                rowPropietario["Id"] = dsRepresentantes.Tables["Datos"].Rows[i]["Dpi"];
                Ds_Formulario_Empresas.Tables["Dt_Representantes"].Rows.Add(rowPropietario);
            }
            dsRepresentantes.Clear();

            if (SubCategoriaId == 9)
            {
                DataSet dsProdImportacion = Productos_Importacion_Empresas(GestionId);
                Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].Clear();
                for (int i = 0; i < dsProdImportacion.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowProductoImportacion = Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].NewRow();
                    rowProductoImportacion["No"] = dsProdImportacion.Tables["Datos"].Rows[i]["Num"];
                    rowProductoImportacion["Producto"] = dsProdImportacion.Tables["Datos"].Rows[i]["Nombre_Producto"];
                    rowProductoImportacion["CodigoFSC"] = dsProdImportacion.Tables["Datos"].Rows[i]["CodigoFSC"];
                    Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].Rows.Add(rowProductoImportacion);
                }
                dsProdImportacion.Clear();

                DataSet dsProdExportacion = Productos_Exportacion_Empresas(GestionId);
                Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].Clear();
                for (int i = 0; i < dsProdExportacion.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowPropietarioExportacion = Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].NewRow();
                    rowPropietarioExportacion["No"] = dsProdExportacion.Tables["Datos"].Rows[i]["Num"];
                    rowPropietarioExportacion["Producto"] = dsProdExportacion.Tables["Datos"].Rows[i]["Nombre_Producto"];
                    rowPropietarioExportacion["CodigoFSC"] = dsProdExportacion.Tables["Datos"].Rows[i]["CodigoFSC"];
                    Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].Rows.Add(rowPropietarioExportacion);
                }
            }
            return Ds_Formulario_Empresas;
        }

        public DataSet ImpresionFormularioEntidad(int GestionId)
        {
            DataSet dsDatos = Datos_Formulario_Entidad(GestionId);
            Ds_Profesionales Ds_Formulario_Entidad = new Ds_Profesionales();
            Ds_Formulario_Entidad.Tables["Dt_Entidad"].Clear();
            DataRow row = Ds_Formulario_Entidad.Tables["Dt_Entidad"].NewRow();
            row["Requisitos"] = "Requisitos:\n1)  Copia del documento que ampare la constitución y objeto de la entidad;  \n2)  Copia del carné de identificación tributaria; ";
            if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Representantes"]) == 0)
            {
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario;";
                if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "28")
                    row["Requisitos"] = row["Requisitos"] + "\n4)  Copia simple del acta de toma de posesión o nombramiento según sea el caso.";
            }
            else
            {
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del Representante Legal;  \n4) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
                if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "28")
                    row["Requisitos"] = row["Requisitos"] + "\n5)  Copia simple del acta de toma de posesión o nombramiento según sea el caso.";
            }
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];

            row["TipoInscripcion"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_SubCategoria"];

            row["NombreEntidad"] = dsDatos.Tables["DATOS"].Rows[0]["NombreEntidad"];
            row["NIT"] = dsDatos.Tables["DATOS"].Rows[0]["Nit"];
            row["Objeto"] = dsDatos.Tables["DATOS"].Rows[0]["Objeto"];
            row["DireccionEntidad"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion"];
            row["DepEntidad"] = dsDatos.Tables["DATOS"].Rows[0]["DepartamentoEntidad"];
            row["MunEntidad"] = dsDatos.Tables["DATOS"].Rows[0]["MunicipioEntidad"];
            row["TelefonoUno"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoUno"];
            if (dsDatos.Tables["DATOS"].Rows[0]["TelefonoDos"].ToString() == "")
                row["TelefonoDos"] = "";
            else
                row["TelefonoDos"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoDos"];
            row["CorreoEntidad"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
            row["TipoPersona"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_Persona"];
            row["RazonSocial"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Empresa"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["TelNotifica"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CelularNotifica"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CorreoNotifica"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TipoEntidadId"] = dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"];
            row["TieneRepresentantes"] = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Representantes"]);
            int SubCategoria = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"]);
            Ds_Formulario_Entidad.Tables["Dt_Entidad"].Rows.Add(row);

            dsDatos.Clear();

            if (SubCategoria == 25)
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].Clear();
                DataSet dsDatosEntidad = Get_Datos_Formulario_Entidad_Institucion(GestionId);
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].NewRow();
                rowInstitucion["Tipo"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tipo_Entidad"];
                rowInstitucion["Cobertura"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Cobertura"];
                rowInstitucion["Propiedad"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tipo_Propiedad"];
                rowInstitucion["ActividadPrincipal"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["ActividadPrincipal"];
                rowInstitucion["NoFamAtendidas"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_FamiliasAtendidas"];
                Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].Rows.Add(rowInstitucion);
                dsDatosEntidad.Clear();

            }
            else if (SubCategoria == 26)
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].Clear();
                DataSet dsDatosEntidad = Get_Datos_Formulario_Entidad_Organizacion(GestionId);
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].NewRow();
                rowInstitucion["Tipo"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tipo_Entidad"];
                rowInstitucion["Cobertura"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Cobertura"];
                rowInstitucion["Propiedad"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tipo_Propiedad"];
                rowInstitucion["Tamano"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tamano"];
                rowInstitucion["Produccion"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Tipo_Produccion"];
                rowInstitucion["ActividadPrincipal"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["ActividadPrincipal"];
                rowInstitucion["NoFamAtendidas"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_FamiliasAtendidas"];
                Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].Rows.Add(rowInstitucion);
            }
            else if (SubCategoria == 27)
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].Clear();
                DataSet dsDatosEntidad = Get_Datos_Formulario_Entidad_Asociacion(GestionId);
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].NewRow();
                rowInstitucion["GrupoEtnico"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Etnia"];
                rowInstitucion["Finalidad"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["Finalidad"];
                rowInstitucion["ActividadPrincipal"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["ActividadPrincipal"];
                rowInstitucion["NoIntengrantes"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_Integrantes"];
                rowInstitucion["NoFamAtendidas"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_FamiliasAtendidas"];
                rowInstitucion["TotBosqueNatural"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["TotalReforestacion"];
                rowInstitucion["TotReforestacion"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_FamiliasAtendidas"];
                Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].Rows.Add(rowInstitucion);
            }
            else if (SubCategoria == 28)
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].Clear();
                DataSet dsDatosEntidad = Get_Datos_Formulario_Entidad_Municipalidad(GestionId);
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].NewRow();
                rowInstitucion["NombreOficina"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["NombreOficina"];
                rowInstitucion["YearCreacion"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["YearCreacion"];
                rowInstitucion["CorreoOficina"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["CorreoOficina"];
                rowInstitucion["Telefono"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["TelefonoOficina"];
                rowInstitucion["Nombres"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["NombreEncargado"];
                rowInstitucion["Apellidos"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["ApellidoEncargado"];
                rowInstitucion["CorreoEncargado"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["CorreoEncargado"];
                rowInstitucion["Celular"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["CelularEncargado"];
                rowInstitucion["NoFamAtendidas"] = dsDatosEntidad.Tables["DATOS"].Rows[0]["No_FamiliasAtendidas"];
                Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].Rows.Add(rowInstitucion);
            }

            DataSet dsRepresentantes = Get_Representantes_Gestion(GestionId);
            Ds_Formulario_Entidad.Tables["Dt_Representantes"].Clear();
            for (int i = 0; i < dsRepresentantes.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowPropietario = Ds_Formulario_Entidad.Tables["Dt_Representantes"].NewRow();
                rowPropietario["Nombres"] = dsRepresentantes.Tables["Datos"].Rows[i]["Nombres"] + " " + dsRepresentantes.Tables["Datos"].Rows[i]["Apellidos"];
                if (dsRepresentantes.Tables["Datos"].Rows[i]["PaisId"].ToString() == "0")
                    rowPropietario["TipoId"] = "DPI";
                else
                    rowPropietario["TipoId"] = "Pasporte";
                rowPropietario["Id"] = dsRepresentantes.Tables["Datos"].Rows[i]["Dpi"];
                Ds_Formulario_Entidad.Tables["Dt_Representantes"].Rows.Add(rowPropietario);
            }
            dsRepresentantes.Clear();
            return Ds_Formulario_Entidad;
        }

        public DataSet ImpresionFormularioMotoSierra(int GestionId)
        {
            DataSet dsDatos = Get_Datos_Formulario_MotoSierra(GestionId);
            Ds_Profesionales Ds_Formulario_MotoSierra = new Ds_Profesionales();
            Ds_Formulario_MotoSierra.Tables["Dt_MotoSierra"].Clear();
            DataRow row = Ds_Formulario_MotoSierra.Tables["Dt_MotoSierra"].NewRow();
            int Requisitos = 1;
            if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "11")
            {
                row["Requisitos"] = "Requisitos:\n1) Documento original que acredite la propiedad del bien; \n2)  Copia que acredite la propiedad del bien;";
                if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0)))
                    row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario.";
                if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) > 0)))
                    row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario. \n4) Copia del documento personal de identificación del Representante Legal;  \n5) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
                if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) == 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0)))
                    row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del Representante Legal;  \n4) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
                row["Requisitos"] = row["Requisitos"] + "\n No se inscribirán motosierras que carezcan del número de serie o que tengan alteraciones.";
            }
            else
            {
                row["Requisitos"] = "Requisitos:\n" + Requisitos + ") Copia de la patente de comercio, la cual debe indicar el objeto de dicha actividad. Las sucursales deben contar con su propia patente de comercio ; ";
                Requisitos++;
                row["Requisitos"] = row["Requisitos"] + " \n" + Requisitos + ")  Constancia de inscripción en el Registro Tributario Unificado (RTU);";

                Requisitos++;
                if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"]) == 1) && (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"])> 0))
                {
                    row["Requisitos"] = row["Requisitos"] + "\n " + Requisitos + ") Copia del documento personal de identificación del propietario.";
                    Requisitos++;
                }

                if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"]) == 2) || (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) > 0))
                {
                    row["Requisitos"] = row["Requisitos"] + "\n " + Requisitos + ") Copia del documento personal de identificación del Representante Legal;";
                    Requisitos++;
                    row["Requisitos"] = row["Requisitos"] + "\n " + Requisitos + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
                }
            }
            row["Region"] = dsDatos.Tables["DATOS"].Rows[0]["Region"];
            row["SubRegion"] = dsDatos.Tables["DATOS"].Rows[0]["SubRegion"];
            row["NUG"] = dsDatos.Tables["DATOS"].Rows[0]["NUG"];
            row["Fecha"] = dsDatos.Tables["DATOS"].Rows[0]["Fecha"];

            row["SubCategoriaRNF"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_SubCategoria"];
            if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "15")
            {
                row["NombreComercial"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre"];
                row["NIT"] = dsDatos.Tables["DATOS"].Rows[0]["Nit"];
                row["NoRegMercantil"] = dsDatos.Tables["DATOS"].Rows[0]["Numero"];
                row["Folio"] = dsDatos.Tables["DATOS"].Rows[0]["Folio"];
                row["Libro"] = dsDatos.Tables["DATOS"].Rows[0]["Libro"];
                row["De"] = dsDatos.Tables["DATOS"].Rows[0]["CategoriaEmpresaMercantil"];
                row["Categoria"] = dsDatos.Tables["DATOS"].Rows[0]["Categoria_Empresa"];
                row["Objeto"] = dsDatos.Tables["DATOS"].Rows[0]["Objeto"];
                row["TurnoHoras"] = dsDatos.Tables["DATOS"].Rows[0]["HorasTurno"];
                row["DiasYear"] = dsDatos.Tables["DATOS"].Rows[0]["DiasYear"];
                if (dsDatos.Tables["DATOS"].Rows[0]["Nombre_SubCategoria"].ToString() == "")
                    row["StockMotoSierra"] = "";
                else
                    row["StockMotoSierra"] = dsDatos.Tables["DATOS"].Rows[0]["StockMotoSierra"];
                row["NoEmplAdmin"] = dsDatos.Tables["DATOS"].Rows[0]["NoEmpleados_fijo"];
                row["NoEmplOper"] = dsDatos.Tables["DATOS"].Rows[0]["NoEmpleados_nofijos"];
                row["DireccionEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["DireccionEmpresa"];
                row["DepEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["DepEmp"];
                row["MunEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["MunEmp"];
                row["TelefonoUno"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoUno"];
                row["TelefonoDos"] = dsDatos.Tables["DATOS"].Rows[0]["TelefonoDos"];
                row["CorreoEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["CorreoEmpresa"];
            }
            else if (dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"].ToString() == "11")
            {
                row["Marca"] = dsDatos.Tables["DATOS"].Rows[0]["Marca"];
                row["Modelo"] = dsDatos.Tables["DATOS"].Rows[0]["Modelo"];
                row["Cilindraje"] = dsDatos.Tables["DATOS"].Rows[0]["Cilindraje"];
                row["Potencia"] = dsDatos.Tables["DATOS"].Rows[0]["Potencia"];
                row["NoSerie"] = dsDatos.Tables["DATOS"].Rows[0]["Serie"];
                row["TipoDocumento"] = dsDatos.Tables["DATOS"].Rows[0]["TipoDocumento"];
                if ((dsDatos.Tables["DATOS"].Rows[0]["TipoDocumentoId"].ToString() == "1") || (dsDatos.Tables["DATOS"].Rows[0]["TipoDocumentoId"].ToString() == "4"))
                {
                    row["NoFactura"] = dsDatos.Tables["DATOS"].Rows[0]["NoFactura"];
                    row["DeEmpresa"] = dsDatos.Tables["DATOS"].Rows[0]["Empresa"];
                }
                else
                {
                    row["NoFactura"] = "--------------";
                    row["DeEmpresa"] = "--------------";
                }
                if (dsDatos.Tables["DATOS"].Rows[0]["TipoDocumentoId"].ToString() == "5")
                    row["Especifique"] = dsDatos.Tables["DATOS"].Rows[0]["Especifique"];
                else
                    row["Especifique"] = "--------------";
            }

            row["TipoPersona"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_Persona"];
            row["RazonSocial"] = dsDatos.Tables["DATOS"].Rows[0]["Razon_Social"];
            row["Direccion_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Direccion_Notificacion"];
            row["Municipio_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Municipio_Notificacion"];
            row["Departamento_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["Departamento_Notificacion"];
            if (dsDatos.Tables["DATOS"].Rows[0]["TelNotifica"].ToString() == "")
                row["Telefono_Notificacion"] = "";
            else
                row["Telefono_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["TelNotifica"];
            row["Celular_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CelularNotifica"];
            row["Correo_Notificacion"] = dsDatos.Tables["DATOS"].Rows[0]["CorreoNotifica"];
            row["Observaciones"] = dsDatos.Tables["DATOS"].Rows[0]["Observaciones"];
            row["Nombre"] = dsDatos.Tables["DATOS"].Rows[0]["Nombre_Firma"];
            row["TipoPersonaId"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"];

            row["SubCategoriaId"] = dsDatos.Tables["DATOS"].Rows[0]["SubCategoriaId"];
            int TipoPersona = Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Tipo_PersonaId"]);
            Ds_Formulario_MotoSierra.Tables["Dt_MotoSierra"].Rows.Add(row);
            dsDatos.Clear();

            if (TipoPersona == 1)
            {

                ClEmpresa = new Cl_Persona_Juridica();
                DataSet dsPropietarios = ClEmpresa.Get_Propietarios_Gestion(GestionId);
                for (int i = 0; i < dsPropietarios.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowPropietario = Ds_Formulario_MotoSierra.Tables["Dt_Propietarios"].NewRow();
                    rowPropietario["Nombre"] = dsPropietarios.Tables["Datos"].Rows[i]["Nombres"] + " " + dsPropietarios.Tables["Datos"].Rows[i]["Apellidos"];
                    if (dsPropietarios.Tables["Datos"].Rows[i]["PaisId"].ToString() == "0")
                        rowPropietario["TipoId"] = "DPI";
                    else
                        rowPropietario["TipoId"] = "Pasporte";
                    rowPropietario["Id"] = dsPropietarios.Tables["Datos"].Rows[i]["Dpi"];
                    Ds_Formulario_MotoSierra.Tables["Dt_Propietarios"].Rows.Add(rowPropietario);
                }
                dsPropietarios.Clear();
            }

            DataSet dsRepresentantes = Get_Representantes_Gestion(GestionId);
            Ds_Formulario_MotoSierra.Tables["Dt_Representantes"].Clear();
            for (int i = 0; i < dsRepresentantes.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowPropietario = Ds_Formulario_MotoSierra.Tables["Dt_Representantes"].NewRow();
                rowPropietario["Nombres"] = dsRepresentantes.Tables["Datos"].Rows[i]["Nombres"] + " " + dsRepresentantes.Tables["Datos"].Rows[i]["Apellidos"];
                if (dsRepresentantes.Tables["Datos"].Rows[i]["PaisId"].ToString() == "0")
                    rowPropietario["TipoId"] = "DPI";
                else
                    rowPropietario["TipoId"] = "Pasporte";
                rowPropietario["Id"] = dsRepresentantes.Tables["Datos"].Rows[i]["Dpi"];
                Ds_Formulario_MotoSierra.Tables["Dt_Representantes"].Rows.Add(rowPropietario);
            }
            dsRepresentantes.Clear();

            return Ds_Formulario_MotoSierra;
        }

        string EspeciesArborea(int GestionId)
        {
            string Especies = "";
            DataSet dsDatos = Datos_EspeciesArboreas(GestionId);
            for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
            {
                if (Especies == "")
                    Especies = dsDatos.Tables["DATOS"].Rows[i]["Especie"].ToString();
                else
                    Especies = Especies + ", " + dsDatos.Tables["DATOS"].Rows[i]["Especie"].ToString();
            }

            return Especies;
        }

        public DataSet Impresion_Inventario_Forestal(int GestionId, int Tipo)
        {
            DataSet dsDatos = Datos_Inventario_Forestal(GestionId, Tipo);
            Ds_Gestiones Ds_Inventario_Forestal = new Ds_Gestiones();
            if (Tipo ==  0) //PV
            {
                Ds_Inventario_Forestal.Tables["Dt_Inventario"].Clear();
                for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Inventario_Forestal.Tables["Dt_Inventario"].NewRow();
                    row["Rodal"] = dsDatos.Tables["DATOS"].Rows[i]["Rodal"];
                    row["Especie"] = dsDatos.Tables["DATOS"].Rows[i]["Especie"];
                    row["Area"] = dsDatos.Tables["DATOS"].Rows[i]["Area"];
                    row["Densidad"] = dsDatos.Tables["DATOS"].Rows[i]["Densidad"];
                    row["Dap"] = dsDatos.Tables["DATOS"].Rows[i]["Dap"];
                    row["Altura"] = dsDatos.Tables["DATOS"].Rows[i]["Altura"];
                    row["Volumen"] = dsDatos.Tables["DATOS"].Rows[i]["Volumen"];
                    row["YearEst"] = dsDatos.Tables["DATOS"].Rows[i]["YearEst"];
                    row["Tipo"] = Tipo;
                    Ds_Inventario_Forestal.Tables["Dt_Inventario"].Rows.Add(row);
                }
            }
            else if (Tipo == 1) //SAF
            {
                Ds_Inventario_Forestal.Tables["Dt_Inventario"].Clear();
                for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Inventario_Forestal.Tables["Dt_Inventario"].NewRow();
                    row["Rodal"] = dsDatos.Tables["DATOS"].Rows[i]["num"];
                    row["Especie"] = dsDatos.Tables["DATOS"].Rows[i]["Especie"];
                    row["Area"] = dsDatos.Tables["DATOS"].Rows[i]["CntArboles"];
                    row["Densidad"] = dsDatos.Tables["DATOS"].Rows[i]["Distancia"];
                    row["Dap"] = dsDatos.Tables["DATOS"].Rows[i]["Dap"];
                    row["Altura"] = dsDatos.Tables["DATOS"].Rows[i]["Altura"];
                    row["Volumen"] = dsDatos.Tables["DATOS"].Rows[i]["Volumen"];
                    row["YearEst"] = dsDatos.Tables["DATOS"].Rows[i]["YearEst"];
                    row["Tipo"] = Tipo;
                    row["NombreSAF"] = dsDatos.Tables["DATOS"].Rows[i]["NombreSAF"];
                    row["AreaMetros"] = dsDatos.Tables["DATOS"].Rows[i]["AreaMetros"];
                    row["SubCategoriaId"] = dsDatos.Tables["DATOS"].Rows[i]["SubCategoriaId"];
                    Ds_Inventario_Forestal.Tables["Dt_Inventario"].Rows.Add(row);
                }
            }
            else if (Tipo == 2) //FSYMV
            {
                Ds_Inventario_Forestal.Tables["Dt_Inventario"].Clear();
                for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Inventario_Forestal.Tables["Dt_Inventario"].NewRow();
                    row["Rodal"] = dsDatos.Tables["DATOS"].Rows[i]["num"];
                    row["Especie"] = dsDatos.Tables["DATOS"].Rows[i]["Especie"];
                    row["Area"] = dsDatos.Tables["DATOS"].Rows[i]["CntArboles"];
                    row["Dap"] = dsDatos.Tables["DATOS"].Rows[i]["Dap"];
                    row["Altura"] = dsDatos.Tables["DATOS"].Rows[i]["Altura"];
                    row["YearEst"] = dsDatos.Tables["DATOS"].Rows[i]["YearEst"];
                    row["Tipo"] = Tipo;
                    row["NombreSAF"] = dsDatos.Tables["DATOS"].Rows[i]["NombreSAF"];
                    row["AreaMetros"] = dsDatos.Tables["DATOS"].Rows[i]["AreaMetros"];
                    row["SubCategoriaId"] = dsDatos.Tables["DATOS"].Rows[i]["SubCategoriaId"];
                    Ds_Inventario_Forestal.Tables["Dt_Inventario"].Rows.Add(row);
                }
            }
            
            
            dsDatos.Clear();
            return Ds_Inventario_Forestal;
        }

        public void Insert_Inventario_Forestal(int GestionId, XmlDocument Inventario, int UsuarioId, string NombreSAF = "",  double AreaMetros = 0)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_Insert_Inventario_Forestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Inventario", SqlDbType.Xml).Value = Inventario.OuterXml.ToString();
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                if (NombreSAF == "")
                    cmd.Parameters.Add("@NombreSAF", SqlDbType.VarChar,200).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@NombreSAF", SqlDbType.VarChar,200).Value = NombreSAF;
                if (AreaMetros == 0)
                    cmd.Parameters.Add("@AreaMetros", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AreaMetros", SqlDbType.Int).Value = AreaMetros;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public int Tiene_Anexos_Inventerio(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Tiene_Anexos_Inventerio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public int Tiene_Anexos_Poligono(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Tiene_Anexos_Poligono", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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


        public string NombreCategoriaRNF(int SubCagtegoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_NomCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCagtegoriaId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();

            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Existe_Resolucion_Admision(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Resolucion_Admision", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        

        public int Get_SubRegionId_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_SubRegionId_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public bool Existe_Tipo_Gestion(int PersonaId, int CategoriaId, int SubCategoriaId, int DetSubCategoriaId, int PersonaJuridicaId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Tipo_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@SubCategoriaId", OleDbType.Integer).Value = SubCategoriaId;
                cmd.Parameters.Add("@DetSubCategoriaId", OleDbType.Integer).Value = DetSubCategoriaId;
                cmd.Parameters.Add("@PersonaJuridicaId", OleDbType.Integer).Value = PersonaJuridicaId;
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

        public bool Existe_Entidad(string NIT)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Entidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NIT", OleDbType.VarChar,15).Value = NIT;
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



        public bool Existe_Serie_MotoSierra(string Serie)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Serie_MotoSierra", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SerieMotoSierra", OleDbType.VarChar, 200).Value = Serie;
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

        public bool Existe_Factura_Gestion(string Serie, int Numero)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Existe_Factura_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SerieFactura", OleDbType.VarChar, 200).Value = Serie;
                cmd.Parameters.Add("@No_Factura", OleDbType.Integer).Value = Numero;
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

        public DataSet Get_Representantes_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Representantes_Gestion", cn);
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

        public void InsertTempFincaGestionRegistro(int InmuebleId, int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertTempFincaRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_TempFincaRegistro(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetTempFincaRegistro", cn);
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
                return ds;
            }
        }

        public DataSet Inmuebles_GetAll_Registro(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Inmuebles_GetAll_Registro", cn);
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

        public void EliminarTempFincaRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarTempFincaRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet GetPropietarios_Inmuebles_Registro(int UsuarioId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetPropietarios_Inmuebles_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet GetTipoPersonaTempRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_GetTipoPersonaTempRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public void ActualizarTipoPersonaTempFincaRegistro(int UsuarioId, int InmuebleId, int Tipo_PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarTipoPersonaTempFincaRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Tipo_PersonaId", SqlDbType.Int).Value = Tipo_PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void InsertTempFincaPropietarioRegistro(int UsuarioId, int InmuebleId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertTempFincaPropietarioRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void ActualizarNombre_EmpresaTempFincaRegistro(int UsuarioId, int InmuebleId, string Nombre_Empresa, string NIT)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarNombre_EmpresaTempFincaRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@Nombre_Empresa", SqlDbType.VarChar, 300).Value = Nombre_Empresa;
                cmd.Parameters.Add("@Nit", SqlDbType.VarChar, 10).Value = NIT;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Elimina_PropietarioFinca_Registro(int UsuarioId, int InmuebleId, int PersonaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Elimina_PropietarioFinca_Registro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", SqlDbType.Int).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet GetAreasFinca_Registro(int UsuarioId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetAreasFinca_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public void Eliminar_AreasInmuebleRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_AreasInmuebleRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insertar_AreasInmuebleRegistro(int UsuarioId, int InmuebleId, double AreaForestal, double AreaFuente = 0)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insertar_AreasInmuebleRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.Parameters.Add("@AreaForestal", SqlDbType.Float).Value = AreaForestal;
                cmd.Parameters.Add("@AreaFuente", SqlDbType.Float).Value = AreaFuente;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Eliminar_Datos_Temp_Registro(int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Eliminar_Datos_Temp_Registro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void EliminarTempPropietariosRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarTempPropietariosRegistro", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = InmuebleId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public double GetSumaAreasForestalesTempFincaRegistro(int UsuarioId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetSumaAreasForestalesTempFincaRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Area", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@Area"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int TienePropietarioNomEmp_FincaRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_TienePropietarioNomEmp_FincaRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int TieneAras_FincaRegistro(int UsuarioId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_TieneAras_FincaRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet GetFinca_Registro(int Id, int Origen)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetFinca_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
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

        public DataSet Fincas_Registro(int Id, int Origen)
        {
            
            Ds_PlanManejo Ds_PlanManejo = new Ds_PlanManejo();
            //LLenar Fincas
            DataSet dsDatos = GetFinca_Registro(Id, Origen);
            Ds_PlanManejo.Tables["DtFinca"].Clear();
            for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowFinca = Ds_PlanManejo.Tables["DtFinca"].NewRow();
                rowFinca["Nombre"] = dsDatos.Tables["Datos"].Rows[i]["Finca"];
                rowFinca["GTMNorte"] = dsDatos.Tables["Datos"].Rows[i]["Gtm_Norte"];
                rowFinca["GTMOEste"] = dsDatos.Tables["Datos"].Rows[i]["Gtm_Oeste"];
                rowFinca["TipoDocuementoPosecion"] = dsDatos.Tables["Datos"].Rows[i]["TipoDocPropiedad"];
                rowFinca["TipoDocumentoId"] = dsDatos.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"];
                rowFinca["FecEmision"] = dsDatos.Tables["Datos"].Rows[i]["FecEmi"];
                if (dsDatos.Tables["Datos"].Rows[i]["Nofinca"].ToString() == "")
                    rowFinca["No_Finca"] = "";
                else
                    rowFinca["No_Finca"] = dsDatos.Tables["Datos"].Rows[i]["Nofinca"];
                if (dsDatos.Tables["Datos"].Rows[i]["Folio"].ToString() == "")
                    rowFinca["Folio"] = "";
                else
                    rowFinca["Folio"] = dsDatos.Tables["Datos"].Rows[i]["Folio"];
                if (dsDatos.Tables["Datos"].Rows[i]["Libro"].ToString() == "")
                    rowFinca["Libro"] = "";
                else
                    rowFinca["Libro"] = dsDatos.Tables["Datos"].Rows[i]["Libro"];
                if (dsDatos.Tables["Datos"].Rows[i]["De"].ToString() == "")
                    rowFinca["De"] = "";
                else
                    rowFinca["De"] = dsDatos.Tables["Datos"].Rows[i]["De"];
                if (dsDatos.Tables["Datos"].Rows[i]["NoCertificacion"].ToString() == "")
                    rowFinca["No_Certificacion"] = "";
                else
                    rowFinca["No_Certificacion"] = dsDatos.Tables["Datos"].Rows[i]["NoCertificacion"];
                if (dsDatos.Tables["Datos"].Rows[i]["Municipalidad"].ToString() == "")
                    rowFinca["Municipalidad"] = "";
                else
                    rowFinca["Municipalidad"] = dsDatos.Tables["Datos"].Rows[i]["Municipalidad"];
                if (dsDatos.Tables["Datos"].Rows[i]["NoEscritura"].ToString() == "")
                    rowFinca["No_Escritura"] = "";
                else
                    rowFinca["No_Escritura"] = dsDatos.Tables["Datos"].Rows[i]["NoEscritura"];
                if (dsDatos.Tables["Datos"].Rows[i]["TituloNotario"].ToString() == "")
                    rowFinca["TituloNotario"] = "";
                else
                    rowFinca["TituloNotario"] = dsDatos.Tables["Datos"].Rows[i]["TituloNotario"];
                if (dsDatos.Tables["Datos"].Rows[i]["Notario"].ToString() == "")
                    rowFinca["NombreNotario"] = "";
                else
                    rowFinca["NombreNotario"] = dsDatos.Tables["Datos"].Rows[i]["Notario"];
                rowFinca["Direccion"] = dsDatos.Tables["Datos"].Rows[i]["Direccion"];
                rowFinca["Aldea"] = dsDatos.Tables["Datos"].Rows[i]["Aldea"];
                rowFinca["Departamento"] = dsDatos.Tables["Datos"].Rows[i]["Departamento"];
                rowFinca["Municipio"] = dsDatos.Tables["Datos"].Rows[i]["Municipio"];
                rowFinca["ColNorte"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Norte"];
                rowFinca["ColSur"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Sur"];
                rowFinca["ColEste"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Este"];
                rowFinca["ColOeste"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Oeste"];
                rowFinca["AreaFinca"] = dsDatos.Tables["Datos"].Rows[i]["Area"];
                rowFinca["NomPropietario"] = dsDatos.Tables["Datos"].Rows[i]["Propietario"];
                rowFinca["TipoIdPropietario"] = dsDatos.Tables["Datos"].Rows[i]["TipoIdPropietario"];
                rowFinca["IdPropietario"] = dsDatos.Tables["Datos"].Rows[i]["DPI"];
                rowFinca["InmuebleId"] = dsDatos.Tables["Datos"].Rows[i]["InmuebleId"];
                rowFinca["TipoPropiedad"] = dsDatos.Tables["Datos"].Rows[i]["Tipo_PersonaId"];
                rowFinca["NombreEmpresa"] = dsDatos.Tables["Datos"].Rows[i]["Nombre_Empresa"];
                Ds_PlanManejo.Tables["DtFinca"].Rows.Add(rowFinca);
            }
            dsDatos.Clear();
            return Ds_PlanManejo;

        }

        public double Get_Sum_Area_Plantacion(int Id, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Sum_Area_Plantacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@AreaTotal", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@AreaTotal"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public double Get_Sum_Area_Fuente(int Id, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Sum_Area_Fuente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@AreaTotal", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@AreaTotal"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet SumaAreasPlantacion(int Id, int Origen)
        {
            Ds_Profesionales Ds_SumaAreaes = new Ds_Profesionales();
            //LLenar Fincas
            double SumaAreas = Get_Sum_Area_Plantacion(Id, Origen);
            Ds_SumaAreaes.Tables["Dt_SumaAreas"].Clear();
            DataRow rowFinca = Ds_SumaAreaes.Tables["Dt_SumaAreas"].NewRow();
            rowFinca["AreaTotal"] = SumaAreas;
            Ds_SumaAreaes.Tables["Dt_SumaAreas"].Rows.Add(rowFinca);
            return Ds_SumaAreaes;

        }

        public void GrabarFincasRegistro(int GestionId, int UsuarioId)
        {
            try
            {

                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GrabarFincasRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insert_Propietarios_Gestion(int GestionId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Insert_Propietarios_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public void Insert_Representantes_Gestion(int GestionId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Insert_Representantes_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public int TieneRepresentantes_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_TieneRepresentantes_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public DataSet GetRepresentantes_Gestion(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetRepresentantes_Gestion", cn);
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
                return ds;
            }
        }

        public string GetNombresRepresentantes_Gestion_Juntos(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetNombresRepresentantes_Gestion_Juntos", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar,8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }

        public string GetDatosFinca_Gestion_Juntos(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetDatosFinca_Gestion_Juntos", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar,8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }

        public string GetDatosFinca_Gestion_Juntos_RegistroPropiedad(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetDatosFinca_Gestion_Juntos_RegistroPropiedad", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }

        public string GetDatosFinca_Gestion_Juntos_SoloNombre(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetDatosFinca_Gestion_Juntos_SoloNombre", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }


        public string GetDatosFinca_Gestion_Juntos_SoloDepMun(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetDatosFinca_Gestion_Juntos_SoloDepMun", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }
        public string GetNombresPropietarios_Gestion_Juntos(int GestionId, int Tipo)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetNombresPropietarios_Gestion_Juntos", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = Tipo;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }

        public string GetNombresPropietarios_Gestion_Juntos_SinFinca(int GestionId, int Tipo, int CategoriaId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_GetNombresPropietarios_Gestion_Juntos_SinFinca", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = Tipo;
                cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = CategoriaId;
                cmd.Parameters.Add("@Resul", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return "";
            }
        }

        public int Get_Tipo_Propietario_Finca(int UsuarioId, int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Tipo_Propietario_Finca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@TipoPersonaId", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@TipoPersonaId"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_propietarios_Temp_Finca(int UsuarioId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_propietarios_Temp_Finca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet Get_Otras_Temp_Finca(int UsuarioId, int InmuebleId)
        {
            try
            {
                if (ds.Tables["DATOS2"] != null)
                    ds.Tables.Remove("DATOS2");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Otras_Temp_Finca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS2");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Existe_Propietarios_OtroInmueble(int UsuarioId, int InmuebleId, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Existe_Propietarios_OtroInmueble", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
                cmd.Parameters.Add("@Existe", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Existe"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_Fincas_Completas(int UsuarioId)
        {
            try
            {
                if (ds.Tables["DATOS2"] != null)
                    ds.Tables.Remove("DATOS2");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Fincas_Completas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS2");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public int Get_Tipo_Persona_Fincas(int Tipo, int Id)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Tipo_Persona_Fincas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_Propietrios_Caratula(int GestionId, int CategoriaId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Propietrios_Caratula", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
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

        public DataSet Get_Representantes_Caratula(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Representantes_Caratula", cn);
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
                return ds;
            }
        }

        public int Finca_EnGestion_Registro(int InmuebleId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Finca_EnGestion_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string Get_Propietarios_Gestion_Registro(int GestionId, int CategoriaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Propietarios_Gestion_Registro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@CategoriaId", OleDbType.Integer).Value = CategoriaId;
                cmd.Parameters.Add("@Nombres", OleDbType.VarChar, 7000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Nombres"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "No Existe Actividad";
            }
        }

        public string Get_Propietarios_Manejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Propietarios_Manejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Nombres", OleDbType.VarChar, 7000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Nombres"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "No Existe Actividad";
            }
        }

        public string Get_Propietarios_Manejo_DPI(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Propietarios_Manejo_DPI", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Nombres", OleDbType.VarChar, 7000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Nombres"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "No Existe Actividad";
            }
        }


        public DataSet Get_TiposInmuebles(int GestionId, int ModuloId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_Get_TiposInmuebles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@ModuloId", OleDbType.Integer).Value = ModuloId;
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

        public int CuantosPropietarios_GestionRegistro(int Tipo, int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_CuantosPropietarios_GestionRegistro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int CuantosPropietarios_GestionManejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_CuantosPropietarios_GestionManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Get_SubCategoria_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_SubCategoria_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Get_GestionId_GestionIncompleta(int GestionIncompletaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_GestionId_GestionIncompleta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionIncompletaId", OleDbType.Integer).Value = GestionIncompletaId;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public void Insert_DatosEmpresa_Gestion(int GestionId, int HorasTurno, int Empleados_Fijo, int Empleados_NoFijo, int DiasYear, int StockMotosierra)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_DatosEmpresa_Gestion", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@HorasTurno", SqlDbType.Int).Value = HorasTurno;
                cmd.Parameters.Add("@Empleados_Fijo", SqlDbType.Int).Value = Empleados_Fijo;
                cmd.Parameters.Add("@Empleados_NoFijo", SqlDbType.Int).Value = Empleados_NoFijo;
                cmd.Parameters.Add("@DiasYear", SqlDbType.Int).Value = DiasYear;
                cmd.Parameters.Add("@StockMotoSierra", SqlDbType.Int).Value = StockMotosierra;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void BorraGestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_BorraGestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        public DataSet LeerXml_InventarioForestalBosqueNatural(int GestionId, int Tipo) //1 Latifoleada,  2. Conifera, Otro
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("LeerXml_InventarioForestalBosqueNatural", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
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

        public bool existediploma(string NoDiploma)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_existediploma", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NoDiploma", OleDbType.VarChar, 100).Value = NoDiploma;
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

        public bool ExisteNit(string Nit, int PersonaId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_ExisteNit", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nit", OleDbType.VarChar, 100).Value = Nit;
                cmd.Parameters.Add("@PersonaId", OleDbType.Integer).Value = PersonaId;
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

        public DataSet ImpresionDictamenTecnio(int GestionId, int Tipo, int CategoriaId, DataSet DatosDictamen, DataSet DatosEtapa, int UsuarioId) //1VP 2Gestion
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();
            Cl_Especie ClEspecie;
            ClEspecie = new Cl_Especie();

            DataSet dsDataDicTec = Sp_Get_Datos_Dictamen_Tecnico(GestionId, Tipo, UsuarioId);
            Ds_Gestiones Ds_DictamenTecnico = new Ds_Gestiones();
            Ds_DictamenTecnico.Tables["Dt_Dictamen_Tecnico"].Clear();
            DataRow row = Ds_DictamenTecnico.Tables["Dt_Dictamen_Tecnico"].NewRow();
            if (dsDataDicTec.Tables[0].Rows.Count > 0)
            {
                row["Autoriza"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["DictamenId"].ToString(); 
                row["NoSubRegion"] = dsDataDicTec.Tables[0].Rows[0]["Sub_Region"];
                if (Tipo == 1)
                {
                    row["NoInforme"] = "";
                    row["Fecha"] = DateTime.Now;
                    row["NombreTecnico"] = dsDataDicTec.Tables[2].Rows[0]["SubRegional"];
                    row["PuestoTecnico"] = dsDataDicTec.Tables[2].Rows[0]["nombre"];
                }
                else
                {
                    row["NoInforme"] = dsDataDicTec.Tables[0].Rows[0]["No_Dictamen"];
                    row["Fecha"] = dsDataDicTec.Tables[0].Rows[0]["Fecha"];
                    row["NombreTecnico"] = dsDataDicTec.Tables[0].Rows[0]["NombreTecnico"];
                    row["PuestoTecnico"] = dsDataDicTec.Tables[0].Rows[0]["nombre"];
                }
                row["LugarSubRegion"] = dsDataDicTec.Tables[0].Rows[0]["Municipio"] + ", " + dsDataDicTec.Tables[0].Rows[0]["Departamento"];
                
                row["DirectorSubRegional"] = dsDataDicTec.Tables[0].Rows[0]["NombresSubRegional"];
                int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                string Fincas = GetDatosFinca_Gestion_Juntos(GestionId);
                string AgraegadoSol = "";
                string Solicitante = "";
                if (ModuloId == 2)
                {
                    Solicitante = Get_Propietarios_Manejo(GestionId);
                    if (Tipo == 1)
                        AgraegadoSol = Get_CompletaPropietarios(CategoriaId, GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = Get_CompletaPropietarios(CategoriaId, GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                }

                row["Solicitud"] = "Por este medio le informo de la inspección realizada en la finca (s)" + Fincas + " propiedad del (los) señor (a) (es) (as): " + Solicitante + " quien solicita Licencia de Manejo Forestal (" + dsDataDicTec.Tables[1].Rows[0]["SubCategoria"] + ") " + ", para lo cual ha presentado a la oficina Sub-regional " + dsDataDicTec.Tables[0].Rows[0]["Sub_Region"] + ", el expediente identificado con el No." + dsDataDicTec.Tables[1].Rows[0]["No_Expediente"] + ", que el plan de manejo elaborado por " + dsDataDicTec.Tables[1].Rows[0]["NombreRegente"] + ", con Registro de Elaborador de Plan de Manejo No " + dsDataDicTec.Tables[1].Rows[0]["Correlativo"] + ", presentándole a continuación los resultados de la inspección de campo.";
                row["TipoPlan"] = dsDataDicTec.Tables[1].Rows[0]["SubCategoria"];
                row["Propietarios"] = Solicitante;
                DataSet dsAreas = ClManejo.Get_Areas_PlanManejo(GestionId);
                row["AreaBosque"] = dsAreas.Tables["Datos"].Rows[0]["AreaBosque"].ToString();
                row["AreaIntervenir"] = dsAreas.Tables["Datos"].Rows[0]["AreaIntervenir"].ToString();
                row["AreaProteccion"] = dsAreas.Tables["Datos"].Rows[0]["AreaProteccion"].ToString();
                row["OtrosUsos"] = dsAreas.Tables["Datos"].Rows[0]["AreaProteccion"].ToString();
                dsAreas.Clear();
                DataSet dsCaracBio = ClManejo.Get_CaracBiofisicas(GestionId);
                row["ZonaVida"] = dsCaracBio.Tables["Datos"].Rows[0]["Zona_Vida"].ToString();
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, ModuloId);
                if (SubCategoriaId == 4)
                    row["FuentesAgua"] = "N/A";
                dsCaracBio.Clear();
                row["MetodologiaCorro"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["MetodologiaCorro"].ToString();
                row["FormaEvaluacion"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["FormaEvaluacion"].ToString();
                row["MetodologiaInvForestal"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["MetodologiaInvForestal"].ToString();
                row["ConCaracBio"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["ConCaracBio"].ToString();
                row["ConVeracidad"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["ConVeracidad"].ToString();
                row["ConPropuestaManejo"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["ConPropuestaManejo"].ToString();
                row["ConPropuestaTratamiento"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["ConPropuestaTratamiento"].ToString();
                if (Convert.ToInt32(DatosDictamen.Tables["DatosDictamen"].Rows[0]["DictamenId"]) == 1)
                    row["Dictamen"] = "Con base a la  revisión y análisis del expediente en estudio y a la comprobación de campo realizada por el suscrito, se dictamina " + DatosDictamen.Tables["DatosDictamen"].Rows[0]["Dictamen"].ToString() + " la ejecución del plan de manejo forestal, de la (s) Finca(s): " + GetDatosFinca_Gestion_Juntos_SoloNombre(GestionId) + ", con un aprovechamiento de acuerdo al cuadro siguiente:";
                else
                    row["Dictamen"] = "Con base a la  revisión y análisis del expediente en estudio y a la comprobación de campo realizada por el suscrito, se dictamina " + DatosDictamen.Tables["DatosDictamen"].Rows[0]["Dictamen"].ToString() + " la ejecución del plan de manejo forestal, de la (s) Finca(s): " + GetDatosFinca_Gestion_Juntos_SoloNombre(GestionId) + ".";
                
                row["AreaCompromiso"] = ClManejo.Get_Compromiso_Area(GestionId).ToString();
                string Especies = "";
                DataSet EspeciesCompromiso = ClManejo.Get_Especies_Compromiso(GestionId);
                for (int i = 0; i < EspeciesCompromiso.Tables["Datos"].Rows.Count; i++)
                {
                    if (Especies == "")
                        Especies = EspeciesCompromiso.Tables["Datos"].Rows[i]["Codigo_Especie"].ToString();
                    else
                        Especies = Especies + ", " + EspeciesCompromiso.Tables["Datos"].Rows[i]["Codigo_Especie"].ToString();
                }
                EspeciesCompromiso.Clear();
                row["EspeciesCompromiso"] = Especies;
                row["DensidadInicial"] = ClManejo.Get_DensidadIni_Compromiso(GestionId).ToString();
                row["Lugar"] = GetDatosFinca_Gestion_Juntos(GestionId);
                row["DocumentoGarantia"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["TipoGarantia"].ToString();
                string SistemaRepoblacionText = "";
                DataSet SistemaRepoblacion = ClManejo.Get_SistemaRepoblacion_Compromiso(GestionId);
                for (int i = 0; i < SistemaRepoblacion.Tables["Datos"].Rows.Count; i++)
                {
                    if (SistemaRepoblacionText == "")
                        SistemaRepoblacionText = SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
                    else
                        SistemaRepoblacionText = SistemaRepoblacionText + ", " + SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
                }
                SistemaRepoblacion.Clear();
                row["Metodo"] = SistemaRepoblacionText;
                if (Convert.ToInt32(DatosDictamen.Tables["DatosDictamen"].Rows[0]["DictamenId"]) == 1)
                {
                    row["MontoCompromiso"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["MontoGarantia"].ToString();
                    row["PorGarantia"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["PorGarantia"].ToString();
                    row["TotalMaderaPie"] = "El titular de la licencia se obliga a pagar al fondo forestal privativo del INAB por concepto de derecho de corta un valor de Q. " + DatosDictamen.Tables["DatosDictamen"].Rows[0]["TotValMaderaPie"] + " de acuerdo al cuadro siguiente:";
                    row["RecomendacionUno"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["RecomendacionUno"].ToString();
                    row["RecomendacionDos"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["RecomendacionDos"].ToString();
                    row["OtrasRecomendaciones"] = DatosDictamen.Tables["DatosDictamen"].Rows[0]["OtrasRecomendaciones"].ToString();
                }
                
                dsDataDicTec.Clear();
            }
            
            Ds_DictamenTecnico.Tables["Dt_Dictamen_Tecnico"].Rows.Add(row);

            //Fincas
            Ds_DictamenTecnico.Tables["DtFincasDictTecnico"].Clear();
            DataSet FincasDicTec = ClManejo.GetFincaPlanManejoPol(GestionId, 2);
            for (int i = 0; i < FincasDicTec.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowFincas = Ds_DictamenTecnico.Tables["DtFincasDictTecnico"].NewRow();
                rowFincas["Finca"] = FincasDicTec.Tables["Datos"].Rows[i]["Finca"];
                rowFincas["Area"] = FincasDicTec.Tables["Datos"].Rows[i]["Area"];
                rowFincas["UbGeografica"] = FincasDicTec.Tables["Datos"].Rows[i]["UbicacionGeo"];
                rowFincas["UbPolitica"] = FincasDicTec.Tables["Datos"].Rows[i]["UbPol"];
                rowFincas["Colindancias"] = FincasDicTec.Tables["Datos"].Rows[i]["Colindancias"];
                Ds_DictamenTecnico.Tables["DtFincasDictTecnico"].Rows.Add(rowFincas);
            }
            FincasDicTec.Clear();

            //ResumenInventario
            DataSet dsResumenInv =  ClManejo.Get_Resumen_Censo(2, GestionId);
            Ds_DictamenTecnico.Tables["Dt_ResumenInvDicTec"].Clear();
            for (int i = 0; i < dsResumenInv.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowData = Ds_DictamenTecnico.Tables["Dt_ResumenInvDicTec"].NewRow();
                rowData["Rodal"] = dsResumenInv.Tables["Datos"].Rows[i]["Rodal"];
                rowData["AreaRodal"] = dsResumenInv.Tables["Datos"].Rows[i]["AreaRodal"];
                rowData["ClaseDesarrollo"] = dsResumenInv.Tables["Datos"].Rows[i]["Clase_Desarrollo"];
                rowData["Pendiente"] = dsResumenInv.Tables["Datos"].Rows[i]["Pendiente"];
                rowData["Especie"] = dsResumenInv.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                rowData["DapMedio"] = dsResumenInv.Tables["Datos"].Rows[i]["Dap"];
                rowData["AlturaMedia"] = dsResumenInv.Tables["Datos"].Rows[i]["Altura"];
                rowData["Densidad"] = dsResumenInv.Tables["Datos"].Rows[i]["Densidad"];
                rowData["AreaBasalha"] = dsResumenInv.Tables["Datos"].Rows[i]["AreaBasal"];
                rowData["AreaBasalRodal"] = dsResumenInv.Tables["Datos"].Rows[i]["AreaBasalRodal"];
                rowData["VolHa"] = dsResumenInv.Tables["Datos"].Rows[i]["VolHa"];
                rowData["VolRodal"] = dsResumenInv.Tables["Datos"].Rows[i]["VolRodal"];
                Ds_DictamenTecnico.Tables["Dt_ResumenInvDicTec"].Rows.Add(rowData);
            }
            FincasDicTec.Clear();

            
            //Silvicultura
            DataSet dsSilvilculturaInv = ClManejo.Get_Resumen_Censo(2, GestionId);
            Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Clear();
            for (int i = 0; i < dsSilvilculturaInv.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowData = Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].NewRow();
                rowData["Correlativo"] = dsSilvilculturaInv.Tables["Datos"].Rows[i]["Correlativo"];
                rowData["Turno"] = 0;
                rowData["Rodal"] = dsSilvilculturaInv.Tables["Datos"].Rows[i]["Rodal"];
                rowData["Area"] = dsSilvilculturaInv.Tables["Datos"].Rows[i]["AreaRodal"];
                rowData["Especie"] = dsSilvilculturaInv.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                rowData["Tratamiento"] = "";
                rowData["VolTroza"] = 0;
                rowData["VolLena"] = 0;
                rowData["VolTotal"] = 0;
                Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows.Add(rowData);
            }
            dsSilvilculturaInv.Clear();

            if (Convert.ToInt32(DatosDictamen.Tables["DatosDictamen"].Rows[0]["DictamenId"]) == 1)
            {
                for (int i = 0; i < Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows.Count; i++)
                {
                    DataSet DsDatosExtrae = ClManejo.Get_Dato_Silvicultura_Extrae_PlanManejo(GestionId, Convert.ToInt32(Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["Correlativo"]));
                    if (DsDatosExtrae.Tables.Count > 0)
                    {
                        Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["Turno"] = DsDatosExtrae.Tables["Datos"].Rows[0]["Turno"].ToString();
                        if (DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento"].ToString() == "Otro")
                            Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["Tratamiento"] = DsDatosExtrae.Tables["Datos"].Rows[0]["Otro"].ToString();
                        else
                            Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["Tratamiento"] = DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento"].ToString();
                        Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["VolTroza"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTroza"].ToString();
                        Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["VolLena"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolLena"].ToString();
                        Ds_DictamenTecnico.Tables["Dt_Silvicultura_DicTec"].Rows[i]["VolTotal"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTotal"].ToString();
                        DsDatosExtrae.Clear();
                    }
                }
            }

            


            //Etapas
            Ds_DictamenTecnico.Tables["Dt_Etapa_DictTec"].Clear();
            for (int i = 0; i < DatosEtapa.Tables["Etapa"].Rows.Count; i++)
            {
                DataRow rowData = Ds_DictamenTecnico.Tables["Dt_Etapa_DictTec"].NewRow();
                rowData["Etapa"] = DatosEtapa.Tables["Etapa"].Rows[i]["Etapa"];
                rowData["FecIni"] = DatosEtapa.Tables["Etapa"].Rows[i]["FecIni"];
                rowData["FecFin"] = DatosEtapa.Tables["Etapa"].Rows[i]["FecFin"];
                Ds_DictamenTecnico.Tables["Dt_Etapa_DictTec"].Rows.Add(rowData);
            }


            if (Convert.ToInt32(DatosDictamen.Tables["DatosDictamen"].Rows[0]["DictamenId"]) == 1)
            {
                //MaderaPie
                DataSet dsMaderaPie = ClManejo.Get_Resumen_Censo(2, GestionId);
                Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Clear();
                for (int i = 0; i < dsMaderaPie.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow rowData = Ds_DictamenTecnico.Tables["Dt_MaderaPie"].NewRow();
                    rowData["Correlativo"] = dsMaderaPie.Tables["Datos"].Rows[i]["Correlativo"];
                    rowData["Turno"] = 0;
                    rowData["Rodal"] = dsMaderaPie.Tables["Datos"].Rows[i]["Rodal"];
                    rowData["Especie"] = dsMaderaPie.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                    rowData["EspecieId"] = dsMaderaPie.Tables["Datos"].Rows[i]["EspecieId"];
                    rowData["VolTroza"] = 0;
                    rowData["VolLena"] = 0;
                    rowData["VolTotal"] = 0;
                    rowData["VolMaderaTroza"] = 0;
                    rowData["ValorLenaTroza"] = 0;
                    rowData["ValorTotal"] = 0;
                    rowData["ValorPagar"] = 0;
                    Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows.Add(rowData);
                }
                dsMaderaPie.Clear();

                for (int i = 0; i < Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows.Count; i++)
                {
                    DataSet DsDatosExtrae = ClManejo.Get_Dato_Silvicultura_Extrae_PlanManejo(GestionId, Convert.ToInt32(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["Correlativo"]));
                    Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["Turno"] = DsDatosExtrae.Tables["Datos"].Rows[0]["Turno"].ToString();
                    Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolTroza"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTroza"].ToString();
                    Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolLena"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolLena"].ToString();
                    Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolTotal"] = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTotal"].ToString();
                    DataSet ValorMadera = ClEspecie.Valor_MaderaPie_Especie(Convert.ToInt32(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["EspecieId"]), GestionId);
                    if (ValorMadera.Tables["Datos"].Rows.Count > 0)
                    {
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolMaderaTroza"] = (Convert.ToDouble(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolTroza"]) * Convert.ToDouble(ValorMadera.Tables["Datos"].Rows[0]["VolTroza"])).ToString();
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorLenaTroza"] = (Convert.ToDouble(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolLena"]) * Convert.ToDouble(ValorMadera.Tables["Datos"].Rows[0]["VolLena"])).ToString();
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorTotal"] = (Convert.ToDouble(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorLenaTroza"]) + Convert.ToDouble(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolMaderaTroza"])).ToString();
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorPagar"] = (((Convert.ToDouble(Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorTotal"]) / 100) * 10)).ToString();
                    }
                    else
                    {
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["VolMaderaTroza"] = "0";
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorLenaTroza"] = "0";
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorTotal"] = "0";
                        Ds_DictamenTecnico.Tables["Dt_MaderaPie"].Rows[i]["ValorPagar"] = "0";
                    }
                    ValorMadera.Clear();
                    DsDatosExtrae.Clear();
                }

            
            }

            return Ds_DictamenTecnico;
        }

        public DataSet Sp_Get_Datos_Dictamen_Tecnico(int GestionId, int Tipo, int UsuarioId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Datos_Dictamen_Tecnico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
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

        public int Max_Dictamen_Tecnico()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Dictamen_Tecnico", cn);
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

        public int Max_Enmienda_Tecnico()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Enmienda_Tecnico", cn);
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

        public int Max_Dictamen_SubRegional()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Dictamen_SubRegional", cn);
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

        public int Max_Enmiendas_SubRegional()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_Enmiendas_SubRegional", cn);
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

        public int Max_LicenciaForestal()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_LicenciaForestal", cn);
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

        public int Max_OficioEnmiendaRegional()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Max_OficioEnmiendaRegional", cn);
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

        public void Insert_Dictamen_Tecnico(int Dictamen_TecnicoId, int GestionId, string Metodologia_Corroboracion, string Metodologia_Resultados, int Tipo_InventarioId, int TotalRodal, int RodalesMuestreados, int Tamano, int Forma_ParcelaId, string Conclusion_Biofisica, string Conclusion_Veracidad, string Conclusion_PropuestaManejo, string Conclusion_Propuesta_Tratamiento, int Tipo_DictamenId, int Tipo_CalculoCompromisoId, int Tipo_GarantiaId, XmlDocument Etapa, XmlDocument MaderaPie, int Vigencia, int Notas_Autorizadas, int Notas_Entregar, int Notas_Restantes, int Tipo_UsuarioIdDictamenTec, int UsuarioId, int SubRegionId, string OtrasRecomendacion)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_DictamenTecnico", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DictamenTecnicoId", SqlDbType.Int).Value = Dictamen_TecnicoId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Metodologia_Corroboracion", SqlDbType.VarChar, 8000).Value = Metodologia_Corroboracion;
                cmd.Parameters.Add("@Metodologia_Resultados", SqlDbType.VarChar, 8000).Value = Metodologia_Resultados;
                cmd.Parameters.Add("@Tipo_InventarioId", SqlDbType.Int).Value = Tipo_InventarioId;
                cmd.Parameters.Add("@TotalRodal", SqlDbType.Int).Value = TotalRodal;
                cmd.Parameters.Add("@RodalesMuestreados", SqlDbType.Int).Value = RodalesMuestreados;
                if (Tipo_InventarioId == 1)
                {
                    cmd.Parameters.Add("@Tamano", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Forma_ParcelaId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@Tamano", SqlDbType.Int).Value = Tamano;
                    cmd.Parameters.Add("@Forma_ParcelaId", SqlDbType.Int).Value = Forma_ParcelaId;
                }
                cmd.Parameters.Add("@Conclusion_Biofisica", SqlDbType.VarChar, 8000).Value = Conclusion_Biofisica;
                cmd.Parameters.Add("@Conclusion_Veracidad", SqlDbType.VarChar, 8000).Value = Conclusion_Veracidad;
                cmd.Parameters.Add("@Conclusion_PropuestaManejo", SqlDbType.VarChar, 8000).Value = Conclusion_PropuestaManejo;
                cmd.Parameters.Add("@Conclusion_Propuesta_Tratamiento", SqlDbType.VarChar, 8000).Value = Conclusion_Propuesta_Tratamiento;
                cmd.Parameters.Add("@Tipo_DictamenId", SqlDbType.Int).Value = Tipo_DictamenId;
                if (Tipo_CalculoCompromisoId == 0)
                    cmd.Parameters.Add("@Tipo_CalculoCompromisoId", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Tipo_CalculoCompromisoId", SqlDbType.Int).Value = Tipo_CalculoCompromisoId;
                if (Tipo_GarantiaId == 0)
                    cmd.Parameters.Add("@Tipo_GarantiaId", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Tipo_GarantiaId", SqlDbType.Int).Value = Tipo_GarantiaId;
                cmd.Parameters.Add("@Etapa", SqlDbType.Xml).Value = Etapa.OuterXml.ToString();
                cmd.Parameters.Add("@MaderaPie", SqlDbType.Xml).Value = MaderaPie.OuterXml.ToString();
                cmd.Parameters.Add("@Vigencia", SqlDbType.Int).Value = Vigencia;
                cmd.Parameters.Add("@Notas_Autorizadas", SqlDbType.Int).Value = Notas_Autorizadas;
                if (Notas_Autorizadas > 10)
                {
                    cmd.Parameters.Add("@Notas_Entregar", SqlDbType.Int).Value = Notas_Entregar;
                    cmd.Parameters.Add("@Notas_Restantes", SqlDbType.Int).Value = Notas_Restantes;
                    cmd.Parameters.Add("@Tipo_UsuarioIdDictamenTec", SqlDbType.Int).Value = Tipo_UsuarioIdDictamenTec;
                }
                else
                {
                    cmd.Parameters.Add("@Notas_Entregar", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Notas_Restantes", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Tipo_UsuarioIdDictamenTec", SqlDbType.Int).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = @SubRegionId;
                cmd.Parameters.Add("@OtrasRecomendacion", SqlDbType.VarChar,8000).Value = @OtrasRecomendacion;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }



        public void Insert_Enmiendas_Tecnico(int Enmienda_Tec_GestionId, int GestionId, int UsuarioIdSubRegional, int UsuarioId, XmlDocument Enmiendas, int SubRegionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_Enmiendas_Tecnico", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Enmienda_Tec_GestionId", SqlDbType.Int).Value = Enmienda_Tec_GestionId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@UsuarioIdSubRegional", SqlDbType.Int).Value = UsuarioIdSubRegional;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@Enmiendas", SqlDbType.Xml).Value = Enmiendas.OuterXml.ToString();
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = @SubRegionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }
        

        public DataSet Sp_Get_Datos_Dictamen_SubRegional(int GestionId, int Tipo, int UsuarioId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetDatos_DictemenSubRegional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet ImpresionOficioEnmiendasSubRegional(int GestionId, int Tipo, int UsuarioId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_ImpresionOficioEnmiendasSubRegional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet ImpresionOficioEnmiendasRegional(int GestionId, int Tipo, int UsuarioId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_ImpresionOficioEnmiendasRegional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public DataSet GetEnmiendasJuridicas(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetEnmiendasJuridicas", cn);
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
                return ds;
            }
        }

        public DataSet GetEnmiendasRegional(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetEnmiendasRegional", cn);
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
                return ds;
            }
        }

        public DataSet GetEnmiendasTecnicas(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetEnmiendasTecnicas", cn);
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
                return ds;
            }
        }

        public DataSet ImpresionOficioEnmiendasRegional(int GestionId, int Tipo, int UsuarioId, DataSet dsEnmiendas) //1VP 2Gestion int CategoriaId, DataSet DatosDictamen, DataSet DatosEtapa, 
        {
            DataSet dsDatosOficioEnmiendasRegional = ImpresionOficioEnmiendasRegional(GestionId, Tipo, UsuarioId);
            Ds_Gestiones Ds_OficioEnmiendasRegional = new Ds_Gestiones();
            Ds_OficioEnmiendasRegional.Tables["Dt_EnmiendaRegional"].Clear();
            
            if (dsDatosOficioEnmiendasRegional.Tables[0].Rows.Count > 0)
            {
                string AgraegadoSol = "";
                string Solicitante = "";
                int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                Solicitante = Get_Propietarios_Manejo(GestionId);
                AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                if (AgraegadoSol != "")
                    Solicitante = Solicitante + " " + AgraegadoSol + ".";
                else
                    Solicitante = Solicitante + ".";
                string Fincas = GetDatosFinca_Gestion_Juntos(GestionId);
                string Saludo = "Por este medio le informo que previo a continuar con el trámite del expediente No: " + dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["No_Expediente"].ToString() + ", propiedad de " + Solicitante + ", ubicado en " + Fincas + ", deberá solicitar al propietario las siguientes enmiendas:";
                if (Tipo == 1)
                {
                    for (int i = 0; i < dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows.Count; i++)
                    {
                        DataRow row = Ds_OficioEnmiendasRegional.Tables["Dt_EnmiendaRegional"].NewRow();
                        row["Region"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["No_Region"].ToString();
                        row["NoOficio"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["NoOficio"].ToString();
                        row["Fecha"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["FechaOficio"].ToString();
                        row["Subregional"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Subregional"].ToString();
                        row["SubRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                        row["LugarSubRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["MunSubReg"].ToString() + ", " + dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["DepSubReg"].ToString();
                        row["Saludo"] = Saludo;
                        row["Enmienda"] = dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows[i]["Enmienda"];
                        row["Regional"] = dsDatosOficioEnmiendasRegional.Tables["Datos1"].Rows[0]["Nombres"].ToString();
                        row["LugarRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["MunSubReg"].ToString() + ", " + dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["DepSubReg"].ToString();
                        row["PuestoRegional"] = dsDatosOficioEnmiendasRegional.Tables["Datos1"].Rows[0]["Puesto"].ToString();
                        Ds_OficioEnmiendasRegional.Tables["Dt_EnmiendaRegional"].Rows.Add(row);
                    }
                    return Ds_OficioEnmiendasRegional;
                    
                }
                else if (Tipo == 2)
                {
                    for (int i = 0; i < dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows.Count; i++)
                    {
                        DataRow row = Ds_OficioEnmiendasRegional.Tables["Dt_EnmiendaRegional"].NewRow();
                        row["Region"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["No_Region"].ToString();
                        row["NoOficio"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["NoOficio"].ToString();
                        row["Fecha"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["FechaOficio"].ToString();
                        row["Subregional"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Subregional"].ToString();
                        row["SubRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                        row["LugarSubRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["MunSubReg"].ToString() + ", " + dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["DepSubReg"].ToString();
                        row["Saludo"] = Saludo;
                        row["Enmienda"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Enmienda"].ToString();
                        row["Regional"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Regional"].ToString();
                        row["LugarRegion"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["MunSubReg"].ToString() + ", " + dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["DepSubReg"].ToString();
                        row["PuestoRegional"] = dsDatosOficioEnmiendasRegional.Tables["Datos"].Rows[0]["Puesto"].ToString();
                        Ds_OficioEnmiendasRegional.Tables["Dt_EnmiendaRegional"].Rows.Add(row);
                    }
                    return Ds_OficioEnmiendasRegional;
                }
                return ds;
                
            }
            return ds;

        }
        

        public DataSet ImpresionOficioEnmiendasSubRegional(int GestionId, int Tipo, int UsuarioId, DataSet dsEnmiendas) //1VP 2Gestion int CategoriaId, DataSet DatosDictamen, DataSet DatosEtapa, 
        {
            DataSet dsDatosOficioEnmiendasSubRegional = ImpresionOficioEnmiendasSubRegional(GestionId, Tipo, UsuarioId);
            Ds_Gestiones Ds_OficioEnmiendasSubRegional = new Ds_Gestiones();
            Ds_OficioEnmiendasSubRegional.Tables["DtOficioEnmiendaSubRegional"].Clear();
            DataRow row = Ds_OficioEnmiendasSubRegional.Tables["DtOficioEnmiendaSubRegional"].NewRow();
            if (dsDatosOficioEnmiendasSubRegional.Tables[0].Rows.Count > 0)
            {
                if (Tipo == 1)
                {
                    row["NoSubRegion"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                    row["NoOficio"] = "";
                    row["LugarSubRegion"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Municipio"].ToString() + ", " + dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Departamento"].ToString();
                    row["Fecha"] = DateTime.Now;
                    string AgraegadoSol = "";
                    string Solicitante = "";
                    int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                    Solicitante = Get_Propietarios_Manejo(GestionId);
                    if (Tipo == 1)
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    row["Propietario"] = Solicitante;
                    row["Elaborador"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Elaborador"].ToString();
                    row["NoExpediente"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["No_Expediente"].ToString();
                    row["TieneEnmiendasJur"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["TieneEnimendaJur"].ToString();
                    row["TieneEnmiendasTec"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["TieneEnminedaTec"].ToString();
                    row["TieneEnmiendasSubReg"] = dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows.Count;
                    row["SubRegional"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows[0]["Usuario"].ToString();
                    row["Puesto"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows[0]["Puesto"].ToString();
                    Ds_OficioEnmiendasSubRegional.Tables["DtOficioEnmiendaSubRegional"].Rows.Add(row);
                    
                }
                else if (Tipo == 2)
                {
                    row["NoSubRegion"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                    row["NoOficio"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["NoOficio"].ToString();
                    row["LugarSubRegion"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Municipio"].ToString() + ", " + dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Departamento"].ToString();
                    row["Fecha"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Fecha"].ToString();
                    string AgraegadoSol = "";
                    string Solicitante = "";
                    int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                    Solicitante = Get_Propietarios_Manejo(GestionId);
                    if (Tipo == 2)
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    row["Propietario"] = Solicitante;
                    row["Elaborador"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Elaborador"].ToString();
                    row["NoExpediente"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["No_Expediente"].ToString();
                    row["TieneEnmiendasJur"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["TieneEnimendaJur"].ToString();
                    row["TieneEnmiendasTec"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["TieneEnminedaTec"].ToString();
                    row["TieneEnmiendasSubReg"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows.Count;
                    row["SubRegional"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Subregional"].ToString();
                    row["Puesto"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos"].Rows[0]["Puesto"].ToString();
                    Ds_OficioEnmiendasSubRegional.Tables["DtOficioEnmiendaSubRegional"].Rows.Add(row);
                }

                if (Tipo == 1)
                {
                    Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].Clear();
                    if (dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows.Count; i++)
                        {
                            DataRow rowEJ = Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].NewRow();
                            rowEJ["Enmienda"] = dsEnmiendas.Tables["Dt_Enminedas_Subregional"].Rows[i]["Enmienda"].ToString();
                            rowEJ["No"] = i + 1;
                            Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].Rows.Add(rowEJ);
                        }
                    }
                }
                else if (Tipo == 2)
                {
                    Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].Clear();
                    if (dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows.Count; i++)
                        {
                            DataRow rowEJ = Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].NewRow();
                            rowEJ["Enmienda"] = dsDatosOficioEnmiendasSubRegional.Tables["Datos1"].Rows[i]["Enmienda"].ToString();
                            rowEJ["No"] = i + 1;
                            Ds_OficioEnmiendasSubRegional.Tables["Dt_EnmiendasSubregionalDet"].Rows.Add(rowEJ);
                        }
                    }
                }

                dsDatosOficioEnmiendasSubRegional.Clear();
                

                DataSet dsDatosEnmeindaJuridica = GetEnmiendasJuridicas(GestionId);
                Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_JuridicasDet"].Clear();
                
                if (dsDatosEnmeindaJuridica.Tables["DATOS"].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDatosEnmeindaJuridica.Tables["DATOS"].Rows.Count; i++)
                    {
                        DataRow rowEJ = Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_JuridicasDet"].NewRow();
                        rowEJ["NoDictamen"] = dsDatosEnmeindaJuridica.Tables["Datos"].Rows[i]["No_Dictamen"].ToString();
                        rowEJ["Enmienda"] = dsDatosEnmeindaJuridica.Tables["Datos"].Rows[i]["Enmienda"].ToString();
                        rowEJ["No"] = i + 1;
                        Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_JuridicasDet"].Rows.Add(rowEJ);
                    }
                    
                }
                dsDatosEnmeindaJuridica.Clear();


                DataSet dsDatosEnmeindaTecnica = GetEnmiendasTecnicas(GestionId);
                Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_TecnicasDet"].Clear();

                if (dsDatosEnmeindaTecnica.Tables["DATOS"].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDatosEnmeindaTecnica.Tables["DATOS"].Rows.Count; i++)
                    {
                        DataRow rowEJ = Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_TecnicasDet"].NewRow();
                        rowEJ["NoInforme"] = dsDatosEnmeindaTecnica.Tables["Datos"].Rows[i]["No_Informe"].ToString();
                        rowEJ["Enmienda"] = dsDatosEnmeindaTecnica.Tables["Datos"].Rows[i]["Enmienda"].ToString();
                        rowEJ["No"] = i + 1;
                        Ds_OficioEnmiendasSubRegional.Tables["Dt_Enmiendas_TecnicasDet"].Rows.Add(rowEJ);
                    }
                    
                }
                dsDatosEnmeindaTecnica.Clear();

                
                

                return Ds_OficioEnmiendasSubRegional;
            }
            return ds;

        }

        
        
        public DataSet ImpresionDictamenSubRegional(int GestionId, int Tipo, int UsuarioId, int Folios, string Dictamen) //1VP 2Gestion int CategoriaId, DataSet DatosDictamen, DataSet DatosEtapa, 
        {
            DataSet dsDatosDictamenSubRegional = Sp_Get_Datos_Dictamen_SubRegional(GestionId, Tipo, UsuarioId);
            
            Ds_Gestiones Ds_DictamenSubRegional = new Ds_Gestiones();
            Ds_DictamenSubRegional.Tables["Dt_DictamenSubRegional"].Clear();
            DataRow row = Ds_DictamenSubRegional.Tables["Dt_DictamenSubRegional"].NewRow();
            if (dsDatosDictamenSubRegional.Tables[0].Rows.Count > 0)
            {
                if (Tipo == 1)
                {
                    row["NoSubRegion"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                    row["LugarSubRegion"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Municipio"].ToString() + ", " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Departamento"].ToString();
                    row["NoDictamen"] = "";
                    row["NoExpediente"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["No_Expediente"].ToString();
                    row["FechaExp"] = DateTime.Now;
                    string Fincas = GetDatosFinca_Gestion_Juntos(GestionId);
                    string AgraegadoSol = "";
                    string Solicitante = "";
                    int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                    Solicitante = Get_Propietarios_Manejo(GestionId);
                    if (Tipo == 1)
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    row["Asunto"] = Solicitante + " solicita aprobación del Plan de Manejo para " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["SubCategoria"].ToString() + " en finca (s) " + Fincas;
                    row["Dictamen"] = "Con fecha " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + ", el señor (a) (es): " + Solicitante + ", en su calidad de propietario (s) presentó solicitud a efecto se le autorice implementar Plan de Manejo de " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["SubCategoria"].ToString() + " constando a " + Folios + " folios; el informe jurídico No. " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["DictamenJuridico"].ToString() + " del Asesor (a) " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Juridico"].ToString() + ", quien se pronunció en forma favorable  en cuanto a la petición formulada  y el informe del Técnico No. " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["DictamenTecnico"].ToString() + " emitido por " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Tecnico"].ToString() + " quien verificó las circunstancias consignadas en el plan de manejo. Habiendo sido objeto de análisis la petición planteada, esta Dirección Sub Regional considera que la solicitud llena los requisitos tanto desde el punto de vista técnico como legal. En virtud de lo anterior, salvo mejor opinión al respecto el suscrito RECOMIENDA que es " + Dictamen + " acceder a lo solicitado por " + Solicitante;
                    row["NombreSubregional"] = dsDatosDictamenSubRegional.Tables["Datos1"].Rows[0]["SubRegional"].ToString();
                    row["PuestoSubRegional"] = dsDatosDictamenSubRegional.Tables["Datos1"].Rows[0]["Puesto"].ToString();
                    Ds_DictamenSubRegional.Tables["Dt_DictamenSubRegional"].Rows.Add(row);
                }
                else
                {
                    row["NoSubRegion"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Sub_Region"].ToString();
                    row["LugarSubRegion"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Municipio"].ToString() + ", " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Departamento"].ToString();
                    row["NoDictamen"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["DictamenSubRegional"].ToString();
                    row["NoExpediente"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["No_Expediente"].ToString();
                    row["FechaExp"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["FecDictRegional"].ToString();
                    string Fincas = GetDatosFinca_Gestion_Juntos(GestionId);
                    string AgraegadoSol = "";
                    string Solicitante = "";
                    int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                    Solicitante = Get_Propietarios_Manejo(GestionId);
                    if (Tipo == 1)
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    row["Asunto"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Asunto"].ToString();
                    row["Dictamen"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Dictamen"].ToString();
                    row["NombreSubregional"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["SubRegional"].ToString();
                    row["PuestoSubRegional"] = dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Puesto"].ToString();
                    Ds_DictamenSubRegional.Tables["Dt_DictamenSubRegional"].Rows.Add(row);
                }
            }

            dsDatosDictamenSubRegional.Clear();
            return Ds_DictamenSubRegional;
        }

        public void Insert_DictamenSubRegional(int DictamenId, int GestionId, int ConsideraId, int Folios, int UsuarioId, int SubRegionId, string Asunto, string Dictamen)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_DictamenSubRegional", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DictamenId", SqlDbType.Int).Value = DictamenId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@ConsideraId", SqlDbType.Int).Value = ConsideraId;
                cmd.Parameters.Add("@Folios", SqlDbType.Int).Value = Folios;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = SubRegionId;

                cmd.Parameters.Add("@Asunto", SqlDbType.Text).Value = Asunto;
                cmd.Parameters.Add("@Dictamen", SqlDbType.Text).Value = Dictamen;
                

                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Sp_Insert_EnmiendasSubRegional(int OficioEnmiendaId, int GestionId,  int UsuarioId, int SubRegionId, XmlDocument Enmiendas)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_EnmiendasSubRegional", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OficioEnmiendaSubregionalId", SqlDbType.Int).Value = OficioEnmiendaId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@SubRegionId", SqlDbType.Int).Value = SubRegionId;
                cmd.Parameters.Add("@Enmiendas", SqlDbType.Xml).Value = Enmiendas.OuterXml.ToString();


                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet ImpresionEnmiendasTecnicas(int GestionId, int Tipo)
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();
            DataSet DsEnmiendasTec = ClManejo.Sp_Get_Enmiendas_Tec(GestionId, Tipo);
            if (DsEnmiendasTec.Tables["Datos"].Rows.Count > 0)
            {
                Ds_Gestiones Ds_Enmiendas_Tec = new Ds_Gestiones();
                Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].Clear();
                int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, ModuloId);
                int CategoriaId = Get_CategoriaManejoId(SubCategoriaId);
                string Solicitante = "";
                Solicitante = Get_Propietarios_Manejo(GestionId);
                string AgraegadoSol = Get_CompletaPropietarios(CategoriaId,GestionId, ModuloId);
                if (AgraegadoSol != "")
                    Solicitante = Solicitante + " " + AgraegadoSol + ".";
                else
                    Solicitante = Solicitante + ".";
                string Cuerpo = "Por este medio le informo que luego de la revisión, análisis e inspección realizada del expediente No. " + DsEnmiendasTec.Tables["Datos"].Rows[0]["No_Expediente"] + " cuyo (s) propietario (s) es (son): " + Solicitante + " quien solicita la implementación de Plan de Manejo para " + Get_SubCategoriaManejo(SubCategoriaId) + " en la (s) finca (s): " + GetDatosFinca_Gestion_Juntos(GestionId) + ". Dicho Plan contiene inventario y plan de manejo elaborado por " + DsEnmiendasTec.Tables["Datos"].Rows[0]["Regente"] + ", con Registro de Elaborador de Plan de Manejo No: " + DsEnmiendasTec.Tables["Datos"].Rows[0]["Correlativo"] + ", se determinó que es necesario completar el mismo con los datos siguientes:";
                for (int i = 0; i < DsEnmiendasTec.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].NewRow();
                    row["SubRegion"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["Sub_Region"];
                    row["InformeNo"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["No_Informe"];
                    row["LugarSubRegion"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["Municipio"] + ", " + DsEnmiendasTec.Tables["Datos"].Rows[i]["Departamento"];
                    row["Fecha"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["Fecha"];
                    row["Subregional"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["SubRegional"];
                    row["Cuerpo"] = Cuerpo;
                    row["No"] = (i + 1).ToString() + ".";
                    row["Enmienda"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["Enmienda"];
                    row["Tecnico"] = DsEnmiendasTec.Tables["Datos"].Rows[i]["Tecnico"];
                    Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].Rows.Add(row);
                }
                return Ds_Enmiendas_Tec;
            }
            return ds;
        }


        public DataSet ImpresionLicencia(int GestionId, int Tipo, int UsuarioId, int Periodo, DateTime FecIni, DateTime FecFin, string Aprueba) //1VP 2Gestion int CategoriaId, DataSet DatosDictamen, DataSet DatosEtapa, 
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();
            Cl_Catalogos ClCatalogos;
            ClCatalogos = new Cl_Catalogos();

            DataSet dsDatosLicencia = Get_Datos_Licencia_Forestal(GestionId, Tipo, UsuarioId);

            Ds_Gestiones Ds_Licencia = new Ds_Gestiones();
            Ds_Licencia.Tables["Dt_Licencia"].Clear();
            DataRow row = Ds_Licencia.Tables["Dt_Licencia"].NewRow();
            if (dsDatosLicencia.Tables[0].Rows.Count > 0)
            {
                row["NoRegion"] = dsDatosLicencia.Tables["Datos"].Rows[0]["No_Region"].ToString();
                if (Tipo == 1)
                {
                    row["NoLicencia"] = "";
                    row["Fecha"] = DateTime.Now;
                    row["Periodo"] = Periodo;
                    row["Del"] = FecIni;
                    row["Al"] = FecFin;
                    row["DirectorRegional"] = dsDatosLicencia.Tables["Datos1"].Rows[0]["Regional"].ToString();
                    row["PuestoRegional"] = dsDatosLicencia.Tables["Datos1"].Rows[0]["Puesto"].ToString();
                }
                else
                {
                    row["NoLicencia"] = dsDatosLicencia.Tables["Datos"].Rows[0]["NoLicencia"].ToString();
                    row["Fecha"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FechaLicencia"].ToString();
                    row["Periodo"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Periodo"].ToString();
                    row["Del"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FecIni"].ToString();
                    row["Al"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FecFin"].ToString();
                    row["DirectorRegional"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Regional"].ToString();
                    row["PuestoRegional"] = dsDatosLicencia.Tables["Datos"].Rows[0]["PuestoRegional"].ToString();
                }
                
                row["DepartamentoRegion"] = dsDatosLicencia.Tables["Datos"].Rows[0]["departamento"].ToString();
                
                row["DictamenTecnico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["DictamenTecnico"].ToString();
                row["Tecnico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Tecnico"].ToString();
                row["FechaDicTecnico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FechaDictTec"].ToString();
                row["DictamenJuridico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["DictamenJuridico"].ToString();
                row["Juridico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Juridico"].ToString();
                row["FechaDictamenJuridico"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FechaDictJur"].ToString();
                row["DictmenSubRegional"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Dictamen_Subregional"].ToString();
                row["DirectorSubRegional"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Subregional"].ToString();
                row["FechaDictamenSubRegional"] = dsDatosLicencia.Tables["Datos"].Rows[0]["FecDictSubregional"].ToString();
                row["Turno"] = Get_MaxTurno_PlanManejo(GestionId);
                
                row["ValorMaderaPie"] = Get_TotalValorMadera(GestionId);

                string SistemaRepoblacionText = "";
                DataSet SistemaRepoblacion = ClManejo.Get_SistemaRepoblacion_Compromiso(GestionId);
                for (int i = 0; i < SistemaRepoblacion.Tables["Datos"].Rows.Count; i++)
                {
                    if (SistemaRepoblacionText == "")
                        SistemaRepoblacionText = SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
                    else
                        SistemaRepoblacionText = SistemaRepoblacionText + ", " + SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
                }
                SistemaRepoblacion.Clear();
                row["SistemaRepoblacion"] = SistemaRepoblacion;
                row["TipoGarantia"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Tipo_Garantia"].ToString();
                row["NoSubRegion"] = dsDatosLicencia.Tables["Datos"].Rows[0]["No_SubRegion"].ToString();
                row["MunicipioSubRegion"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Municipio"].ToString();
                row["DepartamentoSubRegion"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Departamento"].ToString();
                

                string AgraegadoSol = "";
                string Solicitante = "";
                int ModuloId = SP_Get_Modulo_Gestion(GestionId);
                Solicitante = Get_Propietarios_Manejo_DPI(GestionId);
                if (Tipo == 1)
                    AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosLicencia.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                else
                {
                    AgraegadoSol = Get_CompletaPropietarios(Convert.ToInt32(dsDatosLicencia.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                }

                if (AgraegadoSol != "")
                    Solicitante = Solicitante + " " + AgraegadoSol + ".";
                else
                    Solicitante = Solicitante + ".";
                row["PropietariosDPI"] = Solicitante;
                string Fincas = GetDatosFinca_Gestion_Juntos_RegistroPropiedad(GestionId);
                row["FincasRegistro"] = Fincas;
                row["DepMunFincas"] = GetDatosFinca_Gestion_Juntos_SoloDepMun(GestionId);


                string AgraegadoSol2 = "";
                string Solicitante2 = "";
                Solicitante2 = Get_Propietarios_Manejo(GestionId);
                if (Tipo == 1)
                    AgraegadoSol2 = Get_CompletaPropietarios(Convert.ToInt32(dsDatosLicencia.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                else
                {
                    AgraegadoSol2 = Get_CompletaPropietarios(Convert.ToInt32(dsDatosLicencia.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                }

                if (AgraegadoSol2 != "")
                    Solicitante2 = Solicitante2 + " " + AgraegadoSol + ".";
                else
                    Solicitante2 = Solicitante2 + ".";
                row["Propietarios"] = Solicitante2;
                row["Elaborador"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Elaborador"];
                row["CorrelativoElab"] = dsDatosLicencia.Tables["Datos"].Rows[0]["Correlativo"];
                row["AreaCompromiso"] = ClManejo.Get_Compromiso_Area(GestionId).ToString();
                DataSet DsGarantia = ClCatalogos.Sp_Get_Monto_Garantia(Convert.ToInt32(dsDatosLicencia.Tables["Datos"].Rows[0]["Tipo_GarantiaId"]));
                row["MontoHa"] = Convert.ToDouble(DsGarantia.Tables["Datos"].Rows[0]["Valor_Hectaria"]);
                row["MontoTotalCompro"] = (Convert.ToInt32(row["AreaCompromiso"]) * Convert.ToDouble(DsGarantia.Tables["Datos"].Rows[0]["Valor_Hectaria"])).ToString();
                DsGarantia.Clear();
                Ds_Licencia.Tables["Dt_Licencia"].Rows.Add(row);
            }



            dsDatosLicencia.Clear();


            //MaderaPie
            DataSet dsMaderaPie = Get_MaderaPieLicencia(GestionId);
            Ds_Licencia.Tables["Dt_MaderaPie"].Clear();
            for (int i = 0; i < dsMaderaPie.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowData = Ds_Licencia.Tables["Dt_MaderaPie"].NewRow();
                rowData["Correlativo"] = 0;
                rowData["Turno"] = dsMaderaPie.Tables["Datos"].Rows[i]["Turno"];
                rowData["Rodal"] = dsMaderaPie.Tables["Datos"].Rows[i]["Rodal"];
                rowData["Especie"] = dsMaderaPie.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                rowData["EspecieId"] = 0;
                rowData["VolTroza"] = dsMaderaPie.Tables["Datos"].Rows[i]["VolTroza"];
                rowData["VolLena"] = dsMaderaPie.Tables["Datos"].Rows[i]["VolLena"];
                rowData["VolTotal"] = dsMaderaPie.Tables["Datos"].Rows[i]["VolTotal"];
                rowData["VolMaderaTroza"] = dsMaderaPie.Tables["Datos"].Rows[i]["ValTroza"];
                rowData["ValorLenaTroza"] = dsMaderaPie.Tables["Datos"].Rows[i]["ValLena"];
                rowData["ValorTotal"] = dsMaderaPie.Tables["Datos"].Rows[i]["ValPagar"];
                rowData["ValorPagar"] = dsMaderaPie.Tables["Datos"].Rows[i]["PorPagar"];
                Ds_Licencia.Tables["Dt_MaderaPie"].Rows.Add(rowData);
            }
            dsMaderaPie.Clear();
            return Ds_Licencia;
        }

        public DataSet Get_Datos_Licencia_Forestal(int GestionId, int Tipo, int UsuarioId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_Datos_Licencia_Forestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
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

        public int Get_MaxTurno_PlanManejo(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_MaxTurno_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
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

        public double Get_TotalValorMadera(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_TotalValorMadera", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@Resul"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public DataSet Get_MaderaPieLicencia(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_MaderaPieLicencia", cn);
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
                return ds;
            }
        }

        public DataSet Get_Datos_DictamenTecnico_Gestion(int GestionId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Datos_DictamenTecnico_Gestion", cn);
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
                return ds;
            }
        }

        public DataSet Get_Etapas_Dictamen_Tecnico(int GestionId)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Etapas_Dictamen_Tecnico", cn);
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
                return ds;
            }
        }

        public void Insert_LicenciaForestal(int LicenciaId, int Aprueba, int Periodo, DateTime FecIni, DateTime FecFin, int UsuarioId, int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_LicenciaForestal", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LicenciaId", SqlDbType.Int).Value = LicenciaId;

                cmd.Parameters.Add("@Aprueba", SqlDbType.Int).Value = Aprueba;
                cmd.Parameters.Add("@Periodo", SqlDbType.Int).Value = Periodo;
                cmd.Parameters.Add("@FecIni", SqlDbType.DateTime).Value = FecIni;
                cmd.Parameters.Add("@FecFin", SqlDbType.DateTime).Value = FecFin;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public void Insert_OfcioEnmiendaRegional(int OficioEnmindaRegionalId, int GestionId, XmlDocument Enmiendas, int UsuarioId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("Sp_Insert_OfcioEnmiendaRegional", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OficioEnmindaRegionalId", SqlDbType.Int).Value = OficioEnmindaRegionalId;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@Enmienda", SqlDbType.Xml).Value = Enmiendas.OuterXml.ToString();
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = UsuarioId;
                cmd.ExecuteNonQuery();
                cnSql.Close();
            }
            catch (Exception ex)
            {
                cnSql.Close();
            }
        }

        public DataSet Get_InteresadosEnviaCorreo(int GestionId) //1 VP,  2. gestion
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_InteresadosEnviaCorreo", cn);
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
                return ds;
            }
        }

        public DataSet Get_Historial_ManejoForestal(int UsuarioId, string No_Expediente)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_Historial_ManejoForestal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", OleDbType.Integer).Value = UsuarioId;
                cmd.Parameters.Add("@No_Expediente", OleDbType.VarChar, 200).Value = No_Expediente;
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

        public int Tiene_DictamenTecnico_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Tiene_DictamenTecnico_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Tiene_DictamenSubRegional_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Tiene_DictamenSubRegional_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int Tiene_EnmienasTecnico_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Tiene_EnmienasTecnico_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string Get_Oficio_SubRegional(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Get_Oficio_SubRegional", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Oficio", OleDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Oficio"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Tiene_EnminedasSubRegional_Gestion(int GestionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_Tiene_EnminedasSubRegional_Gestion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", OleDbType.Integer).Value = GestionId;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public int RetornaPlanManejoEnmienda(int GestionId)
        {
            try
            {
                cnSql.Open();
                SqlCommand cmd = new SqlCommand("SP_RetornaPlanManejoEnmienda", cnSql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GestionId", SqlDbType.Int).Value = GestionId;
                cmd.Parameters.Add("@AsignacionId", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnSql.Close();
                return Convert.ToInt32(cmd.Parameters["@AsignacionId"].Value.ToString());
            }
            catch (Exception ex)
            {
                cnSql.Close();
                return 0;
            }
        }

    }
}