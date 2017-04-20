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
    public partial class Wfrm_GestionManejoForestal : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        

        protected void Page_Load(object sender, EventArgs e)
        {
                
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            BtnSiguiente.Click += BtnSiguiente_Click;
            //
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                if (ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["taille"].ToString()), true) == "0")
                    ClUsuario.Insertar_Ingreso_Pagina(45, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 45);
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
                if (ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["taille"].ToString()), true) != "0")
                    TxtAreaAprovecha.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["taille"].ToString()), true);
                else
                    TxtAreaAprovecha.Text = "";
                int CategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorie"].ToString()), true));
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

        void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int CategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorie"].ToString()), true));
            Response.Redirect("Wfrm_GestionManejoForestalAsocRegente.aspx?taille=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtAreaAprovecha.Text.ToString(), true)) + "&categorie="+ HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaId.ToString(), true) + ""));
        }
    }
}