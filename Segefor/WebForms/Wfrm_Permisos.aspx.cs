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
   

    public partial class Wfrm_Permisos : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            GrdUsuarios.NeedDataSource += GrdUsuarios_NeedDataSource;
            GrdUsuarios.ItemCommand += GrdUsuarios_ItemCommand;
            TreePermisos.NeedDataSource += TreePermisos_NeedDataSource;
            TreePermisos.ChildItemsDataBind += TreePermisos_ChildItemsDataBind;
            TreePermisos.ItemDataBound += TreePermisos_ItemDataBound;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(29, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 29);
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

        void TreePermisos_ItemDataBound(object sender, Telerik.Web.UI.TreeListItemDataBoundEventArgs e)
        {
            if (e.Item.ItemType == TreeListItemType.Item || e.Item.ItemType == TreeListItemType.AlternatingItem)
            {
                TreeListDataItem item = e.Item as TreeListDataItem;
                TableCell cell = item["Consultar"];
                string ConsultarVal = cell.Text;

                CheckBox Consultar;
                Consultar = (CheckBox)item.FindControl("ChkConsultar");
                if (ConsultarVal == "1")
                {
                    Consultar.Checked = true;

                }
                if (TxtEditar.Text == "0")
                    Consultar.Enabled = false;

                TableCell cellIns = item["Insertar"];
                string InsertarVal = cellIns.Text;
                CheckBox Insertar;
                Insertar = (CheckBox)item.FindControl("ChkInsertar");
                if (InsertarVal == "1")
                {

                    Insertar.Checked = true;

                }
                if (TxtEditar.Text == "0")
                    Insertar.Enabled = false;


                TableCell cellEdit = item["Editar"];
                string EditarVal = cellEdit.Text;
                CheckBox Editar;
                Editar = (CheckBox)item.FindControl("ChkEditar");
                if (EditarVal == "1")
                {

                    Editar.Checked = true;

                }
                if (TxtEditar.Text == "0")
                    Editar.Enabled = false;


                TableCell cellEliminar = item["Eliminar"];
                string EliminarVal = cellEliminar.Text;
                CheckBox Eliminar;
                Eliminar = (CheckBox)item.FindControl("ChkEliminar");
                if (EliminarVal == "1")
                {

                    Eliminar.Checked = true;

                }
                if (TxtEditar.Text == "0")
                    Eliminar.Enabled = false;
            }
        }

        void TreePermisos_ChildItemsDataBind(object sender, Telerik.Web.UI.TreeListChildItemsDataBindEventArgs e)
        {
            int PadreId = Convert.ToInt32(e.ParentDataKeyValues["FormaId"].ToString());
            e.ChildItemsDataSource = ClUsuario.Roles_Usuario(Convert.ToInt32(TxtUsuarioId.Text), PadreId, 0);
        }

        void TreePermisos_NeedDataSource(object sender, Telerik.Web.UI.TreeListNeedDataSourceEventArgs e)
        {
            if (TxtUsuarioId.Text !="")
                ClUtilitarios.LlenaRadTree(ClUsuario.Roles_Usuario(Convert.ToInt32(TxtUsuarioId.Text), 0, 1), TreePermisos);
        }

        void 
            GrdUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdPermisos")
            {
                LblUsuario.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nombres"].ToString();
                TxtUsuarioId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString();
                TreePermisos.Rebind();
            }
        }

        void GrdUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClUsuario.Get_Usuarios(0), GrdUsuarios);
        }

        protected void ChkConsultar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkConsultar = (CheckBox)sender;
            TreeListDataItem item = (TreeListDataItem)ChkConsultar.Parent.Parent;
            TableCell cell = item["FormaId"];
            int Permiso = 0;
            if (ChkConsultar.Checked == true)
                Permiso = 1;
            else
                Permiso = 0;
            ClUsuario.Actualiza_Rol_Usuario(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 1);
        }

        protected void ChkInsertar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkInsertar = (CheckBox)sender;
            TreeListDataItem item = (TreeListDataItem)ChkInsertar.Parent.Parent;
            TableCell cell = item["FormaId"];
            int Permiso = 0;
            if (ChkInsertar.Checked == true)
                Permiso = 1;
            else
                Permiso = 0;
            ClUsuario.Actualiza_Rol_Usuario(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 2);
        }

        protected void ChkEditar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkEditar = (CheckBox)sender;
            TreeListDataItem item = (TreeListDataItem)ChkEditar.Parent.Parent;
            TableCell cell = item["FormaId"];
            int Permiso = 0;
            if (ChkEditar.Checked == true)
                Permiso = 1;
            else
                Permiso = 0;
            ClUsuario.Actualiza_Rol_Usuario(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 3);
        }

        protected void ChkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkEliminar = (CheckBox)sender;
            TreeListDataItem item = (TreeListDataItem)ChkEliminar.Parent.Parent;
            TableCell cell = item["FormaId"];
            int Permiso = 0;
            if (ChkEliminar.Checked == true)
                Permiso = 1;
            else
                Permiso = 0;
            ClUsuario.Actualiza_Rol_Usuario(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 4);
        }
    }
}