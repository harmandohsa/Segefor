using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using Telerik.Web.UI;
using System.Data;
using SEGEFOR.Data_Set;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Historial_Gestion : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Manejo ClManejo;
        Cl_Catalogos ClCatalogos;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();
            ClCatalogos = new Cl_Catalogos();

            GrdSolicitudes.ItemDataBound += GrdSolicitudes_ItemDataBound;
            GrdSolicitudes.NeedDataSource += GrdSolicitudes_NeedDataSource;
            GrdSolicitudes.ItemCommand += GrdSolicitudes_ItemCommand;
            GrdDetalle.ItemCommand += GrdDetalle_ItemCommand;
            GrdDetalle.NeedDataSource += GrdDetalle_NeedDataSource;
            BtnBuscar.ServerClick += BtnBuscar_ServerClick;
            GrdManejoForestal.NeedDataSource += GrdManejoForestal_NeedDataSource;
            GrdManejoForestal.ItemDataBound += GrdManejoForestal_ItemDataBound;
            GrdManejoForestal.ItemCommand += GrdManejoForestal_ItemCommand;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(8, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

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

        void GrdManejoForestal_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadWindow1.Title = "Plan de Manejo Forestal";
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2,2);
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "CmdSolComple")
            {
                TxtGestionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString();
                GrdDetalle.Rebind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocAceptacion")
            {
                RadWindow1.Title = "Constancia de Admisión de Expediente";
                string Admision_GestionId = ClGestion.Get_DocumentoId(1, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                string SubCategoria = ClGestion.Get_SubCategoria_Gestion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepConstanciaAdmisionExp.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Admision_GestionId, true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoria.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocProvidencia")
            {
                RadWindow1.Title = "Providencia para traslado de Expediente";
                string ProvidenciaId = ClGestion.Get_DocumentoId(2, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
                Session["DatosProvidencia"] = DatosProvidencia;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocJurudico")
            {
                RadWindow1.Title = "Dictamen Juridico";
                string Dictamen_Juridico_Id = ClGestion.Get_DocumentoId(3, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet dsTemp = new DataSet();
                DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "", 0, "", "", "", dsTemp, "");
                Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocEnmiendas")
            {
                RadWindow1.Title = "Oficio de enmiendas jurídicas";
                string OficioEnmiendaId = ClGestion.Get_DocumentoId(5, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosOficioEnmiendas = ClGestion.ImpresionOficioEnmiendasJuridico(2, Convert.ToInt32(OficioEnmiendaId), Convert.ToInt32(Session["UsuarioId"]));
                Session["DatosOficioEnmiendasJuridico"] = DatosOficioEnmiendas;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendaJuridica.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocOficioDev")
            {
                RadWindow1.Title = "Oficio de Devolución";
                string OficioDevolucionId = ClGestion.Get_DocumentoId(6, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet dsTemp = new DataSet();
                DataSet DatosOficioDevolucion = ClGestion.ImpresionOficioDevolucion(2, Convert.ToInt32(OficioDevolucionId), 0, dsTemp, "", 0, 0);
                Session["DatosOficioDevolucion"] = DatosOficioDevolucion;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioDevolucion.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocDictamenTec")
            {
                RadWindow1.Title = "Dictamen Técnico";
                int GestionId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]);


                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2, 2);
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
                if (dsDatos.Tables["Datos"].Rows[0]["Tipo_DictamenId"].ToString() == "1")
                {
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
                }
                DsDatosDictamen.Tables["DatosDictamen"].Rows.Add(item);
                dsDatos.Clear();


                Session["DatosDictamenTec"] = ClGestion.ImpresionDictamenTecnio(GestionId, 2, CategoriaId, DsDatosDictamen, DsEtapa, Convert.ToInt32(Session["UsuarioId"]));
                RadWindow1.Title = "Dictamen Técnico";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenTecnico.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocDictamenSubReg")
            {
                int GestionId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]);
                Session["DatosDictamenSubRegion"] = ClGestion.ImpresionDictamenSubRegional(GestionId, 2, Convert.ToInt32(Session["UsuarioId"]), 0, "");
                RadWindow1.Title = "Dictamen Subregional";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenSubRegional.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocLicencia")
            {
                int GestionId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]);
                Session["DatosLicencia"] = ClGestion.ImpresionLicencia(GestionId, 2, Convert.ToInt32(Session["UsuarioId"]), 0, DateTime.Now, DateTime.Now, "");
                RadWindow1.Title = "Vista Previa Licencia Forestal";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepLicencia_Forestal.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdManejoForestal_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion_Manejo(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                
                LinkButton LnkDictamenJuridico;
                LnkDictamenJuridico = (LinkButton)item.FindControl("LnkDictamenJuridico");
                LnkDictamenJuridico.Text = ds.Tables["DATOS"].Rows[0]["No_Dictamen"].ToString();
                
                LinkButton DocProvidencia;
                DocProvidencia = (LinkButton)item.FindControl("LnkProvidencia");
                DocProvidencia.Text = ds.Tables["DATOS"].Rows[0]["No_Providencia"].ToString();

                LinkButton LnkOficioEnmiendas;
                LnkOficioEnmiendas = (LinkButton)item.FindControl("LnkOficioEnmiendas");
                LnkOficioEnmiendas.Text = ds.Tables["DATOS"].Rows[0]["Oficio_Enmiendas"].ToString();

                LinkButton LnkOficioDev;
                LnkOficioDev = (LinkButton)item.FindControl("LnkOficioDev");
                LnkOficioDev.Text = ds.Tables["DATOS"].Rows[0]["Oficio_Dev"].ToString();

                LinkButton LnkDictamenTecnico;
                LnkDictamenTecnico = (LinkButton)item.FindControl("LnkDictamenTecnico");
                LnkDictamenTecnico.Text = ds.Tables["DATOS"].Rows[0]["Dic_Tec"].ToString();

                LinkButton LnkDictamenSubRegional;
                LnkDictamenSubRegional = (LinkButton)item.FindControl("LnkDictamenSubRegional");
                LnkDictamenSubRegional.Text = ds.Tables["DATOS"].Rows[0]["DictamenSubRegional"].ToString();

                LinkButton LnkLicenciaForestal;
                LnkLicenciaForestal = (LinkButton)item.FindControl("LnkLicenciaForestal");
                LnkLicenciaForestal.Text = ds.Tables["DATOS"].Rows[0]["NoLicencia"].ToString();

                ds.Clear();
            }
        }

        void GrdManejoForestal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClGestion.Get_Historial_ManejoForestal(Convert.ToInt32(Session["UsuarioId"]), TxtNoExpediente.Text), GrdManejoForestal);
            
        }

        void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            GrdSolicitudes.Rebind();
        }

        void GrdDetalle_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TxtGestionId.Text != "")
            {
                ClUtilitarios.LlenaGrid(ClGestion.Solicitud_Completacion_Gestion_Historial(Convert.ToInt32(TxtGestionId.Text)), GrdDetalle);
            }
        }

        void GrdDetalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadWindow1.Title = "Solicitud de Completación de Gestión";
                string Max_GestionInCompletaId = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Gestion_IncompletaId"].ToString();
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepSolCompletacionGestion.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdSolicitudes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "3")
                {
                    int Actividad = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()));
                    if ((Actividad == 1) || (Actividad == 2) || (Actividad == 3) || (Actividad == 16))
                    {
                        DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
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
                    else if ((Actividad == 4) || (Actividad == 5) || (Actividad == 19) || (Actividad == 20) || (Actividad == 21))
                    {
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]),1);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (Actividad == 18) 
                    {
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                }
            }
            else if (e.CommandName == "CmdSolComple")
            {
                TxtGestionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString();
                GrdDetalle.Rebind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocAceptacion")
            {
                RadWindow1.Title = "Constancia de Admisión de Expediente";
                string Admision_GestionId = ClGestion.Get_DocumentoId(1, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                string SubCategoria = ClGestion.Get_SubCategoria_Gestion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepConstanciaAdmisionExp.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Admision_GestionId, true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoria.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocProvidencia")
            {
                RadWindow1.Title = "Providencia para traslado de Expediente";
                string ProvidenciaId = ClGestion.Get_DocumentoId(2, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
                Session["DatosProvidencia"] = DatosProvidencia;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocJurudico")
            {
                RadWindow1.Title = "Dictamen Juridico";
                string Dictamen_Juridico_Id = ClGestion.Get_DocumentoId(3, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet dsTemp = new DataSet();
                DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "", 0, "", "", "", dsTemp, "");
                Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocResolucion")
            {
                RadWindow1.Title = "Resolución de Aprobación de Inscripción";
                string ResolucionId = ClGestion.Get_DocumentoId(4, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosResolucion = ClGestion.ImpresionResolucion_Aprobacion(2, Convert.ToInt32(ResolucionId), Convert.ToInt32(Session["UsuarioId"]), 0);
                Session["DatosResolucion"] = DatosResolucion;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAprobacion.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocEnmiendas")
            {
                RadWindow1.Title = "Oficio de enmiendas jurídicas";
                string OficioEnmiendaId = ClGestion.Get_DocumentoId(5, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosOficioEnmiendas = ClGestion.ImpresionOficioEnmiendasJuridico(2, Convert.ToInt32(OficioEnmiendaId), Convert.ToInt32(Session["UsuarioId"]));
                Session["DatosOficioEnmiendasJuridico"] = DatosOficioEnmiendas;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendaJuridica.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocOficioDev")
            {
                RadWindow1.Title = "Oficio de Devolución";
                string OficioDevolucionId = ClGestion.Get_DocumentoId(6, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet dsTemp = new DataSet();
                DataSet DatosOficioDevolucion = ClGestion.ImpresionOficioDevolucion(2, Convert.ToInt32(OficioDevolucionId), 0, dsTemp, "", 0, 0);
                Session["DatosOficioDevolucion"] = DatosOficioDevolucion;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioDevolucion.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "DocRnf")
            {
                RadWindow1.Title = "Constancia RNF";
                string RegistroId = ClGestion.Get_DocumentoId(7, Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"])).ToString();
                DataSet DatosRegistro = ClGestion.ImpresionConstanciaRFF(2, Convert.ToInt32(RegistroId), 0, 0, DateTime.Now, "01/01/2000", DateTime.Now);
                Session["DatosConstanciaRRF"] = DatosRegistro;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_ConstanciaRRF.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones_Historial(Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"]),TxtNoExpediente.Text), GrdSolicitudes);
        }

        void GrdSolicitudes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                
                item["actividad"].Text = ClGestion.Get_Actividad_Registro(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                LinkButton LnkResolucion;
                LnkResolucion = (LinkButton)item.FindControl("LnkResolucion");
                LnkResolucion.Text = ds.Tables["DATOS"].Rows[0]["No_Resolucion"].ToString();
                
                LinkButton LnkDictamenJuridico;
                LnkDictamenJuridico = (LinkButton)item.FindControl("LnkDictamenJuridico");
                LnkDictamenJuridico.Text = ds.Tables["DATOS"].Rows[0]["No_Dictamen"].ToString();
                
                LinkButton DocProvidencia;
                DocProvidencia = (LinkButton)item.FindControl("LnkProvidencia");
                DocProvidencia.Text = ds.Tables["DATOS"].Rows[0]["No_Providencia"].ToString();

                LinkButton LnkOficioEnmiendas;
                LnkOficioEnmiendas = (LinkButton)item.FindControl("LnkOficioEnmiendas");
                LnkOficioEnmiendas.Text = ds.Tables["DATOS"].Rows[0]["Oficio_Enmiendas"].ToString();

                LinkButton LnkOficioDev;
                LnkOficioDev = (LinkButton)item.FindControl("LnkOficioDev");
                LnkOficioDev.Text = ds.Tables["DATOS"].Rows[0]["Oficio_Dev"].ToString();

                LinkButton LnkRegRnf;
                LnkRegRnf = (LinkButton)item.FindControl("LnkRegRnf");
                LnkRegRnf.Text = ds.Tables["DATOS"].Rows[0]["Rnf"].ToString();

                ds.Clear();
            }
        }

    }
}