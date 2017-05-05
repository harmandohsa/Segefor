using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using System.Xml;
using SEGEFOR.Data_Set;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Seguimiento_SubRegional : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Xml ClXml;
        Cl_Manejo ClManejo;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClXml = new Cl_Xml();
            ClManejo = new Cl_Manejo();

            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            BtnEnviar.Click += BtnEnviar_Click;
            BtnYes.Click += BtnYes_Click;
            ImgVerinfo.Click += ImgVerinfo_Click;
            BtnVistaPreviaResolucion.Click += BtnVistaPreviaResolucion_Click;
            BtnEnviarRes.Click += BtnEnviarRes_Click;
            IngVerAnexos.Click += IngVerAnexos_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(31, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 31);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnEnviar.Visible = false;
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
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                    LblIdentificacion.Text = ClManejo.Get_Identificacion_Gestion_Manejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                if (CboJuridico.Items.Count > 1)
                    ClUtilitarios.AgregarSeleccioneCombo(CboJuridico,"Jurídico");
                if (ClGestion.Existe_Resolucion_Admision(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) == 1)
                { 
                    DivProvidencia.Visible = true;
                    ClUtilitarios.LlenaCombo(ClGestion.Get_Juridicos_SubRegion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["sousregion"].ToString()), true))), CboJuridico, "UsuarioId", "Nombres");
                }
                else
                    DivResolucion.Visible = true;
            }
        }

        void IngVerAnexos_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                if (ClGestion.Tiene_Anexos_Inventerio(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) == 1)
                {
                    //Llamada 0 = PV, AF 1 = SAF
                    int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int Categoria = ClGestion.Get_CategoriaRNFId(Actividad);
                    int Tipo = 0;
                    if ((Categoria == 2) || (Categoria == 3))
                        Tipo = 0;
                    else if (Categoria == 4)
                        Tipo = 1;
                    else if (Categoria == 6)
                        Tipo = 2;

                    Session["Datos_InventarioForestal"] = ClGestion.Impresion_Inventario_Forestal(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Tipo);
                    RadWindow1.Title = "Inventario Forestal";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepInventarioForestal.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Tipo.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                if (ClGestion.Tiene_Anexos_Poligono(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) == 1)
                {
                    int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    string url = "";
                    //url = "/Mapas/MenuMapas.aspx?Id=" + Id;
                    url = "/Segefor_new/Mapas/MenuMapas.aspx?Id=" + Id;
                    string popupScript = "window.open('" + url + "', 'popup_window', 'left=100,top=100,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", popupScript, true);
                }
            }
            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                string GestionNo = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                String js = "window.open('Wfrm_AnexosPlanManejo.aspx?idgestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Id.ToString(), true)) + "&NUG=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionNo.ToString(), true)) + "', '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
            }
        }

        void BtnEnviarRes_Click(object sender, EventArgs e)
        {
            LblTitConfirmacion.Text = "La Gestíon generara la resolución de admisión de expediente, ¿esta seguro (a)?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void BtnVistaPreviaResolucion_Click(object sender, EventArgs e)
        {
            int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
            int CategoriaId = ClGestion.Get_CategoriaRNFId(SubCategoriaId);
            Session["Datos_Resolucion_Admision"] = ClGestion.ImpresionResolucion_Admision(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]), CategoriaId);
            RadWindow1.Title = "Vista Previa Resolución";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAdmision.aspx";
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
                else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 1);
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (SubCategoriaId == 18)
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2);
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_SistemaAgroforestal"] = ClGestion.ImpresionFormularioSistemaAgroforestal(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioSistemasAgroforestales.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_FuenteSemillera"] = ClGestion.ImpresionFormularioFuenteSemillera(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioFuenteSemillera.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24))
                {
                    Session["Ds_Empresas"] = ClGestion.ImpresionFormularioEmpresas(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEmpresas.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28))
                {
                    Session["Dt_Entidad"] = ClGestion.ImpresionFormularioEntidad(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Vista Previa Insripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEntidad.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 11) || (SubCategoriaId == 15))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_MotoSierra"] = ClGestion.ImpresionFormularioMotoSierra(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Vista Previa Insripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioMotoSierra.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (SubCategoriaId == 14)
                {
                    Session["Ds_Formulario_BosqueNatural"] = ClGestion.ImpresionFormularioBosqueNatural(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    Session["TipoReporte"] = 2;
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioBosqueNatural.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }

            }
            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                RadWindow1.Title = "Plan de Manejo Forestal";
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2);
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (DivResolucion.Visible == true)
            {
                int ModuloId = ClGestion.SP_Get_Modulo_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                if (ModuloId == 3)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    int CategoriaId = ClGestion.Get_CategoriaRNFId(SubCategoriaId);
                    string Solicitante = ClGestion.Get_Propietarios_Gestion_Registro(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), CategoriaId);
                    string AgraegadoSol = ClGestion.Get_CompletaPropietarios(CategoriaId, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));


                    DataSet dsDatos = ClGestion.Datos_Impresion_Resolucion_Admision(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), CategoriaId);
                    string Asunto = "";
                    string Cuerpo_Resolucion = "";
                    string ConsiderandoUno = "";
                    string ConsiderandoDos = "";


                    if ((CategoriaId == 2) || (CategoriaId == 3) || (CategoriaId == 4) || (CategoriaId == 6) || (CategoriaId == 1))
                    {
                        string Fincas = ClGestion.GetDatosFinca_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string Representantes = ClGestion.GetNombresRepresentantes_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        int TipoPersona = ClGestion.Get_Tipo_Persona_Fincas(2, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string Propietarios = ClGestion.GetNombresPropietarios_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TipoPersona);
                        Asunto = Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Asunto = Asunto + " representado por: " + Representantes;
                        Asunto = Asunto + " Solicita (n): aprobación de registro de plantación voluntaria en ";
                        Asunto = Asunto + Fincas;

                        Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Cuerpo_Resolucion = Cuerpo_Resolucion + " representado por: " + Representantes;
                        Cuerpo_Resolucion = Cuerpo_Resolucion + ".";

                        ConsiderandoUno = "Que " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoUno = ConsiderandoUno + " representado por: " + Representantes;
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " en " + Fincas + ", quien ha cumplido con los requisitos de ley establecidos.";
                        ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoDos = ConsiderandoDos = " representado por: " + Representantes;
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                    }
                    else if ((CategoriaId == 5) || (CategoriaId == 8))
                    {
                        int Tipo_PersonaId = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_PersonaId"]);
                        string Propietarios = ClGestion.GetNombresPropietarios_Gestion_Juntos_SinFinca(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Tipo_PersonaId, CategoriaId);
                        string Representantes = ClGestion.GetNombresRepresentantes_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        Asunto = Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Asunto = Asunto + " Representado legalmente por: " + Representantes;
                        Asunto = Asunto + " Solicita: aprobación de inscripción de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"] + " en la subcategoría de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " ubicado en " + dsDatos.Tables["Datos"].Rows[0]["Direccion"] + " en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ".";
                        Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            Cuerpo_Resolucion = Cuerpo_Resolucion + " representado legalmente por " + Representantes;
                        Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                        ConsiderandoUno = "Que " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " ubicado en " + dsDatos.Tables["Datos"].Rows[0]["Direccion"] + " en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ", quien ha cumplido con los requisitos de ley establecidos.";
                        ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                        if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                            ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + Representantes;
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                    }
                    else if (CategoriaId == 7)
                    {
                        Asunto = dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        Asunto = Asunto + " solicita su INSCRIPCIÓN en el Registro nacional Forestal como : " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + ".";
                        Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString() + ".";
                        ConsiderandoUno = "Que " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para su inscripción en el Registro Nacional Forestal como:  " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + ", quien ha cumplido con los requisitos de ley establecidos.";
                        ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + dsDatos.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                    }
                    else if (CategoriaId == 9)
                    {
                        if (dsDatos.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "15")
                        {
                            string Representantes = ClGestion.GetNombresRepresentantes_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                            int TipoPersona = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_PersonaId"]);
                            string Propietarios = ClGestion.GetNombresPropietarios_Gestion_Juntos_SinFinca(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TipoPersona, CategoriaId);
                            Asunto = Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Asunto = Asunto + " Representado legalmente por: " + Representantes;
                            Asunto = Asunto + " Solicita: aprobación de inscripción de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_Categoria"] + " en la subcategoría de: " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " de la empresa " + dsDatos.Tables["Datos"].Rows[0]["Nombre"] + ", ubicada en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ".";
                            Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Cuerpo_Resolucion = Cuerpo_Resolucion + " representado legalmente por " + Representantes;
                            Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                            ConsiderandoUno = "Que " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                            ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " de la empresa denominada " + dsDatos.Tables["Datos"].Rows[0]["Nombre"] + " ubicada en el municipio de " + dsDatos.Tables["Datos"].Rows[0]["Municipio"] + ", departamento de " + dsDatos.Tables["Datos"].Rows[0]["Departamento"] + ". Ha cumplido con los requisitos de ley establecidos.";
                            ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + Representantes;
                            ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        }
                        else if (dsDatos.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "11")
                        {
                            string Representantes = ClGestion.GetNombresRepresentantes_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                            int TipoPersona = Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_PersonaId"]);
                            string Propietarios = ClGestion.GetNombresPropietarios_Gestion_Juntos_SinFinca(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TipoPersona, CategoriaId);
                            Asunto = Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Asunto = Asunto + " Representado legalmente por: " + Representantes;
                            Asunto = Asunto + " Solicita: aprobación de inscripción de una MOTOSIERRA, marca: " + dsDatos.Tables["Datos"].Rows[0]["Marca"] + ", modelo: " + dsDatos.Tables["Datos"].Rows[0]["Modelo"] + ", serie No." + dsDatos.Tables["Datos"].Rows[0]["Serie"] + ".";
                            Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                Cuerpo_Resolucion = Cuerpo_Resolucion + " representado legalmente por " + Representantes;
                            Cuerpo_Resolucion = Cuerpo_Resolucion + ".";
                            ConsiderandoUno = "Que " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoUno = ConsiderandoUno + " representado legalmente por " + Representantes;
                            ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de una MOTOSIERRA, marca " + dsDatos.Tables["Datos"].Rows[0]["Marca"] + ", modelo " + dsDatos.Tables["Datos"].Rows[0]["Modelo"] + ", serie " + dsDatos.Tables["Datos"].Rows[0]["Serie"] + " quien ha cumplido con los requisitos de ley establecidos.";
                            ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                                ConsiderandoDos = ConsiderandoDos + " representado legalmente por " + Representantes;
                            ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                        }

                    }
                    dsDatos.Clear();
                    int Resolucion_AdmisionId = ClGestion.Max_ResolucionAdmision();
                    ClGestion.Insert_Resolucion_Aceptacion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Asunto, Cuerpo_Resolucion, ConsiderandoUno, ConsiderandoDos, Convert.ToInt32(Session["UsuarioId"]), SubRegionId);
                    Session["Datos_Resolucion_Admision"] = ClGestion.ImpresionResolucion_Admision(2, Resolucion_AdmisionId, Convert.ToInt32(Session["UsuarioId"]), CategoriaId);
                    Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("7", true)) + "");
                }
                else if (ModuloId == 2)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    int CategoriaId = ClGestion.Get_CategoriaRNFId(SubCategoriaId);
                    string Solicitante = ClGestion.Get_Propietarios_Gestion_Registro(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), CategoriaId);
                    string AgraegadoSol = ClGestion.Get_CompletaPropietarios(CategoriaId, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));


                    DataSet dsDatos = ClGestion.Datos_Impresion_Resolucion_Admision(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), CategoriaId);
                    string Asunto = "";
                    string Cuerpo_Resolucion = "";
                    string ConsiderandoUno = "";
                    string ConsiderandoDos = "";


                    string Fincas = ClGestion.GetDatosFinca_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string Representantes = ClGestion.GetNombresRepresentantes_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int TipoPersona = ClGestion.Get_Tipo_Persona_Fincas(2, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string Propietarios = ClGestion.GetNombresPropietarios_Gestion_Juntos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TipoPersona);
                    Asunto = Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        Asunto = Asunto + " representado por: " + Representantes;
                    Asunto = Asunto + " Solicita (n): aprobación de registro de plantación voluntaria en ";
                    Asunto = Asunto + Fincas;

                    Cuerpo_Resolucion = "Se tiene a la vista para resolver el expediente arriba identificado, promovido a instancia de " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        Cuerpo_Resolucion = Cuerpo_Resolucion + " representado por: " + Representantes;
                    Cuerpo_Resolucion = Cuerpo_Resolucion + ".";

                    ConsiderandoUno = "Que " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        ConsiderandoUno = ConsiderandoUno + " representado por: " + Representantes;
                    ConsiderandoUno = ConsiderandoUno + " con fecha " + dsDatos.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + " gestionó ante el Instituto Nacional de Bosques, solicitud para el Registro de " + dsDatos.Tables["Datos"].Rows[0]["Nombre_SubCategoria"] + " en " + Fincas + ", quien ha cumplido con los requisitos de ley establecidos.";
                    ConsiderandoDos = "Con base en lo considerado y lo preceptuado en los Artículos 88: de la Ley Forestal; 3, 5, 6 y 14 de la Resolución No. JD.03.26.2015, Reglamento del Registro Nacional Forestal. Esta Dirección Subregional Resuelve: a) Admitir para su trámite la solicitud presentada por " + Solicitante;
                    if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Representantes"]) > 0)
                        ConsiderandoDos = ConsiderandoDos = " representado por: " + Representantes;
                    ConsiderandoDos = ConsiderandoDos + ", contenida en el expediente No. " + dsDatos.Tables["Datos"].Rows[0]["No_Expediente"] + " el cual consta de _________ folios inclusive, b) Notifíquese al interesado.";
                }
                
                //RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAdmision.aspx";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else
            {
                string SubRegion = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));

                string Asunto;
                Asunto = LblSolicitante.Text;
                int Orden = 1;
                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Providencia");
                XmlNode iElementos = iInformacion.CreateElement("Cuerpo");
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    Asunto = Asunto + ", solicita Inscripción en el Registro Nacional Forestal (RNF) en la categoría de";
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                        Asunto = Asunto + " “" + ClGestion.Get_Nombre_CategoriaRNF(SubCategoriaId) + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".";
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Punto", "La presente providencia forme parte del expediente físico.", iElementoDetalle);
                    ClXml.AgregarAtributo("Orden", Orden, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    Orden = Orden + 1;
                    XmlNode iElementoDetalle1 = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Punto", "Traslade el expediente original al Asesor Jurídico Licenciado(a): " + CboJuridico.Text + "para que se sirva emitir Dictamen a la mayor brevedad.", iElementoDetalle1);
                    ClXml.AgregarAtributo("Orden", Orden, iElementoDetalle1);
                    iElementos.AppendChild(iElementoDetalle1);
                    Orden = Orden + 1;
                    XmlNode iElementoDetalle2 = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Punto", "Diligenciado vuelva a esta Dirección Subregional.", iElementoDetalle2);
                    ClXml.AgregarAtributo("Orden", Orden, iElementoDetalle2);
                    iElementos.AppendChild(iElementoDetalle2);
                    Orden = Orden + 1;
                    XmlNode iElementoDetalle3 = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Punto", "El expediente consta de ___ folios inclusive.", iElementoDetalle3);
                    ClXml.AgregarAtributo("Orden", Orden, iElementoDetalle3);
                    iElementos.AppendChild(iElementoDetalle3);
                    Orden = Orden + 1;
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                }
                int ProvidenciaId = ClGestion.Max_Providencia();
                ClGestion.Insert_Providencia_Exp(SubRegion, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Asunto, iInformacion, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(CboJuridico.SelectedValue), SubRegionId);
                ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 14);
                DataSet dsDatosUsuario = ClGestion.Get_Datos_Persona(1, Convert.ToInt32(CboJuridico.SelectedValue));
                string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                ClUtilitarios.EnvioCorreo(dsDatosUsuario.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosUsuario.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                dsDatosUsuario.Clear();
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("3", true)) + "&providence=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProvidenciaId.ToString(), true)) + "");
            }
            
        }

        void BtnEnviar_Click(object sender, EventArgs e)
        {
            LblTitConfirmacion.Text = "La Gestíon sera providenciada al Licenciado(a): " + CboJuridico.Text + ", ¿esta seguro (a)?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        bool Valida()
        {
            bool HayError = false;
            DivError.Visible = false;
            if (CboJuridico.Text == "")
            {
                LblMensaje.Text = "Debe seleccionar el asedor jurídico";
                HayError = true;
            }

            if (HayError == true)
            {
                DivError.Visible = true;
                return false;
            }

            else
                return true;

        }

        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {

            if (Valida() == true)
            {
                string Asunto = "";
                string Cuerpo = "";
                Asunto = LblSolicitante.Text;
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    Asunto = Asunto + ", solicita Inscripción en el Registro Nacional Forestal (RNF) en la categoría de";
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                        Asunto = Asunto + " “" + ClGestion.Get_Nombre_CategoriaRNF(SubCategoriaId) + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".";
                    Cuerpo = "La presente providencia forme parte del expediente físico.|";
                    Cuerpo = Cuerpo + "Traslade el expediente original al Asesor Jurídico Licenciado(a): " + CboJuridico.Text + "para que se sirva emitir Dictamen a la mayor brevedad.|";
                    Cuerpo = Cuerpo + "Diligenciado vuelva a esta Dirección Subregional.|";
                    Cuerpo = Cuerpo + "El expediente consta de ___ folios inclusive.";
                }
                RadWindow1.Title = "Vista Previa Providencia de Traslado de Expediente";
                DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Asunto, Cuerpo, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(CboJuridico.SelectedValue));
                Session["DatosProvidencia"] = DatosProvidencia;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }
    }
}