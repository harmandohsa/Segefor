using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_SeleccionPlanMenejo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Manejo ClManejo;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClManejo = new Cl_Manejo();

            BtnGrabar.ServerClick += BtnGrabar_ServerClick;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(47, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 47);
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
                double Area = ClManejo.Get_Area_PlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["affectation"].ToString()), true)));
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_SubCategoriaMF(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["typecategoria"].ToString()), true)),Area), CboTipoPlanManejo, "SubCategoriaId", "SubCategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoPlanManejo, "Plan de Manejo");
                
            }
        }

        void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            if (CboTipoPlanManejo.SelectedValue == "")
            {
                DivErr.Visible = true;
                LblMansajeErr.Text = "Debe seleccionar el Tipo de Plan de Manejo Forestal";
            }
            else
            {

                ClManejo.ActualizaSubCategoriaPlan(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["affectation"].ToString()), true)), Convert.ToInt32(CboTipoPlanManejo.SelectedValue));
                Response.Redirect("~/WebForms/Wfrm_TipoPlanManejo.aspx?affectation=" + HttpUtility.UrlEncode(Request.QueryString["affectation"].ToString()) + "&typeplan=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CboTipoPlanManejo.SelectedValue, true)) + "&utilisater=" + HttpUtility.UrlEncode(Request.QueryString["utilisater"].ToString()) + "");
            }
        }
    }
}