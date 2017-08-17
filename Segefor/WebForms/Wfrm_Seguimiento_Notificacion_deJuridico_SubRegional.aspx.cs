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

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Seguimiento_Notificacion_deJuridico_SubRegional : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Manejo ClManejo;
        Cl_Catalogos ClCatalogos;
        Cl_Xml ClXml;

        Ds_Temporales Ds_Temporal = new Ds_Temporales();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();
            ClCatalogos = new Cl_Catalogos();
            ClXml = new Cl_Xml();

            ImgVerProvidencia.Click += ImgVerProvidencia_Click;
            ImgVerinfo.Click += ImgVerinfo_Click;
            ImgVerDictamenJuridico.Click += ImgVerDictamenJuridico_Click;
            BtnVistaPreviaOficio.Click += BtnVistaPreviaOficio_Click;
            BtnEnviarOficio.Click += BtnEnviarOficio_Click;
            BtnYes.Click += BtnYes_Click;
            OptApruebaInscripción.SelectedIndexChanged += OptApruebaInscripción_SelectedIndexChanged;
            BtnVPResolucion.Click += BtnVPResolucion_Click;
            BtnGrabaResolucion.Click += BtnGrabaResolucion_Click;
            BtnVistaPreviaDictamen.Click += BtnVistaPreviaDictamen_Click;
            BtnGrabarDictamen.Click += BtnGrabarDictamen_Click;
            ImgVerDictamenTecnico.Click += ImgVerDictamenTecnico_Click;
            ImgVerEnmiendasTec.Click += ImgVerEnmiendasTec_Click;
            OptEnmiendas.SelectedIndexChanged += OptEnmiendas_SelectedIndexChanged;
            BtnAddEnmienda.ServerClick += BtnAddEnmienda_ServerClick;
            GrdEnmiendas.NeedDataSource += GrdEnmiendas_NeedDataSource;
            GrdEnmiendas.ItemCommand += GrdEnmiendas_ItemCommand;
            

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(34, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 34);
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
                int TieneEnmiendas = ClGestion.Tiene_Enmiendas_Dictamen_Juridico(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                if (TieneEnmiendas == 0)
                {
                    LblEstado.Text = "Dictamen Jurídico sin enmiendas";
                    DivSinEnmiendas.Visible = true;
                }
                else
                {
                    LblEstado.Text = "Dictamen Jurídico con enmiendas";
                    DivConEnmiendas.Visible = true;
                    //BtnVistaPreviaDictamen.Text = "Vista Previa Oficio de Enmiendas";
                    //BtnGrabarDictamen.Text = "Grabar Oficio de Enmiendas";
                    //TxtTieneEnmiendas.Text = "1";
                }
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    LblSolicitante.Text = "Solicitante: " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                    DivVerDicTec.Visible = false;
                }
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                {
                    LblIdentificacion.Text = ClManejo.Get_Identificacion_Gestion_Manejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    DivSinEnmiendas.Visible = false;
                    DivConEnmiendas.Visible = false;
                    DivDictamenManejo.Visible = true;
                }
                ClUtilitarios.LlenaCombo(ClCatalogos.Considera_Dictamen_Juridico_GET(), CboConsidera, "ConsideraId", "Considera");
                TieneDictamenTec();
                TieneEnmiendasTec();

                
            }
        }

        void GrdEnmiendas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdDel")
            {
                EliminarEnmienda(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Enmienda"].ToString());
                TxtEnmienda.Text = "";

            }
        }

        void EliminarEnmienda(string Item)
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                if (Item !=  GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"].ToString())
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Enminedas_Subregional"].NewRow();
                    row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                    Ds_Temporal.Tables["Dt_Enminedas_Subregional"].Rows.Add(row);
                }
            }
            GrdEnmiendas.Rebind();
        }

        void GrdEnmiendas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Enminedas_Subregional"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdEnmiendas, "Dt_Enminedas_Subregional");
        }

        void BtnAddEnmienda_ServerClick(object sender, EventArgs e)
        {
            DivErrEnmineda.Visible = false;
            if (TxtEnmienda.Text == "")
            {
                DivErrEnmineda.Visible = true;
                LblErrEnmienda.Text = "Debe ingresar una enmienda";
            }
            else
            {
                AgregarEnmienda(TxtEnmienda.Text);
                GrdEnmiendas.Rebind();
                TxtEnmienda.Text = "";
            }
        }

        void AgregarEnmienda(string Enmienda)
        {
            CargaDataSetEnmienda();
            DataRow rowNew = Ds_Temporal.Tables["Dt_Enminedas_Subregional"].NewRow();
            rowNew["Enmienda"] = Enmienda;
            Ds_Temporal.Tables["Dt_Enminedas_Subregional"].Rows.Add(rowNew);

        }

        void CargaDataSetEnmienda()
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Enminedas_Subregional"].NewRow();
                row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                Ds_Temporal.Tables["Dt_Enminedas_Subregional"].Rows.Add(row);
            }
        }

        void OptEnmiendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptEnmiendas.SelectedItem.Value == "1")
            {
                DivEnmiendas.Visible = true;
                DivEnmiendaGrid.Visible = true;
                DivDictamenTecUno.Visible = false;
                DivDictamenTecDos.Visible = false;
                BtnVistaPreviaDictamen.Text = "Vista Previa Oficio de Enmiendas";
                BtnGrabarDictamen.Text = "Grabar Oficio de Enmiendas";
                TxtTieneEnmiendas.Text = "1";
            }
            else
            {
                DivEnmiendas.Visible = false;
                DivEnmiendaGrid.Visible = false;
                DivDictamenTecUno.Visible = true;
                DivDictamenTecDos.Visible = true;
                BtnVistaPreviaDictamen.Text = "Vista Previa Dictamen";
                BtnGrabarDictamen.Text = "Grabar Dictamen";
                TxtTieneEnmiendas.Text = "0";
                
                
            }
        }

        void ImgVerEnmiendasTec_Click(object sender, ImageClickEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            Session["DatosEnmiendasTecnicas"] = ClGestion.ImpresionEnmiendasTecnicas(GestionId, 2);
            RadWindow1.Title = "Enmiendas Técnicas";
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepEnmiendasTec.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void TieneDictamenTec()
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            int TieneDictamen = ClGestion.Tiene_DictamenTecnico_Gestion(GestionId);
            if (TieneDictamen == 0)
                DivVerDicTec.Visible = false;
            else
                LblEstado.Text = LblEstado.Text + ", Expediente con dictamen técnico favorable";
                
        }


        void TieneEnmiendasTec()
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            int TieneDictamen = ClGestion.Tiene_EnmienasTecnico_Gestion(GestionId);
            if (TieneDictamen > 0)
            {
                DivEnmiendasTec.Visible = true;
                LblEstado.Text = LblEstado.Text + ", Expediente con informe de enmiendas técnicas";
                BtnVistaPreviaDictamen.Text = "Vista Previa Oficio de Enmiendas";
                BtnGrabarDictamen.Text = "Grabar Oficio de Enmiendas";
                TxtTieneEnmiendas.Text = "1";
            }
                
        }


        void ImgVerDictamenTecnico_Click(object sender, ImageClickEventArgs e)
        {
            //int DictamenTecId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["dictamentecid"].ToString()), true));
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

        void BtnGrabarDictamen_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                LblTitConfirmacion.Text = "La Gestíon sera enviada al director Regional, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnVistaPreviaDictamen_Click(object sender, EventArgs e)
        {
            if (TxtTieneEnmiendas.Text == "0")
            {
                if (ValidaDictamen() == false)
                {
                    int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    Session["DatosDictamenSubRegion"] = ClGestion.ImpresionDictamenSubRegional(GestionId, 1, Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(TxtFolios.Text), CboConsidera.Text);
                    RadWindow1.Title = "Vista Previa Dictamen Subregional";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenSubRegional.aspx";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
            else
            {
                
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                CargaDataSetEnmienda();
                Session["DatosEnmiendaSubRegion"] = ClGestion.ImpresionOficioEnmiendasSubRegional(GestionId, 1, Convert.ToInt32(Session["UsuarioId"]), Ds_Temporal);
                RadWindow1.Title = "Vista Previa Oficio de Enmiendas";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendasSubRegional.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                
            }
            
        }

        bool ValidaDictamen()
        {
            DivErrDictamen.Visible = false;
            bool HayError = false; 
            string LblMensaje = "";
            if (TxtFolios.Text == "")
            {
                if (LblMensaje == "")
                    LblMensaje = "Debe seleccionar la cantidad de folios";
                else
                    LblMensaje = LblMensaje + ", debe seleccionar la cantidad de folios";
                HayError = true;
            }
            if (HayError == true)
            {
                DivErrDictamen.Visible = true;
                LblErrDictamen.Text = LblMensaje;
            }
            return HayError;
        }

        void BtnGrabaResolucion_Click(object sender, EventArgs e)
        {
            
            if (Valida() == true)
                
                if (RadUploadExp.UploadedFiles.Count > 0)
                {
                    Stream fileStream = RadUploadExp.UploadedFiles[0].InputStream;
                    byte[] attachmentBytes = new byte[fileStream.Length];
                    fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                    fileStream.Close();
                    Session["bytes"] = attachmentBytes;
                    Session["ContentType"] = RadUploadExp.UploadedFiles[0].ContentType;
                    Session["FileName"] = RadUploadExp.UploadedFiles[0].FileName;
                }
                Session["Enmiendas"] = "0";
                LblTitConfirmacion.Text = "El sistema generara la resolución de aprobación, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);       
        }

        void BtnVPResolucion_Click(object sender, EventArgs e)
        {
            if (OptApruebaInscripción.SelectedValue == "1")
                RadWindow1.Title = "Vista Previa Resolución de Aprobación de Inscripción";
            else
                RadWindow1.Title = "Vista Previa Resolución de denegatoria de Inscripción";
            DataSet DatosResolucion = ClGestion.ImpresionResolucion_Aprobacion(1, Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(OptApruebaInscripción.SelectedValue));
            Session["DatosResolucion"] = DatosResolucion;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepResolucionAprobacion.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void OptApruebaInscripción_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptApruebaInscripción.SelectedValue == "1")
                DivCarga.Visible = true;
            else
                DivCarga.Visible = false;
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                if (Convert.ToInt32(Session["Enmiendas"]) == 1)
                {
                    Session["Enmiendas"] = 0;
                    string SubRegion = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int Oficio_Enmiendas_JuridicoId = ClGestion.Max_Oficio_Dictamen_Juridico();
                    ClGestion.Insert_Oficio_Enmiendas_Juridico(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), SubRegionId, Convert.ToInt32(Session["UsuarioId"]), SubRegion);
                    DataSet ds = ClGestion.Get_Datos_Persona_Desde_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string Correo = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                    string Solicitante = ds.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                    ds.Clear();
                    DataSet dsEnmiendas = ClGestion.Get_EnmiendasXml_Dictamen_juridico(Oficio_Enmiendas_JuridicoId);
                    string Mensaje = "<table>";
                    Mensaje = Mensaje + "<tr><td colspan=2>Enmiendas:</td></tr>";
                    for (int i = 0; i < dsEnmiendas.Tables["Datos"].Rows.Count; i++)
                    {
                        Mensaje = Mensaje + "<tr><td>-  </td><td>" + dsEnmiendas.Tables["Datos"].Rows[i]["Enmienda"] + "</td></tr>";
                    }
                    Mensaje = Mensaje + "</table>";
                    ClUtilitarios.EnvioCorreo(Correo, Solicitante, "Solicitud Con Enmiendas", Mensaje, 0, "", "");
                    ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 3);
                    DataSet dsDatosRegional = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string MensajeCorreo = "La gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true) + " tiene enmiendas juridicas";
                    ClUtilitarios.EnvioCorreo(dsDatosRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                    dsDatosRegional.Clear();
                    Response.Redirect("~/WebForms/Wfrm_Notificacion_deJuridico.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&oficioenmienda=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Oficio_Enmiendas_JuridicoId.ToString(), true)) + "");
                }
                else
                {
                    Session["Enmiendas"] = 0;
                    string SubRegion = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    int ResolucionId = ClGestion.Max_Resolucion();
                    ClGestion.Insert_Resolucion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(OptApruebaInscripción.SelectedValue), SubRegionId, SubRegion);
                    ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 10);
                    if (OptApruebaInscripción.SelectedValue == "1")
                    {
                        byte[] atachh = (byte[])Session["bytes"];
                        ClGestion.Inserta_Expediente_Resolucion(ResolucionId, atachh, Session["ContentType"].ToString(), Session["FileName"].ToString());
                    }
                    else
                    {
                        DataSet ds = ClGestion.Get_Datos_Persona_Desde_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string Correo = ds.Tables["Datos"].Rows[0]["Correo"].ToString();
                        string Solicitante = ds.Tables["Datos"].Rows[0]["Solicitante"].ToString();
                        ds.Clear();
                        string Mensaje = "";
                        if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                            Mensaje = "Su solicitud de inscripción al Registro Nacional Forestal ha sido denegada";
                        ClUtilitarios.EnvioCorreo(Correo, Solicitante, "Denegatoria  de Inscripción", Mensaje, 0, "", "");
                        ClGestion.Cambia_Estatus_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 4);
                    }
                    DataSet dsDatosRegional = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                    ClUtilitarios.EnvioCorreo(dsDatosRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                    dsDatosRegional.Clear();
                    Response.Redirect("~/WebForms/Wfrm_Notificacion_deJuridico.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&resolution=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(ResolucionId.ToString(), true)) + "");
                }
            }
            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                if (TxtTieneEnmiendas.Text == "0")
                {
                    int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    int Dictamen_SubRegional_Id = ClGestion.Max_Dictamen_SubRegional();
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(GestionId);
                    string Fincas = ClGestion.GetDatosFinca_Gestion_Juntos(GestionId);
                    string AgraegadoSol = "";
                    string Solicitante = "";
                    DataSet dsDatosDictamenSubRegional = ClGestion.Sp_Get_Datos_Dictamen_SubRegional(GestionId, 1, Convert.ToInt32(Session["UsuarioId"]));
                    int ModuloId = ClGestion.SP_Get_Modulo_Gestion(GestionId);
                    Solicitante = ClGestion.Get_Propietarios_Manejo(GestionId);
                    int Tipo = 1;
                    if (Tipo == 1)
                        AgraegadoSol = ClGestion.Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    else
                    {
                        AgraegadoSol = ClGestion.Get_CompletaPropietarios(Convert.ToInt32(dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["CategoriaId"]), GestionId, ModuloId);
                    }

                    if (AgraegadoSol != "")
                        Solicitante = Solicitante + " " + AgraegadoSol + ".";
                    else
                        Solicitante = Solicitante + ".";




                    string Asunto = Solicitante + " solicita aprobación del Plan de Manejo para " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["SubCategoria"].ToString() + " en finca (s) " + Fincas;
                    string Dictamen = "Con fecha " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Fecha_Gestion"].ToString() + ", el señor (a) (es): " + Solicitante + ", en su calidad de propietario (s) presentó solicitud a efecto se le autorice implementar Plan de Manejo de " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["SubCategoria"].ToString() + " constando a " + TxtFolios.Text + " folios; el informe jurídico No. " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["DictamenJuridico"].ToString() + " del Asesor (a) " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Juridico"].ToString() + ", quien se pronunció en forma favorable  en cuanto a la petición formulada  y el informe del Técnico No. " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["DictamenTecnico"].ToString() + " emitido por " + dsDatosDictamenSubRegional.Tables["Datos"].Rows[0]["Tecnico"].ToString() + " quien verificó las circunstancias consignadas en el plan de manejo. Habiendo sido objeto de análisis la petición planteada, esta Dirección Sub Regional considera que la solicitud llena los requisitos tanto desde el punto de vista técnico como legal. En virtud de lo anterior, salvo mejor opinión al respecto el suscrito RECOMIENDA que es " + CboConsidera.Text + " acceder a lo solicitado por " + Solicitante;

                    ClGestion.Insert_DictamenSubRegional(Dictamen_SubRegional_Id, GestionId, Convert.ToInt32(CboConsidera.SelectedValue), Convert.ToInt32(TxtFolios.Text), Convert.ToInt32(Session["UsuarioId"]), SubRegionId, Asunto, Dictamen);
                    ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 10);
                    DataSet dsDatosRegional = ClGestion.Get_Regional_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string MensajeCorreo = "Se ha enviado a su despacho la gestión de Manejo Forestal";
                    ClUtilitarios.EnvioCorreo(dsDatosRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                    dsDatosRegional.Clear();
                    Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("9", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "");
                }
                else
                {
                    int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                    int OficioEnmiendaSubregionalId = ClGestion.Max_Enmiendas_SubRegional();
                    int SubRegionId = ClGestion.Get_SubRegion_Gestion(GestionId);
                    
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Enmienda");
                    XmlNode iElementos = iInformacion.CreateElement("Enmienda");
                    for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
                    {
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("Enmienda", GrdEnmiendas.Items[i].GetDataKeyValue("Enmienda"), iElementoDetalle);
                        iElementos.AppendChild(iElementoDetalle);
                    }
                    iInformacion.ChildNodes[1].AppendChild(iElementos);
                    
                    ClGestion.Sp_Insert_EnmiendasSubRegional(OficioEnmiendaSubregionalId, GestionId, Convert.ToInt32(Session["UsuarioId"]), SubRegionId, iInformacion);
                    ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 1);
                    DataSet dsDatosUsuario = ClGestion.GetPersona_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string Nombres = dsDatosUsuario.Tables["Datos"].Rows[0]["Nombres"].ToString() + " " + dsDatosUsuario.Tables["Datos"].Rows[0]["Apellidos"].ToString();
                    string Correo = dsDatosUsuario.Tables["Datos"].Rows[0]["Correo"].ToString();
                    string MensajeCorreo = "Solicitud de enminedas para su licencia forestal";
                    int TieneEnmiendas = ClGestion.Tiene_Enmiendas_Dictamen_Juridico(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    if (TieneEnmiendas > 0)
                    {
                        DataSet dsDatosEnmeindaJuridica = ClGestion.GetEnmiendasJuridicas(GestionId);
                        MensajeCorreo = (("<body><table><tr><td colspan='2'><b>Enmiendas Jurídicas</b></td></tr><tr><td colspan='2'><b>DICTAMEN JURÍDICO No. " + dsDatosEnmeindaJuridica.Tables["Datos"].Rows[0]["No_Dictamen"].ToString() + "</b></td></tr>"));
                        for (int i = 0; i < dsDatosEnmeindaJuridica.Tables["DATOS"].Rows.Count; i++)
                        {
                            MensajeCorreo = MensajeCorreo + (("<tr><td>" + (i + 1).ToString() + "</td><td>" + dsDatosEnmeindaJuridica.Tables["Datos"].Rows[i]["Enmienda"].ToString() + " </td></tr>"));
                        }
                        MensajeCorreo = MensajeCorreo + (("</table></body>"));
                        
                        dsDatosEnmeindaJuridica.Clear();

                        int TieneDictamen = ClGestion.Tiene_EnmienasTecnico_Gestion(GestionId);
                        if (TieneDictamen > 0)
                        {
                            DataSet dsDatosEnmeindaTecnica = ClGestion.GetEnmiendasTecnicas(GestionId);
                            MensajeCorreo = MensajeCorreo + (("<body><table><tr><td colspan='2'><b>Enmiendas Técnicas</b></td></tr><tr><td colspan='2'><b>INFORME TÉCNICO No. " + dsDatosEnmeindaTecnica.Tables["Datos"].Rows[0]["No_Informe"].ToString() + "</b></td></tr>"));
                            for (int i = 0; i < dsDatosEnmeindaTecnica.Tables["DATOS"].Rows.Count; i++)
                            {
                                MensajeCorreo = MensajeCorreo + (("<tr><td>" + (i + 1).ToString() + "</td><td>" + dsDatosEnmeindaTecnica.Tables["Datos"].Rows[i]["Enmienda"].ToString() + " </td></tr>"));
                            }
                            MensajeCorreo = MensajeCorreo + (("</table></body>"));

                            dsDatosEnmeindaTecnica.Clear();
                        }


                        string OficioSubregional = ClGestion.Get_Oficio_SubRegional(GestionId);
                        MensajeCorreo = MensajeCorreo + (("<body><table><tr><td colspan='2'><b>Enmiendas del Director SubRegional</b></td></tr><tr><td colspan='2'><b>INFORME SUBREGIONAL No. " + OficioSubregional + "</b></td></tr>"));
                        for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
                        {
                            MensajeCorreo = MensajeCorreo + (("<tr><td>" + (i + 1).ToString() + "</td><td>" + GrdEnmiendas.Items[i].GetDataKeyValue("Enmienda") + " </td></tr>"));
                            
                        }
                        MensajeCorreo = MensajeCorreo + (("</table></body>"));
                        
                    }
                    ClUtilitarios.EnvioCorreo(Correo, Nombres, "Solicitud de enminedas para su licencia forestal", MensajeCorreo, 0, "", "");
                    dsDatosUsuario.Clear();
                    Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("12", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "");
                }
                
            }
            
            
        }

        void BtnEnviarOficio_Click(object sender, EventArgs e)
        {
            Session["Enmiendas"] = 1;
            LblTitConfirmacion.Text = "Se enviara notificación al usuario de las enmiendas, ¿esta seguro (a)?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            

        }

        void BtnVistaPreviaOficio_Click(object sender, EventArgs e)
        {
            RadWindow1.Title = "Vista Previa Oficio de enmiendas jurídicas";
            DataSet DatosOficioEnmiendas = ClGestion.ImpresionOficioEnmiendasJuridico(1,Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),Convert.ToInt32(Session["UsuarioId"]));
            Session["DatosOficioEnmiendasJuridico"] = DatosOficioEnmiendas;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepOficioEnmiendaJuridica.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgVerDictamenJuridico_Click(object sender, ImageClickEventArgs e)
        {
 	        RadWindow1.Title = "Dictamen Juridico";
            string Dictamen_Juridico_Id = ClGestion.Get_Ditamen_JuridicoId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet dsTemp= new DataSet();
            DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(2, Convert.ToInt32(Dictamen_Juridico_Id), 0, "", "", "", "", "",0, "", "", "", dsTemp, "");
            Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
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

        void ImgVerProvidencia_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Providencia para traslado de Expediente";
            string ProvidenciaId = ClGestion.Get_No_Providencia(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
            Session["DatosProvidencia"] = DatosProvidencia;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        bool Valida()
        {
            string Mensaje = "";
            DivError.Visible = false;
            bool HayError = false;
            if ((OptApruebaInscripción.SelectedValue == "1") && (RadUploadExp.UploadedFiles.Count == 0))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe cargar el scan del expediente en PDF";
                else
                    Mensaje = Mensaje + ", debe cargar el scan del expediente en PDF";
                HayError = true;
            }
            LblMensaje.Text = Mensaje;
            if (HayError == true)
            {
                DivError.Visible = true;
                return false;
            }

            else
                return true;
        }
    }
}