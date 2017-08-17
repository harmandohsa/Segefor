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
using System.Xml;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Seguimiento_Tecnico : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Catalogos ClCatalogos;
        Cl_Xml ClXml;
        Cl_Manejo ClManejo;
        Cl_Manejo_Impresion ClManejoImpresion;
        Cl_Especie ClEspecie;


        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        DataSet resultXls = new DataSet();
        DataSet DsDatosDictamen = new DataSet("DsDatosDictamen");
        DataSet DsEtapa = new DataSet("DsDatosEtapa");
        double Total = 0;
        int TipoSi = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImgVerinfo.Click += ImgVerinfo_Click;
            IngVerAnexos.Click += IngVerAnexos_Click;
            ImgVerProvidencia.Click += ImgVerProvidencia_Click;
            GrdInmuebles.NeedDataSource += GrdInmuebles_NeedDataSource;
            OptEnmiendas.SelectedIndexChanged += OptEnmiendas_SelectedIndexChanged;
            CboEnmienda.SelectedIndexChanged += CboEnmienda_SelectedIndexChanged;
            BtnAddEnmienda.ServerClick += BtnAddEnmienda_ServerClick;
            GrdEnmiendas.NeedDataSource += GrdEnmiendas_NeedDataSource;
            GrdEnmiendas.ItemCommand += GrdEnmiendas_ItemCommand;
            BtVistaPreviaEnminada.Click += BtVistaPreviaEnminada_Click;
            CboTipoInventario.SelectedIndexChanged += CboTipoInventario_SelectedIndexChanged;
            GrdResumen.NeedDataSource += GrdResumen_NeedDataSource;
            GrdResumen.PreRender += GrdResumen_PreRender;
            CboDictamina.SelectedIndexChanged += CboDictamina_SelectedIndexChanged;
            BtnCargarBoleta.ServerClick += BtnCargarBoleta_ServerClick;
            GrdBoleta.NeedDataSource += GrdBoleta_NeedDataSource;
            GrdSilvicultural.NeedDataSource += GrdSilvicultural_NeedDataSource;
            GrdSilvicultural.ItemDataBound += GrdSilvicultural_ItemDataBound;
            GrdSilvicultural.PreRender += GrdSilvicultural_PreRender;
            GrdEtapa.NeedDataSource += GrdEtapa_NeedDataSource;
            CboGarantia.SelectedIndexChanged += CboGarantia_SelectedIndexChanged;
            GrdMaderaPie.NeedDataSource += GrdMaderaPie_NeedDataSource;
            GrdMaderaPie.ItemDataBound += GrdMaderaPie_ItemDataBound;
            GrdMaderaPie.PreRender += GrdMaderaPie_PreRender;
            CboTipoUsuario.SelectedIndexChanged += CboTipoUsuario_SelectedIndexChanged;
            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            BtnEnviar.Click += BtnEnviar_Click;
            BtnYes.Click += BtnYes_Click;
            BtnEnviarEnmienda.Click += BtnEnviarEnmienda_Click;

            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClCatalogos = new Cl_Catalogos();
            ClXml = new Cl_Xml();
            ClManejo = new Cl_Manejo();
            ClManejoImpresion = new Cl_Manejo_Impresion();
            ClEspecie = new Cl_Especie();

            DataTable DtDatosDictamen = DsDatosDictamen.Tables.Add("DatosDictamen");
            DataColumn MetodologiaCorro = DtDatosDictamen.Columns.Add("MetodologiaCorro", typeof(string));
            DataColumn MetodologiaInvForestal = DtDatosDictamen.Columns.Add("MetodologiaInvForestal", typeof(string));
            DataColumn FormaEvaluacion = DtDatosDictamen.Columns.Add("FormaEvaluacion", typeof(string));
            DataColumn ConCaracBio = DtDatosDictamen.Columns.Add("ConCaracBio", typeof(string));
            DataColumn ConVeracidad = DtDatosDictamen.Columns.Add("ConVeracidad", typeof(string));
            DataColumn ConPropuestaManejo = DtDatosDictamen.Columns.Add("ConPropuestaManejo", typeof(string));
            DataColumn ConPropuestaTratamiento = DtDatosDictamen.Columns.Add("ConPropuestaTratamiento", typeof(string));
            DataColumn Dictamen = DtDatosDictamen.Columns.Add("Dictamen", typeof(string));
            DataColumn TipoGarantia = DtDatosDictamen.Columns.Add("TipoGarantia", typeof(string));
            DataColumn DictamenId = DtDatosDictamen.Columns.Add("DictamenId", typeof(int));
            DataColumn MontoGarantia = DtDatosDictamen.Columns.Add("MontoGarantia", typeof(double));
            DataColumn PorGarantia = DtDatosDictamen.Columns.Add("PorGarantia", typeof(int));
            DataColumn TotValMaderaPie = DtDatosDictamen.Columns.Add("TotValMaderaPie", typeof(double));
            DataColumn RecomendacionUno = DtDatosDictamen.Columns.Add("RecomendacionUno", typeof(string));
            DataColumn RecomendacionDos = DtDatosDictamen.Columns.Add("RecomendacionDos", typeof(string));
            DataColumn OtrasRecomendaciones = DtDatosDictamen.Columns.Add("OtrasRecomendaciones", typeof(string));


            DataTable DtEtapa = DsEtapa.Tables.Add("Etapa");
            DataColumn EtapaId = DtEtapa.Columns.Add("EtapaId", typeof(int));
            DataColumn Etapa = DtEtapa.Columns.Add("Etapa", typeof(string));
            DataColumn FecIni = DtEtapa.Columns.Add("FecIni", typeof(string));
            DataColumn FecFin = DtEtapa.Columns.Add("FecFin", typeof(string));

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(54, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 54);
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
                LblNug.Text = "Gestión No.: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                LblSolicitante.Text = "Solicitante: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                    LblIdentificacion.Text = ClManejo.Get_Identificacion_Gestion_Manejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_EnmiendasTec(), CboEnmienda, "EnmiendaTecId", "EnmiendaTec");
                ClUtilitarios.AgregarSeleccioneCombo(CboEnmienda, "Enmienda");
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Listado_Tipo_Dictamen(), CboDictamina, "Tipo_DictamenId", "Tipo_Dictamen");
                ClUtilitarios.AgregarSeleccioneCombo(CboDictamina, "Dictamen");
                
                CargaInforGeneral();
                ClUtilitarios.LlenaCombo(ClCatalogos.GetListado_TipoInventario(), CboTipoInventario, "Tipo_InventarioId", "Tipo_Inventario");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoInventario, "Tipo de Inventario");
                ClUtilitarios.LlenaCombo(ClCatalogos.Listado_Parcela(), CboFormaParcela, "Forma_ParcelaId", "Forma_Parcela");
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Listado_TipoCalculoCopromiso(), CboAreaCompromiso, "Tipo_CalculoCompromisoId", "Tipo_CalculoCompromiso");
                ClUtilitarios.AgregarSeleccioneCombo(CboAreaCompromiso, "Tipo Calculo");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipo_Garantia(1), CboGarantia, "Tipo_GarantiaId", "Tipo_Garantia");
                ClUtilitarios.AgregarSeleccioneCombo(CboGarantia, "Tipo de Garantía");
                ClUtilitarios.LlenaCombo(ClCatalogos.Get_Listado_TipoUsuario_DicTecnico(), CboTipoUsuario, "Tipo_UsuarioIdDictemenTec", "Tipo_UsuarioDictamenTecnico");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoUsuario, "Tipo de Usuario");
            }
            else
            {
                if (TxtCantidadAutorizadas.Text != "" )
                {
                    if (Convert.ToInt32(TxtCantidadAutorizadas.Text) > 10)
                    {
                        DivAprueba12.Style.Add("display", "block");
                        DivAprueba11.Style.Add("display", "block");
                    }
                }
            }
        }

        void BtnEnviarEnmienda_Click(object sender, EventArgs e)
        {
            if (ValidaEnmiendas() == true)
            {
                TxtTipoSi.Text = "2";
                LblTitConfirmacion.Text = "La Gestion generará un informe de enmiendas?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TxtTipoSi.Text) == 1)
            {
                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Etapas");
                XmlNode iElementos = iInformacion.CreateElement("Etapas");
                for (int i = 0; i < GrdEtapa.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("EtapaId", GrdEtapa.Items[i].GetDataKeyValue("EtapaId"), iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    RadDatePicker TxtFecIni = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecIni"));
                    ClXml.AgregarAtributo("FecIni", TxtFecIni.SelectedDate, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);

                    RadDatePicker TxtFecFin = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecFin"));
                    ClXml.AgregarAtributo("FecFin", TxtFecFin.SelectedDate, iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);

                XmlDocument iMAderaPie = ClXml.CrearDocumentoXML("MaderaPie");
                XmlNode iElemenoMaderaPie = iMAderaPie.CreateElement("MaderaPie");
                for (int i = 0; i < GrdMaderaPie.Items.Count; i++)
                {
                    XmlNode iMaderaDetalle = iMAderaPie.CreateElement("Item");
                    ClXml.AgregarAtributo("Turno", GrdMaderaPie.Items[i]["Turno"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("Rodal", GrdMaderaPie.Items[i].GetDataKeyValue("Rodal"), iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("EspecieId", GrdMaderaPie.Items[i].GetDataKeyValue("EspecieId"), iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("VolTroza", GrdMaderaPie.Items[i]["VolTroza"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("VolLena", GrdMaderaPie.Items[i]["VolLena"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("VolTotal", GrdMaderaPie.Items[i]["VolTotal"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("ValTroza", GrdMaderaPie.Items[i]["ValTroza"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("ValLena", GrdMaderaPie.Items[i]["ValLena"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("ValPagar", GrdMaderaPie.Items[i]["ValPagar"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                    ClXml.AgregarAtributo("PorPagar", GrdMaderaPie.Items[i]["PorPagar"].Text, iMaderaDetalle);
                    iElemenoMaderaPie.AppendChild(iMaderaDetalle);

                }
                iMAderaPie.ChildNodes[1].AppendChild(iElemenoMaderaPie);
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                int Dictamen_Tecnico_Id = ClGestion.Max_Dictamen_Tecnico();
                int SubRegionId = ClGestion.Get_SubRegion_Gestion(GestionId);
                int Tamano = 0;
                int FormaParcelaId = 0;
                int NotasEntregar = 0;
                int NotasPendientes = 0;
                int TipoUSuarioIdDictamen = 0;
                if ((CboTipoInventario.SelectedValue == "2") || (CboTipoInventario.SelectedValue == "3"))
                {
                    Tamano = Convert.ToInt32(TxtSize.Text);
                    FormaParcelaId = Convert.ToInt32(CboFormaParcela.SelectedValue);
                }

                if (CboDictamina.SelectedValue == "1")
                {
                    if (Convert.ToInt32(TxtCantidadAutorizadas.Text) > 10)
                    {
                        NotasEntregar = Convert.ToInt32(TxtNotasEntregar.Text);
                        NotasPendientes = Convert.ToInt32(TXtCantidadRestante.Text);
                        TipoUSuarioIdDictamen = Convert.ToInt32(CboTipoUsuario.SelectedValue);
                    }

                }
                int AreaCompromiso = 0;
                int Garantia = 0;
                int CantidadAutorizada = 0;
                if (CboDictamina.SelectedValue == "1")
                {
                    AreaCompromiso = Convert.ToInt32(CboAreaCompromiso.SelectedValue);
                    Garantia = Convert.ToInt32(CboGarantia.SelectedValue);
                    CantidadAutorizada = Convert.ToInt32(TxtCantidadAutorizadas.Text);
                }


                ClGestion.Insert_Dictamen_Tecnico(Dictamen_Tecnico_Id, GestionId, TxtMetodologia.Text, TxtMetodologiaResultados.Text, Convert.ToInt32(CboTipoInventario.SelectedValue), Convert.ToInt32(TxtTotRodales.Text), Convert.ToInt32(TxtRodalesMuestreados.Text), Tamano, FormaParcelaId, TxtConclusionCaracBio.Text, TxtConclusionInventario.Text, TxtConcluManejo.Text, TxtCncluPropuesta.Text, Convert.ToInt32(CboDictamina.SelectedValue), AreaCompromiso, Garantia, iInformacion, iMAderaPie, Convert.ToInt32(TxtVigenciaApro.Text), CantidadAutorizada, NotasEntregar, NotasPendientes, TipoUSuarioIdDictamen, Convert.ToInt32(Session["UsuarioId"]), SubRegionId, TxtOtrasReco.Text);
                ClGestion.Manda_Gestion_Usuario_Validacion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11, 1);
                DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                string MensajeCorreo = "Se ha enviado a su despacho la gestión de Manejo Forestal";
                ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                dsDatosSubRegional.Clear();
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("8", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "");
            }
            else if (Convert.ToInt32(TxtTipoSi.Text) == 2)
            {
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                int Enmienda_Tecnico_Id = ClGestion.Max_Enmienda_Tecnico();
                int SubRegionId = ClGestion.Get_SubRegion_Gestion(GestionId);

                XmlDocument iInformacion = ClXml.CrearDocumentoXML("Enmiendas");
                XmlNode iElementos = iInformacion.CreateElement("Enmiendas");
                for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Enmineda", GrdEnmiendas.Items[i].GetDataKeyValue("Enmienda"), iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);

                DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                ClGestion.Insert_Enmiendas_Tecnico(Enmienda_Tecnico_Id, GestionId, Convert.ToInt32(dsDatosSubRegional.Tables["Datos"].Rows[0]["UsuarioId"].ToString()), Convert.ToInt32(Session["UsuarioId"]), iInformacion, SubRegionId);
                ClGestion.Manda_Gestion_Usuario_Validacion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11, 1);
                string MensajeCorreo = "Se ha enviado a su despacho la gestión de Manejo Forestal";
                ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                dsDatosSubRegional.Clear();
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("11", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "");

            }
            
        }

        void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                TxtTipoSi.Text = "1";
                LblTitConfirmacion.Text = "La Gestíon sera enviada al director subregional, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2, 2);
                int CategoriaId = ClGestion.Get_CategoriaRNFId(SubCategoriaId);

                DataRow item = DsDatosDictamen.Tables["DatosDictamen"].NewRow();
                item["MetodologiaCorro"] = TxtMetodologia.Text;
                item["MetodologiaInvForestal"] = TxtMetodologiaResultados.Text;
                if (CboTipoInventario.SelectedValue == "1")
                    item["FormaEvaluacion"] = "Forma de Evaluación: " + CboTipoInventario.Text + ", Total de Rodales: " + TxtTotRodales.Text + " Rodales Muestreados: " + TxtRodalesMuestreados.Text;
                else
                    item["FormaEvaluacion"] = "Forma de Evaluación: " + CboTipoInventario.Text + ", Tamaño y Forma de parcela: " + TxtSize.Text + " " + CboFormaParcela.Text + ",  Total de Rodales: " + TxtTotRodales.Text + " Rodales Muestreados: " + TxtRodalesMuestreados.Text;
                item["ConCaracBio"] = TxtConclusionCaracBio.Text;
                item["ConVeracidad"] = TxtConclusionInventario.Text;
                item["ConPropuestaManejo"] = TxtConcluManejo.Text;
                item["ConPropuestaTratamiento"] = TxtCncluPropuesta.Text;
                item["Dictamen"] = CboDictamina.Text;
                item["TipoGarantia"] = CboGarantia.Text;
                item["DictamenId"] = CboDictamina.SelectedValue;
                if (CboDictamina.SelectedValue == "1")
                {
                    item["MontoGarantia"] = TxtMonto.Text;
                    item["PorGarantia"] = TxtPorcentajeGarantia.Text;
                    item["TotValMaderaPie"] = TxtTotMaderaPie.Text;
                    item["RecomendacionUno"] = "7.1     Se recomienda que la vigencia del aprovechamiento sea de: " + TxtVigenciaApro.Text + ".";
                    if (Convert.ToInt32(TxtCantidadAutorizadas.Text) > 10)
                        item["RecomendacionDos"] = "7.1     Que se le autorice la venta total de " + TxtCantidadAutorizadas.Text + ". De estas, se le entregarán únicamente " + TxtNotasEntregar.Text + ", las otras " + TXtCantidadRestante.Text + " se adjuntarán al expediente, sin foliar, mismas que se entregarán luego al titular o su representante legal cuando presente Informe de uso de notas de envío.";
                    else
                        item["RecomendacionDos"] = "7.7     Que se le autorice la venta total de " + TxtCantidadAutorizadas.Text + ".";
                    if (Convert.ToDouble(LblAreaIntervenir.InnerText) > 100)
                        item["OtrasRecomendaciones"] = "7.9     " + TxtOtrasReco.Text;
                    else
                        item["OtrasRecomendaciones"] = "7.8     " + TxtOtrasReco.Text;
                }
                    
                
                DsDatosDictamen.Tables["DatosDictamen"].Rows.Add(item);

                
                for (int i = 0; i < GrdEtapa.Items.Count; i++)
                {
                    DataRow itemEtapa = DsEtapa.Tables["Etapa"].NewRow();
                    itemEtapa["EtapaId"] = GrdEtapa.Items[i].GetDataKeyValue("EtapaId");
                    itemEtapa["Etapa"] = GrdEtapa.Items[i].GetDataKeyValue("Etapa");
                    RadDatePicker TxtFecIni = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecIni"));
                    RadDatePicker TxtFecFin = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecFin"));
                    itemEtapa["FecIni"] = TxtFecIni.SelectedDate;
                    itemEtapa["Fecfin"] = TxtFecFin.SelectedDate;
                    DsEtapa.Tables["Etapa"].Rows.Add(itemEtapa);    
                }
                

                Session["DatosDictamenTec"] = ClGestion.ImpresionDictamenTecnio(GestionId, 1, CategoriaId, DsDatosDictamen, DsEtapa, Convert.ToInt32(Session["UsuarioId"]));
                RadWindow1.Title = "Vista Previa Dictamen Técnico";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenTecnico.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        bool Valida()
        {
            DivErrDictamenGen.Visible = false;
            bool HayError = false;
            string LblErr = "";
            if (TxtMetodologia.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe Ingresar la metodología utilizada para la corroboración del Inventario y Plan de Manejo Forestal";
                else
                    LblErr = LblErr  + ", debe Ingresar la metodología utilizada para la corroboración del Inventario y Plan de Manejo Forestal";
                HayError = true;
            }
            if (TxtMetodologiaResultados.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe Ingresar la metodología y los resultados de la comprobación del Inventario Forestal. ";
                else
                    LblErr = LblErr  + ", debe Ingresar la metodología y los resultados de la comprobación del Inventario Forestal. ";
                HayError = true;
            }
            if ((CboTipoInventario.SelectedValue == "") || (CboTipoInventario.SelectedValue == "0"))
            {
                if (LblErr == "")
                    LblErr = "Debe seleccionar la forma de evaluación";
                else
                    LblErr = LblErr + ", debe seleccionar la forma de evaluación";
                HayError = true;
            }
            
            if  (TxtTotRodales.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar el total de rodales";
                else
                    LblErr = LblErr + ", debe ingresar el total de rodales";
                HayError = true;
            }
            if (TxtRodalesMuestreados.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar los rodales muestreados";
                else
                    LblErr = LblErr + ", debe ingresar los rodales muestreados";
                HayError = true;
            }
            if ((CboTipoInventario.SelectedValue == "2") && (TxtSize.Text == ""))
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar el tamaño de la evaluación";
                else
                    LblErr = LblErr + ", debe ingresar el tamaño de la evaluación";
                HayError = true;
            }
            if ((CboTipoInventario.SelectedValue == "2")  && ((CboFormaParcela.SelectedValue == "") || (CboFormaParcela.SelectedValue == "0")))
            {
                if (LblErr == "")
                    LblErr = "Debe seleccionar la forma de parcela";
                else
                    LblErr = LblErr + ", debe seleccionar la forma de parcela";
                HayError = true;
            }
            if (TxtConclusionCaracBio.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar la conclusión sobre las características biofísicas. ";
                else
                    LblErr = LblErr + ", debe ingresar la conclusión sobre las características biofísicas";
                HayError = true;
            }
            if (TxtConclusionInventario.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar la conclusión sobre los resultados y veracidad de la información presentada en el inventario forestal, estratificación y/o rodalización del bosque. ";
                else
                    LblErr = LblErr + ", debe ingresar la conclusión sobre los resultados y veracidad de la información presentada en el inventario forestal, estratificación y/o rodalización del bosque";
                HayError = true;
            }
            if (TxtConcluManejo.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar la conclusión sobre la propuesta de manejo forestal, haciendo especial énfasis sobre la Corta Anual Permisible ";
                else
                    LblErr = LblErr + ", debe ingresar la conclusión sobre la propuesta de manejo forestal, haciendo especial énfasis sobre la Corta Anual Permisible";
                HayError = true;
            }
            if (TxtCncluPropuesta.Text == "")
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar la conclusión sobre la propuesta de tratamiento integrando características biofísicas, inventario forestal.";
                else
                    LblErr = LblErr + ", debe ingresar la conclusión sobre la propuesta de tratamiento integrando características biofísicas, inventario forestal.";
                HayError = true;
            }
            if ((CboDictamina.SelectedValue == "1") && ((CboAreaCompromiso.SelectedValue == "") || (CboAreaCompromiso.SelectedValue == "0")))
            {
                if (LblErr == "")
                    LblErr = "Debe seleccionar el tipo de calculo de compromiso";
                else
                    LblErr = LblErr + ", debe seleccionar el tipo de calculo de compromiso";
                HayError = true;
            }
            if ((CboDictamina.SelectedValue == "1") &&  ((CboGarantia.SelectedValue == "") || (CboGarantia.SelectedValue == "0")))
            {
                if (LblErr == "")
                    LblErr = "Debe seleccionar el tipo de garantía";
                else
                    LblErr = LblErr + ", debe seleccionar el tipo de garantía";
                HayError = true;
            }
            if ((CboDictamina.SelectedValue == "1") &&  (TxtCantidadAutorizadas.Text == ""))
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar la cantidad de notas autorizadas";
                else
                    LblErr = LblErr + ", debe ingresar la cantidad de notas autorizadas";
                HayError = true;
            }
            if ((CboDictamina.SelectedValue == "1") && ((Convert.ToInt32(TxtCantidadAutorizadas.Text) > 10) && ((CboTipoUsuario.SelectedValue == "") || (CboTipoUsuario.SelectedValue == "0"))))
            {
                if (LblErr == "")
                    LblErr = "Debe seleccionar el tipo de usuario";
                else
                    LblErr = LblErr + ", debe seleccionar el tipo de usuario";
                HayError = true;
            }
            if ((CboDictamina.SelectedValue == "1") &&  (TxtOtrasReco.Text == ""))
            {
                if (LblErr == "")
                    LblErr = "Debe ingresar otras recomendaciones";
                else
                    LblErr = LblErr + ", debe ingresar otras recomendaciones";
                HayError = true;
            }
            string MensajeEtapa = "";
            if ((CboDictamina.SelectedValue == "1") &&  (ValidaEtapas(ref MensajeEtapa) == false))
            {
                if (LblErr == "")
                    LblErr = MensajeEtapa;
                else
                    LblErr = LblErr + ", " + MensajeEtapa;
                HayError = true;
            }
            if (HayError == true)
            {
                LblErrDictamenGen.Text = LblErr;
                DivErrDictamenGen.Visible = true;
                return false;
            }
            else
            {
                
                return true;
            }
                
            
        }

        bool ValidaEtapas(ref string Mensaje)
        {
            bool Valido = true;
            for (int i = 0; i < GrdEtapa.Items.Count; i++)
            {
                RadDatePicker TxtFecIni = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecIni"));
                if (TxtFecIni.DateInput.Text == "")
                {
                    if (Mensaje == "")
                        Mensaje = "No ha ingresado todas las fechas de inicio de las etapas";
                    else
                        Mensaje = Mensaje + "No ha ingresado todas las fechas de inicio de las etapas";
                    Valido = false;
                    break;
                }
                RadDatePicker TxtFecFin = ((RadDatePicker)GrdEtapa.Items[i].FindControl("TxtFecFin"));
                if (TxtFecFin.DateInput.Text == "")
                {
                    if (Mensaje == "")
                        Mensaje = "No ha ingresado todas las fechas de finalización de las etapas";
                    else
                        Mensaje = Mensaje + "No ha ingresado todas las fechas de finalización de las etapas";
                    Valido = false;
                    break;

                }
                if ((TxtFecFin.DateInput.Text != "") && (TxtFecFin.SelectedDate.Value.Day != 31) || (TxtFecFin.SelectedDate.Value.Month != 10))
                {
                    if (Mensaje == "")
                        Mensaje = "La fecha de finalización debe ser el 31 de Octubre";
                    else
                        Mensaje = Mensaje + " la fecha de finalización debe ser el 31 de Octubre";
                    Valido = false;
                    break;
                }
            }
            return Valido;
        }

        public void CboTipoUsuario_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboTipoUsuario.SelectedValue == "1")
            {
                TxtNotasEntregar.Text = Math.Round(((Convert.ToDouble(TxtCantidadAutorizadas.Text) / 100)) * 75, 0).ToString();
                TXtCantidadRestante.Text = (Convert.ToInt32(TxtCantidadAutorizadas.Text) - Convert.ToInt32(TxtNotasEntregar.Text)).ToString();
            }
            else if (CboTipoUsuario.SelectedValue == "2")
            {
                TxtNotasEntregar.Text = Math.Round(((Convert.ToDouble(TxtCantidadAutorizadas.Text) / 100)) * 50, 0).ToString();
                TXtCantidadRestante.Text = (Convert.ToInt32(TxtCantidadAutorizadas.Text) - Convert.ToInt32(TxtNotasEntregar.Text)).ToString();
            }
            else if (CboTipoUsuario.SelectedValue == "3")
            {
                TxtNotasEntregar.Text = Math.Round(((Convert.ToDouble(TxtCantidadAutorizadas.Text) / 100)) * 25, 0).ToString();
                TXtCantidadRestante.Text = (Convert.ToInt32(TxtCantidadAutorizadas.Text) - Convert.ToInt32(TxtNotasEntregar.Text)).ToString();
            }
            else
            {
                TxtNotasEntregar.Text = ((Convert.ToInt32(TxtCantidadAutorizadas.Text) / 100) * 100).ToString();
                TXtCantidadRestante.Text = (Convert.ToInt32(TxtCantidadAutorizadas.Text) - Convert.ToInt32(TxtNotasEntregar.Text)).ToString();
            }
            
        }

        void GrdMaderaPie_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in this.GrdMaderaPie.Items)
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
                        }
                    }
                }
            }
        }

        void GrdMaderaPie_ItemDataBound(object sender, GridItemEventArgs e)
        {
            TxtTotMaderaPie.Text = "";
            
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                GridDataItem item = e.Item as GridDataItem;
                DataSet DsDatosExtrae = ClManejo.Get_Dato_Silvicultura_Extrae_PlanManejo(GestionId, Convert.ToInt32(item.GetDataKeyValue("Correlativo")));
                if (DsDatosExtrae.Tables["Datos"].Rows.Count > 0)
                {
                    item["Turno"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Turno"].ToString();
                    item["VolTroza"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTroza"].ToString();
                    item["VolLena"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolLena"].ToString();
                    item["VolTotal"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTotal"].ToString();
                    DataSet ValorMadera = ClEspecie.Valor_MaderaPie_Especie(Convert.ToInt32(item.GetDataKeyValue("EspecieId")), GestionId);
                    if (ValorMadera.Tables["Datos"].Rows.Count > 0)
                    {
                        item["ValTroza"].Text = (Convert.ToDouble(item["VolTroza"].Text) * Convert.ToDouble(ValorMadera.Tables["Datos"].Rows[0]["VolTroza"])).ToString();
                        item["ValLena"].Text = (Convert.ToDouble(item["VolLena"].Text) * Convert.ToDouble(ValorMadera.Tables["Datos"].Rows[0]["VolLena"])).ToString();
                        item["ValPagar"].Text = (Convert.ToDouble(item["ValLena"].Text) + Convert.ToDouble(item["ValTroza"].Text)).ToString();
                        item["PorPagar"].Text = (((Convert.ToDouble(item["ValPagar"].Text) / 100) * 10)).ToString();
                        Total = Total + ((Convert.ToDouble(item["ValPagar"].Text) / 100) * 10);
                    }
                    else
                    {
                        item["ValTroza"].Text = "0";
                        item["ValLena"].Text = "0";
                        item["ValPagar"].Text = "0";
                        item["PorPagar"].Text = "0";
                    }
                    ValorMadera.Clear();
                }
            }
            TxtTotMaderaPie.Text = Total.ToString();
        }

        void GrdMaderaPie_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.Get_Resumen_Censo(2, Convert.ToInt32(GestionId)), GrdMaderaPie);
        }

        void CboGarantia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet DsGarantia = ClCatalogos.Sp_Get_Monto_Garantia(Convert.ToInt32(CboGarantia.SelectedValue));
            TxtMonto.Text = (Convert.ToInt32(TxtAreaCompromiso.Text) * Convert.ToDouble(DsGarantia.Tables["Datos"].Rows[0]["Valor_Hectaria"])).ToString();
            TxtPorcentajeGarantia.Text = DsGarantia.Tables["Datos"].Rows[0]["Porcentaje"].ToString();
            DsGarantia.Clear();
        }

        void GrdEtapa_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.Get_Etapas_Compromiso(GestionId), GrdEtapa);
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
                        if (item2["Rodal"].Text == item3["Rodal"].Text)
                        {
                            item2["Rodal"].RowSpan = (item3["Rodal"].RowSpan < 2) ? 2 : (item3["Rodal"].RowSpan + 1);
                            item3["Rodal"].Visible = false;

                            item2["AreaRodal"].RowSpan = (item3["AreaRodal"].RowSpan < 2) ? 2 : (item3["AreaRodal"].RowSpan + 1);
                            item3["AreaRodal"].Visible = false;
                            item2["Edad"].RowSpan = (item3["Edad"].RowSpan < 2) ? 2 : (item3["Edad"].RowSpan + 1);
                            item3["Edad"].Visible = false;
                        }
                    }
                }
            }
        }

        void GrdSilvicultural_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                GridDataItem item = e.Item as GridDataItem;
                DataSet DsDatosExtrae = ClManejo.Get_Dato_Silvicultura_Extrae_PlanManejo(GestionId,Convert.ToInt32(item.GetDataKeyValue("Correlativo")));
                if (DsDatosExtrae.Tables["Datos"].Rows.Count > 0)
                {
                    item["Turno"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Turno"].ToString();
                    item["Tratamiento"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Tratamiento"].ToString();
                    item["Otro"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["Otro"].ToString();
                    item["VolTroza"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTroza"].ToString();
                    item["VolLena"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolLena"].ToString();
                    item["VolTotal"].Text = DsDatosExtrae.Tables["Datos"].Rows[0]["VolTotal"].ToString();
                }
            }
        }

        void GrdSilvicultural_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.Get_Resumen_Censo(2, Convert.ToInt32(GestionId)), GrdSilvicultural);
        }

        void GrdBoleta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.Get_Boleta_Dictamen_Tecnico(GestionId), GrdBoleta);

        }

        bool ValidaCargaboleta()
        {
            LblErrCargaCenso.Text = "";
            DivErrCargaCenso.Visible = false;
            DivGoodCargaCenso.Visible = false;
            bool HayError = false;
            if (CboTipoInventario.SelectedValue == "")
            {
                if (LblErrCargaCenso.Text == "")
                    LblErrCargaCenso.Text = LblErrCargaCenso.Text + "Debe Seleccionar el tipo de Inventario";
                else
                    LblErrCargaCenso.Text = LblErrCargaCenso.Text + ", debe Seleccionar el tipo de Inventario";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrCargaCenso.Visible = true;
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
                    int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    ClManejo.Elimina_Boleta_Dictamen_Tecnico(GestionId);
                    XmlDocument iInformacionBoleta = ClXml.CrearDocumentoXML("Boleta");
                    XmlNode iElementosBoleta = iInformacionBoleta.CreateElement("Boleta");
                    foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                    {
                        if (iDtRow["Rodal"].ToString() != "")
                        {
                            if (Convert.ToInt32(ClManejo.Existe_Especie(iDtRow["NOMBRE_CIENTIFICO"].ToString())) > 0)
                            {
                                int X = 0;
                                int Y = 0;
                                if (Convert.ToInt32(iDtRow["X"].ToString().Length) > 0)
                                    X = Convert.ToInt32(iDtRow["X"]);
                                if (Convert.ToInt32(iDtRow["Y"].ToString().Length) > 0)
                                    Y = Convert.ToInt32(iDtRow["Y"]);

                                XmlNode iElementoDetalleBoleta = iInformacionBoleta.CreateElement("Item");

                                if (CboTipoInventario.SelectedValue == "1")
                                {
                                    ClXml.AgregarAtributo("Turno", Convert.ToInt32(iDtRow["Turno"]), iElementoDetalleBoleta);
                                    iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                }
                                else
                                {
                                    ClXml.AgregarAtributo("Turno", 0, iElementoDetalleBoleta);
                                    iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                }
                                ClXml.AgregarAtributo("Rodal", Convert.ToInt32(iDtRow["Rodal"]), iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("No", Convert.ToInt32(iDtRow["No"]), iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("Dap", Convert.ToInt32(iDtRow["Dap"]), iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("Altura", Convert.ToInt32(iDtRow["Altura"]), iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                int EspecieId = ClEspecie.Get_EspecieId(iDtRow["NOMBRE_CIENTIFICO"].ToString());
                                ClXml.AgregarAtributo("EspecieId", EspecieId, iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("Troza", Convert.ToDouble(iDtRow["%_TROZA"]), iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("X", X, iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("Y", Y, iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                ClXml.AgregarAtributo("Volumen", Y, iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                double VolumenTroza = ClEspecie.Get_Volumen_Especie_Boleta(EspecieId, Convert.ToDouble(iDtRow["Dap"]), Convert.ToDouble(iDtRow["Altura"]));
                                ClXml.AgregarAtributo("Volumen", VolumenTroza, iElementoDetalleBoleta);
                                iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                
                                
                                if (CboTipoInventario.SelectedValue == "1")
                                {
                                    ClXml.AgregarAtributo("Parcela", 0, iElementoDetalleBoleta);
                                    iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                }
                                else
                                {
                                    ClXml.AgregarAtributo("Parcela", Convert.ToInt32(iDtRow["Parcela"]), iElementoDetalleBoleta);
                                    iElementosBoleta.AppendChild(iElementoDetalleBoleta);
                                }
                                iInformacionBoleta.ChildNodes[1].AppendChild(iElementosBoleta);
                            }

                        }

                    }
                    ClManejo.InsertBoleta_Dictamen_Tecnico(GestionId, iInformacionBoleta);
                    DivGoodCargaCenso.Visible = true;
                    LblGoodCargaCenso.Text = "Archivo Cargado exitosamente";
                }
                catch (Exception ex)
                {
                    String iM = ex.Message;

                    //pMensaje();

                }
            }
        }

        void 
            CboDictamina_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboDictamina.SelectedValue == "1")
            {
                DivAprueba.Visible = true;
                DivAprueba2.Visible = true;
                DivAprueba3.Visible = true;
                DivAprueba4.Visible = true;
                DivAprueba5.Visible = true;
                DivAprueba6.Visible = true;
                DivAprueba7.Visible = true;
                DivAprueba8.Visible = true;
                DivAprueba9.Visible = true;
                DivAprueba10.Visible = true;
                
                DivAprueba13.Visible = true;
                GrdSilvicultural.Rebind();
                CargaDatosAprueba();
                GrdEtapa.Rebind();
                GrdMaderaPie.Rebind();
            }
            else
            {
                DivAprueba.Visible = false;
                DivAprueba2.Visible = false;
                DivAprueba3.Visible = false;
                DivAprueba4.Visible = false;
                DivAprueba5.Visible = false;
                DivAprueba6.Visible = false;
                DivAprueba7.Visible = false;
                DivAprueba8.Visible = false;
                DivAprueba9.Visible = false;
                DivAprueba10.Visible = false;
                DivAprueba11.Visible = false;
                DivAprueba12.Visible = false;
                DivAprueba13.Visible = false;
            }
            DivDictamen.Visible = true;
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
                            item2["Clase_Desarrollo"].RowSpan = (item3["Clase_Desarrollo"].RowSpan < 2) ? 2 : (item3["Clase_Desarrollo"].RowSpan + 1);
                            item3["Clase_Desarrollo"].Visible = false;
                            item2["Edad"].RowSpan = (item3["Edad"].RowSpan < 2) ? 2 : (item3["Edad"].RowSpan + 1);
                            item3["Edad"].Visible = false;
                            
                            
                            item2["Pendiente"].RowSpan = (item3["Pendiente"].RowSpan < 2) ? 2 : (item3["Pendiente"].RowSpan + 1);
                            item3["Pendiente"].Visible = false;
                            item2["INC"].RowSpan = (item3["INC"].RowSpan < 2) ? 2 : (item3["INC"].RowSpan + 1);
                            item3["INC"].Visible = false;
                            

                        }
                    }
                }
            }
        }

        void GrdResumen_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.Get_Resumen_Censo(2, Convert.ToInt32(GestionId)), GrdResumen);
        }

        void CboTipoInventario_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboTipoInventario.SelectedValue == "1")
            {
                DivEvalCenso.Visible = true;
                DivEvalMuestreo.Visible = false;
                LbltitPanCenso.Text = "Censo";
                LblCargueCenso.Text = "Cargue el Censo";
            }
            else if (CboTipoInventario.SelectedValue == "2")
            {
                DivEvalCenso.Visible = true;
                DivEvalMuestreo.Visible  = true;
                LbltitPanCenso.Text = "Muestreo";
                LblCargueCenso.Text = "Cargue el Muestreo";
            }
            else
            {
                DivEvalCenso.Visible = false;
                DivEvalMuestreo.Visible = false;
            }
        }

        bool ValidaEnmiendas()
        {
            DivErrEnmineda.Visible = false;
            
            bool Valida = true;
            if (GrdEnmiendas.Items.Count <= 0)
            {
                Valida = false;
                DivErrEnmineda.Visible = true;
                LblErrEnmienda.Text = "Debe ingresar al menos una enmienda";
            }
            return Valida;
        }

        void BtVistaPreviaEnminada_Click(object sender, EventArgs e)
        {
            CargaDataSetEnmienda();
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            if (ValidaEnmiendas() == true)
            {
                RadWindow1.Title = "Vista Previa Solicitud de Emiendas";
                DataSet DsEnmiendasTec = ClManejo.Sp_Get_Enmiendas_Tec(GestionId, 1);
                if (DsEnmiendasTec.Tables["Datos"].Rows.Count > 0)
                {
                    Ds_Gestiones Ds_Enmiendas_Tec = new Ds_Gestiones();
                    Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].Clear();
                    int ModuloId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true));
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, ModuloId);
                    int CategoriaId = ClGestion.Get_CategoriaManejoId(SubCategoriaId);
                    string Solicitante = "";
                    Solicitante = ClGestion.Get_Propietarios_Manejo(GestionId);
                    string AgraegadoSol = ClGestion.Get_CompletaPropietarios(CategoriaId, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), ModuloId);
                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";
                    string Cuerpo = "Por este medio le informo que luego de la revisión, análisis e inspección realizada del expediente No. " + DsEnmiendasTec.Tables["Datos"].Rows[0]["No_Expediente"] + " cuyo (s) propietario (s) es (son): " + Solicitante + " quien solicita la implementación de Plan de Manejo para " + ClGestion.Get_SubCategoriaManejo(SubCategoriaId) + " en la (s) finca (s): " + ClGestion.GetDatosFinca_Gestion_Juntos(GestionId) + ". Dicho Plan contiene inventario y plan de manejo elaborado por " + DsEnmiendasTec.Tables["Datos"].Rows[0]["Regente"] + ", con Registro de Elaborador de Plan de Manejo No: " + DsEnmiendasTec.Tables["Datos"].Rows[0]["Correlativo"] + ", se determinó que es necesario completar el mismo con los datos siguientes:";
                    for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
                    {
                        DataRow row = Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].NewRow();
                        row["SubRegion"] = DsEnmiendasTec.Tables["Datos"].Rows[0]["Sub_Region"];
                        row["InformeNo"] = "";
                        row["LugarSubRegion"] = DsEnmiendasTec.Tables["Datos"].Rows[0]["Municipio"] + ", " + DsEnmiendasTec.Tables["Datos"].Rows[0]["Departamento"];
                        row["Fecha"] = DateTime.Now;
                        row["Subregional"] = DsEnmiendasTec.Tables["Datos"].Rows[0]["SubRegional"];
                        row["Cuerpo"] = Cuerpo;
                        row["No"] = (i + 1).ToString() + ".";
                        row["Enmienda"] = GrdEnmiendas.Items[i].GetDataKeyValue("Enmienda");
                        row["Tecnico"] = ClPersona.Nombre_Usuario(ClPersona.GetPersonaId(Convert.ToInt32(Session["UsuarioId"])));
                        Ds_Enmiendas_Tec.Tables["Dt_EnmiendasTec"].Rows.Add(row);        
                    }
                    Session["DatosEnmiendasTecnicas"] = Ds_Enmiendas_Tec;
                }
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepEnmiendasTec.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);


                
                
            }
        }

        void GrdEnmiendas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarEnmienda(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Enmienda"]));
                TxtOtra.Text = "";
                TxtIdEnmienda.Text = "";

            }
        }

        void EliminarEnmienda(int Item)
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                if (Item != Convert.ToInt32(GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Id_Enmienda"]))
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].NewRow();
                    row["Id_Enmienda"] = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Count;
                    row["EnmiendaTecId"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["EnmiendaTecId"];
                    row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                    row["Otra"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Otra"];
                    Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Add(row);
                }
            }
            GrdEnmiendas.Rebind();
        }

        void GrdEnmiendas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdEnmiendas, "Dt_Enmienda_Dictamen_Tec");
        }

        void BtnAddEnmienda_ServerClick(object sender, EventArgs e)
        {
            DivErrEnmineda.Visible = false;
            if ((CboEnmienda.SelectedValue == "") || (CboEnmienda.SelectedValue == "0"))
            {
                DivErrEnmineda.Visible = true;
                LblErrEnmienda.Text = "Debe seleccionar una enmienda";
            }
            else
            {
                AgregarEnmienda(CboEnmienda.SelectedValue, CboEnmienda.Text, TxtOtra.Text);
                GrdEnmiendas.Rebind();
                TxtOtra.Text = "";
                TxtIdEnmienda.Text = "";
            }
        }

        bool ExisteEnmienda(int EnmiendaTecId)
        {
            bool Existe = false;
            if (EnmiendaTecId != 5)
            {
                for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
                {
                    if (EnmiendaTecId == Convert.ToInt32(GrdEnmiendas.Items[i].GetDataKeyValue("EnmiendaTecId")))
                    {
                        Existe = true;
                        break;
                    }
                }
            }
            return Existe;
        }

        void AgregarEnmienda(string EnmiendaTecId, string Enmienda, string Otra)
        {
            CargaDataSetEnmienda();
            if (ExisteEnmienda(Convert.ToInt32(EnmiendaTecId)) == true)
            {
                DivErrEnmineda.Visible = true;
                LblErrEnmienda.Text = "Ya agrego esta enmienda";
            }
            else
            {
                DataRow rowNew = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].NewRow();
                rowNew["Id_Enmienda"] = GrdEnmiendas.Items.Count;
                rowNew["EnmiendaTecId"] = EnmiendaTecId;
                if (EnmiendaTecId == "5")
                    rowNew["Enmienda"] = Otra;
                else
                    rowNew["Enmienda"] = Enmienda;
                rowNew["Otra"] = Otra;
                Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Add(rowNew);
            }
            
        }

        void CargaDataSetEnmienda()
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].NewRow();
                row["Id_Enmienda"] = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Count;
                row["EnmiendaTecId"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["EnmiendaTecId"];
                row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                row["Otra"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Otra"];
                Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Tec"].Rows.Add(row);
            }
        }

        void CboEnmienda_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboEnmienda.SelectedValue == "5")
                DivOtra.Visible = true;
            else
                DivOtra.Visible = false;
        }

        void OptEnmiendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptEnmiendas.SelectedItem.Value == "1")
            {
                DivEnmiendas.Visible = true;
                DivEnmiendaGrid.Visible = true;
                DivEnmiendasBotonos.Visible = true;
                DivDictamenTec.Visible = false;
            }
            else
            {
                DivEnmiendas.Visible = false;
                DivEnmiendaGrid.Visible = false;
                DivEnmiendasBotonos.Visible = false;
                DivDictamenTec.Visible = true;
            }
        }

        void GrdInmuebles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.GetFincaPlanManejoPol(GestionId, 2), GrdInmuebles);
        }

        void CargaDatosAprueba()
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            TxtAreaCompromiso.Text = ClManejo.Get_Compromiso_Area(GestionId).ToString();
            string Especies = "";
            DataSet EspeciesCompromiso = ClManejo.Get_Especies_Compromiso(GestionId);
            for (int i = 0; i < EspeciesCompromiso.Tables["Datos"].Rows.Count; i++)
            {
                if (Especies == "")
                    Especies = EspeciesCompromiso.Tables["Datos"].Rows[i]["Codigo_Especie"].ToString();
                else
                    Especies = Especies + ", " + EspeciesCompromiso.Tables["Datos"].Rows[i]["Codigo_Especie"].ToString();
            }
            EspeciesCompromiso.Clear();
            TxtEspeciesCompromiso.Text = Especies;
            TxtDensidadInicial.Text = ClManejo.Get_DensidadIni_Compromiso(GestionId).ToString();
            string SistemaRepoblacionText = "";
            DataSet SistemaRepoblacion = ClManejo.Get_SistemaRepoblacion_Compromiso(GestionId);
            for (int i = 0; i < SistemaRepoblacion.Tables["Datos"].Rows.Count; i++)
            {
                if (SistemaRepoblacionText == "")
                    SistemaRepoblacionText = SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
                else
                    SistemaRepoblacionText = SistemaRepoblacionText + ", " + SistemaRepoblacion.Tables["Datos"].Rows[i]["SistemaRepoblacion"].ToString();
            }
            SistemaRepoblacion.Clear();
            TxtSistemaRepoblacion.Text = SistemaRepoblacionText;
            TxtLugarFinca.Text = ClGestion.GetDatosFinca_Gestion_Juntos(GestionId);
            DataSet Garantia = ClManejo.Get_TipoGarantiaPlanManejo(GestionId);
            CboGarantia.SelectedValue = Garantia.Tables["Datos"].Rows[0]["Tipo_GarantiaId"].ToString();
            CboGarantia.Text = Garantia.Tables["Datos"].Rows[0]["Tipo_Garantia"].ToString();
            Garantia.Clear();
            DataSet DsGarantia = ClCatalogos.Sp_Get_Monto_Garantia(Convert.ToInt32(CboGarantia.SelectedValue));
            TxtMonto.Text = (Convert.ToInt32(TxtAreaCompromiso.Text) * Convert.ToDouble(DsGarantia.Tables["Datos"].Rows[0]["Valor_Hectaria"])).ToString();
            TxtPorcentajeGarantia.Text = DsGarantia.Tables["Datos"].Rows[0]["Porcentaje"].ToString();
            DsGarantia.Clear();

        }

        void CargaInforGeneral()
        {
            int ModuloId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true));
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, ModuloId);
            int CategoriaId = ClGestion.Get_CategoriaManejoId(SubCategoriaId);
            lblTipoPlan.InnerText = ClGestion.Get_SubCategoriaManejo(SubCategoriaId);
            string Solicitante = ClGestion.Get_Propietarios_Manejo(GestionId);
            string AgraegadoSol = ClGestion.Get_CompletaPropietarios(CategoriaId, GestionId, ModuloId);
            if (AgraegadoSol != "")
                Solicitante = Solicitante + " " + AgraegadoSol + ".";
            else
                Solicitante = Solicitante + ".";
            LblPropietarios.InnerText = Solicitante;
            DataSet dsAreas = ClManejo.Get_Areas_PlanManejo(GestionId);
            LblAreaBosque.InnerText = dsAreas.Tables["Datos"].Rows[0]["AreaBosque"].ToString();
            LblAreaIntervenir.InnerText = dsAreas.Tables["Datos"].Rows[0]["AreaIntervenir"].ToString();
            LblAreaProteccion.InnerText = dsAreas.Tables["Datos"].Rows[0]["AreaProteccion"].ToString();
            dsAreas.Clear();
            DataSet dsCaracBio = ClManejo.Get_CaracBiofisicas(GestionId);
            LblZonaVida.InnerText = dsCaracBio.Tables["Datos"].Rows[0]["Zona_Vida"].ToString();
            if (SubCategoriaId == 4)
                LblFuentesAgua.InnerText = "N/A";
            dsCaracBio.Clear();
            DataSet dsDatos = ClManejoImpresion.Get_CategoriaCompletaPlanManejo(GestionId, 2);
            LblNomElaborador.InnerText = dsDatos.Tables["Datos"].Rows[0]["Regente"].ToString();
            LblCodigo.InnerText = dsDatos.Tables["Datos"].Rows[0]["CodReg"].ToString();
            dsDatos.Clear();
        }

        void ImgVerProvidencia_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Providencia para traslado de Expediente";
            string ProvidenciaId = ClGestion.Get_No_Providencia(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
            Session["DatosProvidencia"] = DatosProvidencia;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgVerinfo_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                {
                    DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    Ds_Profesionales Ds_Inscripcion = new Ds_Profesionales();
                    Ds_Inscripcion.Tables["Dt_Formulario"].Clear();
                    DataRow row = Ds_Inscripcion.Tables["Dt_Formulario"].NewRow();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "1")
                        row["Requisitos"] = "Requisitos:\n1) Copia legalizada del título;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU); y  \n3) Copia de documento personal de identificación (DPI).";
                    else if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "2")
                        row["Requisitos"] = "Requisitos:\n1) Constancia original de colegiado activo vigente;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU);   y \n3) Copia de documento personal de identificación (DPI).\nPara profesionales con post grado en materia forestal, presentar el documento extendido por la universidad que lo avala.";
                    if (ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "1" || ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "16")
                        row["Requisitos"] = row["Requisitos"] + "\nademás de los requisitos anteriores, copia del diploma de aprobación del curso correspondiente.";
                    row["Region"] = ds.Tables["Datos"].Rows[0]["region"].ToString();
                    row["SubRegion"] = ds.Tables["Datos"].Rows[0]["Subregion"].ToString();
                    row["NUG"] = ds.Tables["Datos"].Rows[0]["NUG"].ToString();
                    row["Fecha"] = ds.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString();
                    row["Actividad"] = ds.Tables["Datos"].Rows[0]["Nombre_Subcategoria"].ToString();
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 16))
                        row["ActividadId"] = 1;
                    else
                        row["ActividadId"] = 0;
                    row["Nombres"] = ds.Tables["Datos"].Rows[0]["Nombres"].ToString();
                    row["Apellidos"] = ds.Tables["Datos"].Rows[0]["Apellidos"].ToString();
                    row["DPI"] = ds.Tables["Datos"].Rows[0]["DPI"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "")
                        row["NIT"] = "";
                    else
                        row["NIT"] = ds.Tables["Datos"].Rows[0]["NIT"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["telefono"].ToString() == "")
                        row["Telefono"] = "";
                    else
                        row["Telefono"] = ds.Tables["Datos"].Rows[0]["telefono"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["celular"].ToString() == "")
                        row["Celular"] = "";
                    else
                        row["Celular"] = ds.Tables["Datos"].Rows[0]["celular"].ToString();
                    row["Correo"] = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                    row["Direccion"] = ds.Tables["Datos"].Rows[0]["Direccion"].ToString();
                    row["Municipio"] = ds.Tables["Datos"].Rows[0]["MunVivienda"].ToString();
                    row["Departamento"] = ds.Tables["Datos"].Rows[0]["DepaVivienda"].ToString();
                    row["CategoriaProfesion"] = ds.Tables["Datos"].Rows[0]["CategoriaProfesion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString() == "1")
                        row["No_Colegiado"] = "---------------";
                    else
                        row["No_Colegiado"] = ds.Tables["Datos"].Rows[0]["No_Colegiado"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "1" || ds.Tables["Datos"].Rows[0]["SubCategoriaId"].ToString() == "16")
                        row["No_Diploma"] = ds.Tables["Datos"].Rows[0]["No_Diploma"].ToString();
                    else
                        row["No_Diploma"] = "-----------------";
                    row["Direccion_Notifica"] = ds.Tables["Datos"].Rows[0]["Direccion_Notificacion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["Aldea_Notificacion"].ToString() == "")
                        row["Aldea_Notifica"] = "";
                    else
                        row["Aldea_Notifica"] = ds.Tables["Datos"].Rows[0]["Aldea_Notificacion"].ToString();
                    row["Municipio_Notifica"] = ds.Tables["Datos"].Rows[0]["MunNotifica"].ToString();
                    row["Departamento_Notifica"] = ds.Tables["Datos"].Rows[0]["DepNotifica"].ToString();
                    row["Observaciones"] = ds.Tables["Datos"].Rows[0]["Observaciones"].ToString();
                    row["Nombre"] = ds.Tables["Datos"].Rows[0]["Nombre_Firma"].ToString();
                    row["Profesion"] = ds.Tables["Datos"].Rows[0]["Profesion"].ToString();
                    if (ds.Tables["Datos"].Rows[0]["Origen_PersonaId"].ToString() == "1")
                        row["LabelId"] = "2.3 Número de DPI:";
                    else
                        row["LabelId"] = "2.3 Número de Pasaporte:";
                    Ds_Inscripcion.Tables["Dt_Formulario"].Rows.Add(row);
                    ds.Tables.Clear();
                    Session["DataFormulario"] = Ds_Inscripcion;
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioProfesional.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("00", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }

            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                RadWindow1.Title = "Plan de Manejo Forestal";
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2, 2);
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void IngVerAnexos_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                if (ClGestion.Tiene_Anexos_Inventerio(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) == 1)
                {
                    //Llamada 0 = PV, AF 1 = SAF
                    int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int Categoria = ClGestion.Get_CategoriaRNFId(Actividad);
                    int Tipo = 0;
                    if ((Categoria == 2) || (Categoria == 3))
                        Tipo = 0;
                    else if (Categoria == 4)
                        Tipo = 1;
                    else if (Categoria == 6)
                        Tipo = 2;

                    Session["Datos_InventarioForestal"] = ClGestion.Impresion_Inventario_Forestal(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Tipo);
                    RadWindow1.Title = "Inventario Forestal";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepInventarioForestal.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Tipo.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                if (ClGestion.Tiene_Anexos_Poligono(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) == 1)
                {
                    int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    string url = "";
                    //url = "/Mapas/MenuMapas.aspx?Id=" + Id;
                    url = "/Segefor_new/Mapas/MenuMapas.aspx?Id=" + Id;
                    string popupScript = "window.open('" + url + "', 'popup_window', 'left=100,top=100,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", popupScript, true);
                }
            }
            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                int Id = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                string GestionNo = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                String js = "window.open('Wfrm_AnexosPlanManejo.aspx?idgestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Id.ToString(), true)) + "&NUG=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionNo.ToString(), true)) + "', '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
            }
        }


    }
}