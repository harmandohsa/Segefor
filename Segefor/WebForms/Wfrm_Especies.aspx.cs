using SEGEFOR.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Especies : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Especie ClEspecie;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClEspecie = new Cl_Especie();

            GrdEspecies.NeedDataSource += GrdEspecies_NeedDataSource;
            GrdEspecies.ItemDataBound += GrdEspecies_ItemDataBound;
            GrdEspecies.ItemCommand += GrdEspecies_ItemCommand;
            btnNuevo.ServerClick += btnNuevo_ServerClick;
            BtnGrabar.ServerClick += BtnGrabar_ServerClick;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(53, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 53);
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
            }

        }

        bool Valida()
        {
            DivErrGrabar.Visible = false;
            bool HayError = false;
            LblErrGrabar.Text = "";
            LblGoodGrabar.Text = "";
            if (TxtCodigo.Text == "")
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe ingresar el código de especie";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe ingresar el código de especie";
                HayError = true;
            }
            
            if (ClEspecie.SP_Existe_Especie_Codigo(TxtCodigo.Text) > 0)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "La especie ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", La especie ya existe";
                HayError = true;
            }
            if (TxtNomCientifico.Text == "")
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe ingresar el nombre cientifico";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe ingresar el nombre cientifico";
                HayError = true;
            }
            if (ClEspecie.SP_Existe_Especie_NombreCientifico(TxtNomCientifico.Text) > 0)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Este nombre cientifico ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", este nombre cientifico ya existe";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrGrabar.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ValidaMod()
        {
            DivErrGrabar.Visible = false;
            bool HayError = false;
            LblErrGrabar.Text = "";
            LblGoodGrabar.Text = "";
            if (TxtCodigo.Text == "")
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe ingresar el código de especie";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe ingresar el código de especie";
                HayError = true;
            }

            if ((TxtCodigo.Text != TxtCodigoEspecieOrignial.Text) && (ClEspecie.SP_Existe_Especie_Codigo(TxtCodigo.Text) > 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "La especie ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", La especie ya existe";
                HayError = true;
            }
            if (TxtNomCientifico.Text == "")
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe ingresar el nombre cientifico";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe ingresar el nombre cientifico";
                HayError = true;
            }
            if ((TxtNomCientifico.Text != TxtNombreCientificoOriginal.Text) && (ClEspecie.SP_Existe_Especie_NombreCientifico(TxtNomCientifico.Text) > 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Este nombre cientifico ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", este nombre cientifico ya existe";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrGrabar.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            DivGoodGrabar.Visible = false;
            DivErrGrabar.Visible = false;
            if (TxtEspecieId.Text == "")
            {
                if (Valida() == true)
                {
                    ClEspecie.Insert_Especie(TxtCodigo.Text, TxtNomCientifico.Text, TxtFamilia.Text, TxtGenero.Text, TxtAutores.Text, TxtNombreComun.Text, TxtSinonimo.Text, TxtNombreComercial.Text);
                    DivGoodGrabar.Visible = true;
                    LblGoodGrabar.Text = "Especie grababa";
                    GrdEspecies.Rebind();
                    Limpiar();
                    Bloquear();
                }
                
            }
            else
            {
                if (ValidaMod() == true)
                {
                    ClEspecie.Update_Especie(Convert.ToInt32(TxtEspecieId.Text), TxtCodigo.Text, TxtNomCientifico.Text, TxtFamilia.Text, TxtGenero.Text, TxtAutores.Text, TxtNombreComun.Text, TxtSinonimo.Text, TxtNombreComercial.Text);
                    DivGoodGrabar.Visible = true;
                    LblGoodGrabar.Text = "Especie modificada";
                    GrdEspecies.Rebind();
                    Limpiar();
                    Bloquear();
                }
                
            }
        }

        void Limpiar()
        {
            TxtCodigo.Text = "";
            TxtNomCientifico.Text = "";
            TxtFamilia.Text = "";
            TxtGenero.Text = "";
            TxtAutores.Text = "";
            TxtNombreComun.Text = "";
            TxtSinonimo.Text = "";
            TxtNombreComercial.Text = "";
            TxtCodigoEspecieOrignial.Text = "";
            TxtNombreCientificoOriginal.Text = "";
        }

        void btnNuevo_ServerClick(object sender, EventArgs e)
        {
            DivErrGrabar.Visible = false;
            DivGoodGrabar.Visible = false;
            Desbloquear();
            TxtEspecieId.Text = "";
            Limpiar();
        }

        void GrdEspecies_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdInac")
            {
                ClEspecie.Update_Estatus_Especie(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"]), 0);
                GrdEspecies.Rebind();
            }
            else if (e.CommandName == "CmdActivar")
            {
                ClEspecie.Update_Estatus_Especie(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"]), 1);
                GrdEspecies.Rebind();
            }
            else if (e.CommandName == "CmdEdit")
            {
                TxtEspecieId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString();
                TxtCodigo.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Codigo_Especie"].ToString();
                TxtNomCientifico.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Nombre_Cientifico"].ToString();
                TxtFamilia.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Familia"].ToString();
                TxtGenero.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Genero"].ToString();
                TxtAutores.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Autores"].ToString();
                TxtNombreComun.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Nombre_Comun"].ToString();
                TxtSinonimo.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Sinonimos"].ToString();
                TxtNombreComercial.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Nombre_Comercial"].ToString();
                TxtCodigoEspecieOrignial.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Codigo_Especie"].ToString();
                TxtNombreCientificoOriginal.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Nombre_Cientifico"].ToString();
                Desbloquear();
            }
        }

        void Desbloquear()
        {
            TxtCodigo.Enabled = true;
            TxtNomCientifico.Enabled = true;
            TxtFamilia.Enabled = true;
            TxtGenero.Enabled = true;
            TxtAutores.Enabled = true;
            TxtNombreComun.Enabled = true;
            TxtSinonimo.Enabled = true;
            TxtNombreComercial.Enabled = true;
        }

        void Bloquear()
        {
            TxtCodigo.Enabled = false;
            TxtNomCientifico.Enabled = false;
            TxtFamilia.Enabled = false;
            TxtGenero.Enabled = false;
            TxtAutores.Enabled = false;
            TxtNombreComun.Enabled = false;
            TxtSinonimo.Enabled = false;
            TxtNombreComercial.Enabled = false;
        }

        void GrdEspecies_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("Estado_EspecieId").ToString() == "1")
                {
                    ImageButton ImgAct;
                    ImgAct = (ImageButton)item.FindControl("ImgInactivar");
                    ImgAct.Visible = true;
                }
                else
                {
                    ImageButton ImgDes;
                    ImgDes = (ImageButton)item.FindControl("ImgActivar");
                    ImgDes.Visible = true;
                }

            }
        }

        void GrdEspecies_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClCatalogos.ListadoEspecie_Catalogo(), GrdEspecies);
        }
    }
}