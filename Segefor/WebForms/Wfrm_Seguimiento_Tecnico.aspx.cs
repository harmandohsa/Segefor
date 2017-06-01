using SEGEFOR.Clases;
using SEGEFOR.Data_Set;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


        Ds_Temporales Ds_Temporal = new Ds_Temporales();

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

            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClCatalogos = new Cl_Catalogos();
            ClXml = new Cl_Xml();
            ClManejo = new Cl_Manejo();
            ClManejoImpresion = new Cl_Manejo_Impresion();

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
                //ClUtilitarios.LlenaCombo(ClCatalogos.Opinion_Dictamen_Juridico_GET(1), CboOpinion, "OpinionId", "Opinion");
                CargaInforGeneral();
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
                    int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2);
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
                        row["No"] = i + 1;
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
            }
            else
            {
                DivEnmiendas.Visible = false;
                DivEnmiendaGrid.Visible = false;
                DivEnmiendasBotonos.Visible = false;
            }
        }

        void GrdInmuebles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            ClUtilitarios.LlenaGrid(ClManejo.GetFincaPlanManejoPol(GestionId, 2), GrdInmuebles);
        }

        void CargaInforGeneral()
        {
            int ModuloId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true));
            int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
            int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(GestionId, 2);
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
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2);
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