using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Inicio : System.Web.UI.Page
    {
        Cl_Utilitarios Clutilitarios;
        Cl_Usuario Clususario;
        Cl_Persona ClPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            Clutilitarios = new Cl_Utilitarios();
            Clususario = new Cl_Usuario();
            ClPersona = new Cl_Persona();

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                Clutilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = Clususario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

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

                System.Web.UI.HtmlControls.HtmlGenericControl Notif;
                Notif = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("Notif");
                Notif.Visible = false;
                string llamada = "";
                if (Request.QueryString["appel"].ToString() != "0")
                    llamada = Clutilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
                if (llamada == "0")
                {
                    Mensaje.Visible = false;
                    LnkLink.Visible = false;
                }
                else if (llamada == "1") 
                {
                    Mensaje.Visible = true;
                    LnkLink.Visible = true;
                    Mensaje.Text = "Estimado(a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])) + " su solicitud fue enviada exitosamente, en breve recibira un correo donde podra imprimir su formulario de inscripción, debera presentarse a la oficina del INAB correspondiente lo antes posible, por favor dirigase al siguiente enlace para ver sus gestiones e imprimir los documentos correspondientes para el ingreso de su gestión";
                    //RadWindow1.Title = "Formulario de Inscripción";
                    //RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioProfesional.aspx?appel=" + HttpUtility.UrlEncode(Clutilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (llamada == "2") 
                {
                    Mensaje.Visible = true;
                    LnkLink.Visible = true;
                    Mensaje.Text = "Estimado(a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])) + " su solicitud fue enviada exitosamente, en breve recibira un correo donde podra imprimir su formulario de inscripción, debera presentarse a la oficina del INAB correspondiente lo antes posible, por favor dirigase al siguiente enlace para ver sus gestiones e imprimir los documentos correspondientes para el ingreso de su gestión";
                    //RadWindow1.Title = "Formulario de Inscripción";
                    //RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(Clutilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (llamada == "3")
                {
                    Mensaje.Visible = true;
                    LnkLink.Visible = true;
                    Mensaje.Text = Clutilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["message"].ToString()), true);
                }
                else if (llamada == "4")
                {
                    Mensaje.Visible = true;
                    LnkLink.Visible = true;
                    Mensaje.Text = "Estimado(a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])) + " la solicitud de inscripción de Bosques Naturales a nombre de: " + Session["Solicitante"] + " fue enviada exitosamente.";
                    //RadWindow1.Title = "Formulario de Inscripción";
                    //RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(Clutilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
        }
    }
}