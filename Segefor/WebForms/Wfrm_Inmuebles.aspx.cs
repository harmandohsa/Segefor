using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using Telerik.Web.UI;
using System.IO;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Inmuebles : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Inmueble ClInmueble;
        Cl_Persona_Juridica ClPersonaJuridica;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        DataSet DsPropietarios = new DataSet("Propietarios");
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClInmueble = new Cl_Inmueble();
            ClPersonaJuridica = new Cl_Persona_Juridica();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();

            ChkOtraJur.CheckedChanged += ChkOtraJur_CheckedChanged;
            CboTipoDocumento.TextChanged += CboTipoDocumento_TextChanged;
            OptAreasPro.SelectedIndexChanged += OptAreasPro_SelectedIndexChanged;
            GrdInmuebles.NeedDataSource += GrdInmuebles_NeedDataSource;
            BtnGrabar.Click += BtnGrabar_Click;
            CboDepartamento.TextChanged += CboDepartamento_TextChanged;
            BtnNuevo.ServerClick += BtnNuevo_ServerClick;
            GrdInmuebles.ItemCommand += GrdInmuebles_ItemCommand;
            GrdInmuebles.ItemDataBound += GrdInmuebles_ItemDataBound;
            ChkIngNomFinca.CheckedChanged += ChkIngNomFinca_CheckedChanged;
            ChkRepreseanteyPropietario.CheckedChanged += ChkRepreseanteyPropietario_CheckedChanged;
            ChkRepresentanteVariasPer.CheckedChanged += ChkRepresentanteVariasPer_CheckedChanged;
            BtnValidaPropietario.ServerClick += BtnValidaPropietario_ServerClick;
            GrdPropietarios.NeedDataSource += GrdPropietarios_NeedDataSource;
            BtnAddPropietario.ServerClick += BtnAddPropietario_ServerClick;
            GrdPropietarios.PageIndexChanged += GrdPropietarios_PageIndexChanged;
            GrdPropietarios.ItemCommand += GrdPropietarios_ItemCommand;

            DataTable DtPropietarios = DsPropietarios.Tables.Add("Propietarios");
            DataColumn Existe = DtPropietarios.Columns.Add("Existe", typeof(Boolean));
            DataColumn PersonaId = DtPropietarios.Columns.Add("PersonaId", typeof(Int64));
            DataColumn Dpi = DtPropietarios.Columns.Add("Dpi", typeof(string));
            DataColumn Nombres = DtPropietarios.Columns.Add("Nombres", typeof(string));
            DataColumn Apellidos = DtPropietarios.Columns.Add("Apellidos", typeof(string));
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 16);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    GrdInmuebles.Columns[6].Visible = false;
                    BtnGrabar.Visible = false;
                    
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnGrabar.Visible = false;
                    BtnNuevo.Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Eliminar"]) == 0)
                {
                    GrdInmuebles.Columns[7].Visible = false;
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
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(1), CboMunicipio, "MunicipioId", "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.TipoDoc_Propiedad_GetAll(), CboTipoDocumento, "TipoDoc_PropiedadId", "TipoDocPropiedad");
                ClUtilitarios.LlenaCombo(ClCatalogos.Titulo_GetAll(), CboTitulo, "TituloNotarioId", "TituloNotario");
                ClUtilitarios.LlenaCombo(ClCatalogos.Area_Protegida_GetAll(), CboArea, "AreaProtegidaId", "Nombre");
                ClUtilitarios.LlenaCombo(ClPersonaJuridica.Persona_Juridica_Get(Convert.ToInt32(Session["UsuarioId"])), CboJuridico, "PersonaJuridicaId", "NOMBRE");
                CboTipoDocumento.Items.Insert(0, "");
                TxtFecEmi.SelectedDate = DateTime.Today;
                if (ClPersonaJuridica.Existe_Juridco(Convert.ToInt32(Session["UsuarioId"])) == true)
                    ChkOtraJur.Visible = true;
                TxtNombre.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));
            }
        }

        void GrdPropietarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                string Dpi = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Dpi"].ToString().Trim();
                for (int i = 0; i < GrdPropietarios.Items.Count; i++)
                {
                    if (Dpi != e.Item.OwnerTableView.DataKeyValues[i]["Dpi"].ToString().Trim())
                    {
                        DataRow itemGrid = DsPropietarios.Tables["Propietarios"].NewRow();
                        itemGrid["Existe"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Existe"];
                        itemGrid["PersonaId"] = Convert.ToInt64(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PersonaId"]);
                        itemGrid["Dpi"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"];
                        itemGrid["Nombres"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Nombres"];
                        itemGrid["Apellidos"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Apellidos"];
                        DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
                    }
                }
                GrdPropietarios.Rebind();
            }
            
        }

        bool ExistePropietario(string Dpi)
        {
            bool Existe = false;
            for (int i = 0; i < GrdPropietarios.Items.Count; i++)
            {
                if (Dpi == GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"].ToString().Trim())
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        void GrdPropietarios_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LeeGridPropietarios();
        }

        void BtnAddPropietario_ServerClick(object sender, EventArgs e)
        {
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            LblMansajeBadPropietario.Text = "";
            LblMansajeGoodPropietario.Text = "";
            bool HayError = false;
            if (TxtNombrePropietario.Text == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe ingresar el nombre del propietario";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe ingresar el nombre del propietario";
                HayError = true;
            }
            if (TxtApellidoPropietario.Text == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe ingresar el apellido del propietario";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe ingresar el apellido del propietario";
                HayError = true;
            }
            if (HayError == true)
                DivBadPropietario.Visible = true;
            else
            {
                if (ExistePropietario(TxtDpi.Text) == true)
                {
                    LblMansajeBadPropietario.Text = "Ya Agrego a este propietario";
                    DivBadPropietario.Visible = true;
                }
                else
                {
                    LeeGridPropietarios();
                    DataRow item = DsPropietarios.Tables["Propietarios"].NewRow();
                    item["Existe"] = 0;
                    item["PersonaId"] = 0;
                    item["Dpi"] = TxtDpi.Text;
                    item["Nombres"] = TxtNombrePropietario.Text;
                    item["Apellidos"] = TxtApellidoPropietario.Text;
                    DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                    DivGoodPropietario.Visible = true;
                    LblMansajeGoodPropietario.Text = "Propietario Agregado Exitosamente";
                    GrdPropietarios.Rebind();
                    LimiarPropietario();
                    DivNombresProp.Visible = false;
                    DivApeProp.Visible = false;
                    DivAddProp.Visible = false;
                }
                
            }
        }

        void GrdPropietarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TxtInmuebleId.Text == "")
                ClUtilitarios.LlenaGridDataSet(DsPropietarios, GrdPropietarios, "Propietarios");
            else
                ClUtilitarios.LlenaGrid(ClInmueble.Propietarios_GetAll(Convert.ToInt32(TxtInmuebleId.Text)), GrdPropietarios);
                
        }

        void LimiarPropietario()
        {
            TxtDpi.Text = "";
            TxtNombrePropietario.Text = "";
            TxtApellidoPropietario.Text = "";
        }

        void LeeGridPropietarios()
        {
            
            GrdPropietarios.AllowPaging = false;
            for (int i = 0; i < GrdPropietarios.Items.Count; i++)
            {
                DataRow itemGrid = DsPropietarios.Tables["Propietarios"].NewRow();
                itemGrid["Existe"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Existe"];
                itemGrid["PersonaId"] = Convert.ToInt64(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PersonaId"]);
                itemGrid["Dpi"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"];
                itemGrid["Nombres"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Nombres"];
                itemGrid["Apellidos"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Apellidos"];
                DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
            }
        }

        void BtnValidaPropietario_ServerClick(object sender, EventArgs e)
        {
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            LblMansajeBadPropietario.Text = "";
            LblMansajeGoodPropietario.Text = "";
            if (TxtDpi.Text == "")
            {
                DivBadPropietario.Visible = true;
                LblMansajeBadPropietario.Text = "Debe ingresar el número de DPI";
            }
            else
            {
                if (TxtDpi.Text.Length < 13)
                {
                    DivBadPropietario.Visible = true;
                    LblMansajeBadPropietario.Text = "El número de DPI esta incompleto";
                }
                else
                {
                    DataSet DatosPersona = new DataSet();
                    DatosPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                    if (TxtDpi.Text.Trim().Replace("-","") == DatosPersona.Tables["DATOS"].Rows[0]["Dpi"].ToString().Trim())
                    {
                        LblMansajeBadPropietario.Text = "No puede agregarse usted mismo";
                        DivBadPropietario.Visible = true;
                    }
                    else
                    {
                        if (ExistePropietario(TxtDpi.Text) == true)
                        {
                            LblMansajeBadPropietario.Text = "Ya Agrego a este propietario";
                            DivBadPropietario.Visible = true;
                        }
                        else
                        {
                            if (ClPersona.Existe_Dpi(TxtDpi.Text.Replace("-", ""), 1) == true)
                            {
                                LeeGridPropietarios();
                                DataSet dsDatos = new DataSet();
                                dsDatos = ClPersona.Datos_Persona_Dpi(TxtDpi.Text.Replace("-", ""),1);
                                DataRow item = DsPropietarios.Tables["Propietarios"].NewRow();
                                item["Existe"] = 1;
                                item["PersonaId"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                                item["Dpi"] = TxtDpi.Text;
                                item["Nombres"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                                item["Apellidos"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                                DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                                DivGoodPropietario.Visible = true;
                                LblMansajeGoodPropietario.Text = "Propietario Agregado Exitosamente";
                                GrdPropietarios.Rebind();
                                LimiarPropietario();
                            }
                            else
                            {
                                DivNombresProp.Visible = true;
                                DivApeProp.Visible = true;
                                DivAddProp.Visible = true;
                                DivBadPropietario.Visible = true;
                                LblMansajeBadPropietario.Text = "El núemero de DPI no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                            }
                        }
                    }
                }
            }
        }

        void ChkRepresentanteVariasPer_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkRepresentanteVariasPer.Checked == true)
                ChkRepreseanteyPropietario.Checked = false;
            MuestraAddPropietario();
        }

        void MuestraAddPropietario()
        {
            if ((ChkRepreseanteyPropietario.Checked == true) || (ChkRepresentanteVariasPer.Checked == true))
            {
                DivPropietario.Visible = true;
                DivAddPropietarioMensaje.Visible = true;
                DivGrigPropietarios.Visible = true;
                DivAddPropietario.Visible = true;
            }
            else
            {
                DivPropietario.Visible = false;
                DivAddPropietarioMensaje.Visible = false;
                DivGrigPropietarios.Visible = false;
                DivAddPropietario.Visible = false;
            }
        }

        void ChkRepreseanteyPropietario_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkRepreseanteyPropietario.Checked == true)
                ChkRepresentanteVariasPer.Checked = false;
            MuestraAddPropietario();
        }

        void ChkIngNomFinca_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIngNomFinca.Checked == true)
            {
                TxtFinca.Enabled = true;
                TxtFinca.Text = "";
            }
            else
            {
                TxtFinca.Enabled = false;
                TxtFinca.Text = "SIN NOMBRE";
            }
        }

        void GrdInmuebles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                dataItem["Gestiones"].Text = "0";
                if (ClInmueble.Existe_Archivo(Convert.ToInt32(dataItem.GetDataKeyValue("InmuebleId"))) == true)
                    dataItem["Vista"].FindControl("ImgPreview").Visible = true;
                else
                    dataItem["Vista"].FindControl("ImgPreview").Visible = false;
                var imgBtn = e.Item.FindControl("ImgPreview") as ImageButton;
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(imgBtn);

            }
        }

        void GrdInmuebles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdPreview")
            {
                DataSet DsArchivo = new DataSet();
                DsArchivo = ClInmueble.Archivo_Inmueble(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                byte[] bytes;
                string fileName, contentType;
                bytes = (byte[])DsArchivo.Tables["DATOS"].Rows[0]["Archivo_Propiedad"];
                contentType = DsArchivo.Tables["DATOS"].Rows[0]["ContenyType"].ToString();
                fileName = DsArchivo.Tables["DATOS"].Rows[0]["Nombre_Archivo"].ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                
                
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.AddHeader("content-length", (bytes.Length - 1).ToString());
                Response.BinaryWrite(bytes.ToArray());
                Response.Flush();
                Response.End();
            }
            else if (e.CommandName == "CmdEdit")
            {
                DataSet ds = new DataSet();
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                ds = ClInmueble.Inmueble_Get(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                if (ds.Tables["DATOS"].Rows[0]["PersonaJuridicaId"].ToString() == "")
                {
                    ChkOtraJur.Checked = false;
                    DivJuridico.Visible = false;
                }
                else
                {
                    ChkOtraJur.Checked = true;
                    DivJuridico.Visible = true;
                    CboJuridico.SelectedValue = ds.Tables["DATOS"].Rows[0]["PersonaJuridicaId"].ToString();
                    CboJuridico.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["jur"].ToString();
                }
                TxtDirccion.Text = ds.Tables["DATOS"].Rows[0]["Direccion"].ToString();
                TxtAldea.Text = ds.Tables["DATOS"].Rows[0]["Aldea"].ToString();
                TxtFinca.Text = ds.Tables["DATOS"].Rows[0]["Finca"].ToString();
                CboDepartamento.SelectedValue = ds.Tables["DATOS"].Rows[0]["DepartamentoId"].ToString();
                CboDepartamento.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["Departamento"].ToString();
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
                CboMunicipio.SelectedValue = ds.Tables["DATOS"].Rows[0]["MunicipioId"].ToString();
                CboMunicipio.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["Municipio"].ToString();
                CboTipoDocumento.SelectedValue = ds.Tables["DATOS"].Rows[0]["TipoDoc_PropiedadId"].ToString();
                CboTipoDocumento.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["TipoDocPropiedad"].ToString();
                TxtFecEmi.SelectedDate = Convert.ToDateTime(ds.Tables["DATOS"].Rows[0]["Fec_Emision"].ToString());
                if (CboTipoDocumento.SelectedValue == "1")
                {
                    DivPropiedad.Visible = true;
                    DivMun.Visible = false;
                    DiVPos.Visible = false;
                    TxtNoFinca.Text = ds.Tables["DATOS"].Rows[0]["NoFinca"].ToString();
                    TxtFolio.Text = ds.Tables["DATOS"].Rows[0]["Folio"].ToString();
                    TxtLibro.Text = ds.Tables["DATOS"].Rows[0]["Libro"].ToString();
                    TxtDe.Text = ds.Tables["DATOS"].Rows[0]["De"].ToString();
                }
                else if (CboTipoDocumento.SelectedValue == "2")
                {
                    DivPropiedad.Visible = false;
                    DivMun.Visible = true;
                    DiVPos.Visible = false;
                    TxtNoCerti.Text = ds.Tables["DATOS"].Rows[0]["NoCertificacion"].ToString();
                }
                else if (CboTipoDocumento.SelectedValue == "3")
                {
                    DivPropiedad.Visible = false;
                    DivMun.Visible = false;
                    DiVPos.Visible = true;
                    TxtNoEscritura.Text = ds.Tables["DATOS"].Rows[0]["NoEscritura"].ToString();
                    CboTitulo.SelectedValue = ds.Tables["DATOS"].Rows[0]["TituloNotarioId"].ToString();
                    CboTitulo.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["TituloNotario"].ToString();

                }
                if (ds.Tables["DATOS"].Rows[0]["En_Area"].ToString() == "1")
                {
                    OptAreasPro.SelectedValue = "1";
                    DivArea.Visible = true;
                    CboArea.SelectedValue = ds.Tables["DATOS"].Rows[0]["AreaProtegidaId"].ToString();
                    CboArea.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["AreaPro"].ToString();
                }
                else
                {
                    OptAreasPro.SelectedValue = "0";
                    DivArea.Visible = false;
                }
                TxtArea.Text = ds.Tables["DATOS"].Rows[0]["Area"].ToString();
                if (ds.Tables["DATOS"].Rows[0]["EsRepresentante"].ToString() == "1")
                {
                    ChkRepreseanteyPropietario.Checked = true;
                    ChkRepresentanteVariasPer.Checked = false;
                }
                if (ds.Tables["DATOS"].Rows[0]["EsRepresentanteVarios"].ToString() == "1")
                {
                    ChkRepresentanteVariasPer.Checked = true;
                    ChkRepreseanteyPropietario.Checked = false;
                }
                DivGrigPropietarios.Visible = true;
                GrdPropietarios.Rebind();
                BtnGrabar.Visible = false;
                GrdPropietarios.Columns[5].Visible = false;

            }
            else if (e.CommandName == "CmdDel")
            {
                ClUsuario.Insertar_Actividad_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 3);
                ClInmueble.Elimina_Inmueble(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                GrdInmuebles.Rebind();
                Limpiar();
                BtnGoodInmueble.Visible = true;
                LblGoodInmueble.Text = "Inmueble eliminado";
            }
        }

        void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            Limpiar();
            HabilitaNuevo();
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            BtnBadInmueble.Visible = false;
            BtnGoodInmueble.Visible = false;
        }

        void CboDepartamento_TextChanged(object sender, EventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
        }

        bool Valida()
        {
            BtnBadInmueble.Visible = false;
            LblBadInmueble.Text = "";
            bool HayError = false;
            
            int SeleccionJur = 0;
            int PersonaJuridicaId = 0;

            if (ChkOtraJur.Checked == true && CboJuridico.Text == "")
            {
                
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "Debe Seleccionar una personería jurídica";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", Debe Seleccionar una personería jurídica";
                HayError = true;
            }
            if (ChkOtraJur.Checked == true)
            {
                SeleccionJur = 1;
                PersonaJuridicaId = Convert.ToInt32(CboJuridico.SelectedValue);
            }
            if (CboTipoDocumento.Text == "")
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "Debe seleccionar el tipo de documento de la propiedad";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", Debe seleccionar el tipo de documento de la propiedad";
                HayError = true;
            }
            if (TxtFecEmi.DateInput.Text == "")
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "Debe ingresar la Fecha de la Certificación de la propiedad";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", Debe ingresar la Fecha de la Certificación de la propiedad";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") && (TxtFecEmi.SelectedDate > ClUtilitarios.FechaDB()))
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "La Fecha de Certificación de la propiedad no puede ser mayor a la fecha actual";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", La Fecha de Certificación de la propiedad no puede ser mayor a la fecha actual";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") &&  (ClUtilitarios.EsMayorDays(120, Convert.ToDateTime(TxtFecEmi.SelectedDate)) == true))
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "La Certificación de la propiedad no puede tener mas de 120 dias de antiguedad";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", La Certificación de la propiedad no puede tener mas de 120 dias de antiguedad";
                HayError = true;
            }
            
            if (OptAreasPro.SelectedValue == "")
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "Debe seleccionar si la propiedad se encuentra o no en áreas protegidas";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", Debe seleccionar si la propiedad se encuentra o no en áreas protegidas";
                HayError = true;
            }
            if (OptAreasPro.SelectedValue == "1" && CboArea.Text == "")
            {
                if (LblBadInmueble.Text == "")
                    LblBadInmueble.Text = LblBadInmueble.Text + "Debe seleccionar el área protegida";
                else
                    LblBadInmueble.Text = LblBadInmueble.Text + ", Debe seleccionar el área protegida";
                HayError = true;
            }

            //if ((TxtFecEmi.DateInput.Text != "") && (CboTipoDocumento.Text != "") && (ClInmueble.Existe_Propiedad_Usuario(Convert.ToInt32(Session["UsuarioId"]), SeleccionJur, PersonaJuridicaId, TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), Convert.ToInt32(CboArea.SelectedValue),Convert.ToDecimal(TxtArea.Text)) == true))
            //{
            //    if (LblBadInmueble.Text == "")
            //        LblBadInmueble.Text = LblBadInmueble.Text + "Esta propiedad ya fue ingresada";
            //    else
            //        LblBadInmueble.Text = LblBadInmueble.Text + ", Esta propiedad ya fue ingresada";
            //    HayError = true;
            //}
            //if ((TxtFecEmi.DateInput.Text != "") && (CboTipoDocumento.Text != "") && (ClInmueble.Existe_Propiedad_OtroUsuario(Convert.ToInt32(Session["UsuarioId"]), SeleccionJur, PersonaJuridicaId, TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), Convert.ToInt32(CboArea.SelectedValue), Convert.ToDecimal(TxtArea.Text)) == true))
            //{
            //    if (LblBadInmueble.Text == "")
            //        LblBadInmueble.Text = LblBadInmueble.Text + "Esta propiedad ya fue ingresada por otro usuario";
            //    else
            //        LblBadInmueble.Text = LblBadInmueble.Text + ", Esta propiedad ya fue ingresada por otro usuario";
            //    HayError = true;
            //}
            if (HayError == true)
            {
                BtnBadInmueble.Visible = true;
                return false;
            }
            else
                return true;
        }

        void BtnGrabar_Click(object sender, EventArgs e)
        {
            BtnGoodInmueble.Visible = false;
            LblGoodInmueble.Text = "";
            int SeleccionJur = 0;
            int PersonaJuridicaId = 0;
            if (Valida() == true)
            {
                if (ChkOtraJur.Checked == true)
                {
                    SeleccionJur = 1;
                    PersonaJuridicaId = Convert.ToInt32(CboJuridico.SelectedValue);
                }
                if (TxtInmuebleId.Text == "")
                {
                    int InmuebleId = ClInmueble.Max_Inmueble();
                    ClUsuario.Insertar_Actividad_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                    int EsRePresentante = 0;
                    int EsRepresentanteVarios = 0;
                    if (ChkRepreseanteyPropietario.Checked == true) 
                        EsRePresentante = 1;
                    if (ChkRepresentanteVariasPer.Checked == true)
                        EsRepresentanteVarios = 1;
                    //ClInmueble.Inserta_Inmueble(InmuebleId, Convert.ToInt32(Session["UsuarioId"]), SeleccionJur, PersonaJuridicaId, TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), Convert.ToDouble(TxtArea.Text), Convert.ToInt32(CboArea.SelectedValue), EsRePresentante, EsRepresentanteVarios);
                    for (int i = 0; i < GrdPropietarios.Items.Count; i++)
                    {
                        //int PersonaId = Convert.ToInt32(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PersonaId"]);
                        //if (Convert.ToBoolean(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Existe"]) == false)
                        //{
                        //    //PersonaId = ClPersona.MaxPersonaId();
                        //    //ClPersona.Insertar_Persona_Propietario(PersonaId, GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Nombres"].ToString(), GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Apellidos"].ToString(), GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"].ToString().Replace("-",""));
                        //}
                        //ClInmueble.Inserta_Inmueble_Propietario(PersonaId,InmuebleId);
                    }
                    if (RadUploadFile.UploadedFiles.Count > 0)
                    {
                        Stream fileStream = RadUploadFile.UploadedFiles[0].InputStream;
                        byte[] attachmentBytes = new byte[fileStream.Length];
                        fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                        ClInmueble.Actualiza_Archivo(InmuebleId, attachmentBytes, RadUploadFile.UploadedFiles[0].ContentType, RadUploadFile.UploadedFiles[0].FileName);
                        fileStream.Close();
                    }

                    
                    BtnGoodInmueble.Visible = true;
                    LblGoodInmueble.Text = "Inmueble Agregado Exitosamente";
                }
                else
                {
                    ClUsuario.Insertar_Actividad_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
                    ClInmueble.Actualiza_Inmueble(Convert.ToInt32(TxtInmuebleId.Text) , SeleccionJur, PersonaJuridicaId, TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), Convert.ToDouble(TxtArea.Text), Convert.ToInt32(CboArea.SelectedValue));
                    if (RadUploadFile.UploadedFiles.Count > 0)
                    {
                        Stream fileStream = RadUploadFile.UploadedFiles[0].InputStream;
                        byte[] attachmentBytes = new byte[fileStream.Length];
                        fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                        ClInmueble.Actualiza_Archivo(Convert.ToInt32(TxtInmuebleId.Text), attachmentBytes, RadUploadFile.UploadedFiles[0].ContentType, RadUploadFile.UploadedFiles[0].FileName);
                        fileStream.Close();
                    }
                    BtnGoodInmueble.Visible = true;
                    LblGoodInmueble.Text = "Inmueble modificado Exitosamente";
                }
                GrdInmuebles.Rebind();
                Limpiar();
            }
        }

        void GrdInmuebles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClInmueble.Inmueble_GetAll(Convert.ToInt32(Session["UsuarioId"])), GrdInmuebles);
        }

        void OptAreasPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptAreasPro.Items[0].Selected == true)
                DivArea.Visible = false;
            else
                DivArea.Visible = true;
        }

        void CboTipoDocumento_TextChanged(object sender, EventArgs e)
        {
            if (CboTipoDocumento.SelectedValue == "1")
            {
                DivPropiedad.Visible = true;
                DivMun.Visible = false;
                DiVPos.Visible = false;
            }
            else if (CboTipoDocumento.SelectedValue == "2")
            {
                DivPropiedad.Visible = false;
                DivMun.Visible = true;
                DiVPos.Visible = false;
            }
            else if (CboTipoDocumento.SelectedValue == "3")
            {
                DivPropiedad.Visible = false;
                DivMun.Visible = false;
                DiVPos.Visible = true;
            }
        }

        void ChkOtraJur_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkOtraJur.Checked == true)
                DivJuridico.Visible = true;
            else
                DivJuridico.Visible = false;

        }

        void HabilitaNuevo()
        {
            BtnGrabar.Visible = true;
            GrdPropietarios.Columns[5].Visible = false;
        }

        void Limpiar()
        {
            ChkOtraJur.Checked = false;
            TxtDirccion.Text = "";
            TxtAldea.Text = "";
            TxtFinca.Text = "SIN NOMBRE";
            CboTipoDocumento.ClearSelection();
            DivPropiedad.Visible = false;
            DivMun.Visible = false;
            DiVPos.Visible = false;
            TxtNoFinca.Text = "";
            TxtFolio.Text = "";
            TxtLibro.Text = "";
            TxtDe.Text = "";
            TxtNoCerti.Text = "";
            TxtNoEscritura.Text = "";
            TxtNomNotario.Text = "";
            OptAreasPro.SelectedValue = "0";
            TxtArea.Text = "";
            TxtInmuebleId.Text = "";
            DivJuridico.Visible = false;
            DivArea.Visible = false;
            ChkRepresentanteVariasPer.Checked = false;
            ChkRepreseanteyPropietario.Checked = false;
            DivPropietario.Visible = false;
            DivAddPropietarioMensaje.Visible = false;
            DivGrigPropietarios.Visible = false;
            DivAddPropietario.Visible = false;
            DsPropietarios.Tables["Propietarios"].Clear();
            GrdPropietarios.Rebind();
            BtnGrabar.Visible = false;
            GrdPropietarios.Columns[5].Visible = false;
            
        }

    }
}