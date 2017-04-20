using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using Telerik.Web.UI;
using System.Data;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Profesion_ActividadProfesional : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Profesion_ActividadProfesional ClProfesionActividadProfesional;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClProfesionActividadProfesional = new Cl_Profesion_ActividadProfesional();
            
            GrdProfesioActividadProfesioanal.NeedDataSource += GrdProfesioActividadProfesioanal_NeedDataSource;
            GrdProfesioActividadProfesioanal.ItemDataBound += GrdProfesioActividadProfesioanal_ItemDataBound;
            GrdProfesioActividadProfesioanal.ItemCommand += GrdProfesioActividadProfesioanal_ItemCommand;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(24, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 24);
                TxtEditar.Text = dsPermisos.Tables["Datos"].Rows[0]["Editar"].ToString();
                
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

        void GrdProfesioActividadProfesioanal_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdSiRF")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(2, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 3);
            }
            if (e.CommandName == "CmdNoRF")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 3);
            }
            if (e.CommandName == "CmdSiEPMF")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(2, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 2);
            }
            if (e.CommandName == "CmdNoEPMF")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 2);
            }
            if (e.CommandName == "CmdSiEECUT")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(2, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 1);
            }
            if (e.CommandName == "CmdNoEECUT")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 1);
            }
            if (e.CommandName == "CmdSiCFSS")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(2, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 16);
            }
            if (e.CommandName == "CmdNoCFSS")
            {
                ClProfesionActividadProfesional.Actualiza_Profesion_Actividad_Profesional(1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ProfesionId"]), 7, 16);
            }
            GrdProfesioActividadProfesioanal.Rebind();
        }

        void GrdProfesioActividadProfesioanal_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item.GetDataKeyValue("RF").ToString() == "1")
                {
                    ImageButton ImgSiRF;
                    ImgSiRF = (ImageButton)item.FindControl("ImgSiRF");
                    ImgSiRF.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgSiRF.Enabled = false;
                }
                else
                {
                    ImageButton ImgNoRF;
                    ImgNoRF = (ImageButton)item.FindControl("ImgNoRF");
                    ImgNoRF.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgNoRF.Enabled = false;
                }

                if (item.GetDataKeyValue("EPMF").ToString() == "1")
                {
                    ImageButton ImgSiEPMF;
                    ImgSiEPMF = (ImageButton)item.FindControl("ImgSiEPMF");
                    ImgSiEPMF.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgSiEPMF.Enabled = false;
                }
                else
                {
                    ImageButton ImgNoEPMF;
                    ImgNoEPMF = (ImageButton)item.FindControl("ImgNoEPMF");
                    ImgNoEPMF.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgNoEPMF.Enabled = false;
                }

                if (item.GetDataKeyValue("EECUT").ToString() == "1")
                {
                    ImageButton ImgSiEECUT;
                    ImgSiEECUT = (ImageButton)item.FindControl("ImgSiEECUT");
                    ImgSiEECUT.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgSiEECUT.Enabled = false;
                }
                else
                {
                    ImageButton ImgNoEECUT;
                    ImgNoEECUT = (ImageButton)item.FindControl("ImgNoEECUT");
                    ImgNoEECUT.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgNoEECUT.Enabled = false;
                }

                if (item.GetDataKeyValue("CFSS").ToString() == "1")
                {
                    ImageButton ImgSiCFSS;
                    ImgSiCFSS = (ImageButton)item.FindControl("ImgSiCFSS");
                    ImgSiCFSS.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgSiCFSS.Enabled = false;
                }
                else
                {
                    ImageButton ImgNoCFSS;
                    ImgNoCFSS = (ImageButton)item.FindControl("ImgNoCFSS");
                    ImgNoCFSS.Visible = true;
                    if (TxtEditar.Text == "0")
                        ImgNoCFSS.Enabled = false;
                }
            }
        }

        void GrdProfesioActividadProfesioanal_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClProfesionActividadProfesional.Profesion_Actividad_Profesional_GetAll(), GrdProfesioActividadProfesioanal);
        }
    }
}