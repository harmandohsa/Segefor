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
    public partial class Wfrm_Inscripcion_PlantacionForestal : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Inmueble ClInmueble;
        Cl_Gestion_Registro ClGestionRegistro;
        Cl_Xml ClXml;
        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        DataView dv = new DataView();
        DataSet resultXls = new DataSet();
        Cl_Poligono ClPoligono;
        Cl_Manejo ClManejo;

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
            ClPoligono = new Cl_Poligono();
            ClManejo = new Cl_Manejo();
            

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

            CboTipoPlantacion.SelectedIndexChanged += CboTipoPlantacion_SelectedIndexChanged;
            ChkConIncentivos.CheckedChanged += ChkConIncentivos_CheckedChanged;
            CboFinca.SelectedIndexChanged += CboFinca_SelectedIndexChanged;
            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            CboDepartamento.SelectedIndexChanged += CboDepartamento_SelectedIndexChanged;
            BtnEnviar.Click += BtnEnviar_Click;
            BtnYes.Click += BtnYes_Click;
            GrdInventario.NeedDataSource += GrdInventario_NeedDataSource;
            btnAddEspecie.ServerClick += btnAddEspecie_ServerClick;
            GrdInventario.ItemCommand += GrdInventario_ItemCommand;
            GrdInventario.PreRender += GrdInventario_PreRender;
            BtnCargar.ServerClick += BtnCargar_ServerClick;
            GrdPoligono.NeedDataSource += GrdPoligono_NeedDataSource;
            ChkIngNomFinca.CheckedChanged += ChkIngNomFinca_CheckedChanged;
            CboTipoDocumento.SelectedIndexChanged += CboTipoDocumento_SelectedIndexChanged;
            BtnNuevaFinca.ServerClick += BtnNuevaFinca_ServerClick;
            btnGrabarFinca.ServerClick += btnGrabarFinca_ServerClick;
            GrdInmuebles.NeedDataSource += GrdInmuebles_NeedDataSource;
            CboDepartamentoFinca.SelectedIndexChanged += CboDepartamentoFinca_SelectedIndexChanged;
            BtnCargarPolFinca.ServerClick += BtnCargarPolFinca_ServerClick;
            GrdPoligonoFinca.NeedDataSource += GrdPoligonoFinca_NeedDataSource;
            BtnAddFincaPlan.ServerClick += BtnAddFincaPlan_ServerClick;
            GrdInmuebles.ItemCommand += GrdInmuebles_ItemCommand;
            GrdPropietarios.NeedDataSource += GrdPropietarios_NeedDataSource;
            CboTipoPersona.SelectedIndexChanged += CboTipoPersona_SelectedIndexChanged;
            CboTipoIdPropietario.SelectedIndexChanged += CboTipoIdPropietario_SelectedIndexChanged;
            BtnValidarDpi.ServerClick += BtnValidarDpi_ServerClick;
            BtnValidarPasaporte.ServerClick += BtnValidarPasaporte_ServerClick;
            BtnAddPropietario.ServerClick += BtnAddPropietario_ServerClick;
            BtnGrabarNomEmpresa.ServerClick += BtnGrabarNomEmpresa_ServerClick;
            GrdPropietarios.ItemCommand += GrdPropietarios_ItemCommand;
            BtnGrabarAreas.ServerClick += BtnGrabarAreas_ServerClick;
            CboTipoIdentificacionRep.SelectedIndexChanged += CboTipoIdentificacionRep_SelectedIndexChanged;
            BtnValidarDpiRep.ServerClick += BtnValidarDpiRep_ServerClick;
            BtnValidarPasaporteRep.ServerClick += BtnValidarPasaporteRep_ServerClick;
            BtnAddRepresentante.ServerClick += BtnAddRepresentante_ServerClick;
            GrdRepresentantes.NeedDataSource += GrdRepresentantes_NeedDataSource;
            GrdRepresentantes.ItemCommand += GrdRepresentantes_ItemCommand;
            

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(37, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 37);
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
                ClGestionRegistro.Eliminar_Datos_Temp_Registro(Convert.ToInt32(Session["UsuarioId"]));
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSubCategoriasRegistro(2, Convert.ToInt32(Session["PersonaId"])), CboTipoPlantacion, "SubCategoriaId", "Nombre_Subcategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoPlantacion, "Tipo de Plantación");
                ClUtilitarios.LlenaCombo(ClGestionRegistro.Inmuebles_GetAll_Registro(Convert.ToInt32(Session["UsuarioId"])), CboFinca, "InmuebleId", "Finca");
                ClUtilitarios.AgregarSeleccioneCombo(CboFinca, "Finca/Inmueble");
                if (CboFinca.Items.Count > 1)
                    BtnAddFincaPlan.Visible = true;
                DataSet dsPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                TxtOrigenPersonaId.Text = dsPersona.Tables["Datos"].Rows[0]["Origen_PersonaId"].ToString(); 
                dsPersona.Clear();
                if (TxtOrigenPersonaId.Text == "2")
                    LblDirecNotifica.InnerText = "Dirección en Guatemala";
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Especie(), CboEspecie, "EspecieId", "Especie");
                ClUtilitarios.AgregarSeleccioneCombo(CboEspecie, "Especie");
                ClUtilitarios.LlenaCombo(ClCatalogos.Titulo_GetAll(), CboTitulo, "TituloNotarioId", "TituloNotario");
                ClUtilitarios.LlenaCombo(ClCatalogos.Area_Protegida_GetAll(), CboArea, "AreaProtegidaId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboArea, "Área");
                ClUtilitarios.LlenaCombo(ClCatalogos.TipoDoc_Propiedad_GetAll(), CboTipoDocumento, "TipoDoc_PropiedadId", "TipoDocPropiedad");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoDocumento, "Tipo de Documento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamentoFinca, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamentoFinca, "Departamento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(1), CboMunicipioFinca, "MunicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioFinca, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdPropietario, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdPropietario, "Tipo de Identificación");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPais, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPais, "País");
                //Representantes
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdentificacionRep, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdentificacionRep, "Tipo de Identificación");

                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPaisRep, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPaisRep, "País");
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

        void GrdRepresentantes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsRepresentantes.Tables["Representantes"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsRepresentantes, GrdRepresentantes, "Representantes");
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

        void BtnGrabarAreas_ServerClick(object sender, EventArgs e)
        {

            LblGoodAreas.Text = "";
            LblErrAreas.Text = "";
            DivErrAreas.Visible = false;
            DivGoodAreas.Visible = false;
            GrabarAreas();
        }

        void GrabarAreas()
        {
            if ((TxtAreaForestal.Text == "") || (TxtAreaForestal.Text == "0"))
            {
                DivErrAreas.Visible = true;
                LblErrAreas.Text = "Debe Ingresar el área forestal";
            }
            else if (Convert.ToDouble(TxtAreaForestal.Text) > Convert.ToDouble(TxtAreaFincaValida.Text))
            {
                DivErrAreas.Visible = true;
                LblErrAreas.Text = "El Área forestal debe ser igual o menor a el área de la finca";
            }
            else
            {
                int UsuarioId = Convert.ToInt32(Session["UsuarioId"]);
                int InmuebleId = Convert.ToInt32(TxtInmuebleId.Text);
                ClGestionRegistro.Eliminar_AreasInmuebleRegistro(UsuarioId, InmuebleId);
                ClGestionRegistro.Insertar_AreasInmuebleRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToDouble(ClUtilitarios.IIf(TxtAreaForestal.Text == "", 0, TxtAreaForestal.Text)));
                DivGoodAreas.Visible = true;
                LblGoodAreas.Text = "Áreas grabados correctamente";
            }
            
        }

        void GrdPropietarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClGestionRegistro.Elimina_PropietarioFinca_Registro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PersonaId"]));
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
                DivGoodPropietario.Visible = true;
                LblMansajeGoodPropietario.Text = "Propietario eliminado de la finca";
            }
        }

        void BtnGrabarNomEmpresa_ServerClick(object sender, EventArgs e)
        {
            ClGestionRegistro.ActualizarNombre_EmpresaTempFincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text), TxtNombreEmpresaSocial.Text,TxtNit.Text);
        }

        void BtnAddPropietario_ServerClick(object sender, EventArgs e)
        {
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            LblMansajeBadPropietario.Text = "";
            LblMansajeGoodPropietario.Text = "";
            bool HayError = false;
            int PaisId;
            string Dpi;
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
            if (TxtFecVenc.DateInput.SelectedDate < DateTime.Now)
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Documento De Identificación Vencido";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Documento De Identificación Vencido";
                HayError = true;
            }

            if (HayError == true)
                DivBadPropietario.Visible = true;
            else
            {
                string DocaValidar = "";
                if (Convert.ToInt32(CboTipoIdPropietario.SelectedValue) == 1)
                    DocaValidar = TxtDpi.Text;
                else
                    DocaValidar = TxtPasaportePropietario.Text;
                if (ExistePropietario(DocaValidar) == true)
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
                        PaisId = 0;
                        item["Dpi"] = TxtDpi.Text;
                        Dpi = TxtDpi.Text.Replace("-", "").ToString();
                    }
                    else
                    {
                        item["PaisId"] = CboPais.SelectedValue;
                        PaisId = Convert.ToInt32(CboPais.SelectedValue);
                        item["Dpi"] = TxtPasaportePropietario.Text;
                        Dpi = TxtPasaportePropietario.Text;
                    }
                    DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                    DivGoodPropietario.Visible = true;
                    LblMansajeGoodPropietario.Text = "Propietario Agregado Exitosamente";
                    GrdPropietarios.Rebind();
                    AgregarPropietarioAlPlan(0, 0, TxtNombrePropietario.Text, TxtApellidoPropietario.Text, Convert.ToDateTime(TxtFecVenc.SelectedDate), PaisId, Dpi, Convert.ToInt32(CboTipoIdPropietario.SelectedValue));
                    LimiarPropietario();
                    DivNombresProp.Visible = false;
                    DivApeProp.Visible = false;
                    DivAddProp.Visible = false;
                    DivFecVencimiento.Visible = false;
                }

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
                            AgregarPropietarioAlPlan(1, Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]), dsDatos.Tables["DATOS"].Rows[0]["Nombres"].ToString(), dsDatos.Tables["DATOS"].Rows[0]["Apellidos"].ToString(), Convert.ToDateTime(dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]), Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PaisId"]), TxtDpi.Text, 2);
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
                            dsDatos = ClPersona.Datos_Persona_Dpi(TxtDpi.Text.Replace("-", ""), 1);
                            DataRow item = DsPropietarios.Tables["Propietarios"].NewRow();
                            item["Existe"] = 1;
                            item["PersonaId"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                            item["Dpi"] = TxtDpi.Text;
                            item["Nombres"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                            item["Apellidos"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                            item["Fec_Venc_Id"] = string.Format("{0:dd/MM/yyyy}", dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            item["PaisId"] = 0;
                            DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                            DivGoodPropietario.Visible = true;
                            AgregarPropietarioAlPlan(1, Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]), dsDatos.Tables["DATOS"].Rows[0]["Nombres"].ToString(), dsDatos.Tables["DATOS"].Rows[0]["Apellidos"].ToString(), Convert.ToDateTime(dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]), 0, TxtDpi.Text, 1);
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
            TxtFecVenc.Clear();
        }

        void AgregarPropietarioAlPlan(int Existe, int PersonaId, string Nombres, string Apellidos, DateTime FecVence, int PaisId, string Dpi, int Origen_Persona)
        {
            if (Existe == 0)
            {
                PersonaId = ClPersona.MaxPersonaId();
                ClPersona.Insertar_Persona_Propietario(PersonaId, Nombres, Apellidos, Dpi, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", FecVence)), Origen_Persona, PaisId);
            }
            ClGestionRegistro.InsertTempFincaPropietarioRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text), PersonaId);

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
            ClGestionRegistro.ActualizarTipoPersonaTempFincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(CboTipoPersona.SelectedValue));
        }

        void GrdPropietarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsPropietarios.Tables["Propietarios"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsPropietarios, GrdPropietarios, "Propietarios");
        }

        void GrdInmuebles_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DivErrFinca.Visible = false;
            LblErrFinca.Text = "";
            if (e.CommandName == "CmdDel")
            {
                ClGestionRegistro.Eliminar_AreasInmuebleRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                ClGestionRegistro.EliminarTempPropietariosRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                ClGestionRegistro.EliminarTempFincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                LblGoodFinca.Text = "Finca eliminada de la gestión";
                DivGoodFinca.Visible = true;
                ClUtilitarios.LlenaCombo(ClGestionRegistro.Inmuebles_GetAll_Registro(Convert.ToInt32(Session["UsuarioId"])), CboFinca, "InmuebleId", "Finca");
                ClUtilitarios.AgregarSeleccioneCombo(CboFinca, "Finca/Inmueble");
                if (CboFinca.Items.Count > 1)
                    BtnAddFincaPlan.Visible = true;
                else
                    BtnAddFincaPlan.Visible = false;
                GrdInmuebles.Rebind();
                BloquearFinca();
                LimpiarFinca();
                
                LimpiarAreas();
                DivPropietariosFinca.Visible = false;
                DivUsosAreas.Visible = false;
            }
            if (e.CommandName == "CmdPropietarios")
            {
                DivUsosAreas.Visible = false;
                DivAreaForestal.Visible = false;
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                DivPropietariosFinca.Visible = true;
                PropietariosYaExistentes(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
                GrdPropietarios.Rebind();
                DataSet dsTipoPersona = ClGestionRegistro.GetTipoPersonaTempRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
                if (dsTipoPersona.Tables["Datos"].Rows.Count > 0)
                {
                    CboTipoPersona.SelectedValue = dsTipoPersona.Tables["Datos"].Rows[0]["Tipo_PersonaId"].ToString();
                    CboTipoPersona.Text = dsTipoPersona.Tables["Datos"].Rows[0]["Tipo_Persona"].ToString();
                    if (Convert.ToInt32(CboTipoPersona.SelectedValue) == 2)
                    {
                        DivJuridica.Visible = true;
                        DivPropietarios.Visible = false;
                        TxtNombreEmpresaSocial.Text = dsTipoPersona.Tables["Datos"].Rows[0]["Nombre_Empresa"].ToString();
                    }
                    else
                    {
                        DivJuridica.Visible = false;
                        DivPropietarios.Visible = true;
                        DivGrigPropietarios.Visible = true;
                    }
                    dsTipoPersona.Clear();
                    int TipoDocPropiedad = ClCatalogos.Get_TipoPropietarioInmueble(2, Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(Session["UsuarioId"]));
                    if (TipoDocPropiedad == 1)
                        TitPropietarios.Text = "Propietarios";
                    else
                        TitPropietarios.Text = "Titulares";
                }
                else
                {
                    TxtNombreEmpresaSocial.Text = "";
                    ClUtilitarios.LlenaCombo(ClCatalogos.Tipo_Persona(), CboTipoPersona, "Tipo_PersonaId", "Tipo_Persona");
                    ClUtilitarios.AgregarSeleccioneCombo(CboTipoPersona, "Tipo Persona");
                    DivPropietarios.Visible = false;
                    DivJuridica.Visible = false;
                }

            }
            if (e.CommandName == "CmdAreas")
            {
                DivPropietariosFinca.Visible = false;
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                TxtAreaFincaValida.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Area"].ToString();
                DivUsosAreas.Visible = true;
                DivAreaForestal.Visible = true;
                LimpiarAreas();
                RetornoAreas();
            }
            if (e.CommandName == "CmdEdit")
            {
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                int ExisteRegistro = ClGestionRegistro.Finca_EnGestion_Registro(Convert.ToInt32(TxtInmuebleId.Text));
                int ExisteManejo = ClManejo.Finca_EnGestion_Manejo_Temporal(Convert.ToInt32(TxtInmuebleId.Text));
                if ((ExisteRegistro > 0) || (ExisteManejo > 0))
                {
                    DivErrFinca.Visible = true;
                    LblErrFinca.Text = "Esta finca no se puede modificar porque ya es parte de algúna gestión";
                    BloquearFinca();
                }
                else
                {
                    DesbloqueFinca();
                    CargaInfo(Convert.ToInt32(TxtInmuebleId.Text));
                }
            }
        }

        void RetornoAreas()
        {
            DataSet dsDatosAreas = ClGestionRegistro.GetAreasFinca_Registro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
            if (dsDatosAreas.Tables["Datos"].Rows.Count > 0)
                TxtAreaForestal.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Area"].ToString();
            dsDatosAreas.Clear();

        }

        void LimpiarAreas()
        {
            TxtAreaForestal.Text = "";
        }

        void PropietariosYaExistentes(int UsuarioId, int InmuebleId)
        {
            //DsPropietarios.Tables["Propietarios"].Clear();
            DataSet dsPropietariosFinca = ClGestionRegistro.GetPropietarios_Inmuebles_Registro(UsuarioId, InmuebleId);
            for (int i = 0; i < dsPropietariosFinca.Tables["Datos"].Rows.Count; i++)
            {
                DataRow itemGrid = DsPropietarios.Tables["Propietarios"].NewRow();
                itemGrid["Existe"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["Existe"];
                itemGrid["PersonaId"] = Convert.ToInt64(dsPropietariosFinca.Tables["Datos"].Rows[i]["PersonaId"]);
                itemGrid["Dpi"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["Dpi"];
                itemGrid["Nombres"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["Nombres"];
                itemGrid["Apellidos"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["Apellidos"];
                itemGrid["Fec_Venc_Id"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["Fec_Ven_Id"];
                itemGrid["PaisId"] = dsPropietariosFinca.Tables["Datos"].Rows[i]["PaisId"];
                DsPropietarios.Tables["Propietarios"].Rows.Add(itemGrid);
            }
        }

        void AgregarFincaGestion()
        {
            OcultaMensajes();
            if (CboFinca.SelectedValue == "")
            {
                DivErrFinca.Visible = true;
                LblErrFinca.Text = "Debe seleccionar la finca";
            }
            else
            {
                ClGestionRegistro.InsertTempFincaGestionRegistro(Convert.ToInt32(CboFinca.SelectedValue), Convert.ToInt32(Session["UsuarioId"]));
                LblGoodFinca.Text = "Finca agregada a la gestión";
                DivGoodFinca.Visible = true;
                ClUtilitarios.LlenaCombo(ClGestionRegistro.Inmuebles_GetAll_Registro(Convert.ToInt32(Session["UsuarioId"])), CboFinca, "InmuebleId", "Finca");
                ClUtilitarios.AgregarSeleccioneCombo(CboFinca, "Finca/Inmueble");
                if (CboFinca.Items.Count > 1)
                    BtnAddFincaPlan.Visible = true;
                else
                    BtnAddFincaPlan.Visible = false;
                GrdInmuebles.Rebind();
                BloquearFinca();
                LimpiarFinca();

            }
        }

        void BtnAddFincaPlan_ServerClick(object sender, EventArgs e)
        {
            AgregarFincaGestion();
        }

        void GrdPoligonoFinca_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_PoligonoBosque"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPoligonoFinca, "Dt_PoligonoBosque");
        }

        void BtnCargarPolFinca_ServerClick(object sender, EventArgs e)
        {
            DivErrPoligono.Visible = false;
            if (UploadPolFinca.UploadedFiles.Count > 0)
            {
                string Extension = UploadPolFinca.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPoligono.Visible = true;
                    LblErrPoligino.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = UploadPolFinca.UploadedFiles[0].InputStream;
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        resultXls = excelReader.AsDataSet();

                        Ds_Temporal.Tables["Dt_PoligonoBosque"].Clear();
                        foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                        {
                            if (iDtRow["X"].ToString() != "")
                            {
                                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoBosque"].NewRow();
                                rowNew["Id"] = iDtRow["Poligono"];
                                rowNew["X"] = iDtRow["X"];
                                rowNew["Y"] = iDtRow["Y"];
                                Ds_Temporal.Tables["Dt_PoligonoBosque"].Rows.Add(rowNew);
                            }

                        }

                        GrdPoligonoFinca.Rebind();
                    }
                    catch (Exception ex)
                    {
                        DivErrPoligono.Visible = true;
                        LblErrPoligino.Text = ex.Message;  
                    }
                }
                
            }
            else
            {
                DivErrPoligono.Visible = true;
                LblErrPoligino.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }

           
        }

        void CboDepartamentoFinca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamentoFinca.SelectedValue)), CboMunicipioFinca, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioFinca, "Municipio");
        }

        void GrdInmuebles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClGestionRegistro.Get_TempFincaRegistro(Convert.ToInt32(Session["UsuarioId"])), GrdInmuebles);
        }

        bool ValidaFinca()
        {
            LblErrFinca.Text = "";
            DivErrFinca.Visible = false;
            bool HayError = false;
            int AreaId = 0;
            if (CboArea.SelectedValue != "")
                AreaId = Convert.ToInt32(CboArea.SelectedValue);

            if (TxtUbicacionOeste.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar la Coordenada X";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe ingresar la Coordenada X";
                HayError = true;
            }
            if ((TxtUbicacionOeste.Text != "") && ((Convert.ToDouble(TxtUbicacionOeste.Text) > 742321.341707) || (Convert.ToDouble(TxtUbicacionOeste.Text) < 312146.719860)))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "La Coordenada X esta fuera del rango de Guatemala";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", la Coordenada X esta fuera del rango de Guatemala";
                HayError = true;
            }
            if (TxtUbicacionNorte.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar la Coordenada Y";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe ingresar la Coordenada Y";
                HayError = true;
            }
            if ((TxtUbicacionNorte.Text != "") && ((Convert.ToDouble(TxtUbicacionNorte.Text) > 1970263.493114) || (Convert.ToDouble(TxtUbicacionNorte.Text) < 1519520.063473)))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "La Coordenada Y esta fuera del rango de Guatemala";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", la Coordenada Y esta fuera del rango de Guatemala";
                HayError = true;
            }
            if (CboTipoDocumento.SelectedValue == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar el tipo de documento de la propiedad";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe seleccionar el tipo de documento de la propiedad";
                HayError = true;
            }
            if (TxtFecEmi.DateInput.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar la Fecha de la Certificación de la propiedad";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar la Fecha de la Certificación de la propiedad";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "1") && (TxtNoFinca.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el número de finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el número de finca";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "1") && (TxtFolio.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el número de folio";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el número de folio";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "1") && (TxtLibro.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el número de libro";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el número de libro";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "1") && (TxtDe.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el de en la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el de en la finca";
                HayError = true;
            }
            
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "2") && (TxtMunEmiteDoc.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el nombre de la municipalidad que emite el documento";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el nombre de la municipalidad que emite el documento";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "3") && (TxtNoEscritura.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el número de escritura";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el número de escritura";
                HayError = true;
            }
            if ((CboTipoDocumento.SelectedValue != "") && (CboTipoDocumento.SelectedValue == "3") && (TxtNomNotario.Text == ""))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el nombre del notario";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe ingresar el nombre del notario";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") && (ClUtilitarios.EsMayorDays(120, Convert.ToDateTime(TxtFecEmi.SelectedDate)) == true))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "La Certificación de la propiedad no puede tener mas de 120 dias de antiguedad";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", La Certificación de la propiedad no puede tener mas de 120 dias de antiguedad";
                HayError = true;
            }
            if (CboDepartamentoFinca.SelectedValue == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar el departamento de la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe seleccionar el departamento de la finca";
                HayError = true;
            }
            if (CboMunicipioFinca.SelectedValue == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar el municipio de la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe seleccionar el municipio de la finca";
                HayError = true;
            }
            if (OptAreasPro.SelectedValue == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar si la propiedad se encuentra o no en áreas protegidas";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe seleccionar si la propiedad se encuentra o no en áreas protegidas";
                HayError = true;
            }
            if (OptAreasPro.SelectedValue == "1" && CboArea.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar el área protegida";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Debe seleccionar el área protegida";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") && (CboMunicipio.SelectedValue != "") && (CboTipoDocumento.SelectedValue != "") && (ClInmueble.Existe_Propiedad_Usuario(Convert.ToInt32(Session["UsuarioId"]), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDecimal(TxtAreaFinca.Text)) == true))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Esta propiedad ya fue ingresada";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Esta propiedad ya fue ingresada";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") && (CboMunicipio.SelectedValue != "") && (CboTipoDocumento.SelectedValue != "") && (ClInmueble.Existe_Propiedad_OtroUsuario(Convert.ToInt32(Session["UsuarioId"]), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDecimal(TxtAreaFinca.Text)) == true))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Esta propiedad ya fue ingresada por otro usuario";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Esta propiedad ya fue ingresada por otro usuario";
                HayError = true;
            }
            if (TxtAreaFinca.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe ingresar el área de la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe ingresar el área de la finca";
                HayError = true;
            }
            //if (TxtColSur.Text == "")
            //{
            //    if (LblErrFinca.Text == "")
            //        LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia sur";
            //    else
            //        LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia sur";
            //    HayError = true;
            //}
            //if (TxtColEste.Text == "")
            //{
            //    if (LblErrFinca.Text == "")
            //        LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia este";
            //    else
            //        LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia este";
            //    HayError = true;
            //}
            //if (TxtColOeste.Text == "")
            //{
            //    if (LblErrFinca.Text == "")
            //        LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia oeste";
            //    else
            //        LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia oeste";
            //    HayError = true;
            //}
            //if (GrdPoligonoFinca.Items.Count == 0)
            //{
            //    if (LblErrFinca.Text == "")
            //        LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado el polígono de la finca";
            //    else
            //        LblErrFinca.Text = LblErrFinca.Text + ", no ha ingresado el polígono de la finca";
            //    HayError = true;
            //}

            if (HayError == true)
            {
                DivErrFinca.Visible = true;
                return false;
            }

            else
                return true;
        }

        void Grabar()
        {
            if (ValidaFinca() == true)
            {

                bool GraboFinca = true;
                int AreaId = 0;
                if (CboArea.SelectedValue != "")
                    AreaId = Convert.ToInt32(CboArea.SelectedValue);
                int InmuebleId = ClInmueble.Max_Inmueble();
                ClUsuario.Insertar_Actividad_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                ClInmueble.Inserta_InmuebleT(InmuebleId, Convert.ToInt32(Session["UsuarioId"]), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipioFinca.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), TxtFolio.Text, Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDouble(TxtAreaFinca.Text), Convert.ToDouble(TxtUbicacionNorte.Text), Convert.ToDouble(TxtUbicacionOeste.Text), TxtColNorte.Text, TxtColSur.Text, TxtColEste.Text, TxtColOeste.Text);
                if (RadUploadFile.UploadedFiles.Count > 0)
                {
                    Stream fileStream = RadUploadFile.UploadedFiles[0].InputStream;
                    byte[] attachmentBytes = new byte[fileStream.Length];
                    fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                    ClInmueble.Actualiza_Archivo(InmuebleId, attachmentBytes, RadUploadFile.UploadedFiles[0].ContentType, RadUploadFile.UploadedFiles[0].FileName);
                    fileStream.Close();
                }

                if (GrdPoligonoFinca.Items.Count > 0)
                {
                    XmlDocument iInformacionPol = ClXml.CrearDocumentoXML("Poligonos");
                    XmlNode iElementoPoligono = iInformacionPol.CreateElement("Puntos");

                    for (int i = 0; i < GrdPoligonoFinca.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacionPol.CreateElement("Item");
                        ClXml.AgregarAtributo("Id", GrdPoligonoFinca.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                        ClXml.AgregarAtributo("X", GrdPoligonoFinca.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                        ClXml.AgregarAtributo("Y", GrdPoligonoFinca.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                        iElementoPoligono.AppendChild(iElementoDetalle);
                    }
                    iInformacionPol.ChildNodes[1].AppendChild(iElementoPoligono);
                    String iPoligonoGML = "";
                    string ErrorMapa = "";
                    if (ClPoligono.Actualizar_Poligono_Finca(iInformacionPol, ref InmuebleId, ref iPoligonoGML, ref ErrorMapa))
                    {

                    }
                    else
                    {
                        DivErrEspecie.Visible = true;
                        ClInmueble.Elimina_Inmueble(InmuebleId);
                        GraboFinca = false;
                        LblErrFinca.Text = ErrorMapa;
                        DivErrFinca.Visible = true;
                    }

                }
                if (GraboFinca == true)
                {
                    ClGestionRegistro.InsertTempFincaGestionRegistro(InmuebleId, Convert.ToInt32(Session["UsuarioId"]));
                    GrdInmuebles.Rebind();
                    DivGoodFinca.Visible = true;
                    LblGoodFinca.Text = "Finca/Inmueble agregado correctamente";
                    BloquearFinca();
                    LimpiarFinca();
                }


            }
        }

        void btnGrabarFinca_ServerClick(object sender, EventArgs e)
        {
            if (TxtInmuebleId.Text == "")
            {
                Grabar();
            }

            else
            {
                ClGestionRegistro.EliminarTempPropietariosRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
                ClGestionRegistro.Eliminar_AreasInmuebleRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
                ClGestionRegistro.EliminarTempFincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtInmuebleId.Text));
                ClInmueble.Sp_Elimina_PoligonoFinca(Convert.ToInt32(TxtInmuebleId.Text));
                ClInmueble.Elimina_Inmueble(Convert.ToInt32(TxtInmuebleId.Text));
                Grabar();
                AgregarFincaGestion();
                DivGoodFinca.Visible = true;
                LblGoodFinca.Text = "La finca fue modificada, debera agregar nuevamente los datos de los propietarios y áreas";
                TxtInmuebleId.Text = "";
                DivErrFinca.Visible = false;
            }


            
        }

        

        void BloquearFinca()
        {
            
            TxtUbicacionNorte.Enabled = false;
            TxtUbicacionOeste.Enabled = false;
            CboTipoDocumento.Enabled = false;
            TxtNoFinca.Enabled = false;
            TxtFolio.Enabled = false;
            TxtLibro.Enabled = false;
            TxtDe.Enabled = false;
            TxtNoCerti.Enabled = false;
            TxtMunEmiteDoc.Enabled = false;
            TxtNoEscritura.Enabled = false;
            CboTitulo.Enabled = false;
            TxtNomNotario.Enabled = false;
            TxtFecEmi.Enabled = false;
            RadUploadFile.Enabled = false;
            TxtDirccion.Enabled = false;
            TxtAldea.Enabled = false;
            CboDepartamentoFinca.Enabled = false;
            CboMunicipioFinca.Enabled = false;
            TxtColNorte.Enabled = false;
            TxtColSur.Enabled = false;
            TxtColEste.Enabled = false;
            TxtColOeste.Enabled = false;
            TxtAreaFinca.Enabled = false;
            UploadPolFinca.Enabled = false;
            OptAreasPro.Enabled = false;
            btnGrabarFinca.Visible = false;
            TxtAreaFinca.Enabled = false;
            TxtFinca.Enabled = false;
            

        }

        void BtnNuevaFinca_ServerClick(object sender, EventArgs e)
        {
            OcultaMensajes();
            LimpiarFinca();
            DesbloqueFinca();
            TxtInmuebleId.Text = "";
            DivPropietariosFinca.Visible = false;
        }

        void DesbloqueFinca()
        {
            ChkIngNomFinca.Enabled = true;
            TxtUbicacionNorte.Enabled = true;
            TxtUbicacionOeste.Enabled = true;
            CboTipoDocumento.Enabled = true;
            TxtNoFinca.Enabled = true;
            TxtFolio.Enabled = true;
            TxtLibro.Enabled = true;
            TxtDe.Enabled = true;
            TxtNoCerti.Enabled = true;
            TxtMunEmiteDoc.Enabled = true;
            TxtNoEscritura.Enabled = true;
            CboTitulo.Enabled = true;
            TxtNomNotario.Enabled = true;
            TxtFecEmi.Enabled = true;
            RadUploadFile.Enabled = true;
            TxtDirccion.Enabled = true;
            TxtAldea.Enabled = true;
            CboDepartamentoFinca.Enabled = true;
            CboMunicipioFinca.Enabled = true;
            TxtColNorte.Enabled = true;
            TxtColSur.Enabled = true;
            TxtColEste.Enabled = true;
            TxtColOeste.Enabled = true;
            TxtAreaFinca.Enabled = true;
            UploadPolFinca.Enabled = true;
            OptAreasPro.Enabled = true;
            btnGrabarFinca.Visible = true;
        }

        void OcultaMensajes()
        {
            LblErrFinca.Text = "";
            LblGoodFinca.Text = "";
            DivErrFinca.Visible = false;
            DivGoodFinca.Visible = false;
        }

        void LimpiarFinca()
        {
            ChkIngNomFinca.Checked = false;
            TxtUbicacionNorte.Text = "";
            TxtUbicacionOeste.Text = "";
            CboTipoDocumento.SelectedValue = "";
            TxtNoFinca.Text = "";
            TxtFolio.Text = "";
            TxtLibro.Text = "";
            TxtDe.Text = "";
            TxtNoCerti.Text = "";
            TxtMunEmiteDoc.Text = "";
            TxtNoEscritura.Text = "";
            CboTitulo.SelectedValue = "";
            TxtNomNotario.Text = "";
            TxtFecEmi.DateInput.Text = "";
            RadUploadFile.UploadedFiles.Clear();
            TxtDirccion.Text = "";
            TxtAldea.Text = "";
            CboDepartamentoFinca.SelectedValue = "";
            CboMunicipioFinca.SelectedValue = "";
            TxtColNorte.Text = "";
            TxtColSur.Text = "";
            TxtColEste.Text = "";
            TxtColOeste.Text = "";
            TxtArea.Text = "";
            UploadPolFinca.UploadedFiles.Clear();
            Ds_Temporal.Tables["Dt_Poligono"].Clear();
            GrdPoligono.Rebind();
            //DivPropietariosFinca.Visible = false;
            TxtFinca.Text = "SIN NOMBRE";
            TxtAreaFinca.Text = "";
        }

        void CboTipoDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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

        void GrdPoligono_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Poligono"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPoligono, "Dt_Poligono");
        }

        void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            DivErrPolBosque.Visible = false;
            if (RadTxtFile.UploadedFiles.Count > 0)
            {
                string Extension = RadTxtFile.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPolBosque.Visible = true;
                    LblErrPolBosuqe.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = RadTxtFile.UploadedFiles[0].InputStream;
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        resultXls = excelReader.AsDataSet();

                        Ds_Temporal.Tables["Dt_Poligono"].Clear();
                        foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                        {
                            if (iDtRow["X"].ToString() != "")
                            {
                                DataRow rowNew = Ds_Temporal.Tables["Dt_Poligono"].NewRow();
                                rowNew["Id"] = iDtRow["Poligono"];
                                rowNew["X"] = iDtRow["X"];
                                rowNew["Y"] = iDtRow["Y"];
                                Ds_Temporal.Tables["Dt_Poligono"].Rows.Add(rowNew);
                            }

                        }

                        GrdPoligono.Rebind();




                    }
                    catch (Exception ex)
                    {
                        DivErrPolBosque.Visible = true;
                        LblErrPolBosuqe.Text = ex.Message;
                    }
                }
                
            }
            else
            {
                DivErrPolBosque.Visible = true;
                LblErrPolBosuqe.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }
            

        }

        void GrdInventario_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in GrdInventario.Items)
            {
                if (item.OwnerTableView.Name == "LabelsInventario")
                {
                    GridTableView ownerTableView = item.OwnerTableView;
                    for (int i = ownerTableView.Items.Count - 2; i >= 0; i--)
                    {
                        GridDataItem item2 = ownerTableView.Items[i];
                        GridDataItem item3 = ownerTableView.Items[i + 1];
                        if (item2["Rodal"].Text == item3["Rodal"].Text)
                        {
                            item2["Rodal"].RowSpan = (item3["Rodal"].RowSpan < 2) ? 2 : (item3["Rodal"].RowSpan + 1);
                            item3["Rodal"].Visible = false;
                        }
                        if (item2["Area"].Text == item3["Area"].Text)
                        {
                            item2["Area"].RowSpan = (item3["Area"].RowSpan < 2) ? 2 : (item3["Area"].RowSpan + 1);
                            item3["Area"].Visible = false;
                        }
                        if (item2["Densidad"].Text == item3["Densidad"].Text)
                        {
                            item2["Densidad"].RowSpan = (item3["Densidad"].RowSpan < 2) ? 2 : (item3["Densidad"].RowSpan + 1);
                            item3["Densidad"].Visible = false;
                        }
                        if (item2["Dap"].Text == item3["Dap"].Text)
                        {
                            item2["Dap"].RowSpan = (item3["Dap"].RowSpan < 2) ? 2 : (item3["Dap"].RowSpan + 1);
                            item3["Dap"].Visible = false;
                        }
                        if (item2["Altura"].Text == item3["Altura"].Text)
                        {
                            item2["Altura"].RowSpan = (item3["Altura"].RowSpan < 2) ? 2 : (item3["Altura"].RowSpan + 1);
                            item3["Altura"].Visible = false;
                        }
                        if (item2["Volumen"].Text == item3["Volumen"].Text)
                        {
                            item2["Volumen"].RowSpan = (item3["Volumen"].RowSpan < 2) ? 2 : (item3["Volumen"].RowSpan + 1);
                            item3["Volumen"].Visible = false;
                        }
                    }
                }
            }
        }


        void ActualizaDatosGeneralesInv(int Rodal, double Area, double Densidad, double DAP, double Altura, double Volumen)
        {
            for (int i = 0; i < Ds_Temporal.Tables["Dt_Inventario"].Rows.Count; i++)
            {
                if (Convert.ToInt32(Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Rodal"]) == Rodal)
                {
                    Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Area"] = Area;
                    Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Densidad"] = Densidad;
                    Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Dap"] = DAP;
                    Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Altura"] = Altura;
                    Ds_Temporal.Tables["Dt_Inventario"].Rows[i]["Volumen"] = Volumen;
                }
            }
        }

        void GrdInventario_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdAdd")
            {
                TxtRodal.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Rodal"].ToString();
                TxtArea.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Area"].ToString();
                TxtDensidad.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Densidad"].ToString();
                TxtDap.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Dap"].ToString();
                TxtAltura.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Altura"].ToString();
                TxtVolumen.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Volumen"].ToString();
            }
            if (e.CommandName == "CmdDel")
            {
                EliminarEspecie(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Rodal"].ToString()), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString()));
            }
        }

        void EliminarEspecie(int Rodal, int EspecieId)
        {
            for (int i = 0; i < GrdInventario.Items.Count; i++)
            {
                if ((Rodal == Convert.ToInt32(GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"])) && (EspecieId == Convert.ToInt32(GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"])))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_Inventario"].NewRow();
                    rowNew["Rodal"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"];
                    rowNew["EspecieId"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                    rowNew["Especie"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                    rowNew["Area"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Area"];
                    rowNew["Densidad"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Densidad"];
                    rowNew["Dap"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Dap"];
                    rowNew["Altura"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Altura"];
                    rowNew["Volumen"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Volumen"];
                    rowNew["YearEst"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["YearEst"];
                    Ds_Temporal.Tables["Dt_Inventario"].Rows.Add(rowNew);
                }
            }
            dv = Ds_Temporal.Tables["Dt_Inventario"].DefaultView;
            dv.Sort = "Rodal ASC";
            GrdInventario.Rebind();
        }

        bool ValidaEspecie()
        {
            LblMensajeEspecie.Text = "";
            DivErrEspecie.Visible = false;
            bool HayError = false;

            if ((TxtRodal.Text == "") || (TxtRodal.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar el rodal";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar el rodal";
                HayError = true;
            }
            if ((TxtArea.Text == "") || (TxtArea.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar el área";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar el área";
                HayError = true;
            }
            if ((TxtDensidad.Text == "") || (TxtDensidad.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar la densidad";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar la densidad";
                HayError = true;
            }
            if ((TxtDap.Text == "") || (TxtDap.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar el DAP";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar el DAP";
                HayError = true;
            }
            if ((TxtAltura.Text == "") || (TxtAltura.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar la altura";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar la altura";
                HayError = true;
            }
            if ((TxtVolumen.Text == "") || (TxtVolumen.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar el volumen";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar el volumen";
                HayError = true;
            }
            if ((CboEspecie.Text == "") || (CboEspecie.SelectedValue == ""))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe seleccionar la especie";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe seleccionar la especie";
                HayError = true;
            }
            if ((TxtAnisEsta.Text == "") || (TxtAnisEsta.Text == "0"))
            {
                if (LblMensajeEspecie.Text == "")
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + "Debe ingresar el año de establecimiento";
                else
                    LblMensajeEspecie.Text = LblMensajeEspecie.Text + ", debe ingresar el año de establecimiento";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrEspecie.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ExisteEspecie(int Rodal, int EspecieId)
        {
            bool Existe = false;
            for (int i = 0; i < GrdInventario.Items.Count; i++)
            {
                if ((Convert.ToInt32(GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"]) == Rodal)  && (Convert.ToInt32(GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]) == EspecieId))
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        double SumaAreasRodales()
        {
            double SumaAreas = 0;
            for (int i = 0; i < GrdInventario.Items.Count; i++)
            {
                SumaAreas = SumaAreas + (Convert.ToDouble(GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Area"]));
            }
            return SumaAreas;
        }

        void btnAddEspecie_ServerClick(object sender, EventArgs e)
        {
            DivErrEspecie.Visible = false;
            if (ValidaEspecie() == true)
            {
                if (ExisteEspecie(Convert.ToInt32(TxtRodal.Text),Convert.ToInt32(CboEspecie.SelectedValue)))
                {
                    DivErrEspecie.Visible = true;
                    LblMensajeEspecie.Text = "Especie ya existe en el rodal";
                
                }
                else if ((SumaAreasRodales() + Convert.ToDouble(TxtArea.Text) > ClGestionRegistro.GetSumaAreasForestalesTempFincaRegistro(Convert.ToInt32(Session["UsuarioId"]))))
                {
                    DivErrEspecie.Visible = true;
                    LblMensajeEspecie.Text = "Al agregar esta especie sobrepasa el total de áreas";
                }
                else
                {
                    CargarGridEspecie();
                    AgregaEspecie();
                    dv = Ds_Temporal.Tables["Dt_Inventario"].DefaultView;
                    dv.Sort = "Rodal ASC";
                    ActualizaDatosGeneralesInv(Convert.ToInt32(TxtRodal.Text), Convert.ToDouble(TxtArea.Text), Convert.ToDouble(TxtDensidad.Text), Convert.ToDouble(TxtDap.Text), Convert.ToDouble(TxtAltura.Text), Convert.ToDouble(TxtVolumen.Text));
                    GrdInventario.Rebind();
                    LimpiarEspecie();
                }
                
                
            }
        }

        void GrdInventario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (dv.Count > 0)
                ClUtilitarios.LlenaGridDataView(dv, GrdInventario, "Dt_Inventario");

        }

        void CargarGridEspecie()
        {
            for (int i = 0; i < GrdInventario.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_Inventario"].NewRow();
                rowNew["Rodal"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"];
                rowNew["EspecieId"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                rowNew["Especie"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                rowNew["Area"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Area"];
                rowNew["Densidad"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Densidad"];
                rowNew["Dap"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Dap"];
                rowNew["Altura"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Altura"];
                rowNew["Volumen"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Volumen"];
                rowNew["YearEst"] = GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["YearEst"];
                Ds_Temporal.Tables["Dt_Inventario"].Rows.Add(rowNew);
            }
        }


        void BtnYes_Click(object sender, EventArgs e)
        {

            int GestionId = ClGestionRegistro.MaxGestionId(1);
            int Correlativo_Anual = ClGestionRegistro.MaxGestionId(2);
            string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;

            ClGestionRegistro.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(TxtSubRegionId.Text), 13, 3, Correlativo_Anual);

            int Telefono = 0;
            if (TxtTelefonoNotifica.Text != "")
                Telefono = Convert.ToInt32(TxtTelefonoNotifica.Text.Replace("-", ""));
            int Procedencia = 0;
            if (CboProcedencia.SelectedValue != "")
                Procedencia = Convert.ToInt32(CboProcedencia.SelectedValue);
            ClGestionRegistro.Insertar_Gestion_Plantacion(GestionId, Convert.ToInt32(CboTipoPlantacion.SelectedValue), 2, Procedencia, TxtDireccionNotifica.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Telefono, Convert.ToInt32(TxtCelularNotifica.Text.Replace("-", "")), TxtCorreoNotifica.Text, TxtObservaciones.Text, TxtNomFirma.Text, Convert.ToInt32(Session["UsuarioId"]));
            ClGestionRegistro.GrabarFincasRegistro(GestionId, Convert.ToInt32(Session["UsuarioId"]));
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

            if (GrdInventario.Items.Count > 0)
            {

                XmlDocument iInformacion = ClXml.CrearDocumentoXML("InventarioForestal");
                XmlNode iElementos = iInformacion.CreateElement("Especie");
                for (int i = 0; i < GrdInventario.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Rodal", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("EspecieId", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("Area", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Area"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("Densidad", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Densidad"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("Dap", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Dap"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("Altura", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Altura"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("Volumen", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["Volumen"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("YearEst", GrdInventario.Items[i].OwnerTableView.DataKeyValues[i]["YearEst"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
                ClGestionRegistro.Insert_Inventario_Forestal(GestionId, iInformacion, Convert.ToInt32(Session["UsuarioId"]));
            }
            if (GrdPoligono.Items.Count > 0)
            {
                XmlDocument iInformacionPol = ClXml.CrearDocumentoXML("Poligonos");
                XmlNode iElementoPoligono = iInformacionPol.CreateElement("Puntos");

                for (int i = 0; i < GrdPoligono.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacionPol.CreateElement("Item");
                    ClXml.AgregarAtributo("Id", GrdPoligono.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                    ClXml.AgregarAtributo("X", GrdPoligono.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                    ClXml.AgregarAtributo("Y", GrdPoligono.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                    iElementoPoligono.AppendChild(iElementoDetalle);
                }
                iInformacionPol.ChildNodes[1].AppendChild(iElementoPoligono);
                String iPoligonoGML = "";
                string ErrorMapa = "";
                if (ClPoligono.Actualizar_Poligono(iInformacionPol, ref GestionId, ref iPoligonoGML, Convert.ToInt32(Session["UsuarioId"]), ref ErrorMapa))
                {

                }
                else
                {
                    DivErrEspecie.Visible = true;
                    LblMensajeEspecie.Text = ErrorMapa;
                }

            }


            //Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestionRegistro.ImpresionFormularioPlantacionVoluntaria(GestionId,1);
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
            int TipoPersona = ClGestionRegistro.Get_Tipo_Persona_Fincas(1, Convert.ToInt32(Session["UsuarioId"]));
            int NoRequisito = 1;
            Ds_Profesionales Ds_Formulario_Plantacion_Voluntaria = new Ds_Profesionales();
            Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Clear();
            DataRow row = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].NewRow();
            row["Requisitos"] = "Requisitos:\n " + NoRequisito +") Certificación original o copia legalizada de dicha certificación que acredite la propiedad  del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud; ";
            NoRequisito++;
            if (TipoPersona == 1)
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del propietario; ";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Polígono georeferenciado a registrar, en coordenadas GTM.";
            NoRequisito++;
            if ((GrdRepresentantes.Items.Count > 0) || (TipoPersona == 2))
            {
                row["Requisitos"] = row["Requisitos"] + "\n " + NoRequisito + ") Copia del documento personal de identificación del Representante Legal;";
                NoRequisito++;
            }
            row["Requisitos"] = row["Requisitos"] + " \n " + NoRequisito + ") Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
            row["Region"] = TxtRegion.Text;
            row["SubRegion"] = TxtSubRegion.Text;
            row["NUG"] = "-------";
            row["Fecha"] = string.Format("{0:dd/MM/yyyy}", ClUtilitarios.FechaDB());
            row["TipoPlantacion"] = CboTipoPlantacion.Text;
            if (CboProcedencia.SelectedValue == "")
                row["Procedencia"] = "--------------";
            else
                row["Procedencia"] = CboProcedencia.Text;
            row["Direccion_Notificacion"] = TxtDireccionNotifica.Text;
            row["Municipio_Notificacion"] = CboMunicipio.Text;
            row["Departamento_Notificacion"] = CboDepartamento.Text;
            row["Telefono_Notificacion"] = TxtTelefonoNotifica.Text;
            row["Celular_Notificacion"] = TxtCelularNotifica.Text;
            row["Correo_Notificacion"] = TxtCorreoNotifica.Text;
            row["Observaciones"] = TxtObservaciones.Text;
            row["Nombre"] = TxtNomFirma.Text;
            row["Tipo"] = 1; //PV
            row["TotalForestal"] = ClGestionRegistro.Get_Sum_Area_Plantacion(Convert.ToInt32(Session["UsuarioId"]), 1);
            if (GrdRepresentantes.Items.Count > 0)
                row["TieneRepresentantes"] = 1;
            else
                row["TieneRepresentantes"] = 0;
            Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Plantacion_Voluntaria"].Rows.Add(row);
            
            if (GrdRepresentantes.Items.Count > 0)
            {
                Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"] + " " + GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"];
                    if (GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"].ToString() == "0")
                        rowRepresentantes["TipoId"] = "DPI";
                    else
                        rowRepresentantes["TipoId"] = "Pasporte";
                    rowRepresentantes["Id"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"];
                    Ds_Formulario_Plantacion_Voluntaria.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            
            
            

            Session["Ds_Formulario_Plantacion_Voluntaria"] = Ds_Formulario_Plantacion_Voluntaria;
            RadWindow1.Title = "Vista Previa Insripción";
            Session["TipoReporte"] = "1";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void OptAreaProtegida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptAreasPro.Items[0].Selected == true)
                DivArea.Visible = false;
            else
                DivArea.Visible = true;

        }


        bool TodasLasFincasMismaSubRegion(ref string Mensaje)
        {
            int SubRegion = 0;
            bool TodasEnMisma = true;
            for (int i = 0; i < GrdInmuebles.Items.Count; i++)
            {
                DataSet dsRegioSubregionInmueble =  ClInmueble.Get_Region_Subregion_Inmueble(Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                if (SubRegion == 0)
                {
                    SubRegion = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString());
                    TxtSubRegionId.Text = SubRegion.ToString();
                    TxtSubRegion.Text = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubRegion"].ToString();
                    TxtRegion.Text = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["Region"].ToString();
                }

                else
                {
                    if (SubRegion != Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString()))
                    {
                        Mensaje = "La Finca: " + GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["Finca"].ToString() + " no pertenece a la subregión de la primer finca";
                        TodasEnMisma = false;
                        break;
                    }
                }
            }
            return TodasEnMisma;
        }

        bool Valida()
        {
            LblMensaje.Text = "";
            BtnEror.Visible = false;
            bool HayError = false;
            int ValidaProcedencia = 0;
            if ((CboTipoPlantacion.SelectedValue == "4") || (CboTipoPlantacion.SelectedValue == "20"))
                ValidaProcedencia = 1;

            if ((CboTipoPlantacion.Text == "") || (CboTipoPlantacion.SelectedValue == "" ))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de plantación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de plantación";
                HayError = true;
            }
            if ((ValidaProcedencia == 0)  && ((CboProcedencia.Text == "") || (CboProcedencia.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la procedencía de la plantación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la procedencía de la plantación";
                HayError = true;
            }
            if (GrdInmuebles.Items.Count == 0)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar al menos una finca";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar al menos una finca";
                HayError = true;
            }
            string MensajeTemp = "";
            if (ValidaDatosFincaPropietarios(ref MensajeTemp) == true)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + " " + MensajeTemp;
                else
                    LblMensaje.Text = LblMensaje.Text + ", " +  MensajeTemp; ;
                HayError = true;
            }
            if (ValidaDatosFincaAreas(ref MensajeTemp) == true)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + " " + MensajeTemp;
                else
                    LblMensaje.Text = LblMensaje.Text + ", " + MensajeTemp; ;
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
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el departamento de notificación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el departamento de notificación";
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
            if ((CboMunicipio.SelectedValue == "") || (CboMunicipio.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el municipio de notificación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el municipio de notificación";
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
            if (GrdInventario.Items.Count == 0)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar al menos un item al inventario";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar al menos un item al inventario";
                HayError = true;
            }
            if (GrdPoligono.Items.Count == 0)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar el poligono de la plantación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe ingresar el poligono de la plantación";
                HayError = true;
            }
            string MensajeTempMismaFinca = "";
            if (TodasLasFincasMismaSubRegion(ref MensajeTempMismaFinca) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + " " + MensajeTempMismaFinca;
                else
                    LblMensaje.Text = LblMensaje.Text + ", " + MensajeTempMismaFinca;
                HayError = true;
            }
            string MensajeDiferentiTipoProp = "";
            bool ValidaMismosPropietarios = true;
            int TipoPropietario = 1;
            if (ValidaIgualTipoPropietario(Convert.ToInt32(Session["UsuarioId"]), ref MensajeDiferentiTipoProp, ref  ValidaMismosPropietarios, ref TipoPropietario) == false)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + " " + MensajeDiferentiTipoProp;
                else
                    LblMensaje.Text = LblMensaje.Text + ", " + MensajeDiferentiTipoProp;
                HayError = true;
            }
            if (ValidaMismosPropietarios == true)
            {
                string MensajeMismosPropietarios = "";
                if (TempValidaMismosPropietarios(Convert.ToInt32(Session["UsuarioId"]),TipoPropietario,ref MensajeMismosPropietarios) == true)
                {
                    if (LblMensaje.Text == "")
                        LblMensaje.Text = LblMensaje.Text + " " + MensajeMismosPropietarios;
                    else
                        LblMensaje.Text = LblMensaje.Text + ", " + MensajeMismosPropietarios;
                    HayError = true;
                }
            }
            if (HayError == true)
            {
                BtnEror.Visible = true;
                return false;
            }

            else
                return true;

        }

        bool ValidaDatosFincaPropietarios(ref string Mensaje) //Valida que tenga propietariios/nom emp
        {
            bool Valida = false;
            for (int i = 0; i < GrdInmuebles.Items.Count; i++)
            {
                if (ClGestionRegistro.TienePropietarioNomEmp_FincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"])) == 0)
                {
                    BtnEror.Visible = true;
                    Mensaje = "A la Finca: " + GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["Finca"].ToString() + " no se han ingresado los propietarios o la persona júridica";
                    Valida = true;
                    break;
                }
                
            }
            return Valida;
        }

        bool ValidaDatosFincaAreas(ref string Mensaje) //Valida areas fincas
        {
            bool Valida = false;
            for (int i = 0; i < GrdInmuebles.Items.Count; i++)
            {
                if (ClGestionRegistro.TieneAras_FincaRegistro(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"])) == 0)
                {
                    BtnEror.Visible = true;
                    Mensaje = "A la Finca: " + GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["Finca"].ToString() + " no se ha ingresado el área forestal";
                    Valida = true;
                    break;
                }

            }
            return Valida;
        }


        void CboFinca_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboFinca.SelectedValue) > 0)
            {
                CargaInfo(Convert.ToInt32(CboFinca.SelectedValue));
                BloquearFinca();
            }
        }

        void CargaInfo(int InmuebleId)
        {
            DataSet dsInmueble = ClInmueble.Inmueble_Get(InmuebleId);
            TxtFinca.Text = dsInmueble.Tables["DATOS"].Rows[0]["Finca"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Gtm_Norte"].ToString() != "")
                TxtUbicacionNorte.Text = dsInmueble.Tables["DATOS"].Rows[0]["Gtm_Norte"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Gtm_Oeste"].ToString() != "")
                TxtUbicacionOeste.Text = dsInmueble.Tables["DATOS"].Rows[0]["Gtm_Oeste"].ToString();
            CboTipoDocumento.Text = dsInmueble.Tables["DATOS"].Rows[0]["TipoDocPropiedad"].ToString();
            CboTipoDocumento.SelectedValue = dsInmueble.Tables["DATOS"].Rows[0]["TipoDoc_PropiedadId"].ToString();
            TxtFecEmi.SelectedDate = Convert.ToDateTime(dsInmueble.Tables["DATOS"].Rows[0]["FecEmi"]);
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

            if (Convert.ToInt32(dsInmueble.Tables["DATOS"].Rows[0]["TipoDoc_PropiedadId"]) == 1)
            {
                TxtNoFinca.Text = dsInmueble.Tables["DATOS"].Rows[0]["NoFinca"].ToString();
                TxtFolio.Text = dsInmueble.Tables["DATOS"].Rows[0]["Folio"].ToString();
                TxtLibro.Text = dsInmueble.Tables["DATOS"].Rows[0]["Libro"].ToString();
                TxtDe.Text = dsInmueble.Tables["DATOS"].Rows[0]["De"].ToString();
                TxtNoCerti.Text = "";
                TxtMunEmiteDoc.Text = "";
                TxtNoEscritura.Text = "";
                CboTitulo.ClearSelection();
                TxtNomNotario.Text = "";
            }
            else if (Convert.ToInt32(dsInmueble.Tables["DATOS"].Rows[0]["TipoDoc_PropiedadId"]) == 2)
            {
                TxtNoFinca.Text = "";
                TxtFolio.Text = "";
                TxtLibro.Text = "";
                TxtDe.Text = "";
                TxtNoCerti.Text = dsInmueble.Tables["DATOS"].Rows[0]["NoCertificacion"].ToString();
                if (dsInmueble.Tables["DATOS"].Rows[0]["Municipalidad"].ToString() != "")
                    TxtMunEmiteDoc.Text = dsInmueble.Tables["DATOS"].Rows[0]["Municipalidad"].ToString();
                TxtNoEscritura.Text = "";
                CboTitulo.ClearSelection();
                TxtNomNotario.Text = "";
            }
            else if (Convert.ToInt32(dsInmueble.Tables["DATOS"].Rows[0]["TipoDoc_PropiedadId"]) == 3)
            {
                TxtNoFinca.Text = "";
                TxtFolio.Text = "";
                TxtLibro.Text = "";
                TxtDe.Text = "";
                TxtNoCerti.Text = "";
                TxtMunEmiteDoc.Text = "";
                TxtNoEscritura.Text = dsInmueble.Tables["DATOS"].Rows[0]["NoEscritura"].ToString();
                CboTitulo.SelectedValue = dsInmueble.Tables["DATOS"].Rows[0]["TituloNotarioId"].ToString();
                CboTitulo.Text = dsInmueble.Tables["DATOS"].Rows[0]["TituloNotario"].ToString();
                TxtNomNotario.Text = dsInmueble.Tables["DATOS"].Rows[0]["Notario"].ToString();
            }
            TxtDirccion.Text = dsInmueble.Tables["DATOS"].Rows[0]["Direccion"].ToString();
            TxtAldea.Text = dsInmueble.Tables["DATOS"].Rows[0]["Aldea"].ToString();
            CboMunicipioFinca.Text = dsInmueble.Tables["DATOS"].Rows[0]["Municipio"].ToString();
            CboDepartamentoFinca.Text = dsInmueble.Tables["DATOS"].Rows[0]["Departamento"].ToString();
            CboMunicipioFinca.SelectedValue = dsInmueble.Tables["DATOS"].Rows[0]["MunicipioId"].ToString();
            CboDepartamentoFinca.SelectedValue = dsInmueble.Tables["DATOS"].Rows[0]["DepartamentoId"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Norte"].ToString() != "")
                TxtColNorte.Text = dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Norte"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Sur"].ToString() != "")
                TxtColSur.Text = dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Sur"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Este"].ToString() != "")
                TxtColEste.Text = dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Este"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Oeste"].ToString() != "")
                TxtColOeste.Text = dsInmueble.Tables["DATOS"].Rows[0]["Colindancia_Oeste"].ToString();
            TxtAreaFinca.Text = dsInmueble.Tables["DATOS"].Rows[0]["Area"].ToString();
            if (dsInmueble.Tables["DATOS"].Rows[0]["AreaProtegidaId"].ToString() != "")
            {
                OptAreasPro.SelectedValue = "1";
                DivArea.Visible = true;
                CboArea.SelectedValue = dsInmueble.Tables["DATOS"].Rows[0]["AreaProtegidaId"].ToString();
                CboArea.Text = dsInmueble.Tables["DATOS"].Rows[0]["AreaPro"].ToString();
            }
            else
            {
                OptAreasPro.SelectedValue = "0";
                DivArea.Visible = false;
                CboArea.ClearSelection();
            }
            dsInmueble.Clear();

            DataSet dsDatosPuntosInmueble = ClInmueble.obtener_puntos_poligonos_Inmueble(InmuebleId);
            for (int i = 0; i < dsDatosPuntosInmueble.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoBosque"].NewRow();
                rowNew["Id"] = dsDatosPuntosInmueble.Tables["Datos"].Rows[i]["Id"];
                rowNew["X"] = dsDatosPuntosInmueble.Tables["Datos"].Rows[i]["Punto_X"];
                rowNew["Y"] = dsDatosPuntosInmueble.Tables["Datos"].Rows[i]["Punto_Y"];
                Ds_Temporal.Tables["Dt_PoligonoBosque"].Rows.Add(rowNew);
            }
            GrdPoligonoFinca.Rebind();
            dsDatosPuntosInmueble.Clear();
            

        }

        void ChkConIncentivos_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConIncentivos.Checked == true)
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoProcedencia(2), CboProcedencia, "ProcedenciaId", "Procedencia");
                ClUtilitarios.AgregarSeleccioneCombo(CboProcedencia, "Procedencia");
            }
            else
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoProcedencia(1), CboProcedencia, "ProcedenciaId", "Procedencia");
                ClUtilitarios.AgregarSeleccioneCombo(CboProcedencia, "Procedencia");
            }
        }

        void CboTipoPlantacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if ((CboTipoPlantacion.SelectedValue == "4") || (CboTipoPlantacion.SelectedValue == "20"))
                DivProcedencia.Visible = false;
            else
                DivProcedencia.Visible = true;
            if (CboTipoPlantacion.SelectedValue == "5")
            {
                ChkConIncentivos.Visible = true;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoProcedencia(3), CboProcedencia, "ProcedenciaId", "Procedencia");
                ClUtilitarios.AgregarSeleccioneCombo(CboProcedencia, "Procedencia");
            }
            else
            {
                ChkConIncentivos.Visible = false;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoProcedencia(7), CboProcedencia, "ProcedenciaId", "Procedencia");
                ClUtilitarios.AgregarSeleccioneCombo(CboProcedencia, "Procedencia");
            }
                
        }

        void LimpiarEspecie()
        {
            TxtRodal.Text = "";
            TxtArea.Text = "";
            TxtDensidad.Text = "";
            TxtDap.Text = "";
            TxtAltura.Text = "";
            TxtVolumen.Text = "";
            TxtAnisEsta.Text = "";
            CboEspecie.ClearSelection();
        }


        void AgregaEspecie()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_Inventario"].NewRow();
            rowNew["Rodal"] = TxtRodal.Text;
            rowNew["EspecieId"] = CboEspecie.SelectedValue;
            rowNew["Especie"] = CboEspecie.Text;
            rowNew["Area"] = TxtArea.Text;
            rowNew["Densidad"] = TxtDensidad.Text;
            rowNew["Dap"] = TxtDap.Text;
            rowNew["Altura"] = TxtAltura.Text;
            rowNew["Volumen"] = TxtVolumen.Text;
            rowNew["YearEst"] = TxtAnisEsta.Text;
            Ds_Temporal.Tables["Dt_Inventario"].Rows.Add(rowNew);
        }

        bool TempValidaMismosPropietarios(int UsuarioId, int TipoPropietario, ref string Mensaje)
        {
            bool HayDiferente = false;

            if (GrdInmuebles.Items.Count> 1)
            {
                for (int i = 0; i < GrdInmuebles.Items.Count; i++)
                {
                    if (TipoPropietario == 1)
                    {
                        DataSet Propietarios = ClGestionRegistro.Get_propietarios_Temp_Finca(UsuarioId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                        for (int j = 0; j < Propietarios.Tables["Datos"].Rows.Count; j++)
                        {
                            //obtener todos los demas inmuebles 
                            DataSet Inmuebles = ClGestionRegistro.Get_Otras_Temp_Finca(UsuarioId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                            for (int k = 0; k < Inmuebles.Tables["Datos2"].Rows.Count; k++)
                            {
                                if (ClGestionRegistro.Existe_Propietarios_OtroInmueble(UsuarioId, Convert.ToInt32(Inmuebles.Tables["Datos2"].Rows[k]["InmuebleId"]), Convert.ToInt32(Propietarios.Tables["Datos"].Rows[j]["PersonaId"])) == 0)
                                {
                                    Mensaje = "El propietario: " + Propietarios.Tables["Datos"].Rows[j]["Nombres"] + " " + Propietarios.Tables["Datos"].Rows[j]["Apellidos"] + " no está en la finca: " + Inmuebles.Tables["Datos2"].Rows[k]["Finca"];
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        string PrimerNombre = "";
                        string TempNombre = "";
                        DataSet Fincas = ClGestionRegistro.Get_Fincas_Completas(Convert.ToInt32(Session["UsuarioId"]));
                        for (int l = 0; l < Fincas.Tables["Datos2"].Rows.Count; l++)
                        {
                            if (l == 0)
                            {
                                PrimerNombre = Fincas.Tables["Datos2"].Rows[l]["Nombre_Empresa"].ToString();
                            }
                            else
                            {
                                TempNombre = Fincas.Tables["Datos2"].Rows[l]["Nombre_Empresa"].ToString();
                                if (PrimerNombre != TempNombre)
                                {
                                    Mensaje = "La finca: " + Fincas.Tables["Datos2"].Rows[l]["Finca"] + " tiene diferente propietario";
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            

            return HayDiferente;
        }

        bool ValidaIgualTipoPropietario(int UsuarioId, ref string Mensaje, ref bool ValidaMismosPropietarios, ref int TipoPropietario)
        {
            bool IgualTipoPropietario = true;
            int PrimerTipo = 0;
            int TempTipo = 0;
            ValidaMismosPropietarios = true;
            if (GrdInmuebles.Items.Count > 1)
            {
                for (int i = 0; i < GrdInmuebles.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        PrimerTipo = ClGestionRegistro.Get_Tipo_Propietario_Finca(UsuarioId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                        TipoPropietario = PrimerTipo;
                    }
                    else
                    {
                        TempTipo = ClGestionRegistro.Get_Tipo_Propietario_Finca(UsuarioId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                        if (TempTipo != PrimerTipo)
                        {
                            Mensaje = "La Finca " + GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["Finca"] + " tiene diferente tipo de propietario";
                            ValidaMismosPropietarios = false;
                            return false;
                        }
                    }
                }
            }

            return IgualTipoPropietario;
        }

        
    }
}