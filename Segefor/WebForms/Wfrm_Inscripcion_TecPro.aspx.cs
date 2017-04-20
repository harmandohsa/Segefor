using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using SEGEFOR.Data_Set;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Inscripcion_TecPro : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;
        Cl_Gestion_Registro ClGestionRegistro;
        DataSet dsPersona = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();
            ClGestionRegistro = new Cl_Gestion_Registro();

            CboRegion.SelectedIndexChanged += CboRegion_SelectedIndexChanged;
            CboDepartamento.SelectedIndexChanged += CboDepartamento_SelectedIndexChanged;
            CboActividad.SelectedIndexChanged += CboActividad_SelectedIndexChanged;
            CboProfesion.SelectedIndexChanged += CboProfesion_SelectedIndexChanged;
            BtnVistaPrevia.Click += BtnVistaPrevia_Click;
            BtnEnviar.Click += BtnEnviar_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(23, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 23);
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

                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoRegion(), CboRegion, "RegionId", "Nombre");
                ClUtilitarios.AgregarSeleccioneCombo(CboRegion, "Región");
                ClUtilitarios.AgregarSeleccioneCombo(CboSubRegion, "Subregión");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDepartamento, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDepartamento, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSubCategoriasRegistro(7,Convert.ToInt32(Session["PersonaId"])), CboActividad, "SubCategoriaId", "Nombre_Subcategoria");
                ClUtilitarios.AgregarSeleccioneCombo(CboActividad, "Actividad");
                CargaInfo();
                TxtCelular.Enabled = true;
            }

        }

        void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos() == true)
            {
                int GestionId = ClGestionRegistro.MaxGestionId(1);
                int Correlativo_Anual = ClGestionRegistro.MaxGestionId(2);
                string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;
                int Telefono = 0;
                if (TxtTelefono.Text != "")
                    Telefono = Convert.ToInt32(TxtTelefono.Text.Replace("-", ""));
                ClGestionRegistro.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(CboSubRegion.SelectedValue), 13, 3, Correlativo_Anual);
                ClGestionRegistro.Insertar_Gestion_Profesional(GestionId,Telefono, Convert.ToInt32(CboProfesion.SelectedValue), Convert.ToInt32(CboCategoriaProfesion.SelectedValue), TxtNoCol.Text, TxtDiploma.Text, TxtDireccionNotifica.Text, Convert.ToInt32(CboMunicipio.SelectedValue), TxtObservaciones.Text, TxtNomFirma.Text, Convert.ToInt32(CboActividad.SelectedValue), 7,TxtAldea.Text);
                GenerarFormulario(2,NUG);
                string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + NUG + ". Por lo que solicitamos presentarse a la oficina Subregional " + CboSubRegion.SelectedItem.Text + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
                ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 0, "", "");
                ClPersona.ActualizaNit_Persona(TxtNit.Text,Convert.ToInt32(Session["PersonaId"]));
                ClPersona.ActualizaCel_Persona(Convert.ToInt32(TxtCelular.Text.Replace("-","")), Convert.ToInt32(Session["PersonaId"]));
                Response.Redirect("~/WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG, true)) + "");
            }
        }

        void BtnVistaPrevia_Click(object sender, EventArgs e)
        {
            if (ValidaDatos() == true)
            {
                RadWindow1.Title = "Vista Previa Insripción";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioProfesional.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("00", true)) + "";
                GenerarFormulario(1,"");
            }
        }


        void GenerarFormulario(int Opcion, string NUG) //1 Vista Previa, 2Envio de Solicitud
        {
            Ds_Profesionales Ds_Inscripcion = new Ds_Profesionales();
            Ds_Inscripcion.Tables["Dt_Formulario"].Clear();
            DataRow row = Ds_Inscripcion.Tables["Dt_Formulario"].NewRow();
            if (CboCategoriaProfesion.SelectedValue == "1")
                row["Requisitos"] = "Requisitos:\n1) Copia legalizada del título;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU); y  \n3) Copia de documento personal de identificación (DPI).";
            else if (CboCategoriaProfesion.SelectedValue == "2")
                row["Requisitos"] = "Requisitos:\n1) Constancia original de colegiado activo vigente;  \n2) Constancia de inscripción en el Registro Tributario Unificado (RTU);   y \n3) Copia de documento personal de identificación (DPI).\nPara profesionales con post grado en materia forestal, presentar el documento extendido por la universidad que lo avala.";
            if (CboActividad.SelectedValue == "1" || CboActividad.SelectedValue == "16")
                row["Requisitos"] = row["Requisitos"] + "\nademás de los requisitos anteriores, copia del diploma de aprobación del curso correspondiente.";
            row["Region"] = CboRegion.Text;
            row["SubRegion"] = CboSubRegion.Text;
            if (Opcion == 1)
                row["NUG"] = "---------------";
            else
                row["NUG"] = NUG;
            row["Fecha"] = string.Format("{0:dd/MM/yyyy}", ClUtilitarios.FechaDB());
            row["Actividad"] = CboActividad.Text;
            if ((CboActividad.SelectedValue == "1") || (CboActividad.SelectedValue == "16"))
                row["ActividadId"] = 1;
            else
                row["ActividadId"] = 0;
            row["Nombres"] = TxtNombre.Text;
            row["Apellidos"] = TxtApellidos.Text;
            row["DPI"] = TxtDpi.Text;
            row["NIT"] = TxtNit.Text;
            row["Telefono"] = TxtTelefono.Text;
            row["Celular"] = TxtCelular.Text;
            row["Correo"] = TxtCorreo.Text;
            row["Direccion"] = TxtDireccion.Text;
            row["Municipio"] = TxtMunicipio.Text;
            row["Departamento"] = TxtDepartamento.Text;
            row["CategoriaProfesion"] = CboCategoriaProfesion.Text;
            if (CboCategoriaProfesion.SelectedValue == "1")
                row["No_Colegiado"] = "---------------";
            else
                row["No_Colegiado"] = TxtNoCol.Text;
            if (CboActividad.SelectedValue == "1" || CboActividad.SelectedValue == "16")
                row["No_Diploma"] = TxtDiploma.Text;
            else
                row["No_Diploma"] = "-----------------";
            row["Direccion_Notifica"] = TxtDireccionNotifica.Text;
            row["Aldea_Notifica"] = TxtAldea.Text;
            row["Municipio_Notifica"] = CboMunicipio.Text;
            row["Departamento_Notifica"] = CboDepartamento.Text;
            row["Observaciones"] = TxtObservaciones.Text;
            row["Nombre"] = TxtNomFirma.Text;
            row["Profesion"] = CboProfesion.Text;
            if (LblTipoId.InnerText == "DPI")
                row["LabelId"] = "2.3 Número de DPI:";
            else
                row["LabelId"] = "2.3 Número de Pasaporte:";
            Ds_Inscripcion.Tables["Dt_Formulario"].Rows.Add(row);
            Session["DataFormulario"] = Ds_Inscripcion;
            if (Opcion == 1)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        bool ValidaDatos()
        {
            LblMensaje.Text = "";
            BtnEror.Visible = false;
            bool HayError = false;
            if ((CboRegion.SelectedValue == "") || (CboRegion.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la Región";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar la Región";
                HayError = true;
            }
            if ((CboSubRegion.SelectedValue == "") || (CboSubRegion.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la Subregión";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar la Subregión";
                HayError = true;
            }
            if ((CboActividad.Text == "") || (CboActividad.SelectedValue == "") || (CboActividad.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar la actividad";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar la actividad";
                HayError = true;
            }
            if (Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecVen.Text)) < Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", ClUtilitarios.FechaDB())))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "la vigencia de su documento de identificación esta vencida, para modificar la fecha de vencimiento, ingresar a la pantalla perfil de usuario.";
                else
                    LblMensaje.Text = LblMensaje.Text + ", la vigencia de su documento de identificación esta vencida, para modificar la fecha de vencimiento, ingresar a la pantalla perfil de usuario.";
                HayError = true;
            }
            if (TxtNit.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar nu número de NIT";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe ingresar nu número de NIT";
                HayError = true;
            }
            if ((CboProfesion.Text == "") || (CboProfesion.SelectedValue == "") ||(CboProfesion.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar su profesión";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar su profesión";
                HayError = true;
            }
            if ((CboCategoriaProfesion.Text == "") || (CboCategoriaProfesion.SelectedValue == "") || (CboCategoriaProfesion.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar su grado académico";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe Seleccionar su grado académico";
                HayError = true;
            }
            if (CboCategoriaProfesion.SelectedValue == "2" && TxtNoCol.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su número de colegiado";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe ingresar su número de colegiado";
                HayError = true;
            }
            if ((CboActividad.SelectedValue == "1" && TxtDiploma.Text == "") || (CboActividad.SelectedValue == "16" && TxtDiploma.Text == ""))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su número de diploma";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe ingresar su número de diploma";
                HayError = true;
            }
            if ((TxtDiploma.Text != "") && (ClGestionRegistro.existediploma(TxtDiploma.Text) == true))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Este número de diploma ya está ingresado en sistema";
                else
                    LblMensaje.Text = LblMensaje.Text + ", este número de diploma ya está ingresado en sistema";
                HayError = true;
            }
            if ((TxtNit.Text != "") && (ClGestionRegistro.ExisteNit(TxtNit.Text,Convert.ToInt32(Session["PersonaId"])) == true))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Este número de Identificación Tributaria registrado por otro usuario, verificar NIT ó comunicarse a la oficina más cercana para su corrección";
                else
                    LblMensaje.Text = LblMensaje.Text + ", este número de Identificación Tributaria registrado por otro usuario, verificar NIT ó comunicarse a la oficina más cercana para su corrección";
                HayError = true;
            }
            if (TxtDireccionNotifica.Text == "")
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe ingresar su dirección de notificación";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe ingresar su dirección de notificación";
                HayError = true;
            }
            if ((CboDepartamento.SelectedValue == "") || (CboDepartamento.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el departamento";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar el departamento";
                HayError = true;
            }
            if ((CboMunicipio.SelectedValue == "") || (CboMunicipio.SelectedValue == "0"))
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Debe seleccionar el municipio";
                else
                    LblMensaje.Text = LblMensaje.Text + ", Debe seleccionar el municipio";
                HayError = true;
            }
            if ((TxtCorreo.Text != "") && (ClUtilitarios.EsInstitucional(TxtCorreo.Text) == true))
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
                    LblMensaje.Text = LblMensaje.Text + ", Debe ingresar su nombre y firma";
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

        void CboProfesion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if ((CboProfesion.SelectedValue != "") || (CboProfesion.SelectedValue != "0"))
            {
                CboCategoriaProfesion.ClearSelection();
                CboCategoriaProfesion.Items.Clear();
                ClUtilitarios.LlenaCombo(ClCatalogos.CategoriaProfesion_Get(Convert.ToInt32(CboProfesion.SelectedValue)), CboCategoriaProfesion, "CategoriaProfesionId", "CategoriaProfesion");
                //ClUtilitarios.AgregarSeleccioneCombo(CboCategoriaProfesion, "Grado Académico");
            }
            else
            {
                CboCategoriaProfesion.ClearSelection();
                CboCategoriaProfesion.Items.Clear();
            }
        }

        void CboActividad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if ((CboActividad.SelectedValue != "") || (CboActividad.SelectedValue != "0"))
            {
                CboProfesion.ClearSelection();
                CboProfesion.Items.Clear();
                CboCategoriaProfesion.ClearSelection();
                CboCategoriaProfesion.Items.Clear();
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoProfesionesPorActividad(7, Convert.ToInt32(CboActividad.SelectedValue)), CboProfesion, "ProfesionId", "Profesion");
                ClUtilitarios.AgregarSeleccioneCombo(CboProfesion, "Profesión");
            }
            else
            {
                CboProfesion.ClearSelection();
                CboProfesion.Items.Clear();
                CboCategoriaProfesion.ClearSelection();
                CboCategoriaProfesion.Items.Clear();
            }
            
        }

        void CboDepartamento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDepartamento.SelectedValue)), CboMunicipio, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMunicipio, "Municipio");
        }

        void CboRegion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoSubRegion(Convert.ToInt32(CboRegion.SelectedValue)), CboSubRegion, "SubRegionId", "Nombre");
            ClUtilitarios.AgregarSeleccioneCombo(CboSubRegion, "Subregión");
        }

        void 
            CargaInfo()
        {
            dsPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
            TxtNombre.Text = dsPersona.Tables["DATOS"].Rows[0]["Nombres"].ToString();
            TxtApellidos.Text = dsPersona.Tables["DATOS"].Rows[0]["Apellidos"].ToString();
            TxtNomFirma.Text = dsPersona.Tables["DATOS"].Rows[0]["Nombres"].ToString() + " " + dsPersona.Tables["DATOS"].Rows[0]["Apellidos"].ToString();
            TxtNomFirma.Enabled = false;
            if (dsPersona.Tables["DATOS"].Rows[0]["Origen_PersonaId"].ToString() == "2")
                TxtDpi.Text = dsPersona.Tables["DATOS"].Rows[0]["dpi"].ToString();
            else
            {
                if (dsPersona.Tables["DATOS"].Rows[0]["dpi"].ToString().Length == 13)
                    TxtDpi.Text = dsPersona.Tables["DATOS"].Rows[0]["dpi1"].ToString() + "-" + dsPersona.Tables["DATOS"].Rows[0]["dpi2"].ToString() + "-" + dsPersona.Tables["DATOS"].Rows[0]["dpi3"].ToString();

                else
                    TxtDpi.Text = "";
            }
            if (dsPersona.Tables["DATOS"].Rows[0]["Nit"].ToString() != "")
            {
                TxtNit.Text = dsPersona.Tables["DATOS"].Rows[0]["Nit"].ToString();
                TxtNit.Enabled = false;
            }
            TxtCelular.Text = dsPersona.Tables["DATOS"].Rows[0]["tel1"].ToString() + "-" + dsPersona.Tables["DATOS"].Rows[0]["tel2"].ToString();
            TxtCorreo.Text = dsPersona.Tables["DATOS"].Rows[0]["correo"].ToString();
            TxtDireccion.Text = dsPersona.Tables["DATOS"].Rows[0]["direccion"].ToString();
            TxtDepartamento.Text = dsPersona.Tables["DATOS"].Rows[0]["departamento"].ToString();
            TxtMunicipio.Text = dsPersona.Tables["DATOS"].Rows[0]["municipio"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["Origen_PersonaId"].ToString() == "1")
            {
                LblTipoId.InnerText = "DPI";
            }
            else
            {
                LblTipoId.InnerText = "Número de Pasaporte";
            }
            if (dsPersona.Tables["DATOS"].Rows[0]["FecVenId"].ToString() != "")
                TxtFecVen.Text = dsPersona.Tables["DATOS"].Rows[0]["FecVenId"].ToString();
        }
    }
}