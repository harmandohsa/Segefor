using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_PlanManejo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Manejo ClManejo;
        Cl_Registro ClRegistro;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClManejo = new Cl_Manejo();
            ClRegistro = new Cl_Registro();

            GrdPlanesSolicitados.NeedDataSource += GrdPlanesSolicitados_NeedDataSource;
            GrdPlanesSolicitadosComoRegente.NeedDataSource += GrdPlanesSolicitadosComoRegente_NeedDataSource;
            GrdPlanesSolicitadosComoRegente.ItemCommand += GrdPlanesSolicitadosComoRegente_ItemCommand;
            GrdPlanesSolicitadosComoRegente.ItemDataBound += GrdPlanesSolicitadosComoRegente_ItemDataBound;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(46, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 46);
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

                int EsEMPF = ClRegistro.EsElaboradorPM_Activo(Convert.ToInt32(Session["PersonaId"]));
                if (EsEMPF == 0)
                {
                    DivTitPlanProceso.Visible = false;
                    DivPlanProceso.Visible = false;
                }
            }

        }

        void GrdPlanesSolicitadosComoRegente_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (dataItem["FechaAcepta"].Text == "&nbsp;")
                    dataItem["Go"].FindControl("ImgIrPlan").Visible = false;
                else
                {
                    dataItem["Ok"].FindControl("ImgAceptar").Visible = false;
                    dataItem["No"].FindControl("ImgRechazar").Visible = false;
                }
            }
        }

        void GrdPlanesSolicitadosComoRegente_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdOk")
            {
                ClManejo.ActualizaEstatusFechaAsignacionElaborador(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 2);
                GrdPlanesSolicitadosComoRegente.Rebind();
            }
            else if (e.CommandName == "CmdNo")
            {
                ClManejo.ActualizaEstatusFechaAsignacionElaborador(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 3);
                DataSet dsUsuario =  ClUsuario.Datos_UsuarioId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]));
                string Correo = dsUsuario.Tables["Datos"].Rows[0]["Correo"].ToString();
                int PersonaId = Convert.ToInt32(dsUsuario.Tables["Datos"].Rows[0]["PersonaId"].ToString());
                dsUsuario.Clear();
                string Mensaje = "Se le notifica que No se acepta la realización de su Plan de Manejo Forestal.";
                ClUtilitarios.EnvioCorreo(Correo, ClPersona.Nombre_Usuario(PersonaId), "Rechazo Plan de Manejo", Mensaje, 0, "", "");
                GrdPlanesSolicitadosComoRegente.Rebind();
            }
            else if (e.CommandName == "CmdGo")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubCategoriaId"].ToString() == "")
                    Response.Redirect("~/WebForms/Wfrm_SeleccionPlanMenejo.aspx?typecategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CategoriaId"].ToString(), true)) + "&affectation=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&utilisater=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString(), true)) + "");
                else
                    Response.Redirect("~/WebForms/Wfrm_TipoPlanManejo.aspx?typeplan=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubCategoriaId"].ToString(), true)) + "&affectation=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&utilisater=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString(), true)) + "");
            }
        }

        void GrdPlanesSolicitadosComoRegente_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_PlanesManejo(2, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(Session["UsuarioId"])), GrdPlanesSolicitadosComoRegente);
        }

        void GrdPlanesSolicitados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_PlanesManejo(1,Convert.ToInt32(Session["PersonaId"]),Convert.ToInt32(Session["UsuarioId"])),GrdPlanesSolicitados);
        }
    }
}