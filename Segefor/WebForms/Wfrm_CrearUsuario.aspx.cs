using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Web.Services;
using System.Web.Script.Serialization;


namespace SEGEFOR.WebForms
{
    public partial class Wfrm_CrearUsuario : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Catalogos ClCatalogos;
        Cl_Persona ClPersona;
        DataSet ds = new DataSet();
        Regex rex = new Regex("^[A-Z a-z á-é-í-ó-ú Á-É-Í-Ó-Í-Ú]*$");

        protected void Page_Load(object sender, EventArgs e)
        {
            ClCatalogos = new Cl_Catalogos();
            ClUtilitarios = new Cl_Utilitarios();
            ClUsuario = new Cl_Usuario();
            ClPersona = new Cl_Persona();
            CboDep.SelectedIndexChanged += CboDep_SelectedIndexChanged;
            BtnCrearUsuario.Click += BtnCrearUsuario_Click;
            CboTipoIdentificacion.SelectedIndexChanged += CboTipoIdentificacion_SelectedIndexChanged;
            LnkTerminos.Click += LnkTerminos_Click;

            System.Web.UI.HtmlControls.HtmlGenericControl sidvar;
            sidvar = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("MenuAdm");
            sidvar.Visible = false;

            System.Web.UI.WebControls.Label lblUsuario;
            lblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
            lblUsuario.Text = "Nuevo Usuario";

            System.Web.UI.WebControls.Label LblTipoUsuario;
            LblTipoUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
            LblTipoUsuario.Visible = false;

            System.Web.UI.HtmlControls.HtmlGenericControl lines;
            lines = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("lines");
            lines.Visible = false;

            System.Web.UI.HtmlControls.HtmlGenericControl Notif;
            Notif = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("Notif");
            Notif.Visible = false;

            System.Web.UI.HtmlControls.HtmlImage ImgPerfil;
            ImgPerfil = (System.Web.UI.HtmlControls.HtmlImage)Master.FindControl("ImgPerfilFake");
            ImgPerfil.Src = "../imagenes/New_User.png";
            ImgPerfil.Visible = true;
            TxtFecNac.MinDate = new DateTime(1920, 1, 1);
            TxtFecNac.MaxDate = DateTime.Today;
            

            if (!IsPostBack)
            {
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoGenero(), CboGenero, "generoid", "genero");
                ClUtilitarios.AgregarSeleccioneCombo(CboGenero, "Genero");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoDepartamentos(), CboDep, "DepartamentoId", "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboDep, "Departamento");
                ClUtilitarios.AgregarSeleccioneCombo(CboMun, "Municipio");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoTipoIdentificacion(), CboTipoIdentificacion, "Origen_PersonaId", "Origen_Persona");
                ClUtilitarios.AgregarSeleccioneCombo(CboTipoIdentificacion, "Tipo de identificación");
                TxtFecNac.SelectedDate = DateTime.Today;
                TxtFecNac.MaxDate = DateTime.Today;
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoPais(), CboPais, "PaisId", "Pais");
                ClUtilitarios.AgregarSeleccioneCombo(CboPais, "Pais");
                TxtFecVenId.MinDate = DateTime.Today;
            }
        }

        void LnkTerminos_Click(object sender, EventArgs e)
        {
            RadWindow1.Title = "Terminos y condiciones de uso";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void CboTipoIdentificacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboTipoIdentificacion.SelectedValue == "1")
            {
                DivDpi.Visible = true;
                DivPasaporte.Visible = false;
                LblDireccion.InnerText = "Dirección";
                DivPais.Visible = false;

            }
            else
            {
                DivDpi.Visible = false;
                DivPasaporte.Visible = true;
                LblDireccion.InnerText = "Dirección en Guatemala";
                DivPais.Visible = true;
            }
        }

        void BtnCrearUsuario_Click(object sender, EventArgs e)
        {
            if (Valida() == true)
            {
                int UsuarioId = ClUsuario.UsurioId();
                int PersonaId  = ClPersona.MaxPersonaId();
                string Clave = ClUtilitarios.Encrypt(ClUtilitarios.GenerarPass(6, 10), true);
                int PaisId = 0;
                if (CboTipoIdentificacion.SelectedValue == "2")
                    PaisId = Convert.ToInt32(CboPais.SelectedValue);
                ClPersona.Insertar_Persona(PersonaId, TxtNombre.Text, TxtApellido.Text, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecNac.SelectedDate)), Convert.ToInt32(CboGenero.SelectedValue), ClUtilitarios.IIf(CboTipoIdentificacion.SelectedValue == "1", TxtDpi.Text.Replace("-", ""),TxtPasaporte.Text).ToString(), TxtCelular.Text.Replace("-", ""), TxtDireccion.Text, Convert.ToInt32(CboMun.SelectedValue),Convert.ToInt32(CboTipoIdentificacion.SelectedValue), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecVenId.SelectedDate)), PaisId);
                ClUsuario.Insertar_Usuario(UsuarioId, TxtCorreo.Text, 1, Clave, PersonaId, 1, 0, TxtCorreo.Text);
                ClUsuario.Insertar_Permisos(UsuarioId, 1);
                string Asunto = "Notificacion de creación de Usuario";
                string Mensaje = "<body><table><tr><td>Le informamos que se ha creado su usuario para poder acceder al sistema: Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + TxtCorreo.Text + ", la contraseña: " + ClUtilitarios.Decrypt(Clave,true) + "</td></tr></table>";
                ClUtilitarios.EnvioCorreo(TxtCorreo.Text, TxtNombre.Text + ' ' + TxtApellido.Text, Asunto, Mensaje,0,"","");
                Response.Redirect("Wfrm_RegistroCompleto.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PersonaId.ToString(), true) + ""));
            }
        }

        void CboDep_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClUtilitarios.LlenaCombo(ClCatalogos.ListadoMunicipios(Convert.ToInt32(CboDep.SelectedValue)), CboMun, "MunicipioId", "Municipio");
            ClUtilitarios.AgregarSeleccioneCombo(CboMun, "Municipio");
        }

        bool Valida()
        {
            string Mensaje = "";
            BtnEror.Visible = false;
            bool HayError = false;
            bool HayErrorFecha = false;
            if (!rex.IsMatch(TxtNombre.Text))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "No puede ingresar números en su nombre";
                else
                    Mensaje = Mensaje + ", No puede ingresar números en su nombre";
                HayError = true;
                TxtNombre.Focus();
            }
            if (!rex.IsMatch(TxtApellido.Text))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "No puede ingresar números en su apellido";
                else
                    Mensaje = Mensaje + ", No puede ingresar números en su apellido";
                HayError = true;

            }
            if (ClUtilitarios.email_bien_escrito(TxtCorreo.Text) == false)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe ingresar el correo electrónico en formato correcto";
                else
                    Mensaje = Mensaje + ", Debe ingresar el correo electrónico en formato correcto";
                HayError = true;
            }
            if (ClUtilitarios.email_bien_escrito(TxtCorreoVal.Text) == false)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe ingresar el correo electrónico de validación en formato correcto";
                else
                    Mensaje = Mensaje + ", Debe ingresar el correo electrónico  de validación en formato correcto";
                HayError = true;
            }
            if (TxtCorreo.Text != TxtCorreoVal.Text)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Los Correos Electrónicos no coinciden";
                else
                    Mensaje = Mensaje + ", Los Correos Electrónicos no coinciden";
               
            }
            if (ClUsuario.Existe_Correo(TxtCorreo.Text) == true)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Ya Existe este correo electrónico en nuestros registros";
                else
                    Mensaje = Mensaje + ", Ya Existe este correo electrónico en nuestros registros";
                HayError = true;
            }
            if (ClUsuario.Existe_Usuario(TxtCorreo.Text) == true)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Un usuario ha utilizado su correo electrónico como usuario";
                else
                    Mensaje = Mensaje + ", Un usuario ha utilizado su correo electrónico como usuario";
                HayError = true;
            }
            if (ClUtilitarios.EsInstitucional(TxtCorreo.Text) == true)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "No puede agregar correos del dominio inab.gob.gt";
                else
                    Mensaje = Mensaje + ", No puede agregar correos del dominio inab.gob.gt";
                HayError = true;
            }
            if (TxtFecNac.DateInput.Text == "")
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe ingresar su fecha de nacimiento";
                else
                    Mensaje = Mensaje + ", Debe ingresar su fecha de nacimiento";
                HayError = true;
                HayErrorFecha = true;
            }
            if ((TxtFecNac.DateInput.Text != "") && (Convert.ToDateTime(TxtFecNac.SelectedDate) > ClUtilitarios.FechaDB()))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "La Fecha de Nacimiento no puede ser mayor a la actual";
                else
                    Mensaje = Mensaje + ", La Fecha de Nacimiento no puede ser mayor a la actual";
                HayError = true;
                HayErrorFecha = true;
            }
            if (!HayErrorFecha == true)
            {
                if (Convert.ToInt32(Convert.ToDateTime(TxtFecNac.SelectedDate).Year) <= ClUtilitarios.FechaDB().Year && !ClUtilitarios.EsMayor(Convert.ToDateTime(TxtFecNac.SelectedDate)))
                {
                    if (Mensaje == "")
                        Mensaje = Mensaje + "Debe ser mayor de edad";
                    else
                        Mensaje = Mensaje + ", Debe ser mayor de edad";
                    HayError = true;
                }
            }
            if ((CboTipoIdentificacion.SelectedValue == "0") || (CboTipoIdentificacion.SelectedValue == ""))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe seleccionar el tipo de identificación";
                else
                    Mensaje = Mensaje + ", Debe seleccionar el tipo de identificación";
                HayError = true;
            }
            if ((CboTipoIdentificacion.SelectedValue == "1") && (ClPersona.Existe_Dpi(TxtDpi.Text.Replace("-",""), 1) == true))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Ya Existe este DPI en nuestros registros";
                else
                    Mensaje = Mensaje + ", Ya Existe este DPI en nuestros registros";
                HayError = true;
            }
            if (TxtFecVenId.DateInput.Text == "")
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe ingresar la fecha de vencimiento de su documento de identificación";
                else
                    Mensaje = Mensaje + ", Debe ingresar la fecha de vencimiento de su documento de identificación";
                HayError = true;
            }
            if ((CboTipoIdentificacion.SelectedValue == "2") && ((CboPais.SelectedValue == "") || (CboPais.SelectedValue == "0")))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe seleccionar su país de origen";
                else
                    Mensaje = Mensaje + ", Debe seleccionar su país de origen";
                HayError = true;
            }
            if ((CboGenero.SelectedValue == "") || (CboGenero.SelectedValue == "0"))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe seleccionar su genero";
                else
                    Mensaje = Mensaje + ", Debe seleccionar su genero";
                HayError = true;
            }
            if ((CboDep.SelectedValue == "") || (CboDep.SelectedValue == "0"))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe seleccionar su departamento de dirección";
                else
                    Mensaje = Mensaje + ", Debe seleccionar su departamento de dirección";
                HayError = true;
            }
            if ((CboMun.SelectedValue == "") || (CboMun.SelectedValue == "0"))
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe seleccionar su municipio de dirección";
                else
                    Mensaje = Mensaje + ", Debe seleccionar su municipio de dirección";
                HayError = true;
            }
            if (ValidaCaptcha() == true)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Ingrese los caracteres";
                else
                    Mensaje = Mensaje + ", Ingrese los caracteres";
                HayError = true;
            }
            if (ChkAcepto.Checked == false)
            {
                if (Mensaje == "")
                    Mensaje = Mensaje + "Debe aceptar las condiciones de privacidad";
                else
                    Mensaje = Mensaje + ", Debe aceptar las condiciones de privacidad";
                HayError = true;
            }
            
            LblMensaje.Text = Mensaje;
            if (HayError == true)
            {
                BtnEror.Visible = true;
                return false;
            }

            else
                return true;
        }

        bool ValidaCaptcha()
        {
            CapValidate.Validate();
            if (!CapValidate.IsValid)
                return true;
            else
                return false;
        }
    }
}