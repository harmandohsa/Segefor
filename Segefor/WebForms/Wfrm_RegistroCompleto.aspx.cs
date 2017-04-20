using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_RegistroCompleto : System.Web.UI.Page
    {
        Cl_Persona ClPersona;
        Cl_Utilitarios ClUtilitarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClPersona = new Cl_Persona();
            ClUtilitarios = new Cl_Utilitarios();
            System.Web.UI.HtmlControls.HtmlGenericControl sidvar;
            sidvar = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("MenuAdm");
            sidvar.Visible = false;

            System.Web.UI.WebControls.Label lblUsuario;
            lblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
            lblUsuario.Text = "Usuario Creado";

            System.Web.UI.WebControls.Label LblTipoUsuario;
            LblTipoUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
            LblTipoUsuario.Visible = false;

            System.Web.UI.HtmlControls.HtmlGenericControl lines;
            lines = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("lines");
            lines.Visible = false;

            System.Web.UI.HtmlControls.HtmlGenericControl Notif;
            Notif = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("Notif");
            Notif.Visible = false;

            System.Web.UI.HtmlControls.HtmlImage ImgPerfil;
            ImgPerfil = (System.Web.UI.HtmlControls.HtmlImage)Master.FindControl("ImgPerfilFake");
            ImgPerfil.Src = "../imagenes/User_Ok.png";
            ImgPerfil.Visible = true;
            
            if (!IsPostBack)
            {
                Mensaje.Text = "Estimado(a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true))) + " tenemos el agrado de informarle que su usuario para ingresar al Sistema Electrónico de Gestión Forestal -SEGEFOR- fue creado con éxito en breve estará recibiendo en su correo electrónico sus datos de ingreso al sistema.";
            }
        }
    }
}