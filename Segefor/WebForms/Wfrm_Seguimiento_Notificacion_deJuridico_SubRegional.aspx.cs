using SEGEFOR.Clases;
using SEGEFOR.Data_Set;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Seguimiento_Notificacion_deJuridico_SubRegional : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ImgVerProvidencia.Click += ImgVerProvidencia_Click;
            ImgVerinfo.Click += ImgVerinfo_Click;
            ImgVerDictamenJuridico.Click += ImgVerDictamenJuridico_Click;
            BtnVistaPreviaOficio.Click += BtnVistaPreviaOficio_Click;
            BtnEnviarOficio.Click += BtnEnviarOficio_Click;
            BtnYes.Click += BtnYes_Click;
            OptApruebaInscripción.SelectedIndexChanged += OptApruebaInscripción_SelectedIndexChanged;
            BtnVPResolucion.Click += BtnVPResolucion_Click;
            BtnGrabaResolucion.Click += BtnGrabaResolucion_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(34, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 34);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {

                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Eliminar"]) == 0)
                {

                }
                dsPermisos.Clear();


                System.Web.UI.HtmlControls.HtmlImage ImgPerfilFake;
                System.Web.UI.WebControls.Image ImgPerfil;
                ImgPerfilFake = (System.Web.UI.HtmlControls.HtmlImage)Master.FindControl("ImgPerfilFake");
                ImgPerfil = (System.Web.UI.WebControls.Image)Master.FindControl("ImgPerfil");

                if (ClPersona.Existe_FotoPerfil(Convert.ToInt32(Session["PersonaId"])) == true)
                {
                    ImgPerfilFake.Visible = false;
                    ImgPerfil.Visible = true;
                }
                else
                {
                    ImgPerfilFake.Visible = true;
                    ImgPerfil.Visible = false;
                    if (ClPersona.Genero_Usuario(Convert.ToInt32(Session["PersonaId"])) == 1)
                        ImgPerfilFake.Src = "../imagenes/male.png";

                    else
                        ImgPerfilFake.Src = "../imagenes/female.png";
                }
                LblNug.Text = "Gestión No.: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                LblSolicitante.Text = "Solicitante: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int TieneEnmiendas = ClGestion.Tiene_Enmiendas_Dictamen_Juridico(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                if (TieneEnmiendas == 0)
                {
                    LblEstado.Text = "Dictamen Jurídico sin enmiendas";
                    DivSinEnmiendas.Visible = true;
                }
                else
                {
                    LblEstado.Text = "Dictamen Jurídico con enmiendas";
                    DivConEnmiendas.Visible = true;
                }
                
            }
        }

        void BtnGrabaResolucion_Click(object sender, EventArgs e)
        {
            
            if (Valida() == true)
                
                if (RadUploadExp.UploadedFiles.Count > 0)
                {
                    Stream fileStream = RadUploadExp.UploadedFiles[0].InputStream;
                    byte[] attachmentBytes = new byte[fileStream.Length];
                    fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                    fileStream.Close();
                    Session["bytes"] = attachmentBytes;
                    Session["ContentType"] = RadUploadExp.UploadedFiles[0].ContentType;
                    Session["FileName"] = RadUploadExp.UploadedFiles[0].FileName;
                }
                Session["Enmiendas"] = "0";
                LblTitConfirmacion.Text = "El sistema generara la resolución de aprobación, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);       
        }

        void BtnVPResolucion_Click(object sender, EventArgs e)
        {
            if (OptApruebaInscripción.SelectedValue == "1")
                RadWindow1.Title = "Vista Previa Resolución de Aprobación de Inscripción";
            else
                RadWindow1.Title = "Vista Previa Resolución de denegatoria de Inscripción";
            DataSet DatosResolucion = ClGestion.ImpresionResolucion_Aprobacion(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(OptApruebaInscripción.SelectedValue));
            Session["DatosResolucion"] = DatosResolucion;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAprobacion.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void OptApruebaInscripción_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptApruebaInscripción.SelectedValue == "1")
                DivCarga.Visible = true;
            else
                DivCarga.Visible = false;
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["Enmiendas"]) == 1)
            {
                Session["Enmiendas"] = 0;
                string SubRegion = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int Oficio_Enmiendas_JuridicoId = ClGestion.Max_Oficio_Dictamen_Juridico();
                ClGestion.Insert_Oficio_Enmiendas_Juridico(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), SubRegionId, Convert.ToInt32(Session["UsuarioId"]), SubRegion);
                DataSet ds = ClGestion.Get_Datos_Persona_Desde_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                string Correo = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                string Solicitante = ds.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                ds.Clear();
                DataSet dsEnmiendas = ClGestion.Get_EnmiendasXml_Dictamen_juridico(Oficio_Enmiendas_JuridicoId);
                string Mensaje  = "<table>";
                Mensaje = Mensaje + "<tr><td colspan=2>Enmiendas:</td></tr>";
                for (int i = 0; i < dsEnmiendas.Tables["Datos"].Rows.Count; i++ )
                {
                    Mensaje = Mensaje + "<tr><td>-  </td><td>" + dsEnmiendas.Tables["Datos"].Rows[i]["Enmienda"] + "</td></tr>";
                }
                Mensaje = Mensaje + "</table>";
                ClUtilitarios.EnvioCorreo(Correo, Solicitante, "Solicitud Con Enmiendas",Mensaje,0,"","");
                ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 3);
                DataSet dsDatosRegional = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                string MensajeCorreo = "La gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true) + " tiene enmiendas juridicas";
                ClUtilitarios.EnvioCorreo(dsDatosRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                dsDatosRegional.Clear();
                Response.Redirect("~/WebForms/Wfrm_Notificacion_deJuridico.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&oficioenmienda=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Oficio_Enmiendas_JuridicoId.ToString(), true)) + "");
            }
            else
            {
                Session["Enmiendas"] = 0;
                string SubRegion = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int ResolucionId = ClGestion.Max_Resolucion();
                ClGestion.Insert_Resolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]),Convert.ToInt32(OptApruebaInscripción.SelectedValue), SubRegionId, SubRegion);
                ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 10);
                if (OptApruebaInscripción.SelectedValue == "1")
                {
                    byte[] atachh = (byte[])Session["bytes"];
                    ClGestion.Inserta_Expediente_Resolucion(ResolucionId, atachh, Session["ContentType"].ToString(), Session["FileName"].ToString());
                }
                else
                {
                    DataSet ds = ClGestion.Get_Datos_Persona_Desde_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string Correo = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                    string Solicitante = ds.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                    ds.Clear();
                     string Mensaje = "";
                     if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                        Mensaje = "Su solicitud de inscripción al Registro Nacional Forestal ha sido denegada";
                    ClUtilitarios.EnvioCorreo(Correo, Solicitante, "Denegatoria  de Inscripción", Mensaje, 0, "", "");
                    ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 4);
                }
                DataSet dsDatosRegional = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                ClUtilitarios.EnvioCorreo(dsDatosRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                dsDatosRegional.Clear();
                Response.Redirect("~/WebForms/Wfrm_Notificacion_deJuridico.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&resolution=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ResolucionId.ToString(), true)) + "");
            }
            
        }

        void BtnEnviarOficio_Click(object sender, EventArgs e)
        {
            Session["Enmiendas"] = 1;
            LblTitConfirmacion.Text = "Se enviara notificación al usuario de las enmiendas, ¿esta seguro (a)?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            

        }

        void BtnVistaPreviaOficio_Click(object sender, EventArgs e)
        {
            RadWindow1.Title = "Vista Previa Oficio de enmiendas jurídicas";
            DataSet DatosOficioEnmiendas = ClGestion.ImpresionOficioEnmiendasJuridico(1,Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),Convert.ToInt32(Session["UsuarioId"]));
            Session["DatosOficioEnmiendasJuridico"] = DatosOficioEnmiendas;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendaJuridica.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgVerDictamenJuridico_Click(object sender, ImageClickEventArgs e)
        {
 	        RadWindow1.Title = "Dictamen Juridico";
            string Dictamen_Juridico_Id = ClGestion.Get_Ditamen_JuridicoId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet dsTemp= new DataSet();
            DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "",0, "", "", "", dsTemp, "");
            Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgVerinfo_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                {
                    DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    Ds_Profesionales Ds_Inscripcion = new Ds_Profesionales();
                    Ds_Inscripcion.Tables["Dt_Formulario"].Clear();
                    DataRow row = Ds_Inscripcion.Tables["Dt_Formulario"].NewRow();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "1")
                        row["Requisitos"] = "Requisitos:\n1) Copia legalizada del título;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU); y  \n3) Copia de documento personal de identificación (DPI).";
                    else if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "2")
                        row["Requisitos"] = "Requisitos:\n1) Constancia original de colegiado activo vigente;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU);   y \n3) Copia de documento personal de identificación (DPI).\nPara profesionales con post grado en materia forestal, presentar el documento extendido por la universidad que lo avala.";
                    if (ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "1" || ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "16")
                        row["Requisitos"] = row["Requisitos"] + "\nademás de los requisitos anteriores, copia del diploma de aprobación del curso correspondiente.";
                    row["Region"] = ds.Tables["Datos"].Rows[0]["region"].ToString();
                    row["SubRegion"] = ds.Tables["Datos"].Rows[0]["Subregion"].ToString();
                    row["NUG"] = ds.Tables["Datos"].Rows[0]["NUG"].ToString();
                    row["Fecha"] = ds.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString();
                    row["Actividad"] = ds.Tables["Datos"].Rows[0]["Nombre_Subcategoria"].ToString();
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 16))
                        row["ActividadId"] = 1;
                    else
                        row["ActividadId"] = 0;
                    row["Nombres"] = ds.Tables["Datos"].Rows[0]["Nombres"].ToString();
                    row["Apellidos"] = ds.Tables["Datos"].Rows[0]["Apellidos"].ToString();
                    row["DPI"] = ds.Tables["Datos"].Rows[0]["DPI"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "")
                        row["NIT"] = "";
                    else
                        row["NIT"] = ds.Tables["Datos"].Rows[0]["NIT"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["telefono"].ToString() == "")
                        row["Telefono"] = "";
                    else
                        row["Telefono"] = ds.Tables["Datos"].Rows[0]["telefono"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["celular"].ToString() == "")
                        row["Celular"] = "";
                    else
                        row["Celular"] = ds.Tables["Datos"].Rows[0]["celular"].ToString();
                    row["Correo"] = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                    row["Direccion"] = ds.Tables["Datos"].Rows[0]["Direccion"].ToString();
                    row["Municipio"] = ds.Tables["Datos"].Rows[0]["MunVivienda"].ToString();
                    row["Departamento"] = ds.Tables["Datos"].Rows[0]["DepaVivienda"].ToString();
                    row["CategoriaProfesion"] = ds.Tables["Datos"].Rows[0]["CategoriaProfesion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "1")
                        row["No_Colegiado"] = "---------------";
                    else
                        row["No_Colegiado"] = ds.Tables["Datos"].Rows[0]["No_Colegiado"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "1" || ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "16")
                        row["No_Diploma"] = ds.Tables["Datos"].Rows[0]["No_Diploma"].ToString();
                    else
                        row["No_Diploma"] = "-----------------";
                    row["Direccion_Notifica"] = ds.Tables["Datos"].Rows[0]["Direccion_Notificacion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["Aldea_Notificacion"].ToString() == "")
                        row["Aldea_Notifica"] = "";
                    else
                        row["Aldea_Notifica"] = ds.Tables["Datos"].Rows[0]["Aldea_Notificacion"].ToString();
                    row["Municipio_Notifica"] = ds.Tables["Datos"].Rows[0]["MunNotifica"].ToString();
                    row["Departamento_Notifica"] = ds.Tables["Datos"].Rows[0]["DepNotifica"].ToString();
                    row["Observaciones"] = ds.Tables["Datos"].Rows[0]["Observaciones"].ToString();
                    row["Nombre"] = ds.Tables["Datos"].Rows[0]["Nombre_Firma"].ToString();
                    row["Profesion"] = ds.Tables["Datos"].Rows[0]["Profesion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["Origen_PersonaId"].ToString() == "1")
                        row["LabelId"] = "2.3 Número de DPI:";
                    else
                        row["LabelId"] = "2.3 Número de Pasaporte:";
                    Ds_Inscripcion.Tables["Dt_Formulario"].Rows.Add(row);
                    ds.Tables.Clear();
                    Session["DataFormulario"] = Ds_Inscripcion;
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioProfesional.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("00", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
        }

        void ImgVerProvidencia_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Providencia para traslado de Expediente";
            string ProvidenciaId = ClGestion.Get_No_Providencia(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
            Session["DatosProvidencia"] = DatosProvidencia;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        bool Valida()
        {
            string Mensaje = "";
            DivError.Visible = false;
            bool HayError = false;
            if ((OptApruebaInscripción.SelectedValue == "1") && (RadUploadExp.UploadedFiles.Count == 0))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe cargar el scan del expediente en PDF";
                else
                    Mensaje = Mensaje + ", debe cargar el scan del expediente en PDF";
                HayError = true;
            }
            LblMensaje.Text = Mensaje;
            if (HayError == true)
            {
                DivError.Visible = true;
                return false;
            }

            else
                return true;
        }
    }
}