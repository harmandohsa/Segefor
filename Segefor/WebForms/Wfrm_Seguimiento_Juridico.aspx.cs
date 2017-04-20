using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using SEGEFOR.Data_Set;
using System.Xml;

namespace SEGEFOR.WebForms
{
   

    public partial class Wfrm_Seguimiento_Juridico : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Catalogos ClCatalogos;
        Cl_Xml ClXml;
        Ds_Temporales Ds_Temporal = new Ds_Temporales();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClCatalogos = new Cl_Catalogos();
            ClXml = new Cl_Xml();

            GrdArticulo.NeedDataSource += GrdArticulo_NeedDataSource;
            btnAddArticulo.ServerClick += btnAddArticulo_ServerClick;
            GrdArticulo.ItemCommand += GrdArticulo_ItemCommand;
            GrdAnalisis.NeedDataSource += GrdAnalisis_NeedDataSource;
            BtnAnalisis.ServerClick += BtnAnalisis_ServerClick;
            GrdAnalisis.ItemCommand += GrdAnalisis_ItemCommand;
            OptEnmiendas.SelectedIndexChanged += OptEnmiendas_SelectedIndexChanged;
            GrdEnmiendas.NeedDataSource += GrdEnmiendas_NeedDataSource;
            BtnAddEnmienda.ServerClick += BtnAddEnmienda_ServerClick;
            GrdEnmiendas.ItemCommand += GrdEnmiendas_ItemCommand;
            ImgVerProvidencia.Click += ImgVerProvidencia_Click;
            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            BtnEnviar.Click += BtnEnviar_Click;
            BtnYes.Click += BtnYes_Click;
            ImgVerinfo.Click += ImgVerinfo_Click;
            

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(32, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 32);
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
                ClUtilitarios.LlenaCombo(ClCatalogos.Considera_Dictamen_Juridico_GET(), CboConsidera, "ConsideraId", "Considera");
                ClUtilitarios.LlenaCombo(ClCatalogos.Opinion_Dictamen_Juridico_GET(1), CboOpinion, "OpinionId", "Opinion");

            }

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

        void BtnYes_Click(object sender, EventArgs e)
        {
            DataSet dsRegion = ClGestion.Get_Datos_Persona(2,Convert.ToInt32(Session["UsuarioId"]));
            int RegionId = Convert.ToInt32(dsRegion.Tables["Datos"].Rows[0]["RegionId"]);
            XmlDocument iInformacion = ClXml.CrearDocumentoXML("DictamenJuridico");
            XmlNode iElementos = iInformacion.CreateElement("Articulo");
            CargaDataSet();
            for (int i = 0; i < Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Count; i++)
            {
                XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                ClXml.AgregarAtributo("Articulo", Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows[i]["Articulo"] , iElementoDetalle);
                iElementos.AppendChild(iElementoDetalle);
            }
            iInformacion.ChildNodes[1].AppendChild(iElementos);

            XmlDocument iInformacionAnalisis = ClXml.CrearDocumentoXML("DictamenJuridico");
            XmlNode iElementosAnalisis = iInformacionAnalisis.CreateElement("Analisis");
            CargaDataSetAnalisis();
            for (int i = 0; i < Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Count; i++)
            {
                XmlNode iElementoDetalle = iInformacionAnalisis.CreateElement("Item");
                ClXml.AgregarAtributo("Analisis", Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows[i]["Analisis"] , iElementoDetalle);
                ClXml.AgregarAtributo("Orden", Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows[i]["Id_Analisis"], iElementoDetalle);
                iElementosAnalisis.AppendChild(iElementoDetalle);
            }
            iInformacionAnalisis.ChildNodes[1].AppendChild(iElementosAnalisis);

            XmlDocument iInformacionEnmiendas = ClXml.CrearDocumentoXML("DictamenJuridico");
            XmlNode iElementosEnmiendas = iInformacionEnmiendas.CreateElement("Enmiendas");
            CargaDataSetEnmienda();
            for (int i = 0; i < Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Count; i++)
            {
                XmlNode iElementoDetalle = iInformacionEnmiendas.CreateElement("Item");
                ClXml.AgregarAtributo("Enmienda", Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows[i]["Enmienda"], iElementoDetalle);
                ClXml.AgregarAtributo("Orden", Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows[i]["Id_Enmienda"], iElementoDetalle);
                iElementosEnmiendas.AppendChild(iElementoDetalle);
            }
            iInformacionEnmiendas.ChildNodes[1].AppendChild(iElementosEnmiendas);

            int Dictamen_Juridico_Id = ClGestion.Max_Dictamen_Juridico();
            ClGestion.Insert_Dictamen_Juridico( Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),TxtTitulo.Text,TxtTituloRegente.Text,iInformacion,TxtAnalisisGen.Text,iInformacionAnalisis,Convert.ToInt32(CboConsidera.SelectedValue),Convert.ToInt32(CboOpinion.SelectedValue),Convert.ToInt32(Session["UsuarioId"]),RegionId,iInformacionEnmiendas);
            ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
            DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
            string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
            ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
            dsDatosSubRegional.Clear();
            Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("4", true)) + "&consultationjuridique=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Dictamen_Juridico_Id.ToString(), true)) + "");
        }

        void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                LblTitConfirmacion.Text = "La Gestíon sera enviada al director subregional, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            { 
                int SubRegionalUsuario = ClGestion.Get_UsuarioSubRegional_Providencia(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                DataSet dsDatos_Solicitante = ClGestion.Get_Datos_Solicitante(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                string No_Expediente = dsDatos_Solicitante.Tables["Datos"].Rows[0]["No_Expediente"].ToString();
                string Solicitante = dsDatos_Solicitante.Tables["Datos"].Rows[0]["nombres"].ToString();
                string Solicitud  = "";
                string SubCategoria = dsDatos_Solicitante.Tables["Datos"].Rows[0]["Nombre_Subcategoria"].ToString();
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                    Solicitud = "solicita inscripción en el Registro Nacional Forestal de " + dsDatos_Solicitante.Tables["Datos"].Rows[0]["Nombre_Subcategoria"].ToString();
                dsDatos_Solicitante.Clear();
                
                string[] ParteExpediente = No_Expediente.Split('-');
                string No_ExpedienteLetras = "Número ";
                string Cod_SubCategoriaLetras = "";
                for (int i = 0; i < ParteExpediente.Length; i++)
                {
                    if (i == 0)
                        No_Expediente = ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                    else if (i == 1)
                        No_Expediente = No_Expediente + " guion " + ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                    else if (i == 2)
                    {
                        string[] Codigo_Subcategoria = ParteExpediente[i].Split('.');
                        for (int j = 0; j < Codigo_Subcategoria.Length; j++)
                        {
                            if (j == 0)
                                Cod_SubCategoriaLetras = ClUtilitarios.enletras(Codigo_Subcategoria[j]).ToLower();
                            else
                                Cod_SubCategoriaLetras = Cod_SubCategoriaLetras + " punto " + ClUtilitarios.enletras(Codigo_Subcategoria[j]).ToLower();
                        }
                        No_Expediente = No_Expediente + " guion " + Cod_SubCategoriaLetras;
                    }
                    else if (i == 3)
                        No_Expediente = No_Expediente + " guion " + ClUtilitarios.enletras(ParteExpediente[i]).ToLower();
                }
                RadWindow1.Title = "Vista Previa Dictamen Jurídico";
                CargaDataSet();
                CargaDataSetAnalisis();
                DataSet DatosDictamenJuridico = ClGestion.ImpresionDictamenJuridicoGestion(1, 0, SubRegionalUsuario, No_Expediente, TxtTitulo.Text, TxtTituloRegente.Text, CboConsidera.Text, CboOpinion.Text, Convert.ToInt32(Session["UsuarioId"]), Solicitante, Solicitud, SubCategoria, Ds_Temporal,TxtAnalisisGen.Text);
                Session["DatosDictamenJuridico"] = DatosDictamenJuridico;
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepDictamenJuridico.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        bool Valida()
        {
            bool HayError = false;
            DivError.Visible = false;
            string Mensaje = "";
            LblMensaje.Text = "";
            if (GrdArticulo.Items.Count == 0)
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar al menos un articulo en fundamento legal";
                else
                    Mensaje = Mensaje + ", debe ingresar al menos un articulo en fundamento legal";
                HayError = true;
            }
            if (GrdAnalisis.Items.Count == 0)
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar al menos un punto de analísis";
                else
                    Mensaje = Mensaje + ", debe ingresar al menos un punto de analísis";
                HayError = true;
            }
            if ((OptEnmiendas.SelectedItem.Value == "1") && (GrdEnmiendas.Items.Count == 0))
            {
                if (Mensaje == "")
                    Mensaje = "Debe ingresar al menos una enmienda";
                else
                    Mensaje = Mensaje + ", debe ingresar al menos una enmienda";
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

        void ImgVerProvidencia_Click(object sender, ImageClickEventArgs e)
        {
            RadWindow1.Title = "Providencia para traslado de Expediente";
            string ProvidenciaId = ClGestion.Get_No_Providencia(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))).ToString();
            DataSet DatosProvidencia = ClGestion.ImpresionProvidenciaGestion(2, Convert.ToInt32(ProvidenciaId), "", "", Convert.ToInt32(Session["UsuarioId"]), 0);
            Session["DatosProvidencia"] = DatosProvidencia;
            RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepProvidenciaTrasladoExp.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void GrdEnmiendas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEditar")
            {
                TxtIdEnmienda.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Enmienda"].ToString();
                TxtEnmienda.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Enmienda"].ToString();
            }
            if (e.CommandName == "CmdDel")
            {
                EliminarEnmienda(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Enmienda"]));
                TxtEnmienda.Text = "";
                TxtIdEnmienda.Text = "";

            }
        }

        void BtnAddEnmienda_ServerClick(object sender, EventArgs e)
        {
            if (TxtEnmienda.Text == "")
                TxtEnmienda.Focus();
            else
            {
                if (TxtIdEnmienda.Text == "")
                    AgregarEnmienda(TxtEnmienda.Text);
                else
                    ModificarEnmienda(Convert.ToInt32(TxtIdEnmienda.Text), TxtEnmienda.Text);
                GrdEnmiendas.Rebind();
                TxtEnmienda.Text = "";
                TxtIdEnmienda.Text = "";
            }
        }

        void GrdEnmiendas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdEnmiendas, "Dt_Enmienda_Dictamen_Juridico");
        }

        void OptEnmiendas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptEnmiendas.SelectedItem.Value == "1")
            {
                DivEnmiendas.Visible = true;
                DivEnmiendaGrid.Visible = true;
                ClUtilitarios.LlenaCombo(ClCatalogos.Opinion_Dictamen_Juridico_GET(2), CboOpinion, "OpinionId", "Opinion");
            }
            else
            {
                DivEnmiendas.Visible = false;
                DivEnmiendaGrid.Visible = false;
                ClUtilitarios.LlenaCombo(ClCatalogos.Opinion_Dictamen_Juridico_GET(1), CboOpinion, "OpinionId", "Opinion");
            }
        }

        

        void GrdAnalisis_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEditar")
            {
                TxtIdAnalisis.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Analisis"].ToString();
                TxtAnalis.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Analisis"].ToString();
            }
            if (e.CommandName == "CmdDel")
            {
                EliminarAnalisis(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Analisis"]));
                TxtAnalis.Text = "";
                TxtIdAnalisis.Text = "";

            }
        }

        void BtnAnalisis_ServerClick(object sender, EventArgs e)
        {
            if (TxtAnalis.Text == "")
                TxtAnalis.Focus();
            else
            {
                if (TxtIdAnalisis.Text == "")
                    AgregarAnalisis(TxtAnalis.Text);
                else
                    ModificarAnalisis(Convert.ToInt32(TxtIdAnalisis.Text), TxtAnalis.Text);
                GrdAnalisis.Rebind();
                TxtAnalis.Text = "";
                TxtIdAnalisis.Text = "";
            }
        }

        void GrdAnalisis_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdAnalisis, "Dt_Analisis_Dictamen_Juridico");
        }

        void GrdArticulo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdEditar")
            {
                TxtIdArticulo.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Articulo"].ToString();
                TxtArticulo.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Articulo"].ToString();
            }
            if (e.CommandName == "CmdDel")
            {
                EliminarArticulo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id_Articulo"]));
                TxtArticulo.Text = "";
                TxtIdArticulo.Text = "";
            }
        }

        void CargaDataSet()
        {
            for (int i = 0; i < GrdArticulo.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].NewRow();
                row["Id_Articulo"] = Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Count;
                row["Articulo"] = GrdArticulo.Items[i].OwnerTableView.DataKeyValues[i]["Articulo"];
                Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Add(row);
            }
        }

        void CargaDataSetAnalisis()
        {
            for (int i = 0; i < GrdAnalisis.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].NewRow();
                row["Id_Analisis"] = Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Count;
                row["Analisis"] = GrdAnalisis.Items[i].OwnerTableView.DataKeyValues[i]["Analisis"];
                Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Add(row);
            }
        }

        void CargaDataSetEnmienda()
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                DataRow row = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].NewRow();
                row["Id_Enmienda"] = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Count;
                row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Add(row);
            }
        }

        void btnAddArticulo_ServerClick(object sender, EventArgs e)
        {
            if (TxtArticulo.Text == "")
                TxtArticulo.Focus();
            else
            {
                if (TxtIdArticulo.Text == "")
                    AgregarArticulo(TxtArticulo.Text);
                else
                    ModificarArticulo(Convert.ToInt32(TxtIdArticulo.Text), TxtArticulo.Text);
                GrdArticulo.Rebind();
                TxtArticulo.Text = "";
                TxtIdArticulo.Text = "";
            }
        }

        void EliminarArticulo(int Item)
        {
            for (int i = 0; i < GrdArticulo.Items.Count; i++)
            {
                if (Item != Convert.ToInt32(GrdArticulo.Items[i].OwnerTableView.DataKeyValues[i]["Id_Articulo"]))
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].NewRow();
                    row["Id_Articulo"] = Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Count;
                    row["Articulo"] = GrdArticulo.Items[i].OwnerTableView.DataKeyValues[i]["Articulo"];
                    Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Add(row);
                }
            }
            GrdArticulo.Rebind();
        }

        void EliminarAnalisis(int Item)
        {
            for (int i = 0; i < GrdAnalisis.Items.Count; i++)
            {
                if (Item != Convert.ToInt32(GrdAnalisis.Items[i].OwnerTableView.DataKeyValues[i]["Id_Analisis"]))
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].NewRow();
                    row["Id_Analisis"] = Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Count;
                    row["Analisis"] = GrdAnalisis.Items[i].OwnerTableView.DataKeyValues[i]["Analisis"];
                    Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Add(row);
                }
            }
            GrdAnalisis.Rebind();
        }

        void EliminarEnmienda(int Item)
        {
            for (int i = 0; i < GrdEnmiendas.Items.Count; i++)
            {
                if (Item != Convert.ToInt32(GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Id_Enmienda"]))
                {
                    DataRow row = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].NewRow();
                    row["Id_Enmienda"] = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Count;
                    row["Enmienda"] = GrdEnmiendas.Items[i].OwnerTableView.DataKeyValues[i]["Enmienda"];
                    Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Add(row);
                }
            }
            GrdEnmiendas.Rebind();
        }


        void ModificarArticulo(int Item,  string Articulo)
        {
            CargaDataSet();
            Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows[Item]["Articulo"] = Articulo;
            GrdArticulo.Rebind();
        }

        void ModificarAnalisis(int Item, string Analisis)
        {
            CargaDataSetAnalisis();
            Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows[Item]["Analisis"] = Analisis;
            GrdAnalisis.Rebind();
        }

        void ModificarEnmienda(int Item, string Enmienda)
        {
            CargaDataSetEnmienda();
            Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows[Item]["Enmienda"] = Enmienda;
            GrdEnmiendas.Rebind();
        }


        void AgregarArticulo(string Articulo)
        {
            CargaDataSet();
            DataRow rowNew = Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].NewRow();
            rowNew["Id_Articulo"] = GrdArticulo.Items.Count;
            rowNew["Articulo"] = Articulo;
            Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Add(rowNew);
        }

        void AgregarAnalisis(string Analisis)
        {
            CargaDataSetAnalisis();
            DataRow rowNew = Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].NewRow();
            rowNew["Id_Analisis"] = GrdAnalisis.Items.Count;
            rowNew["Analisis"] = Analisis;
            Ds_Temporal.Tables["Dt_Analisis_Dictamen_Juridico"].Rows.Add(rowNew);
        }

        void AgregarEnmienda(string Enmienda)
        {
            CargaDataSetEnmienda();
            DataRow rowNew = Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].NewRow();
            rowNew["Id_Enmienda"] = GrdEnmiendas.Items.Count;
            rowNew["Enmienda"] = Enmienda;
            Ds_Temporal.Tables["Dt_Enmienda_Dictamen_Juridico"].Rows.Add(rowNew);
        }

        void GrdArticulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Articulo_Dictamen_Juridico"].Rows.Count > 0)
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdArticulo,"Dt_Articulo_Dictamen_Juridico");
        }
    }
}