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
    public partial class Wfrm_Notificacion_deJuridico : System.Web.UI.Page
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
            GrdSolicitudes.NeedDataSource += GrdSolicitudes_NeedDataSource;
            GrdSolicitudes.ItemDataBound += GrdSolicitudes_ItemDataBound;
            GrdSolicitudes.ItemCommand += GrdSolicitudes_ItemCommand;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(33, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 33);
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
                string llamada = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
                if (llamada == "1") //Oficio Enmienda de Juridico
                {
                    RadWindow1.Title = "Oficio de enmiendas jurídicas";
                    string OficioEnmiendaId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["oficioenmienda"].ToString()), true);
                    DataSet DatosOficioEnmiendas = ClGestion.ImpresionOficioEnmiendasJuridico(2, Convert.ToInt32(OficioEnmiendaId), Convert.ToInt32(Session["UsuarioId"]));
                    Session["DatosOficioEnmiendasJuridico"] = DatosOficioEnmiendas;
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendaJuridica.aspx";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (llamada == "2") //Resolución
                {
                    RadWindow1.Title = "Resolución de Aprobación de Inscripción";
                    string ResolucionId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["resolution"].ToString()), true);
                    DataSet DatosResolucion = ClGestion.ImpresionResolucion_Aprobacion(2, Convert.ToInt32(ResolucionId), Convert.ToInt32(Session["UsuarioId"]),0);
                    Session["DatosResolucion"] = DatosResolucion;
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAprobacion.aspx";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
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
            else if (e.CommandName == "CmdSeg")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "3")
                {
                    int SubCategoriaId = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                    {
                        DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        int CategoriaProfesion = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"]);
                        int ProfesionId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["ProfesionId"]);
                        int OrigenPersonaId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["Origen_PersonaId"]);
                        int SubRegionId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["SubRegionId"]);
                        ds.Tables.Clear();
                        string NUG = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUG"].ToString();
                        string Nombre = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nombres"].ToString();
                        if (Convert.ToInt32(Session["TipoUsuarioId"]) == 11)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_Notificacion_deJuridico_SubRegional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&profession=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProfesionId.ToString(), true)) + "&categorieprofessionnelle=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaProfesion.ToString(), true)) + "&originepersonne=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OrigenPersonaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    }

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
                    item["actividad"].Text = ClGestion.Get_Actividad_Registro(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                }
                if (Convert.ToInt32(Session["TipoUsuarioId"]) == 11)
                {
                    DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    item["No_Exp"].Text = ds.Tables["DATOS"].Rows[0]["No_Expediente"].ToString();
                    item["Fecha_Exp"].Text = ds.Tables["DATOS"].Rows[0]["Fecha"].ToString();
                    item["No_Dictamen_Juridico"].Text = ds.Tables["DATOS"].Rows[0]["No_Dictamen"].ToString();
                    ds.Clear();
                }
            }
        }

        void GrdSolicitudes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["TipoUsuarioId"].ToString() != "1")
                if (Session["TipoUsuarioId"].ToString() == "11")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(4, Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
        }
    }
}