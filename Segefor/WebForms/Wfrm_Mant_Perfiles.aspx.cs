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
    public partial class Wfrm_Mant_Perfiles : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Perfil ClPerfil;
        Cl_Catalogos ClCatalogos;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClPerfil = new Cl_Perfil();
            ClCatalogos = new Cl_Catalogos();
            GrdPerfiles.NeedDataSource += GrdPerfiles_NeedDataSource;
            GrdPerfiles.ItemCommand += GrdPerfiles_ItemCommand;
            BtnModificar.Click += BtnModificar_Click;
            TreePermisos.NeedDataSource += TreePermisos_NeedDataSource;
            TreePermisos.ItemDataBound += TreePermisos_ItemDataBound;
            TreePermisos.ChildItemsDataBind += TreePermisos_ChildItemsDataBind;
            btnNuevo.Click += btnNuevo_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(27, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 27);
                TxtEditar.Text = dsPermisos.Tables["Datos"].Rows[0]["Editar"].ToString();
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    BtnModificar.Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    btnNuevo.Visible = false;
                    BtnModificar.Visible = false;
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
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoAmbito(), CboAmbito, "AmbitoId", "Ambito");
                ClUtilitarios.AgregarSeleccioneCombo(CboAmbito, "Nivel");
            }

        }

        void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();    
        }


        protected void ChkConsultar_CheckedChanged(object sender,EventArgs e)
        {
            CheckBox ChkConsultar = (CheckBox)sender;
            TreeListDataItem item = (TreeListDataItem)ChkConsultar.Parent.Parent;
            TableCell cell = item["FormaId"];
            int Permiso = 0;
            if (ChkConsultar.Checked == true)
                Permiso = 1;
            else
                Permiso = 0;
            ClPerfil.Actualiza_Rol(Convert.ToInt32(TxtTipo_UsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 1);
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
            ClPerfil.Actualiza_Rol(Convert.ToInt32(TxtTipo_UsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 2);
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
            ClPerfil.Actualiza_Rol(Convert.ToInt32(TxtTipo_UsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 3);
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
            ClPerfil.Actualiza_Rol(Convert.ToInt32(TxtTipo_UsuarioId.Text), Convert.ToInt32(cell.Text), Permiso, 4);
        }

        void TreePermisos_ChildItemsDataBind(object sender, TreeListChildItemsDataBindEventArgs e)
        {
            int PadreId = Convert.ToInt32(e.ParentDataKeyValues["FormaId"].ToString());
            e.ChildItemsDataSource = ClPerfil.Roles_Perfil(Convert.ToInt32(TxtTipo_UsuarioId.Text), PadreId, 0);
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

        void TreePermisos_NeedDataSource(object sender, Telerik.Web.UI.TreeListNeedDataSourceEventArgs e)
        {
            if (TxtTipo_UsuarioId.Text != "")
                ClUtilitarios.LlenaRadTree(ClPerfil.Roles_Perfil(Convert.ToInt32(TxtTipo_UsuarioId.Text), 0, 1), TreePermisos);
        }

        void BtnModificar_Click(object sender, EventArgs e)
        {
            if (TxtTipo_UsuarioId.Text != "")
            {
                BtnMensajeMod.Visible = false;
                ClPerfil.Actualiza_Perfil(Convert.ToInt32(TxtTipo_UsuarioId.Text), Convert.ToInt32(CboAmbito.SelectedValue), TxtPerfil.Text);
                BtnMensajeMod.Visible = true;
                LblMensajeMod.Text = "Perfil modificado exitosamente";
                GrdPerfiles.Rebind();
                Limpiar();
            }
            else
            {
                if (Valida() == true)
                {
                    ClPerfil.Insert_Perfil(TxtPerfil.Text, Convert.ToInt32(CboAmbito.SelectedValue));
                    Limpiar();
                    BtnMensajeMod.Visible = true;
                    LblMensajeMod.Text = "Perfil Creado exitosamente";
                    GrdPerfiles.Rebind();
                }
            }
        }

        bool Valida()
        {
            bool HayError = false;
            DivError.Visible = false;
            LblErorr.Text = "";

            if (TxtPerfil.Text == "")
            {
                if (LblErorr.Text == "")
                    LblErorr.Text = LblErorr.Text + "Debe ingresar un nombre de perfil";
                else
                    LblErorr.Text = LblErorr.Text + ", Debe ingresar un nombre de perfil";
                HayError = true;
            }
            if (ClPerfil.Existe_Perfil(TxtPerfil.Text) == true)
            {
                if (LblErorr.Text == "")
                    LblErorr.Text = LblErorr.Text + "Nombre de perfil ya existe";
                else
                    LblErorr.Text = LblErorr.Text + ", Nombre de perfil ya existe";
                HayError = true;
            }
            if ((CboAmbito.SelectedValue == "") || (CboAmbito.SelectedValue == "0"))
            {
                if (LblErorr.Text == "")
                    LblErorr.Text = LblErorr.Text + "Debe seleccionar el nivel";
                else
                    LblErorr.Text = LblErorr.Text + ", Debe seleccionar el nivel";
                HayError = true;
            }
            if (HayError == true)
            {
                DivError.Visible = true;
                return false;
            }

            else
                return true;

        }

        void GrdPerfiles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEdit")
            {
                TxtTipo_UsuarioId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Tipo_UsuarioId"].ToString();
                TxtPerfil.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Tipo_Usuario"].ToString();
                CboAmbito.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Ambito"].ToString();
                CboAmbito.SelectedValue = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AmbitoId"].ToString();
                TxtTipo_UsuarioId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Tipo_UsuarioId"].ToString();
                LblPerfil.Text = "Roles: " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Tipo_Usuario"].ToString();
                TreePermisos.Rebind();
                TreePermisos.Visible = true;
                DivError.Visible = false;
                BtnMensajeMod.Visible = false;
            }
        }

        void GrdPerfiles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClPerfil.Perfiles_Ambito_GetAll(), GrdPerfiles);
        }

        void Limpiar()
        {
            TxtPerfil.Text = "";
            TxtTipo_UsuarioId.Text = "";
            CboAmbito.Text = "";
            CboAmbito.SelectedValue = "0";
            TreePermisos.Visible = false;
            LblPerfil.Text = "";
        }


    }
}