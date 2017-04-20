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
    public partial class Wfrm_Inscripcion_Entidad : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Gestion_Registro ClGestionRegistro;
        Cl_Inmueble ClInmueble;
        DataSet DsRepresentantes = new DataSet("Representantes");

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClGestionRegistro = new Cl_Gestion_Registro();
            ClInmueble = new Cl_Inmueble();
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
            CboDepartamentoEntidad.SelectedIndexChanged += CboDepartamentoEntidad_SelectedIndexChanged;
            CboTipoInscripcion.SelectedIndexChanged += CboTipoInscripcion_SelectedIndexChanged;
            CboMunicipioEntidad.SelectedIndexChanged += CboMunicipioEntidad_SelectedIndexChanged;
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
                
                DataSet dsPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
                TxtOrigenPersonaId.Text = dsPersona.Tables["Datos"].Rows[0]["Origen_PersonaId"].ToString();
                dsPersona.Clear();
                if (TxtOrigenPersonaId.Text == "2")
                    LblDirecNotifica.InnerText = "Dirección en Guatemala";
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSubCategoriasRegistro(8, Convert.ToInt32(Session["PersonaId"])), CboTipoInscripcion, "SubCategoriaId", "Nombre_Subcategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoInscripcion, "Tipo de Inscripción");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamentoEntidad, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamentoEntidad, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioEntidad, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Entidad(), CboTipoIns, "Tipo_EntidadId", "Tipo_Entidad");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIns, "Tipo");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Entidad(), CboTipoOrg, "Tipo_EntidadId", "Tipo_Entidad");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoOrg, "Tipo");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Cobertura_Entidad(), CboCoberturaIns, "CoberturaId", "Cobertura");
                ClUtilitarios.AgregarSeleccioneCombo(CboCoberturaIns, "Cobertura");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Cobertura_Entidad(), CboCoberturaOrg, "CoberturaId", "Cobertura");
                ClUtilitarios.AgregarSeleccioneCombo(CboCoberturaOrg, "Cobertura");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Propiedad(), CboPropiedadINs, "Tipo_PropiedadId", "Tipo_Propiedad");
                ClUtilitarios.AgregarSeleccioneCombo(CboPropiedadINs, "Tipo Propiedad");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Propiedad(), CboPropiedadOrg, "Tipo_PropiedadId", "Tipo_Propiedad");
                ClUtilitarios.AgregarSeleccioneCombo(CboPropiedadOrg, "Tipo Propiedad");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Tamano(), CboTamOrg, "TamanoId", "Tamano");
                ClUtilitarios.AgregarSeleccioneCombo(CboTamOrg, "Tamaño");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Tipo_Produccion(), CboProdOrg, "Tipo_ProduccionId", "Tipo_Produccion");
                ClUtilitarios.AgregarSeleccioneCombo(CboProdOrg, "Producción");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEtnia(), CboGrupoEtnico, "EtniaId", "Etnia");
                ClUtilitarios.AgregarSeleccioneCombo(CboGrupoEtnico, "Étnia");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdentificacionRep, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdentificacionRep, "Tipo de Identificación");

                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPaisRep, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPaisRep, "País");

            }
        }

        void CboMunicipioEntidad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet dsRegioSubregionEmpresa = ClInmueble.Get_Region_Subregion_MunicipioId(Convert.ToInt32(CboMunicipioEntidad.SelectedValue));
            TxtRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["Region"].ToString();
            TxtSubRegion.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubRegion"].ToString();
            TxtSubRegionId.Text = dsRegioSubregionEmpresa.Tables["Datos"].Rows[0]["SubregionId"].ToString();
            dsRegioSubregionEmpresa.Clear();
        }

        void CboTipoInscripcion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboTipoInscripcion.SelectedValue == "25")
            {
                DivDatosInstiuciones.Visible = true;
                DivDatosOrga.Visible = false;
                DivDatosAsociacion.Visible = false;
                DivDatosMun.Visible = false;
                LblTituloEmpresa.InnerText = "Nombre de Institución";
            }
            else if (CboTipoInscripcion.SelectedValue == "26")
            {
                DivDatosInstiuciones.Visible = false;
                DivDatosOrga.Visible = true;
                DivDatosAsociacion.Visible = false;
                DivDatosMun.Visible = false;
                LblTituloEmpresa.InnerText = "Nombre de Organización";
            }
            else if (CboTipoInscripcion.SelectedValue == "27")
            {
                DivDatosInstiuciones.Visible = false;
                DivDatosOrga.Visible = false;
                DivDatosAsociacion.Visible = true;
                DivDatosMun.Visible = false;
                LblTituloEmpresa.InnerText = "Nombre de Asociación";
            }
            else if (CboTipoInscripcion.SelectedValue == "28")
            {
                DivDatosInstiuciones.Visible = false;
                DivDatosOrga.Visible = false;
                DivDatosAsociacion.Visible = false;
                DivDatosMun.Visible = true;
                LblTituloEmpresa.InnerText = "Nombre de Municipalidad";
            }
        }

        void CboDepartamentoEntidad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamentoEntidad.SelectedValue)), CboMunicipioEntidad, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipioEntidad, "Municipio");
        }

       

      


       

      
      
      
        


        void BtnYes_Click(object sender, EventArgs e)
        {
            int GestionId = ClGestionRegistro.MaxGestionId(1);
            int Correlativo_Anual = ClGestionRegistro.MaxGestionId(2);
            string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;

            ClGestionRegistro.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(TxtSubRegionId.Text), 13, 3, Correlativo_Anual);
            
            int TelefonoDos = 0;
            if (TxtTelefonoNotifica.Text != "")
                TelefonoDos = Convert.ToInt32(TxtTelefonoNotifica.Text.Replace("-", ""));
            int TipoEntidad = 0;
            int CoberturaId = 0;
            int Tipo_PropiedadId = 0;
            if ((CboTipoInscripcion.SelectedValue == "25") || (CboTipoInscripcion.SelectedValue == "26"))
            {
                if (CboTipoInscripcion.SelectedValue == "25")
                {
                    TipoEntidad = Convert.ToInt32(CboTipoIns.SelectedValue);
                    CoberturaId = Convert.ToInt32(CboCoberturaIns.SelectedValue);
                    Tipo_PropiedadId = Convert.ToInt32(CboPropiedadINs.SelectedValue);
                }
                else if (CboTipoInscripcion.SelectedValue == "26")
                {
                    TipoEntidad = Convert.ToInt32(CboTipoOrg.SelectedValue);
                    CoberturaId = Convert.ToInt32(CboCoberturaOrg.SelectedValue);
                    Tipo_PropiedadId = Convert.ToInt32(CboPropiedadOrg.SelectedValue);
                }
            }
            string ActividadPrincipal = "";
            if ((CboTipoInscripcion.SelectedValue == "25") || (CboTipoInscripcion.SelectedValue == "26") || (CboTipoInscripcion.SelectedValue == "27"))
            {
                if (CboTipoInscripcion.SelectedValue == "25")
                    ActividadPrincipal = TxtActividadesPrincipalesIns.Text;
                else if (CboTipoInscripcion.SelectedValue == "26")
                    ActividadPrincipal = TxtActividadesPrincipalesOrg.Text;
                if (CboTipoInscripcion.SelectedValue == "27")
                    ActividadPrincipal = TxtActividadesPrincialesAso.Text;
            }
            int NoFamAtendidas = 0;
            if (CboTipoInscripcion.SelectedValue == "25")
                NoFamAtendidas = Convert.ToInt32(TxtNoFamIns.Text);
            else if (CboTipoInscripcion.SelectedValue == "26")
                NoFamAtendidas = Convert.ToInt32(TxtNoFamOrg.Text);
            if (CboTipoInscripcion.SelectedValue == "27")
                NoFamAtendidas = Convert.ToInt32(TxtNoFamAso.Text);
            if (CboTipoInscripcion.SelectedValue == "28")
                NoFamAtendidas = Convert.ToInt32(TxtNoFamMun.Text);
            int TamanoId = 0;
            int Tipo_ProduccionId = 0;
            if (CboTipoInscripcion.SelectedValue == "26")
            {
                TamanoId = Convert.ToInt32(CboTamOrg.SelectedValue);
                Tipo_ProduccionId = Convert.ToInt32(CboProdOrg.SelectedValue);
            }
            int EtniaId = 0;
            string Finalidada = "";
            int No_Integrantes = 0;
            double TotBosqueNatural = 0;
            double TotReforestacion = 0;
            if (CboTipoInscripcion.SelectedValue == "27")
            {
                EtniaId = Convert.ToInt32(CboGrupoEtnico.SelectedValue);
                Finalidada = TxtFinalidad.Text;
                No_Integrantes = Convert.ToInt32(TxtNoIntegrantes.Text);
                TotBosqueNatural = Convert.ToInt32(TxtTotalBosque.Text);
                TotReforestacion = Convert.ToInt32(TxtTotalRefo.Text);
            }
            int YearCreacion = 0;
            int TelefonoOficina = 0;
            int CelularEncargado = 0;
            if (CboTipoInscripcion.SelectedValue == "28")
            {
                YearCreacion = Convert.ToInt32(TxtAnisCrea.Text);
                TelefonoOficina = Convert.ToInt32(TxtTelefonoMun.Text.Replace("-", ""));
                CelularEncargado = Convert.ToInt32(TxtTelEncargado.Text.Replace("-", ""));
            }
            int Telefono = 0;
            if (TxtTelefonoNotifica.Text != "")
                Telefono = Convert.ToInt32(TxtTelefonoNotifica.Text.Replace("-", ""));

           ClGestionRegistro.Insert_Gestion_Entidad(GestionId, Convert.ToInt32(CboTipoInscripcion.SelectedValue), 8, TxtNomEmpresa.Text, TxtNit.Text, TxtObjeto.Text, TxtDireccion.Text, Convert.ToInt32(CboMunicipioEntidad.SelectedValue), Convert.ToInt32(TxtTelUno.Text.Replace("-", "")), TelefonoDos, TxtCorreo.Text, 2, TxtNomEmpresa.Text, TipoEntidad, CoberturaId, Tipo_PropiedadId, ActividadPrincipal, NoFamAtendidas, TamanoId, Tipo_ProduccionId, EtniaId, Finalidada, No_Integrantes, TotBosqueNatural, TotReforestacion, TxtNomOficina.Text, YearCreacion, TxtCorreoOficina.Text, TelefonoOficina, TxtNombresMun.Text, TxtApellidosMun.Text, TxtCorreoEncargado.Text, CelularEncargado, TxtDireccionNotifica.Text, Convert.ToInt32(CboMunicipio.SelectedValue), Telefono, Convert.ToInt32(TxtCelularNotifica.Text.Replace("-", "")), TxtCorreoNotifica.Text, TxtObservaciones.Text, TxtNomFirma.Text, Convert.ToInt32(Session["UsuarioId"]));
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
            Ds_Profesionales Ds_Formulario_Entidad = new Ds_Profesionales();
            Ds_Formulario_Entidad.Tables["Dt_Entidad"].Clear();
            DataRow row = Ds_Formulario_Entidad.Tables["Dt_Entidad"].NewRow();
            row["Requisitos"] = "Requisitos:\n1)  Copia del documento que ampare la constitución y objeto de la entidad;  \n2)  Copia del carné de identificación tributaria;";
            if (GrdRepresentantes.Items.Count == 0)
            {
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del propietario;";
                if (CboTipoInscripcion.SelectedValue == "28")
                    row["Requisitos"] = row["Requisitos"] + "\n4)  Copia simple del acta de toma de posesión o nombramiento según sea el caso.";
            }
            else
            {
                row["Requisitos"] = row["Requisitos"] + "\n3) Copia del documento personal de identificación del Representante Legal;  \n4) Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente;";
                if (CboTipoInscripcion.SelectedValue == "28")
                    row["Requisitos"] = row["Requisitos"] + "\n5)  Copia simple del acta de toma de posesión o nombramiento según sea el caso.";
            }
            
            row["Region"] = TxtRegion.Text;
            row["SubRegion"] = TxtSubRegion.Text;
            row["NUG"] = "-------";
            row["Fecha"] = string.Format("{0:dd/MM/yyyy}", ClUtilitarios.FechaDB());
            row["TipoInscripcion"] = CboTipoInscripcion.Text;

            row["NombreEntidad"] = TxtNomEmpresa.Text;
            row["NIT"] = TxtNit.Text;
            row["Objeto"] = TxtObjeto.Text;
            row["DireccionEntidad"] = TxtDireccion.Text;
            row["DepEntidad"] = CboDepartamentoEntidad.Text;
            row["MunEntidad"] = CboMunicipioEntidad.Text;
            row["TelefonoUno"] = TxtTelUno.Text;
            row["TelefonoDos"] = TxtTelefonoDos.Text;
            row["CorreoEntidad"] = TxtCorreo.Text;
            row["TipoPersona"] = "Persona Jurídica";
            row["RazonSocial"] = TxtNomEmpresa.Text;

            row["Direccion_Notificacion"] = TxtDireccionNotifica.Text;
            row["Municipio_Notificacion"] = CboMunicipio.Text;
            row["Departamento_Notificacion"] = CboDepartamento.Text;
            row["Telefono_Notificacion"] = TxtTelefonoNotifica.Text;
            row["Celular_Notificacion"] = TxtCelularNotifica.Text;
            row["Correo_Notificacion"] = TxtCorreoNotifica.Text;
            row["Observaciones"] = TxtObservaciones.Text;
            row["Nombre"] = TxtNomFirma.Text;
            row["TipoEntidadId"] = Convert.ToInt32(CboTipoInscripcion.SelectedValue);
            row["TieneRepresentantes"] = GrdRepresentantes.Items.Count;
            Ds_Formulario_Entidad.Tables["Dt_Entidad"].Rows.Add(row);

            if (CboTipoInscripcion.SelectedValue == "25")
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].Clear();
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].NewRow();
                rowInstitucion["Tipo"] = CboTipoIns.Text;
                rowInstitucion["Cobertura"] = CboCoberturaIns.Text;
                rowInstitucion["Propiedad"] = CboPropiedadINs.Text;
                rowInstitucion["ActividadPrincipal"] = TxtActividadesPrincipalesIns.Text;
                rowInstitucion["NoFamAtendidas"] = TxtNoFamIns.Text;
                Ds_Formulario_Entidad.Tables["Dt_EntidadInstitucion"].Rows.Add(rowInstitucion);
            }
            else if (CboTipoInscripcion.SelectedValue == "26")
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].Clear();
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].NewRow();
                rowInstitucion["Tipo"] = CboTipoOrg.Text;
                rowInstitucion["Cobertura"] = CboCoberturaOrg.Text;
                rowInstitucion["Propiedad"] = CboPropiedadOrg.Text;
                rowInstitucion["Tamano"] = CboTamOrg.Text;
                rowInstitucion["Produccion"] = CboProdOrg.Text;
                rowInstitucion["ActividadPrincipal"] = TxtActividadesPrincipalesOrg.Text;
                rowInstitucion["NoFamAtendidas"] = TxtNoFamOrg.Text;
                Ds_Formulario_Entidad.Tables["Dt_EntidadOrganizacion"].Rows.Add(rowInstitucion);
            }
            else if (CboTipoInscripcion.SelectedValue == "27")
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].Clear();
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].NewRow();
                rowInstitucion["GrupoEtnico"] = CboGrupoEtnico.Text;
                rowInstitucion["Finalidad"] = TxtFinalidad.Text;
                rowInstitucion["ActividadPrincipal"] = TxtActividadesPrincialesAso.Text;
                rowInstitucion["NoIntengrantes"] = TxtNoIntegrantes.Text;
                rowInstitucion["NoFamAtendidas"] = TxtNoFamAso.Text;
                rowInstitucion["TotBosqueNatural"] = TxtTotalBosque.Text;
                rowInstitucion["TotReforestacion"] = TxtTotalRefo.Text;
                Ds_Formulario_Entidad.Tables["Dt_EntidadAsociacion"].Rows.Add(rowInstitucion);
            }
            else if (CboTipoInscripcion.SelectedValue == "28")
            {
                Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].Clear();
                DataRow rowInstitucion = Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].NewRow();
                rowInstitucion["NombreOficina"] = TxtNomOficina.Text;
                rowInstitucion["YearCreacion"] = TxtAnisCrea.Text;
                rowInstitucion["CorreoOficina"] = TxtCorreoOficina.Text;
                rowInstitucion["Telefono"] = TxtTelefonoMun.Text;
                rowInstitucion["Nombres"] = TxtNombresMun.Text;
                rowInstitucion["Apellidos"] = TxtApellidosMun.Text;
                rowInstitucion["CorreoEncargado"] = TxtCorreoEncargado.Text;
                rowInstitucion["Celular"] = TxtTelEncargado.Text;
                rowInstitucion["NoFamAtendidas"] = TxtNoFamMun.Text;
                Ds_Formulario_Entidad.Tables["Dt_EntidadMunicipalidad"].Rows.Add(rowInstitucion);
            }

            if (GrdRepresentantes.Items.Count > 0)
            {
                Ds_Formulario_Entidad.Tables["Dt_Representantes"].Clear();
                for (int i = 0; i < GrdRepresentantes.Items.Count; i++)
                {
                    DataRow rowRepresentantes = Ds_Formulario_Entidad.Tables["Dt_Representantes"].NewRow();
                    rowRepresentantes["Nombres"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["NombresRep"] + " " + GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["ApellidosRep"];
                    if (GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["PaisIdRep"].ToString() == "0")
                        rowRepresentantes["TipoId"] = "DPI";
                    else
                        rowRepresentantes["TipoId"] = "Pasporte";
                    rowRepresentantes["Id"] = GrdRepresentantes.Items[i].OwnerTableView.DataKeyValues[i]["DpiRep"];
                    Ds_Formulario_Entidad.Tables["Dt_Representantes"].Rows.Add(rowRepresentantes);
                }
            }
            Session["Dt_Entidad"] = Ds_Formulario_Entidad;
            RadWindow1.Title = "Vista Previa Insripción";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEntidad.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

    


       

        bool Valida()
        {
            LblMensaje.Text = "";
            BtnEror.Visible = false;
            bool HayError = false;

            if ((CboTipoInscripcion.Text == "") || (CboTipoInscripcion.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de inscripción";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de inscripción";
                HayError = true;
            }
            if ((CboDepartamentoEntidad.Text == "") || (CboDepartamentoEntidad.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el departamento de la entidad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el departamento de la entidad";
                HayError = true;
            }
            if ((CboMunicipioEntidad.Text == "") || (CboMunicipioEntidad.SelectedValue == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el municipio de la entidad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el municipio de la entidad";
                HayError = true;
            }
            if ((TxtNit.Text != "") && (ClGestionRegistro.Existe_Entidad(TxtNit.Text) == true))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Este número de NIT ya existe en nuestros registros";
                else
                    LblMensaje.Text = LblMensaje.Text + ", este número de NIT ya existe en nuestros registros";
                HayError = true;
            }
           
            if ((CboTipoInscripcion.SelectedValue == "25") && ((CboTipoIns.Text == "") || (CboTipoIns.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de Institución";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de Institución";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "25") && ((CboCoberturaIns.Text == "") || (CboCoberturaIns.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de Cobertura";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de Cobertura";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "25") && ((CboPropiedadINs.Text == "") || (CboPropiedadINs.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de propiedad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de propiedad";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "26") && ((CboTipoOrg.Text == "") || (CboTipoOrg.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de Organización";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de Organización";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "26") && ((CboCoberturaOrg.Text == "") || (CboCoberturaOrg.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de Cobertura";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de Cobertura";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "26") && ((CboPropiedadOrg.Text == "") || (CboPropiedadOrg.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de propiedad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de propiedad";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "26") && ((CboTamOrg.Text == "") || (CboTamOrg.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de propiedad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de propiedad";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "26") && ((CboProdOrg.Text == "") || (CboProdOrg.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el tipo de propiedad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar el tipo de propiedad";
                HayError = true;
            }
            if ((CboTipoInscripcion.SelectedValue == "27") && ((CboGrupoEtnico.Text == "") || (CboGrupoEtnico.SelectedValue == "")))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la étnia de la asociación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", debe seleccionar la étnia de la asociación";
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
                DivGoodRepresentante.Visible = true;
                LblMansajeGoodRepresentante.Text = "Representante eliminado";
            }
        }

        
        
    }
}