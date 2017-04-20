using Excel;
using SEGEFOR.Clases;
using SEGEFOR.Data_Set;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_MantRegionSubregion : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Regiones ClRegiones;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClRegiones = new Cl_Regiones();

            GrdRegiones.ItemDataBound += GrdRegiones_ItemDataBound;
            GrdRegiones.ItemCommand += GrdRegiones_ItemCommand;
            GrdSubRegiones.NeedDataSource += GrdSubRegiones_NeedDataSource;
            GrdRegiones.NeedDataSource += GrdRegiones_NeedDataSource;
            GrdSubRegiones.ItemDataBound += GrdSubRegiones_ItemDataBound;
            GrdSubRegiones.ItemCommand += GrdSubRegiones_ItemCommand;
            btnNuevo.ServerClick += btnNuevo_ServerClick;
            BtnGrabar.ServerClick += BtnGrabar_ServerClick;
            GrdMunicipios.NeedDataSource += GrdMunicipios_NeedDataSource;
            BtnNuevoMun.ServerClick += BtnNuevoMun_ServerClick;
            CboDepartamento.SelectedIndexChanged += CboDepartamento_SelectedIndexChanged;
            BtnGrabarMunicipio.ServerClick += BtnGrabarMunicipio_ServerClick;
            GrdMunicipios.ItemDataBound += GrdMunicipios_ItemDataBound;
            GrdMunicipios.ItemCommand += GrdMunicipios_ItemCommand;
            CboDepartamentoSubRegion.SelectedIndexChanged += CboDepartamentoSubRegion_SelectedIndexChanged;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(51, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 51);
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
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPersonas(), CboPersona, "PersonaId", "Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboPersona, "Persona");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamentoSubRegion, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamentoSubRegion, "Departamento");
                
            }
        }

        void CboDepartamentoSubRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboDepartamentoSubRegion.SelectedIndex > 0)
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamentoSubRegion.SelectedValue)), CboMunicipioSubRegion, "MUnicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioSubRegion, "Municipio");
            }
        }

        void GrdMunicipios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdInac")
            {
                ClRegiones.Update_Estatus_MunicipioSubRegion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MunicipioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"]), 2);
                GrdMunicipios.Rebind();
            }
            else if (e.CommandName == "CmdActivar")
            {
                ClRegiones.Update_Estatus_MunicipioSubRegion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MunicipioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"]), 1);
                GrdMunicipios.Rebind();
            }
        }

        void GrdMunicipios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item.GetDataKeyValue("EstadoSubregionMunicipioId").ToString() == "1")
                {
                    ImageButton ImgAct;
                    ImgAct = (ImageButton)item.FindControl("ImgInactivarMun");
                    ImgAct.Visible = true;
                }
                else
                {
                    ImageButton ImgDes;
                    ImgDes = (ImageButton)item.FindControl("ImgActivarMun");
                    ImgDes.Visible = true;
                }
            }
            
        }

        bool ValidaAddMunicipio()
        {
            DivErrMunicipio.Visible = false;
            bool HayError = false;
            if (CboDepartamento.SelectedIndex == 0)
            {
                if (LblMensajeErrMun.Text == "")
                    LblMensajeErrMun.Text = LblMensajeErrMun.Text + "Debe seleccionar el departamento";
                else
                    LblMensajeErrMun.Text = LblMensajeErrMun.Text + ", Debe seleccionar el departamento";
                HayError = true;
            }
            if (CboMunicipio.SelectedIndex == 0)
            {
                if (LblMensajeErrMun.Text == "")
                    LblMensajeErrMun.Text = LblMensajeErrMun.Text + "Debe seleccionar el municipio";
                else
                    LblMensajeErrMun.Text = LblMensajeErrMun.Text + ", Debe seleccionar el municipio";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrMunicipio.Visible = true;
                return false;
            }

            else
                return true;
        }



        void BtnGrabarMunicipio_ServerClick(object sender, EventArgs e)
        {
            DivNoErrMunicipio.Visible = false;
            DivErrMunicipio.Visible = false;
            if (ValidaAddMunicipio() == true)
            {
                ClRegiones.Insert_Municipio_SubRegion(Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(TxtSubRegionId.Text));
                DivAddMunicipios.Visible = false;
                GrdMunicipios.Rebind();
                DivNoErrMunicipio.Visible = true;
                LblMensajeNoErrMun.Text = "Municipio agregado exitosamente";
            }
        }

        void CboDepartamento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboDepartamento.SelectedIndex > 0 )
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MUnicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
            }
        }

        void BtnNuevoMun_ServerClick(object sender, EventArgs e)
        {
            DivAddMunicipios.Visible = true;
            DesbloqueNuevoMunicipio();
        }

        void DesbloqueNuevoMunicipio()
        {
            CboMunicipio.Enabled = true;
            CboDepartamento.Enabled = true;
        }

        void GrdMunicipios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TxtSubRegionId.Text != "")
                ClUtilitarios.LlenaGrid(ClRegiones.Get_MunicipioSubRegion(Convert.ToInt32(TxtSubRegionId.Text)),GrdMunicipios);
        }

        void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            DivNoErr.Visible = false;
            DivErr.Visible = false;
            if (ValidaSubRegion() == true)
            {
                int PersonaId = 0;
                if (CboPersona.SelectedIndex > 0)
                    PersonaId = Convert.ToInt32(CboPersona.SelectedValue);
                ClRegiones.Insert_SubRegion(TxtNoSubRegion.Text, TxtNoRegion.Text + "-" + TxtNoSubRegion.Text, TxtSubRegion.Text, PersonaId, TxtUbicacion.Text, Convert.ToInt32(TxtRegionId.Text),Convert.ToInt32(CboMunicipioSubRegion.SelectedValue));
                BloqInsertRegion();
                DivNoErr.Visible = true;
                LblMensajeNoErr.Text = "Sub región grabada";
                GrdSubRegiones.Rebind();
            }
        }

        void BloqInsertRegion()
        {
            TxtNoSubRegion.Text = "";
            TxtSubRegion.Text = "";
            TxtUbicacion.Text = "";
            TxtNoSubRegion.Enabled = false;
            TxtSubRegion.Enabled = false;
            TxtUbicacion.Enabled = false;
            CboPersona.Enabled = false;
            DivAddSubRegion.Visible = false;
        }

        bool ValidaSubRegion()
        {
            bool HayError = false;
            if (TxtNoSubRegion.Text == "")
            {
                if (LblMensajeErr.Text == "")
                    LblMensajeErr.Text = LblMensajeErr.Text + "Debe ingresar el número de subregión";
                else
                    LblMensajeErr.Text = LblMensajeErr.Text + ", Debe ingresar el número de subregión";
                HayError = true;
            }
            if (TxtSubRegion.Text == "")
            {
                if (LblMensajeErr.Text == "")
                    LblMensajeErr.Text = LblMensajeErr.Text + "Debe ingresar el nombre de subregión";
                else
                    LblMensajeErr.Text = LblMensajeErr.Text + ", Debe ingresar el nombre de subregión";
                HayError = true;
            }
            if (TxtUbicacion.Text == "")
            {
                if (LblMensajeErr.Text == "")
                    LblMensajeErr.Text = LblMensajeErr.Text + "Debe ingresar la ubicación de la subregión";
                else
                    LblMensajeErr.Text = LblMensajeErr.Text + ", Debe ingresar la ubicación de la subregión";
                HayError = true;
            }
            if (CboDepartamentoSubRegion.SelectedIndex == 0)
            {
                if (LblMensajeErr.Text == "")
                    LblMensajeErr.Text = LblMensajeErr.Text + "Debe seleccionar el departamento de la subregión";
                else
                    LblMensajeErr.Text = LblMensajeErr.Text + ", Debe seleccionar el departamento de la subregión";
                HayError = true;
            }
            if (CboMunicipioSubRegion.SelectedIndex == 0)
            {
                if (LblMensajeErr.Text == "")
                    LblMensajeErr.Text = LblMensajeErr.Text + "Debe seleccionar el municipio de la subregión";
                else
                    LblMensajeErr.Text = LblMensajeErr.Text + ", Debe seleccionar el municipio de la subregión";
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

       

        void DesbloqNuevaRegion()
        {
            TxtNoSubRegion.Enabled = true;
            TxtSubRegion.Enabled = true;
            CboPersona.Enabled = true;
            TxtUbicacion.Enabled = true;
            CboDepartamentoSubRegion.Enabled = true;
            CboMunicipioSubRegion.Enabled = true;
        }

        void BloqNuevaRegion()
        {
            TxtNoSubRegion.Enabled = false;
            TxtSubRegion.Enabled = false;
            CboPersona.Enabled = false;
            TxtUbicacion.Enabled = false;
            CboDepartamentoSubRegion.Enabled = false;
            CboMunicipioSubRegion.Enabled = false;
        }

        void btnNuevo_ServerClick(object sender, EventArgs e)
        {
            DivAddSubRegion.Visible = true;
            DesbloqNuevaRegion();
        }

        void GrdSubRegiones_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdInac")
            {
                ClRegiones.Update_Estatus_SubRegion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"]), 2);
                GrdSubRegiones.Rebind();
            }
            else if (e.CommandName == "CmdActivar")
            {
                ClRegiones.Update_Estatus_SubRegion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"]), 1);
                GrdSubRegiones.Rebind();
            }
            else if (e.CommandName == "CmdMunicipios")
            {
                TxtSubRegionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"].ToString();
                GrdMunicipios.Visible = true;
                DivMunicipios.Visible = true;
                GrdMunicipios.Rebind();
            }
        }

        void 
            GrdSubRegiones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPersonas(), (RadComboBox)item["PersonaEdit"].FindControl("CboPersonaSubReg"), "PersonaId", "Persona");
                ClUtilitarios.AgregarSeleccioneCombo((RadComboBox)item["Persona"].FindControl("CboPersonaSubReg"), "Persona");

                if (item.GetDataKeyValue("Persona").ToString() != null)
                {
                    ((RadComboBox)item["PersonaEdit"].FindControl("CboPersonaSubReg")).SelectedValue = item.GetDataKeyValue("PersonaId").ToString();
                    ((RadComboBox)item["PersonaEdit"].FindControl("CboPersonaSubReg")).Text = item.GetDataKeyValue("Persona").ToString();
                }

                if (item.GetDataKeyValue("EstadoSubregionId").ToString() == "1")
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

        void GrdRegiones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClRegiones.Regiones_Mantenimiento(), GrdRegiones);
        }

        void GrdSubRegiones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TxtRegionId.Text != "")
                ClUtilitarios.LlenaGrid(ClRegiones.SubRegiones_Mantenimiento(Convert.ToInt32(TxtRegionId.Text)), GrdSubRegiones);
        }

        void GrdRegiones_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdSubRegiones")
            {
                DivSubRegiones.Visible = true;
                GrdSubRegiones.Visible = true;
                TxtRegionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegionId"].ToString();
                TxtNoRegion.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["No_Region"].ToString();
                GrdSubRegiones.Rebind();
            }
            else if (e.CommandName == "CmdPoligono")
            {
                RadWindowDetalle.Title = "Poligono de la región";
                RadWindowDetalle.NavigateUrl = "~/WebForms/Wfrm_Poligno_Region.aspx?region=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Nombre"].ToString(), true)) + "&regionid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegionId"].ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            
        }

        void GrdRegiones_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPersonas(), (RadComboBox)item["PersonaEdit"].FindControl("CboPersona"), "PersonaId", "Persona");
                ClUtilitarios.AgregarSeleccioneCombo((RadComboBox)item["Persona"].FindControl("CboPersona"), "Persona");

                if (item.GetDataKeyValue("Persona").ToString() != null)
                {
                    ((RadComboBox)item["PersonaEdit"].FindControl("CboPersona")).SelectedValue = item.GetDataKeyValue("PersonaId").ToString();
                    ((RadComboBox)item["PersonaEdit"].FindControl("CboPersona")).Text = item.GetDataKeyValue("Persona").ToString();
                }

            }
        }

        protected void CboPersona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int PersonaId = 0;
            if (e.Value != "")
                PersonaId = Convert.ToInt32(e.Value);
            RadComboBox combo = (RadComboBox)sender;
            GridDataItem item = (GridDataItem)combo.NamingContainer;
            ClRegiones.Actualiza_Regional(Convert.ToInt32(item.GetDataKeyValue("RegionId")), PersonaId);
            
        }

        protected void CboPersonaSubReg_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int PersonaId = 0;
            if (e.Value != "")
                PersonaId = Convert.ToInt32(e.Value);
            RadComboBox combo = (RadComboBox)sender;
            GridDataItem item = (GridDataItem)combo.NamingContainer;
            ClRegiones.SP_Actualiza_SubRegional(Convert.ToInt32(item.GetDataKeyValue("SubRegionId")), PersonaId);

        }
    }
}