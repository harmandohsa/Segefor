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
    public partial class Wfrm_CambioClave : System.Web.UI.Page
    {
        Cl_Utilitarios Clutilitarios;
        Cl_Usuario Clususario;
        Cl_Persona ClPersona;
        protected void Page_Load(object sender, EventArgs e)
        {
            Clutilitarios = new Cl_Utilitarios();
            Clususario = new Cl_Usuario();
            ClPersona = new Cl_Persona();

            BtnCambia.Click += BtnCambia_Click;
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                Clususario.Insertar_Ingreso_Pagina(15, Convert.ToInt32(Session["UsuarioId"]),Clususario.CorrId_IngresoPagina());
                if (Clutilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true) == "1")
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl sidvar;
                    sidvar = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("MenuAdm");
                    sidvar.Visible = false;

                    System.Web.UI.WebControls.Label lblUsuario;
                    lblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                    lblUsuario.Text = "Nuevo Usuario";


                    System.Web.UI.HtmlControls.HtmlGenericControl lines;
                    lines = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("lines");
                    lines.Visible = false;

                    System.Web.UI.HtmlControls.HtmlGenericControl Notif;
                    Notif = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("Notif");
                    Notif.Visible = false;

                    DataSet dsPermisos = Clususario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 15);
                    if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                    {
                        BtnCambia.Visible = false;
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

                    System.Web.UI.WebControls.Label LblPerfil;
                    LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                    LblPerfil.Text = Clususario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                    System.Web.UI.WebControls.Label LblUsuario;
                    LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                    LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));
                }
                else
                {
                    Clutilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                    System.Web.UI.WebControls.Label LblPerfil;
                    LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                    LblPerfil.Text = Clususario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                    System.Web.UI.WebControls.Label LblUsuario;
                    LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                    LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                    DataSet dsPermisos = Clususario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 15);
                    if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                    {
                        BtnCambia.Visible = false;
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

                }
            }
        }

        void BtnCambia_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                Clususario.Actualiza_Clave(Convert.ToInt32(Session["UsuarioId"]), Clutilitarios.Encrypt(TxtNuevaClave.Text, true), 0);
                Response.Redirect("Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(Clutilitarios.Encrypt("0", true)) + "");
            }
        }

        bool Valida()
        {
            bool HayError = false;
            LblMensaje.Text = "";
            BtnEror.Visible = false;
            string ClaveActual = Clutilitarios.Decrypt(Clususario.Get_Clave(Convert.ToInt32(Session["UsuarioID"])), true);
            if (ClaveActual != TxtClaveActual.Text)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La Contraseña actual no coincide";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La Contraseña actual no coincide";
                HayError = true;
            }
            if (TxtNuevaClave.Text.Length < 8)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La nueva contraseña debe ser mayor a 8 caracteres";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La nueva contraseña debe ser mayor a 8 caracteres";
                HayError = true;
            }
            
            if (Clutilitarios.TieneNumero(TxtNuevaClave.Text) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La nueva contraseña debe contener al menos un número";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La nueva contraseña debe contener al menos un número";
                HayError = true;
            }
            if (Clutilitarios.TieneMayus(TxtNuevaClave.Text) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La nueva contraseña debe contener al menos una letra mayuscula";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La nueva contraseña debe contener al menos una letra mayuscula";
                HayError = true;
            }
            if (Clutilitarios.TieneMinus(TxtNuevaClave.Text) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La nueva contraseña debe contener al menos una letra minuscula";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La nueva contraseña debe contener al menos una letra minuscula";
                HayError = true;
            }
            if (Clutilitarios.TieneEspecial(TxtNuevaClave.Text) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La nueva contraseña debe contener al menos un caracter especial";
                else
                    LblMensaje.Text = LblMensaje.Text + ", La nueva contraseña debe contener al menos un caracter especial";
                HayError = true;
            }
            if (TxtConfClave.Text != TxtNuevaClave.Text)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Las nuevas contraseñas no coinciden";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Las nuevas contraseñas no coinciden";
                HayError = true;
            }
            if (HayError == true)
            {
                BtnEror.Visible = true;
                return false;
            }

            else
                return true;
        }
    }
}