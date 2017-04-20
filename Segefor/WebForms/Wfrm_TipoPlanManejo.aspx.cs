﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using System.IO;
using Excel;
using SEGEFOR.Data_Set;
using System.Xml;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_TipoPlanManejo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Manejo ClManejo;
        Cl_Inmueble ClInmueble;
        Cl_Xml ClXml;
        Cl_Poligono ClPoligono;
        Cl_Gestion_Registro Cl_Gestion;

        DataSet DsPropietarios = new DataSet("Propietarios");
        DataSet DsRepresentantes = new DataSet("Representantes");
        DataSet DsProductoNoForestales = new DataSet("ProdNoForestales");
        DataSet DsProductoNoForestalesExtraer = new DataSet("ProdNoForestalesExtraer");
        DataSet DsEspeciesRepoblacion = new DataSet("EspeciesRepoblacion");
        DataSet DsAnexos = new DataSet("Anexos");
        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        DataSet resultXls = new DataSet();
        DataView dv = new DataView();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClManejo = new Cl_Manejo();
            ClInmueble = new Cl_Inmueble();
            ClXml = new Cl_Xml();
            ClPoligono = new Cl_Poligono();
            Cl_Gestion = new Cl_Gestion_Registro();

            DataTable DtPropietarios = DsPropietarios.Tables.Add("Propietarios");
            DataColumn Existe = DtPropietarios.Columns.Add("Existe", typeof(Boolean));
            DataColumn PersonaId = DtPropietarios.Columns.Add("PersonaId", typeof(Int64));
            DataColumn Dpi = DtPropietarios.Columns.Add("Dpi", typeof(string));
            DataColumn Nombres = DtPropietarios.Columns.Add("Nombres", typeof(string));
            DataColumn Apellidos = DtPropietarios.Columns.Add("Apellidos", typeof(string));
            DataColumn Fec_Venc_Id = DtPropietarios.Columns.Add("Fec_Venc_Id", typeof(string));
            DataColumn PaisId = DtPropietarios.Columns.Add("PaisId", typeof(Int32));
            DataColumn GeneroId = DtPropietarios.Columns.Add("GeneroId", typeof(Int32));
            DataColumn Fec_Nac = DtPropietarios.Columns.Add("Fec_Nac", typeof(string));
            DataColumn EstadoCivilId = DtPropietarios.Columns.Add("EstadoCivilId", typeof(Int32));
            DataColumn OcupacionId = DtPropietarios.Columns.Add("OcupacionId", typeof(Int32));

            DataTable DtRepresentantes = DsRepresentantes.Tables.Add("Representantes");
            DataColumn ExisteRep = DtRepresentantes.Columns.Add("ExisteRep", typeof(Boolean));
            DataColumn PersonaIdRep = DtRepresentantes.Columns.Add("PersonaIdRep", typeof(Int64));
            DataColumn DpiRep = DtRepresentantes.Columns.Add("DpiRep", typeof(string));
            DataColumn NombresRep = DtRepresentantes.Columns.Add("NombresRep", typeof(string));
            DataColumn ApellidosRep = DtRepresentantes.Columns.Add("ApellidosRep", typeof(string));
            DataColumn Fec_Venc_IdRep = DtRepresentantes.Columns.Add("Fec_Venc_IdRep", typeof(string));
            DataColumn PaisIdRep = DtRepresentantes.Columns.Add("PaisIdRep", typeof(Int32));

            DataTable DtProductoNoForestales = DsProductoNoForestales.Tables.Add("ProductoNoForestal");
            DataColumn Turno = DtProductoNoForestales.Columns.Add("Turno", typeof(Int32));
            DataColumn Rodal = DtProductoNoForestales.Columns.Add("Rodal", typeof(Int32));
            DataColumn Anis = DtProductoNoForestales.Columns.Add("Anis", typeof(Int32));
            DataColumn Area = DtProductoNoForestales.Columns.Add("Area", typeof(double));
            DataColumn Codigo_Producto = DtProductoNoForestales.Columns.Add("Codigo_Producto", typeof(int));
            DataColumn Producto = DtProductoNoForestales.Columns.Add("Producto", typeof(string));
            DataColumn Unidad_MedidaId = DtProductoNoForestales.Columns.Add("Unidad_MedidaId", typeof(int));
            DataColumn Unidad_Medida = DtProductoNoForestales.Columns.Add("Unidad_Medida", typeof(string));
            DataColumn Peso = DtProductoNoForestales.Columns.Add("Peso", typeof(double));


            DataTable DtProductoNoForestalesExtraer = DsProductoNoForestalesExtraer.Tables.Add("ProductoNoForestalExtraer");
            DataColumn TurnoExtraer = DtProductoNoForestalesExtraer.Columns.Add("TurnoExtraer", typeof(Int32));
            DataColumn RodalExtraer = DtProductoNoForestalesExtraer.Columns.Add("RodalExtraer", typeof(Int32));
            DataColumn AnisExtraer = DtProductoNoForestalesExtraer.Columns.Add("AnisExtraer", typeof(Int32));
            DataColumn AreaExtraer = DtProductoNoForestalesExtraer.Columns.Add("AreaExtraer", typeof(double));
            DataColumn Codigo_ProductoExtraer = DtProductoNoForestalesExtraer.Columns.Add("Codigo_ProductoExtraer", typeof(int));
            DataColumn ProductoExtraer = DtProductoNoForestalesExtraer.Columns.Add("ProductoExtraer", typeof(string));
            DataColumn Unidad_MedidaIdExtraer = DtProductoNoForestalesExtraer.Columns.Add("Unidad_MedidaIdExtraer", typeof(int));
            DataColumn Unidad_MedidaExtraer = DtProductoNoForestalesExtraer.Columns.Add("Unidad_MedidaExtraer", typeof(string));
            DataColumn PesoExtraer = DtProductoNoForestalesExtraer.Columns.Add("PesoExtraer", typeof(double));

            DataTable DtEspeciesRepoblacion = DsEspeciesRepoblacion.Tables.Add("EspeciesRepoblacion");
            DataColumn TurnoRepo = DtEspeciesRepoblacion.Columns.Add("TurnoRepo", typeof(Int32));
            DataColumn RodalRepo = DtEspeciesRepoblacion.Columns.Add("RodalRepo", typeof(Int32));
            DataColumn EtapaIdRepo = DtEspeciesRepoblacion.Columns.Add("EtapaIdRepo", typeof(int));
            DataColumn EtapaRepo = DtEspeciesRepoblacion.Columns.Add("EtapaRepo", typeof(string));
            DataColumn AreaRepo = DtEspeciesRepoblacion.Columns.Add("AreaRepo", typeof(double));
            DataColumn Tratamiento = DtEspeciesRepoblacion.Columns.Add("Tratamiento", typeof(string));
            DataColumn AnisRepo = DtEspeciesRepoblacion.Columns.Add("AnisRepo", typeof(Int32));
            DataColumn EspecieRepoId = DtEspeciesRepoblacion.Columns.Add("EspecieRepoId", typeof(int));
            DataColumn EspecieRepo = DtEspeciesRepoblacion.Columns.Add("EspecieRepo", typeof(string));
            DataColumn Descripcion = DtEspeciesRepoblacion.Columns.Add("Descripcion", typeof(string));
            DataColumn DensidadRepo = DtEspeciesRepoblacion.Columns.Add("DensidadRepo", typeof(double));
            DataColumn TiempoEje = DtEspeciesRepoblacion.Columns.Add("TiempoEje", typeof(string));
            DataColumn OtrasActividades = DtEspeciesRepoblacion.Columns.Add("OtrasActividades", typeof(string));
            DataColumn SistemaRepoId = DtEspeciesRepoblacion.Columns.Add("SistemaRepoId", typeof(int));
            DataColumn SistemaRepo = DtEspeciesRepoblacion.Columns.Add("SistemaRepo", typeof(string));

            DataTable DtAnexosCroquis = DsAnexos.Tables.Add("AnexoCroquis");
            DataColumn NombreAnexoCroquis = DtAnexosCroquis.Columns.Add("NombreAnexoCroquis", typeof(string));
            DataColumn PathAnexoCroquis = DtAnexosCroquis.Columns.Add("PathAnexoCroquis", typeof(string));


            DataTable DtAnexosMapaUsoActual = DsAnexos.Tables.Add("AnexoUsoActual");
            DataColumn NombreAnexoMapaUsoActual = DtAnexosMapaUsoActual.Columns.Add("NombreAnexoMapaUsoActual", typeof(string));
            DataColumn PathAnexoMapaUsoActual = DtAnexosMapaUsoActual.Columns.Add("PathAnexoMapaUsoActual", typeof(string));

            DataTable DtAnexosMapaPendintes = DsAnexos.Tables.Add("AnexoMapaPendiente");
            DataColumn NombreAnexoMapaPendiente = DtAnexosMapaPendintes.Columns.Add("NombreAnexoMapaPendiente", typeof(string));
            DataColumn PathAnexoMapaPendiente = DtAnexosMapaPendintes.Columns.Add("PathAnexoMapaPendiente", typeof(string));

            DataTable DtAnexosMapaUbicacion = DsAnexos.Tables.Add("AnexoMapaUbicacion");
            DataColumn NombreAnexoMapaUbicacion = DtAnexosMapaUbicacion.Columns.Add("NombreAnexoMapaUbicacion", typeof(string));
            DataColumn PathAnexoMapaUbicacion = DtAnexosMapaUbicacion.Columns.Add("PathAnexoMapaUbicacion", typeof(string));

            DataTable DtAnexosMapaRonda = DsAnexos.Tables.Add("AnexoMapaRonda");
            DataColumn NombreAnexoMapaRonda = DtAnexosMapaRonda.Columns.Add("NombreAnexoMapaRonda", typeof(string));
            DataColumn PathAnexoMapaRonda = DtAnexosMapaRonda.Columns.Add("PathAnexoMapaRonda", typeof(string));


            CboTipoDocumento.SelectedIndexChanged += CboTipoDocumento_SelectedIndexChanged;
            ChkIngNomFinca.CheckedChanged += ChkIngNomFinca_CheckedChanged;
            OptAreasPro.SelectedIndexChanged += OptAreasPro_SelectedIndexChanged;
            CboTipoPersona.SelectedIndexChanged += CboTipoPersona_SelectedIndexChanged;
            BtnValidarDpi.ServerClick += BtnValidarDpi_ServerClick;
            CboTipoIdPropietario.SelectedIndexChanged += CboTipoIdPropietario_SelectedIndexChanged;
            BtnValidarPasaporte.ServerClick += BtnValidarPasaporte_ServerClick;
            BtnAddPropietario.ServerClick += BtnAddPropietario_ServerClick;
            GrdPropietarios.NeedDataSource += GrdPropietarios_NeedDataSource;
            GrdPropietarios.ItemCommand += GrdPropietarios_ItemCommand;
            
            BtnCargar.ServerClick += BtnCargar_ServerClick;
            GrdPoligono.NeedDataSource += GrdPoligono_NeedDataSource;
            BtnAddEspecie.ServerClick += BtnAddEspecie_ServerClick;
            GrdEspecies.NeedDataSource += GrdEspecies_NeedDataSource;
            GrdEspecies.ItemCommand += GrdEspecies_ItemCommand;
            BtnCargaPolBosque.ServerClick += BtnCargaPolBosque_ServerClick;
            GrdPolBoque.NeedDataSource += GrdPolBoque_NeedDataSource;
            BtnCargaPolIntervenir.ServerClick += BtnCargaPolIntervenir_ServerClick;
            GrdPolIntervenir.NeedDataSource += GrdPolIntervenir_NeedDataSource;
            btnGrabarFinca.Click += btnGrabarFinca_Click;
            BtnNuevaFinca.ServerClick += BtnNuevaFinca_ServerClick;
            CboDepartamento.SelectedIndexChanged += CboDepartamento_SelectedIndexChanged;
            GrdInmuebles.NeedDataSource += GrdInmuebles_NeedDataSource;
            GrdInmuebles.ItemCommand += GrdInmuebles_ItemCommand;
            BtnAddFincaPlan.ServerClick += BtnAddFincaPlan_ServerClick;
            BtnGrabarNomEmpresa.ServerClick += BtnGrabarNomEmpresa_ServerClick;
            BtnGrabarAreas.Click += BtnGrabarAreas_Click;
            BtncargarPolProteccion.ServerClick += BtncargarPolProteccion_ServerClick;
            GrdPolProteccion.NeedDataSource += GrdPolProteccion_NeedDataSource;

            CboTipoIdentificacionRep.SelectedIndexChanged += CboTipoIdentificacionRep_SelectedIndexChanged;
            BtnValidarDpiRep.ServerClick += BtnValidarDpiRep_ServerClick;
            BtnValidarPasaporteRep.ServerClick += BtnValidarPasaporteRep_ServerClick;
            BtnAddRepresentante.ServerClick += BtnAddRepresentante_ServerClick;
            GrdRepresentantes.NeedDataSource += GrdRepresentantes_NeedDataSource;
            GrdRepresentantes.ItemCommand += GrdRepresentantes_ItemCommand;
            BtnGrabarDatosNotifica.Click += BtnGrabarDatosNotifica_Click;
            CboDepartamentoNotifica.SelectedIndexChanged += CboDepartamentoNotifica_SelectedIndexChanged;
            BtnAddEspeciePlanManejo.ServerClick += BtnAddEspeciePlanManejo_ServerClick;
            GrdEspeciePLanManejo.NeedDataSource += GrdEspeciePLanManejo_NeedDataSource;
            GrdEspeciePLanManejo.ItemCommand += GrdEspeciePLanManejo_ItemCommand;
            btnCargarPolAreaRepo.ServerClick += btnCargarPolAreaRepo_ServerClick;
            GrdPolAreaRepo.NeedDataSource += GrdPolAreaRepo_NeedDataSource;
            BtnGrabarInfoGenPlan.Click += BtnGrabarInfoGenPlan_Click;
            BtnGrabarCarcBiofisicas.Click += BtnGrabarCarcBiofisicas_Click;
            RadTabStrip1.TabClick += RadTabStrip1_TabClick;
            BtnGrabarPlanCientifico.Click += BtnGrabarPlanCientifico_Click;
            btnGrabarPlaga.Click += btnGrabarPlaga_Click;
            BtnGrabarMedidasControl.Click += BtnGrabarMedidasControl_Click;
            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            BtnGrabarProteccionForestal.Click += BtnGrabarProteccionForestal_Click;
            BtnGrabarActicidad.Click += BtnGrabarActicidad_Click;
            GrdActividades.NeedDataSource += GrdActividades_NeedDataSource;
            GrdActividades.ItemCommand += GrdActividades_ItemCommand;
            BtnAddActividadApro.Click += BtnAddActividadApro_Click;
            GrdActividadAprovechamiento.NeedDataSource += GrdActividadAprovechamiento_NeedDataSource;
            GrdActividadAprovechamiento.ItemCommand += GrdActividadAprovechamiento_ItemCommand;
            BtnGrabarCaminos.ServerClick += BtnGrabarCaminos_ServerClick;
            BtnCargarBoleta.ServerClick += BtnCargarBoleta_ServerClick;
            GrdBoleta.NeedDataSource += GrdBoleta_NeedDataSource;
            GrdResumen.NeedDataSource += GrdResumen_NeedDataSource;
            GrdResumen.PreRender += GrdResumen_PreRender;
            GrdResumen.ItemDataBound += GrdResumen_ItemDataBound;
            btnGrabarAprovechamiento.Click += btnGrabarAprovechamiento_Click;
            BtnGeneraCalculos.ServerClick += BtnGeneraCalculos_ServerClick;
            CboTipoIngresoDatos.SelectedIndexChanged += CboTipoIngresoDatos_SelectedIndexChanged;
            BtnAddEspecieRepo.ServerClick += BtnAddEspecieRepo_ServerClick;
            GrdEspecieRepo.NeedDataSource += GrdEspecieRepo_NeedDataSource;
            GrdEspecieRepo.ItemCommand += GrdEspecieRepo_ItemCommand;
            btnGrabarRepo.ServerClick += btnGrabarRepo_ServerClick;
            CboTurno.SelectedIndexChanged += CboTurno_SelectedIndexChanged;
            BtnAddProductoNoForestal.ServerClick += BtnAddProductoNoForestal_ServerClick;
            GrdProdNoForestal.NeedDataSource += GrdProdNoForestal_NeedDataSource;
            BtnGrabarProdNoMaderables.ServerClick += BtnGrabarProdNoMaderables_ServerClick;
            GrdProdNoForestal.ItemCommand += GrdProdNoForestal_ItemCommand;
            GrdSilvicultural.NeedDataSource += GrdSilvicultural_NeedDataSource;
            GrdSilvicultural.PreRender += GrdSilvicultural_PreRender;
            GrdSilvicultural.ItemCommand += GrdSilvicultural_ItemCommand;
            BtnGrabarSilvicultura.Click += BtnGrabarSilvicultura_Click;
            GrdSilvicultural.ItemDataBound += GrdSilvicultural_ItemDataBound;
            GrdArboles.NeedDataSource += GrdArboles_NeedDataSource;
            BtnGrabarArboles.ServerClick += BtnGrabarArboles_ServerClick;
            GrdArboles.ItemDataBound += GrdArboles_ItemDataBound;
            GrdProdNoMaderablesExtrae.NeedDataSource += GrdProdNoMaderablesExtrae_NeedDataSource;
            GrdProdNoMaderablesExtrae.ItemDataBound += GrdProdNoMaderablesExtrae_ItemDataBound;
            BtnGrabarProdNoMaderablesExtraeSave.Click += BtnGrabarProdNoMaderablesExtraeSave_Click;
            CboCriterioReg.SelectedIndexChanged += CboCriterioReg_SelectedIndexChanged;
            BtnGrabarOtrosAprovecha.Click += BtnGrabarOtrosAprovecha_Click;
            BtnAddEspecieRepoPlanifica.ServerClick += BtnAddEspecieRepoPlanifica_ServerClick;
            GrdEspeciesRepoblacion.NeedDataSource += GrdEspeciesRepoblacion_NeedDataSource;
            GrdEspeciesRepoblacion.ItemCommand += GrdEspeciesRepoblacion_ItemCommand;
            BtnSaveEspeciesRepo.ServerClick += BtnSaveEspeciesRepo_ServerClick;
            CboCambioUsoForestal.SelectedIndexChanged += CboCambioUsoForestal_SelectedIndexChanged;
            BtnEnviarSol.ServerClick += BtnEnviarSol_ServerClick;
            BtnYes.ServerClick += BtnYes_ServerClick;
            BtnCargarCroquis.ServerClick += BtnCargarCroquis_ServerClick;
            GrdAnexoCroquia.NeedDataSource += GrdAnexoCroquia_NeedDataSource;
            GrdAnexoCroquia.ItemCommand += GrdAnexoCroquia_ItemCommand;
            BtnCargarMapaUsoActual.ServerClick += BtnCargarMapaUsoActual_ServerClick;
            GrdAnexoMapaUsoActual.NeedDataSource += GrdAnexoMapaUsoActual_NeedDataSource;
            GrdAnexoMapaUsoActual.ItemCommand += GrdAnexoMapaUsoActual_ItemCommand;
            BtnCargarMapaPendiente.ServerClick += BtnCargarMapaPendiente_ServerClick;
            GrdAnexoMapaPendiente.NeedDataSource += GrdAnexoMapaPendiente_NeedDataSource;
            GrdAnexoMapaPendiente.ItemCommand += GrdAnexoMapaPendiente_ItemCommand;
            BtnCargarMapaUbicacion.ServerClick += BtnCargarMapaUbicacion_ServerClick;
            GrdAnexoMapaUbicacion.NeedDataSource += GrdAnexoMapaUbicacion_NeedDataSource;
            GrdAnexoMapaUbicacion.ItemCommand += GrdAnexoMapaUbicacion_ItemCommand;
            BtnCargarMapaRonda.ServerClick += BtnCargarMapaRonda_ServerClick;
            GrdAnexoMapaRonda.NeedDataSource += GrdAnexoMapaRonda_NeedDataSource;
            GrdAnexoMapaRonda.ItemCommand += GrdAnexoMapaRonda_ItemCommand;
            BtnPrintSolicitud.ServerClick += BtnPrintSolicitud_ServerClick;
            CboFinca.SelectedIndexChanged += CboFinca_SelectedIndexChanged;
            CboMunicipio.SelectedIndexChanged += CboMunicipio_SelectedIndexChanged;
            GrdEspeciesRepoblacion.PreRender += GrdEspeciesRepoblacion_PreRender;
            CboEtapa.SelectedIndexChanged += CboEtapa_SelectedIndexChanged;
            BtnGeneraRepo.ServerClick += BtnGeneraRepo_ServerClick;
            BtnGrabarCompromisoRepo.ServerClick += BtnGrabarCompromisoRepo_ServerClick;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                
                ClUsuario.Insertar_Ingreso_Pagina(48, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 48);
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
                TxtAsignacionId.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["affectation"].ToString()), true);
                GrdActividades.Rebind();
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
                ConfiguraPlanManejo(SubCategoriaId);
                ClUtilitarios.LlenaCombo(ClCatalogos.TipoDoc_Propiedad_GetAll(), CboTipoDocumento, "TipoDoc_PropiedadId", "TipoDocPropiedad");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoDocumento, "Tipo de Documento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(1), CboMunicipio, "MunicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.Titulo_GetAll(), CboTitulo, "TituloNotarioId", "TituloNotario");
                ClUtilitarios.LlenaCombo(ClCatalogos.Area_Protegida_GetAll(), CboArea, "AreaProtegidaId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboArea, "Área");
                ClUtilitarios.LlenaCombo(ClCatalogos.Tipo_Persona(), CboTipoPersona, "Tipo_PersonaId", "Tipo_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoPersona, "Tipo Persona");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdPropietario, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdPropietario, "Tipo de Identificación");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPais, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPais, "País");
                LblTipoPlan.Text = ClManejo.Get_SubCategoria(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["typeplan"].ToString()), true)));
                
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Especie(), CboEspecie, "EspecieId", "Especie");
                ClUtilitarios.AgregarSeleccioneCombo(CboEspecie, "Especie");
                TxtFecEmi.MaxDate = DateTime.Now;
                TxtFecEmi.SelectedDate = DateTime.Now;
                TxtUsrOwnPlan.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["utilisater"].ToString()), true);
                ClUtilitarios.LlenaCombo(ClInmueble.Inmueble_GetAll_Manejo(Convert.ToInt32(TxtUsrOwnPlan.Text),Convert.ToInt32(TxtAsignacionId.Text)), CboFinca, "InmuebleId", "Finca");
                ClUtilitarios.AgregarSeleccioneCombo(CboFinca, "Finca/Inmueble");
                BloquearFinca();
                if (CboFinca.Items.Count > 1)
                    BtnAddFincaPlan.Visible = true;

                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdentificacionRep, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdentificacionRep, "Tipo de Identificación");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPaisRep, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPaisRep, "País");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamentoNotifica, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamentoNotifica, "Departamento");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(1), CboMunicipioNotifica, "MunicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioNotifica, "Municipio");
                Get_Datos_Notifica();
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSistemaRepoblacion(), CboSistemaRepoblacion, "SistemaRepoblacioId", "SistemaRepoblacion");
                //ClUtilitarios.AgregarSeleccioneCombo(CboSistemaRepoblacion, "Sistema de Repoblación");
                
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Especie(), CboEspeciePlanManejo, "EspecieId", "Especie");
                ClUtilitarios.AgregarSeleccioneCombo(CboEspeciePlanManejo, "Especie");
                RetornoInfoGeneral_PlanManejo();
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoZona_Vida(), CboZonaVida, "Zona_VidaId", "Zona_Vida");
                ClUtilitarios.AgregarSeleccioneCombo(CboZonaVida, "Zona de Vida");
                RetornoCaracBiofisicas_PlanManejo();
                RetornoPlanCientifico_PlanManejo();
                RetornoPlagas_PlanManejo();
                RetornoMedidasControl_PlanManejo();
                RetornoProteccionForestal_PlanManejo();
                TxtFecIni.SelectedDate = DateTime.Now;
                TXtFecFin.SelectedDate = DateTime.Now;
                //TxtFecVenc.MinDate = DateTime.Now;
                
                //TxtFecVenceIdRep.MinDate = DateTime.Now;
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Descripcion_Actividad_Aprovechmiento(), CboTipoProducto, "Descripcion_ActividadId", "Descripcion_Actividad");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoProducto, "Tipo de Producto");
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Tipo_Aprovechamiento(), CboTipoAprovechamiento, "Tipo_AprovechamientoId", "Tipo_Aprovechamiento");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoAprovechamiento, "Tipo de Aprovechamiento");
                GrdActividadAprovechamiento.Rebind();
                RetornoCaminos_PlanManejo();
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_TipoIngresoDatos(), CboTipoIngresoDatos, "Tipo_Ingreso_DatosId", "Tipo_ingreso_Datos");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIngresoDatos, "Tipo de Ingreso de Datos");
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_TipoInventario(), CboTipoInventario, "Tipo_InventarioId", "Tipo_Inventario");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoInventario, "Tipo de Inventario");
                GrdBoleta.Rebind();
                GrdResumen.Rebind();
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_Ecuacion_Volumen(), CboEcuacion, "EcuacionId", "Ecuacion");
                RetornoAprovechamiento_Forestal_PlanManejo();
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Especie(), CboEspecieRepoblacion, "EspecieId", "Especie");
                ClUtilitarios.AgregarSeleccioneCombo(CboEspecieRepoblacion, "Especie");
                RetornoAreaProteccion();
                GrdPropietarios.Rebind();
                GrdRepresentantes.Rebind();
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Productos(), CboProducto, "Codigo_Producto", "Nombre_Producto");
                ClUtilitarios.AgregarSeleccioneCombo(CboProducto, "Producto");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Unidad_Medida(), CboUMedida, "Unidad_MedidaId", "Unidad_Medida");
                ClUtilitarios.AgregarSeleccioneCombo(CboUMedida, "Unidad de medida");
                GrdProdNoForestal.Rebind();
                ProdNoMaderablesYaExistentes(Convert.ToInt32(TxtAsignacionId.Text));
                GrdSilvicultural.Rebind();
                //GrdBoleta.Rebind();
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Criterio_Regulacion(), CboCriterioReg, "Criterio_RegulacionId", "Creiterio_Regulacion");
                ClUtilitarios.AgregarSeleccioneCombo(CboCriterioReg, "Criterio");
                BloquearTodoslosVolumenes();
                GrdProdNoMaderablesExtrae.Rebind();
                ClUtilitarios.LlenaCombo(ClManejo.Get_Rodal_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text)), CboRodal, "Rodal", "Rodal");
                ClUtilitarios.AgregarSeleccioneCombo(CboRodal, "Rodal");
                ClUtilitarios.AgregarSeleccioneCombo(CboFormula, "Formula");
                RetornoPlanificacionManejo_Forestal_PlanManejo();
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Especie(), CboEspecieRepo, "EspecieId", "Especie");
                ClUtilitarios.AgregarSeleccioneCombo(CboEspecieRepo, "Especie");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSistemaRepoblacion(), CboSistemaRepo, "SistemaRepoblacioId", "SistemaRepoblacion");
                ClUtilitarios.AgregarSeleccioneCombo(CboSistemaRepo, "Sistema Repoblación");
                EspeciesRepoblacionYaExistentes(Convert.ToInt32(TxtAsignacionId.Text));
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_CambioUso(), CboCambioUsoForestal, "TipoCambioUsoId", "TipoCambioUso");
                ClUtilitarios.AgregarSeleccioneCombo(CboCambioUsoForestal, "Tipo Cambio de Uso");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoGenero(), CboGenero, "GeneroId", "Genero");
                ClUtilitarios.AgregarSeleccioneCombo(CboGenero, "Genero");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoOcupacion(), CboOcupacion, "OcupacionId", "Ocupacion");
                ClUtilitarios.AgregarSeleccioneCombo(CboOcupacion, "Ocupacion");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEstadoCivil(), CboEstadoCivil, "EstadoCivilId", "EstadoCivil");
                ClUtilitarios.AgregarSeleccioneCombo(CboEstadoCivil, "Estado Civil");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoGenero(), CboGeneroRep, "GeneroId", "Genero");
                ClUtilitarios.AgregarSeleccioneCombo(CboGeneroRep, "Genero");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoOcupacion(), CboOcupacionRep, "OcupacionId", "Ocupacion");
                ClUtilitarios.AgregarSeleccioneCombo(CboOcupacionRep, "Ocupacion");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEstadoCivil(), CboEstadoCivilRep, "EstadoCivilId", "EstadoCivil");
                ClUtilitarios.AgregarSeleccioneCombo(CboEstadoCivilRep, "Estado Civil");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEtapa(), CboEtapa, "EtapaId", "Etapa");
                ClUtilitarios.AgregarSeleccioneCombo(CboEtapa, "Etapa");
                RetornoCalculosCompromisoForestal();
            }
            else
            {
                if (RadUploadFile.UploadedFiles.Count > 0)
                {
                    Stream fileStream = RadUploadFile.UploadedFiles[0].InputStream;
                }
            }

        }

        void BtnGrabarCompromisoRepo_ServerClick(object sender, EventArgs e)
        {
            ClManejo.Insert_Temp_Compromiso_Calculo(Convert.ToInt32(TxtAsignacionId.Text), TxtAreaBasalExtrae.Text, TxtAreaBasalExis.Text, TxtAreaTotIntervenir.Text, TxtAreaCompromiso.Text);
            DivGoodCalculoRepo.Visible = true;
            LblGoodCalculoRepo.Text = "Datos Grabados";
        }

        void CalculaCompromisoRepoblacion()
        {
            double AreaBasalRodal = 0;
            double AreaBasalRodalTodos = 0;
            double AreaRodal = 0;
            TxtAreaBasalExtrae.Text = "";
            TxtAreaBasalExis.Text = "";
            TxtAreaTotIntervenir.Text = "";
            TxtAreaCompromiso.Text = "";
            int TipodeSeleccion = 0; //0 Nada 1 Algo 2Solo Corta Final
            for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
            {
                RadComboBox CboTratamiento = ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento"));
                RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtTurno"));
                CheckBox ChkExtrae = ((CheckBox)this.GrdSilvicultural.Items[i].FindControl("ChkExtrae"));


                if ((CboTratamiento.SelectedValue != "") && (TxtTurno.Text != "") && (ChkExtrae.Checked == true))
                {
                    if (Convert.ToInt32(CboTratamiento.SelectedValue) != 1)
                    {
                        TipodeSeleccion = 1;
                        break;
                    }
                    else
                    {
                        TipodeSeleccion = 2;
                    }
                }
            }
            if (TipodeSeleccion == 2)
            {
                

                for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
                {
                    RadComboBox CboTratamiento = ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento"));
                    RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtTurno"));
                    CheckBox ChkExtrae = ((CheckBox)this.GrdSilvicultural.Items[i].FindControl("ChkExtrae"));

                    RadNumericTextBox TxtAreaBasalRodal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtAreaBasalRodal"));
                    RadNumericTextBox TxtAreaRodal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtAreaRodal"));


                    if ((CboTratamiento.SelectedValue != "") && (TxtTurno.Text != "") && (ChkExtrae.Checked == true))
                    {
                        if (TxtAreaBasalRodal.Text != "")
                            AreaBasalRodal = AreaBasalRodal + Convert.ToDouble(TxtAreaBasalRodal.Text);
                        if (TxtAreaRodal.Text != "")
                            AreaRodal = AreaRodal + Convert.ToDouble(TxtAreaRodal.Text);
                    }
                    AreaBasalRodalTodos = AreaBasalRodalTodos + Convert.ToDouble(TxtAreaBasalRodal.Text);
                }
                TxtAreaBasalExtrae.Enabled = false;
                TxtAreaBasalExtrae.Text = AreaBasalRodal.ToString();
                TxtAreaBasalExis.Enabled = false;
                TxtAreaBasalExis.Text = AreaBasalRodalTodos.ToString();
                TxtAreaTotIntervenir.Enabled = false;
                TxtAreaTotIntervenir.Text = AreaRodal.ToString();
                if (AreaBasalRodalTodos == 0)
                    TxtAreaCompromiso.Text = "0";
                else
                    TxtAreaCompromiso.Text = Math.Round(((AreaBasalRodal / AreaBasalRodalTodos) * AreaRodal),2).ToString();
            }
            else if (TipodeSeleccion == 1)
            {
                for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
                {
                    RadComboBox CboTratamiento = ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento"));
                    RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtTurno"));
                    CheckBox ChkExtrae = ((CheckBox)this.GrdSilvicultural.Items[i].FindControl("ChkExtrae"));

                    RadNumericTextBox TxtAreaRodal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtAreaRodal"));
                    if ((CboTratamiento.SelectedValue != "") && (TxtTurno.Text != "") && (ChkExtrae.Checked == true))
                    {
                        if (TxtAreaRodal.Text != "")
                            AreaRodal = AreaRodal + Convert.ToDouble(TxtAreaRodal.Text);
                    }
                }
                TxtAreaBasalExtrae.Enabled = true;
                TxtAreaBasalExis.Enabled = true;
                TxtAreaTotIntervenir.Enabled = true;
                TxtAreaCompromiso.Text = AreaRodal.ToString();
            }
            else
            {
                TxtAreaBasalExtrae.Enabled = false;
                TxtAreaBasalExtrae.Text = "";
                TxtAreaBasalExis.Enabled = false;
                TxtAreaBasalExis.Text = "";
                TxtAreaTotIntervenir.Enabled = false;
                TxtAreaTotIntervenir.Text = "";
                TxtAreaCompromiso.Text = "";
            }
        }

        void BtnGeneraRepo_ServerClick(object sender, EventArgs e)
        {
            CalculaCompromisoRepoblacion();
        }

        void CboEtapa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TxtAreaRepo.Text = "";
            TxtTrataminetoRepo.Text = "";
            TxtAnisRepo.Text = "";
        }

        void GrdEspeciesRepoblacion_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in this.GrdEspeciesRepoblacion.Items)
            {
                if (item.OwnerTableView.Name == "LabelsResumen")
                {
                    GridTableView ownerTableView = item.OwnerTableView;
                    for (int i = ownerTableView.Items.Count - 2; i >= 0; i--)
                    {
                        GridDataItem item2 = ownerTableView.Items[i];
                        GridDataItem item3 = ownerTableView.Items[i + 1];
                        if ((item2["Turno"].Text == item3["Turno"].Text) && (item2["Rodal"].Text == item3["Rodal"].Text) && (item2["EtapaRepo"].Text == item3["EtapaRepo"].Text))
                        {

                            item2["Area"].RowSpan = (item3["Area"].RowSpan < 2) ? 2 : (item3["Area"].RowSpan + 1);
                            item3["Area"].Visible = false;

                            item2["Tratamiento"].RowSpan = (item3["Tratamiento"].RowSpan < 2) ? 2 : (item3["Tratamiento"].RowSpan + 1);
                            item3["Tratamiento"].Visible = false;
                            
                            item2["Anis"].RowSpan = (item3["Anis"].RowSpan < 2) ? 2 : (item3["Anis"].RowSpan + 1);
                            item3["Anis"].Visible = false;

                            

                        }
                    }
                }
            }
        }

        void CboMunicipio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
        }

        void CboFinca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboFinca.Text != "")
            {
                CargaDatosFinca(Convert.ToInt32(CboFinca.SelectedValue));                
            }
        }

        void BtnPrintSolicitud_ServerClick(object sender, EventArgs e)
        {
            DivErrorGeneral.Visible = false;
            lblMensajeErrorGen.Text = "";
            bool HayError = false;
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            if (GrdInmuebles.Items.Count == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar al menos una finca";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar al menos una finca";
                HayError = true;
            }
            string MensajeTemp = "";
            if (ValidaDatosFincaPropietarios(ref MensajeTemp) == true)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTemp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTemp; ;
                HayError = true;
            }
            if (ValidaDatosFincaAreas(ref MensajeTemp) == true)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTemp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTemp; ;
                HayError = true;
            }
            string MensajeTempMismaFinca = "";
            if (TodasLasFincasMismaSubRegion(ref MensajeTempMismaFinca) == false)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTempMismaFinca;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTempMismaFinca;
                HayError = true;
            }
            string MensajeDiferentiTipoProp = "";
            bool ValidaMismosPropietarios = true;
            int TipoPropietario = 1;
            if (ValidaIgualTipoPropietario(Convert.ToInt32(TxtAsignacionId.Text), ref MensajeDiferentiTipoProp, ref  ValidaMismosPropietarios, ref TipoPropietario) == false)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeDiferentiTipoProp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeDiferentiTipoProp;
                HayError = true;
            }
            if (ValidaMismosPropietarios == true)
            {
                string MensajeMismosPropietarios = "";
                if (TempValidaMismosPropietarios(Convert.ToInt32(TxtAsignacionId.Text), TipoPropietario, ref MensajeMismosPropietarios) == true)
                {
                    if (lblMensajeErrorGen.Text == "")
                        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeMismosPropietarios;
                    else
                        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeMismosPropietarios;
                    HayError = true;
                }
            }
            //if (ClManejo.Existe_Representatnes_PlanManejo(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar al menos un representante";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar al menos un representante";
            //    HayError = true;
            //}
            if (ClManejo.Existe_DatosNotifica(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar los datos de notificación";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar los datos de notificación";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrorGeneral.Visible = true;
            }
            else
            {
                DataSet dsRegioSubregionInmueble = ClInmueble.Get_Region_Subregion_Inmueble(Convert.ToInt32(GrdInmuebles.Items[0].OwnerTableView.DataKeyValues[0]["InmuebleId"]));
                int RegionId = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["RegionId"].ToString());
                int SubRegionId = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString());
                RadWindow1.Title = "Solicitud Plan de Manejo Forestal";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepSolicitudPlanManejo.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtAsignacionId.Text, true)) + "&idnoiger=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(RegionId.ToString(), true)) + "&sousnoigerid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            
        }

        void GrdAnexoMapaRonda_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaRonda\" + AsignacionId;
                if (Directory.Exists(PathArchivo))
                {
                    DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                    FileInfo[] files = directory.GetFiles("*.*");
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < files.Length; i++)
                    {
                        ((FileInfo)files[i]).Delete();
                    }
                    GrdAnexoMapaRonda.Rebind();
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Archivo eliminado";
                }
            }
            else if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de rondas corta fuegos perimetrales e intermedias dentro del area de compromiso de repoblacion forestal";
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaRonda"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaRonda_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaRonda\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaRonda"].NewRow();
                    item["NombreAnexoMapaRonda"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaRonda"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaRonda"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaRonda"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaRonda, "AnexoMapaRonda");
            }
        }

        void BtnCargarMapaRonda_ServerClick(object sender, EventArgs e)
        {
            DivErrAnexo.Visible = false;
            DivGoodAnexo.Visible = false;
            try
            {
                foreach (UploadedFile f in UploadAnexoMapaRonda.UploadedFiles)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaRonda\" + AsignacionId;
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Mapa de rondas corta fuegos perimetrales e intermedias dentro del area de compromiso de repoblacion forestal cargado";
                    GrdAnexoMapaRonda.Rebind();
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
                DivErrAnexo.Visible = true;
                LblErrAnexo.Text = "Error: " + iM;
            }
        }

        void GrdAnexoMapaUbicacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUbicacion\" + AsignacionId;
                if (Directory.Exists(PathArchivo))
                {
                    DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                    FileInfo[] files = directory.GetFiles("*.*");
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < files.Length; i++)
                    {
                        ((FileInfo)files[i]).Delete();
                    }
                    GrdAnexoMapaUbicacion.Rebind();
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Archivo eliminado";
                }
            }
            else if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de ubicación del área a aprovechar, caminos.";
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaUbicacion"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaUbicacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUbicacion\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaUbicacion"].NewRow();
                    item["NombreAnexoMapaUbicacion"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaUbicacion"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaUbicacion"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaUbicacion"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaUbicacion, "AnexoMapaUbicacion");
            }
        }

        void BtnCargarMapaUbicacion_ServerClick(object sender, EventArgs e)
        {
            DivErrAnexo.Visible = false;
            DivGoodAnexo.Visible = false;
            try
            {
                foreach (UploadedFile f in UploadAnexoMapaUbicacion.UploadedFiles)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUbicacion\" + AsignacionId;
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Mapa de ubicación del área a aprovechar, caminos cargado";
                    GrdAnexoMapaUbicacion.Rebind();
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
                DivErrAnexo.Visible = true;
                LblErrAnexo.Text = "Error: " + iM;
            }
        }

        void GrdAnexoMapaPendiente_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaPendiente\" + AsignacionId;
                if (Directory.Exists(PathArchivo))
                {
                    DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                    FileInfo[] files = directory.GetFiles("*.*");
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < files.Length; i++)
                    {
                        ((FileInfo)files[i]).Delete();
                    }
                    GrdAnexoMapaPendiente.Rebind();
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Archivo eliminado";
                }
            }
            else if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de pendientes.";
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaPendiente"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaPendiente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaPendiente\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaPendiente"].NewRow();
                    item["NombreAnexoMapaPendiente"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaPendiente"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaPendiente"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaPendiente"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaPendiente, "AnexoMapaPendiente");
            }
        }

        void BtnCargarMapaPendiente_ServerClick(object sender, EventArgs e)
        {
            DivErrAnexo.Visible = false;
            DivGoodAnexo.Visible = false;
            try
            {
                foreach (UploadedFile f in UploadAnexoMapaPendiente.UploadedFiles)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaPendiente\" + AsignacionId;
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Mapa de pendiente cargado";
                    GrdAnexoMapaPendiente.Rebind();
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
                DivErrAnexo.Visible = true;
                LblErrAnexo.Text = "Error: " + iM;
            }
        }

        void GrdAnexoMapaUsoActual_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUsoActual\" + AsignacionId;
                if (Directory.Exists(PathArchivo))
                {
                    DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                    FileInfo[] files = directory.GetFiles("*.*");
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < files.Length; i++)
                    {
                        ((FileInfo)files[i]).Delete();
                    }
                    GrdAnexoMapaUsoActual.Rebind();
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Archivo eliminado";
                }
            }
            else if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de uso actual y recursos hidricos";
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaUsoActual"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaUsoActual_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUsoActual\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoUsoActual"].NewRow();
                    item["NombreAnexoMapaUsoActual"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaUsoActual"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoUsoActual"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoUsoActual"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaUsoActual, "AnexoUsoActual");
            }
        }

        void BtnCargarMapaUsoActual_ServerClick(object sender, EventArgs e)
        {
            DivErrAnexo.Visible = false;
            DivGoodAnexo.Visible = false;
            try
            {
                foreach (UploadedFile f in UploadAnexoMapaUsoActual.UploadedFiles)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\MapaUsoActual\" + AsignacionId;
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Mapa de uso actual y recursos hidricos cargado";
                    GrdAnexoMapaUsoActual.Rebind();
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
                DivErrAnexo.Visible = true;
                LblErrAnexo.Text = "Error: " + iM;
            }
        }

        void GrdAnexoCroquia_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId;
                if (Directory.Exists(PathArchivo))
                {
                    DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                    FileInfo[] files = directory.GetFiles("*.*");
                    DirectoryInfo[] directories = directory.GetDirectories();
                    for (int i = 0; i < files.Length; i++)
                    {
                        ((FileInfo)files[i]).Delete();
                    }
                    GrdAnexoCroquia.Rebind();
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Archivo eliminado";
                }
            }
            else if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Croquis de acceso a la finca desde el casco municipal";
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoCroquis"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoCroquia_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoCroquis"].NewRow();
                    item["NombreAnexoCroquis"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoCroquis"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoCroquis"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoCroquis"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoCroquia, "AnexoCroquis");
            }

        }

        void BtnCargarCroquis_ServerClick(object sender, EventArgs e)
        {
            DivErrAnexo.Visible = false;
            DivGoodAnexo.Visible = false;
            try
            {
                foreach (UploadedFile f in UploadAnexoCroquis.UploadedFiles)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId;
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivGoodAnexo.Visible = true;
                    LblGoodAnexo.Text = "Croquis de acceso a la finca desde el casco municipal cargado";
                    GrdAnexoCroquia.Rebind();
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
                DivErrAnexo.Visible = true;
                LblErrAnexo.Text = "Error: " + iM;
            }
        }


        void BtnYes_ServerClick(object sender, EventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            int GestionId = Cl_Gestion.MaxGestionId(1);
            int Correlativo_Anual = Cl_Gestion.MaxGestionId(2);
            string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;
            int PersonaId = ClManejo.Get_PersonaId_AsignacionId(AsignacionId);

            Cl_Gestion.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(PersonaId), Convert.ToInt32(TxtSubRegionId.Text), 13, 2, Correlativo_Anual);
            ClManejo.Traslada_TempManejo(GestionId, AsignacionId);
            ClManejo.Traslada_PlanManejo_ConvierteXml(GestionId, AsignacionId);
            ClManejo.Elimina_TempPlanManejo(AsignacionId);
            string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + NUG + ". Por lo que solicitamos presentarse a la oficina Subregional " + TxtSubRegion.Text + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
            ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 0, "", "");
            Response.Redirect("~/WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG, true)) + "");
        }


        


        bool TodasLasFincasMismaSubRegion(ref string Mensaje)
        {
            int SubRegion = 0;
            bool TodasEnMisma = true;
            for (int i = 0; i < GrdInmuebles.Items.Count; i++)
            {
                DataSet dsRegioSubregionInmueble = ClInmueble.Get_Region_Subregion_Inmueble(Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                if (SubRegion == 0)
                {
                    SubRegion = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString());
                    TxtSubRegionId.Text = SubRegion.ToString();
                    TxtRegion.Text = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["Region"].ToString();
                    TxtSubRegion.Text = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubRegion"].ToString();
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


        bool ValidaIgualTipoPropietario(int AsignacionId, ref string Mensaje, ref bool ValidaMismosPropietarios, ref int TipoPropietario)
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
                        PrimerTipo = ClManejo.Get_Tipo_Propietario_Finca_Manejo(AsignacionId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                        TipoPropietario = PrimerTipo;
                    }
                    else
                    {
                        TempTipo = ClManejo.Get_Tipo_Propietario_Finca_Manejo(AsignacionId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
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

        bool TempValidaMismosPropietarios(int AsignacionId, int TipoPropietario, ref string Mensaje)
        {
            bool HayDiferente = false;

            if (GrdInmuebles.Items.Count > 1)
            {
                for (int i = 0; i < GrdInmuebles.Items.Count; i++)
                {
                    if (TipoPropietario == 1)
                    {
                        DataSet Propietarios = ClManejo.Get_propietarios_Temp_Finca_Manejo(AsignacionId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                        for (int j = 0; j < Propietarios.Tables["Datos"].Rows.Count; j++)
                        {
                            //obtener todos los demas inmuebles 
                            DataSet Inmuebles = ClManejo.Get_Otras_Temp_Finca_Manejo(AsignacionId, Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"]));
                            for (int k = 0; k < Inmuebles.Tables["Datos2"].Rows.Count; k++)
                            {
                                if (ClManejo.Existe_Propietarios_OtroInmueble_Manejo(AsignacionId, Convert.ToInt32(Inmuebles.Tables["Datos2"].Rows[k]["InmuebleId"]), Convert.ToInt32(Propietarios.Tables["Datos"].Rows[j]["PersonaId"])) == 0)
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
                        DataSet Fincas = ClManejo.Get_Fincas_Completas_Manejo(Convert.ToInt32(TxtAsignacionId.Text));
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

        bool ValidaPlanManejo()
        {
            DivErrorGeneral.Visible = false;
            lblMensajeErrorGen.Text = "";
            bool HayError = false;
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            if (GrdInmuebles.Items.Count == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar al menos una finca";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar al menos una finca";
                HayError = true;
            }
            string MensajeTemp = "";
            if (ValidaDatosFincaPropietarios(ref MensajeTemp) == true)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTemp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTemp; ;
                HayError = true;
            }
            if (ValidaDatosFincaAreas(ref MensajeTemp) == true)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTemp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTemp; ;
                HayError = true;
            }
            string MensajeTempMismaFinca = "";
            if (TodasLasFincasMismaSubRegion(ref MensajeTempMismaFinca) == false)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeTempMismaFinca;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeTempMismaFinca;
                HayError = true;
            }
            string MensajeDiferentiTipoProp = "";
            bool ValidaMismosPropietarios = true;
            int TipoPropietario = 1;
            if (ValidaIgualTipoPropietario(Convert.ToInt32(TxtAsignacionId.Text), ref MensajeDiferentiTipoProp, ref  ValidaMismosPropietarios, ref TipoPropietario) == false)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeDiferentiTipoProp;
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeDiferentiTipoProp;
                HayError = true;
            }
            if (ValidaMismosPropietarios == true)
            {
                string MensajeMismosPropietarios = "";
                if (TempValidaMismosPropietarios(Convert.ToInt32(TxtAsignacionId.Text), TipoPropietario, ref MensajeMismosPropietarios) == true)
                {
                    if (lblMensajeErrorGen.Text == "")
                        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + " " + MensajeMismosPropietarios;
                    else
                        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", " + MensajeMismosPropietarios;
                    HayError = true;
                }
            }
            //if (ClManejo.Existe_Representatnes_PlanManejo(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar al menos un representante";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar al menos un representante";
            //    HayError = true;
            //}
            if (ClManejo.Existe_DatosNotifica(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar los datos de notificación";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar los datos de notificación";
                HayError = true;
            }
            if (ClManejo.Existe_InfoGeneral_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar los datos de información general";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar los datos información general";
                HayError = true;
            }
            //if (ClManejo.Existe_InfoGeneral_SistemaRepoblacion(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe seleccionar el sistema de repoblación";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe seleccionar el sistema de repoblación";
            //    HayError = true;
            //}
            if (ClManejo.Existe_InfoGeneral_Poligono_Repoblacion(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar el poligono del Área de Repoblación Forestal";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar el poligono del Área de Repoblación Forestal";
                HayError = true;
            }
            if (ClManejo.Existe_Caracbiofisicas_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar los datos de las caracteristicas biofisicas";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar los datos de las caracteristicas biofisicas";
                HayError = true;
            }
            if (ClManejo.Existe_Aprovechamiento_PlanMenejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe seleccionar el tipo de ingreso de datos e inventario";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe seleccionar el tipo de ingreso de datos e inventario";
                HayError = true;
            }
            //if (ClManejo.Existe_Ecuacion_PlanManejo(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe seleccionar la o las ecuaciones";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe seleccionar la o las ecuaciones";
            //    HayError = true;
            //}
            if (ClManejo.Existe_Resumen_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar el resumen del aprovechamiento forestal";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar el resumen del aprovechamiento forestal";
                HayError = true;
            }
            //if (ClManejo.Existe_Productos_NoMaderables(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe ingresar al menos un producto no maderable";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe ingresar al menos un producto no maderable";
            //    HayError = true; 
            //}
            if (ClManejo.Existe_Silvicultura_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar el tratamiento de silvicultura";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar el tratamiento de silvicultura";
                HayError = true;
            }
            //if (ClManejo.Existe_Productos_NoMaderablesExtraer(AsignacionId) == 0)
            //{
            //    if (lblMensajeErrorGen.Text == "")
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar los productos no maderables a extraer";
            //    else
            //        lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar los productos no maderables a extraer";
            //    HayError = true;
            //}
            if (ClManejo.Existe_PlanificacionManejo_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar los datos de la planificación del manejo";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar los datos de la planificación del manejo";
                HayError = true;
            }
            if (ClManejo.Existe_Sistema_Repoblacion_Especie(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar al menos una especie de repoblación";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar al menos una especie de repoblación";
                HayError = true;
            }
            if (ClManejo.Existe_ProteccionForestal_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar la información de proteccion forestal";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar la información de proteccion forestal";
                HayError = true;
            }
            if (ClManejo.Existe_Cronograma_PlanManejo(AsignacionId) == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar el cronograma";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar el cronograma";
                HayError = true;
            }
            DataSet dsDatosCalculosCompromiso = ClManejo.Get_CalculosCompromisoForestal(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosCalculosCompromiso.Tables["Datos"].Rows.Count == 0)
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "Debe guardar los calculos del compromiso forestal";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", debe guardar los calculos del compromiso forestal";
                HayError = true;
            }
            dsDatosCalculosCompromiso.Clear();
            double TotalAreaRepoMEtodo = 0;
            for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
            {
                TotalAreaRepoMEtodo = Math.Round(TotalAreaRepoMEtodo + Convert.ToDouble(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("AreaRepo")),2);
            }
            if (TotalAreaRepoMEtodo != Convert.ToDouble(TxtAreaCompromiso.Text))
            {
                if (lblMensajeErrorGen.Text == "")
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + "El área de compromiso debe ser igual a la sumatoria de las áreas del descripción del metodo";
                else
                    lblMensajeErrorGen.Text = lblMensajeErrorGen.Text + ", El área de compromiso debe ser igual a la sumatoria de las áreas del descripción del metodo";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrorGeneral.Visible = true;
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
                if (ClManejo.TienePropietarioNomEmp_FincaManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"])) == 0)
                {
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
                if (ClManejo.TieneAras_FincaManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["InmuebleId"])) == 0)
                {
                    Mensaje = "A la Finca: " + GrdInmuebles.Items[i].OwnerTableView.DataKeyValues[i]["Finca"].ToString() + " no se ha ingresado el área forestal";
                    Valida = true;
                    break;
                }

            }
            return Valida;
        }

        void TrasladoPlanTablas()
        {

        }

        void BtnEnviarSol_ServerClick(object sender, EventArgs e)
        {
            if (ValidaPlanManejo() == true)
            {
                LblTitConfirmacion.Text = "La Gestíon sera enviada al INAB, ¿esta seguro (a)?";
                if (ClManejo.Existe_Representatnes_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text)) == 0)
                {
                    DocPropietario.Visible = true;
                }
                else
                {
                    DocRepresentante.Visible = true;
                    DocRepresentanteDos.Visible = true;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void CboCambioUsoForestal_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboCambioUsoForestal.SelectedValue == "2")
                DivEspecifiqueCambioUso.Visible = true;
            else
                DivEspecifiqueCambioUso.Visible = false;
        }

        bool LasDosEtapadaSistemaRepo()
        {
            bool NoHayEtapa = true;
            int TurnoActual = 0;
            int RodalActual = 0;
            int EtapaActual = 0;
            int EtapaBuscar = 1;
            string EtapaBuscarDesc = "";
            for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
            {
                TurnoActual = Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("TurnoRepo"));
                RodalActual = Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("RodalRepo"));
                EtapaActual = Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("EtapaIdRepo"));
                if (EtapaActual == 1)
                {
                    EtapaBuscar = 2;
                    EtapaBuscarDesc = "Mantenimiento";
                }
                else
                {
                    EtapaBuscar = 1;
                    EtapaBuscarDesc = "Establecimiento";
                }

                for (int j = 0; j < GrdEspeciesRepoblacion.Items.Count; j++)
                {
                    if ((TurnoActual == Convert.ToInt32(GrdEspeciesRepoblacion.Items[j].GetDataKeyValue("TurnoRepo"))) && (RodalActual == Convert.ToInt32(GrdEspeciesRepoblacion.Items[j].GetDataKeyValue("RodalRepo"))) && (EtapaBuscar == Convert.ToInt32(GrdEspeciesRepoblacion.Items[j].GetDataKeyValue("EtapaIdRepo"))))
                    {
                        NoHayEtapa = false;
                        break;
                    }
                    else
                    {
                        NoHayEtapa = true;
                    }
                }
                if (NoHayEtapa == true)
                {
                    DivErrEspeciesRepo.Visible = true;
                    LblErrEspeciesRepo.Text = "Al Turno: " + GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("TurnoRepo") + " y rodal: " + GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("RodalRepo") + " falta ingrese la etapa de: " + EtapaBuscarDesc;
                    break;
                }
            }
            
            return NoHayEtapa;
        }

        void BtnSaveEspeciesRepo_ServerClick(object sender, EventArgs e)
        {
            DivGoodEspeciesRepo.Visible = false;
            DivErrEspeciesRepo.Visible = false;
            if (LasDosEtapadaSistemaRepo() == false)
            {
                if (GrdEspeciesRepoblacion.Items.Count > 0)
                {
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Especies");
                    XmlNode iElementos = iInformacion.CreateElement("Especies");
                    for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                        ClXml.AgregarAtributo("Turno", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TurnoRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Rodal", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["RodalRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("EtapaId", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EtapaIdRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Area", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AreaRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Tratamiento", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Tratamiento"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Anis", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AnisRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("EspecieId", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EspecieRepoId"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Descripcion", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Descripcion"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("Densidad", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["DensidadRepo"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("TiempoEje", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TiempoEje"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("OtrasActividades", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["OtrasActividades"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                        ClXml.AgregarAtributo("SistemaRepoId", GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["SistemaRepoId"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                    }
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                    ClManejo.Insert_Especies_Repoblacion(Convert.ToInt32(TxtAsignacionId.Text), iInformacion);
                }
                DivGoodEspeciesRepo.Visible = true;
                LblGoodEspeciesRepo.Text = "Especies ingresadas correctamente";
            }
            
            
        }

        void LimpiarRepoblacionEspecie()
        {
            CboEspecieRepo.ClearSelection();
            TxtDescripcion.Text = "";
            TxtDensidadRepo.Text = "";
            TxtTiempoEjecucion.Text = "";
            TxtOtrasActividades.Text = "";
            CboSistemaRepo.ClearSelection();
        }

        void GrdEspeciesRepoblacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                int Turno = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TurnoRepo"].ToString().Trim());
                int Rodal = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RodalRepo"].ToString().Trim());
                int EtapaIdRepo = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EtapaIdRepo"].ToString().Trim());
                int EspecieId =  Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieRepoId"].ToString().Trim());

                for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
                {
                    if ((Turno == Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[i]["TurnoRepo"].ToString().Trim())) && (Rodal == Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[i]["RodalRepo"].ToString().Trim())) && (EtapaIdRepo == Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[i]["EtapaIdRepo"].ToString().Trim())) && (EspecieId == Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[i]["EspecieRepoId"].ToString().Trim())))
                    {

                    }
                    else
                    {
                        DataRow itemGrid = DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].NewRow();
                        itemGrid["TurnoRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TurnoRepo"];
                        itemGrid["RodalRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["RodalRepo"];
                        itemGrid["EtapaIdRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EtapaIdRepo"];
                        itemGrid["EtapaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EtapaRepo"];
                        itemGrid["AreaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AreaRepo"];
                        itemGrid["Tratamiento"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Tratamiento"];
                        itemGrid["AnisRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AnisRepo"];
                        itemGrid["EspecieRepoId"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EspecieRepoId"];
                        itemGrid["EspecieRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EspecieRepo"];
                        itemGrid["Descripcion"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Descripcion"];
                        itemGrid["TiempoEje"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TiempoEje"];
                        itemGrid["OtrasActividades"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["OtrasActividades"];
                        itemGrid["DensidadRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["DensidadRepo"];
                        itemGrid["SistemaRepoId"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["SistemaRepoId"];
                        itemGrid["SistemaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["SistemaRepo"];
                        
                        DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].Rows.Add(itemGrid);
                    }
                }
                GrdEspeciesRepoblacion.Rebind();
            }
        }

        void GrdEspeciesRepoblacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].Rows.Count > 0)
            {
                DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].DefaultView.Sort = "[TurnoRepo] ASC, [RodalRepo] ASC, [EtapaIdRepo] ASC";
                ClUtilitarios.LlenaGridDataSet(DsEspeciesRepoblacion, GrdEspeciesRepoblacion, "EspeciesRepoblacion");
                
            }
                
        }


        void BtnAddEspecieRepoPlanifica_ServerClick(object sender, EventArgs e)
        {
            DivErrEspeciesRepo.Visible = false;
            DivGoodEspeciesRepo.Visible = false;
            
            if (ValidaAgregarEspecieRepoPlanificacion() == true)
            {
                if (ExisteEspecieRepoblacion(Convert.ToInt32(TxtTurnoRepo.Text), Convert.ToInt32(TxtRodalRepo.Text), Convert.ToInt32(CboEtapa.SelectedValue), Convert.ToInt32(CboEspecieRepo.SelectedValue)) == true)
                {
                    DivErrEspeciesRepo.Visible = true;
                    LblErrEspeciesRepo.Text = "Esta especie ya existe en este turno, rodal y etapa";
                }
                else
                {
                    LeeGridEspecieRepoblacion();
                    DataRow item = DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].NewRow();
                    item["TurnoRepo"] = TxtTurnoRepo.Text;
                    item["RodalRepo"] = TxtRodalRepo.Text;
                    item["EtapaIdRepo"] = CboEtapa.SelectedValue;
                    item["EtapaRepo"] = CboEtapa.Text;
                    item["AreaRepo"] = TxtAreaRepo.Text;
                    item["Tratamiento"] = TxtTrataminetoRepo.Text;
                    item["AnisRepo"] = TxtAnisRepo.Text;
                    item["EspecieRepoId"] = CboEspecieRepo.SelectedValue;
                    item["EspecieRepo"] = CboEspecieRepo.Text;
                    item["Descripcion"] = TxtDescripcion.Text;
                    item["DensidadRepo"] = TxtDensidadRepo.Text;
                    item["TiempoEje"] = TxtTiempoEjecucion.Text;
                    item["OtrasActividades"] = TxtOtrasActividades.Text;
                    item["SistemaRepoId"] = CboSistemaRepo.SelectedValue;
                    item["SistemaRepo"] = CboSistemaRepo.Text;
                    DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].Rows.Add(item);
                    GrdEspeciesRepoblacion.Rebind();
                    LimpiarRepoblacionEspecie();
                }
            }
        }

        bool ValidaAgregarEspecieRepoPlanificacion()
        {
            LblErrEspecieRepoPlanifica.Text = "";
            DivErrEspecieRepoPlanifica.Visible = false;
            bool HayError = false;

            if (TxtTurnoRepo.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                     LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar el turno";
                 else
                     LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar el turno";
                 HayError = true;
            }
            if (TxtRodalRepo.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                     LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar el rodal";
                 else
                     LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar el rodal";
                 HayError = true;
            }
            
            if (CboEtapa.SelectedValue == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe seleccionar la etapa";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe seleccionar la etapa";
                HayError = true;
            }
            if (TxtAreaRepo.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar el área";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar el área";
                HayError = true;
            }
            if (TxtAnisRepo.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar el año";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar el año";
                HayError = true;
            }
            if (CboEspecieRepo.SelectedValue == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe seleccionar la especie";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe seleccionar la especie";
                HayError = true;
            }
            if (TxtDescripcion.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar la descripción";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar la descripción";
                HayError = true;
            }
            if (TxtDensidadRepo.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar la densidad";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar la densidad";
                HayError = true;
            }
            if ((TxtDensidadRepo.Text != "") && (Convert.ToInt32(TxtDensidadRepo.Text) < 1110))
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "La densidad debe ser mayor a 1110";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", la densidad debe ser mayor a 1110";
                HayError = true;
            }
            if (TxtTiempoEjecucion.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar el tiempo de ejecución";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar el tiempo de ejecución";
                HayError = true;
            }
            if (TxtOtrasActividades.Text == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe ingresar las otras actividades de silvicultura";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe ingresar las otras actividades de silvicultura";
                HayError = true;
            }
            if (CboSistemaRepo.SelectedValue == "")
            {
                if (LblErrEspecieRepoPlanifica.Text == "")
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + "Debe seleccionar el sistema";
                else
                    LblErrEspecieRepoPlanifica.Text = LblErrEspecieRepoPlanifica.Text + ", debe seleccionar el sistema";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrEspecieRepoPlanifica.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ExisteEspecieRepoblacion(int Turno, int Rodal, int EtapaId, int EspecieId)
        {
            bool Existe = false;
            for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
            {
                if ((Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("TurnoRepo")) == Turno) && (Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("RodalRepo")) == Rodal) && (Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("EtapaIdRepo")) == EtapaId) && (Convert.ToInt32(GrdEspeciesRepoblacion.Items[i].GetDataKeyValue("EspecieRepoId")) == EspecieId))
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        void BtnGrabarOtrosAprovecha_Click(object sender, EventArgs e)
        {
            if (ValidaPlanificacionManejo() == true)
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                ClManejo.Insert_OtrosDatos_Aprovechamiento(AsignacionId, Convert.ToInt32(CboFormula.SelectedValue), Convert.ToDecimal(TxtCap.Text), TxtJustificacionFormula.Text, TxtActividadesApro.Text, TxtObjetivosRecuperacion.Text, TxtJustificacionEspecies.Text, TxtSistemaRepo.Text);
                DivGoodPlanificacion.Visible = true;
                LblGoodPlanificacion.Text = "Datos Grabados";
            }
        }

        bool ValidaPlanificacionManejo()
        {
            LblErrPlanificacion.Text = "";
            DivErrPlanificacion.Visible = false;
            bool HayError = false;

            if (CboFormula.SelectedValue == "")
            {
                if (LblErrEspecieRepo.Text == "")
                    LblErrPlanificacion.Text = LblErrPlanificacion.Text + "Debe seleccionar la formula";
                else
                    LblErrPlanificacion.Text = LblErrPlanificacion.Text + ", debe seleccionar la formula";
                HayError = true;
            }

            if (HayError == true)
            {
                DivErrPlanificacion.Visible = true;
                return false;
            }

            else
                return true;
        }

        void CboCriterioReg_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboCriterioReg.SelectedValue != "")
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Formula_Manejo(Convert.ToInt32(CboCriterioReg.SelectedValue)), CboFormula, "Formulaid", "Formula");
                ClUtilitarios.AgregarSeleccioneCombo(CboFormula, "Formula");    
            }
        }

        void BtnGrabarProdNoMaderablesExtraeSave_Click(object sender, EventArgs e)
        {
            bool Agrego = false;
            if (ValidaSilviculturaNoMaderable() == true)
            {
                if (GrdProdNoMaderablesExtrae.Items.Count > 0)
                {

                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Productos");
                    XmlNode iElementos = iInformacion.CreateElement("Productos");
                    for (int i = 0; i < GrdProdNoForestal.Items.Count; i++)
                    {
                        RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[i].FindControl("TxtTurno"));
                        if (TxtTurno.Text != "")
                        {
                            Agrego = true;
                            XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                            ClXml.AgregarAtributo("Turno", ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[i].FindControl("TxtTurno")).Text, iElementoDetalle);
                            iElementos.AppendChild(iElementoDetalle);
                            ClXml.AgregarAtributo("Rodal", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"], iElementoDetalle);
                            iElementos.AppendChild(iElementoDetalle);
                            ClXml.AgregarAtributo("Codigo_Producto", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"], iElementoDetalle);
                            iElementos.AppendChild(iElementoDetalle);
                            ClXml.AgregarAtributo("Peso", ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[i].FindControl("TxtPeso")).Text, iElementoDetalle);
                            iElementos.AppendChild(iElementoDetalle);
                        }

                    }
                    if (Agrego == true)
                    {
                        iInformacion.ChildNodes[1].AppendChild(iElementos);
                        ClManejo.Sp_Insert_Prod_NoMaderable_Extrae_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), iInformacion);
                        DivGoodMaderableExtrae.Visible = true;
                        LblGoodNoMaderableExtrae.Text = "Productos No Maderables grabados";
                    }
                }
            }
        }

        void GrdProdNoMaderablesExtrae_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item.GetDataKeyValue("Peso") != null)
                {
                    ((RadNumericTextBox)item["PesoEdit"].FindControl("TxtPeso")).Text = item.GetDataKeyValue("Peso").ToString();
                }
            }
        }

        void GrdProdNoMaderablesExtrae_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsProductoNoForestales, GrdProdNoMaderablesExtrae, "ProductoNoForestal");
            
        }

        void GrdArboles_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                CheckBox ChkSeleccionaArbol = ((CheckBox)item.FindControl("ChkSeleccionaArbol"));
                if (Convert.ToInt32(item.GetDataKeyValue("Extrae")) == 1)
                    ChkSeleccionaArbol.Checked = true;
                else
                    ChkSeleccionaArbol.Checked = false;
            }
        }

        void TotalVolumenArboles(ref double Troza, ref double Lena)
        {
            double TotalTroza = 0;
            double TotalLena = 0;
            for (int i = 0; i < GrdArboles.Items.Count; i++)
            {
                CheckBox ChkSeleccionaArbol = ((CheckBox)this.GrdArboles.Items[i].FindControl("ChkSeleccionaArbol"));
                if (ChkSeleccionaArbol.Checked == true)
                {
                    TotalTroza = TotalTroza + Convert.ToDouble(GrdArboles.Items[i].GetDataKeyValue("VolTroza"));
                    TotalLena = TotalLena + Convert.ToDouble(GrdArboles.Items[i].GetDataKeyValue("VolLena"));
                }

            }
            Troza = TotalTroza;
            Lena = TotalLena;
        }

        void SetValorGridSilvicultura(double Troza, double Lena)
        {
            for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
            {
                if ((TxtRodalArbol.Text == GrdSilvicultural.Items[i].GetDataKeyValue("Rodal").ToString()) && (TxtEspecieIdArbol.Text == GrdSilvicultural.Items[i].GetDataKeyValue("EspecieId").ToString()))
                {
                    ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTroza")).Text = Troza.ToString();
                    ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxTVolLena")).Text = Lena.ToString();
                    ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal")).Text = (Troza + Lena).ToString();
                    break;
                }
            }
        }

        void BtnGrabarArboles_ServerClick(object sender, EventArgs e)
        {
            double TotalTroza = 0;
            double TotalLena = 0;
            DivErrArboles.Visible = false;
            TotalVolumenArboles(ref TotalTroza, ref TotalLena);
            if (TotalTroza == 0)
            {
                DivErrArboles.Visible = true;
                LblErrArboles.Text = "Debe seleccionar al menos un arbol";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else
            {
                for (int i = 0; i < GrdArboles.Items.Count; i++)
                {
                    CheckBox ChkSeleccionaArbol = ((CheckBox)this.GrdArboles.Items[i].FindControl("ChkSeleccionaArbol"));
                    int Rodal = Convert.ToInt32(GrdArboles.Items[i].GetDataKeyValue("Rodal"));
                    int EspecieId = Convert.ToInt32(GrdArboles.Items[i].GetDataKeyValue("EspecieId"));
                    int ArbolNo = Convert.ToInt32(GrdArboles.Items[i].GetDataKeyValue("No"));
                    if (ChkSeleccionaArbol.Checked== true)
                        ClManejo.Update_Extrae_Arbol(Convert.ToInt32(TxtAsignacionId.Text), Rodal, EspecieId, ArbolNo, 1);
                    else
                        ClManejo.Update_Extrae_Arbol(Convert.ToInt32(TxtAsignacionId.Text), Rodal, EspecieId, ArbolNo, 0);
                }
                SetValorGridSilvicultura(TotalTroza, TotalLena);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').close();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
                
        }

        void GrdArboles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Sp_Get_Arboles_Extraer(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtRodalArbol.Text), Convert.ToInt32(TxtEspecieIdArbol.Text)), GrdArboles);
        }


        void BloquearTodoslosVolumenes()
        {
            if (CboTipoIngresoDatos.SelectedValue != "")
            {
                if (CboTipoIngresoDatos.SelectedValue == "2")
                {
                    for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
                    {
                        RadNumericTextBox TxtVolTroza = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTroza"));
                        RadNumericTextBox TxtVolLena = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolLena"));
                        RadNumericTextBox TxtVolTotal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal"));
                        TxtVolTroza.Enabled = false;
                        TxtVolLena.Enabled = false;
                        TxtVolTotal.Enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
                    {
                        RadNumericTextBox TxtVolTroza = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTroza"));
                        RadNumericTextBox TxtVolLena = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolLena"));
                        RadNumericTextBox TxtVolTotal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal"));
                        TxtVolTroza.Enabled = true;
                        TxtVolLena.Enabled = true;
                        TxtVolTotal.Enabled = true;
                    }
                }
            }
            
        }

        protected void CboTratamiento_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboTipoIngresoDatos.SelectedValue == "2")
            {
                RadComboBox combo = (RadComboBox)o;
                GridDataItem item = (GridDataItem)combo.NamingContainer;
                ImageButton ImgArboles = (ImageButton)item.FindControl("ImgArboles");
                if (combo.SelectedValue == "1")
                {
                
                    ImgArboles.Visible = false;

                }
                else
                {
                
                    ImgArboles.Visible = true;
                }
            }
            CalculaCompromisoRepoblacion();
            
        }

        


        void GrdSilvicultural_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoClaseDesarrollo(), (RadComboBox)item["Clase_Desarrollo_Edit"].FindControl("CboClaseDesarrollo"), "Clase_DesarrolloId", "Clase_Desarrollo");
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_TratamientoSilvicultural(), (RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento"), "Tratamiento_Id", "Tratamiento");
                ClUtilitarios.AgregarSeleccioneCombo((RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento"), "Tratamiento");
                if (item.GetDataKeyValue("Turno") != null)
                {
                    ((RadNumericTextBox)item["TurnoEdit"].FindControl("TxtTurno")).Text = item.GetDataKeyValue("Turno").ToString();
                }
                if (item.GetDataKeyValue("AreaRodal") != null)
                {
                    ((RadNumericTextBox)item["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text = item.GetDataKeyValue("AreaRodal").ToString();
                }
                if (item.GetDataKeyValue("Clase_Desarrollo") != null)
                {
                    ((RadComboBox)item["Clase_Desarrollo_Edit"].FindControl("CboClaseDesarrollo")).Text = item.GetDataKeyValue("Clase_Desarrollo").ToString();
                }
                if (item.GetDataKeyValue("Edad") != null)
                {
                    ((TextBox)item["EdadEdit"].FindControl("TxtEdad")).Text = item.GetDataKeyValue("Edad").ToString();
                }
                if (item.GetDataKeyValue("Tratamiento") != null)
                {
                    ((RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento")).Text = item.GetDataKeyValue("Tratamiento").ToString();
                }
                if (item.GetDataKeyValue("Dap") != null)
                {
                    ((RadNumericTextBox)item["DapEdit"].FindControl("TxtDap")).Text = item.GetDataKeyValue("Dap").ToString();
                }
                if (item.GetDataKeyValue("Altura") != null)
                {
                    ((RadNumericTextBox)item["AlturaEdit"].FindControl("TxtAltura")).Text = item.GetDataKeyValue("Altura").ToString();
                }
                if (item.GetDataKeyValue("Densidad") != null)
                {
                    ((RadNumericTextBox)item["DensidadEdit"].FindControl("TxtDensidad")).Text = item.GetDataKeyValue("Densidad").ToString();
                }
                if (item.GetDataKeyValue("AreaBasal") != null)
                {
                    ((RadNumericTextBox)item["AreaBasalEdit"].FindControl("TxtAreaBasal")).Text = item.GetDataKeyValue("AreaBasal").ToString();
                }
                if (item.GetDataKeyValue("VolTroza") != null)
                {
                    ((RadNumericTextBox)item["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = item.GetDataKeyValue("VolTroza").ToString();
                }
                if (item.GetDataKeyValue("VolLena") != null)
                {
                    ((RadNumericTextBox)item["VolLenaEdit"].FindControl("TxTVolLena")).Text = item.GetDataKeyValue("VolLena").ToString();
                }
                if (item.GetDataKeyValue("VolTotal") != null)
                {
                    ((RadNumericTextBox)item["VolTotalEdit"].FindControl("TxtVolTotal")).Text = item.GetDataKeyValue("VolTotal").ToString();
                }
                if (item.GetDataKeyValue("Pendiente") != null)
                {
                    ((RadNumericTextBox)item["PendienteEdit"].FindControl("TxtPendiente")).Text = item.GetDataKeyValue("Pendiente").ToString();
                }
                if (item.GetDataKeyValue("INC") != null)
                {
                    ((RadNumericTextBox)item["INCEdit"].FindControl("TxtINC")).Text = item.GetDataKeyValue("INC").ToString();
                }
                if (item.GetDataKeyValue("VolHa") != null)
                {
                    ((RadNumericTextBox)item["VolHaEdit"].FindControl("TxtVolHa")).Text = item.GetDataKeyValue("VolHa").ToString();
                }
                if (item.GetDataKeyValue("VolRodal") != null)
                {
                    ((RadNumericTextBox)item["VolRodalEdit"].FindControl("TxtVolRodal")).Text = item.GetDataKeyValue("VolRodal").ToString();
                }
                if (item.GetDataKeyValue("AreaBasalRodal") != null)
                {
                    ((RadNumericTextBox)item["AreaBasalRodalEdit"].FindControl("TxtAreaBasalRodal")).Text = item.GetDataKeyValue("AreaBasalRodal").ToString();
                }
                if (item.GetDataKeyValue("Correlativo").ToString() != "")
                {
                    DataSet DsDatosExtrae = ClManejo.Get_Dato_Silvicultura_Extrae(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(item.GetDataKeyValue("Correlativo")));
                    if (DsDatosExtrae.Tables["Datos"].Rows.Count > 0)
                    {
                        ((RadNumericTextBox)item["TurnoEdit"].FindControl("TxtTurno")).Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Turno"].ToString();
                        ((RadNumericTextBox)item["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTroza"].ToString();
                        ((RadNumericTextBox)item["VolLenaEdit"].FindControl("TxTVolLena")).Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolLena"].ToString();
                        ((RadNumericTextBox)item["VolTotalEdit"].FindControl("TxtVolTotal")).Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTotal"].ToString();
                        ((RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento")).SelectedValue = DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento_Id"].ToString();
                        ((RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento")).Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento"].ToString();
                        ((CheckBox)item["ExtraeEdit"].FindControl("ChkExtrae")).Checked = true;
                        if (Convert.ToInt32(DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento_Id"].ToString()) != 1)
                            ((ImageButton)item["Arboles"].FindControl("ImgArboles")).Visible = true;

                    }
                }
                
            }
        }

        bool ValidaSilviculturaNoMaderable()
        {
            bool Valido = true;
            for (int i = 0; i < GrdProdNoMaderablesExtrae.Items.Count; i++)
            {
                RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[i].FindControl("TxtTurno"));
                if (TxtTurno.Text != "")
                {
                    RadNumericTextBox TxtPeso = ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[i].FindControl("TxtPeso"));
                    if (Convert.ToDouble(TxtPeso.Text) > Convert.ToDouble(GrdProdNoMaderablesExtrae.Items[i].GetDataKeyValue("Peso")))
                    {
                        TxtPeso.Style.Add("color ", "Red");
                        Valido = false;
                    }
                    else
                        TxtPeso.Style.Add("color ", "Black");
                    
                }
            }
            if (Valido == false)
            {
                DivErrNoMaderableExtrae.Visible = true;
                LblErrNoMaderableExtrae.Text = "Hay Valores que sobrepasan el peso del inventario de productos no maderables por favor corregir (marcados en rojo)";
            }
            return Valido;
        }

        bool ValidaSilviculturaMaderable()
        {
            bool Valido = true;
            string Error = "";
            double TotalExtrae = 0;
            for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
            {
                RadNumericTextBox TxtTurno = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtTurno"));
                if (TxtTurno.Text != "")
                {
                    CheckBox ChkExtrae = ((CheckBox)this.GrdSilvicultural.Items[i].FindControl("ChkExtrae"));
                    if (ChkExtrae.Checked == true)
                    {
                        RadNumericTextBox TxtVolTroza = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTroza"));
                        RadNumericTextBox TxTVolLena = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxTVolLena"));
                        RadNumericTextBox TxtVolTotal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal"));
                        double VolTotalRodal = Convert.ToDouble(GrdSilvicultural.Items[i].GetDataKeyValue("VolRodal"));
                        ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal")).Text = (Convert.ToDouble(TxtVolTroza.Text) + Convert.ToDouble(TxTVolLena.Text)).ToString();
                        TotalExtrae = TotalExtrae + (Convert.ToDouble(TxtVolTroza.Text) + Convert.ToDouble(TxTVolLena.Text));
                        //Validaciones anteriores
                        //if (Convert.ToDouble(TxtVolTroza.Text) > Convert.ToDouble(GrdSilvicultural.Items[i].GetDataKeyValue("VolTroza")))
                        //{
                        //    TxtVolTroza.Style.Add("color ", "Red");
                        //    Valido = false;
                        //}
                        //else
                        //    TxtVolTroza.Style.Add("color ", "Black");
                        //if (Convert.ToDouble(TxTVolLena.Text) > Convert.ToDouble(GrdSilvicultural.Items[i].GetDataKeyValue("VolLena")))
                        //{
                        //    TxTVolLena.Style.Add("color ", "Red");
                        //    Valido = false;
                        //}
                        //else
                        //    TxTVolLena.Style.Add("color ", "Black");
                        //if (Convert.ToDouble(TxtVolTotal.Text) > Convert.ToDouble(GrdSilvicultural.Items[i].GetDataKeyValue("VolTotal")))
                        //{
                        //    TxtVolTotal.Style.Add("color ", "Red");
                        //    Valido = false;
                        //}
                        //else
                        //    TxtVolTotal.Style.Add("color ", "Black");
                        string TratamientoId = ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento")).SelectedValue;
                        if (TratamientoId == "")
                        {
                            ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento")).BackColor = System.Drawing.Color.Red;
                            Valido = false;
                            if (Error == "")
                                Error = "A la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre_Cientifico") + " no selecciono el tratamiento silvicultural";
                            else
                                Error = Error + ", a la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre_Cientifico") + " no selecciono el tratamiento silvicultural";
                        }
                        else
                        {
                            ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento")).BackColor = System.Drawing.Color.White;
                            if (TratamientoId == "1")
                            {
                                if (Math.Round(Convert.ToDouble(TxtVolTroza.Text) + Convert.ToDouble(TxTVolLena.Text),2) != Convert.ToDouble(VolTotalRodal))
                                {
                                    TxtVolTroza.Style.Add("color ", "Red");
                                    TxTVolLena.Style.Add("color ", "Red");
                                    if (Error == "")
                                        Error = "La sumatoria de volumenes de la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre_Cientifico") + " supera el volumen total";
                                    else
                                        Error = Error + ", La sumatoria de volumenes de la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre_Cientifico") + " supera el volumen total";
                                    Valido = false;
                                }
                                else
                                {
                                    TxtVolTroza.Style.Add("color ", "Black");
                                    TxTVolLena.Style.Add("color ", "Black");
                                }
                            }
                            else
                            {
                                if ((Convert.ToDouble(TxtVolTroza.Text) + Convert.ToDouble(TxTVolLena.Text)) >= VolTotalRodal)
                                {
                                    TxtVolTroza.Style.Add("color ", "Red");
                                    TxTVolLena.Style.Add("color ", "Red");
                                    if (Error == "")
                                        Error = "La sumatoria de volumenes de la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre Cientifico") + " no puede ser mayor o igual que el volumen total";
                                    else
                                        Error = Error + ", La sumatoria de volumenes de la especie " + GrdSilvicultural.Items[i].GetDataKeyValue("Nombre Cientifico") + " no puede ser mayor o igual que el volumen total";
                                    Valido = false;
                                }
                                else
                                {
                                    TxtVolTroza.Style.Add("color ", "Black");
                                    TxTVolLena.Style.Add("color ", "Black");
                                }
                            }
                        }
                        

                    }
                    
                }
            }
            if (Valido == false)
            {
                DivErrSilvicultura.Visible = true;
                LblErrSilvicultura.Text = Error;
            }
            else
            {
                TxtVolExtraer.Text = TotalExtrae.ToString();
            }
            return Valido;
        }

        void OverrideProductosNoMaderablesExtrae()
        {
            DataSet dsProdNoMaderablesExtrae = ClManejo.LeerXml_Prod_NoMaderables_Extrae(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsProdNoMaderablesExtrae.Tables["Datos"].Rows.Count; i++)
            {
                for (int j = 0; j < GrdProdNoMaderablesExtrae.Items.Count; j++)
                {
                    if ((Convert.ToInt32(dsProdNoMaderablesExtrae.Tables["Datos"].Rows[i]["Rodal"]) == Convert.ToInt32(GrdProdNoMaderablesExtrae.Items[j].GetDataKeyValue("Rodal"))) && (Convert.ToInt32(dsProdNoMaderablesExtrae.Tables["Datos"].Rows[i]["Codigo_Producto"]) == Convert.ToInt32(GrdProdNoMaderablesExtrae.Items[j].GetDataKeyValue("Codigo_Producto"))))
                    {
                        ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[j].FindControl("TxtTurno")).Text = dsProdNoMaderablesExtrae.Tables["Datos"].Rows[i]["Turno"].ToString();
                        ((RadNumericTextBox)this.GrdProdNoMaderablesExtrae.Items[j].FindControl("TxtPeso")).Text = dsProdNoMaderablesExtrae.Tables["Datos"].Rows[i]["Peso"].ToString();
                        break;
                    }
                }
            }
        }

        void BtnGrabarSilvicultura_Click(object sender, EventArgs e)
        {
            bool Agrego = false;
            DivErrSilvicultura.Visible = false;
                if (ValidaSilviculturaMaderable() == true)
                {
                    
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    ClManejo.Delete_Silvicultura_MaderableExtrae(AsignacionId);
                    for (int i = 0; i < GrdSilvicultural.Items.Count; i++)
                    {
                        object TxtTurno = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtTurno")).Text;
                        if (TxtTurno.ToString() != "")
                        {
                            CheckBox ChkExtrae = ((CheckBox)this.GrdSilvicultural.Items[i].FindControl("ChkExtrae"));
                            if (ChkExtrae.Checked == true)
                            {
                                Agrego = true;
                                string TratamientoId = ((RadComboBox)this.GrdSilvicultural.Items[i].FindControl("CboTratamiento")).SelectedValue;
                                object VolTroza = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTroza")).Text;
                                if (VolTroza == "")
                                    VolTroza = 0;
                                object VolLena = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolLena")).Text;
                                if (VolLena == "")
                                    VolLena = 0;
                                object VolTotal = ((RadNumericTextBox)this.GrdSilvicultural.Items[i].FindControl("TxtVolTotal")).Text;
                                if (VolTotal == "")
                                    VolTotal = 0;
                                
                                
                                
                                ClManejo.Insert_Silvicultura_MaderableExtrae(AsignacionId, Convert.ToInt32(GrdSilvicultural.Items[i].OwnerTableView.DataKeyValues[i]["Correlativo"]), Convert.ToInt32(TxtTurno), Convert.ToDecimal(VolTroza), Convert.ToDecimal(VolLena), Convert.ToDecimal(VolTotal), Convert.ToInt32(TratamientoId));
                            }
                        }
                        
                    }
                }
                if (Agrego == true)
                {
                    DivGodSilvicultura.Visible = true;
                    LblGoodSilvicultura.Text = "Productos maderables ingresados correctamente";
                }
            }
            

        void GrdSilvicultural_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdArboles")
            {

                TxtRodalArbol.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Rodal"].ToString();
                TxtEspecieIdArbol.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString();
                GrdArboles.Rebind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdSilvicultural_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in this.GrdSilvicultural.Items)
            {
                if (item.OwnerTableView.Name == "LabelsResumen")
                {
                    GridTableView ownerTableView = item.OwnerTableView;
                    for (int i = ownerTableView.Items.Count - 2; i >= 0; i--)
                    {
                        GridDataItem item2 = ownerTableView.Items[i];
                        GridDataItem item3 = ownerTableView.Items[i + 1];
                        //if (item2["Turno"].Text == item3["Turno"].Text)
                        //{
                        //    item2["Turno"].RowSpan = (item3["Turno"].RowSpan < 2) ? 2 : (item3["Turno"].RowSpan + 1);
                        //    item3["Turno"].Visible = false;
                        //}
                        if (item2["Rodal"].Text == item3["Rodal"].Text)
                        {
                            //item2["Turno"].RowSpan = (item3["Turno"].RowSpan < 2) ? 2 : (item3["Turno"].RowSpan + 1);
                            //item3["Turno"].Visible = false;
                            //item2["TurnoEdit"].RowSpan = (item3["TurnoEdit"].RowSpan < 2) ? 2 : (item3["TurnoEdit"].RowSpan + 1);
                            //item3["TurnoEdit"].Visible = false;
                            item2["Rodal"].RowSpan = (item3["Rodal"].RowSpan < 2) ? 2 : (item3["Rodal"].RowSpan + 1);
                            item3["Rodal"].Visible = false;

                            item2["AreaRodal"].RowSpan = (item3["AreaRodal"].RowSpan < 2) ? 2 : (item3["AreaRodal"].RowSpan + 1);
                            item3["AreaRodal"].Visible = false;
                            item2["AreaRodalEdit"].RowSpan = (item3["AreaRodalEdit"].RowSpan < 2) ? 2 : (item3["AreaRodalEdit"].RowSpan + 1);
                            item3["AreaRodalEdit"].Visible = false;
                            item2["Clase_Desarrollo"].RowSpan = (item3["Clase_Desarrollo"].RowSpan < 2) ? 2 : (item3["Clase_Desarrollo"].RowSpan + 1);
                            item3["Clase_Desarrollo"].Visible = false;
                            item2["Clase_Desarrollo_Edit"].RowSpan = (item3["Clase_Desarrollo_Edit"].RowSpan < 2) ? 2 : (item3["Clase_Desarrollo_Edit"].RowSpan + 1);
                            item3["Clase_Desarrollo_Edit"].Visible = false;
                            item2["Edad"].RowSpan = (item3["Edad"].RowSpan < 2) ? 2 : (item3["Edad"].RowSpan + 1);
                            item3["Edad"].Visible = false;
                            item2["EdadEdit"].RowSpan = (item3["EdadEdit"].RowSpan < 2) ? 2 : (item3["EdadEdit"].RowSpan + 1);
                            item3["EdadEdit"].Visible = false;

                          

                            item2["Pendiente"].RowSpan = (item3["Pendiente"].RowSpan < 2) ? 2 : (item3["Pendiente"].RowSpan + 1);
                            item3["Pendiente"].Visible = false;
                            item2["PendienteEdit"].RowSpan = (item3["PendienteEdit"].RowSpan < 2) ? 2 : (item3["PendienteEdit"].RowSpan + 1);
                            item3["PendienteEdit"].Visible = false;
                            item2["INC"].RowSpan = (item3["INC"].RowSpan < 2) ? 2 : (item3["INC"].RowSpan + 1);
                            item3["INC"].Visible = false;
                            item2["INCEdit"].RowSpan = (item3["INCEdit"].RowSpan < 2) ? 2 : (item3["INCEdit"].RowSpan + 1);
                            item3["INCEdit"].Visible = false;

                        }
                    }
                }
            }
        }

        void GrdSilvicultural_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_Resumen_Censo(1, Convert.ToInt32(TxtAsignacionId.Text)), GrdSilvicultural);
        }

        void GrdProdNoForestal_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                string Producto = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Codigo_Producto"].ToString().Trim();
                for (int i = 0; i < GrdProdNoForestal.Items.Count; i++)
                {
                    if (Producto != e.Item.OwnerTableView.DataKeyValues[i]["Codigo_Producto"].ToString().Trim())
                    {
                        DataRow itemGrid = DsProductoNoForestales.Tables["ProductoNoForestal"].NewRow();
                        itemGrid["Turno"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Turno"];
                        itemGrid["Rodal"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"];
                        itemGrid["Anis"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Anis"];
                        itemGrid["Area"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Area"];
                        itemGrid["Codigo_Producto"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                        itemGrid["Producto"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Producto"];
                        itemGrid["Unidad_MedidaId"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Unidad_MedidaId"];
                        itemGrid["Unidad_Medida"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Unidad_Medida"];
                        itemGrid["Peso"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Peso"];
                        DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Add(itemGrid);
                    }
                }
                GrdProdNoForestal.Rebind();
            }
        }

        void BtnGrabarProdNoMaderables_ServerClick(object sender, EventArgs e)
        {
            DivGoodProdMaderable.Visible = false;
            if (GrdProdNoForestal.Items.Count > 0)
            {
                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Productos");
                XmlNode iElementos = iInformacion.CreateElement("Productos");
                for (int i = 0; i < GrdProdNoForestal.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                    ClXml.AgregarAtributo("Turno", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Turno"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Rodal", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Anis", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Anis"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Area", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Area"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Codigo_Producto", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Unidad_MedidaId", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Unidad_MedidaId"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                    ClXml.AgregarAtributo("Peso", GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Peso"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
                ClManejo.Sp_Insert_Prod_NoMaderable_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), iInformacion);
            }
            DivGoodProdMaderable.Visible = true;
            LblGoodProdMaderable.Text = "Productos ingresados correctamente";
            
        }

        void GrdProdNoForestal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            if (DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsProductoNoForestales, GrdProdNoForestal, "ProductoNoForestal");
        }

        void BtnAddProductoNoForestal_ServerClick(object sender, EventArgs e)
        {
            LblErrProdNoMaderable.Text = "";
            DivErrProdNoMaderable.Visible = false;
            bool HayError = false;
            if (CboRodal.SelectedValue == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe seleccionar el Rodal";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar el Rodal";
                HayError = true;
            }
            if (TxtYearNoForestal.Text == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe ingresar el año";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar el año";
                HayError = true;
            }
            if (TxtAreaNoForesal.Text == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe ingresar el área";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar el área";
                HayError = true;
            }
            if (CboProducto.SelectedValue == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe seleccionar el producto";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar el producto";
                HayError = true;
            }
            if (CboUMedida.SelectedValue == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe seleccionar la unidad de medida";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar la unidad medida";
                HayError = true;
            }
            if (TxtPesoNoForestal.Text == "")
            {
                if (LblErrProdNoMaderable.Text == "")
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + "Debe ingresar el peso";
                else
                    LblErrProdNoMaderable.Text = LblErrProdNoMaderable.Text + ", debe seleccionar el peso";
                HayError = true;
            }
            if (HayError != true)
            {
                LeeGridProdNoForestal();
                DataRow item = DsProductoNoForestales.Tables["ProductoNoForestal"].NewRow();
                item["Turno"] = 1;
                item["Rodal"] = CboRodal.Text;
                item["Anis"] = TxtYearNoForestal.Text;
                item["Area"] = TxtAreaNoForesal.Text;
                item["Codigo_Producto"] = CboProducto.SelectedValue;
                item["Producto"] = CboProducto.Text;
                item["Unidad_MedidaId"] = CboUMedida.SelectedValue;
                item["Unidad_Medida"] = CboUMedida.Text;
                item["Peso"] = TxtPesoNoForestal.Text;
                DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Add(item);
                GrdProdNoForestal.Rebind();
                GrdProdNoMaderablesExtrae.Rebind();
            }
            else
                DivErrProdNoMaderable.Visible = true;
        }

        void LeeGridProdNoForestal()
        {

            GrdProdNoForestal.AllowPaging = false;
            for (int i = 0; i < GrdProdNoForestal.Items.Count; i++)
            {
                DataRow itemGrid = DsProductoNoForestales.Tables["ProductoNoForestal"].NewRow();
                itemGrid["Turno"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Turno"];
                itemGrid["Rodal"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"];
                itemGrid["Anis"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Anis"];
                itemGrid["Area"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Area"];
                itemGrid["Codigo_Producto"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Codigo_Producto"];
                itemGrid["Producto"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Producto"];
                itemGrid["Unidad_MedidaId"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Unidad_MedidaId"];
                itemGrid["Unidad_Medida"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Unidad_Medida"];
                itemGrid["Peso"] = GrdProdNoForestal.Items[i].OwnerTableView.DataKeyValues[i]["Peso"];
                DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Add(itemGrid);
            }
        }

        void LeeGridEspecieRepoblacion()
        {

            GrdEspeciesRepoblacion.AllowPaging = false;
            for (int i = 0; i < GrdEspeciesRepoblacion.Items.Count; i++)
            {
                DataRow itemGrid = DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].NewRow();
                itemGrid["TurnoRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TurnoRepo"];
                itemGrid["RodalRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["RodalRepo"];
                itemGrid["EtapaIdRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EtapaIdRepo"];
                itemGrid["EtapaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EtapaRepo"];
                itemGrid["AreaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AreaRepo"];
                itemGrid["Tratamiento"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Tratamiento"];
                itemGrid["AnisRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["AnisRepo"];
                itemGrid["EspecieRepoId"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EspecieRepoId"];
                itemGrid["EspecieRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["EspecieRepo"];
                itemGrid["Descripcion"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["Descripcion"];
                itemGrid["DensidadRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["DensidadRepo"];
                itemGrid["TiempoEje"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["TiempoEje"];
                itemGrid["OtrasActividades"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["OtrasActividades"];
                itemGrid["SistemaRepoId"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["SistemaRepoId"];
                itemGrid["SistemaRepo"] = GrdEspeciesRepoblacion.Items[i].OwnerTableView.DataKeyValues[i]["SistemaRepo"];
                DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].Rows.Add(itemGrid);
            }
        }

        void CboTurno_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(CboTurno.SelectedValue) > 0)
            {
                
            }
               
        }

        void btnGrabarRepo_ServerClick(object sender, EventArgs e)
        {
            if (ValidaDatosRepo() == true)
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);

                ClManejo.Eliminar_Accion_Repoblacion_PlanManejo(AsignacionId);
                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Especies");
                XmlNode iElementos = iInformacion.CreateElement("Especie");
                for (int i = 0; i < GrdEspecieRepo.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                    ClXml.AgregarAtributo("No", i + 1, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("EspecieId", GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
                ClManejo.Insert_Accion_Repoblacion_PlanManejo(AsignacionId, iInformacion, Convert.ToInt32(txtDensidadIni.Text));
                DivGoodRepo.Visible = true;
                LblGoodRepo.Text = "Datos Grabados"; 
            }
        }

        bool ValidaDatosRepo()
        {
            LblErrEspecieRepo.Text = "";
            DivErrEspecieRepo.Visible = false;
            bool HayError = false;
            if (GrdEspecieRepo.Items.Count == 0)
            {
                if (LblErrEspecieRepo.Text == "")
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + "Debe ingresar al menos una especie";
                else
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + ", debe ingresar al menos una especie";
                HayError = true;
            }
            if (txtDensidadIni.Text == "")
            {
                if (LblErrEspecieRepo.Text == "")
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + "Debe ingresar la densidad inicial";
                else
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + ", debe ingresar la densidad inicial";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrorInfoGenPlan.Visible = true;
                return false;
            }

            else
                return true;
        }

        void GrdEspecieRepo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarEspecieRepo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString()));
            }

        }

        void GrdEspecieRepo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (dv.Count > 0)
                ClUtilitarios.LlenaGridDataView(dv, GrdEspecieRepo, "Dt_EspecieRepo");
        }

        void BtnAddEspecieRepo_ServerClick(object sender, EventArgs e)
        {
            if (ValidaEspecieRepo() == true)
            {
                if (ExisteEspecieRepoblacion(Convert.ToInt32(CboEspecieRepoblacion.SelectedValue)))
                {
                    DivErrEspecieRepo.Visible = true;
                    LblErrEspecieRepo.Text = "Especie ya existe";

                }
                else
                {
                    CargarGridEspecieRepo();
                    AgregaEspecieRepo();
                    dv = Ds_Temporal.Tables["Dt_EspecieRepo"].DefaultView;
                    GrdEspecieRepo.Rebind();
                    LimpiarEspecieRepo();
                }
            }
        }

        void BtnGeneraCalculos_ServerClick(object sender, EventArgs e)
        {
            double AreaRodal = 0;
            int Rodal = 0;
            int RodalAct = 0;
            for (int i = 0; i < GrdResumen.Items.Count; i++)
            {
                ((RadNumericTextBox)GrdResumen.Items[i]["DapEdit"].FindControl("TxtDap")).Text = (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["sumadap"]) / Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["arboles"])).ToString();
                ((RadNumericTextBox)GrdResumen.Items[i]["AlturaEdit"].FindControl("TxtAltura")).Text = (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["sumaaltura"]) / Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["arboles"])).ToString();
                Rodal = Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"]);
                if (((((RadNumericTextBox)GrdResumen.Items[i]["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text != "") && (Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text) > 0)) || (AreaRodal > 0))
                {
                    if (Rodal != RodalAct)
                    {
                        if ((((RadNumericTextBox)GrdResumen.Items[i]["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text != "") && (Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text) > 0))
                            AreaRodal = Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["AreaRodalEdit"].FindControl("TxtAreaRodal")).Value);
                        else
                            AreaRodal = 0;
                    }
                       
                    if (AreaRodal > 0)
                    {
                        //double Temp = 0;
                        //double arboles = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["arboles"]);
                        //double SumBa = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["SumBa"]);
                        //double Volumen = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"]);
                        //Temp = AreaRodal * 10000;
                        //((RadNumericTextBox)GrdResumen.Items[i]["DensidadEdit"].FindControl("TxtDensidad")).Text = ((10000 * arboles) / Temp).ToString("N2");
                        //((RadNumericTextBox)GrdResumen.Items[i]["AreaBasalEdit"].FindControl("TxtAreaBasal")).Text = SumBa.ToString("N2");
                        //RodalAct = Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"]);
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"]) * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100)).ToString();
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolLenaEdit"].FindControl("TxTVolLena")).Text = (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"]) - (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"]) * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100))).ToString();
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolTotalEdit"].FindControl("TxtVolTotal")).Text = (Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["VolTrozaEdit"].FindControl("TxtVolTroza")).Text) + Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["VolLenaEdit"].FindControl("TxTVolLena")).Text)).ToString();
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolHaEdit"].FindControl("TxtVolHa")).Text = ((10000 * 0.9083) / Temp).ToString("N2");
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolRodalEdit"].FindControl("TxtVolRodal")).Text = Volumen.ToString("N2");
                        
                        double Temp = 0;
                        double arboles = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["arboles"]);
                        double SumBa = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["SumBa"]);
                        double Volumen = 0;
                        if ((GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"].ToString()) != "")
                            Volumen = Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["volumen"]);

                        Temp = AreaRodal * 10000;
                        ((RadNumericTextBox)GrdResumen.Items[i]["DensidadEdit"].FindControl("TxtDensidad")).Text = ((10000 * arboles) / Temp).ToString("N2");
                        ((RadNumericTextBox)GrdResumen.Items[i]["AreaBasalEdit"].FindControl("TxtAreaBasal")).Text = SumBa.ToString("N2");
                        RodalAct = Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"]);
                        
                        
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = (Volumen * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100)).ToString();
                        //((RadNumericTextBox)GrdResumen.Items[i]["VolLenaEdit"].FindControl("TxTVolLena")).Text = (Volumen * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100)).ToString();
                        ((RadNumericTextBox)GrdResumen.Items[i]["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = (Volumen * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100)).ToString();
                        ((RadNumericTextBox)GrdResumen.Items[i]["VolLenaEdit"].FindControl("TxTVolLena")).Text = (Volumen - (Volumen * (Convert.ToDouble(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Troza"]) / 100))).ToString();
                        
                        
                        ((RadNumericTextBox)GrdResumen.Items[i]["VolTotalEdit"].FindControl("TxtVolTotal")).Text = (Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["VolTrozaEdit"].FindControl("TxtVolTroza")).Text) + Convert.ToDouble(((RadNumericTextBox)GrdResumen.Items[i]["VolLenaEdit"].FindControl("TxTVolLena")).Text)).ToString();
                        ((RadNumericTextBox)GrdResumen.Items[i]["VolHaEdit"].FindControl("TxtVolHa")).Text = ((10000 * 0.9083) / Temp).ToString("N2");
                        ((RadNumericTextBox)GrdResumen.Items[i]["VolRodalEdit"].FindControl("TxtVolRodal")).Text = Volumen.ToString("N2");
                    }
                    
                }
                    

            }
            GrdSilvicultural.Rebind();
        }

        void 
            CboTipoIngresoDatos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet ExisteBoleta = ClManejo.Get_Datos_Boleta(Convert.ToInt32(TxtAsignacionId.Text));
            if (ExisteBoleta.Tables["Datos"].Rows.Count > 0)
            {
                DivOtraEcuacion.Visible = true;
                if (CboTipoIngresoDatos.SelectedValue == "1")
                {
                    BtnGeneraCalculos.Visible = false;
                    DivEcuacion.Visible = true;
                    LblEcuacion.Text = "Otras Ecuaciones";
                }
                else
                {
                    BtnGeneraCalculos.Visible = true;
                    DivEcuacion.Visible = false;
                    LblEcuacion.Text = "Ecuaciones Utilizadas";
                }
            }
            BloquearTodoslosVolumenes();
        }



        void btnGrabarAprovechamiento_Click(object sender, EventArgs e)
        {
            if (ValidaAprovechamiento() == true)
            {

                if (ValidaAprovechamientoFilas() != true)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    ClManejo.Elimniar_Resumen(AsignacionId);
                    ClManejo.Eliminiar_Aprovechamiento_Forestal(AsignacionId);
                    ClManejo.Eliminiar_Ecuaciones_Manejo(AsignacionId);
                    ClManejo.Insert_Aprovechamiento_Forestal(AsignacionId, Convert.ToInt32(CboTipoIngresoDatos.SelectedValue), Convert.ToInt32(CboTipoInventario.SelectedValue), TxtDatosRegresion.Text, Convert.ToInt32(TxtDiametroMinimo.Text), Convert.ToInt32(TxtTotRodal.Text), TxtOtraEcuacion.Text);
                    if (CboTipoIngresoDatos.SelectedValue == "1")
                    {
                        var collection = CboEcuacion.CheckedItems;
                        foreach (var item in collection)
                        {
                            ClManejo.Insert_Ecuacion_PlanManejo(AsignacionId, Convert.ToInt32(item.Value));
                        }
                    }
                    for (int i = 0; i < GrdResumen.Items.Count; i++)
                    {
                        object AreaRodal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtAreaRodal")).Text;
                        if (AreaRodal == "")
                            AreaRodal = 0;
                        string CodClaseDesarrollo = ((RadComboBox)this.GrdResumen.Items[i].FindControl("CboClaseDesarrollo")).SelectedValue;
                        string Edad = ((TextBox)this.GrdResumen.Items[i].FindControl("TxtEdad")).Text;
                        string TratamientoId = ((RadComboBox)this.GrdResumen.Items[i].FindControl("CboTratamiento")).SelectedValue;
                        object Dap = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtDap")).Text;
                        if (Dap == "")
                            Dap = 0;
                        object Altura = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtAltura")).Text;
                        if (Altura == "")
                            Altura = 0;
                        object Densidad = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtDensidad")).Text;
                        if (Densidad == "")
                            Densidad = 0;
                        object AreaBasal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtAreaBasal")).Text;
                        if (AreaBasal == "")
                            AreaBasal = 0;
                        object VolTroza = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolTroza")).Text;
                        if (VolTroza == "")
                            VolTroza = 0;
                        object VolLena = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolLena")).Text;
                        if (VolLena == "")
                            VolLena = 0;
                        object VolOtro = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolOtro")).Text;
                        if (VolOtro == "")
                            VolOtro = 0;
                        object VolTotal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolTotal")).Text;
                        if (VolTotal == "")
                            VolTotal = 0;
                        object Pendiente = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtPendiente")).Text;
                        if (Pendiente == "")
                            Pendiente = 0;
                        object INC = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtINC")).Text;
                        if (INC == "")
                            INC = 0;
                        object VolHa = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolHa")).Text;
                        if (VolHa == "")
                            VolHa = 0;
                        object VolRodal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtVolRodal")).Text;
                        if (VolRodal == "")
                            VolRodal = 0;
                        object AreaBasalRodal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtAreaBasalRodal")).Text;
                        if (AreaBasalRodal == "")
                            AreaBasalRodal = 0;

                        object VolTrozaExtrae = GrdResumen.Items[i].GetDataKeyValue("TxtVolTrozaExtrae");
                        if ((VolTrozaExtrae == "") || (VolTrozaExtrae == "0") || (VolTrozaExtrae == null))
                            VolTrozaExtrae = VolTroza;
                        object VolLenaExtrae = GrdResumen.Items[i].GetDataKeyValue("TxtVolLenaExtrae");
                        if ((VolLenaExtrae == "") || (VolLenaExtrae == "0") || (VolLenaExtrae == null))
                            VolLenaExtrae = VolLena;
                        object VolOtroExtreae = GrdResumen.Items[i].GetDataKeyValue("TxtVolOtroExtrae");
                        if ((VolOtroExtreae == "") || (VolOtroExtreae == "0") || (VolOtroExtreae == null))
                            VolOtroExtreae = VolOtro;
                        ClManejo.Insertar_Resumen_PlanManejo(AsignacionId, Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]), Convert.ToDecimal(AreaRodal), Convert.ToInt32(CodClaseDesarrollo), Edad, Convert.ToInt32(TratamientoId), Convert.ToDecimal(Dap), Convert.ToDecimal(Altura), Convert.ToDecimal(Densidad), Convert.ToDecimal(AreaBasal), Convert.ToDecimal(VolTroza), Convert.ToDecimal(VolLena), Convert.ToDecimal(VolOtro), Convert.ToDecimal(Convert.ToDecimal(VolTroza) + Convert.ToDecimal(VolLena) + Convert.ToDecimal(VolOtro)), Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Rodal"]), Convert.ToInt32(Pendiente), Convert.ToDecimal(INC), Convert.ToDecimal(VolHa), Convert.ToDecimal(VolRodal), Convert.ToInt32(GrdResumen.Items[i].OwnerTableView.DataKeyValues[i]["Extrae"]), Convert.ToDecimal(VolTrozaExtrae), Convert.ToDecimal(VolLenaExtrae), Convert.ToDecimal(VolOtroExtreae), Convert.ToDecimal(Convert.ToDecimal(VolTrozaExtrae) + Convert.ToDecimal(VolLenaExtrae) + Convert.ToDecimal(VolOtroExtreae)), Convert.ToDecimal(AreaBasalRodal));
                    }
                    DivGoodAprovechamiento.Visible = true;
                    LblGoodAprovechamiento.Text = "Datos de Aprovechamiento Forestal Grabados";
                    GrdSilvicultural.Rebind();
                }
                
            }
        }

        bool ValidaAprovechamientoFilas()
        {
            bool NoHayDato = false;
            for (int i = 0; i < GrdResumen.Items.Count; i++)
            {
                object VolRodal = ((RadNumericTextBox)this.GrdResumen.Items[i].FindControl("TxtAreaBasalRodal")).Text;
                if (VolRodal.ToString() == "")
                {
                    NoHayDato = true;
                    DivErrAprovechamiento.Visible = true;
                    LblErrAprovechamiento.Text = "La especie: " + GrdResumen.Items[i].GetDataKeyValue("Nombre_Cientifico") + " no se ha ingresado el Area basal  m2/rodal";
                    break;
                }
            }
            return NoHayDato;
        }

        bool ValidaAprovechamiento()
        {
            LblErrAprovechamiento.Text = "";
            DivErrAprovechamiento.Visible = false;
            bool HayError = false;

            if ((CboTipoIngresoDatos.SelectedValue == "") || (CboTipoIngresoDatos.SelectedValue == "0"))
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe seleccionar el tipo de ingreso de datos";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", ebe seleccionar el tipo de ingreso de datos";
                HayError = true;
            }
            if ((CboTipoInventario.SelectedValue == "") || (CboTipoInventario.SelectedValue == "0"))
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe seleccionar el tipo de inventario";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", ebe seleccionar el tipo de inventario";
                HayError = true;
            }
            if (GrdBoleta.Items.Count == 0)
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe Ingresar la boleta del censo";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", debe Ingresar la boleta del censo";
                HayError = true;
            }
            var item = CboEcuacion.CheckedItems;
            if ((CboTipoIngresoDatos.SelectedValue == "1") && (item.Count == 0))
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe seleccionar al menos una ecuación";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", debe seleccionar al menos una ecuación";
                HayError = true;
            }
            if ((CboTipoIngresoDatos.SelectedValue == "2") && (TxtOtraEcuacion.Text == ""))
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe ingresar las ecuaciones utilizadas";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", debe ingresar las ecuaciones utilizadas";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrAprovechamiento.Visible = true;
                return false;
            }

            else
                return true;
        }

        void GrdResumen_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem; 
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoClaseDesarrollo(), (RadComboBox)item["Clase_Desarrollo_Edit"].FindControl("CboClaseDesarrollo"), "Clase_DesarrolloId", "Clase_Desarrollo");
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_TratamientoSilvicultural(), (RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento"), "Tratamiento_Id", "Tratamiento");
                if (item.GetDataKeyValue("AreaRodal") != null)
                {
                    ((RadNumericTextBox)item["AreaRodalEdit"].FindControl("TxtAreaRodal")).Text = item.GetDataKeyValue("AreaRodal").ToString();
                }
                if (item.GetDataKeyValue("Clase_Desarrollo") != null)
                {
                    ((RadComboBox)item["Clase_Desarrollo_Edit"].FindControl("CboClaseDesarrollo")).Text = item.GetDataKeyValue("Clase_Desarrollo").ToString();
                }
                if (item.GetDataKeyValue("Edad") != null)
                {
                    ((TextBox)item["EdadEdit"].FindControl("TxtEdad")).Text = item.GetDataKeyValue("Edad").ToString();
                }
                if (item.GetDataKeyValue("Tratamiento") != null)
                {
                    ((RadComboBox)item["Tratamiento_Edit"].FindControl("CboTratamiento")).Text = item.GetDataKeyValue("Tratamiento").ToString();
                }
                if (item.GetDataKeyValue("Dap") != null)
                {
                    ((RadNumericTextBox)item["DapEdit"].FindControl("TxtDap")).Text = item.GetDataKeyValue("Dap").ToString();
                }
                if (item.GetDataKeyValue("Altura") != null)
                {
                    ((RadNumericTextBox)item["AlturaEdit"].FindControl("TxtAltura")).Text = item.GetDataKeyValue("Altura").ToString();
                }
                if (item.GetDataKeyValue("Densidad") != null)
                {
                    ((RadNumericTextBox)item["DensidadEdit"].FindControl("TxtDensidad")).Text = item.GetDataKeyValue("Densidad").ToString();
                }
                if (item.GetDataKeyValue("AreaBasal") != null)
                {
                    ((RadNumericTextBox)item["AreaBasalEdit"].FindControl("TxtAreaBasal")).Text = item.GetDataKeyValue("AreaBasal").ToString();
                }
                if (item.GetDataKeyValue("VolTroza") != null)
                {
                    ((RadNumericTextBox)item["VolTrozaEdit"].FindControl("TxtVolTroza")).Text = item.GetDataKeyValue("VolTroza").ToString();
                }
                if (item.GetDataKeyValue("VolLena") != null)
                {
                    ((RadNumericTextBox)item["VolLenaEdit"].FindControl("TxTVolLena")).Text = item.GetDataKeyValue("VolLena").ToString();
                }
                if (item.GetDataKeyValue("VolOtro") != null)
                {
                    ((RadNumericTextBox)item["VolOtroEdit"].FindControl("TxtVolOtro")).Text = item.GetDataKeyValue("VolOtro").ToString();
                }
                if (item.GetDataKeyValue("VolTotal") != null)
                {
                    ((RadNumericTextBox)item["VolTotalEdit"].FindControl("TxtVolTotal")).Text = item.GetDataKeyValue("VolTotal").ToString();
                }
                if (item.GetDataKeyValue("Pendiente") != null)
                {
                    ((RadNumericTextBox)item["PendienteEdit"].FindControl("TxtPendiente")).Text = item.GetDataKeyValue("Pendiente").ToString();
                }
                if (item.GetDataKeyValue("INC") != null)
                {
                    ((RadNumericTextBox)item["INCEdit"].FindControl("TxtINC")).Text = item.GetDataKeyValue("INC").ToString();
                }
                if (item.GetDataKeyValue("VolHa") != null)
                {
                    ((RadNumericTextBox)item["VolHaEdit"].FindControl("TxtVolHa")).Text = item.GetDataKeyValue("VolHa").ToString();
                }
                if (item.GetDataKeyValue("VolRodal") != null)
                {
                    ((RadNumericTextBox)item["VolRodalEdit"].FindControl("TxtVolRodal")).Text = item.GetDataKeyValue("VolRodal").ToString();
                }
                if (item.GetDataKeyValue("AreaBasalRodal") != null)
                {
                    ((RadNumericTextBox)item["AreaBasalRodalEdit"].FindControl("TxtAreaBasalRodal")).Text = item.GetDataKeyValue("AreaBasalRodal").ToString();
                }
            }
        }

        void GrdResumen_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in this.GrdResumen.Items)
            {
                if (item.OwnerTableView.Name == "LabelsResumen")
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
                            
                            item2["AreaRodal"].RowSpan = (item3["AreaRodal"].RowSpan < 2) ? 2 : (item3["AreaRodal"].RowSpan + 1);
                            item3["AreaRodal"].Visible = false;
                            item2["AreaRodalEdit"].RowSpan = (item3["AreaRodalEdit"].RowSpan < 2) ? 2 : (item3["AreaRodalEdit"].RowSpan + 1);
                            item3["AreaRodalEdit"].Visible = false;
                            item2["Clase_Desarrollo"].RowSpan = (item3["Clase_Desarrollo"].RowSpan < 2) ? 2 : (item3["Clase_Desarrollo"].RowSpan + 1);
                            item3["Clase_Desarrollo"].Visible = false;
                            item2["Clase_Desarrollo_Edit"].RowSpan = (item3["Clase_Desarrollo_Edit"].RowSpan < 2) ? 2 : (item3["Clase_Desarrollo_Edit"].RowSpan + 1);
                            item3["Clase_Desarrollo_Edit"].Visible = false;
                            item2["Edad"].RowSpan = (item3["Edad"].RowSpan < 2) ? 2 : (item3["Edad"].RowSpan + 1);
                            item3["Edad"].Visible = false;
                            item2["EdadEdit"].RowSpan = (item3["EdadEdit"].RowSpan < 2) ? 2 : (item3["EdadEdit"].RowSpan + 1);
                            item3["EdadEdit"].Visible = false;

                            item2["Tratamiento"].RowSpan = (item3["Tratamiento"].RowSpan < 2) ? 2 : (item3["Tratamiento"].RowSpan + 1);
                            item3["Tratamiento"].Visible = false;
                            item2["Tratamiento_Edit"].RowSpan = (item3["Tratamiento_Edit"].RowSpan < 2) ? 2 : (item3["Tratamiento_Edit"].RowSpan + 1);
                            item3["Tratamiento_Edit"].Visible = false;

                            item2["Pendiente"].RowSpan = (item3["Pendiente"].RowSpan < 2) ? 2 : (item3["Pendiente"].RowSpan + 1);
                            item3["Pendiente"].Visible = false;
                            item2["PendienteEdit"].RowSpan = (item3["PendienteEdit"].RowSpan < 2) ? 2 : (item3["PendienteEdit"].RowSpan + 1);
                            item3["PendienteEdit"].Visible = false;
                            item2["INC"].RowSpan = (item3["INC"].RowSpan < 2) ? 2 : (item3["INC"].RowSpan + 1);
                            item3["INC"].Visible = false;
                            item2["INCEdit"].RowSpan = (item3["INCEdit"].RowSpan < 2) ? 2 : (item3["INCEdit"].RowSpan + 1);
                            item3["INCEdit"].Visible = false;

                        }
                    }
                }
            }
        }

        void GrdResumen_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_Resumen_Censo(1, Convert.ToInt32(TxtAsignacionId.Text)), GrdResumen);
        }

        void GrdBoleta_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_Datos_Boleta(Convert.ToInt32(TxtAsignacionId.Text)), GrdBoleta);
        }

        bool ValidaCargaboleta()
        {
            LblErrAprovechamiento.Text = "";
            DivErrAprovechamiento.Visible = false;
            bool HayError = false;

            if (CboTipoIngresoDatos.SelectedValue == "")
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe Seleccionar el tipo de Ingreso de datos";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", debe Seleccionar el tipo de Ingreso de datos";
                HayError = true;
            }
            if (CboTipoInventario.SelectedValue == "")
            {
                if (LblErrAprovechamiento.Text == "")
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + "Debe Seleccionar el tipo de Inventario";
                else
                    LblErrAprovechamiento.Text = LblErrAprovechamiento.Text + ", debe Seleccionar el tipo de Inventario";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrAprovechamiento.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnCargarBoleta_ServerClick(object sender, EventArgs e)
        {
            if (ValidaCargaboleta() == true)
            {
                try
                {
                    Stream stream = RadUploadBoleta.UploadedFiles[0].InputStream;
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    excelReader.IsFirstRowAsColumnNames = true;
                    resultXls = excelReader.AsDataSet();
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    ClManejo.Elimina_Boleta(AsignacionId);
                    ClManejo.Elimina_Boleta_Dos(AsignacionId);
                    foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                    {
                        if (iDtRow["Turno"].ToString() != "")
                        {
                            if (Convert.ToInt32(ClManejo.Existe_Especie(iDtRow["NOMBRE_CIENTIFICO"].ToString())) > 0)
                            {
                                int X = 0;
                                int Y = 0;
                                if (Convert.ToInt32(iDtRow["X"].ToString().Length) > 0)
                                    X = Convert.ToInt32(iDtRow["X"]);
                                if (Convert.ToInt32(iDtRow["Y"].ToString().Length) > 0)
                                    Y = Convert.ToInt32(iDtRow["Y"]);

                                ClManejo.Sp_Insert_Boleta(AsignacionId, Convert.ToInt32(iDtRow["Turno"]), Convert.ToInt32(iDtRow["Rodal"]), Convert.ToDouble(iDtRow["No"]), Convert.ToDouble(iDtRow["Dap"]), Convert.ToDouble(iDtRow["Altura"]), iDtRow["NOMBRE_CIENTIFICO"].ToString(), Convert.ToDouble(iDtRow["%_TROZA"]), X, Y);
                            }

                        }

                    }

                    GrdBoleta.Rebind();
                    GrdResumen.Rebind();
                    GrdSilvicultural.Rebind();
                    ClManejo.Get_Turno_PlanManejo(AsignacionId);
                }
                catch (Exception ex)
                {
                    String iM = ex.Message;

                    //pMensaje();

                }
            }
        }

        void BtnGrabarCaminos_ServerClick(object sender, EventArgs e)
        {
            ClManejo.Eliminar_RedCaminos_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            ClManejo.Insert_RedCaminos_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToDouble(ClUtilitarios.IIf(TxtPrimarioExistente.Text == "", 0, TxtPrimarioExistente.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPrimarioConstruir.Text == "", 0, TxtPrimarioConstruir.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtSecundarioExistente.Text == "", 0, TxtSecundarioExistente.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtSecundarioConstruir.Text == "", 0, TxtSecundarioConstruir.Text)), TxtOtroEspecifique.Text, Convert.ToDouble(ClUtilitarios.IIf(TxtOtroExistente.Text == "", 0, TxtOtroExistente.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtOtroConstruir.Text == "", 0, TxtOtroConstruir.Text)));
            DivGoodCaminos.Visible = true;
            LblGoodCaminos.Text = "Caminos Grabados";
        }


        void GrdActividadAprovechamiento_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClManejo.Eliminar_Actividad_Aprovechamiento(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ActividadId"].ToString()));
                GrdActividadAprovechamiento.Rebind();
            }
        }

        void GrdActividadAprovechamiento_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_Actividad_Aprovechamiento(Convert.ToInt32(TxtAsignacionId.Text)), GrdActividadAprovechamiento);
        }

        void BtnAddActividadApro_Click(object sender, EventArgs e)
        {
            ClManejo.Insert_Actividad_Aprovechamiento(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(CboTipoProducto.SelectedValue), Convert.ToInt32(CboTipoAprovechamiento.SelectedValue), TxtActividadAprovechamiento.Text);
            DivGoodActividadAprovechamiento.Visible = true;
            LblGoodActividadAprovechamiento.Text = "Actividad Ingresada";
            GrdActividadAprovechamiento.Rebind();
        }

        void GrdActividades_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClManejo.Eliminar_Actividad_Cronograma(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ActividadId"].ToString()));
                GrdActividades.Rebind();
            }
        }

        void GrdActividades_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_Actividades_Cronograma(Convert.ToInt32(TxtAsignacionId.Text)),GrdActividades);
        }

        void BtnGrabarActicidad_Click(object sender, EventArgs e)
        {
                if (ValidaActividad() == true)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    ClManejo.Insert_ActividadCronograma(AsignacionId, TxtActividad.Text, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecIni.SelectedDate)), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TXtFecFin.SelectedDate)));
                    DivGoodActividad.Visible = true;
                    LblGoodActividad.Text = "Actividad ingresada correctamente";
                    GrdActividades.Rebind();
                    LimpiarActividad();
                }
        }

        void LimpiarActividad()
        {
            TxtActividad.Text = "";
            TxtFecIni.SelectedDate = DateTime.Now;
            TXtFecFin.SelectedDate = DateTime.Now;
        }

        bool ValidaActividad()
        {
            LblErrActividad.Text = "";
            DivErrActividad.Visible = false;
            bool HayError = false;
            if (TxtFecIni.DateInput.Text == "")
            {
                if (LblErrActividad.Text == "")
                    LblErrActividad.Text = LblErrActividad.Text + "Debe ingresar la fecha inicial";
                else
                    LblErrActividad.Text = LblErrActividad.Text + ", debe ingresar la fecha inicial";
                HayError = true;
            }
            if (TXtFecFin.DateInput.Text == "")
            {
                if (LblErrActividad.Text == "")
                    LblErrActividad.Text = LblErrActividad.Text + "Debe ingresar la fecha final";
                else
                    LblErrActividad.Text = LblErrActividad.Text + ", debe ingresar la fecha final";
                HayError = true;
            }
            if ((TxtFecIni.DateInput.Text != "") && (TXtFecFin.DateInput.Text != "") &&  (TxtFecIni.SelectedDate.Value > TXtFecFin.SelectedDate.Value))
            {
                if (LblErrActividad.Text == "")
                    LblErrActividad.Text = LblErrActividad.Text + "La Fecha inicial no puede ser mayor a la final";
                else
                    LblErrActividad.Text = LblErrActividad.Text + ", la fecha inicial no puede ser mayor a la final";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrActividad.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnGrabarProteccionForestal_Click(object sender, EventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            ClManejo.Eliminar_ProteccionForestal(AsignacionId);
            ClManejo.Insert_ProteccionForestal(AsignacionId, TxtMedidasPrevencion.Text, TxtPrevencionControlPlaga.Text, TxtJustificacionPrevencionIF.Text, TxtLineasControlRonda.Text, TxtVigilanciaPuestoPunto.Text, TxtManejoCombustible.Text, TxtIdentificacionAreaCritica.Text, TxtRespuestaCasoIf.Text, TxtJustificacionPf.Text, TxtMonitoreoPlaga.Text, TxtControlPlaga.Text, TxtAmpliacionRonda.Text, TXtRondasCortaFuego.Text, TxtBrigadaIncendio.Text, TxtIdentificacionRutaEscape.Text,TxtProteccionAgua.Text, TxtProteccionOtrosFac.Text);
            DivGoodProteccionForestal.Visible = true;
            LblGoodProteccionForestal.Text = "Datos Grabados";
        }



        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {
            RadWindow1.Title = "Vista Previa Plan de Manejo Forestal";
            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtAsignacionId.Text, true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void BtnGrabarMedidasControl_Click(object sender, EventArgs e)
        {
            DivGoodMediadasControl.Visible = false;
            LblGoodMedidasControl.Text = "";
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            ClManejo.Eliminar_MedidaControl_PlanManejo(AsignacionId);
            ClManejo.Insertar_MedidaControl_PlanManejo(AsignacionId, TxtDescripcionMedidas.Text, Txttratamiento.Text, TxtResiduos.Text);
            DivGoodMediadasControl.Visible = true;
            LblGoodMedidasControl.Text = "Datos Grabados";
        }

        void btnGrabarPlaga_Click(object sender, EventArgs e)
        {
            DivGoodPlaga.Visible = false;
            LblGoodPlaga.Text = "";
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            ClManejo.Eliminar_Plagas_PlanManejo(AsignacionId);
            ClManejo.Insertar_Plagas_PlanManejo(AsignacionId, TxtAgenteCausal.Text, TxtSintomologia.Text, TxtDano.Text, TxtFenomenoNatural.Text, TxtEstimacionMadera.Text);
            DivGoodPlaga.Visible = true;
            LblGoodPlaga.Text = "Datos Grabados";
        }

        void BtnGrabarPlanCientifico_Click(object sender, EventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            ClManejo.Elimina_Temp_PlanInvestigacion_PlanMenjo(AsignacionId);
            ClManejo.Insert_PlanInvestigacion_PlanMenjo(AsignacionId, TxtIndice.Text, TxtIntro.Text, TxtDelimitacion.Text, TxtAntecedentes.Text, TxtObjetivos.Text, TxtAlcances.Text, TxtPlanteamiento.Text, TxtJustificacion.Text, TxtMetodologia.Text, TxtRecursos.Text, TxtResultados.Text, TxtCronograma.Text, TxtBibliografia.Text);
            DivGoodPlanCientifico.Visible = true;
            LblGoodPlanCientifico.Text = "Datos Grabados";
        }

        void RadTabStrip1_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            if (e.Tab.Index == 0)
            {
                RadPageFincas.Visible = true;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            else if (e.Tab.Index == 1)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = true;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;

            }
            if (e.Tab.Index == 2)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = true;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;

                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 3)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = true;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 4)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = true;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 5)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = true;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 6)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = true;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 7)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = true;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 8)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = true;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 9)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = true;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 10)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = true;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 11)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = true;
                RadPagePlanificacionManejo.Visible = true;
                OverrideProductosNoMaderablesExtrae();
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = false;
            }
            if (e.Tab.Index == 12)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = true;
                RadPageCronograma.Visible = false;
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
                if (SubCategoriaId == 6)
                {
                    decimal Area = ClManejo.Get_SumAreaIntervenir_AreaProteccion(Convert.ToInt32(TxtAsignacionId.Text));
                    if (Area <= 45)
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                    }
                    else
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                        DivBrigadaIncendio.Visible = true;
                        DivIdentificacionRutaEscape.Visible = true;
                    }
                }
                else if (SubCategoriaId == 9)
                {
                    decimal Area = ClManejo.Get_SumAreaIntervenir_AreaProteccion(Convert.ToInt32(TxtAsignacionId.Text));
                    if (Area <= 15)
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        
                    }
                    else if ((Area > 15) && (Area <= 45))
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                    }
                    else 
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                        DivBrigadaIncendio.Visible = true;
                        DivIdentificacionRutaEscape.Visible = true;
                    }
                }
                else if (SubCategoriaId == 11)
                {
                    decimal Area = ClManejo.Get_SumAreaIntervenir_AreaProteccion(Convert.ToInt32(TxtAsignacionId.Text));
                    if (Area <= 15)
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";

                    }
                    else if ((Area > 15) && (Area <= 45))
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                    }
                    else
                    {
                        DivProteccionForJustPrev.Visible = true;
                        DivProteccionForLineaControlRonda.Visible = true;
                        DivProteccionForVigilanciaPuestoPunto.Visible = true;
                        DivProteccionForManejoCombustible.Visible = true;
                        DivProteccionForJustificacionPF.Visible = true;
                        DivProteccionForMonitoreoPlaga.Visible = true;
                        DivProteccionForControlPlaga.Visible = true;
                        DivProteccionForAreasCriticas.Visible = true;
                        DivProteccionForRespuestaCasoIF.Visible = true;
                        LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                        DivAmpliacionRonda.Visible = true;
                        DivRondasCortaFuego.Visible = true;
                        DivBrigadaIncendio.Visible = true;
                        DivIdentificacionRutaEscape.Visible = true;
                    }
                }
            }
            if (e.Tab.Index == 13)
            {
                RadPageFincas.Visible = false;
                RadPageRepresentantes.Visible = false;
                RadPageDatosNotifica.Visible = false;
                RadPageDatosPlan.Visible = false;
                RadPageCaracBio.Visible = false;
                RadPagePlanInvestigacion.Visible = false;
                RadPagePlaga.Visible = false;
                RadPageMedidasdeControl.Visible = false;
                RadPageAprovechamiento.Visible = false;
                RadPageActividadesApro.Visible = false;
                RadPageRepoblacion.Visible = false;
                RadPagePlanificacionManejo.Visible = false;
                RadPageProteccionForestal.Visible = false;
                RadPageCronograma.Visible = true;
            }
        }

        void ConfiguraPlanManejo(int SubCategoriaId)
        {
            TxtSubCategoria.Text = SubCategoriaId.ToString();
            if (SubCategoriaId == 13) 
            {
                RadTabStrip1.Tabs[5].Visible = true;
            }
            if (SubCategoriaId == 11)
            {
                RadTabStrip1.Tabs[6].Visible = true;
                RadTabStrip1.Tabs[7].Visible = true;
            }
            if (SubCategoriaId == 12)
            {
                RadTabStrip1.Tabs[6].Visible = true;
                LblTitPagePlaga.Text = "DESCRIPCION DEL FENÓMENO NATURAL Y ESTIMACION DEL DAÑO CAUSADO";
                DivFenNatural.Visible = true;
                DivSintomologia.Visible = false;
                DivAgenteCausal.Visible = false;
            }
            if (SubCategoriaId == 10)
            
            {
                RadTabStrip1.Tabs[3].Visible = false;
                RadTabStrip1.Tabs[4].Visible = false;
                RadTabStrip1.Tabs[6].Visible = true;
                LblTitPagePlaga.Text = "DESCRIPCION DEL FENÓMENO NATURAL Y ESTIMACION DEL DAÑO CAUSADO";
                DivSintomologia.Visible = false;
                DivDescDamage.Visible = false;
                DivEstimacionMadera.Visible = true;
                RadTabStrip1.Tabs[7].Visible = true;
                RadTabStrip1.Tabs[9].Visible = true;
            }
            if (SubCategoriaId == 4)
            {
                DivProteccionForJustPrev.Visible = true;
                DivProteccionForLineaControlRonda.Visible = true;
                DivProteccionForVigilanciaPuestoPunto.Visible = true;
                DivProteccionForManejoCombustible.Visible = true;
                DivProteccionForJustificacionPF.Visible = true;
                DivProteccionForMonitoreoPlaga.Visible = true;
                DivProteccionForControlPlaga.Visible = true;
                DivProteccionForAreasCriticas.Visible = true;
                DivProteccionForRespuestaCasoIF.Visible = true;
                DivDiametroMinRodal.Visible = true;
                DivProdNoForesalUno.Visible = true;
                DivProdNoForesalDos.Visible = true;
                DivProdNoForesalTres.Visible = true;
                RadTabStrip1.Tabs[11].Visible = true;
                RadTabStrip1.Tabs[12].Visible = true;
                LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                GrdResumen.Columns.FindByUniqueName("Tratamiento_Edit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("PendienteEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("INCEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("VolTrozaEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolLenaEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolOtroEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolTotalEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolHaEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("VolRodalEdit").Visible = true;

                GrdSilvicultural.Columns.FindByUniqueName("Clase_Desarrollo_Edit").Visible = false;
                GrdSilvicultural.Columns.FindByUniqueName("DapEdit").Visible = false;
                GrdSilvicultural.Columns.FindByUniqueName("AlturaEdit").Visible = false;
            }
            if (SubCategoriaId == 5)
            {
                RadTabStrip1.Tabs[9].Visible = true;
                DivProteccionForJustPrev.Visible = true;
                DivProteccionForLineaControlRonda.Visible = true;
                DivProteccionForVigilanciaPuestoPunto.Visible = true;
                DivProteccionForManejoCombustible.Visible = true;
                DivProteccionForJustificacionPF.Visible = true;
                DivProteccionForMonitoreoPlaga.Visible = true;
                DivProteccionForControlPlaga.Visible = true;
                DivProteccionForAreasCriticas.Visible = true;
                DivProteccionForRespuestaCasoIF.Visible = true;
                RadTabStrip1.Tabs[11].Visible = true;
                RadTabStrip1.Tabs[12].Visible = true;
                LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
                GrdResumen.Columns.FindByUniqueName("Tratamiento_Edit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("PendienteEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("INCEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("VolTrozaEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolLenaEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolOtroEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolTotalEdit").Visible = false;
                GrdResumen.Columns.FindByUniqueName("VolHaEdit").Visible = true;
                GrdResumen.Columns.FindByUniqueName("VolRodalEdit").Visible = true;

                GrdSilvicultural.Columns.FindByUniqueName("Clase_Desarrollo_Edit").Visible = false;
                GrdSilvicultural.Columns.FindByUniqueName("DapEdit").Visible = false;
                GrdSilvicultural.Columns.FindByUniqueName("AlturaEdit").Visible = false;
            }
            if (SubCategoriaId == 6)
            {
                RadTabStrip1.Tabs[9].Visible = true;
            }
            if (SubCategoriaId == 7)
            {
                RadTabStrip1.Tabs[9].Visible = true;
                DivProteccionAgua.Visible = true;
                DivProteccionOtrosFac.Visible = true;
                LblPrevecionPlaga.Text = "Medidas de Prevención Contra Plagas Forestales";
            }
            if (SubCategoriaId == 8)
            {
                RadTabStrip1.Tabs[8].Visible = true;
            }
            if (SubCategoriaId == 9)
            {
                RadTabStrip1.Tabs[9].Visible = true;
            }
            if (SubCategoriaId == 11)
            {
                RadTabStrip1.Tabs[10].Visible = true;
                RadTabStrip1.Tabs[12].Visible = true;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipo_Garantia(2), CboTipoGarantia, "Tipo_GarantiaId", "Tipo_Garantia");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoGarantia, "Tipo de Garantía");
                GrdBoleta.Columns[1].HeaderText = "Foco";
                GrdResumen.Columns[0].HeaderText = "Foco";
                GrdResumen.Columns[1].HeaderText = "Área del Foco (ha)";
                GrdResumen.Columns[2].HeaderText = "Área del Foco (ha)";
            }
            if (SubCategoriaId == 14)
            {
                RadTabStrip1.Tabs[8].Visible = false;
                RadTabStrip1.Tabs[9].Visible = true;
                DivCambioUso.Visible = true;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipo_Garantia(1), CboTipoGarantia, "Tipo_GarantiaId", "Tipo_Garantia");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoGarantia, "Tipo de Garantía");
            }
            else
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipo_Garantia(1), CboTipoGarantia, "Tipo_GarantiaId", "Tipo_Garantia");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoGarantia, "Tipo de Garantía");
            }

            
        }

        void BtnGrabarCarcBiofisicas_Click(object sender, EventArgs e)
        {
            if (ValidaCaracBiofisicas() == true)
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                ClManejo.Eliminar_CaracteristicasBiofisicas_PlanManejo(AsignacionId);
                ClManejo.Insertar_CaracteristicasBiofisicas_PlanManejo(AsignacionId, TxtTopografia.Text, TxtSuelos.Text, TxtHidrografia.Text, Convert.ToInt32(CboZonaVida.SelectedValue), TxtAltitud.Text);
                DivGoodBio.Visible = true;
                lblGoodBio.Text = "Datos Grabados";
            }
        }

        bool ValidaCaracBiofisicas()
        {
            LblErrorBio.Text = "";
            DivErrorBio.Visible = false;
            lblGoodBio.Text = "";
            DivGoodBio.Visible = false;
            bool HayError = false;
            if (TxtTopografia.Text == "")
            {
                if (LblErrorBio.Text == "")
                    LblErrorBio.Text = LblErrorBio.Text + "Debe ingresar la topografía";
                else
                    LblErrorBio.Text = LblErrorBio.Text + ", ebe ingresar la topografía";
                HayError = true;
            }
            if (TxtSuelos.Text == "")
            {
                if (LblErrorBio.Text == "")
                    LblErrorBio.Text = LblErrorBio.Text + "Debe ingresar los suelos";
                else
                    LblErrorBio.Text = LblErrorBio.Text + ", ebe ingresar los suelos";
                HayError = true;
            }
            if ((CboZonaVida.Text == "") || (CboZonaVida.SelectedValue == ""))
            {
                if (LblErrorBio.Text == "")
                    LblErrorBio.Text = LblErrorBio.Text + "Debe seleccionar la zona de vida";
                else
                    LblErrorBio.Text = LblErrorBio.Text + ", debe seleccionar la zona de vida";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrDatosNotifica.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnGrabarInfoGenPlan_Click(object sender, EventArgs e)
        {
            if (ValidaInfoGen() == true)
            {
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                
                ClManejo.Eliminar_InfoGeneral_SistemaRepoblacion_PlanManejo(AsignacionId);
                ClManejo.Eliminar_Poligono_Repoblacion_PlanManejo(AsignacionId);
                ClManejo.Eliminar_InfoGeneral_PlanManejo(AsignacionId);

                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Especies");
                XmlNode iElementos = iInformacion.CreateElement("Especie");
                for (int i = 0; i < GrdEspeciePLanManejo.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                    ClXml.AgregarAtributo("No", i+1, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    ClXml.AgregarAtributo("EspecieId", GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
                ClManejo.Insert_InfoGeneral_PlanManejo(AsignacionId,Convert.ToInt32(TxtTiempoPlanManejo.Text),Convert.ToInt32(TxtTiempoExtraccion.Text),Convert.ToDouble(TxtVolExtraer.Text),TxtSistemaCorta.Text,Convert.ToDouble(TxtIncrementoAnual.Text),Convert.ToDouble(TxtCortaAnual.Text),iInformacion,Convert.ToInt32(CboTipoGarantia.SelectedValue));
                if (TxtSubCategoria.Text == "14")
                {

                    ClManejo.Actualizo_DatosCambioUso(AsignacionId, Convert.ToInt32(CboCambioUsoForestal.SelectedValue), TxtCambioUsoEspecifique.Text);
                }
                var collection = CboSistemaRepoblacion.CheckedItems;
                foreach ( var item in collection )
                {
                    ClManejo.Insert_SistemaRepoblacionPlanManejo(AsignacionId, Convert.ToInt32(item.Value));
                }
                string ErrorMapa = "";
                if (GrdPolAreaRepo.Items.Count > 0)
                {
                    XmlDocument iInformacionPolBosque = ClXml.CrearDocumentoXML("Poligonos");
                    XmlNode iElementoPoligono = iInformacionPolBosque.CreateElement("Puntos");

                    for (int i = 0; i < GrdPolAreaRepo.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacionPolBosque.CreateElement("Item");
                        ClXml.AgregarAtributo("Id", GrdPolAreaRepo.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                        ClXml.AgregarAtributo("X", GrdPolAreaRepo.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                        ClXml.AgregarAtributo("Y", GrdPolAreaRepo.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                        iElementoPoligono.AppendChild(iElementoDetalle);
                    }
                    iInformacionPolBosque.ChildNodes[1].AppendChild(iElementoPoligono);
                    String iPoligonoGML = "";


                    if (!ClPoligono.Actualizar_Poligono_AreaRepoblacion(iInformacionPolBosque, ref AsignacionId, ref iPoligonoGML, ref ErrorMapa))
                    {
                        DivErrorInfoGenPlan.Visible = true;
                        LblErrorInfoGenPlan.Text = ErrorMapa;
                        ClManejo.Eliminar_InfoGeneral_SistemaRepoblacion_PlanManejo(AsignacionId);
                        ClManejo.Eliminar_Poligono_Repoblacion_PlanManejo(AsignacionId);
                        ClManejo.Eliminar_InfoGeneral_PlanManejo(AsignacionId);
                    }
                    else
                    {
                        DivGoodInfoGenPlan.Visible = true;
                        LblGoodInfoGenPlan.Text = "Datos Grabados"; 
                    }
                }
            }
        }

        bool ValidaInfoGen()
        {
            LblErrorInfoGenPlan.Text = "";
            DivErrorInfoGenPlan.Visible = false;
            bool HayError = false;
            if (TxtTiempoPlanManejo.Text == "") 
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el de  Dato de Tiempo de Ejecución del Plan de Manejo Forestal (Años)";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el de  Dato de Tiempo de Ejecución del Plan de Manejo Forestal (Años)";
                HayError = true;
            }
            if (TxtTiempoExtraccion.Text == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el dato de Tiempo de Ejecución de la extracción: (Meses)";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el de Dato de Tiempo de Ejecución de la extracción: (Meses)";
                HayError = true;
            }
            if (TxtVolExtraer.Text == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el dato de Volumen a extraer";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el de Volumen a extraer";
                HayError = true;
            }
            if (TxtSistemaCorta.Text == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el sistema de corta";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el sistema de corta";
                HayError = true;
            }
            if (TxtIncrementoAnual.Text == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el incremento anual";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el incremento anual";
                HayError = true;
            }
            if (TxtCortaAnual.Text == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar la corta anual permisible";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar la corta anual permisible";
                HayError = true;
            }
            if (GrdEspeciePLanManejo.Items.Count == 0)
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar al menos una especie";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar al menos una especie";
                HayError = true;
            }
            var item = CboSistemaRepoblacion.CheckedItems;
            if (item.Count == 0)
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe seleccionar al menos un sistema de repoblación";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe seleccionar al menos un sistema de repoblación";
                HayError = true;
            }
            if (GrdPolAreaRepo.Items.Count == 0)
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe ingresar el polígono del Área de repoblación";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", debe ingresar el polígono del Área de repoblación";
                HayError = true;
            }
            if (CboTipoGarantia.SelectedValue == "")
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe seleccionar el tipo de garantía";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", Debe seleccionar el tipo de garantía";
                HayError = true;
            }
            if ((TxtSubCategoria.Text == "14") && (CboCambioUsoForestal.SelectedValue == ""))
            {
                if (LblErrorInfoGenPlan.Text == "")
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + "Debe seleccionar el tipo de cambio de uso";
                else
                    LblErrorInfoGenPlan.Text = LblErrorInfoGenPlan.Text + ", Debe seleccionar el tipo de cambio de uso";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrorInfoGenPlan.Visible = true;
                return false;
            }

            else
                return true;
        }

        void GrdPolAreaRepo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPolAreaRepo, "Dt_PoligonoAreaRepo");
        }

        void btnCargarPolAreaRepo_ServerClick(object sender, EventArgs e)
        {
            DivErrPolAreaRepo.Visible = false;
            if (UploadPolAreaRepo.UploadedFiles.Count > 0)
            {
                string Extension = UploadPolAreaRepo.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPolAreaRepo.Visible = true;
                    LblErrAreaRepo.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = UploadPolAreaRepo.UploadedFiles[0].InputStream;
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        resultXls = excelReader.AsDataSet();

                        Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].Clear();
                        foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                        {
                            if (iDtRow["X"].ToString() != "")
                            {
                                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].NewRow();
                                rowNew["Id"] = iDtRow["Poligono"];
                                rowNew["X"] = iDtRow["X"];
                                rowNew["Y"] = iDtRow["Y"];
                                Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].Rows.Add(rowNew);
                            }

                        }

                        GrdPolAreaRepo.Rebind();
                    }
                    catch (Exception ex)
                    {
                        DivErrPolAreaRepo.Visible = true;
                        LblErrAreaRepo.Text = ex.Message;
                    }
                }

                
                
            }
            else
            {
                DivErrPolAreaRepo.Visible = true;
                LblErrAreaRepo.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }
            
        }

        void GrdEspeciePLanManejo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarEspeciePlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString()));
            }
        }

        void GrdEspeciePLanManejo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (dv.Count > 0)
                ClUtilitarios.LlenaGridDataView(dv, GrdEspeciePLanManejo, "Dt_EspecieArb");
        }

        void BtnAddEspeciePlanManejo_ServerClick(object sender, EventArgs e)
        {
            if (ValidaEspeciePlanManejo() == true)
            {
                if (ExisteEspeciePlanManejo(Convert.ToInt32(CboEspeciePlanManejo.SelectedValue)))
                {
                    DivErrEspeciePlan.Visible = true;
                    LblErrEspeciePlan.Text = "Especie ya existe";

                }
                else
                {
                    CargarGridEspeciePlanManejo();
                    AgregaEspeciePlanManejo();
                    dv = Ds_Temporal.Tables["Dt_EspecieArb"].DefaultView;
                    GrdEspeciePLanManejo.Rebind();
                    LimpiarEspeciePlanManejo();
                }
            }
        }

        void CboDepartamentoNotifica_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamentoNotifica.SelectedValue)), CboMunicipioNotifica, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioNotifica, "Municipio");
        }

        void BtnGrabarDatosNotifica_Click(object sender, EventArgs e)
        {
            if (ValidaDatosNotifica() == true)
            {
                int TelDomicilio = 0;
                if (TxtTelDomicilio.Text != "")
                    TelDomicilio = Convert.ToInt32(TxtTelDomicilio.Text.Replace("-", ""));
                int Telefono = 0;
                if (TxtTelefonoNotifica.Text != "")
                    Telefono = Convert.ToInt32(TxtTelefonoNotifica.Text.Replace("-", ""));
                ClManejo.Delete_Datos_Notifica_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
                ClManejo.Insertar_Datos_Notifica_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), TxtDireccionNotifica.Text, Convert.ToInt32(CboMunicipioNotifica.SelectedValue), TelDomicilio, Telefono, Convert.ToInt32(TxtCelularNotifica.Text.Replace("-", "")), TxtCorreoNotifica.Text);
                DivGoodDatosNotifica.Visible = true;
                LblGoodDatosNotifica.Text = "Datos Grabados correctamente";
            }
        }

        void Get_Datos_Notifica()
        {
            DataSet dsDatosNotifica = ClManejo.Get_Datos_Notifica_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosNotifica.Tables["Datos"].Rows.Count > 0)
            {
                TxtDireccionNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Direccion"].ToString();
                CboDepartamentoNotifica.SelectedValue = dsDatosNotifica.Tables["Datos"].Rows[0]["DepartamentoId"].ToString();
                CboDepartamentoNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Departamento"].ToString();
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamentoNotifica.SelectedValue)), CboMunicipioNotifica, "MunicipioId", "Municipio");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioNotifica, "Municipio");
                CboMunicipioNotifica.SelectedValue = dsDatosNotifica.Tables["Datos"].Rows[0]["MunicipioId"].ToString();
                CboMunicipioNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Municipio"].ToString();
                TxtTelDomicilio.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Telefono_Domicilio"].ToString();
                if (TxtTelDomicilio.Text == "0")
                    TxtTelDomicilio.Text = "";
                TxtTelefonoNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Telefono"].ToString();
                if (TxtTelefonoNotifica.Text == "0")
                    TxtTelefonoNotifica.Text = "";
                TxtCelularNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Celular"].ToString();
                TxtCorreoNotifica.Text = dsDatosNotifica.Tables["Datos"].Rows[0]["Correo"].ToString();
            }
            
            dsDatosNotifica.Clear();
        }

        bool ValidaDatosNotifica()
        {
            LblErrDatosNotifica.Text = "";
            DivErrDatosNotifica.Visible = false;
            LblGoodDatosNotifica.Text = "";
            DivGoodDatosNotifica.Visible = false;
            bool HayError = false;
            if ((CboDepartamentoNotifica.Text == "") || (CboDepartamentoNotifica.SelectedValue == ""))
            {
                if (LblErrDatosNotifica.Text == "")
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + "Debe seleccionar el departamento";
                else
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + ", debe seleccionar el departamento";
                HayError = true;
            }
            if ((CboMunicipioNotifica.Text == "") || (CboMunicipioNotifica.SelectedValue == ""))
            {
                if (LblErrDatosNotifica.Text == "")
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + "Debe seleccionar el Municipio";
                else
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + ", debe seleccionar el Municipio";
                HayError = true;
            }
            if ((TxtCorreoNotifica.Text != "") && (ClUtilitarios.EsInstitucional(TxtCorreoNotifica.Text) == true))
            {
                if (LblErrDatosNotifica.Text == "")
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + "No puede agregar correos del dominio inab.gob.gt";
                else
                    LblErrDatosNotifica.Text = LblErrDatosNotifica.Text + ", No puede agregar correos del dominio inab.gob.gt";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrDatosNotifica.Visible = true;
                return false;
            }

            else
                return true;
        }

        void GrdPolProteccion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_PoligonoProteccion"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPolProteccion, "Dt_PoligonoProteccion");
        }

        void BtncargarPolProteccion_ServerClick(object sender, EventArgs e)
        {
            DivErrPolProteccion.Visible = false;
            if (RadUloadPolProteccion.UploadedFiles.Count > 0)
            {
                string Extension = RadUloadPolProteccion.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPolProteccion.Visible = true;
                    LblErrPolProteccion.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = RadUloadPolProteccion.UploadedFiles[0].InputStream;
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        resultXls = excelReader.AsDataSet();

                        Ds_Temporal.Tables["Dt_PoligonoProteccion"].Clear();
                        foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                        {
                            if (iDtRow["X"].ToString() != "")
                            {
                                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoProteccion"].NewRow();
                                rowNew["Id"] = iDtRow["Poligono"];
                                rowNew["X"] = iDtRow["X"];
                                rowNew["Y"] = iDtRow["Y"];
                                Ds_Temporal.Tables["Dt_PoligonoProteccion"].Rows.Add(rowNew);
                            }

                        }

                        GrdPolProteccion.Rebind();
                    }
                    catch (Exception ex)
                    {
                        String iM = ex.Message;
                        DivErrPolProteccion.Visible = true;
                        LblErrPolProteccion.Text = ex.Message;
                    }
                }
                
            }
            else
            {
                DivErrPolProteccion.Visible = true;
                LblErrPolProteccion.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }
            
        }

        void LimpiarAreas()
        {
            TxtUsoForestal.Text = "";
            TxtUsoPorForestal.Text = "";
            TxtUsoAgricultora.Text = "";
            TxtUsoPorAgricultura.Text = "";
            TxtUsoGanaderia.Text = "";
            TxtUsoPorGanaderia.Text = "";
            TxtUsoAgroForestal.Text = "";
            TxtUsoPorAgroforestal.Text = "";
            TxtUsoOtrosEspecifique.Text = "";
            TxtUsoOtros.Text = "";
            TxtUsoPorOtro.Text = "";
            CboTipoBosque.Items.Clear();
            CboClaseDesarrollo.Items.Clear();
            if (dv.Count > 0)
                dv.Delete(0);
            GrdEspecies.Rebind();
            TxtAreaBosque.Text = "";
            Ds_Temporal.Tables["Dt_PoligonoBosque"].Clear();
            GrdPolBoque.Rebind();
            TxtAreaIntervenir.Text = "";
            Ds_Temporal.Tables["Dt_PoligonoIntervenir"].Clear();
            GrdPolIntervenir.Rebind();
            TxtAreaProteccion.Text = "";
            TxtPendiente.Text = "";
            TxtPorPendiente.Text = "";
            TxtProfundidad.Text = "";
            TxtPorProfundidad.Text = "";
            TxtPedregosidad.Text = "";
            TxtPorPedregosidad.Text = "";
            TxtAnegamiento.Text = "";
            TxtPorAnegamiento.Text = "";
            TxtBosqueGaleria.Text = "";
            TxtPorBosqueGaleria.Text = "";
            TxtEspeciesProtegidas.Text = "";
            TxtPorEspeciesProtegidas.Text = "";
            TxtEspecifiqueProteccion.Text = "";
            TxtValEspecifiqueProteccion.Text = "";
            TxtPorEspecifiqueProteccion.Text = "";
            Ds_Temporal.Tables["Dt_PoligonoProteccion"].Clear();
            GrdPolProteccion.Rebind();
            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
            if ((SubCategoriaId == 13) || (SubCategoriaId == 10))
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Bosque(1), CboTipoBosque, "Tipo_BosqueId", "Tipo_Bosque");
            else if ((SubCategoriaId == 7) || (SubCategoriaId == 8))
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Bosque(3), CboTipoBosque, "Tipo_BosqueId", "Tipo_Bosque");
            else
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Bosque(2), CboTipoBosque, "Tipo_BosqueId", "Tipo_Bosque");
            ClUtilitarios.AgregarSeleccioneCombo(CboTipoBosque, "Tipo de Bosque");
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoClaseDesarrollo(), CboClaseDesarrollo, "Clase_DesarrolloId", "Clase_Desarrollo");
        }


        void GrabarAreas()
        {
            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
            if (SubCategoriaId == 10)
            {
                if (ValidaAreasPlanSanitario() == true)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    int InmuebleId = Convert.ToInt32(TxtInmuebleId.Text);
                    ClManejo.Eliminar_AreasInmueble(AsignacionId, InmuebleId);
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Especies");
                    XmlNode iElementos = iInformacion.CreateElement("Especie");
                    for (int i = 0; i < GrdEspecies.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                        ClXml.AgregarAtributo("No", i + 1, iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);

                        ClXml.AgregarAtributo("EspecieId", GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);

                    }
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                    ClManejo.Insertar_AreasInmueble_PlanSanitario(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(CboTipoBosque.SelectedValue), iInformacion, Convert.ToDouble(ClUtilitarios.IIf(TxtAreaBosque.Text == "", 0, TxtAreaBosque.Text)));
                    DivGoodAreas.Visible = true;
                    LblGoodAreas.Text = "Áreas grabados correctamente";
                }
                
            }
            else
            {
                if (ValidaAreas() == true)
                {
                    int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                    int InmuebleId = Convert.ToInt32(TxtInmuebleId.Text);
                    ClManejo.Eliminar_PoligonoFinca_Bosque(AsignacionId, InmuebleId);
                    ClManejo.Eliminar_PoligonoFinca_Intervenir(AsignacionId, InmuebleId);
                    ClManejo.Eliminar_PoligonoFinca_Proteccion(AsignacionId, InmuebleId);
                    ClManejo.Eliminar_ClaseDesarrolloFinca_PlanManejo(AsignacionId, InmuebleId);
                    ClManejo.Eliminar_AreasInmueble(AsignacionId, InmuebleId);
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Especies");
                    XmlNode iElementos = iInformacion.CreateElement("Especie");
                    for (int i = 0; i < GrdEspecies.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");

                        ClXml.AgregarAtributo("No", i + 1, iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);

                        ClXml.AgregarAtributo("EspecieId", GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"], iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);

                    }
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                    ClManejo.Insertar_AreasInmueble(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoForestal.Text == "", 0, TxtUsoForestal.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoPorForestal.Text == "", 0, TxtUsoPorForestal.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoAgricultora.Text == "", 0, TxtUsoAgricultora.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoPorAgricultura.Text == "", 0, TxtUsoPorAgricultura.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoGanaderia.Text == "", 0, TxtUsoGanaderia.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoPorGanaderia.Text == "", 0, TxtUsoPorGanaderia.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoAgroForestal.Text == "", 0, TxtUsoAgroForestal.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoPorAgroforestal.Text == "", 0, TxtUsoPorAgroforestal.Text)), TxtUsoOtrosEspecifique.Text, Convert.ToDouble(ClUtilitarios.IIf(TxtUsoOtros.Text == "", 0, TxtUsoOtros.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtUsoPorOtro.Text == "", 0, TxtUsoPorOtro.Text)), Convert.ToInt32(CboTipoBosque.SelectedValue), iInformacion, Convert.ToDouble(ClUtilitarios.IIf(TxtAreaBosque.Text == "", 0, TxtAreaBosque.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtAreaIntervenir.Text == "", 0, TxtAreaIntervenir.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtAreaProteccion.Text == "", 0, TxtAreaProteccion.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPendiente.Text == "", 0, TxtPendiente.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorPendiente.Text == "", 0, TxtPorPendiente.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtProfundidad.Text == "", 0, TxtProfundidad.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorProfundidad.Text == "", 0, TxtPorProfundidad.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPedregosidad.Text == "", 0, TxtPedregosidad.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorPedregosidad.Text == "", 0, TxtPorPedregosidad.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtAnegamiento.Text == "", 0, TxtAnegamiento.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorAnegamiento.Text == "", 0, TxtPorAnegamiento.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtBosqueGaleria.Text == "", 0, TxtBosqueGaleria.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorBosqueGaleria.Text == "", 0, TxtPorBosqueGaleria.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtEspeciesProtegidas.Text == "", 0, TxtEspeciesProtegidas.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorEspeciesProtegidas.Text == "", 0, TxtPorEspeciesProtegidas.Text)), TxtEspecifiqueProteccion.Text, Convert.ToDouble(ClUtilitarios.IIf(TxtValEspecifiqueProteccion.Text == "", 0, TxtValEspecifiqueProteccion.Text)), Convert.ToDouble(ClUtilitarios.IIf(TxtPorEspecifiqueProteccion.Text == "", 0, TxtPorEspecifiqueProteccion.Text)));
                    var collection = CboClaseDesarrollo.CheckedItems;
                    foreach (var item in collection)
                    {
                        ClManejo.Insertar_ClaseDesarrolloFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(item.Value));
                    }

                    string ErrorMapaBosque = "";
                    string ErrorMapaIntervencion = "";
                    string ErrorMapa = "";
                    if (GrdPolBoque.Items.Count > 0)
                    {
                        XmlDocument iInformacionPolBosque = ClXml.CrearDocumentoXML("Poligonos");
                        XmlNode iElementoPoligono = iInformacionPolBosque.CreateElement("Puntos");

                        for (int i = 0; i < GrdPolBoque.Items.Count; i++)
                        {
                            XmlNode iElementoDetalle = iInformacionPolBosque.CreateElement("Item");
                            ClXml.AgregarAtributo("Id", GrdPolBoque.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                            ClXml.AgregarAtributo("X", GrdPolBoque.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                            ClXml.AgregarAtributo("Y", GrdPolBoque.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                            iElementoPoligono.AppendChild(iElementoDetalle);
                        }
                        iInformacionPolBosque.ChildNodes[1].AppendChild(iElementoPoligono);
                        String iPoligonoGML = "";


                        if (!ClPoligono.Actualizar_Poligono_AreaBosque(iInformacionPolBosque, ref AsignacionId, ref InmuebleId, ref iPoligonoGML, ref ErrorMapaBosque))
                            ErrorMapa = "Error Poligono Bosque: " + ErrorMapaBosque;
                    }

                    if (GrdPolIntervenir.Items.Count > 0)
                    {
                        XmlDocument iInformacionPolIntervernir = ClXml.CrearDocumentoXML("Poligonos");
                        XmlNode iElementoPoligono = iInformacionPolIntervernir.CreateElement("Puntos");

                        for (int i = 0; i < GrdPolIntervenir.Items.Count; i++)
                        {
                            XmlNode iElementoDetalle = iInformacionPolIntervernir.CreateElement("Item");
                            ClXml.AgregarAtributo("Id", GrdPolIntervenir.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                            ClXml.AgregarAtributo("X", GrdPolIntervenir.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                            ClXml.AgregarAtributo("Y", GrdPolIntervenir.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                            iElementoPoligono.AppendChild(iElementoDetalle);
                        }
                        iInformacionPolIntervernir.ChildNodes[1].AppendChild(iElementoPoligono);
                        String iPoligonoGML = "";


                        if (!ClPoligono.Actualizar_Poligono_AreaIntervenir(iInformacionPolIntervernir, ref AsignacionId, ref InmuebleId, ref iPoligonoGML, ref ErrorMapaIntervencion))
                            if (ErrorMapa == "")
                                ErrorMapa = "Error poligono Área intervención: " + ErrorMapaBosque;
                            else
                                ErrorMapa = ", error poligono Área intervención: " + ErrorMapaBosque;
                    }

                    if (GrdPolProteccion.Items.Count > 0)
                    {
                        XmlDocument iInformacionPolProteccion = ClXml.CrearDocumentoXML("Poligonos");
                        XmlNode iElementoPoligono = iInformacionPolProteccion.CreateElement("Puntos");

                        for (int i = 0; i < GrdPolProteccion.Items.Count; i++)
                        {
                            XmlNode iElementoDetalle = iInformacionPolProteccion.CreateElement("Item");
                            ClXml.AgregarAtributo("Id", GrdPolProteccion.Items[i].OwnerTableView.DataKeyValues[i]["Id"], iElementoDetalle);
                            ClXml.AgregarAtributo("X", GrdPolProteccion.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                            ClXml.AgregarAtributo("Y", GrdPolProteccion.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                            iElementoPoligono.AppendChild(iElementoDetalle);
                        }
                        iInformacionPolProteccion.ChildNodes[1].AppendChild(iElementoPoligono);
                        String iPoligonoGML = "";


                        if (!ClPoligono.Actualizar_Poligono_AreaProteger(iInformacionPolProteccion, ref AsignacionId, ref InmuebleId, ref iPoligonoGML, ref ErrorMapaIntervencion))
                            if (ErrorMapa == "")
                                ErrorMapa = "Error poligono Área Protección: " + ErrorMapaBosque;
                            else
                                ErrorMapa = ", error poligono Área Protección: " + ErrorMapaBosque;
                    }

                    if (ErrorMapa != "")
                    {
                        ClManejo.Eliminar_PoligonoFinca_Bosque(AsignacionId, InmuebleId);
                        ClManejo.Eliminar_PoligonoFinca_Intervenir(AsignacionId, InmuebleId);
                        ClManejo.Eliminar_PoligonoFinca_Proteccion(AsignacionId, InmuebleId);
                        ClManejo.Eliminar_ClaseDesarrolloFinca_PlanManejo(AsignacionId, InmuebleId);
                        ClManejo.Eliminar_AreasInmueble(AsignacionId, InmuebleId);
                        DivErrAreas.Visible = true;
                        LblErrAreas.Text = ErrorMapa;
                    }
                    else
                    {
                        DivGoodAreas.Visible = true;
                        LblGoodAreas.Text = "Áreas y Polígonos grabados correctamente";
                    }

                }
            }
        }

        void BtnGrabarAreas_Click(object sender, EventArgs e)
        {
            LblGoodAreas.Text = "";
            LblErrAreas.Text = "";
            DivErrAreas.Visible = false;
            DivGoodAreas.Visible = false;
            GrabarAreas();
        }

        void RetornoCaracBiofisicas_PlanManejo()
        {
            DataSet dsDatosCaracBiofisicas = ClManejo.Get_CaracteristicasBiofisicas_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosCaracBiofisicas.Tables["Datos"].Rows.Count > 0)
            {
                TxtAltitud.Text = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Altitud"].ToString();
                TxtTopografia.Text = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Topografia"].ToString();
                TxtSuelos.Text = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Suelos"].ToString();
                TxtHidrografia.Text = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Hidrografia"].ToString();
                CboZonaVida.SelectedValue = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Zona_VidaId"].ToString();
                CboZonaVida.Text = dsDatosCaracBiofisicas.Tables["Datos"].Rows[0]["Zona_Vida"].ToString();
            }
            dsDatosCaracBiofisicas.Clear();
        }

        void RetornoCaminos_PlanManejo()
        {
            DataSet dsDatosRedCaminos = ClManejo.Get_RedCaminos_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosRedCaminos.Tables["Datos"].Rows.Count > 0)
            {
                TxtPrimarioExistente.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Primario_Existente"].ToString();
                TxtPrimarioConstruir.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Primario_Construir"].ToString();
                TxtSecundarioExistente.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Secundario_Existente"].ToString();
                TxtSecundarioConstruir.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Secundario_Construir"].ToString();
                TxtOtroEspecifique.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Otro_especifique"].ToString();
                TxtOtroExistente.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Otro_Existente"].ToString();
                TxtOtroConstruir.Text = dsDatosRedCaminos.Tables["Datos"].Rows[0]["Otro_Construir"].ToString();
            }
            dsDatosRedCaminos.Clear();
        }

        void RetornoPlanCientifico_PlanManejo()
        {
            DataSet dsDatosPlanCientifico = ClManejo.Get_PlanInvestigacion_PlanMenjo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosPlanCientifico.Tables["Datos"].Rows.Count > 0)
            {
                TxtIndice.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Indice"].ToString();
                TxtIntro.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Introduccion"].ToString();
                TxtDelimitacion.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Delimitacion_Tema"].ToString();
                TxtAntecedentes.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Antecedentes"].ToString();
                TxtObjetivos.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Objetivos"].ToString();
                TxtAlcances.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Alcances"].ToString();
                TxtPlanteamiento.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Planteamiento"].ToString();
                TxtJustificacion.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Justificacion"].ToString();
                TxtMetodologia.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Metodologia"].ToString();
                TxtRecursos.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Recursos"].ToString();
                TxtResultados.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Resultados"].ToString();
                TxtCronograma.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Cronograma"].ToString();
                TxtBibliografia.Text = dsDatosPlanCientifico.Tables["Datos"].Rows[0]["Bibliografia"].ToString();
            }
            dsDatosPlanCientifico.Clear();
        }

        void RetornoCalculosCompromisoForestal()
        {
            DataSet dsDatosCalculosCompromiso = ClManejo.Get_CalculosCompromisoForestal(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosCalculosCompromiso.Tables["Datos"].Rows.Count > 0)
            {
                if (dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaBasal_Extrae"] != "")
                    TxtAreaBasalExtrae.Text = dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaBasal_Extrae"].ToString();
                if (dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaBasalExistente"] != "")
                    TxtAreaBasalExis.Text = dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaBasalExistente"].ToString();
                if (dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaTotalIntervenir"] != "")
                    TxtAreaTotIntervenir.Text = dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaTotalIntervenir"].ToString();
                if (dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaCompromiso"] != "")
                    TxtAreaCompromiso.Text = dsDatosCalculosCompromiso.Tables["Datos"].Rows[0]["AreaCompromiso"].ToString();

            }
            dsDatosCalculosCompromiso.Clear();
        }

        void RetornoProteccionForestal_PlanManejo()
        {
            DataSet dsDatosPlanProteccionForestal = ClManejo.Get_ProteccionForestal(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosPlanProteccionForestal.Tables["Datos"].Rows.Count > 0)
            {
                TxtMedidasPrevencion.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Medida_Prevencion"].ToString();
                TxtPrevencionControlPlaga.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Prevencion_ControlPlagas"].ToString();
                TxtJustificacionPf.Text   = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Justificacion_PrevencionPF"].ToString();
                TxtLineasControlRonda.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Linea_Control_Ronda"].ToString();
                TxtVigilanciaPuestoPunto.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Vigilancia_Puesto_Control"].ToString();
                TxtManejoCombustible.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Manejo_Combustibles"].ToString();
                TxtIdentificacionAreaCritica.Text   = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Identificacion_Area_Critica"].ToString();
                TxtRespuestaCasoIf.Text =  dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Respuesta_CasoIF"].ToString();
                TxtJustificacionPrevencionIF.Text =  dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Justificacion_PrevencionIF"].ToString();
                TxtMonitoreoPlaga.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Monitoreo_Plaga_Forestal"].ToString();
                TxtControlPlaga.Text = dsDatosPlanProteccionForestal.Tables["Datos"].Rows[0]["Control_Plaga_Forestal"].ToString();
            }
            dsDatosPlanProteccionForestal.Clear();
        }

        void RetornoPlagas_PlanManejo()
        {
            DataSet dsDatosPlagas = ClManejo.Get_Plaga_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosPlagas.Tables["Datos"].Rows.Count > 0)
            {
                TxtAgenteCausal.Text = dsDatosPlagas.Tables["Datos"].Rows[0]["Agente_Causal"].ToString();
                TxtSintomologia.Text = dsDatosPlagas.Tables["Datos"].Rows[0]["Sintomatologia"].ToString();
                TxtDano.Text = dsDatosPlagas.Tables["Datos"].Rows[0]["DescripcionDano"].ToString();
                TxtFenomenoNatural.Text = dsDatosPlagas.Tables["Datos"].Rows[0]["Fenomeno_Natural"].ToString();
                TxtEstimacionMadera.Text = dsDatosPlagas.Tables["Datos"].Rows[0]["Estimacion_Madera"].ToString();
            }
            dsDatosPlagas.Clear();
        }

        void RetornoMedidasControl_PlanManejo()
        {
            DataSet dsDatosMedidasControl = ClManejo.Get_MedidaControl_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosMedidasControl.Tables["Datos"].Rows.Count > 0)
            {
                TxtDescripcionMedidas.Text = dsDatosMedidasControl.Tables["Datos"].Rows[0]["DescripcionMedidas"].ToString();
                Txttratamiento.Text = dsDatosMedidasControl.Tables["Datos"].Rows[0]["Tratamiento"].ToString();
                TxtResiduos.Text = dsDatosMedidasControl.Tables["Datos"].Rows[0]["Manejo_Residuos"].ToString();
            }
            dsDatosMedidasControl.Clear();
        }

        void RetornoAreaProteccion()
        {
            DataSet dsDatosProteccion = ClManejo.Get_Repoblacion_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosProteccion.Tables["Datos"].Rows.Count > 0)
            {
                txtDensidadIni.Text = dsDatosProteccion.Tables["Datos"].Rows[0]["DensidadIni"].ToString();
            }
            dsDatosProteccion.Clear();
            DataSet dsDatosEspecies = ClManejo.Especies_Accion_Repoblacion_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsDatosEspecies.Tables["Datos"].Rows.Count; i++)
            {
                AgregaEspecieCargadaAccionRepo(Convert.ToInt32(dsDatosEspecies.Tables["Datos"].Rows[i]["EspecieId"]), dsDatosEspecies.Tables["Datos"].Rows[i]["Nombre_Cientifico"].ToString());
            }
            dv = Ds_Temporal.Tables["Dt_EspecieRepo"].DefaultView;
            GrdEspecieRepo.Rebind();
            dsDatosEspecies.Clear();

        }

        void RetornoInfoGeneral_PlanManejo()
        {
            DataSet dsDatosInfoGeneral = ClManejo.Get_InfoGeneral_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosInfoGeneral.Tables["Datos"].Rows.Count > 0)
            {
                TxtTiempoPlanManejo.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["TiempoEjecucion"].ToString();
                TxtTiempoExtraccion.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["TiempoExtraccion"].ToString();
                TxtVolExtraer.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["VolExtraer"].ToString();
                TxtSistemaCorta.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["SistemaCorta"].ToString();
                TxtIncrementoAnual.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["IncrementoAnual"].ToString();
                TxtCortaAnual.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["CortaAnual"].ToString();
                CboTipoGarantia.SelectedValue = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["Tipo_GarantiaId"].ToString();
                CboTipoGarantia.Text = dsDatosInfoGeneral.Tables["Datos"].Rows[0]["Tipo_Garantia"].ToString();
            }
            dsDatosInfoGeneral.Clear();
            DataSet dsDatosEspecies = ClManejo.EspeciesInfoGeneral_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsDatosEspecies.Tables["Datos"].Rows.Count; i++)
            {
                AgregaEspecieCargadaPlanManejo(Convert.ToInt32(dsDatosEspecies.Tables["Datos"].Rows[i]["EspecieId"]), dsDatosEspecies.Tables["Datos"].Rows[i]["Nombre_Cientifico"].ToString());
            }
            dv = Ds_Temporal.Tables["Dt_EspecieArb"].DefaultView;
            GrdEspeciePLanManejo.Rebind();
            dsDatosEspecies.Clear();
            DataSet dsDatosSistemaRepoblacion = ClManejo.Get_SistemaRepoblacionPlanManejo(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsDatosSistemaRepoblacion.Tables["Datos"].Rows.Count; i++)
            {
                for (int j = 1; j < CboSistemaRepoblacion.Items.Count; j++)
                {
                    if (Convert.ToInt32(CboSistemaRepoblacion.Items[j].Value) == Convert.ToInt32(dsDatosSistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacioId"]))
                    {
                        CboSistemaRepoblacion.Items[j].Checked = true;
                    }
                }
            }
            dsDatosSistemaRepoblacion.Clear();
            DataSet dsDatosPuntosAreaRepoblacion = ClManejo.obtener_puntos_poligonos_Area_Repoblacion(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsDatosPuntosAreaRepoblacion.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].NewRow();
                rowNew["Id"] = dsDatosPuntosAreaRepoblacion.Tables["Datos"].Rows[i]["Id"];
                rowNew["X"] = dsDatosPuntosAreaRepoblacion.Tables["Datos"].Rows[i]["Punto_X"];
                rowNew["Y"] = dsDatosPuntosAreaRepoblacion.Tables["Datos"].Rows[i]["Punto_Y"];
                Ds_Temporal.Tables["Dt_PoligonoAreaRepo"].Rows.Add(rowNew);
            }
            GrdPolAreaRepo.Rebind();
            dsDatosPuntosAreaRepoblacion.Clear();

            if (TxtSubCategoria.Text == "14")
            {
                DataSet DsDatosCambioUso = ClManejo.Get_DatosCambioUso(Convert.ToInt32(TxtAsignacionId.Text));
                if (DsDatosCambioUso.Tables["Datos"].Rows.Count > 0)
                {
                    CboCambioUsoForestal.SelectedValue = DsDatosCambioUso.Tables["Datos"].Rows[0]["TipoCambioUsoId"].ToString();
                    CboCambioUsoForestal.Text = DsDatosCambioUso.Tables["Datos"].Rows[0]["TipoCambioUso"].ToString();
                    TxtCambioUsoEspecifique.Text = DsDatosCambioUso.Tables["Datos"].Rows[0]["Especifique"].ToString();
                }
            }

        }

       

        void RetornoAreas()
        {
            DataSet dsDatosAreas = ClManejo.GetAreasFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
            if (dsDatosAreas.Tables["Datos"].Rows.Count > 0)
            {
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
                if (SubCategoriaId == 10)
                {
                    CboTipoBosque.SelectedValue = dsDatosAreas.Tables["Datos"].Rows[0]["Tipo_BosqueId"].ToString();
                    CboTipoBosque.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Tipo_Bosque"].ToString();
                    TxtAreaBosque.Text = dsDatosAreas.Tables["Datos"].Rows[0]["AreaBosque"].ToString();
                    dsDatosAreas.Clear();
                    DataSet dsDatosEspecies = ClManejo.EspeciesFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosEspecies.Tables["Datos"].Rows.Count; i++)
                    {
                        AgregaEspecieCargada(Convert.ToInt32(dsDatosEspecies.Tables["Datos"].Rows[i]["EspecieId"]), dsDatosEspecies.Tables["Datos"].Rows[i]["Nombre_Cientifico"].ToString());
                    }
                    dv = Ds_Temporal.Tables["Dt_InventarioSAF"].DefaultView;
                    GrdEspecies.Rebind();
                    dsDatosEspecies.Clear();
                }
                else
                {
                    TxtUsoForestal.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Forestal"].ToString();
                    TxtUsoPorForestal.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorForestal"].ToString();
                    TxtUsoAgricultora.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Agricultura"].ToString();
                    TxtUsoPorAgricultura.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorAgricultura"].ToString();
                    TxtUsoGanaderia.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Ganaderia"].ToString();
                    TxtUsoPorGanaderia.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorGanaderia"].ToString();
                    TxtUsoAgroForestal.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Agroforestal"].ToString();
                    TxtUsoPorAgroforestal.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorAgroforestal"].ToString();
                    TxtUsoOtrosEspecifique.Text = dsDatosAreas.Tables["Datos"].Rows[0]["OtroEspecifique"].ToString();
                    TxtUsoOtros.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Otro"].ToString();
                    TxtUsoPorOtro.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorOtro"].ToString();
                    CboTipoBosque.SelectedValue = dsDatosAreas.Tables["Datos"].Rows[0]["Tipo_BosqueId"].ToString();
                    CboTipoBosque.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Tipo_Bosque"].ToString();
                    TxtAreaBosque.Text = dsDatosAreas.Tables["Datos"].Rows[0]["AreaBosque"].ToString();
                    TxtAreaIntervenir.Text = dsDatosAreas.Tables["Datos"].Rows[0]["AreaIntervenir"].ToString();
                    TxtAreaProteccion.Text = dsDatosAreas.Tables["Datos"].Rows[0]["AreaProteccion"].ToString();
                    TxtPendiente.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Pendiente"].ToString();
                    TxtPorPendiente.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorPendiente"].ToString();
                    TxtProfundidad.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Profundidad"].ToString();
                    TxtPorProfundidad.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorProfundidad"].ToString();
                    TxtPedregosidad.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Pedregosidad"].ToString();
                    TxtPorPedregosidad.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorPedregosidad"].ToString();
                    TxtAnegamiento.Text = dsDatosAreas.Tables["Datos"].Rows[0]["Anegamiento"].ToString();
                    TxtPorAnegamiento.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorAnegamiento"].ToString();
                    TxtBosqueGaleria.Text = dsDatosAreas.Tables["Datos"].Rows[0]["BosqueGaleria"].ToString();
                    TxtPorBosqueGaleria.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorBosqueGaleria"].ToString();
                    TxtEspeciesProtegidas.Text = dsDatosAreas.Tables["Datos"].Rows[0]["EspecieProtegidas"].ToString();
                    TxtPorEspeciesProtegidas.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorEspeciesProtegidas"].ToString();
                    TxtEspecifiqueProteccion.Text = dsDatosAreas.Tables["Datos"].Rows[0]["OtroProteccionEspecifique"].ToString();
                    TxtValEspecifiqueProteccion.Text = dsDatosAreas.Tables["Datos"].Rows[0]["OtroProteccion"].ToString();
                    TxtPorEspecifiqueProteccion.Text = dsDatosAreas.Tables["Datos"].Rows[0]["PorOtroProteccion"].ToString();
                    dsDatosAreas.Clear();
                    DataSet dsDatosClaseDesarrollo = ClManejo.GetClaseDesarrolloFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosClaseDesarrollo.Tables["Datos"].Rows.Count; i++)
                    {
                        for (int j = 0; j < CboClaseDesarrollo.Items.Count; j++)
                        {
                            if (Convert.ToInt32(CboClaseDesarrollo.Items[j].Value) == Convert.ToInt32(dsDatosClaseDesarrollo.Tables["Datos"].Rows[i]["Clase_DesarrolloId"]))
                            {
                                CboClaseDesarrollo.Items[j].Checked = true;
                            }
                        }
                    }
                    dsDatosClaseDesarrollo.Clear();
                    DataSet dsDatosEspecies = ClManejo.EspeciesFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosEspecies.Tables["Datos"].Rows.Count; i++)
                    {
                        AgregaEspecieCargada(Convert.ToInt32(dsDatosEspecies.Tables["Datos"].Rows[i]["EspecieId"]), dsDatosEspecies.Tables["Datos"].Rows[i]["Nombre_Cientifico"].ToString());
                    }
                    dv = Ds_Temporal.Tables["Dt_InventarioSAF"].DefaultView;
                    GrdEspecies.Rebind();
                    dsDatosEspecies.Clear();

                    DataSet dsDatosPuntosAreaBosque = ClManejo.obtener_puntos_poligonos_AreaBosque(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosPuntosAreaBosque.Tables["Datos"].Rows.Count; i++)
                    {
                        DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoBosque"].NewRow();
                        rowNew["Id"] = dsDatosPuntosAreaBosque.Tables["Datos"].Rows[i]["Id"];
                        rowNew["X"] = dsDatosPuntosAreaBosque.Tables["Datos"].Rows[i]["Punto_X"];
                        rowNew["Y"] = dsDatosPuntosAreaBosque.Tables["Datos"].Rows[i]["Punto_Y"];
                        Ds_Temporal.Tables["Dt_PoligonoBosque"].Rows.Add(rowNew);
                    }
                    GrdPolBoque.Rebind();
                    dsDatosPuntosAreaBosque.Clear();

                    DataSet dsDatosPuntosAreaIntervencion = ClManejo.obtener_puntos_poligonos_AreaIntervencion(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosPuntosAreaIntervencion.Tables["Datos"].Rows.Count; i++)
                    {
                        DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoIntervenir"].NewRow();
                        rowNew["Id"] = dsDatosPuntosAreaIntervencion.Tables["Datos"].Rows[i]["Id"];
                        rowNew["X"] = dsDatosPuntosAreaIntervencion.Tables["Datos"].Rows[i]["Punto_X"];
                        rowNew["Y"] = dsDatosPuntosAreaIntervencion.Tables["Datos"].Rows[i]["Punto_Y"];
                        Ds_Temporal.Tables["Dt_PoligonoIntervenir"].Rows.Add(rowNew);
                    }
                    GrdPolIntervenir.Rebind();
                    dsDatosPuntosAreaIntervencion.Clear();

                    DataSet dsDatosPuntosAreaProteccion = ClManejo.obtener_puntos_poligonos_AreaIntervencion(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                    for (int i = 0; i < dsDatosPuntosAreaProteccion.Tables["Datos"].Rows.Count; i++)
                    {
                        DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoProteccion"].NewRow();
                        rowNew["Id"] = dsDatosPuntosAreaProteccion.Tables["Datos"].Rows[i]["Id"];
                        rowNew["X"] = dsDatosPuntosAreaProteccion.Tables["Datos"].Rows[i]["Punto_X"];
                        rowNew["Y"] = dsDatosPuntosAreaProteccion.Tables["Datos"].Rows[i]["Punto_Y"];
                        Ds_Temporal.Tables["Dt_PoligonoProteccion"].Rows.Add(rowNew);
                    }
                    GrdPolProteccion.Rebind();
                    dsDatosPuntosAreaProteccion.Clear();
                }
                
            }
            

        }

        void RetornoPlanificacionManejo_Forestal_PlanManejo()
        {
            DataSet DatosPlanificacion = ClManejo.Get_Datos_PlanificacionManejo(Convert.ToInt32(TxtAsignacionId.Text));
            if (DatosPlanificacion.Tables["Datos"].Rows.Count > 0)
            {
                CboCriterioReg.SelectedValue = DatosPlanificacion.Tables["Datos"].Rows[0]["Criterio_RegulacionId"].ToString();
                CboCriterioReg.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["Creiterio_Regulacion"].ToString();
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Formula_Manejo(Convert.ToInt32(CboCriterioReg.SelectedValue)), CboFormula, "Formulaid", "Formula");
                CboFormula.SelectedValue = DatosPlanificacion.Tables["Datos"].Rows[0]["FormulaId"].ToString();
                CboFormula.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["Formula"].ToString();
                TxtCap.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["Cap"].ToString();
                TxtJustificacionFormula.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["Justificacion"].ToString();
                TxtActividadesApro.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["ActividadAprovechamiento"].ToString();
                TxtObjetivosRecuperacion.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["ObjetivoRecuperacion"].ToString();
                TxtJustificacionEspecies.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["JutificacionEspecie"].ToString();
                TxtSistemaRepo.Text = DatosPlanificacion.Tables["Datos"].Rows[0]["SistemaRepoblacionDesc"].ToString();
            }
            DatosPlanificacion.Clear();
        }

        void RetornoAprovechamiento_Forestal_PlanManejo()
        {
            DataSet dsDatosAprovechamiento = ClManejo.Get_Aprovechamiento_Forestal(Convert.ToInt32(TxtAsignacionId.Text));
            if (dsDatosAprovechamiento.Tables["Datos"].Rows.Count > 0)
            {
                CboTipoIngresoDatos.SelectedValue = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Tipo_Ingreso_DatosId"].ToString();
                CboTipoIngresoDatos.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Tipo_Ingreso_Datos"].ToString();
                if (dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Tipo_Ingreso_DatosId"].ToString() == "1")
                {
                    BtnGeneraCalculos.Visible = false;
                    DivOtraEcuacion.Visible = true;
                    LblEcuacion.Text = "Otras ecuaciones";
                }

                else
                {
                    BtnGeneraCalculos.Visible = true;
                    DivOtraEcuacion.Visible = true;
                    LblEcuacion.Text = "Ingrese ecuaciones utilizadas";
                }
                CboTipoInventario.SelectedValue = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Tipo_InventarioId"].ToString();
                CboTipoInventario.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Tipo_Inventario"].ToString();
                TxtDatosRegresion.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["Datos_Regresion"].ToString();
                TxtDiametroMinimo.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["DiametroMin"].ToString();
                TxtTotRodal.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["TotRodal"].ToString();
                TxtOtraEcuacion.Text = dsDatosAprovechamiento.Tables["Datos"].Rows[0]["OtraEcuacion"].ToString();
                
                

            }
            dsDatosAprovechamiento.Clear();

            DataSet dsDatosEcuacion = ClManejo.Get_Ecuacion_Aprovechamiento_Forestal(Convert.ToInt32(TxtAsignacionId.Text));
            for (int i = 0; i < dsDatosEcuacion.Tables["Datos"].Rows.Count; i++)
            {
                for (int j = 1; j < CboEcuacion.Items.Count; j++)
                {
                    if (Convert.ToInt32(CboEcuacion.Items[j].Value) == Convert.ToInt32(dsDatosEcuacion.Tables["Datos"].Rows[i]["EcuacionId"]))
                    {
                        CboEcuacion.Items[j].Checked = true;
                    }
                }
            }
            dsDatosEcuacion.Clear();
            ClUtilitarios.LlenaCombo(ClManejo.Get_Turno_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text)),CboTurno,"Turno","Turno");
            ClUtilitarios.AgregarSeleccioneCombo(CboTurno, "Turno");    
        }

        bool ValidaAreas()
        {
            LblErrAreas.Text = "";
            DivErrAreas.Visible = false;
            bool HayError = false;
            var collection = CboClaseDesarrollo.CheckedItems;

            if (CboTipoBosque.SelectedValue == "")
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Debe seleccionar el tipo de bosque";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", Debe seleccionar el tipo de bosque";
                HayError = true;
            }
            if (collection.Count == 0)
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Debe seleccionar al menos una clase de desarrollo";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", Debe seleccionar al menos una clase de desarrollo";
                HayError = true;
            }
            if (GrdEspecies.Items.Count == 0)
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Debe ingresar al menos una especie";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", Debe ingresar al menos una especie";
                HayError = true;
            }
            if (GrdPolBoque.Items.Count == 0)
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "No ha ingresado el poligono del área del bosque";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", No ha ingresado el poligono del área del bosque";
                HayError = true;
            }
            if (GrdPolIntervenir.Items.Count == 0)
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "No ha ingresado el poligono del área a intervenir";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", No ha ingresado el poligono del área a intervenir";
                HayError = true;
            }
            //if (TxtAreaProteccion.Text == "")
            //{
            //    if (LblErrAreas.Text == "")
            //        LblErrAreas.Text = LblErrAreas.Text + "No ha ingresado ningun valor en Área de protección";
            //    else
            //        LblErrAreas.Text = LblErrAreas.Text + ", No ha ingresado ningun valor en Área de protección";
            //    HayError = true;
            //}
            if (((TxtPendiente.Text != "") && (TxtPendiente.Text != "0")) || ((TxtProfundidad.Text != "") && (TxtProfundidad.Text != "0")) || ((TxtPedregosidad.Text != "") && (TxtPedregosidad.Text != "0")) || ((TxtAnegamiento.Text != "") && (TxtAnegamiento.Text != "0")) || ((TxtBosqueGaleria.Text != "") && (TxtBosqueGaleria.Text != "0")) || ((TxtEspeciesProtegidas.Text != "") && (TxtEspeciesProtegidas.Text != "0")) || ((TxtValEspecifiqueProteccion.Text != "") && (TxtValEspecifiqueProteccion.Text != "0")) && (GrdPolProteccion.Items.Count == 0))
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "No ha ingresado el poligono del área de protección";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", No ha ingresado el poligono del área de protección";
                HayError = true;
            }
            if ((TxtAreaIntervenir.Text != "") && (TxtAreaBosque.Text != ""))
            {
                if (Convert.ToDouble(TxtAreaIntervenir.Text) > Convert.ToDouble(TxtAreaBosque.Text))
                {
                    if (LblErrAreas.Text == "")
                        LblErrAreas.Text = LblErrAreas.Text + "El área a intervenir no puede ser mayor al área de bosque";
                    else
                        LblErrAreas.Text = LblErrAreas.Text + ", el área a intervenir no puede ser mayor al área de bosque";
                    HayError = true;
                }
            }
            if ((TxtAreaBosque.Text != "") && (TxtUsoForestal.Text != ""))
            {
                if (Convert.ToDouble(TxtAreaBosque.Text) > Convert.ToDouble(TxtUsoForestal.Text))
                {
                    if (LblErrAreas.Text == "")
                        LblErrAreas.Text = LblErrAreas.Text + "El área de bosque no puede ser mayor al área forestal";
                    else
                        LblErrAreas.Text = LblErrAreas.Text + ", el área de bosque no puede ser mayor al área forestal";
                    HayError = true;
                }
            }
            double AreaIntervernir = 0;
            double AreaProteccion = 0;
            double Resul = 0;
            if (TxtAreaIntervenir.Text != "")
                AreaIntervernir = Convert.ToDouble(TxtAreaIntervenir.Text);
            if (TxtAreaProteccion.Text != "")
                AreaProteccion = Convert.ToDouble(TxtAreaProteccion.Text);
            Resul = AreaIntervernir + AreaProteccion;
            if (Resul > Convert.ToDouble(TxtAreaBosque.Text))
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Las sumas de las Áreas a intervenir y el Área de Protección es mayor al Área con bosque";
                    else
                    LblErrAreas.Text = LblErrAreas.Text + ", las sumas de las Áreas a intervenir y el Área de Protección es mayor al Área con bosque";
                    HayError = true;
            }
            if (HayError == true)
            {
                DivErrAreas.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ValidaAreasPlanSanitario()
        {
            LblErrAreas.Text = "";
            DivErrAreas.Visible = false;
            bool HayError = false;

            if (CboTipoBosque.SelectedValue == "")
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Debe seleccionar el tipo de bosque";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", Debe seleccionar el tipo de bosque";
                HayError = true;
            }
            if (GrdEspecies.Items.Count == 0)
            {
                if (LblErrAreas.Text == "")
                    LblErrAreas.Text = LblErrAreas.Text + "Debe ingresar al menos una especie";
                else
                    LblErrAreas.Text = LblErrAreas.Text + ", Debe ingresar al menos una especie";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrAreas.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnGrabarNomEmpresa_ServerClick(object sender, EventArgs e)
        {
            ClManejo.ActualizarNombre_EmpresaTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text),TxtNombreEmpresaSocial.Text);
        }

        void BtnAddFincaPlan_ServerClick(object sender, EventArgs e)
        {
            OcultaMensajes();
            if (CboFinca.SelectedValue == "")
            {
                DivErrFinca.Visible = true;
                LblErrFinca.Text = "Debe seleccionar la finca";
                
            }
            else
            {
                ClManejo.InsertTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(CboFinca.SelectedValue));
                LblGoodFinca.Text = "Finca agregada al plan de manejo";
                DivGoodFinca.Visible = true;
                ClUtilitarios.LlenaCombo(ClInmueble.Inmueble_GetAll_Manejo(Convert.ToInt32(TxtUsrOwnPlan.Text), Convert.ToInt32(TxtAsignacionId.Text)), CboFinca, "InmuebleId", "Finca");
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

        void GrdInmuebles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClManejo.EliminarTempPropietariosPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                ClManejo.EliminarTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]));
                LblGoodFinca.Text = "Finca eliminada del plan de manejo";
                DivGoodFinca.Visible = true;
                ClUtilitarios.LlenaCombo(ClInmueble.Inmueble_GetAll_Manejo(Convert.ToInt32(TxtUsrOwnPlan.Text), Convert.ToInt32(TxtAsignacionId.Text)), CboFinca, "InmuebleId", "Finca");
                ClUtilitarios.AgregarSeleccioneCombo(CboFinca, "Finca/Inmueble");
                if (CboFinca.Items.Count > 1)
                    BtnAddFincaPlan.Visible = true;
                else
                    BtnAddFincaPlan.Visible = false;
                GrdInmuebles.Rebind();
                BloquearFinca();
                LimpiarFinca();
                int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
                int InmuebleId = Convert.ToInt32(TxtInmuebleId.Text);
                ClManejo.Eliminar_PoligonoFinca_Bosque(AsignacionId, InmuebleId);
                ClManejo.Eliminar_PoligonoFinca_Intervenir(AsignacionId, InmuebleId);
                ClManejo.Eliminar_PoligonoFinca_Proteccion(AsignacionId, InmuebleId);
                ClManejo.Eliminar_ClaseDesarrolloFinca_PlanManejo(AsignacionId, InmuebleId);
                ClManejo.Eliminar_AreasInmueble(AsignacionId, InmuebleId);
                ClManejo.Eliminar_Propietarios_Finca(AsignacionId, InmuebleId);
                LimpiarAreas();
                DivPropietariosFinca.Visible = false;
                DivUsosAreas.Visible = false;
            }
            if (e.CommandName == "CmdPropietarios")
            {
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                DivPropietariosFinca.Visible = true;
                PropietariosYaExistentes(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
                GrdPropietarios.Rebind();
                DataSet dsTipoPersona = ClManejo.GetTipoPersonaTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text));
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
                    int TipoDocPropiedad = ClCatalogos.Get_TipoPropietarioInmueble(1,Convert.ToInt32(TxtInmuebleId.Text),Convert.ToInt32(TxtAsignacionId.Text));
                    if (TipoDocPropiedad == 1)
                        TitPropietarios.Text = "Propietarios";
                    else
                        TitPropietarios.Text = "Titulares";
                }
                else
                {
                    ClUtilitarios.LlenaCombo(ClCatalogos.Tipo_Persona(), CboTipoPersona, "Tipo_PersonaId", "Tipo_Persona");
                    ClUtilitarios.AgregarSeleccioneCombo(CboTipoPersona, "Tipo Persona");
                    DivPropietarios.Visible = false;
                    DivJuridica.Visible = false;
                }
                
            }
            if (e.CommandName == "CmdAreas")
            {
                TxtInmuebleId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString();
                DivUsosAreas.Visible = true;
                TxtAreaInmueble.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Area"].ToString();
                LimpiarAreas();
                RetornoAreas();
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text),1);
                if (SubCategoriaId == 10)
                {
                    DivAreaForestal.Visible = false;
                    DivAreaAgricultura.Visible = false;
                    DivAreaGanaderia.Visible = false;
                    DivAreaAgroforestal.Visible = false;
                    DivAreaOtros.Visible = false;
                    DivAreaClaseDesarrollo.Visible = false;
                    LblAreaBosque.InnerText = "Área de Bosque  dañada  (ha): ";
                    DivPoligonoAraBosque.Visible = false;
                    DivAreaIntervenir.Visible = false;
                    DivPolAreaIntervenir.Visible = false;
                    DivAreaProteccion.Visible = false;
                    DivAreaPendiente.Visible = false;
                    DivAreaProfundidad.Visible = false;
                    DivAreaPedrogosidad.Visible = false;
                    DivAreaAnegamiento.Visible = false;
                    DivAreaBosqueGalaria.Visible = false;
                    DivAreaEspeciesProtegidas.Visible = false;
                    DivAreaOtrosProteccion.Visible = false;
                    DivPoligonoProteccion.Visible = false;
                    DivSeparador1.Visible = false;
                    DivSeparador2.Visible = false;
                    DivSeparador3.Visible = false;
                    DivSeparador4.Visible = false;
                    DivSeparador5.Visible = false;
                    DivSeparador6.Visible = false;
                    DivSeparador7.Visible = false;
                    DivSeparador8.Visible = false;
                    DivSeparador9.Visible = false;
                    DivSeparador10.Visible = false;
                    DivSeparador11.Visible = false;
                    DivSeparador12.Visible = false;
                    DivSeparador13.Visible = false;
                    DivSeparador14.Visible = false;
                    DivSeparador15.Visible = false;
                    DivSeparador16.Visible = false;
                    DivSeparador17.Visible = false;

                }
            }
            if (e.CommandName == "CmdPolFinca")
            {
                int Id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]);
                string url = "";
                //url =  "/Segefor_new/Mapas/MenuMapas.aspx?Id=" + Id;
                string RutaMapa = System.Configuration.ConfigurationManager.AppSettings["SitioMapas"];
                url = RutaMapa + "/MenuMapas.aspx?Id=" + Id + "&typecarte=1";
                string popupScript = "window.open('" + url + "', 'popup_window', 'left=100,top=100,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", popupScript, true);
            }
        }

        void GrdInmuebles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_TempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text)), GrdInmuebles);
        }

        void CboDepartamento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
        }

        void BtnNuevaFinca_ServerClick(object sender, EventArgs e)
        {
            OcultaMensajes();
            LimpiarFinca();
            DesbloqueFinca();
            TxtInmuebleId.Text = "";
            DivPropietariosFinca.Visible = false;
        }

        void btnGrabarFinca_Click(object sender, EventArgs e)
        {
            if (ValidaFinca() == true)
            {
                bool GraboFinca = true;
                int AreaId = 0;
                if (CboArea.SelectedValue != "")
                    AreaId = Convert.ToInt32(CboArea.SelectedValue);
                int InmuebleId = ClInmueble.Max_Inmueble();
                ClUsuario.Insertar_Actividad_Pagina(16, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                ClInmueble.Inserta_InmuebleT(InmuebleId, Convert.ToInt32(TxtUsrOwnPlan.Text), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), TxtFolio.Text , Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDouble(TxtArea.Text), Convert.ToDouble(TxtUbicacionNorte.Text), Convert.ToDouble(TxtUbicacionOeste.Text), TxtColNorte.Text,TxtColSur.Text, TxtColEste.Text, TxtColOeste.Text);
                if (RadUploadFile.UploadedFiles.Count > 0)
                {
                    Stream fileStream = RadUploadFile.UploadedFiles[0].InputStream;
                    byte[] attachmentBytes = new byte[fileStream.Length];
                    fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                    ClInmueble.Actualiza_Archivo(InmuebleId, attachmentBytes, RadUploadFile.UploadedFiles[0].ContentType, RadUploadFile.UploadedFiles[0].FileName);
                    fileStream.Close();
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
                    string ErrorMapa = "";
                    if (ClPoligono.actualizar_poligonos_finca_new(iInformacionPol, ref InmuebleId, ref ErrorMapa))
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
                    if (GraboFinca == true)
                    {
                        ClManejo.InsertTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), InmuebleId);
                        GrdInmuebles.Rebind();
                        DivGoodFinca.Visible = true;
                        LblGoodFinca.Text = "Finca/Inmueble agregado correctamente";
                        BloquearFinca();
                        LimpiarFinca();
                    }
                }
                

            }
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
            TxtAldea.Text =  "";
            CboDepartamento.SelectedValue = "";
            CboMunicipio.SelectedValue = "";
            TxtColNorte.Text = "";
            TxtColSur.Text = "";
            TxtColEste.Text = "";
            TxtColOeste.Text = "";
            TxtArea.Text = "";
            UploadPolFinca.UploadedFiles.Clear();
            Ds_Temporal.Tables["Dt_Poligono"].Clear();
            GrdPoligono.Rebind();
            DivPropietariosFinca.Visible = false;
            
        }

        void BloquearFinca()
        {
            ChkIngNomFinca.Enabled = false;
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
            CboDepartamento.Enabled = false;
            CboMunicipio.Enabled = false;
            TxtColNorte.Enabled = false;
            TxtColSur.Enabled = false;
            TxtColEste.Enabled = false;
            TxtColOeste.Enabled = false;
            TxtArea.Enabled = false;
            UploadPolFinca.Enabled = false;
            OptAreasPro.Enabled = false;
            btnGrabarFinca.Visible = false;
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
            CboDepartamento.Enabled = true;
            CboMunicipio.Enabled = true;
            TxtColNorte.Enabled = true;
            TxtColSur.Enabled = true;
            TxtColEste.Enabled = true;
            TxtColOeste.Enabled = true;
            TxtArea.Enabled = true;
            UploadPolFinca.Enabled = true;
            OptAreasPro.Enabled = true;
            btnGrabarFinca.Visible = true;
        }



        void CargaDatosFinca(int InmuebleId)
        {
            DataSet DsInmueble = ClInmueble.Inmueble_Get(InmuebleId);
            TxtFinca.Text = DsInmueble.Tables["Datos"].Rows[0]["Finca"].ToString();
            TxtUbicacionNorte.Text = DsInmueble.Tables["Datos"].Rows[0]["Gtm_Norte"].ToString();
            TxtUbicacionOeste.Text = DsInmueble.Tables["Datos"].Rows[0]["Gtm_Oeste"].ToString();
            CboTipoDocumento.Text = DsInmueble.Tables["Datos"].Rows[0]["TipoDocPropiedad"].ToString();
            CboTipoDocumento.SelectedValue = DsInmueble.Tables["Datos"].Rows[0]["TipoDoc_PropiedadId"].ToString();
            if (CboTipoDocumento.SelectedValue == "1")
            {
                TxtNoFinca.Text = DsInmueble.Tables["Datos"].Rows[0]["NoFinca"].ToString();
                TxtFolio.Text = DsInmueble.Tables["Datos"].Rows[0]["Folio"].ToString();
                TxtLibro.Text = DsInmueble.Tables["Datos"].Rows[0]["Libro"].ToString();
                TxtDe.Text = DsInmueble.Tables["Datos"].Rows[0]["De"].ToString();
            }
            else if (CboTipoDocumento.SelectedValue == "2")
            {
                TxtNoCerti.Text = DsInmueble.Tables["Datos"].Rows[0]["NoCertificacion"].ToString();
                TxtMunEmiteDoc.Text = DsInmueble.Tables["Datos"].Rows[0]["Municipalidad"].ToString();
            }
            else if (CboTipoDocumento.SelectedValue == "3")
            {
                TxtNoEscritura.Text = DsInmueble.Tables["Datos"].Rows[0]["NoEscritura"].ToString();
                CboTitulo.Text = DsInmueble.Tables["Datos"].Rows[0]["TituloNotario"].ToString();
                CboTitulo.SelectedValue = DsInmueble.Tables["Datos"].Rows[0]["TituloNotarioId"].ToString();
                TxtNomNotario.Text = DsInmueble.Tables["Datos"].Rows[0]["Notario"].ToString();
            }
            TxtDirccion.Text = DsInmueble.Tables["Datos"].Rows[0]["Direccion"].ToString();
            TxtAldea.Text = DsInmueble.Tables["Datos"].Rows[0]["Aldea"].ToString();
            CboDepartamento.Text = DsInmueble.Tables["Datos"].Rows[0]["Departamento"].ToString();
            CboDepartamento.SelectedValue = DsInmueble.Tables["Datos"].Rows[0]["DepartamentoId"].ToString();
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
            CboMunicipio.Text = DsInmueble.Tables["Datos"].Rows[0]["Municipio"].ToString();
            CboMunicipio.SelectedValue = DsInmueble.Tables["Datos"].Rows[0]["MunicipioId"].ToString();
            TxtColNorte.Text = DsInmueble.Tables["Datos"].Rows[0]["Colindancia_Norte"].ToString();
            TxtColSur.Text = DsInmueble.Tables["Datos"].Rows[0]["Colindancia_Sur"].ToString();
            TxtColEste.Text = DsInmueble.Tables["Datos"].Rows[0]["Colindancia_Este"].ToString();
            TxtColOeste.Text = DsInmueble.Tables["Datos"].Rows[0]["Colindancia_Oeste"].ToString();
            TxtArea.Text = DsInmueble.Tables["Datos"].Rows[0]["Area"].ToString();
            if (DsInmueble.Tables["Datos"].Rows[0]["En_Area"].ToString() == "1")
            {
                OptAreasPro.SelectedValue = "1";
                DivArea.Visible = true;
                CboArea.Text = DsInmueble.Tables["Datos"].Rows[0]["AreaPro"].ToString();
                CboArea.SelectedValue = DsInmueble.Tables["Datos"].Rows[0]["AreaProtegidaId"].ToString();
            }
            else
            {
                OptAreasPro.SelectedValue = "0";
            }
            BloquearFinca();
            TxtFecEmi.Enabled = true;
            DsInmueble.Clear();
        }

        void GrdPolIntervenir_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_PoligonoIntervenir"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPolIntervenir, "Dt_PoligonoIntervenir");
        }

        void BtnCargaPolIntervenir_ServerClick(object sender, EventArgs e)
        {
            DivErrPolIntervencion.Visible = false;
            if (RadUploadPolIntervenir.UploadedFiles.Count > 0)
            {
                string Extension = RadUploadPolIntervenir.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPolIntervencion.Visible = true;
                    LblErrPolIntervencion.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = RadUploadPolIntervenir.UploadedFiles[0].InputStream;
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = true;
                        resultXls = excelReader.AsDataSet();

                        Ds_Temporal.Tables["Dt_PoligonoIntervenir"].Clear();
                        foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                        {
                            if (iDtRow["X"].ToString() != "")
                            {
                                DataRow rowNew = Ds_Temporal.Tables["Dt_PoligonoIntervenir"].NewRow();
                                rowNew["Id"] = iDtRow["Poligono"];
                                rowNew["X"] = iDtRow["X"];
                                rowNew["Y"] = iDtRow["Y"];
                                Ds_Temporal.Tables["Dt_PoligonoIntervenir"].Rows.Add(rowNew);
                            }

                        }

                        GrdPolIntervenir.Rebind();
                    }
                    catch (Exception ex)
                    {
                        DivErrPolIntervencion.Visible = true;
                        LblErrPolIntervencion.Text = ex.Message;
                    }
                }
            }
            else
            {
                DivErrPolIntervencion.Visible = true;
                LblErrPolIntervencion.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }

            
        }

        void GrdPolBoque_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_PoligonoBosque"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPolBoque, "Dt_PoligonoBosque");
        }

        void BtnCargaPolBosque_ServerClick(object sender, EventArgs e)
        {
            DivErrPolBosque.Visible = false;
            if (RadUploadoPolBosque.UploadedFiles.Count > 0)
            {
                string Extension = RadUploadoPolBosque.UploadedFiles[0].GetExtension().ToString();
                if ((Extension == ".xls") || (Extension == ".XLS"))
                {
                    DivErrPolBosque.Visible = true;
                    LblErrPolBosque.Text = "Solo puede subir archivos .xlsx";
                }
                else
                {
                    try
                    {
                        Stream stream = RadUploadoPolBosque.UploadedFiles[0].InputStream;
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

                        GrdPolBoque.Rebind();
                    }
                    catch (Exception ex)
                    {
                        String iM = ex.Message;
                        DivErrPolBosque.Visible = true;
                        LblErrPolBosque.Text = ex.Message;
                    }
                }
                
            }
            else
            {
                DivErrPolBosque.Visible = true;
                LblErrPolBosque.Text = "Solo puede subir archivos .xlsx, no selecciono un archivo valido";
            }
           
        }

        void GrdEspecies_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarEspecie(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["EspecieId"].ToString()));
            }
        }

        void EliminarEspecie(int EspecieId)
        {
            for (int i = 0; i < GrdEspecies.Items.Count; i++)
            {
                if (EspecieId == Convert.ToInt32(GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_InventarioSAF"].NewRow();
                    rowNew["EspecieId"] = GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                    rowNew["Especie"] = GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                    Ds_Temporal.Tables["Dt_InventarioSAF"].Rows.Add(rowNew);
                }
            }
            dv = Ds_Temporal.Tables["Dt_InventarioSAF"].DefaultView;
            GrdEspecies.Rebind();
        }

        void EliminarEspeciePlanManejo(int EspecieId)
        {
            for (int i = 0; i < GrdEspeciePLanManejo.Items.Count; i++)
            {
                if (EspecieId == Convert.ToInt32(GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieArb"].NewRow();
                    rowNew["EspecieId"] = GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                    rowNew["Especie"] = GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                    Ds_Temporal.Tables["Dt_EspecieArb"].Rows.Add(rowNew);
                }
            }
            dv = Ds_Temporal.Tables["Dt_EspecieArb"].DefaultView;
            GrdEspeciePLanManejo.Rebind();
        }

        void EliminarEspecieRepo(int EspecieId)
        {
            for (int i = 0; i < GrdEspecieRepo.Items.Count; i++)
            {
                if (EspecieId == Convert.ToInt32(GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]))
                {

                }
                else
                {
                    DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieRepo"].NewRow();
                    rowNew["EspecieId"] = GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                    rowNew["Especie"] = GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                    Ds_Temporal.Tables["Dt_EspecieRepo"].Rows.Add(rowNew);
                }
            }
            dv = Ds_Temporal.Tables["Dt_EspecieRepo"].DefaultView;
            GrdEspecieRepo.Rebind();
        }

        void GrdEspecies_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (dv.Count > 0)
                ClUtilitarios.LlenaGridDataView(dv, GrdEspecies, "Dt_InventarioSAF");
        }

        void BtnAddEspecie_ServerClick(object sender, EventArgs e)
        {
            if (ValidaEspecie() == true)
            {
                if (ExisteEspecie(Convert.ToInt32(CboEspecie.SelectedValue)))
                {
                    DivErrEspecie.Visible = true;
                    LblErrEspecie.Text = "Especie ya existe";

                }
                else
                {
                    CargarGridEspecie();
                    AgregaEspecie();
                    dv = Ds_Temporal.Tables["Dt_InventarioSAF"].DefaultView;
                    GrdEspecies.Rebind();
                    LimpiarEspecie();
                }



            }
        }

        void LimpiarEspecie()
        {
            CboEspecie.ClearSelection();
        }

        void LimpiarEspeciePlanManejo()
        {
            CboEspeciePlanManejo.ClearSelection();
        }

        void LimpiarEspecieRepo()
        {
            CboEspecieRepoblacion.ClearSelection();
        }

        void AgregaEspecie()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_InventarioSAF"].NewRow();
            rowNew["EspecieId"] = CboEspecie.SelectedValue;
            rowNew["Especie"] = CboEspecie.Text;
            Ds_Temporal.Tables["Dt_InventarioSAF"].Rows.Add(rowNew);
        }

        void AgregaEspeciePlanManejo()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieArb"].NewRow();
            rowNew["EspecieId"] = CboEspeciePlanManejo.SelectedValue;
            rowNew["Especie"] = CboEspeciePlanManejo.Text;
            Ds_Temporal.Tables["Dt_EspecieArb"].Rows.Add(rowNew);
        }

        void AgregaEspecieRepo()
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieRepo"].NewRow();
            rowNew["EspecieId"] = CboEspecieRepoblacion.SelectedValue;
            rowNew["Especie"] = CboEspecieRepoblacion.Text;
            Ds_Temporal.Tables["Dt_EspecieRepo"].Rows.Add(rowNew);
        }

        void AgregaEspecieCargada(int EspecieId, string Especie)
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_InventarioSAF"].NewRow();
            rowNew["EspecieId"] = EspecieId;
            rowNew["Especie"] = Especie;
            Ds_Temporal.Tables["Dt_InventarioSAF"].Rows.Add(rowNew);
        }

        void AgregaEspecieCargadaPlanManejo(int EspecieId, string Especie)
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieArb"].NewRow();
            rowNew["EspecieId"] = EspecieId;
            rowNew["Especie"] = Especie;
            Ds_Temporal.Tables["Dt_EspecieArb"].Rows.Add(rowNew);
        }

        void AgregaEspecieCargadaAccionRepo(int EspecieId, string Especie)
        {
            DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieRepo"].NewRow();
            rowNew["EspecieId"] = EspecieId;
            rowNew["Especie"] = Especie;
            Ds_Temporal.Tables["Dt_EspecieRepo"].Rows.Add(rowNew);
        }

        void CargarGridEspecie()
        {
            for (int i = 0; i < GrdEspecies.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_InventarioSAF"].NewRow();
                rowNew["EspecieId"] = GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                rowNew["Especie"] = GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                Ds_Temporal.Tables["Dt_InventarioSAF"].Rows.Add(rowNew);
            }
        }

        void CargarGridEspeciePlanManejo()
        {
            for (int i = 0; i < GrdEspeciePLanManejo.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieArb"].NewRow();
                rowNew["EspecieId"] = GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                rowNew["Especie"] = GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                Ds_Temporal.Tables["Dt_EspecieArb"].Rows.Add(rowNew);
            }
        }

        void CargarGridEspecieRepo()
        {
            for (int i = 0; i < GrdEspecieRepo.Items.Count; i++)
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_EspecieRepo"].NewRow();
                rowNew["EspecieId"] = GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"];
                rowNew["Especie"] = GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["Especie"];
                Ds_Temporal.Tables["Dt_EspecieRepo"].Rows.Add(rowNew);
            }
        }

        bool ExisteEspecie(int EspecieId)
        {
            bool Existe = false;
            for (int i = 0; i < GrdEspecies.Items.Count; i++)
            {
                if (Convert.ToInt32(GrdEspecies.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]) == EspecieId)
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        bool ExisteEspeciePlanManejo(int EspecieId)
        {
            bool Existe = false;
            for (int i = 0; i < GrdEspeciePLanManejo.Items.Count; i++)
            {
                if (Convert.ToInt32(GrdEspeciePLanManejo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]) == EspecieId)
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        bool ExisteEspecieRepoblacion(int EspecieId)
        {
            bool Existe = false;
            for (int i = 0; i < GrdEspecieRepo.Items.Count; i++)
            {
                if (Convert.ToInt32(GrdEspecieRepo.Items[i].OwnerTableView.DataKeyValues[i]["EspecieId"]) == EspecieId)
                {
                    Existe = true;
                    break;
                }
            }
            return Existe;
        }

        bool ValidaEspecie()
        {
            LblErrEspecie.Text = "";
            DivErrEspecie.Visible = false;
            bool HayError = false;
            if ((CboEspecie.Text == "") || (CboEspecie.SelectedValue == ""))
            {
                if (LblErrEspecie.Text == "")
                    LblErrEspecie.Text = LblErrEspecie.Text + "Debe seleccionar la especie";
                else
                    LblErrEspecie.Text = LblErrEspecie.Text + ", debe seleccionar la especie";
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

        bool ValidaEspeciePlanManejo()
        {
            LblErrEspeciePlan.Text = "";
            DivErrEspeciePlan.Visible = false;
            bool HayError = false;
            if ((CboEspeciePlanManejo.Text == "") || (CboEspeciePlanManejo.SelectedValue == ""))
            {
                if (LblErrEspeciePlan.Text == "")
                    LblErrEspeciePlan.Text = LblErrEspeciePlan.Text + "Debe seleccionar la especie";
                else
                    LblErrEspeciePlan.Text = LblErrEspeciePlan.Text + ", debe seleccionar la especie";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrEspeciePlan.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ValidaEspecieRepo()
        {
            LblErrEspecieRepo.Text = "";
            DivErrEspecieRepo.Visible = false;
            bool HayError = false;
            if ((CboEspecieRepoblacion.Text == "") || (CboEspecieRepoblacion.SelectedValue == ""))
            {
                if (LblErrEspecieRepo.Text == "")
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + "Debe seleccionar la especie";
                else
                    LblErrEspecieRepo.Text = LblErrEspecieRepo.Text + ", debe seleccionar la especie";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrEspecieRepo.Visible = true;
                return false;
            }

            else
                return true;
        }

        void GrdPoligono_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Poligono"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPoligono, "Dt_Poligono");
        }

        void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            DivOkPoligono.Visible = false;
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
                        String iM = ex.Message;
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

        void GrdRepresentantes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClManejo.Elimina_Representante_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PersonaIdRep"]));
                GrdRepresentantes.Rebind();
            }
        }

        void GrdRepresentantes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RepresentanteYaExistentes(Convert.ToInt32(TxtAsignacionId.Text));
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
            bool HayErrorFecha = false;
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
            if (CboGeneroRep.SelectedValue == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe seleccionar su genero";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe seleccionar su genero";
                HayError = true;
            }
            if (CboEstadoCivilRep.SelectedValue == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe seleccionar su estado civil";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe seleccionar su estado civil";
                HayError = true;
            }
            if (CboOcupacionRep.SelectedValue == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe seleccionar su ocupación";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe seleccionar su ocupación";
                HayError = true;
            }
            if (TxtFecNacRep.DateInput.Text == "")
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe ingresar su fecha de nacimiento";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe ingresar su fecha de nacimiento";
                HayError = true;
                HayErrorFecha = true;
            }
            if ((TxtFecNacRep.DateInput.Text != "") && (Convert.ToDateTime(TxtFecNacRep.SelectedDate) > ClUtilitarios.FechaDB()))
            {
                if (LblMansajeBadRepresentante.Text == "")
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "La Fecha de Nacimiento no puede ser mayor a la actual";
                else
                    LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", La Fecha de Nacimiento no puede ser mayor a la actual";
                HayError = true;
                HayErrorFecha = true;
            }
            if (!HayErrorFecha == true)
            {
                if (Convert.ToInt32(Convert.ToDateTime(TxtFecNac.SelectedDate).Year) <= ClUtilitarios.FechaDB().Year && !ClUtilitarios.EsMayor(Convert.ToDateTime(TxtFecNac.SelectedDate)))
                {
                    if (LblMansajeBadRepresentante.Text == "")
                        LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + "Debe ser mayor de edad";
                    else
                        LblMansajeBadRepresentante.Text = LblMansajeBadRepresentante.Text + ", Debe ser mayor de edad";
                    HayError = true;
                }
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
                        Dpi = TxtDpiRep.Text.Replace("-","");
                    }
                    else
                    {
                        item["PaisIdRep"] = CboPaisRep.SelectedValue;
                        item["DpiRep"] = TxtPasaporteRep.Text;
                        PaisId = Convert.ToInt32(CboPaisRep.SelectedValue);
                        Dpi = TxtPasaporteRep.Text;
                    }
                    //DsRepresentantes.Tables["Representantes"].Rows.Add(item);

                    AgregarRepresentanteAlPlan(0, 0, TxtNombresRep.Text, TxtApellidosRep.Text, Convert.ToDateTime(TxtFecVenceIdRep.SelectedDate), PaisId, Dpi, Convert.ToInt32(CboTipoIdentificacionRep.SelectedValue), Convert.ToInt32(CboGeneroRep.SelectedValue), Convert.ToDateTime(TxtFecNacRep.SelectedDate), Convert.ToInt32(CboEstadoCivilRep.SelectedValue), Convert.ToInt32(CboOcupacionRep.SelectedValue));
                    
                    DivGoodRepresentante.Visible = true;
                    LblMansajeGoodRepresentante.Text = "Representante Agregado Exitosamente";
                    GrdRepresentantes.Rebind();
                    LimiarPropietario();
                    DivNombresPropRep.Visible = false;
                    DivApePropRep.Visible = false;
                    DivAddPropRep.Visible = false;
                    DivFecVencimientoRep.Visible = false;
                    DivGeneroRep.Visible = false;
                    DivFecNacRep.Visible = false;
                    DivEstadoCivilRep.Visible = false;
                    DivOcupacionRep.Visible = false;

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
                            //DataRow item = DsRepresentantes.Tables["Representantes"].NewRow();
                            //item["ExisteRep"] = 1;
                            //item["PersonaIdRep"] = Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]);
                            //item["DpiRep"] = TxtPasaporteRep.Text;
                            //item["NombresRep"] = dsDatos.Tables["DATOS"].Rows[0]["Nombres"];
                            //item["ApellidosRep"] = dsDatos.Tables["DATOS"].Rows[0]["Apellidos"];
                            //item["Fec_Venc_IdRep"] = string.Format("{0:dd/MM/yyyy}", dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]);
                            //item["PaisIdRep"] = dsDatos.Tables["DATOS"].Rows[0]["PaisId"];
                            //DsRepresentantes.Tables["Representantes"].Rows.Add(item);
                            AgregarRepresentanteAlPlan(1, Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]), dsDatos.Tables["DATOS"].Rows[0]["Nombres"].ToString(), dsDatos.Tables["DATOS"].Rows[0]["Apellidos"].ToString(), Convert.ToDateTime(dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]), 0, TxtDpi.Text, 2);
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
                            DivGeneroRep.Visible = true;
                            DivFecNacRep.Visible = true;
                            DivEstadoCivilRep.Visible = true;
                            DivOcupacionRep.Visible = true;
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
                            AgregarRepresentanteAlPlan(1, Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]), dsDatos.Tables["DATOS"].Rows[0]["Nombres"].ToString(), dsDatos.Tables["DATOS"].Rows[0]["Apellidos"].ToString(), Convert.ToDateTime(dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]), 0, TxtDpi.Text, 1);
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
                            DivGeneroRep.Visible = true;
                            DivFecNacRep.Visible = true;
                            DivEstadoCivilRep.Visible = true;
                            DivOcupacionRep.Visible = true;
                            LblMansajeBadRepresentante.Text = "El núemero de DPI no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }

                    }
                }
            }
        }

        void CboTipoIdentificacionRep_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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

        void GrdPropietarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                ClManejo.Elimina_PropietarioFinca_PlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PersonaId"]));
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

        void PropietariosYaExistentes(int AsignacionId, int InmuebleId)
        {
            //DsPropietarios.Tables["Propietarios"].Clear();
            DataSet dsPropietariosFinca = ClManejo.GetPropietarios_Inmuebles_Manejo(AsignacionId, InmuebleId);
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

        void ProdNoMaderablesYaExistentes(int AsignacionId)
        {
            DataSet dsProdNoMaderables = ClManejo.LeerXml_Prod_NoMaderables(AsignacionId);
            for (int i = 0; i < dsProdNoMaderables.Tables["Datos"].Rows.Count; i++)
            {
                DataRow itemGrid = DsProductoNoForestales.Tables["ProductoNoForestal"].NewRow();
                itemGrid["Turno"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Turno"];
                itemGrid["Rodal"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Rodal"];
                itemGrid["Anis"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Anis"];
                itemGrid["Area"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Area"];
                itemGrid["Codigo_Producto"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Codigo_Producto"];
                itemGrid["Producto"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["NOmbre_Producto"];
                itemGrid["Unidad_MedidaId"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Unidad_MedidaId"];
                itemGrid["Unidad_Medida"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Unidad_Medida"];
                itemGrid["Peso"] = dsProdNoMaderables.Tables["Datos"].Rows[i]["Peso"];
                DsProductoNoForestales.Tables["ProductoNoForestal"].Rows.Add(itemGrid);
            }
            dsProdNoMaderables.Clear();
            GrdProdNoForestal.Rebind();
        }

        void EspeciesRepoblacionYaExistentes(int AsignacionId)
        {
            DataSet dsEspeciesRepo = ClManejo.LeerXml_Especie_Repoblacion(AsignacionId);
            for (int i = 0; i < dsEspeciesRepo.Tables["Datos"].Rows.Count; i++)
            {
                DataRow itemGrid = DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].NewRow();
                itemGrid["TurnoRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["TurnoRepo"];
                itemGrid["RodalRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["RodalRepo"];
                itemGrid["EtapaIdRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["EtapaId"];
                itemGrid["EtapaRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["Etapa"];
                itemGrid["AreaRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["AreaRepo"];
                itemGrid["Tratamiento"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["Tratamiento"];
                itemGrid["AnisRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["AnisRepo"];
                itemGrid["EspecieRepoId"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["EspecieRepoId"];
                itemGrid["EspecieRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["EspecieRepo"];
                itemGrid["Descripcion"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["Descripcion"];
                itemGrid["DensidadRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["DensidadRepo"];
                itemGrid["TiempoEje"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["TiempoEje"];
                itemGrid["OtrasActividades"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["OtrasActividades"];
                itemGrid["SistemaRepoId"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["SistemaRepoId"];
                itemGrid["SistemaRepo"] = dsEspeciesRepo.Tables["Datos"].Rows[i]["SistemaRepo"];
                DsEspeciesRepoblacion.Tables["EspeciesRepoblacion"].Rows.Add(itemGrid);
            }
            dsEspeciesRepo.Clear();
            GrdEspeciesRepoblacion.Rebind();
        }

        void RepresentanteYaExistentes(int AsignacionId)
        {
            DsRepresentantes.Tables["Representantes"].Clear();
            DataSet dsRepresentantes = ClManejo.GetRepresentantes_Manejo(AsignacionId);
            if (dsRepresentantes.Tables["Datos"].Rows.Count > 0)
            {
                for (int i = 0; i < dsRepresentantes.Tables["Datos"].Rows.Count; i++)
                {
                    DataRow itemGrid = DsRepresentantes.Tables["Representantes"].NewRow();
                    itemGrid["ExisteRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["Existe"];
                    itemGrid["PersonaIdRep"] = Convert.ToInt64(dsRepresentantes.Tables["Datos"].Rows[i]["PersonaId"]);
                    itemGrid["DpiRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["Dpi"];
                    itemGrid["NombresRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["Nombres"];
                    itemGrid["ApellidosRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["Apellidos"];
                    itemGrid["Fec_Venc_IdRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["Fec_Ven_Id"];
                    itemGrid["PaisIdRep"] = dsRepresentantes.Tables["Datos"].Rows[i]["PaisId"];
                    DsRepresentantes.Tables["Representantes"].Rows.Add(itemGrid);
                }
            }
            
        }

        void GrdPropietarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            if (DsPropietarios.Tables["Propietarios"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDataSet(DsPropietarios, GrdPropietarios, "Propietarios");
        }

        void BtnAddPropietario_ServerClick(object sender, EventArgs e)
        {
            DivBadPropietario.Visible = false;
            DivGoodPropietario.Visible = false;
            LblMansajeBadPropietario.Text = "";
            LblMansajeGoodPropietario.Text = "";
            bool HayError = false;
            bool HayErrorFecha = false;
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
            if (CboGenero.SelectedValue == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe seleccionar su genero";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe seleccionar su genero";
                HayError = true;
            }
            if (CboEstadoCivil.SelectedValue == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe seleccionar su estado civil";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe seleccionar su estado civil";
                HayError = true;
            }
            if (CboOcupacion.SelectedValue == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe seleccionar su ocupación";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe seleccionar su ocupación";
                HayError = true;
            }
            if (TxtFecNac.DateInput.Text == "")
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe ingresar su fecha de nacimiento";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe ingresar su fecha de nacimiento";
                HayError = true;
                HayErrorFecha = true;
            }
            if ((TxtFecNac.DateInput.Text != "") && (Convert.ToDateTime(TxtFecNac.SelectedDate) > ClUtilitarios.FechaDB()))
            {
                if (LblMansajeBadPropietario.Text == "")
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "La Fecha de Nacimiento no puede ser mayor a la actual";
                else
                    LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", La Fecha de Nacimiento no puede ser mayor a la actual";
                HayError = true;
                HayErrorFecha = true;
            }
            if (!HayErrorFecha == true)
            {
                if (Convert.ToInt32(Convert.ToDateTime(TxtFecNac.SelectedDate).Year) <= ClUtilitarios.FechaDB().Year && !ClUtilitarios.EsMayor(Convert.ToDateTime(TxtFecNac.SelectedDate)))
                {
                    if (LblMansajeBadPropietario.Text == "")
                        LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + "Debe ser mayor de edad";
                    else
                        LblMansajeBadPropietario.Text = LblMansajeBadPropietario.Text + ", Debe ser mayor de edad";
                    HayError = true;
                }
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
                        Dpi = TxtDpi.Text.Replace("-","").ToString();
                    }
                    else
                    {
                        item["PaisId"] = CboPais.SelectedValue;
                        PaisId = Convert.ToInt32(CboPais.SelectedValue);
                        item["Dpi"] = TxtPasaportePropietario.Text;
                        Dpi = TxtPasaportePropietario.Text;
                    }
                    item["GeneroId"] = CboGenero.SelectedValue;
                    item["Fec_Nac"] = string.Format("{0:dd/MM/yyyy}", TxtFecNac.SelectedDate);
                    item["EstadoCivilId"] = CboEstadoCivil.SelectedValue;
                    item["OcupacionId"] = CboOcupacion.SelectedValue;
                    DsPropietarios.Tables["Propietarios"].Rows.Add(item);
                    DivGoodPropietario.Visible = true;
                    LblMansajeGoodPropietario.Text = "Propietario Agregado Exitosamente";
                    GrdPropietarios.Rebind();
                    AgregarPropietarioAlPlan(0, 0, TxtNombrePropietario.Text, TxtApellidoPropietario.Text, Convert.ToDateTime(TxtFecVenc.SelectedDate), PaisId, Dpi, Convert.ToInt32(CboTipoIdPropietario.SelectedValue), Convert.ToInt32(CboGenero.SelectedValue), Convert.ToDateTime(TxtFecNac.SelectedDate), Convert.ToInt32(CboEstadoCivil.SelectedValue),Convert.ToInt32(CboOcupacion.SelectedValue));
                    LimiarPropietario();
                    DivNombresProp.Visible = false;
                    DivApeProp.Visible = false;
                    DivAddProp.Visible = false;
                    DivFecVencimiento.Visible = false;
                    DivGeneroProp.Visible = false;
                    DivFecNacProp.Visible = false;
                    DivEstadoCivilProp.Visible = false;
                    DivOcupacionProp.Visible = false;
                }

            }
        }

        void AgregarPropietarioAlPlan(int Existe, int PersonaId, string Nombres, string Apellidos, DateTime FecVence, int PaisId, string Dpi, int Origen_Persona, int GeneroId = 0, DateTime Fe_Nac = default(DateTime), int EstadoCivilId = 0, int OcupacionId = 0)
        {
            if (Existe == 0)
            {
                PersonaId = ClPersona.MaxPersonaId();
                ClPersona.Insertar_Persona_Propietario(PersonaId, Nombres, Apellidos, Dpi, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", FecVence)), Origen_Persona, PaisId, GeneroId, Fe_Nac, EstadoCivilId, OcupacionId);
            }
            ClManejo.InsertTempFincaPropietarioPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), PersonaId);

        }

        void AgregarRepresentanteAlPlan(int Existe, int PersonaId, string Nombres, string Apellidos, DateTime FecVence, int PaisId, string Dpi, int Origen_Persona, int GeneroId = 0, DateTime Fe_Nac = default(DateTime), int EstadoCivilId = 0, int OcupacionId = 0)
        {
            if (Existe == 0)
            {
                PersonaId = ClPersona.MaxPersonaId();
                ClPersona.Insertar_Persona_Propietario(PersonaId, Nombres, Apellidos, Dpi, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", FecVence)), Origen_Persona, PaisId, GeneroId, Fe_Nac, EstadoCivilId, OcupacionId);
            }
            ClManejo.InsertTempRepresentantePlanManejo(Convert.ToInt32(TxtAsignacionId.Text), PersonaId);

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
                            DivGeneroProp.Visible = true;
                            DivFecNacProp.Visible = true;
                            DivEstadoCivilProp.Visible = true;
                            DivOcupacionProp.Visible = true;
                            LblMansajeBadPropietario.Text = "El núemero de Pasaporte no existe en nuetros registros, a continuación ingrese el nombre y apellido de la persona y luego agreguelo";
                        }

                    }
                }
            }
        }

        void CboTipoIdPropietario_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
                            AgregarPropietarioAlPlan(1, Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]), dsDatos.Tables["DATOS"].Rows[0]["Nombres"].ToString(), dsDatos.Tables["DATOS"].Rows[0]["Apellidos"].ToString(), Convert.ToDateTime(dsDatos.Tables["DATOS"].Rows[0]["Fec_Ven_Identificacion"]), 0, TxtDpi.Text,1);
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
                            DivGeneroProp.Visible = true;
                            DivFecNacProp.Visible = true;
                            DivEstadoCivilProp.Visible = true;
                            DivOcupacionProp.Visible = true;
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

        void LimpiarRepresentante()
        {
            TxtDpiRep.Text = "";
            TxtNombresRep.Text = "";
            TxtApellidosRep.Text = "";
            
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

        void CboTipoPersona_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
            ClManejo.ActualizarTipoPersonaTempFincaPlanManejo(Convert.ToInt32(TxtAsignacionId.Text), Convert.ToInt32(TxtInmuebleId.Text), Convert.ToInt32(CboTipoPersona.SelectedValue));
        }

        void OptAreasPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptAreasPro.Items[0].Selected == true)
                DivArea.Visible = false;
            else
                DivArea.Visible = true;
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

        void CboTipoDocumento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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

        bool ValidaFinca()
        {
            LblErrFinca.Text = "";
            DivErrFinca.Visible = false;
            bool HayError = false;
            int AreaId = 0;
            if (CboArea.SelectedValue != "")
                AreaId = Convert.ToInt32(CboArea.SelectedValue);

            if ((Convert.ToDouble(TxtUbicacionOeste.Text) > 742321.341707) || (Convert.ToDouble(TxtUbicacionOeste.Text) < 312146.719860))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "La Coordenada X esta fuera del rango de Guatemala";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", la Coordenada X esta fuera del rango de Guatemala";
                HayError = true;
            }
            if ((Convert.ToDouble(TxtUbicacionNorte.Text) > 1970263.493114) || (Convert.ToDouble(TxtUbicacionNorte.Text) < 1519520.063473))
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
            if (CboDepartamento.SelectedValue == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Debe seleccionar el departamento de la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", debe seleccionar el departamento de la finca";
                HayError = true;
            }
            if (CboMunicipio.SelectedValue == "")
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
            if ((TxtFecEmi.DateInput.Text != "") && (CboMunicipio.SelectedValue != "") && (CboTipoDocumento.SelectedValue != "") && (ClInmueble.Existe_Propiedad_Usuario(Convert.ToInt32(TxtUsrOwnPlan.Text), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDecimal(TxtArea.Text)) == true))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Esta propiedad ya fue ingresada";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Esta propiedad ya fue ingresada";
                HayError = true;
            }
            if ((TxtFecEmi.DateInput.Text != "") && (CboMunicipio.SelectedValue != "") && (CboTipoDocumento.SelectedValue != "") && (ClInmueble.Existe_Propiedad_OtroUsuario(Convert.ToInt32(Session["UsuarioId"]), TxtDirccion.Text, TxtAldea.Text, TxtFinca.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Convert.ToInt32(CboTipoDocumento.SelectedValue), Convert.ToDateTime(TxtFecEmi.SelectedDate), Convert.ToInt32(ClUtilitarios.IIf(TxtNoFinca.Text == "", 0, TxtNoFinca.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtFolio.Text == "", 0, TxtFolio.Text)), Convert.ToInt32(ClUtilitarios.IIf(TxtLibro.Text == "", 0, TxtLibro.Text)), TxtDe.Text, TxtNoCerti.Text, TxtMunEmiteDoc.Text, TxtNoEscritura.Text, Convert.ToInt32(CboTitulo.SelectedValue), TxtNomNotario.Text, Convert.ToInt32(OptAreasPro.SelectedValue), AreaId, Convert.ToDecimal(TxtArea.Text)) == true))
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "Esta propiedad ya fue ingresada por otro usuario";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", Esta propiedad ya fue ingresada por otro usuario";
                HayError = true;
            }
            if (TxtColNorte.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia norte";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia norte";
                HayError = true;
            }
            if (TxtColSur.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia sur";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia sur";
                HayError = true;
            }
            if (TxtColEste.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia este";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia este";
                HayError = true;
            }
            if (TxtColOeste.Text == "")
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado la colindancia oeste";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", No ha ingresado la colindancia oeste";
                HayError = true;
            }
            if (GrdPoligono.Items.Count == 0)
            {
                if (LblErrFinca.Text == "")
                    LblErrFinca.Text = LblErrFinca.Text + "No ha ingresado el polígono de la finca";
                else
                    LblErrFinca.Text = LblErrFinca.Text + ", no ha ingresado el polígono de la finca";
                HayError = true;
            }

            if (HayError == true)
            {
                DivErrFinca.Visible = true;
                return false;
            }

            else
                return true;
        }

        void OcultaMensajes()
        {
            LblErrFinca.Text = "";
            LblGoodFinca.Text = "";
            DivErrFinca.Visible = false;
            DivGoodFinca.Visible = false;
        }
    }
}