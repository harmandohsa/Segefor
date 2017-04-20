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
using System.Xml;
using System.IO;
using Excel;
using LibreriaPinpep;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_InscripcionEmpresas : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Inmueble ClInmueble;
        Cl_Gestion_Registro ClGestionRegistro;
        Cl_Xml ClXml;
        Cl_Persona_Juridica ClEmpresa;
        Cl_Registro ClRegistro;
        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        DataView dv = new DataView();
        DataSet DsPropietarios = new DataSet("Propietarios");
        DataSet DsRepresentantes = new DataSet("Representantes");

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClInmueble = new Cl_Inmueble();
            ClGestionRegistro = new Cl_Gestion_Registro();
            ClXml = new Cl_Xml();
            ClEmpresa = new Cl_Persona_Juridica();
            ClRegistro = new Cl_Registro();

            DataTable DtPropietarios = DsPropietarios.Tables.Add("Propietarios");
            DataColumn Existe = DtPropietarios.Columns.Add("Existe", typeof(Boolean));
            DataColumn PersonaId = DtPropietarios.Columns.Add("PersonaId", typeof(Int64));
            DataColumn Dpi = DtPropietarios.Columns.Add("Dpi", typeof(string));
            DataColumn Nombres = DtPropietarios.Columns.Add("Nombres", typeof(string));
            DataColumn Apellidos = DtPropietarios.Columns.Add("Apellidos", typeof(string));
            DataColumn Fec_Venc_Id = DtPropietarios.Columns.Add("Fec_Venc_Id", typeof(string));
            DataColumn PaisId = DtPropietarios.Columns.Add("PaisId", typeof(Int32));

            DataTable DtRepresentantes = DsRepresentantes.Tables.Add("Representantes");
            DataColumn ExisteRep = DtRepresentantes.Columns.Add("ExisteRep", typeof(Boolean));
            DataColumn PersonaIdRep = DtRepresentantes.Columns.Add("PersonaIdRep", typeof(Int64));
            DataColumn DpiRep = DtRepresentantes.Columns.Add("DpiRep", typeof(string));
            DataColumn NombresRep = DtRepresentantes.Columns.Add("NombresRep", typeof(string));
            DataColumn ApellidosRep = DtRepresentantes.Columns.Add("ApellidosRep", typeof(string));
            DataColumn Fec_Venc_IdRep = DtRepresentantes.Columns.Add("Fec_Venc_IdRep", typeof(string));
            DataColumn PaisIdRep = DtRepresentantes.Columns.Add("PaisIdRep", typeof(Int32));

            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            CboDepartamento.SelectedIndexChanged += CboDepartamento_SelectedIndexChanged;
            BtnEnviar.Click += BtnEnviar_Click;
            BtnYes.Click += BtnYes_Click;
            GrdProductosExportacion.NeedDataSource += GrdProductosExportacion_NeedDataSource;  
            btnAddProducto.ServerClick += btnAddProducto_ServerClick;
            GrdProductosExportacion.ItemCommand += GrdProductosExportacion_ItemCommand;
            CboSubCategoria.SelectedIndexChanged += CboSubCategoria_SelectedIndexChanged;
            CboTipoPersona.SelectedIndexChanged += CboTipoPersona_SelectedIndexChanged;
            BtnValidarDpi.ServerClick += BtnValidarDpi_ServerClick;
            GrdPropietarios.NeedDataSource += GrdPropietarios_NeedDataSource;
            BtnNuevaEmpresa.ServerClick += BtnNuevaEmpresa_ServerClick;
            CboDepEmpresa.SelectedIndexChanged += CboDepEmpresa_SelectedIndexChanged;
            CboTipoIdPropietario.SelectedIndexChanged += CboTipoIdPropietario_SelectedIndexChanged;
            BtnAddPropietario.ServerClick += BtnAddPropietario_ServerClick;
            BtnValidarPasaporte.ServerClick += BtnValidarPasaporte_ServerClick;
            GrdPropietarios.ItemCommand += GrdPropietarios_ItemCommand;
            CboTipoEmpresa.SelectedIndexChanged += CboTipoEmpresa_SelectedIndexChanged;
            OptFabricaProductos.SelectedIndexChanged += OptFabricaProductos_SelectedIndexChanged;
            BtnAddProductoImporatcion.ServerClick += BtnAddProductoImporatcion_ServerClick;
            GrdProductoImportacion.NeedDataSource += GrdProductoImportacion_NeedDataSource;
            GrdProductoImportacion.ItemCommand += GrdProductoImportacion_ItemCommand;
            CboDepFuncionamiento.SelectedIndexChanged += CboDepFuncionamiento_SelectedIndexChanged;
            CboMunEmpresa.SelectedIndexChanged += CboMunEmpresa_SelectedIndexChanged;
            CboEmpresa.SelectedIndexChanged += CboEmpresa_SelectedIndexChanged;
            CboTipoIdentificacionRep.SelectedIndexChanged += CboTipoIdentificacionRep_SelectedIndexChanged;
            BtnValidarDpiRep.ServerClick += BtnValidarDpiRep_ServerClick;
            GrdRepresentantes.NeedDataSource += GrdRepresentantes_NeedDataSource;
            BtnAddRepresentante.ServerClick += BtnAddRepresentante_ServerClick;
            BtnValidarPasaporteRep.ServerClick += BtnValidarPasaporteRep_ServerClick;
            GrdRepresentantes.ItemCommand += GrdRepresentantes_ItemCommand;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(41, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 41);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {

                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnEnviar.Visible = false;
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
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSubCategoriasRegistro(5, Convert.ToInt32(Session["PersonaId"])), CboSubCategoria, "SubCategoriaId", "Nombre_Subcategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboSubCategoria, "Sub Categoría");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdentificacionRep, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdentificacionRep, "Tipo de Identificación");
                
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPaisRep, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPaisRep, "País");
                DataSet dsPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                TxtOrigenPersonaId.Text = dsPersona.Tables["Datos"].Rows[0]["Origen_PersonaId"].ToString();
                dsPersona.Clear();
                if (TxtOrigenPersonaId.Text == "2")
                    LblDirecNotifica.InnerText = "Dirección en Guatemala";
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.Tipo_Persona(), CboTipoPersona, "Tipo_PersonaId", "Tipo_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoPersona, "Tipo Persona");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEmpresaMercantil(), CboDe, "Categoria_EmpresaMercantilId", "CategoriaEmpresaMercantil");
                ClUtilitarios.AgregarSeleccioneCombo(CboDe, "Empresas Mercantiles");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_CategoriaEmpresa(), CboCategoria, "Categoria_EmpresaId", "Categoria_Empresa");
                ClUtilitarios.AgregarSeleccioneCombo(CboCategoria, "Categoría");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Empresas(Convert.ToInt32(Session["UsuarioId"])), CboEmpresa, "PersonaJuridicaId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboEmpresa, "Empresa");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepEmpresa, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepEmpresa, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunEmpresa, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdPropietario, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdPropietario, "Tipo de Identificación");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPais, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPais, "País");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Productos(), CboProducto, "Codigo_Producto", "Nombre_Producto");
                ClUtilitarios.AgregarSeleccioneCombo(CboProducto, "Producto");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Productos(), CboProducto, "Codigo_Producto", "Nombre_Producto");
                ClUtilitarios.AgregarSeleccioneCombo(CboProducto, "Producto");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Productos(), CboProductoImportacion, "Codigo_Producto", "Nombre_Producto");
                ClUtilitarios.AgregarSeleccioneCombo(CboProductoImportacion, "Producto");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_ActividadPrincipal(), CboActividadPrincipal, "Actividad_PrincipalId", "ActividadPrincipal");
                ClUtilitarios.AgregarSeleccioneCombo(CboActividadPrincipal, "Actividad Principal");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepFuncionamiento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepFuncionamiento, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunFuncionamiento, "Municipio");
            }
        }

        void GrdRepresentantes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                string Dpi = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["DpiRep"].ToString().Trim();
                for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
                {
                    if (Dpi != e.Item.OwnerTableView.DataKeyValues[i]["DpiRep"].ToString().Trim())
                    {
                        DataRow itemGrid = DsRepresentantes.Tables["Representantes"].NewRow();
                        itemGrid["ExisteRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ExisteRep"];
                        itemGrid["PersonaIdRep"] = Convert.ToInt64(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PersonaIdRep"]);
                        itemGrid["DpiRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"];
                        itemGrid["NombresRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"];
                        itemGrid["ApellidosRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"];
                        itemGrid["Fec_Venc_IdRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_IdRep"];
                        itemGrid["PaisIdRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"];
                        DsRepresentantes.Tables["Representantes"].Rows.Add(itemGrid);
                    }
                }
                GrdRepresentantes.Rebind();
                DivGoodPropietario.Visible = true;
                LblMansajeGoodPropietario.Text = "Representante eliminado de la finca";
            }
        }

        void BtnValidarPasaporteRep_ServerClick(object sender, EventArgs e)
        {
            DivBadRepresentante.Visible = false;
            DivGoodRepresentante.Visible = false;
            LblMansajeBadRepresentante.Text = "";
            LblMansajeGoodRepresentante.Text = "";
            if (TxtPasaporteRep.Text == "")
            {
                DivBadRepresentante.Visible = true;
                LblMansajeBadRepresentante.Text = "Debe ingresar el número de Pasaporte";
            }
            else
            {
                if (CboPaisRep.SelectedValue == "")
                {
                    DivBadRepresentante.Visible = true;
                    LblMansajeBadRepresentante.Text = "Debe Seleccionar el país";
                }
                else
                {
                    DataSet DatosPersona = new DataSet();
                    DatosPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                    if (ExisteRepresentante(TxtPasaporteRep.Text) == true)
                    {
                        LblMansajeBadRepresentante.Text = "Ya Agrego a este propietario";
                        DivBadRepresentante.Visible = true;
                    }
                    else
                    {
                        if (ClPersona.Existe_Dpi(TxtPasaporteRep.Text, 2, Convert.ToInt32(CboPaisRep.SelectedValue)) == true)
                        {
                            LeeGridRepresentantes();
                            DataSet dsDatos = new DataSet();
                            dsDatos = ClPersona.Datos_Persona_Dpi(TxtPasaporteRep.Text, 2, Convert.ToInt32(CboPaisRep.SelectedValue));
                            DataRow item = DsRepresentantes.Tables["Representantes"].NewRow();
                            item["ExisteRep"] = 1;
                            item["PersonaIdRep"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                            item["DpiRep"] = TxtPasaporteRep.Text;
                            item["NombresRep"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                            item["ApellidosRep"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                            item["Fec_Venc_IdRep"] = string.Format("{0:dd/MM/yyyy}", dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            item["PaisIdRep"] = dsDatos.Tables["DATOS"].Rows[0]["PaisId"];
                            DsRepresentantes.Tables["Representantes"].Rows.Add(item);
                            DivGoodRepresentante.Visible = true;
                            LblMansajeGoodRepresentante.Text = "Representante Agregado Exitosamente";
                            GrdRepresentantes.Rebind();
                            LimpiarRepresentante();
                        }
                        else
                        {
                            DivNombresPropRep.Visible = true;
                            DivApePropRep.Visible = true;
                            DivAddPropRep.Visible = true;
                            DivBadRepresentante.Visible = true;
                            DivFecVencimientoRep.Visible = true;
                            LblMansajeBadRepresentante.Text = "El núemero de Pasaporte no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }

                    }
                }
            }
        }

        void BtnAddRepresentante_ServerClick(object sender, EventArgs e)
        {
            DivBadRepresentante.Visible = false;
            DivGoodRepresentante.Visible = false;
            LblMansajeBadRepresentante.Text = "";
            LblMansajeGoodRepresentante.Text = "";
            bool HayError = false;
            int PaisId;
            string Dpi;
            if (TxtNombresRep.Text == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe ingresar el nombre del representante";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe ingresar el nombre del representante";
                HayError = true;
            }
            if (TxtApellidosRep.Text == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe ingresar el apellido del representante";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe ingresar el apellido del representante";
                HayError = true;
            }
            if (TxtFecVenceIdRep.DateInput.Text == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe ingresar a fecha de vencimiento de su documento de Identificación";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe ingresar a fecha de vencimiento de su documento de Identificación";
                HayError = true;
            }
            if (TxtFecVenceIdRep.SelectedDate < DateTime.Now)
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Documento De Identificación Vencido";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", documento De Identificación Vencido";
                HayError = true;
            }
            if (HayError == true)
                DivBadRepresentante.Visible = true;
            else
            {
                string DocaValidar = "";
                if (Convert.ToInt32(CboTipoIdentificacionRep.SelectedValue) == 1)
                    DocaValidar = TxtDpiRep.Text;
                else
                    DocaValidar = TxtPasaporteRep.Text;
                if (ExisteRepresentante(DocaValidar) == true)
                {
                    LblMansajeBadRepresentante.Text = "Ya Agrego a este representante";
                    DivBadRepresentante.Visible = true;
                }
                else
                {
                    LeeGridRepresentantes();
                    DataRow item = DsRepresentantes.Tables["Representantes"].NewRow();
                    item["ExisteRep"] = 0;
                    item["PersonaIdRep"] = 0;

                    item["NombresRep"] = TxtNombresRep.Text;
                    item["ApellidosRep"] = TxtApellidosRep.Text;
                    item["Fec_Venc_IdRep"] = string.Format("{0:dd/MM/yyyy}", TxtFecVenceIdRep.SelectedDate);
                    if (Convert.ToInt32(CboTipoIdentificacionRep.SelectedValue) == 1)
                    {
                        item["PaisIdRep"] = 0;
                        item["DpiRep"] = TxtDpiRep.Text;
                        PaisId = 0;
                        Dpi = TxtDpiRep.Text.Replace("-", "");
                    }
                    else
                    {
                        item["PaisIdRep"] = CboPaisRep.SelectedValue;
                        item["DpiRep"] = TxtPasaporteRep.Text;
                        PaisId = Convert.ToInt32(CboPaisRep.SelectedValue);
                        Dpi = TxtPasaporteRep.Text;
                    }
                    DsRepresentantes.Tables["Representantes"].Rows.Add(item);
                    DivGoodRepresentante.Visible = true;
                    LblMansajeGoodRepresentante.Text = "Representante Agregado Exitosamente";
                    GrdRepresentantes.Rebind();
                    LimpiarRepresentante();
                    DivNombresPropRep.Visible = false;
                    DivApePropRep.Visible = false;
                    DivAddPropRep.Visible = false;
                    DivFecVencimientoRep.Visible = false;
                }

            }
        }

        void GrdRepresentantes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsRepresentantes.Tables["Representantes"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsRepresentantes, GrdRepresentantes, "Representantes");
        }

        void BtnValidarDpiRep_ServerClick(object sender, EventArgs e)
        {
            DivBadRepresentante.Visible = false;
            DivGoodRepresentante.Visible = false;
            LblMansajeBadRepresentante.Text = "";
            LblMansajeGoodRepresentante.Text = "";
            if (TxtDpiRep.Text == "")
            {
                DivBadRepresentante.Visible = true;
                LblMansajeBadRepresentante.Text = "Debe ingresar el número de DPI";
            }
            else
            {
                if (TxtDpiRep.Text.Length < 13)
                {
                    DivBadRepresentante.Visible = true;
                    LblMansajeBadRepresentante.Text = "El número de DPI esta incompleto";
                }
                else
                {
                    DataSet DatosPersona = new DataSet();
                    DatosPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                    if (ExisteRepresentante(TxtDpiRep.Text) == true)
                    {
                        LblMansajeBadRepresentante.Text = "Ya Agrego a este propietario";
                        DivBadRepresentante.Visible = true;
                    }
                    else
                    {
                        if (ClPersona.Existe_Dpi(TxtDpiRep.Text.Replace("-", ""), 1) == true)
                        {
                            LeeGridRepresentantes();
                            DataSet dsDatos = new DataSet();
                            dsDatos = ClPersona.Datos_Persona_Dpi(TxtDpiRep.Text.Replace("-", ""), 1);
                            DataRow item = DsRepresentantes.Tables["Representantes"].NewRow();
                            item["ExisteRep"] = 1;
                            item["PersonaIdRep"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                            item["DpiRep"] = TxtDpiRep.Text;
                            item["NombresRep"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                            item["ApellidosRep"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                            item["Fec_Venc_IdRep"] = string.Format("{0:dd/MM/yyyy}", dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            item["PaisIdRep"] = 0;
                            DsRepresentantes.Tables["Representantes"].Rows.Add(item);
                            DivGoodRepresentante.Visible = true;
                            LblMansajeGoodRepresentante.Text = "Representante Agregado Exitosamente";
                            GrdRepresentantes.Rebind();
                            LimpiarRepresentante();
                        }
                        else
                        {
                            DivNombresPropRep.Visible = true;
                            DivApePropRep.Visible = true;
                            DivAddPropRep.Visible = true;
                            DivBadRepresentante.Visible = true;
                            DivFecVencimientoRep.Visible = true;
                            LblMansajeBadRepresentante.Text = "El núemero de DPI no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }

                    }
                }
            }
        }

        void LimpiarRepresentante()
        {
            TxtDpiRep.Text = "";
            TxtNombresRep.Text = "";
            TxtApellidosRep.Text = "";
            TxtFecVenceIdRep.Clear();
            TxtPasaporteRep.Text = "";
            CboPaisRep.ClearSelection();
        }

        void LeeGridRepresentantes()
        {

            GrdRepresentantes.AllowPaging = false;
            for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
            {
                DataRow itemGrid = DsRepresentantes.Tables["Representantes"].NewRow();
                itemGrid["ExisteRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ExisteRep"];
                itemGrid["PersonaIdRep"] = Convert.ToInt64(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PersonaIdRep"]);
                itemGrid["DpiRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"];
                itemGrid["NombresRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"];
                itemGrid["ApellidosRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"];
                itemGrid["Fec_Venc_IdRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_IdRep"];
                itemGrid["PaisIdRep"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"];
                DsRepresentantes.Tables["Representantes"].Rows.Add(itemGrid);
            }
        }

        bool ExisteRepresentante(string Dpi)
        {
            bool Existe = false;
            for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
            {
                if (Dpi == GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"].ToString().Trim())
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        void CboTipoIdentificacionRep_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboTipoIdentificacionRep.SelectedValue) == 1)
            {
                DivPropietarioNacionalRepre.Visible = true;
                DivPropietarioInterRep.Visible = false;
            }
            else
            {
                DivPropietarioNacionalRepre.Visible = false;
                DivPropietarioInterRep.Visible = true;
            }
        }

        void BloquarCamposEmpresa()
        {
            TxtNombreComercial.Enabled = false;
            TxtNIT.Enabled = false;
            TxtNoMercantil.Enabled = false;
            TxtFolio.Enabled = false;
            TxtLibro.Enabled = false;
            CboDe.Enabled = false;
            CboCategoria.Enabled = false;
            TxtObjeto.Enabled = false;
            TxtHorasTurno.Enabled = false;
            TxtDiasYear.Enabled = false;
            TxtEmplFijo.Enabled = false;
            TxtEmplNoFijo.Enabled = false;
            TxtDireccion.Enabled = false;
            CboDepEmpresa.Enabled = false;
            CboMunEmpresa.Enabled = false;
            TxtTelUnoEmpresa.Enabled = false;
            TxtTelDosEmpresa.Enabled = false;
            TxtCorreoEmpresa.Enabled = false;

        }

        void CboEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BloquarCamposEmpresa();
            TxtEmpresaId.Text = CboEmpresa.SelectedValue;
            CargaInfo(Convert.ToInt32(CboEmpresa.SelectedValue));
            if (CboSubCategoria.SelectedValue != "")
            {
                bool Resul = false;
                //if (CboSubCategoria.SelectedValue != "10")
                //    Resul = ClGestionRegistro.Existe_Tipo_Gestion(Convert.ToInt32(Session["PersonaId"]), 5, Convert.ToInt32(CboSubCategoria.SelectedValue), 0, Convert.ToInt32(CboEmpresa.SelectedValue), 0);
                //else
                //    Resul = ClGestionRegistro.Existe_Tipo_Gestion(Convert.ToInt32(Session["PersonaId"]), 5, Convert.ToInt32(CboSubCategoria.SelectedValue), Convert.ToInt32(CboTipoEmpresa.SelectedValue), Convert.ToInt32(CboEmpresa.SelectedValue), 0);
                Resul = ClGestionRegistro.Existe_Tipo_Gestion(Convert.ToInt32(Session["PersonaId"]), 5, Convert.ToInt32(CboSubCategoria.SelectedValue), 0, Convert.ToInt32(CboEmpresa.SelectedValue), 0);
                if (Resul == true)
                {
                    BtnEnviar.Visible = false;
                    BtnVistaPrevia.Visible = false;
                    DivErrEmpresa.Visible = true;
                    LblErrEmpresa.Text = "No se Puede inscribir la Empresa Forestal con la misma patente que inscribió otra empresa";
                    BloqueaDatosEmpresa();
                }
                else
                {
                    BtnEnviar.Visible = true;
                    BtnVistaPrevia.Visible = true;
                    DivErrEmpresa.Visible = false;
                    LblErrEmpresa.Text = "";
                    DesbloqueaDatosEmpresa();
                }
            }
        }

        void DesbloqueaDatosEmpresa()
        {
            TxtHorasTurno.Enabled = true;
            TxtDiasYear.Enabled = true;
            TxtEmplFijo.Enabled = true;
            TxtEmplNoFijo.Enabled = true;
        }

        void BloqueaDatosEmpresa()
        {
            TxtHorasTurno.Enabled = false;
            TxtDiasYear.Enabled = false;
            TxtEmplFijo.Enabled = false;
            TxtEmplNoFijo.Enabled = false;
        }

        void CboMunEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet dsRegioSubregionEmpresa = ClInmueble.Get_Region_Subregion_MunicipioId(Convert.ToInt32(CboMunEmpresa.SelectedValue));
            TxtRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["Region"].ToString();
            TxtSubRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubRegion"].ToString();
            TxtSubRegionId.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubregionId"].ToString();
            dsRegioSubregionEmpresa.Clear();
        }

        void CboDepFuncionamiento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepFuncionamiento.SelectedValue)), CboMunFuncionamiento, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunFuncionamiento, "Municipio");
        }

        void GrdProductoImportacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarProductoImportacion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Codigo_Producto"].ToString()));
            }
        }

        void GrdProductoImportacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Productos_Importacion"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(Ds_Temporal, GrdProductoImportacion, "Dt_Productos_Importacion");
        }

        void BtnAddProductoImporatcion_ServerClick(object sender, EventArgs e)
        {
            if (ValidaProductoImportacion() == true)
            {
                if (ExisteProductoImportacion(Convert.ToInt32(CboProductoImportacion.SelectedValue)))
                {
                    DivErrProductoImportacion.Visible = true;
                    LblMensajeImportacion.Text = "Producto ya existe";

                }
                else
                {
                    CargarGridProductoImportacion();
                    AgregaProductoImportacion();
                    GrdProductoImportacion.Rebind();
                    LimpiarProductoImportacion();
                }



            }
        }

        void btnAddProducto_ServerClick(object sender, EventArgs e)
        {
            if (ValidaEspecie() == true)
            {
                if (ExisteProducto(Convert.ToInt32(CboProducto.SelectedValue)))
                {
                    DivErrProducto.Visible = true;
                    LblMensajeProducto.Text = "Producto ya existe";

                }
                else
                {
                    CargarGridProducto();
                    AgregaProducto();
                    GrdProductosExportacion.Rebind();
                    LimpiarProducto();
                }



            }
        }

        void GrdProductosExportacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarProductoExportacion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Codigo_Producto"].ToString()));
            }
        }

      

        void GrdProductosExportacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Productos_Exportacion"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(Ds_Temporal, GrdProductosExportacion, "Dt_Productos_Exportacion");
        }

        void OptFabricaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptFabricaProductos.SelectedValue == "1")
                DivEsFabrica.Visible = true;
            else
                DivEsFabrica.Visible = false;
        }

        void CboTipoEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboTipoEmpresa.SelectedValue) == 2)
                DivMovibles.Visible = true;
            else
                DivMovibles.Visible = false;
            //if (CboEmpresa.SelectedValue != "")
            //{
            //    bool Resul = false;
            //    if (CboSubCategoria.SelectedValue == "10")
            //    {
            //        Resul = ClGestionRegistro.Existe_Tipo_Gestion(Convert.ToInt32(Session["PersonaId"]), 5, Convert.ToInt32(CboSubCategoria.SelectedValue), Convert.ToInt32(CboTipoEmpresa.SelectedValue), Convert.ToInt32(CboEmpresa.SelectedValue), 0);
            //        if (Resul == true)
            //        {
            //            BtnEnviar.Visible = false;
            //            BtnVistaPrevia.Visible = false;
            //            DivErrEmpresa.Visible = true;
            //            LblErrEmpresa.Text = "Ya existe una gestión con esta empresa para la categoría seleccionada";
            //        }
            //        else
            //        {
            //            BtnEnviar.Visible = true;
            //            BtnEnviar.Visible = true;
            //            DivErrEmpresa.Visible = false;
            //            LblErrEmpresa.Text = "";
            //        }
            //    }
                    
            //}
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
                        itemGrid["Fec_Venc_Id"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_Id"];
                        itemGrid["PaisId"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PaisId"];
                        DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
                    }
                }
                GrdPropietarios.Rebind();
            }
        }

        void BtnValidarPasaporte_ServerClick(object sender, EventArgs e)
        {
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            LblMansajeBadPropietario.Text = "";
            LblMansajeGoodPropietario.Text = "";
            if (TxtPasaportePropietario.Text == "")
            {
                DivBadPropietario.Visible = true;
                LblMansajeBadPropietario.Text = "Debe ingresar el número de Pasaporte";
            }
            else
            {
                if (CboPais.SelectedValue == "")
                {
                    DivBadPropietario.Visible = true;
                    LblMansajeBadPropietario.Text = "Debe Seleccionar el país";
                }
                else
                {
                    DataSet DatosPersona = new DataSet();
                    DatosPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                    if (ExistePropietario(TxtPasaportePropietario.Text) == true)
                    {
                        LblMansajeBadPropietario.Text = "Ya Agrego a este propietario";
                        DivBadPropietario.Visible = true;
                    }
                    else
                    {
                        if (ClPersona.Existe_Dpi(TxtPasaportePropietario.Text, 2, Convert.ToInt32(CboPais.SelectedValue)) == true)
                        {
                            LeeGridPropietarios();
                            DataSet dsDatos = new DataSet();
                            dsDatos = ClPersona.Datos_Persona_Dpi(TxtPasaportePropietario.Text, 2, Convert.ToInt32(CboPais.SelectedValue));
                            DataRow item = DsPropietarios.Tables["Propietarios"].NewRow();
                            item["Existe"] = 1;
                            item["PersonaId"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                            item["Dpi"] = TxtPasaportePropietario.Text;
                            item["Nombres"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                            item["Apellidos"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                            item["Fec_Venc_Id"] = string.Format("{0:dd/MM/yyyy}", dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            item["PaisId"] = dsDatos.Tables["DATOS"].Rows[0]["PaisId"];
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
                            DivFecVencimiento.Visible = true;
                            LblMansajeBadPropietario.Text = "El núemero de Pasaporte no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }

                    }
                }
            }
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
            if (TxtFecVenc.DateInput.Text == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe ingresar a fecha de vencimiento de su documento de Identificación";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe ingresar a fecha de vencimiento de su documento de Identificación";
                HayError = true;
            }
            if (TxtFecVenc.SelectedDate < DateTime.Now)
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Documento De Identificación Vencido";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", documento De Identificación Vencido";
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
                    
                    item["Nombres"] = TxtNombrePropietario.Text;
                    item["Apellidos"] = TxtApellidoPropietario.Text;
                    item["Fec_Venc_Id"] = string.Format("{0:dd/MM/yyyy}", TxtFecVenc.SelectedDate);
                    if (Convert.ToInt32(CboTipoIdPropietario.SelectedValue) == 1)
                    {
                        item["PaisId"] = 0;
                        item["Dpi"] = TxtDpi.Text;
                    }
                    else
                    {
                        item["PaisId"] = CboPais.SelectedValue;
                        item["Dpi"] = TxtPasaportePropietario.Text;
                    }
                    DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                    DivGoodPropietario.Visible = true;
                    LblMansajeGoodPropietario.Text = "Propietario Agregado Exitosamente";
                    GrdPropietarios.Rebind();
                    LimiarPropietario();
                    DivNombresProp.Visible = false;
                    DivApeProp.Visible = false;
                    DivAddProp.Visible = false;
                    DivFecVencimiento.Visible = false;
                }

            }
        }

        void CboTipoIdPropietario_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboTipoIdPropietario.SelectedValue) == 1)
            {
                DivPropietarioNacional.Visible = true;
                DivPropietarioInter.Visible = false;
            }
            else
            {
                DivPropietarioNacional.Visible = false;
                DivPropietarioInter.Visible = true;
            }
        }

        void CboDepEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepEmpresa.SelectedValue)), CboMunEmpresa, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunEmpresa, "Municipio");
        }

        void BtnNuevaEmpresa_ServerClick(object sender, EventArgs e)
        {
            NuevaEmpresa();
            TxtEmpresaId.Text = "";
        }

        void GrdPropietarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsPropietarios.Tables["Propietarios"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsPropietarios, GrdPropietarios, "Propietarios");
            else
            {
                //LeeDataSetPropietarios(Convert.ToInt32(TxtEmpresaId.Text));
                //ClUtilitarios.LlenaGridDataSet(DsPropietarios, GrdPropietarios, "Propietarios");
                //DivPropietarios.Visible = true;
                //DivGrigPropietarios.Visible = true;
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

        void BtnValidarDpi_ServerClick(object sender, EventArgs e)
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
                            item["Fec_Venc_Id"] = string.Format("{0:dd/MM/yyyy}",dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            item["PaisId"] = 0;
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
                            DivFecVencimiento.Visible = true;
                            LblMansajeBadPropietario.Text = "El núemero de DPI no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }
                        
                    }
                }
            }
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
                itemGrid["Fec_Venc_Id"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_Id"];
                itemGrid["PaisId"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PaisId"];
                DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
            }
        }

        void LeeDataSetPropietarios(int EmpresaId)
        {
            DataSet dsPropietarios = ClEmpresa.Get_Propietarios_Gestion(EmpresaId);
            GrdPropietarios.AllowPaging = false;
            for (int i = 0; i < GrdPropietarios.Items.Count; i++)
            {
                DataRow itemGrid = DsPropietarios.Tables["Propietarios"].NewRow();
                itemGrid["Existe"] = dsPropietarios.Tables["Datos"].Rows[i]["Existe"];
                itemGrid["PersonaId"] = Convert.ToInt64(dsPropietarios.Tables["Datos"].Rows[i]["PersonaId"]);
                itemGrid["Dpi"] = dsPropietarios.Tables["Datos"].Rows[i]["Dpi"];
                itemGrid["Nombres"] = dsPropietarios.Tables["Datos"].Rows[i]["Nombres"];
                itemGrid["Apellidos"] = dsPropietarios.Tables["Datos"].Rows[i]["Apellidos"];
                itemGrid["Fec_Venc_Id"] = dsPropietarios.Tables["Datos"].Rows[i]["Fec_Venc_Id"];
                itemGrid["PaisId"] = dsPropietarios.Tables["Datos"].Rows[i]["PaisId"];
                DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
            }
            dsPropietarios.Clear();
        }

        void CboTipoPersona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboTipoPersona.SelectedValue) == 2)
            {
                DivJuridica.Visible = true;
                DivPropietarios.Visible = false;
            }
            else
            {
                DivJuridica.Visible = false;
                DivPropietarios.Visible = true;
                DivGrigPropietarios.Visible = true;
            }
        }

        void CboSubCategoria_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboSubCategoria.SelectedValue) == 10)
            {
                DivDetCategoria.Visible = true;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDetCategoriasRegistro(5,Convert.ToInt32(CboSubCategoria.SelectedValue), Convert.ToInt32(Session["PersonaId"])), CboTipoEmpresa, "Det_SubCategoriaId", "Det_Subcategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoEmpresa, "Tipo de Empresa");
            }
            else
                DivDetCategoria.Visible = false;

            if (Convert.ToInt32(CboSubCategoria.SelectedValue) == 17)
                DivMovibles.Visible = true;
            else
                DivMovibles.Visible = false;

            if (Convert.ToInt32(CboSubCategoria.SelectedValue) == 9)
                DivExportadorImportador.Visible = true;
            else
                DivExportadorImportador.Visible = false;

            if (CboEmpresa.SelectedValue != "")
            {
                bool Resul = false;
                if (CboSubCategoria.SelectedValue != "10")
                {
                    Resul = ClGestionRegistro.Existe_Tipo_Gestion(Convert.ToInt32(Session["PersonaId"]), 5, Convert.ToInt32(CboSubCategoria.SelectedValue), 0, Convert.ToInt32(CboEmpresa.SelectedValue), 0);
                    if (Resul == true)
                    {
                        BtnEnviar.Visible = false;
                        BtnVistaPrevia.Visible = false;
                        DivErrEmpresa.Visible = true;
                        LblErrEmpresa.Text = "No se Puede inscribir la Empresa Forestal con la misma patente que inscribió otra empresa";
                    }
                    else
                    {
                        BtnEnviar.Visible = true;
                        BtnVistaPrevia.Visible = true;
                        DivErrEmpresa.Visible = false;
                        LblErrEmpresa.Text = "";
                    }
                }
            }
            
        }


        void EliminarProductoExportacion(int ProductoId)
        {
            for (int i = 0; i < GrdProductosExportacion.Items.Count; i++)
            {
                if (ProductoId == Convert.ToInt32(GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"]))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Exportacion"].NewRow();
                    rowNew["Codigo_Producto"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                    rowNew["Nombre_Producto"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                    rowNew["CodigoFSC"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                    Ds_Temporal.Tables["Dt_Productos_Exportacion"].Rows.Add(rowNew);
                }
            }
            GrdProductosExportacion.Rebind();
        }

        void EliminarProductoImportacion(int ProductoId)
        {
            for (int i = 0; i < GrdProductoImportacion.Items.Count; i++)
            {
                if (ProductoId == Convert.ToInt32(GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"]))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Importacion"].NewRow();
                    rowNew["Codigo_Producto"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                    rowNew["Nombre_Producto"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                    rowNew["CodigoFSC"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                    Ds_Temporal.Tables["Dt_Productos_Importacion"].Rows.Add(rowNew);
                }
            }
            GrdProductoImportacion.Rebind();
        }

        

        bool ValidaEspecie()
        
        {
            LblMensajeProducto.Text = "";
            DivErrProducto.Visible = false;
            bool HayError = false;
            if ((CboProducto.Text == "") || (CboProducto.SelectedValue == ""))
            {
                if (LblMensajeProducto.Text == "")
                    LblMensajeProducto.Text = LblMensajeProducto.Text + "Debe seleccionar el producto";
                else
                    LblMensajeProducto.Text = LblMensajeProducto.Text + ", debe seleccionar el producto";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrProducto.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ValidaProductoImportacion()
        {
            LblMensajeImportacion.Text = "";
            DivErrProductoImportacion.Visible = false;
            bool HayError = false;
            if ((CboProductoImportacion.Text == "") || (CboProductoImportacion.SelectedValue == ""))
            {
                if (LblMensajeImportacion.Text == "")
                    LblMensajeImportacion.Text = LblMensajeImportacion.Text + "Debe seleccionar el producto";
                else
                    LblMensajeImportacion.Text = LblMensajeImportacion.Text + ", debe seleccionar el producto";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrProductoImportacion.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ExisteProducto(int Codigo_Producto)
        {
            bool Existe = false;
            for (int i = 0; i < GrdProductosExportacion.Items.Count; i++)
            {
                if (Convert.ToInt32(GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"]) == Codigo_Producto)
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        bool ExisteProductoImportacion(int Codigo_Producto)
        {
            bool Existe = false;
            for (int i = 0; i < GrdProductoImportacion.Items.Count; i++)
            {
                if (Convert.ToInt32(GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"]) == Codigo_Producto)
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        
        void CargarGridProducto()
        {
            for (int i = 0; i < GrdProductosExportacion.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Exportacion"].NewRow();
                rowNew["Codigo_Producto"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                rowNew["Nombre_Producto"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                rowNew["CodigoFSC"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                Ds_Temporal.Tables["Dt_Productos_Exportacion"].Rows.Add(rowNew);
            }
        }


        void CargarGridProductoImportacion()
        {
            for (int i = 0; i < GrdProductoImportacion.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Importacion"].NewRow();
                rowNew["Codigo_Producto"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                rowNew["Nombre_Producto"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                rowNew["CodigoFSC"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                Ds_Temporal.Tables["Dt_Productos_Importacion"].Rows.Add(rowNew);
            }
        }



        void BtnYes_Click(object sender, EventArgs e)
        {
            int GestionId = ClGestionRegistro.MaxGestionId(1);
            int Correlativo_Anual = ClGestionRegistro.MaxGestionId(2);
            string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;

            ClGestionRegistro.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(TxtSubRegionId.Text), 13, 3, Correlativo_Anual);
            
            int Telefono = 0;
            int TelefonoDos = 0;
            if (TxtTelefonoNotifica.Text != "")
                Telefono = Convert.ToInt32(TxtTelefonoNotifica.Text.Replace("-", ""));
            if (TxtTelDosEmpresa.Text != "")
                TelefonoDos = Convert.ToInt32(TxtTelDosEmpresa.Text.Replace("-", ""));
            int EmpresaId = 0;
            if (TxtEmpresaId.Text == "")
            {
                EmpresaId = ClEmpresa.Max_Persona_Juridica();
                ClEmpresa.Insertar_Empresa(EmpresaId, Convert.ToInt32(Session["UsuarioId"]), TxtNombreComercial.Text,Convert.ToInt32(TxtNoMercantil.Text), Convert.ToInt32(TxtFolio.Text), Convert.ToInt32(TxtLibro.Text), TxtNIT.Text, Convert.ToInt32(CboDe.SelectedValue), TxtObjeto.Text, Convert.ToInt32(TxtHorasTurno.Text), Convert.ToInt32(TxtTurnoDia.Text), Convert.ToInt32(TxtDiasYear.Text), Convert.ToInt32(TxtEmplFijo.Text), Convert.ToInt32(TxtEmplNoFijo.Text), TxtDireccion.Text, Convert.ToInt32(CboMunEmpresa.SelectedValue), Convert.ToInt32(TxtTelUnoEmpresa.Text.Replace("-", "")), TelefonoDos, TxtCorreoEmpresa.Text, Convert.ToInt32(CboCategoria.SelectedValue), 0);
                
            }
            else
                EmpresaId = Convert.ToInt32(TxtEmpresaId.Text);
            int DetCategoriaId = 0;
            if (CboSubCategoria.SelectedValue == "10")
                DetCategoriaId = Convert.ToInt32(CboTipoEmpresa.SelectedValue);
            int MunFuncionamiento = 0;
            if (DivMovibles.Visible == true)
                MunFuncionamiento = Convert.ToInt32(CboMunFuncionamiento.SelectedValue);
            int FabricaProductos = 0;
            if (CboSubCategoria.SelectedValue == "9")
                FabricaProductos = Convert.ToInt32(OptFabricaProductos.SelectedValue);
            int RegistroId = 0;
            if (TxtRnf.Text != "")
                RegistroId = ClRegistro.Get_RegistroId_Rnf(TxtRnf.Text);
            
            XmlDocument iInformacion = ClXml.CrearDocumentoXML("ProductosImportacion");
            if (GrdProductoImportacion.Items.Count > 0)
            {
                XmlNode iElementos = iInformacion.CreateElement("Productos");
                for (int i = 0; i < GrdProductoImportacion.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                    ClXml.AgregarAtributo("No", i + 1, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("CodigoProducto", GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
            }

            XmlDocument iInformacionExportacion = ClXml.CrearDocumentoXML("ProductosExportacion");
            if (GrdProductosExportacion.Items.Count > 0)
            {
                XmlNode iElementosExporta = iInformacionExportacion.CreateElement("Productos");
                for (int i = 0; i < GrdProductosExportacion.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacionExportacion.CreateElement("Item");

                    ClXml.AgregarAtributo("No", i + 1, iElementoDetalle);
                    iElementosExporta.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("CodigoProducto", GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"], iElementoDetalle);
                    iElementosExporta.AppendChild(iElementoDetalle);
                }
                iInformacionExportacion.ChildNodes[1].AppendChild(iElementosExporta);
            }
            ClGestionRegistro.Insertar_Gestion_Empresa(GestionId, DetCategoriaId, Convert.ToInt32(CboSubCategoria.SelectedValue), 5, EmpresaId, TxtDireccionFuncionamiento.Text, MunFuncionamiento, FabricaProductos, RegistroId, iInformacion, iInformacionExportacion, Convert.ToInt32(CboActividadPrincipal.SelectedValue), TxtDireccionNotifica.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Telefono, Convert.ToInt32(TxtCelularNotifica.Text.Replace("-", "")), TxtCorreoNotifica.Text, TxtObservaciones.Text, TxtNomFirma.Text, Convert.ToInt32(Session["UsuarioId"]),Convert.ToInt32(CboTipoPersona.SelectedValue),TxtNombreEmpresaSocial.Text);
            ClGestionRegistro.Insert_DatosEmpresa_Gestion(GestionId, Convert.ToInt32(TxtHorasTurno.Text), Convert.ToInt32(TxtEmplFijo.Text), Convert.ToInt32(TxtEmplNoFijo.Text), Convert.ToInt32(TxtDiasYear.Text), -1);
            ClEmpresa.Actualiza_DatosEmpresa(EmpresaId, Convert.ToInt32(TxtHorasTurno.Text), Convert.ToInt32(TxtEmplFijo.Text), Convert.ToInt32(TxtEmplNoFijo.Text), Convert.ToInt32(TxtDiasYear.Text), -1);
            for (int i = 0; i < GrdPropietarios.Items.Count; i++)
            {
                int PersonaId = Convert.ToInt32(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PersonaId"]);
                if (Convert.ToBoolean(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Existe"]) == false)
                {
                    PersonaId = ClPersona.MaxPersonaId();
                    int OrigenPersona = 1;
                    if (Convert.ToInt32(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PaisId"]) != 0)
                        OrigenPersona = 2;
                    ClPersona.Insertar_Persona_Propietario(PersonaId, GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Nombres"].ToString(), GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Apellidos"].ToString(), GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"].ToString().Replace("-", ""), Convert.ToDateTime(string.Format("{0:MM/dd/yyyy H:mm:ss}", GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_Id"])), OrigenPersona, Convert.ToInt32(GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PaisId"]));
                }
                ClGestionRegistro.Insert_Propietarios_Gestion(GestionId, PersonaId);
            }
            for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
            {
                int PersonaId = Convert.ToInt32(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PersonaIdRep"]);
                if (Convert.ToBoolean(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ExisteRep"]) == false)
                {
                    PersonaId = ClPersona.MaxPersonaId();
                    int OrigenPersona = 1;
                    if (Convert.ToInt32(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"]) != 0)
                        OrigenPersona = 2;
                    ClPersona.Insertar_Persona_Propietario(PersonaId, GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"].ToString(), GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"].ToString(), GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"].ToString().Replace("-", ""), Convert.ToDateTime(string.Format("{0:MM/dd/yyyy H:mm:ss}", GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["Fec_Venc_IdRep"])), OrigenPersona, Convert.ToInt32(GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"]));
                }
                ClGestionRegistro.Insert_Representantes_Gestion(GestionId, PersonaId);
            }
            string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + NUG + ". Por lo que solicitamos presentarse a la oficina Subregional " + TxtSubRegion.Text + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
            ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 0, "", "");
            Response.Redirect("~/WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG, true)) + "");

        }

        void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                LblTitConfirmacion.Text = "La Gestíon sera enviada al INAB, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void CboDepartamento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
        }

        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                VistaPrevia();
            }
        }

        void VistaPrevia()
        {
            
            Ds_Profesionales Ds_Formulario_Empresas = new Ds_Profesionales();
            Ds_Formulario_Empresas.Tables["Dt_Empresas"].Clear();
            DataRow row = Ds_Formulario_Empresas.Tables["Dt_Empresas"].NewRow();
            int HayPropietarios = GrdPropietarios.Items.Count;
            int TipoPersona = Convert.ToInt32(CboTipoPersona.SelectedValue);
            row["Requisitos"] = "Requisitos:\n1) Copia legalizada de la patente de comercio, con la especificación clara del objeto del negocio como actividad forestal; \n2) copia de constancia de inscripción en el Registro Tributario Unificado (RTU). Las sucursales deben contar con su propia patente de comercio;  ";
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario.";
                if (GrdRepresentantes.Items.Count > 0)
                    row["Requisitos"] = row["Requisitos"] + "\n4) Copia del documento personal de identificación del Representante Legal;  \n5) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
            }
            if ((TipoPersona == 2) || (GrdRepresentantes.Items.Count > 0))
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del Representante Legal;  \n4) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente.";
           
                
            row["Region"] = TxtRegion.Text;
            row["SubRegion"] = TxtSubRegion.Text;
            row["NUG"] = "-------";
            row["Fecha"] = string.Format("{0:dd/MM/yyyy}", ClUtilitarios.FechaDB());

            row["CategoriaEmpresa"] = CboSubCategoria.Text;
            if (CboSubCategoria.SelectedValue == "10")
            {
                if (Convert.ToInt32(CboTipoEmpresa.SelectedValue) > 0)
                    row["CategoriaEmpresa"] = row["CategoriaEmpresa"] + "-" + CboTipoEmpresa.Text;
            }
            row["NombreComercial"] = TxtNombreComercial.Text;
            row["NIT"] = TxtNIT.Text;
            row["NoRegMercantil"] = TxtNoMercantil.Text;
            row["Folio"] = TxtFolio.Text;
            row["Libro"] = TxtLibro.Text;
            row["De"] = CboDe.Text;
            row["Categoria"] = CboCategoria.Text;
            row["Objeto"] = TxtObjeto.Text;
            row["HorasTurno"] = TxtHorasTurno.Text;
            row["TurnoDia"] = TxtTurnoDia.Text;
            row["DiasYear"] = TxtDiasYear.Text;
            row["NoEmplFijo"] = TxtEmplFijo.Text;
            row["NoEmplNoFijo"] = TxtEmplNoFijo.Text;
            row["DireccionEmpresa"] = TxtDireccion.Text;
            row["DepEmpresa"] = CboDepEmpresa.Text;
            row["MunEmpresa"] = CboMunEmpresa.Text;
            row["TelefonoUno"] = TxtTelUnoEmpresa.Text;
            row["TelefonoDos"] = TxtTelDosEmpresa.Text;
            row["CorreoEmpresa"] = TxtCorreoEmpresa.Text;
            row["TipoPersona"] = CboTipoPersona.Text;
            row["RazonSocial"] = TxtNombreEmpresaSocial.Text;
            

            row["Direccion_Notificacion"] = TxtDireccionNotifica.Text;
            row["Municipio_Notificacion"] = CboMunicipio.Text;
            row["Departamento_Notificacion"] = CboDepartamento.Text;
            row["Telefono_Notificacion"] = TxtTelefonoNotifica.Text;
            row["Celular_Notificacion"] = TxtCelularNotifica.Text;
            row["Correo_Notificacion"] = TxtCorreoNotifica.Text;
            if (DivMovibles.Visible == true)
            {
                row["DireccionFuncionamiento"] = TxtDireccionFuncionamiento.Text;
                row["DepFuncionamiento"] = CboDepFuncionamiento.Text;
                row["MunFuncionamiento"] = CboMunFuncionamiento.Text;
            }
            else
            {
                row["DireccionFuncionamiento"] = "";
                row["DepFuncionamiento"] = "";
                row["MunFuncionamiento"] = "";
            }
            if (CboSubCategoria.SelectedValue == "9")
            {
                row["FabricaProductosForestales"] = OptFabricaProductos.SelectedItem.Text;
                row["IFRNF"] = TxtRnf.Text;
            }
            else
            {
                row["FabricaProductosForestales"] = "";
                row["IFRNF"] = "";
            }


            row["ActividadPrincipal"] = CboActividadPrincipal.Text;
            row["Observaciones"] = TxtObservaciones.Text;
            row["Nombre"] = TxtNomFirma.Text;
            row["TipoPersonaId"] = CboTipoPersona.SelectedValue;
            if (DivMovibles.Visible == true)
                row["EsMovil"] = "1";
            else
                row["EsMovil"] = "0";
            if (CboSubCategoria.SelectedValue == "9")
            
                row["EsExportador"] = "1";
            else
                row["EsExportador"] = "0";
            if (TxtRnf.Visible == true)
                row["EsFabricante"] = "1";
            else
                row["EsFabricante"] = "0";
            if (GrdRepresentantes.Items.Count == 0)
                row["TieneRepresentantes"] = "0";
            else
                row["TieneRepresentantes"] = "1";
            Ds_Formulario_Empresas.Tables["Dt_Empresas"].Rows.Add(row);
            if (CboTipoPersona.SelectedValue == "1")
            {
                Ds_Formulario_Empresas.Tables["Dt_Propietarios"].Clear();
                for (int i = 0; i < GrdPropietarios.Items.Count; i++)
                {
                    DataRow rowPropietario = Ds_Formulario_Empresas.Tables["Dt_Propietarios"].NewRow();
                    rowPropietario["Nombre"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Nombres"] + " " + GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Apellidos"];
                    if (GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["PaisId"].ToString() == "0")
                        rowPropietario["TipoId"] = "DPI";
                    else
                        rowPropietario["TipoId"] = "Pasporte";
                    rowPropietario["Id"] = GrdPropietarios.Items[i].OwnerTableView.DataKeyValues[i]["Dpi"];
                    Ds_Formulario_Empresas.Tables["Dt_Propietarios"].Rows.Add(rowPropietario);
                }
            }
            if (GrdRepresentantes.Items.Count > 0)
            {
                Ds_Formulario_Empresas.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_Empresas.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"] + " " + GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"];
                    if (GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"].ToString() == "0")
                        rowRepresentantes["TipoId"] = "DPI";
                    else
                        rowRepresentantes["TipoId"] = "Pasporte";
                    rowRepresentantes["Id"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"];
                    Ds_Formulario_Empresas.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            if (CboSubCategoria.SelectedValue == "9")
            {
                Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].Clear();
                for (int i = 0; i < GrdProductoImportacion.Items.Count; i++)
                {
                    DataRow rowProductoImportacion = Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].NewRow();
                    rowProductoImportacion["No"] = i + 1;
                    rowProductoImportacion["Producto"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                    rowProductoImportacion["CodigoFSC"] = GrdProductoImportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                    Ds_Formulario_Empresas.Tables["Dt_ProductosImportacion"].Rows.Add(rowProductoImportacion);
                }

                Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].Clear();
                for (int i = 0; i < GrdProductosExportacion.Items.Count; i++)
                {
                    DataRow rowPropietarioExportacion = Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].NewRow();
                    rowPropietarioExportacion["No"] = i + 1;
                    rowPropietarioExportacion["Producto"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["Nombre_Producto"];
                    rowPropietarioExportacion["CodigoFSC"] = GrdProductosExportacion.Items[i].OwnerTableView.DataKeyValues[i]["CodigoFSC"];
                    Ds_Formulario_Empresas.Tables["Dt_ProductosExportacion"].Rows.Add(rowPropietarioExportacion);
                }
            }
            Session["Ds_Empresas"] = Ds_Formulario_Empresas;
            RadWindow1.Title = "Vista Previa Insripción";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEmpresas.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }


        

       

        bool Valida()
        {
            LblMensaje.Text = "";
            BtnEror.Visible = false;
            bool HayError = false;

            if ((CboSubCategoria.Text == "") || (CboSubCategoria.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la subcategoría";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la subcategoría";
                HayError = true;
            }
            if ((CboSubCategoria.SelectedValue != "") && (Convert.ToInt32(CboSubCategoria.SelectedValue) == 10) && (CboTipoEmpresa.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de industria forestal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de industria forestal";
                HayError = true;
            }
            if (TxtNombreComercial.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el nombre Comercial de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el nombre Comercial de la empresa";
                HayError = true;
            }
            if (TxtNIT.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el número de NIT de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el número de NIT de la empresa";
                HayError = true;
            }
            if (TxtNoMercantil.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el no. mercantil de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el no. mercantil de la empresa";
                HayError = true;
            }
            if (TxtFolio.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el no. de folio de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el no. de folio de la empresa";
                HayError = true;
            }
            if (TxtLibro.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el no. de libre de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el no. de libro de la empresa";
                HayError = true;
            }
            if ((CboDe.Text == "") || (CboDe.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la categoría de empresa mercantil";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la categoría de empresa mercantil";
                HayError = true;
            }
            if ((CboCategoria.Text == "") || (CboCategoria.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la categoría de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la categoría de la empresa";
                HayError = true;
            }
            if (TxtObjeto.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el objeto de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el objeto de la empresa";
                HayError = true;
            }
            if (TxtHorasTurno.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar las horas/turno de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar las horas/turno de la empresa";
                HayError = true;
            }
            if (TxtTurnoDia.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar los turnos/dias de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar los turnos/dias de la empresa";
                HayError = true;
            }
            if (TxtDiasYear.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar los dias/año de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar los dias/año de la empresa";
                HayError = true;
            }
            if (TxtEmplFijo.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar la cantidad de empleados fijo de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar la cantidad de empleados fijo de la empresa";
                HayError = true;
            }
            if (TxtEmplNoFijo.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar la cantidad de empleados no fijos de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar la cantidad de empleados no fijos de la empresa";
                HayError = true;
            }
            if (TxtDireccion.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar la dirección de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar la dirección de la empresa";
                HayError = true;
            }
            if ((CboDepEmpresa.SelectedValue == "") || (CboDepEmpresa.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el departamento de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el departamento de la empresa";
                HayError = true;
            }
            if ((CboMunEmpresa.SelectedValue == "") || (CboMunEmpresa.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el municipio de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el municipio de la empresa";
                HayError = true;
            }
            if (TxtTelUnoEmpresa.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el teléfono uno de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el teléfono uno de la empresa";
                HayError = true;
            }
            if (TxtCorreoEmpresa.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el correo de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el correo de la empresa";
                HayError = true;
            }
            if ((TxtCorreoEmpresa.Text != "") && (ClUtilitarios.email_bien_escrito(TxtCorreoEmpresa.Text)) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el correo de la empresa en el formato correcto";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el correo de la empresa en el formato correcto";
                HayError = true;
            }
            if ((CboTipoPersona.SelectedValue == "") || (CboTipoPersona.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de persona de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de persona de la empresa";
                HayError = true;
            }
            if ((CboTipoPersona.SelectedValue != "") && (CboTipoPersona.SelectedValue == "2") && (TxtNombreEmpresaSocial.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el nombre de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el nombre de la empresa";
                HayError = true;
            }
            if ((CboTipoPersona.SelectedValue != "") && (CboTipoPersona.SelectedValue == "2") && (GrdRepresentantes.Items.Count == 0))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar al menos un representante legal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar al menos un representante legal";
                HayError = true;
            }
            
            if ((TxtEmpresaId.Text == "") && (CboDe.SelectedValue != "") && (ClEmpresa.Existe_Empresa(Convert.ToInt32(TxtNoMercantil.Text), Convert.ToInt32(TxtFolio.Text), Convert.ToInt32(TxtLibro.Text), Convert.ToInt32(CboDe.SelectedValue)) == true))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "La empresa ya existe en nuestros registros";
                else
                    LblMensaje.Text = LblMensaje.Text + ", la empresa ya existe en nuestros registros";
                HayError = true;
            }
            if ((CboTipoPersona.SelectedValue != "") && (CboTipoPersona.SelectedValue == "1") && (GrdPropietarios.Items.Count == 0))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar al menos un propietario de la empresa";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar al menos un propietario de la empresa";
                HayError = true;
            }
           
            if ((DivMovibles.Visible == true) && (TxtDireccionFuncionamiento.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar la dirección de funcionamiento";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar la dirección de funcionamiento";
                HayError = true;
            }
            if ((DivMovibles.Visible == true) && (CboDepFuncionamiento.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el departamento de funcionamiento";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el departamento de funcionamiento";
                HayError = true;
            }
            if ((DivMovibles.Visible == true) && (CboMunFuncionamiento.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el municipio de funcionamiento";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el municipio de funcionamiento";
                HayError = true;
            }
            if ((CboSubCategoria.SelectedValue == "9") && (OptFabricaProductos.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar si Fabrica Productos Forestales";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar si Fabrica Productos Forestales";
                HayError = true;
            }
            if ((CboSubCategoria.SelectedValue == "9") && (OptFabricaProductos.SelectedValue == "1") && (TxtRnf.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su registro de industria forestal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar su registro de industria forestal";
                HayError = true;
            }
            if ((CboSubCategoria.SelectedValue == "9") && (OptFabricaProductos.SelectedValue == "1") && (TxtRnf.Text != "") && (TxtRnf.Text.Substring(0,2) != "IF"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "El registro no corresponde a una industria forestal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", el registro no corresponde a una industria forestal";
                HayError = true;
            }
            if ((CboSubCategoria.SelectedValue == "9") && (OptFabricaProductos.SelectedValue == "1") && (TxtRnf.Text != "") && (TxtRnf.Text.Substring(0, 2) == "IF") && ClRegistro.Existe_Codigo_Registro(TxtRnf.Text) == 0)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "El registro de industria forestal no existe";
                else
                    LblMensaje.Text = LblMensaje.Text + ", el registro de industria forestal no existe";
                HayError = true;
            }
           
            if ((CboActividadPrincipal.SelectedValue == "") || (CboActividadPrincipal.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la actividad principal";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la actividad principal";
                HayError = true;
            }
            if (TxtDireccionNotifica.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su dirección de notificación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar su dirección de notificación";
                HayError = true;
            }
            if ((CboDepartamento.SelectedValue == "") || (CboDepartamento.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el departamento";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el departamento";
                HayError = true;
            }
            if ((CboMunicipio.SelectedValue == "") || (CboMunicipio.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el municipio";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el municipio";
                HayError = true;
            }
            if ((TxtCorreoNotifica.Text != "") && (ClUtilitarios.EsInstitucional(TxtCorreoNotifica.Text) == true))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "No puede agregar correos del dominio inab.gob.gt";
                else
                    LblMensaje.Text = LblMensaje.Text + ", No puede agregar correos del dominio inab.gob.gt";
                HayError = true;
            }
            if (TxtNomFirma.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su nombre y firma";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar su nombre y firma";
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

        


        void CargaInfo(int EmpresaId)
        {
            DataSet dsEmpresa = ClEmpresa.Get_Datos_Empresa(EmpresaId);
            TxtNombreComercial.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Nombre"].ToString();
            TxtNIT.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Nit"].ToString();
            TxtNoMercantil.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Numero"].ToString();
            TxtFolio.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Folio"].ToString();
            TxtLibro.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Libro"].ToString();
            CboDe.SelectedItem.Text = dsEmpresa.Tables["DATOS"].Rows[0]["CategoriaEmpresaMercantil"].ToString();
            CboDe.SelectedItem.Value = dsEmpresa.Tables["DATOS"].Rows[0]["Categoria_EmpresaMercantilId"].ToString();
            CboCategoria.SelectedItem.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Categoria_Empresa"].ToString();
            CboCategoria.SelectedItem.Value = dsEmpresa.Tables["DATOS"].Rows[0]["Categoria_EmpresaId"].ToString();
            TxtObjeto.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Objeto"].ToString();
            TxtHorasTurno.Text = dsEmpresa.Tables["DATOS"].Rows[0]["HorasTurno"].ToString();
            TxtTurnoDia.Text = dsEmpresa.Tables["DATOS"].Rows[0]["TurnoDia"].ToString();
            TxtDiasYear.Text = dsEmpresa.Tables["DATOS"].Rows[0]["DiasYear"].ToString();
            TxtEmplFijo.Text = dsEmpresa.Tables["DATOS"].Rows[0]["NoEmpleados_Fijo"].ToString();
            TxtEmplNoFijo.Text = dsEmpresa.Tables["DATOS"].Rows[0]["NoEmpleados_NoFijos"].ToString();
            TxtDireccion.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Direccion"].ToString();
            CboDepEmpresa.SelectedItem.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Departamento"].ToString();
            CboDepEmpresa.SelectedItem.Value = dsEmpresa.Tables["DATOS"].Rows[0]["DepartamentoId"].ToString();
            CboMunEmpresa.SelectedItem.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Municipio"].ToString();
            CboMunEmpresa.SelectedItem.Value = dsEmpresa.Tables["DATOS"].Rows[0]["MunicipioId"].ToString();
            TxtTelUnoEmpresa.Text = dsEmpresa.Tables["DATOS"].Rows[0]["TelefonoUno"].ToString();
            if (dsEmpresa.Tables["DATOS"].Rows[0]["TelefonoDos"].ToString() != "")
                TxtTelDosEmpresa.Text = dsEmpresa.Tables["DATOS"].Rows[0]["TelefonoDos"].ToString();
            TxtCorreoEmpresa.Text = dsEmpresa.Tables["DATOS"].Rows[0]["Correo"].ToString();
            
            

            DataSet dsRegioSubregionEmpresa = ClInmueble.Get_Region_Subregion_MunicipioId(Convert.ToInt32(dsEmpresa.Tables["DATOS"].Rows[0]["MunicipioId"]));
            TxtRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["Region"].ToString();
            TxtSubRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubRegion"].ToString();
            TxtSubRegionId.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubregionId"].ToString();
            dsRegioSubregionEmpresa.Clear();
            dsEmpresa.Clear();
            
        }


        void LimpiarProducto()
        {
            CboProducto.ClearSelection();
        }

        void LimpiarProductoImportacion()
        {
            CboProductoImportacion.ClearSelection();
        }


        void AgregaProducto()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Exportacion"].NewRow();
            rowNew["Codigo_Producto"] = CboProducto.SelectedValue;
            rowNew["Nombre_Producto"] = CboProducto.Text;
            rowNew["CodigoFSC"] = ClCatalogos.Get_CodigoFSC(Convert.ToInt32(CboProducto.SelectedValue));
            Ds_Temporal.Tables["Dt_Productos_Exportacion"].Rows.Add(rowNew);
        }

        void AgregaProductoImportacion()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_Productos_Importacion"].NewRow();
            rowNew["Codigo_Producto"] = CboProductoImportacion.SelectedValue;
            rowNew["Nombre_Producto"] = CboProductoImportacion.Text;
            rowNew["CodigoFSC"] = ClCatalogos.Get_CodigoFSC(Convert.ToInt32(CboProductoImportacion.SelectedValue));
            Ds_Temporal.Tables["Dt_Productos_Importacion"].Rows.Add(rowNew);
        }
        void NuevaEmpresa()
        {
            DesbloqueCamposEmpresa();
            LimpiaCamposEmpresa();
        }


        void DesbloqueCamposEmpresa()
        {
            TxtNombreComercial.Enabled = true;
            TxtNIT.Enabled = true;
            TxtNoMercantil.Enabled = true;
            TxtFolio.Enabled = true;
            TxtLibro.Enabled = true;
            CboDe.Enabled = true;
            CboCategoria.Enabled = true;
            TxtObjeto.Enabled = true;
            TxtHorasTurno.Enabled = true;
            TxtTurnoDia.Enabled = true;
            TxtDiasYear.Enabled = true;
            TxtEmplFijo.Enabled = true;
            TxtEmplNoFijo.Enabled = true;
            TxtDireccion.Enabled = true;
            CboDepEmpresa.Enabled = true;
            CboMunEmpresa.Enabled = true;
            TxtTelUnoEmpresa.Enabled = true;
            TxtTelDosEmpresa.Enabled = true;
            TxtCorreoEmpresa.Enabled = true;
            CboTipoPersona.Enabled = true;
            TxtNombreEmpresaSocial.Enabled = true;
            CboTipoIdPropietario.Enabled = true;
        }

        void LimpiaCamposEmpresa()
        {
            TxtNombreComercial.Text = "";
            TxtNIT.Text = "";
            TxtNoMercantil.Text = "";
            TxtFolio.Text = "";
            TxtLibro.Text = "";
            CboDe.Text = "";
            CboCategoria.Text = "";
            TxtObjeto.Text = "";
            TxtHorasTurno.Text = "";
            TxtTurnoDia.Text = "";
            TxtDiasYear.Text = "";
            TxtEmplFijo.Text = "";
            TxtEmplNoFijo.Text = "";
            TxtDireccion.Text = "";
            CboDepEmpresa.Text = "";
            CboMunEmpresa.Text = "";
            TxtTelUnoEmpresa.Text = "";
            TxtTelDosEmpresa.Text = "";
            TxtCorreoEmpresa.Text = "";
            CboTipoPersona.Text = "";
            TxtNombreEmpresaSocial.Text = "";
            CboTipoIdPropietario.Text = "";
        }
    }
}