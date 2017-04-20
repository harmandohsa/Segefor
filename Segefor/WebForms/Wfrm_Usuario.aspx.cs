using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using SEGEFOR.Data_Set;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Usuario : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatagos;
        Cl_Regiones ClRegiones;
        Ds_Temporales Ds_Temporal = new Ds_Temporales();



        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatagos = new Cl_Catalogos();
            ClRegiones = new Cl_Regiones();
            CboEmpleado.SelectedIndexChanged += CboEmpleado_SelectedIndexChanged;
            BtnBuscar.ServerClick += BtnBuscar_ServerClick;
            CboRegion.SelectedIndexChanged += CboRegion_SelectedIndexChanged;
            GrdRel_Region.NeedDataSource += GrdRel_Region_NeedDataSource;
            btnAddRegion.ServerClick += btnAddRegion_ServerClick;
            GrdRel_Region.ItemCommand += GrdRel_Region_ItemCommand;
            GrdModulos.NeedDataSource += GrdModulos_NeedDataSource;
            CboPerfil.SelectedIndexChanged += CboPerfil_SelectedIndexChanged;
            BtnGrabar.Click += BtnGrabar_Click;
            GrdUsuarios.NeedDataSource += GrdUsuarios_NeedDataSource;
            GrdUsuarios.ItemDataBound += GrdUsuarios_ItemDataBound;
            GrdUsuarios.ItemCommand += GrdUsuarios_ItemCommand;
            btnNuevo.ServerClick += btnNuevo_ServerClick;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(28, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 28);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    GrdUsuarios.Columns[6].Visible = false;
                    GrdUsuarios.Columns[7].Visible = false;
                    GrdUsuarios.Columns[8].Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    btnNuevo.Visible = false;
                    BtnGrabar.Visible = false;
                }
                if ((Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0) && (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 1))
                {
                    btnNuevo.Visible = false;
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

                ClUtilitarios.LlenaCombo(ClCatagos.ListadoContratacion(), CboTipoContratacion, "Tipo_ContratacionId", "Tipo_Contratacion");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoContratacion, "Tipo de Usuario");
                ClUtilitarios.LlenaCombo(ClCatagos.ListadoEmpleadosINAB(), CboEmpleado, "CodEmpl", "nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboEmpleado, "Empleado");
                ClUtilitarios.LlenaCombo(ClCatagos.ListadoRegion(), CboRegion, "RegionId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboRegion, "Región");
                ClUtilitarios.AgregarSeleccioneCombo(CboSubregion, "SubRegión");
                //ClUtilitarios.LlenaCombo(ClCatagos.ListadoSubRegion(1), CboSubregion, "SubRegionId", "Nombre");
                ClUtilitarios.LlenaCombo(ClCatagos.ListadoPerfiles(), CboPerfil, "Tipo_UsuarioId", "Tipo_Usuario");
                ClUtilitarios.AgregarSeleccioneCombo(CboPerfil, "Perfil");
                CboTipoContratacion.SelectedValue = "0";
                CboPerfil.SelectedValue = "0";
            }

        }

        void btnNuevo_ServerClick(object sender, EventArgs e)
        {
            Limpiar();
            TxtDpi.Enabled = true;
            CboEmpleado.Enabled = true;
            DivErrGrabar.Visible = false;
            DivErrReg.Visible = false;
            DivGoodGrabar.Visible = false;
            CboTipoContratacion.Enabled = true;
        }

        void GrdUsuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DivGoodGrabar.Visible = false;
            LblGoodGrabar.Text = "";
            if (e.CommandName == "CmdPass")
            {
                string Clave = ClUtilitarios.GenerarPass(6, 10);
                ClUsuario.Actualiza_Clave(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]), ClUtilitarios.Encrypt(Clave, true), 1);
                DivGoodGrabar.Visible = true;
                LblGoodGrabar.Text = "Contraseña actualizada";
                DataSet dsDatos = ClUsuario.Datos_Usuario(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Usuario"].ToString());
                string Nombre = ClPersona.Nombre_Usuario(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]));
                string Asunto = "Notificacion de reseteo de Clave";
                string Mensaje = Mensaje = "<body><table><tr><td>Le informamos que se ha reestablecido su contraseña para poder acceder al Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + dsDatos.Tables["DATOS"].Rows[0]["Usuario"] + ", la contraseña: " + Clave + "</td></tr></table>";
                ClUtilitarios.EnvioCorreo(dsDatos.Tables["DATOS"].Rows[0]["Correo"].ToString(), Nombre, Asunto, Mensaje, 0, "", "");
               
            }
            if (e.CommandName == "CmdAct")
            {
                ClUsuario.Cambio_Estatus_Usuario(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]), 1);
                GrdUsuarios.Rebind();
                DivGoodGrabar.Visible = true;
                LblGoodGrabar.Text = "Estatus actualizado";
            }
            if (e.CommandName == "CmdDes")
            {
                ClUsuario.Cambio_Estatus_Usuario(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]), 2);
                GrdUsuarios.Rebind();
                DivGoodGrabar.Visible = true;
                LblGoodGrabar.Text = "Estatus actualizado";
            }
            if (e.CommandName == "CmdEditar")
            {
                CboTipoContratacion.Enabled = false;
                TxtDpi.Enabled = false;
                CboEmpleado.Enabled = false;
                Limpiar();
                TxtUsuarioId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString();
                DataSet dsUsuario = ClUsuario.Datos_UsuarioId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]));
                int PersonaId = Convert.ToInt32(dsUsuario.Tables["Datos"].Rows[0]["PersonaId"]);
                dsUsuario.Clear();
                DataSet ds = ClPersona.Datos_Persona(PersonaId);
                TxtNombre.Text = ds.Tables["DATOS"].Rows[0]["Nombres"].ToString();
                TxtApellidos.Text = ds.Tables["DATOS"].Rows[0]["Apellidos"].ToString();
                TxtPuesto.Text = ds.Tables["DATOS"].Rows[0]["nombre"].ToString();
                TxtUsuario.Text = ds.Tables["DATOS"].Rows[0]["usuario"].ToString();
                TxtUsuarioAntes.Text = TxtUsuario.Text;
                TxtCorreo.Text = ds.Tables["DATOS"].Rows[0]["correo"].ToString();
                TxtCorreoAntes.Text = TxtCorreo.Text;
                CboPerfil.SelectedValue = ds.Tables["DATOS"].Rows[0]["Tipo_UsuarioId"].ToString();
                TxtPerfilId.Text = CboPerfil.SelectedValue;
                CboPerfil.Text = ds.Tables["DATOS"].Rows[0]["Tipo_Usuario"].ToString();
                CboTipoContratacion.SelectedValue = ds.Tables["DATOS"].Rows[0]["Tipo_ContratacionId"].ToString();
                CboTipoContratacion.Text = ds.Tables["DATOS"].Rows[0]["Tipo_Contratacion"].ToString();
                ds.Clear();
                DataSet dsRegiones = ClUsuario.Get_SubRegion_Usuario(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]));
                for (int i = 0; i < dsRegiones.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow row = Ds_Temporal.Tables["DtSubregion"].NewRow();
                    row["RegionId"] = dsRegiones.Tables["Datos"].Rows[i]["RegionId"];
                    row["RegionNombre"] = dsRegiones.Tables["Datos"].Rows[i]["Region"];
                    row["SubRegionId"] = dsRegiones.Tables["Datos"].Rows[i]["SubRegionId"];
                    row["SubRegionNombre"] = dsRegiones.Tables["Datos"].Rows[i]["SubRegion"];
                    Ds_Temporal.Tables["DtSubregion"].Rows.Add(row);  
                }
                dsRegiones.Clear();
                GrdRel_Region.Rebind();
                DataSet DsModulos = ClUsuario.Get_Modulo_Usuario(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]));
                for (int i = 0; i < DsModulos.Tables["Datos"].Rows.Count; i++)
                {
                    for (int j = 0; j < GrdModulos.Items.Count; j++)
                    {
                        if (GrdModulos.Items[j].GetDataKeyValue("ModuloId").ToString() == DsModulos.Tables["Datos"].Rows[i]["ModuloId"].ToString())
                        {
                            CheckBox Modulo;
                            Modulo = (CheckBox)GrdModulos.Items[j].FindControl("ChkModulo");
                            Modulo.Checked = true;
                            break;
                        }
                    }
                }
                DsModulos.Clear();
            }
        }

        void GrdUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("Estatus_UsrId").ToString() == "1")
                {
                    ImageButton ImgAct;
                    ImgAct = (ImageButton)item.FindControl("ImgAct");
                    ImgAct.Visible = false;
                }
                else
                {
                    ImageButton ImgDes;
                    ImgDes = (ImageButton)item.FindControl("ImgDes");
                    ImgDes.Visible = false;
                }

            }
        }

        void GrdUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClUsuario.Get_Usuarios(0), GrdUsuarios);
        }

        void BtnGrabar_Click(object sender, EventArgs e)
        {
            DivErrGrabar.Visible = false;
            LblErrGrabar.Text = "";
            DivGoodGrabar.Visible = false;
            LblGoodGrabar.Text = "";
            if (TxtUsuarioId.Text != "")
            {
                if (ValidaDatosMod() == true)
                {
                    ClUsuario.Actualiza_DatosUsuario(Convert.ToInt32(TxtUsuarioId.Text), TxtUsuario.Text, TxtCorreo.Text, Convert.ToInt32(CboPerfil.SelectedValue), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", "01/01/2000")),0,0);
                    ClUsuario.Elimina_Modulo_Usuario(Convert.ToInt32(TxtUsuarioId.Text));
                    ClUsuario.Elimina_SubRegion_Usuario(Convert.ToInt32(TxtUsuarioId.Text));
                    for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                    {
                        ClUsuario.Insert_Usuario_Subregion(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionId"]));
                    }
                    for (int i = 0; i < GrdModulos.Items.Count; i++)
                    {
                        CheckBox Modulo;
                        Modulo = (CheckBox)GrdModulos.Items[i].FindControl("ChkModulo");
                        if (Modulo.Checked == true)
                        {
                            ClUsuario.Insert_Usuario_Modulo(Convert.ToInt32(TxtUsuarioId.Text), Convert.ToInt32(GrdModulos.Items[i].OwnerTableView.DataKeyValues[i]["ModuloId"]));
                        }

                    }
                    DivGoodGrabar.Visible = true;
                    LblGoodGrabar.Text = "Usuario Modificado";
                    
                    if ((CboPerfil.SelectedValue == "10") || (CboPerfil.SelectedValue == "11"))
                    {
                        if (CboPerfil.SelectedValue == "10")
                        {
                            for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                            {
                                ClRegiones.Actualiza_Regional(Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["RegionId"]), ClPersona.GetPersonaId(Convert.ToInt32(TxtUsuarioId.Text)));
                            }
                        }
                        else if (CboPerfil.SelectedValue == "11")
                        {
                            for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                            {
                                ClRegiones.SP_Actualiza_SubRegional(Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionId"]), ClPersona.GetPersonaId(Convert.ToInt32(TxtUsuarioId.Text)));
                            }
                        }
                    }
                    Limpiar();
                    GrdUsuarios.Rebind();
                }
                else
                    DivErrGrabar.Visible = true;
            }
            else
            {
                if (ValidaDatos() == true)
                {
                    int UsuarioId = ClUsuario.UsurioId();
                    int PersonaId = ClPersona.MaxPersonaId();
                    string Clave = ClUtilitarios.Encrypt(ClUtilitarios.GenerarPass(6, 10), true);
                    ClPersona.Insertar_Persona(PersonaId, TxtNombre.Text, TxtApellidos.Text, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", "01/01/2000")),Convert.ToInt32(TxtGeneroID.Text), TxtDpi.Text.Replace("-", ""), "", "", 0, 1, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", "01/01/2000")),0);
                    ClPersona.Insertar_DatosPersona_INAB(PersonaId, Convert.ToInt32(TxtCodPuesto.Text));
                    ClUsuario.Insertar_Usuario(UsuarioId, TxtUsuario.Text, Convert.ToInt32(CboPerfil.SelectedValue), Clave, PersonaId, Convert.ToInt32(CboTipoContratacion.SelectedValue), Convert.ToInt32(Session["UsuarioId"]), TxtCorreo.Text);
                    ClUsuario.Insertar_Permisos(UsuarioId, Convert.ToInt32(CboPerfil.SelectedValue));
                    for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                    {
                        ClUsuario.Insert_Usuario_Subregion(UsuarioId, Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionId"]));
                    }
                    for (int i = 0; i < GrdModulos.Items.Count; i++)
                    {
                        CheckBox Modulo;
                        Modulo = (CheckBox)GrdModulos.Items[i].FindControl("ChkModulo");
                        if (Modulo.Checked == true)
                        {
                            ClUsuario.Insert_Usuario_Modulo(UsuarioId, Convert.ToInt32(GrdModulos.Items[i].OwnerTableView.DataKeyValues[i]["ModuloId"]));
                        }

                    }
                    string Asunto = "Notificacion de creación de Usuario";
                    string Mensaje = "<body><table><tr><td>Le informamos que se ha creado su usuario para poder acceder al sistema: Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + TxtUsuario.Text + ", la contraseña: " + ClUtilitarios.Decrypt(Clave, true) + "</td></tr></table>";
                    ClUtilitarios.EnvioCorreo(TxtCorreo.Text, TxtNombre.Text + ' ' + TxtApellidos.Text, Asunto, Mensaje, 0, "", "");
                    DivGoodGrabar.Visible = true;
                    LblGoodGrabar.Text = "Usuario Agregado";
                    
                    
                    if ((CboPerfil.SelectedValue == "10") || (CboPerfil.SelectedValue == "11"))
                    {
                        if (CboPerfil.SelectedValue == "10")
                        {
                            for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                            {
                                ClRegiones.Actualiza_Regional(Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["RegionId"]), PersonaId);
                            }
                        }
                        else if (CboPerfil.SelectedValue == "11")
                        {
                            for (int i = 0; i < GrdRel_Region.Items.Count; i++)
                            {
                                ClRegiones.SP_Actualiza_SubRegional(Convert.ToInt32(GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionId"]), PersonaId);
                            }
                        }
                    }
                    Limpiar();
                    GrdUsuarios.Rebind();
                }
                else
                    DivErrGrabar.Visible = true;
            }
                
            
        }

        void CboPerfil_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            AplicoSubRegionPerfil();
        }

        void GrdModulos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClCatagos.ListadoModulos(), GrdModulos);
        }

        void GrdRel_Region_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                CargaDataSet();
                for (int i = 0; i < Ds_Temporal.Tables["DtSubregion"].Rows.Count; i++)
                {
                    if ((e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegionId"].ToString() == Ds_Temporal.Tables["DtSubRegion"].Rows[i]["RegionId"].ToString()) && (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubRegionId"].ToString() == Ds_Temporal.Tables["DtSubRegion"].Rows[i]["SubRegionId"].ToString()))
                    {
                        Ds_Temporal.Tables["DtSubRegion"].Rows[i].Delete();
                    }
                }
                GrdRel_Region.Rebind();
            }
        }

        void CargaDataSet()
        {
            for (int i = 0; i < GrdRel_Region.Items.Count;i++)
            {
                DataRow row = Ds_Temporal.Tables["DtSubregion"].NewRow();
                row["RegionId"] = GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["RegionId"];
                row["RegionNombre"] = GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["RegionNombre"];
                row["SubRegionId"] = GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionId"];
                row["SubRegionNombre"] = GrdRel_Region.Items[i].OwnerTableView.DataKeyValues[i]["SubRegionNombre"];
                Ds_Temporal.Tables["DtSubregion"].Rows.Add(row);      
            }
        }

        void btnAddRegion_ServerClick(object sender, EventArgs e)
        {
            DivErrReg.Visible = false;
            LblRegionError.Text = "";
            if (ExisteSubRegion(Convert.ToInt32(CboSubregion.SelectedValue)) != true)
            {
                AgregarSubRegion(Convert.ToInt32(CboRegion.SelectedValue), CboRegion.Text, Convert.ToInt32(CboSubregion.SelectedValue), CboSubregion.Text);
                GrdRel_Region.Rebind();
            }
            else
            {
                DivErrReg.Visible = true;
                LblRegionError.Text = "Ya Agrego esta región";
            }
            
        }

        void GrdRel_Region_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["DtSubregion"].Rows.Count > 0)
                ClUtilitarios.LlenaGrid(Ds_Temporal, GrdRel_Region);
        }

        void CboRegion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboRegion.SelectedValue != "0")
            {
                ClUtilitarios.LlenaCombo(ClCatagos.ListadoSubRegion(Convert.ToInt32(CboRegion.SelectedValue)), CboSubregion, "SubRegionId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboSubregion, "SubRegión");
            }
                
        }

        void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            BtnErrorDpi.Visible = false;
            if (TxtDpi.Text.Replace("-","")== "")
            {
                BtnErrorDpi.Visible = true;
                LblMensajeErrorDpi.Text = "ingrese el número de DPI";
            }
            else
            {
                DataSet ds = ClUsuario.Get_NombreEmplINAB(Convert.ToInt64(TxtDpi.Text.Replace("-", "")));
                if (ds.Tables["Datos"].Rows.Count > 0)
                {
                    CboEmpleado.SelectedValue = ds.Tables["Datos"].Rows[0]["CodEmpl"].ToString();
                    CboEmpleado.Text = ds.Tables["Datos"].Rows[0]["Nombre"].ToString();
                    CargaDatosEmpl(Convert.ToInt32(ds.Tables["Datos"].Rows[0]["CodEmpl"].ToString()));
                }
                else
                {
                    BtnErrorDpi.Visible = true;
                    LblMensajeErrorDpi.Text = "El Dpi no existe";
                    Limpiar();
                    CboEmpleado.Text = "";
                }
                ds.Tables.Clear();
            }
        }

        void Limpiar()
        {
            TxtDpi.Text = "";
            CboEmpleado.Text = "";
            CboEmpleado.SelectedValue = "0";
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
            TxtPuesto.Text = "";
            TxtCodEmpl.Text = "";
            txtDpitrue.Text = "";
            CboTipoContratacion.SelectedValue = "0";
            TxtUsuario.Text = "";
            TxtCorreo.Text = "";
            Ds_Temporal.Tables["DtSubregion"].Clear();
            GrdRel_Region.Rebind();
            CboPerfil.SelectedValue = "0";
            for (int i = 0; i < GrdModulos.Items.Count; i++)
            {
                CheckBox Modulo;
                Modulo = (CheckBox)GrdModulos.Items[i].FindControl("ChkModulo");
                Modulo.Checked = false;

            }
            TxtUsuarioId.Text = "";
            CboSubregion.SelectedValue = "0";
            CboRegion.SelectedValue = "0";
            

        }

        bool ExisteSubRegion(int SubRegionId)
        {
            CargaDataSet();
            bool Existe = false;
            for (int i = 0; i < Ds_Temporal.Tables["DtSubregion"].Rows.Count; i++)
            {
                if (Convert.ToInt32(Ds_Temporal.Tables["DtSubregion"].Rows[i]["SubRegionId"]) == SubRegionId)
                {
                    Existe = true;
                    break;
                }
            }
            if (Existe == true)
                return true;
            else
                return false;
        }

        void AgregarSubRegion(int RegionId, string Region, int SubRegionId, string SubRegion)
        {
            DataRow row = Ds_Temporal.Tables["DtSubregion"].NewRow();
            row["RegionId"] = RegionId;
            row["RegionNombre"] = Region;
            row["SubRegionId"] = SubRegionId;
            row["SubRegionNombre"] = SubRegion;
            Ds_Temporal.Tables["DtSubregion"].Rows.Add(row);
        }

        void CargaDatosEmpl(int CodEmpl)
        {
            DataSet ds = ClUsuario.Get_DatosEmplINAB(CodEmpl);
            if (ds.Tables["Datos"].Rows.Count > 0)
            {
                TxtNombre.Text = ds.Tables["Datos"].Rows[0]["Nombres"].ToString();
                TxtApellidos.Text = ds.Tables["Datos"].Rows[0]["Apellidos"].ToString();
                TxtPuesto.Text = ds.Tables["Datos"].Rows[0]["Puesto"].ToString();
                TxtCodEmpl.Text = ds.Tables["Datos"].Rows[0]["CodEmpl"].ToString();
                txtDpitrue.Text = ds.Tables["Datos"].Rows[0]["Dpi"].ToString();
                if (ds.Tables["Datos"].Rows[0]["Genero"].ToString() == "Masculino")
                    TxtGeneroID.Text = "1";
                else
                    TxtGeneroID.Text = "2";
                int CodigoPuesto = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["CodigoPuesto"]);
                TxtCodPuesto.Text = CodigoPuesto.ToString();
                int CodSubregion = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["CodSubregion"]);
                DataSet SubRegion = ClUsuario.Get_Datos_Traduce_region(CodSubregion);
                
                if (SubRegion.Tables["Datos"].Rows.Count > 0)
                {
                    CboRegion.SelectedValue = SubRegion.Tables["Datos"].Rows[0]["RegionId"].ToString();
                    CboRegion.Text = SubRegion.Tables["Datos"].Rows[0]["Region"].ToString();
                    ClUtilitarios.LlenaCombo(ClCatagos.ListadoSubRegion(Convert.ToInt32(CboRegion.SelectedValue)), CboSubregion, "SubRegionId", "Nombre");
                    ClUtilitarios.AgregarSeleccioneCombo(CboSubregion, "SubRegión");
                    CboSubregion.SelectedValue = SubRegion.Tables["Datos"].Rows[0]["SubRegionId"].ToString();
                    CboSubregion.Text = SubRegion.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                    AgregarSubRegion(Convert.ToInt32(CboRegion.SelectedValue),CboRegion.Text,Convert.ToInt32(CboSubregion.SelectedValue),CboSubregion.Text);
                    GrdRel_Region.Rebind();
                }
                DataSet Perfil = ClUsuario.Get_Datos_Traduce_Puesto_Perfil(CodigoPuesto);
                if (Perfil.Tables["Datos"].Rows.Count > 0)
                {
                    CboPerfil.SelectedValue = Perfil.Tables["Datos"].Rows[0]["Tipo_UsuarioId"].ToString();
                    CboPerfil.Text = Perfil.Tables["Datos"].Rows[0]["Tipo_Usuario"].ToString();
                }
                else
                {
                    CboPerfil.SelectedValue = "0";

                }
                SubRegion.Clear();
                Perfil.Clear();
                AplicoSubRegionPerfil();
            }
        }

        void AplicoSubRegionPerfil()
        {
            
            if (CboPerfil.SelectedValue != "")
            {
                Ds_Temporal.Tables["DtSubregion"].Clear();
                if (Convert.ToInt32(CboPerfil.SelectedValue) > 0)
                {
                    int AmbitoId = ClUsuario.Get_Ambito_Perfil(Convert.ToInt32(CboPerfil.SelectedValue));
                    if (AmbitoId == 1)
                    {
                        DataSet Region = ClCatagos.Get_RegionesSubRegiones();
                        for (int i = 0; i < Region.Tables["Datos"].Rows.Count; i++)
                        {
                            AgregarSubRegion(Convert.ToInt32(Region.Tables["Datos"].Rows[i]["RegionId"]), Region.Tables["Datos"].Rows[i]["Region"].ToString(), Convert.ToInt32(Region.Tables["Datos"].Rows[i]["SubRegionId"]), Region.Tables["Datos"].Rows[i]["SubRegion"].ToString());
                        }
                        Region.Clear();
                    }
                    else if (AmbitoId == 2)
                    {
                        DataSet SubRegion = ClCatagos.ListadoSubRegion(Convert.ToInt32(CboRegion.SelectedValue));
                        for (int j = 0; j < SubRegion.Tables["Datos"].Rows.Count; j++)
                        {
                            AgregarSubRegion(Convert.ToInt32(Convert.ToInt32(CboRegion.SelectedValue)),CboRegion.Text, Convert.ToInt32(SubRegion.Tables["Datos"].Rows[j]["SubRegionId"]), SubRegion.Tables["Datos"].Rows[j]["Nombre"].ToString());
                        }
                        SubRegion.Clear();
                    }
                    else
                        AgregarSubRegion(Convert.ToInt32(CboRegion.SelectedValue), CboRegion.Text, Convert.ToInt32(CboSubregion.SelectedValue), CboSubregion.Text);
                    GrdRel_Region.Rebind();
                }
                
            }
        }


        void CboEmpleado_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboEmpleado.SelectedValue != "")
            {
                string Dpi = ClUsuario.Get_DpiEmplINAB(Convert.ToInt32(CboEmpleado.SelectedValue)).ToString();
                TxtDpi.Text = Dpi.Substring(0, 4) + "-" + Dpi.Substring(4, 5) + "-" + Dpi.Substring(9, 4);
                CargaDatosEmpl(Convert.ToInt32(CboEmpleado.SelectedValue));
            }
            else
                Limpiar();
        }

        bool SeleccionoModulo()
        {
            bool Selecciono = false;
            for (int i = 0; i < GrdModulos.Items.Count; i++)
            {
                CheckBox Modulo;
                Modulo = (CheckBox)GrdModulos.Items[i].FindControl("ChkModulo");
                if (Modulo.Checked == true)
                {
                    Selecciono = true;
                    break;
                }

            }
            if (Selecciono == true)
                return true;
            else
                return false;
        }

        bool ValidaDatosMod()
        {
            LblErrGrabar.Text = "";
            bool HayError = false;
            if ((CboTipoContratacion.SelectedValue == "") || (Convert.ToInt32(CboTipoContratacion.SelectedValue) == 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe Seleccionar un tipo de contratación";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe Seleccionar un tipo de contratación";
                HayError = true;
            }
            if ((TxtUsuario.Text != TxtUsuarioAntes.Text) && (ClUsuario.Existe_Usuario(TxtUsuario.Text) == true))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Usuario ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Usuario ya existe";
                HayError = true;
            }
            if (ClUtilitarios.EsInstitucional(TxtCorreo.Text) == false)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Solo puede agregar correos del dominio inab.gob.gt";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Solo puede agregar correos del dominio inab.gob.gt";
                HayError = true;
            }
            if ((TxtCorreo.Text != TxtCorreoAntes.Text) && (ClUsuario.Existe_Correo(TxtCorreo.Text) == true))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Correo ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Correo ya existe";
                HayError = true;
            }
            if (GrdRel_Region.Items.Count == 0)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe Agregar al menos una subregión";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe Agregar al menos una subregión";
                HayError = true;
            }
            if ((CboPerfil.SelectedValue == "") || (Convert.ToInt32(CboPerfil.SelectedValue) == 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe seleccionar el perfil";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe seleccionar el perfil";
                HayError = true;
            }
            if (SeleccionoModulo() == false)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe seleccionar al menos un módulo";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe seleccionar al menos un módulo";
                HayError = true;
            }
            if (CboPerfil.SelectedValue != TxtPerfilId.Text)
            {
                if (Convert.ToInt32(CboPerfil.SelectedValue) == 10)
                {
                    int SubRegion = Convert.ToInt32(GrdRel_Region.Items[0].GetDataKeyValue("SubRegionId"));
                    if (ClUsuario.Existe_Usuario_Region_SubRegion(SubRegion, 10) != "")
                    {
                        if (LblErrGrabar.Text == "")
                            LblErrGrabar.Text = LblErrGrabar.Text + "Ya existe un usuario con este perfil para esta región";
                        else
                            LblErrGrabar.Text = LblErrGrabar.Text + ", Ya existe un usuario con este perfil para esta región";
                        HayError = true;
                    }
                }
                if (Convert.ToInt32(CboPerfil.SelectedValue) == 11)
                {
                    int SubRegion = Convert.ToInt32(GrdRel_Region.Items[0].GetDataKeyValue("SubRegionId"));
                    if (ClUsuario.Existe_Usuario_Region_SubRegion(SubRegion, 11) != "")
                    {
                        if (LblErrGrabar.Text == "")
                            LblErrGrabar.Text = LblErrGrabar.Text + "Ya existe un usuario con este perfil para esta subregión";
                        else
                            LblErrGrabar.Text = LblErrGrabar.Text + ", Ya existe un usuario con este perfil para esta subregión";
                        HayError = true;
                    }
                }
            }
            if (HayError == true)
                return false;
            else
                return true;
        }

        bool ValidaDatos()
        {
            LblErrGrabar.Text = "";
            bool HayError = false;
            if ((CboTipoContratacion.SelectedValue == "") || (Convert.ToInt32(CboTipoContratacion.SelectedValue) == 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe Seleccionar tipo de Usuario";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe Seleccionar tipo de Usuario";
                HayError = true;
            }
            if (TxtNombre.Text == "")
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe Seleccionar un empleado";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe Seleccionar un empleado";
                HayError = true;
            }
            if (ClUsuario.Existe_Usuario(TxtUsuario.Text) == true)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Usuario ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Usuario ya existe";
                HayError = true;
            }
            if (ClUtilitarios.EsInstitucional(TxtCorreo.Text) == false)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Solo puede agregar correos del dominio inab.gob.gt";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Solo puede agregar correos del dominio inab.gob.gt";
                HayError = true;
            }
            if (ClUsuario.Existe_Correo(TxtCorreo.Text) == true)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Correo ya existe";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Correo ya existe";
                HayError = true;
            }
            if (GrdRel_Region.Items.Count == 0)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe Agregar al menos una subregión";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe Agregar al menos una subregión";
                HayError = true;
            }
            if ((CboPerfil.SelectedValue == "") || (Convert.ToInt32(CboPerfil.SelectedValue) == 0))
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Seleccione Perfil";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Seleccione Perfil";
                HayError = true;
            }
            if (SeleccionoModulo() == false)
            {
                if (LblErrGrabar.Text == "")
                    LblErrGrabar.Text = LblErrGrabar.Text + "Debe seleccionar al menos un módulo";
                else
                    LblErrGrabar.Text = LblErrGrabar.Text + ", Debe seleccionar al menos un módulo";
                HayError = true;
            }
            if ((CboPerfil.SelectedValue != "") && (Convert.ToInt32(CboPerfil.SelectedValue) == 10))
            {
                int SubRegion = Convert.ToInt32(GrdRel_Region.Items[0].GetDataKeyValue("SubRegionId"));
                if (ClUsuario.Existe_Usuario_Region_SubRegion(SubRegion,10) != "")
                {
                    if (LblErrGrabar.Text == "")
                        LblErrGrabar.Text = LblErrGrabar.Text + "Ya existe un usuario con este perfil para esta región";
                    else
                        LblErrGrabar.Text = LblErrGrabar.Text + ", Ya existe un usuario con este perfil para esta región";
                    HayError = true;
                }
            }
            if ((CboPerfil.SelectedValue != "") && (Convert.ToInt32(CboPerfil.SelectedValue) == 11))
            {
                int SubRegion = Convert.ToInt32(GrdRel_Region.Items[0].GetDataKeyValue("SubRegionId"));
                if (ClUsuario.Existe_Usuario_Region_SubRegion(SubRegion, 11) != "")
                {
                    if (LblErrGrabar.Text == "")
                        LblErrGrabar.Text = LblErrGrabar.Text + "Ya existe un usuario con este perfil para esta subregión";
                    else
                        LblErrGrabar.Text = LblErrGrabar.Text + ", Ya existe un usuario con este perfil para esta subregión";
                    HayError = true;
                }
            }
            if (HayError == true)
                return false;
            else
                return true;
        }
    }
}