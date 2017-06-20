﻿using SEGEFOR.Clases;
using SEGEFOR.Data_Set;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Seguimiento_Regional : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        Cl_Xml ClXml;
        Cl_Manejo ClManejo;
        Cl_Catalogos ClCatalogos;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();
            ClXml = new Cl_Xml();
            ClCatalogos = new Cl_Catalogos();

            ImgVerinfo.Click += ImgVerinfo_Click;
            ImgVerProvidencia.Click += ImgVerProvidencia_Click;
            ImgVerDictamenJuridico.Click += ImgVerDictamenJuridico_Click;
            ImgVerResolucion.Click += ImgVerResolucion_Click;
            OptApruebaInscripción.SelectedIndexChanged += OptApruebaInscripción_SelectedIndexChanged;
            GrdMotivo.NeedDataSource += GrdMotivo_NeedDataSource;
            btnAddMotivo.ServerClick += btnAddMotivo_ServerClick;
            GrdMotivo.ItemCommand += GrdMotivo_ItemCommand;
            BtnVPOficio.Click += BtnVPOficio_Click;
            BtnGrabaOficio.Click += BtnGrabaOficio_Click;
            BtnYes.Click += BtnYes_Click;
            BtnVPConstancia.Click += BtnVPConstancia_Click;
            BtnGrabarConstancia.Click += BtnGrabarConstancia_Click;
            BtnVistaPreviaLicencia.Click += BtnVistaPreviaLicencia_Click;
            ImgVerDictamenTecnico.Click += ImgVerDictamenTecnico_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(35, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 35);
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
                LblNug.Text = "Gestión No.: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    LblSolicitante.Text = "Solicitante: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                    DivResolucion.Visible = true;
                    DivRegionalRegistro.Visible = true;
                }
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                {
                    LblIdentificacion.Text = ClManejo.Get_Identificacion_Gestion_Manejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    DivVerDicTec.Visible = true;
                    DivRegionalManejo.Visible = true;
                }
            }
        }

        void ImgVerDictamenTecnico_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Dictamen Técnico";
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));


            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2, 2);
            int CategoriaId = ClGestion.Get_CategoriaRNFId(SubCategoriaId);

            DataSet dsDatos = ClGestion.Get_Datos_DictamenTecnico_Gestion(GestionId);
            DataSet DsDatosDictamen = new DataSet("DsDatosDictamen");


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

            DataSet DsEtapa = new DataSet("DsDatosEtapa");
            DataTable DtEtapa = DsEtapa.Tables.Add("Etapa");
            DataColumn EtapaId = DtEtapa.Columns.Add("EtapaId", typeof(int));
            DataColumn Etapa = DtEtapa.Columns.Add("Etapa", typeof(string));
            DataColumn FecIni = DtEtapa.Columns.Add("FecIni", typeof(string));
            DataColumn FecFin = DtEtapa.Columns.Add("FecFin", typeof(string));




            DataRow item = DsDatosDictamen.Tables["DatosDictamen"].NewRow();
            item["MetodologiaCorro"] = dsDatos.Tables["Datos"].Rows[0]["Metodologia_Corroboracion_Inventario"];
            item["MetodologiaInvForestal"] = dsDatos.Tables["Datos"].Rows[0]["Metodologia_Resultados_Inventario"];
            if (dsDatos.Tables["Datos"].Rows[0]["Tipo_InventarioId"].ToString() == "1")
                item["FormaEvaluacion"] = "Forma de Evaluación: " + dsDatos.Tables["Datos"].Rows[0]["Tipo_Inventario"].ToString() + ", Total de Rodales: " + dsDatos.Tables["Datos"].Rows[0]["TotalRodal"].ToString() + " Rodales Muestreados: " + dsDatos.Tables["Datos"].Rows[0]["RodalesMuestreados"].ToString();
            else
                item["FormaEvaluacion"] = "Forma de Evaluación: " + dsDatos.Tables["Datos"].Rows[0]["Tipo_Inventario"].ToString() + ", Tamaño y Forma de parcela: " + dsDatos.Tables["Datos"].Rows[0]["Tamano"].ToString() + " " + dsDatos.Tables["Datos"].Rows[0]["Forma_Parcela"].ToString() + ",  Total de Rodales: " + dsDatos.Tables["Datos"].Rows[0]["TotalRodal"].ToString() + " Rodales Muestreados: " + dsDatos.Tables["Datos"].Rows[0]["RodalesMuestreados"].ToString();

            item["ConCaracBio"] = dsDatos.Tables["Datos"].Rows[0]["Conclusion_Biofisica"].ToString();
            item["ConVeracidad"] = dsDatos.Tables["Datos"].Rows[0]["Conclusion_Veracidad"].ToString();
            item["ConPropuestaManejo"] = dsDatos.Tables["Datos"].Rows[0]["Conclusion_PropuestaManejo"].ToString();
            item["ConPropuestaTratamiento"] = dsDatos.Tables["Datos"].Rows[0]["Conclusion_Propuesta_Tratamiento"].ToString();
            item["Dictamen"] = dsDatos.Tables["Datos"].Rows[0]["Tipo_Dictamen"].ToString();
            item["TipoGarantia"] = dsDatos.Tables["Datos"].Rows[0]["Tipo_Garantia"].ToString();
            item["DictamenId"] = dsDatos.Tables["Datos"].Rows[0]["Tipo_DictamenId"].ToString();
            DataSet DsGarantia = ClCatalogos.Sp_Get_Monto_Garantia(Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Tipo_GarantiaId"]));
            item["MontoGarantia"] = (Convert.ToDouble(ClManejo.Get_Compromiso_Area(GestionId)) * Convert.ToDouble(DsGarantia.Tables["Datos"].Rows[0]["Valor_Hectaria"])).ToString();
            item["PorGarantia"] = DsGarantia.Tables["Datos"].Rows[0]["Porcentaje"].ToString();
            DsGarantia.Clear();
            item["TotValMaderaPie"] = ClGestion.Get_TotalValorMadera(GestionId);
            item["RecomendacionUno"] = "7.1     Se recomienda que la vigencia del aprovechamiento sea de: " + dsDatos.Tables["Datos"].Rows[0]["Vigencia"].ToString() + ".";
            if (Convert.ToInt32(dsDatos.Tables["Datos"].Rows[0]["Notas_Autorizadas"]) > 10)
                item["RecomendacionDos"] = "7.1     Que se le autorice la venta total de " + dsDatos.Tables["Datos"].Rows[0]["Notas_Autorizadas"].ToString() + ". De estas, se le entregarán únicamente " + dsDatos.Tables["Datos"].Rows[0]["Notas_Entregar"].ToString() + ", las otras " + dsDatos.Tables["Datos"].Rows[0]["Notas_Restantes"].ToString() + " se adjuntarán al expediente, sin foliar, mismas que se entregarán luego al titular o su representante legal cuando presente Informe de uso de notas de envío.";
            else
                item["RecomendacionDos"] = "7.7     Que se le autorice la venta total de " + dsDatos.Tables["Datos"].Rows[0]["Notas_Autorizadas"].ToString() + ".";
            DataSet dsAreas = ClManejo.Get_Areas_PlanManejo(GestionId);

            if (Convert.ToDouble(dsAreas.Tables["Datos"].Rows[0]["AreaIntervenir"]) > 100)
                item["OtrasRecomendaciones"] = "7.9     " + dsDatos.Tables["Datos"].Rows[0]["OtrasRecomendacion"].ToString();
            else
                item["OtrasRecomendaciones"] = "7.8     " + dsDatos.Tables["Datos"].Rows[0]["OtrasRecomendacion"].ToString();
            DsDatosDictamen.Tables["DatosDictamen"].Rows.Add(item);
            dsDatos.Clear();


            DataSet dsDatosEtapa = ClGestion.Get_Etapas_Dictamen_Tecnico(GestionId);
            for (int i = 0; i < dsDatosEtapa.Tables["Datos"].Rows.Count; i++)
            {
                DataRow itemEtapa = DsEtapa.Tables["Etapa"].NewRow();
                itemEtapa["EtapaId"] = dsDatosEtapa.Tables["Datos"].Rows[0]["EtapaId"];
                itemEtapa["Etapa"] = dsDatosEtapa.Tables["Datos"].Rows[0]["Etapa"];
                itemEtapa["FecIni"] = dsDatosEtapa.Tables["Datos"].Rows[0]["FecIni"];
                itemEtapa["Fecfin"] = dsDatosEtapa.Tables["Datos"].Rows[0]["FecFin"];
                DsEtapa.Tables["Etapa"].Rows.Add(itemEtapa);
            }


            Session["DatosDictamenTec"] = ClGestion.ImpresionDictamenTecnio(GestionId, 2, CategoriaId, DsDatosDictamen, DsEtapa, Convert.ToInt32(Session["UsuarioId"]));
            RadWindow1.Title = "Vista Previa Dictamen Técnico";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenTecnico.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void BtnVistaPreviaLicencia_Click(object sender, EventArgs e)
        {
            if (ValidaLicencia() == false)
            {
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                Session["DatosLicencia"] = ClGestion.ImpresionLicencia(GestionId, 1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtPeriodo.Text), Convert.ToDateTime(TxtFecIni.SelectedDate), Convert.ToDateTime(TxtFecFin.SelectedDate), CboAprueba.Text);
                RadWindow1.Title = "Vista Previa Licencia Forestal";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepLicencia_Forestal.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        bool ValidaLicencia()
        {
            bool HayError = false;
            DivErrLicencia.Visible = false;
            string LblMensaje = "";
            if (TxtPeriodo.Text == "")
            {
                if (LblMensaje == "")
                    LblMensaje = "Debe ingresar el periodo de la licencia";
                else
                    LblMensaje = LblMensaje + ", debe ingresar el periodo de la licencia";
                HayError = true;
            }
            if (TxtFecIni.DateInput.Text == "")
            {
                if (LblMensaje == "")
                    LblMensaje = "Debe ingresar la fecha de inicio de la licencia";
                else
                    LblMensaje = LblMensaje + ", debe ingresar la fecha de inicio de la licencia";
                HayError = true;
            }
            if (TxtFecFin.DateInput.Text == "")
            {
                if (LblMensaje == "")
                    LblMensaje = "Debe ingresar la fecha de finalización de la licencia";
                else
                    LblMensaje = LblMensaje + ", debe ingresar la fecha de finalización de la licencia";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrLicencia.Visible = true;
                LblErrLicencia.Text = LblMensaje;
                
            }
            return HayError;

        }

        void BtnGrabarConstancia_Click(object sender, EventArgs e)
        {
            if (ValidaConstaciaRRF() == true)
            {
                Session["Oficio"] = 0;
                LblTitConfirmacion.Text = "El sistema generara el número en el registro nacional forestal, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                
            }
        }

        void BtnVPConstancia_Click(object sender, EventArgs e)
        {
            if (ValidaConstaciaRRF() == true)
            {
                RadWindow1.Title = "Vista Previa Constancia RRF";
                DataSet DatosConstanciaRRF = ClGestion.ImpresionConstanciaRFF(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecIncripcion.SelectedDate)),ClUtilitarios.IIf(TxtFecUltActualizacion.DateInput.Text == "", "", TxtFecUltActualizacion.DateInput.Text).ToString(), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecVencimiento.SelectedDate)));
                Session["DatosConstanciaRRF"] = DatosConstanciaRRF;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_ConstanciaRRF.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        bool ValidaConstaciaRRF()
        {
            bool HayError = false;
            DivError.Visible = false;
            string Mensaje = "";
            LblMensaje.Text = "";
            if (TxtFecIncripcion.DateInput.Text == "")
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar la fecha de inscripción";
                else
                    Mensaje = Mensaje + ", debe ingresar la fecha de inscripción";
                HayError = true;
            }
            if (TxtFecVencimiento.DateInput.Text == "")
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar la fecha de vencimiento";
                else
                    Mensaje = Mensaje + ", debe ingresar la fecha de vencimiento";
                HayError = true;
            }
            if (HayError == true)
            {
                DivError.Visible = true;
                LblMensaje.Text = Mensaje;
                return false;
            }

            else
                return true;
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["Oficio"]) == 1)
            {
                Session["Oficio"] = 0;
                XmlDocument iInformacion = ClXml.CrearDocumentoXML("OficioDevolucion");
                XmlNode iElementos = iInformacion.CreateElement("Motivos");
                CargaDataSet();
                for (int i = 0; i < Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Motivo", Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows[i]["Motivo"], iElementoDetalle);
                    iElementos.AppendChild(iElementoDetalle);
                }
                iInformacion.ChildNodes[1].AppendChild(iElementos);
                int OficioDevolucionId = ClGestion.Max_Oficio_Devolucion();
                ClGestion.Insert_Oficio_Devolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtDirigido.Text, iInformacion, Convert.ToInt32(Session["UsuarioId"]));

                if (Convert.ToInt32(Session["TipoUsuarioId"]) == 4)
                {
                    DataSet dsDatos = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    ClUtilitarios.EnvioCorreo(dsDatos.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatos.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Oficio de Devolución", "Se ha enviado Oficio de Devolución", 0, "", "");
                    dsDatos.Clear();
                }
                else
                {
                    DataSet dsDatos = ClGestion.Get_Datos_Persona(1, ClGestion.Get_UsuarioSubRegional_Resolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                    ClUtilitarios.EnvioCorreo(dsDatos.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatos.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Oficio de Devolución", "Se ha enviado Oficio de Devolución", 0, "", "");
                    dsDatos.Clear();
                }
                ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 5);
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("5", true)) + "&traderefund=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OficioDevolucionId.ToString(), true)) + "");
            }
            else
            {
                int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                int RegistroId = ClGestion.Max_registroId();
                ClGestion.Insert_RegistroRNF(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecIncripcion.SelectedDate)), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecVencimiento.SelectedDate)), SubCategoriaId, Convert.ToInt32(Session["UsuarioId"]));
                if (Convert.ToInt32(Session["TipoUsuarioId"]) == 4)
                {
                    DataSet dsDatos = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    ClUtilitarios.EnvioCorreo(dsDatos.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatos.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Inscripción en el Registro Nacional Forestal", "Inscripción en el Registro Nacional Forestal", 0, "", "");
                    dsDatos.Clear();
                }
                else
                {
                    DataSet dsDatos = ClGestion.Get_Datos_Persona(1, ClGestion.Get_UsuarioSubRegional_Resolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                    ClUtilitarios.EnvioCorreo(dsDatos.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatos.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Inscripción en el Registro Nacional Forestal", "Inscripción en el Registro Nacional Forestal", 0, "", "");
                    dsDatos.Clear();
                }
                ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 6);
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("6", true)) + "&enregistrement=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(RegistroId.ToString(), true)) + "");
            }
        }

        void BtnGrabaOficio_Click(object sender, EventArgs e)
        {
            if (ValidaOficio() == true)
            {
                Session["Oficio"] = 1;
                LblTitConfirmacion.Text = "El sistema generara el oficio de devolución de expediente, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnVPOficio_Click(object sender, EventArgs e)
        {
            if (ValidaOficio() == true)
            {
                RadWindow1.Title = "Vista Previa Oficio de Devolución";
                CargaDataSet();
                int SubRegionalUsuario = ClGestion.Get_UsuarioSubRegional_Resolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                DataSet DatosOficioDevolucion = ClGestion.ImpresionOficioDevolucion(1, 0, SubRegionalUsuario, Ds_Temporal, TxtDirigido.Text, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(Session["TipoUsuarioId"]));
                Session["DatosOficioDevolucion"] = DatosOficioDevolucion;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioDevolucion.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);

            }
        }

        bool ValidaOficio()
        {
            bool HayError = false;
            DivError.Visible = false;
            string Mensaje = "";
            LblMensaje.Text = "";
            if (GrdMotivo.Items.Count == 0)
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar al menos un motivo al oficio";
                else
                    Mensaje = Mensaje + ", debe ingresar al menos un motivo al oficio";
                HayError = true;
            }
            if (HayError == true)
            {
                DivError.Visible = true;
                LblMensaje.Text = Mensaje;
                return false;
            }

            else
                return true;
        }

        void GrdMotivo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEditar")
            {
                TxtMotivoId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdMotivo"].ToString();
                TxtMotivo.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Motivo"].ToString();
            }
            if (e.CommandName == "CmdDel")
            {
                EliminarMotivo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdMotivo"]));
                TxtMotivo.Text = "";
                TxtMotivoId.Text = "";

            }
        }

        void EliminarMotivo(int Item)
        {
            for (int i = 0; i < GrdMotivo.Items.Count; i++)
            {
                if (Item != Convert.ToInt32(GrdMotivo.Items[i].OwnerTableView.DataKeyValues[i]["IdMotivo"]))
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].NewRow();
                    row["IdMotivo"] = Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Count;
                    row["Motivo"] = GrdMotivo.Items[i].OwnerTableView.DataKeyValues[i]["Motivo"];
                    Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Add(row);
                }
            }
            GrdMotivo.Rebind();
        }

        void btnAddMotivo_ServerClick(object sender, EventArgs e)
        {
            if (TxtMotivo.Text == "")
                TxtMotivo.Focus();
            else
            {
                if (TxtMotivoId.Text == "")
                    AgregarMotivo(TxtMotivo.Text);
                else
                    ModificarMotivo(Convert.ToInt32(TxtMotivoId.Text), TxtMotivo.Text);
                GrdMotivo.Rebind();
                TxtMotivo.Text = "";
                TxtMotivoId.Text = "";
            }
        }

        void AgregarMotivo(string Motivo)
        {
            CargaDataSet();
            DataRow rowNew = Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].NewRow();
            rowNew["IdMotivo"] = GrdMotivo.Items.Count;
            rowNew["Motivo"] = Motivo;
            Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Add(rowNew);
        }

        void ModificarMotivo(int Item, string Motivo)
        {
            CargaDataSet();
            Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows[Item]["Motivo"] = Motivo;
            GrdMotivo.Rebind();
        }

        void CargaDataSet()
        {
            for (int i = 0; i < GrdMotivo.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].NewRow();
                row["IdMotivo"] = Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Count;
                row["Motivo"] = GrdMotivo.Items[i].OwnerTableView.DataKeyValues[i]["Motivo"];
                Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Add(row);
            }
        }


        void GrdMotivo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Motivo_Oficio_Dev"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdMotivo, "Dt_Motivo_Oficio_Dev");
        }

        void OptApruebaInscripción_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptApruebaInscripción.SelectedValue == "1")
            {
                DivSiAprueba.Visible = true;
                DivSiApruebaBotones.Visible = true;
                DivNoApruebaBotones.Visible = false;
                DivNoAprueba.Visible = false;
                TxtFecIncripcion.SelectedDate = DateTime.Now;
                int SubCategoriaId = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                int Vigencia = ClGestion.Get_Vigencia_SubCategoria(SubCategoriaId);
                TxtFecVencimiento.SelectedDate = DateTime.Now.AddYears(Vigencia);
                TxtNoExpediente.Text = ClGestion.Get_No_Expediente(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
            }
            else
            {
                DivSiAprueba.Visible = false;
                DivSiApruebaBotones.Visible = false;
                DivNoApruebaBotones.Visible = true;
                DivNoAprueba.Visible = true;
            }
        }

        void ImgVerResolucion_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Resolución de Aprobación de Inscripción";
            string ResolucionId = ClGestion.Get_ResolucionId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet DatosResolucion = ClGestion.ImpresionResolucion_Aprobacion(2, Convert.ToInt32(ResolucionId), Convert.ToInt32(Session["UsuarioId"]), 0);
            Session["DatosResolucion"] = DatosResolucion;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAprobacion.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgVerDictamenJuridico_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Dictamen Juridico";
            string Dictamen_Juridico_Id = ClGestion.Get_Ditamen_JuridicoId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet dsTemp = new DataSet();
            DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "", 0, "", "", "", dsTemp, "");
            Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
        }
    }
}