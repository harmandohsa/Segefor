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
    public partial class Wfrm_RegistroForestal : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ImgBosqueNat.Click += ImgBosqueNat_Click;
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                if (Session["TipoUsuarioId"].ToString() == "12")
                {
                    ImgEmpresas.Enabled = false;
                    ImgPlantacionFores.Enabled = false;
                    ImgSistemaAgro.Enabled = false;
                    ImgPlantacionArbol.Enabled = false;
                    ImgFuenteSemilla.Enabled = false;
                    ImgTecnico.Enabled = false;
                    ImgMoto.Enabled = false;
                    ImgEntidad.Enabled = false;
                    ImgOtras.Enabled = false;
                }
               

                ClUsuario.Insertar_Ingreso_Pagina(17, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
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
            }
        }

        void ImgBosqueNat_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["TipoUsuarioId"].ToString() == "12")
            {
                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 49);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Consultar"]) == 1)
                {
                    Response.Redirect("~/WebForms/Wfrm_SeleccionaPersona.aspx");
                }
                
            }
            else
                Response.Redirect("~/WebForms/Wfrm_BosqueNatural.aspx");
        }
    }
}