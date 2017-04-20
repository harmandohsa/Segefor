using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_GestionManejoForestalAsocRegente : System.Web.UI.Page
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

            BtnRegresar.Click += BtnRegresar_Click;
            CboNombre.SelectedIndexChanged += CboNombre_SelectedIndexChanged;
            BtnAsignaRegente.Click += BtnAsignaRegente_Click;
            CboNombreEcut.SelectedIndexChanged += CboNombreEcut_SelectedIndexChanged;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
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

                TxtAreaAprovecha.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["taille"].ToString()), true);
                double Area = Convert.ToDouble(TxtAreaAprovecha.Text);
                int CategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorie"].ToString()), true));
                ClUtilitarios.LlenaCombo(ClManejo.Get_Regentes(Area,2), CboNombre, "RegistroId", "Nombre");
                if (CategoriaId == 8)
                {
                    if (Area > 1)
                    {
                        ClUtilitarios.LlenaCombo(ClManejo.Get_Regentes(Area, 1), CboNombreEcut, "RegistroId", "Nombre");
                        DivEcut.Visible = true;
                    }
                }
                
                switch (CategoriaId)
                {
                    case 3:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA DE APROVECHAMIENTO FORESTAL";
                        break;
                    case 4:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA DE APROVECHAMIENTO CON FINES SANITARIOS";
                        break;
                    case 5:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA DE SANEAMIENTO";
                        break;
                    case 6:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA DE SALVAMENTO";
                        break;

                    case 7:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA CON FINES CIENTIFICOS";
                        break;
                    case 8:
                        LblTitulo.Text = "SOLICITUD DE LICENCIA PARA CAMBIO DE USO DE TIERRA";
                        
                        break;
                }
            }
        }

        void CboNombreEcut_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboNombreEcut.SelectedValue != "")
                TxtRnfEcut.Text = ClRegistro.Get_Codigo_RNF(Convert.ToInt32(CboNombreEcut.SelectedValue));
        }

        bool Valida()
        {
            LblMensaje.Text = "";
            DivErr.Visible = false;
            bool HayError = false;

            if (CboNombre.SelectedValue == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccinar un Elaborador de Plan de Manejo Forestal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccinar un Elaborador de Plan de Manejo Forestal";
                HayError = true;
            }
            if ((DivEcut.Visible == true) && (CboNombreEcut.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccinar un Elaborador de Estudio de Capacidad de Uso de la Tierra";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccinar un Elaborador de Estudio de Capacidad de Uso de la Tierra";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErr.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnAsignaRegente_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                int CategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorie"].ToString()), true));
                ClManejo.AsignarElaborador(Convert.ToInt32(CboNombre.SelectedValue), Convert.ToInt32(Session["UsuarioId"]),Convert.ToDouble(TxtAreaAprovecha.Text),CategoriaId);

                string Mensaje = "Se le notifica que fue seleccionado para elaborar el Plan de Manejo Forestal de: " + ClManejo.Get_Categoria(CategoriaId) + " Del (la) Señor (a) " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));
                ClUtilitarios.EnvioCorreo(ClRegistro.Get_Correo_Regente(Convert.ToInt32(CboNombre.SelectedValue)), CboNombre.Text, "Asignación de Elaboración Plan de Manejo", Mensaje, 0, "", "");
                Mensaje = "Estimado (a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])) + " se le informa que ha seleccionado al  EPM: " + CboNombre.Text + ", con código RNF: " + TxtRnf.Text + ", para elaborar su plan de Manejo Forestal.";
                if (DivEcut.Visible == true)
                {
                    ClManejo.AsignarElaborador(Convert.ToInt32(CboNombreEcut.SelectedValue), Convert.ToInt32(Session["UsuarioId"]), Convert.ToDouble(TxtAreaAprovecha.Text), CategoriaId);
                    string Mensaje2 = "Se le notifica que fue seleccionado para elaborar el Plan de Manejo Forestal de: " + ClManejo.Get_Categoria(CategoriaId) + " Del (la) Señor (a) " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));
                    ClUtilitarios.EnvioCorreo(ClRegistro.Get_Correo_Regente(Convert.ToInt32(CboNombreEcut.SelectedValue)), CboNombreEcut.Text, "Asignación de Elaboración Plan de Manejo", Mensaje2, 0, "", "");
                    Mensaje = "Estimado (a): " + ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])) + " se le informa que ha seleccionado al EPMF: " + CboNombre.Text + ", con código RNF: " + TxtRnf.Text + ", para elaborar su plan de Manejo Forestal.  Y  al ECUT: " + CboNombreEcut.Text + ", con código RNF: " + TxtRnf.Text + ", para elaborar su plan de Manejo Cambio de Uso.";
                }
                Response.Redirect("Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("3", true)) + "&message=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Mensaje, true)) + "");
            }
        }

        void CboNombre_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboNombre.SelectedValue != "")
                TxtRnf.Text = ClRegistro.Get_Codigo_RNF(Convert.ToInt32(CboNombre.SelectedValue));
        }

        void BtnRegresar_Click(object sender, EventArgs e)
        {
            int CategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorie"].ToString()), true));
            Response.Redirect("Wfrm_GestionManejoForestal.aspx?taille=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtAreaAprovecha.Text.ToString(), true)) + "&categorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaId.ToString(), true) + ""));
        }
    }
}