using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using Telerik.Web.UI;
using SEGEFOR.Data_Set;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_SeguimientoUsuario : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Gestion_Registro ClGestionRegistro;
        Cl_Manejo ClManejo;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClGestionRegistro = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();

            GrdSolicitudes.NeedDataSource += GrdSolicitudes_NeedDataSource;
            GrdSolicitudes.ItemDataBound += GrdSolicitudes_ItemDataBound;
            GrdSolicitudes.ItemCommand += GrdSolicitudes_ItemCommand;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(11, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

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
                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 11);
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
            }
        }

        void GrdSolicitudes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "3")
                {
                    int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()));
                    if ((Actividad == 1) || (Actividad == 2) || (Actividad == 3) || (Actividad == 16))
                    {
                        DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
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
                        if ((Actividad == 1) || (Actividad == 16))
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
                    else if ((Actividad == 4) || (Actividad == 5) || (Actividad == 19) || (Actividad == 20) || (Actividad == 21))
                    {
                        Session["TipoReporte"] = "2";
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestionRegistro.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]),1);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(),true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (Actividad == 18)
                    {
                        Session["TipoReporte"] = "2";
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestionRegistro.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 34) || (Actividad == 35) || (Actividad == 36) || (Actividad == 37) || (Actividad == 38) || (Actividad == 39) || (Actividad == 40) || (Actividad == 41)|| (Actividad == 42) || (Actividad == 43))                    
                    {
                        Session["TipoReporte"] = "2";
                        Session["Ds_Formulario_SistemaAgroforestal"] = ClGestionRegistro.ImpresionFormularioSistemaAgroforestal(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioSistemasAgroforestales.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 13) || (Actividad == 29) || (Actividad == 30) || (Actividad == 31) || (Actividad == 32) || (Actividad == 33))
                    {
                        Session["TipoReporte"] = "2";
                        Session["Ds_Formulario_FuenteSemillera"] = ClGestionRegistro.ImpresionFormularioFuenteSemillera(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioFuenteSemillera.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 7) || (Actividad == 9) || (Actividad == 10) || (Actividad == 12) || (Actividad == 17) || (Actividad == 22) || (Actividad == 23) || (Actividad == 24))
                    {
                        Session["Ds_Empresas"] = ClGestionRegistro.ImpresionFormularioEmpresas(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEmpresas.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 25) || (Actividad == 26) || (Actividad == 27) || (Actividad == 28))
                    {
                        Session["Dt_Entidad"] = ClGestionRegistro.ImpresionFormularioEntidad(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Vista Previa Insripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEntidad.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 11) || (Actividad == 15))
                    {
                        Session["Ds_MotoSierra"] = ClGestionRegistro.ImpresionFormularioMotoSierra(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Vista Previa Insripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioMotoSierra.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (Actividad == 14)
                    {
                        Session["Ds_Formulario_BosqueNatural"] = ClGestionRegistro.ImpresionFormularioBosqueNatural(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        Session["TipoReporte"] = 2;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioBosqueNatural.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                }
                else if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "2")
                {
                    RadWindow1.Title = "Plan de Manejo Forestal";
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2,2);
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
            else if (e.CommandName == "CmdAnexos")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "3")
                {
                    if (ClGestion.Tiene_Anexos_Inventerio(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])) == 1)
                    {
                        //Llamada 0 = PV, AF 1 = SAF
                        int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        int Categoria = ClGestion.Get_CategoriaRNFId(Actividad);
                        int Tipo = 0;
                        if ((Categoria == 2) || (Categoria == 3))
                            Tipo = 0;
                        else if (Categoria == 4)
                            Tipo = 1;
                        else if (Categoria == 6)
                            Tipo = 2;
                        if (Categoria != 1)
                        {
                            Session["Datos_InventarioForestal"] = ClGestion.Impresion_Inventario_Forestal(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), Tipo);
                            RadWindow1.Title = "Inventario Forestal";
                            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepInventarioForestal.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Tipo.ToString(), true)) + "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                    }
                    if (ClGestion.Tiene_Anexos_Poligono(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])) == 1)
                    {
                        int Id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]);
                        string url = "";
                        //url =  "/Segefor_new/Mapas/MenuMapas.aspx?Id=" + Id;
                        string RutaMapa = System.Configuration.ConfigurationManager.AppSettings["SitioMapas"];
                        url = RutaMapa + "/MenuMapas.aspx?Id=" + Id;
                        string popupScript = "window.open('" + url + "', 'popup_window', 'left=100,top=100,resizable=yes');";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", popupScript, true);
                    }
                }
                else if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "2")
                {
                    int Id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]);
                    string GestionNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUG"].ToString();
                    String js = "window.open('Wfrm_AnexosPlanManejo.aspx?idgestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Id.ToString(), true)) + "&NUG=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionNo.ToString(), true)) + "', '_blank');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
                }
                
                
            }
        }

        void GrdSolicitudes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("ModuloId").ToString() == "3")
                {
                    //item["actividad"].Text = ClGestion.Get_Actividad_Registro(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    int Categoria = ClGestion.Get_CategoriaRNFId(Actividad);
                    if ((Categoria == 2) || (Categoria == 3) || (Categoria == 4) || (Categoria == 6) || (Categoria == 1))
                    {
                        ImageButton Anexo;
                        Anexo = (ImageButton)item.FindControl("ImgAnexos");
                        Anexo.Visible = true;
                        if ((ClGestion.Tiene_Anexos_Inventerio(Convert.ToInt32(item.GetDataKeyValue("GestionId"))) == 0) && (ClGestion.Tiene_Anexos_Poligono(Convert.ToInt32(item.GetDataKeyValue("GestionId"))) == 0))
                        {
                            Anexo.Visible = false;
                        }
                    }
                }
                else if (item.GetDataKeyValue("ModuloId").ToString() == "2")
                {
                    item["actividad"].Text =  ClManejo.Get_Actividad_Manejo(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    ImageButton Anexo;
                    Anexo = (ImageButton)item.FindControl("ImgAnexos");
                    Anexo.Visible = true;
                }
            }
        }

        void GrdSolicitudes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones_Usuario(Convert.ToInt32(Session["PersonaId"])), GrdSolicitudes);
        }
    }
}