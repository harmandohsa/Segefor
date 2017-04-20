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
    public partial class Wfrm_SeleccionaPersona : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            BtnBuscar.ServerClick += BtnBuscar_ServerClick;
            CboPersona.SelectedIndexChanged += CboPersona_SelectedIndexChanged;
            BtnGrabar.Click += BtnGrabar_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(49, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 49);
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

                ClUtilitarios.LlenaCombo(ClCatalogos.Get_PersonasGestionTecnico(), CboPersona, "PersonaId", "Nombres");
                ClUtilitarios.AgregarSeleccioneCombo(CboPersona, "Persona");
            }

        }

        void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (TxtDpi.Text == "")
            {
                LblMensajeErrorDpi.Text = "Debe Seleccionar una persona";
                BtnErrorDpi.Visible = true;
            }
            else
            {
                Response.Redirect("~/WebForms/Wfrm_Inscripcion_BosqueNatural.aspx?personneid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CboPersona.SelectedValue, true)) + "");
            }
        }

        void CboPersona_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboPersona.SelectedValue != "")
            {
                string Dpi = ClUsuario.Get_DpiPersona(Convert.ToInt32(CboPersona.SelectedValue)).ToString();
                TxtDpi.Text = Dpi.Substring(0, 4) + "-" + Dpi.Substring(4, 5) + "-" + Dpi.Substring(9, 4);
            }
        }

        void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            BtnErrorDpi.Visible = false;
            if (TxtDpi.Text.Replace("-", "") == "")
            {
                BtnErrorDpi.Visible = true;
                LblMensajeErrorDpi.Text = "ingrese el número de DPI";
            }
            else
            {
                DataSet ds = ClUsuario.Get_NombrePersona(Convert.ToInt64(TxtDpi.Text.Replace("-", "")));
                if (ds.Tables["Datos"].Rows.Count > 0)
                {
                    CboPersona.SelectedValue = ds.Tables["Datos"].Rows[0]["PersonaId"].ToString();
                    CboPersona.Text = ds.Tables["Datos"].Rows[0]["Nombre"].ToString();
                }
                else
                {
                    BtnErrorDpi.Visible = true;
                    LblMensajeErrorDpi.Text = "El Dpi no existe";
                    CboPersona.Text = "";
                    TxtDpi.Text = "";
                    CboPersona.ClearSelection();
                }
                ds.Tables.Clear();
            }
        }
    }
}