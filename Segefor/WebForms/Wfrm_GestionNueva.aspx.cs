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
    public partial class Wfrm_GestionNueva : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Gestion_Registro ClGestionRegistro;
        Cl_Manejo ClManejo;
        Cl_Catalogos ClCatalogos;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClGestionRegistro = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();
            ClCatalogos = new Cl_Catalogos();

            GrdSolicitudes.NeedDataSource += GrdSolicitudes_NeedDataSource;
            GrdSolicitudes.ItemDataBound += GrdSolicitudes_ItemDataBound;
            GrdSolicitudes.ItemCommand += GrdSolicitudes_ItemCommand;
            GrdDetalle.NeedDataSource += GrdDetalle_NeedDataSource;
            GrdDetalle.ItemCommand += GrdDetalle_ItemCommand;

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
                if (Session["TipoUsuarioId"].ToString() == "1")
                {
                    DivOpcionesPubGen.Visible = true;
                    DivOpcionesINAB.Visible = false;
                    
                }
                else
                {
                    if (Session["TipoUsuarioId"].ToString() == "12")
                    {
                        DivOpcionesPubGen.Visible = true;
                        DivOpcionesINAB.Visible = true;
                        ImgApro.Enabled = false;
                        ImgIncentivos.Enabled = false;
                        ImgExportacion.Enabled = false;
                        ImgNotaEnvio.Enabled = false;
                        ImgGestionVaria.Enabled = false;
                    }
                    else
                    {
                        DivOpcionesPubGen.Visible = false;
                        DivOpcionesINAB.Visible = true;
                    }
                    DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 8);
                    if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                    {
                        GrdSolicitudes.Columns.FindByUniqueName("Seg").Visible = false;
                    }
                    if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                    {
                      
                    }
                    if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Eliminar"]) == 0)
                    {

                    }
                    dsPermisos.Clear();
                    string llamada = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["appel"].ToString()), true);
                    if (llamada == "1") //Solicitud Completacion
                    {
                        RadWindow1.Title = "Solicitud de Completación de Gestión";
                        string Max_GestionInCompletaId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true);
                        string SubCategoria = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true);
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepSolCompletacionGestion.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId, true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoria, true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "2") //Admision Gestion
                    {
                        RadWindow1.Title = "Constancia de Admisión de Expediente";
                        string Admision_GestionId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["admissiongestion"].ToString()), true);
                        string SubCategoria = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true);
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepConstanciaAdmisionExp.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Admision_GestionId, true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoria, true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "3") //Providencia
                    {
                        RadWindow1.Title = "Providencia para traslado de Expediente";
                        string ProvidenciaId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["providence"].ToString()), true);
                        DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["providence"].ToString()), true)), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
                        Session["DatosProvidencia"] = DatosProvidencia;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "4") //Dictamen Juridico
                    {
                        RadWindow1.Title = "Dictamen Juridico";
                        string Dictamen_Juridico_Id = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["consultationjuridique"].ToString()), true);
                        DataSet dsTemp= new DataSet();
                        DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "",0, "", "", "", dsTemp, "");
                        Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "5") //Oficio de Devolucion
                    {
                        RadWindow1.Title = "Oficio de Devolución";
                        string OficioDevolucionId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["traderefund"].ToString()), true);
                        DataSet dsTemp = new DataSet();
                        DataSet DatosOficioDevolucion = ClGestion.ImpresionOficioDevolucion(2,Convert.ToInt32(OficioDevolucionId),0,dsTemp,"",0,0);
                        Session["DatosOficioDevolucion"] = DatosOficioDevolucion;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioDevolucion.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "6") //Constancia RNF
                    {
                        RadWindow1.Title = "Constancia RNF";
                        string RegistroId = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["enregistrement"].ToString()), true);
                        DataSet DatosRegistro = ClGestion.ImpresionConstanciaRFF(2,Convert.ToInt32(RegistroId), 0, 0, DateTime.Now, "01/01/2000", DateTime.Now);
                        Session["DatosConstanciaRRF"] = DatosRegistro;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_ConstanciaRRF.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "7") //Resolucion Admisión
                    {
                        RadWindow1.Title = "Resolución Admisíón Expediente";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAdmision.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "8") //Dictamen_Tecnico
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
                        RadWindow1.Title = "Dictamen Técnico";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenTecnico.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "9") //Dictamen SubRegional
                    {
                        int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                        Session["DatosDictamenSubRegion"] = ClGestion.ImpresionDictamenSubRegional(GestionId, 2, Convert.ToInt32(Session["UsuarioId"]), 0, "");
                        RadWindow1.Title = "Dictamen Subregional";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenSubRegional.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (llamada == "10") //Licencia Forestal
                    {
                        int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                        Session["DatosLicencia"] = ClGestion.ImpresionLicencia(GestionId, 2, Convert.ToInt32(Session["UsuarioId"]), 0, DateTime.Now, DateTime.Now, "");
                        RadWindow1.Title = "Vista Previa Licencia Forestal";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepLicencia_Forestal.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }


                }
                SetColumnasGrid();
            }
        }

        void SetColumnasGrid()
        {
            if(Convert.ToInt32(Session["TipoUsuarioId"]) == 11) //Subregional
            {
                GrdSolicitudes.Columns.FindByUniqueName("SolComple").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Providencia").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Dictamen_Juridico").Visible = false;
            }
            else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 13) //Secretaria
            {
                GrdSolicitudes.Columns.FindByUniqueName("No_Exp").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("Fecha_Exp").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Providencia").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Dictamen_Juridico").Visible = false;
            }
            else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 14) //Juridico
            {
                GrdSolicitudes.Columns.FindByUniqueName("SolComple").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Dictamen_Juridico").Visible = false;
            }
            else if ((Convert.ToInt32(Session["TipoUsuarioId"]) == 10) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 4)) //Director Regional o Registrador Nacional
            {
                
                GrdSolicitudes.Columns.FindByUniqueName("SolComple").Visible = false;
                GrdSolicitudes.Columns.FindByUniqueName("No_Providencia").Visible = false;
            }
        }

        void GrdDetalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadWindow1.Title = "Solicitud de Completación de Gestión";
                string Max_GestionInCompletaId = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Gestion_IncompletaId"].ToString();
                int SubCategoria = ClGestion.Get_SubCategoria_Gestion(ClGestion.Get_GestionId_GestionIncompleta(Convert.ToInt32(Max_GestionInCompletaId)));
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepSolCompletacionGestion.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId, true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoria.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdDetalle_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TxtGestionId.Text != "")
            {
                ClUtilitarios.LlenaGrid(ClGestion.Solicitud_Completacion_Gestion_Historial(Convert.ToInt32(TxtGestionId.Text)), GrdDetalle);
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
                        if (ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"].ToString()  == "1")
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
                        if ((Actividad == 1) || (Actividad == 16))
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
                    else if ((Actividad == 4) || (Actividad == 5) || (Actividad == 19) || (Actividad == 20) || (Actividad == 21))
                    {
                        Session["TipoReporte"] = "2";
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestionRegistro.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]),1);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(),true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (Actividad == 18) 
                    {
                        Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestionRegistro.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2);
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 34) || (Actividad == 35) || (Actividad == 36) || (Actividad == 37) || (Actividad == 38) || (Actividad == 39) || (Actividad == 40) || (Actividad == 41) || (Actividad == 42) || (Actividad == 43))
                    {
                        Session["Ds_Formulario_SistemaAgroforestal"] = ClGestionRegistro.ImpresionFormularioSistemaAgroforestal(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioSistemasAgroforestales.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 13) || (Actividad == 29) || (Actividad == 30) || (Actividad == 31) || (Actividad == 32) || (Actividad == 33))
                    {
                        Session["Ds_Formulario_FuenteSemillera"] = ClGestionRegistro.ImpresionFormularioFuenteSemillera(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioFuenteSemillera.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 7) || (Actividad == 9) || (Actividad == 10) || (Actividad == 12) || (Actividad == 17) || (Actividad == 22) || (Actividad == 23) || (Actividad == 24))
                    {
                        Session["Ds_Empresas"] = ClGestionRegistro.ImpresionFormularioEmpresas(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEmpresas.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 25) || (Actividad == 26) || (Actividad == 27) || (Actividad == 28))
                    {
                        Session["Dt_Entidad"] = ClGestionRegistro.ImpresionFormularioEntidad(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEntidad.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if ((Actividad == 11) || (Actividad == 15))
                    {
                        Session["Ds_MotoSierra"] = ClGestionRegistro.ImpresionFormularioMotoSierra(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioMotoSierra.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else if (Actividad == 14)
                    {
                        Session["Ds_Formulario_BosqueNatural"] = ClGestionRegistro.ImpresionFormularioBosqueNatural(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        RadWindow1.Title = "Formulario de Inscripción";
                        Session["TipoReporte"] = 2;
                        RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioBosqueNatural.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                }
                else if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "2")
                {
                    RadWindow1.Title = "Plan de Manejo Forestal";
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]), 2,2 );
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
            else if (e.CommandName == "CmdSeg")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "3")
                {
                    int SubCategoriaId = ClGestion.Get_Actividad_RegistroId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                    {
                        DataSet ds = ClGestion.Formulario_Profesional(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        int CategoriaProfesion = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["CategoriaProfesionId"]);
                        int ProfesionId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["ProfesionId"]);
                        int OrigenPersonaId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["Origen_PersonaId"]);
                        int SubRegionId = Convert.ToInt32(ds.Tables["Datos"].Rows[0]["SubRegionId"]);
                        ds.Tables.Clear();
                        string NUG = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUG"].ToString();
                        string Nombre = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nombres"].ToString();
                        if (Convert.ToInt32(Session["TipoUsuarioId"]) == 13)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_Secretaria.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&profession=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProfesionId.ToString(), true)) + "&categorieprofessionnelle=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaProfesion.ToString(), true)) + "&originepersonne=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OrigenPersonaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                        else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 11)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_SubRegional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&profession=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProfesionId.ToString(), true)) + "&categorieprofessionnelle=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaProfesion.ToString(), true)) + "&originepersonne=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OrigenPersonaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                        else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 14)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_Juridico.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&profession=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProfesionId.ToString(), true)) + "&categorieprofessionnelle=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaProfesion.ToString(), true)) + "&originepersonne=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OrigenPersonaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                        else if ((Convert.ToInt32(Session["TipoUsuarioId"]) == 10) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 4))
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_Regional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&profession=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ProfesionId.ToString(), true)) + "&categorieprofessionnelle=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(CategoriaProfesion.ToString(), true)) + "&originepersonne=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(OrigenPersonaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21) || (SubCategoriaId == 18) || (SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43) || (SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33) || (SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24) || (SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28) || (SubCategoriaId == 11) || (SubCategoriaId == 15) || (SubCategoriaId == 14))                    
                    {
                        int SubRegionId = ClGestion.Get_SubRegionId_Gestion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                        string NUG = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUG"].ToString();
                        string Nombre = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nombres"].ToString();
                        if (Convert.ToInt32(Session["TipoUsuarioId"]) == 13)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_Secretaria.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                        else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 11)
                            Response.Redirect("~/WebForms/Wfrm_Seguimiento_SubRegional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    }
                }
                else if  (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString() == "2")
                {
                    int SubCategoriaId = ClManejo.Get_Actividad_ManejoId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()));
                    int SubRegionId = ClGestion.Get_SubRegionId_Gestion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"]));
                    string NUG = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["NUG"].ToString();
                    string Nombre = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nombres"].ToString();
                    if (Convert.ToInt32(Session["TipoUsuarioId"]) == 13)
                        Response.Redirect("~/WebForms/Wfrm_Seguimiento_Secretaria.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 11)
                        Response.Redirect("~/WebForms/Wfrm_Seguimiento_SubRegional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 14)
                        Response.Redirect("~/WebForms/Wfrm_Seguimiento_Juridico.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 12)
                        Response.Redirect("~/WebForms/Wfrm_Seguimiento_Tecnico.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&nom=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Nombre.ToString(), true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                    else if (Convert.ToInt32(Session["TipoUsuarioId"]) == 10)
                        Response.Redirect("~/WebForms/Wfrm_Seguimiento_Regional.aspx?gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString(), true)) + "&modulo=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ModuloId"].ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "&gun=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG.ToString(), true)) + "&sousregion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "");
                }
            }
            else if (e.CommandName == "CmdSolComple")
            {
                TxtGestionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString();
                GrdDetalle.Rebind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowDetalle.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdSolicitudes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("ModuloId").ToString() == "3")
                {
                    item["actividad"].Text = ClGestion.Get_Actividad_Registro(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                }
                else if (item.GetDataKeyValue("ModuloId").ToString() == "2")
                {
                    item["actividad"].Text = ClManejo.Get_Actividad_Manejo(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                }
                if ((Convert.ToInt32(Session["TipoUsuarioId"]) == 11) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 14) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 12))
                {
                    DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    item["No_Exp"].Text = ds.Tables["DATOS"].Rows[0]["No_Expediente"].ToString();
                    item["Fecha_Exp"].Text = ds.Tables["DATOS"].Rows[0]["Fecha"].ToString();
                    ds.Clear();
                }
                if ((Convert.ToInt32(Session["TipoUsuarioId"]) == 14) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 12))
                {
                    DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    item["No_Providencia"].Text = ds.Tables["DATOS"].Rows[0]["No_Providencia"].ToString();
                    ds.Clear();
                }
                if (Convert.ToInt32(Session["TipoUsuarioId"]) == 12)
                {
                    DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    if (ds.Tables["Datos"].Rows.Count > 0)
                        item["No_Dictamen_Juridico"].Text = ds.Tables["DATOS"].Rows[0]["No_Dictamen"].ToString();
                    else
                        item["No_Dictamen_Juridico"].Text = "";
                    ds.Clear();
                }
                if ((Convert.ToInt32(Session["TipoUsuarioId"]) == 10) || (Convert.ToInt32(Session["TipoUsuarioId"]) == 4))
                {
                    DataSet ds = ClGestion.Get_Datos_Adicionales_Gestion(Convert.ToInt32(item.GetDataKeyValue("GestionId")));
                    item["No_Exp"].Text = ds.Tables["DATOS"].Rows[0]["No_Expediente"].ToString();
                    item["Fecha_Exp"].Text = ds.Tables["DATOS"].Rows[0]["Fecha"].ToString();
                    item["No_Dictamen_Juridico"].Text = ds.Tables["DATOS"].Rows[0]["No_Dictamen"].ToString();
                    ds.Clear();
                }
                
            }
        }

        void GrdSolicitudes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["TipoUsuarioId"].ToString() != "1")
                if (Session["TipoUsuarioId"].ToString() == "14")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(2,Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
                else if (Session["TipoUsuarioId"].ToString() == "11")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(3,Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
                else if (Session["TipoUsuarioId"].ToString() == "4")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(5, 10, Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
                else if (Session["TipoUsuarioId"].ToString() == "12")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(6, 12, Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
                else
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(1, Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
        }
    }
}