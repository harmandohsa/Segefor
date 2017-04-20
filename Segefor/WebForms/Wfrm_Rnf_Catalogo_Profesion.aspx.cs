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
    public partial class Wfrm_Rnf_Catalogo_Profesion : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Profesion ClProfesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClProfesion = new Cl_Profesion();
            GrdProfesion.NeedDataSource += GrdProfesion_NeedDataSource;
            BtnGrabar.Click += BtnGrabar_Click;
            BtnNuevo.ServerClick += BtnNuevo_ServerClick;
            GrdProfesion.ItemCommand += GrdProfesion_ItemCommand;
            BtnYes.Click += BtnYes_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(22, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 22);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    GrdProfesion.Columns[4].Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnGrabar.Visible = false;
                    BtnNuevo.Visible = false;
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

                ClUtilitarios.LlenaCombo(ClCatalogos.Categoria_Profesion_GetAll(), CboCategoria, "CategoriaProfesionId", "CategoriaProfesion");
                ClUtilitarios.AgregarSeleccioneCombo(CboCategoria, "Grado Académico");
                ClUtilitarios.LlenaCombo(ClCatalogos.Estatus_Profesion_GetAll(), CboEstatus, "EstatusProfesionId", "EstatusProfesion");
                ClUtilitarios.AgregarSeleccioneCombo(CboEstatus, "Estatus");
            }
        }

        void Limpiar()
        {
            TxtProfesion.Text = "";
            CboCategoria.SelectedValue = "0";
            CboEstatus.SelectedValue = "0";
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            ClProfesion.Actualiza_Profesion(Convert.ToInt32(TxtProfesionId.Text), TxtProfesion.Text, Convert.ToInt32(CboCategoria.SelectedValue), Convert.ToInt32(CboEstatus.SelectedValue));
            ClUsuario.Insertar_Actividad_Pagina(22, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
            BtnGood.Visible = true;
            LblGood.Text = "Profesión modificada exitosamente";
            GrdProfesion.Rebind();
            Nuevo();
        }

        void GrdProfesion_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEdit")
            {
                DataSet ds = new DataSet();
                TxtProfesionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"].ToString();
                ds = ClProfesion.Profesion_Get(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]));
                TxtProfesion.Text = ds.Tables["DATOS"].Rows[0]["Profesion"].ToString();
                CboCategoria.SelectedValue = ds.Tables["DATOS"].Rows[0]["CategoriaProfesionId"].ToString();
                CboCategoria.Text = ds.Tables["DATOS"].Rows[0]["CategoriaProfesion"].ToString();
                CboEstatus.SelectedValue = ds.Tables["DATOS"].Rows[0]["EstatusProfesionId"].ToString();
                CboEstatus.Text = ds.Tables["DATOS"].Rows[0]["EstatusProfesion"].ToString();
                TxtOriginalProfesion.Text = ds.Tables["DATOS"].Rows[0]["Profesion"].ToString();
                TxtOriginalCategoriaId.Text = ds.Tables["DATOS"].Rows[0]["CategoriaProfesionId"].ToString();
            }
        }

        void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            BtnGood.Visible = false;
            BtnEror.Visible = false;
            Nuevo();
        }

        void Nuevo()
        {
            
            TxtProfesionId.Text = "";
            TxtOriginalCategoriaId.Text = "";
            TxtOriginalProfesion.Text = "";
            Limpiar();
        }

        void BtnGrabar_Click(object sender, EventArgs e)
        {
            BtnGood.Visible = false;
            BtnEror.Visible = false;
            if (TxtProfesionId.Text == "")
            {
                if (Valida() == true)
                {
                    ClProfesion.Inserta_Profesion(TxtProfesion.Text, Convert.ToInt32(CboCategoria.SelectedValue), Convert.ToInt32(CboEstatus.SelectedValue));
                    ClUsuario.Insertar_Actividad_Pagina(22, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                    BtnGood.Visible = true;
                    LblGood.Text = "Profesión agregada exitosamente";
                    GrdProfesion.Rebind();
                    Nuevo();
                }
            }
            else
            {
                if ((CboCategoria.SelectedValue != TxtOriginalCategoriaId.Text) || (TxtProfesion.Text != TxtOriginalProfesion.Text))
                {
                    if (Valida() == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + WindowAsk.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + WindowAsk.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                
            }
            
        }

        void GrdProfesion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClCatalogos.Profesiones_GetAll(), GrdProfesion);
        }

        bool Valida()
        {
            LblMensaje.Text = "";
            bool HayError = false;
            if (ClProfesion.Existe_Profesion(TxtProfesion.Text, 0) == true)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Ya existe esta profesión";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Ya existe esta profesión";
                HayError = true;
            }
            if ((CboCategoria.SelectedValue == "") || (CboCategoria.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe Seleccionar un Grado Académico";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe Seleccionar un Grado Académico";
                HayError = true;
            }
            if ((CboEstatus.SelectedValue == "") || (CboEstatus.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe Seleccionar un estatus";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe Seleccionar un estatus";
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