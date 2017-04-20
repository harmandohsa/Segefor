using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;

namespace SEGEFOR
{
    public partial class Wfrm_Login : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        DataSet dsDatos = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();

            BtnIngresar.Click += BtnIngresar_Click;
            BtnOlvio.ServerClick += BtnOlvio_ServerClick;
            BtnEnviaClave.ServerClick += BtnEnviaClave_ServerClick;
            string conver = HttpUtility.UrlEncode(ClUtilitarios.Encrypt("8", true));
        }

        void BtnEnviaClave_ServerClick(object sender, EventArgs e)
        {
            BtnEror.Visible = false;
            if (TxtUsuarioOlv.Text == "")
            {
                LblMensaje.Text = "Debe Ingresar su usuario";
                BtnEror.Visible = true;
            }
            else if (ClUsuario.Existe_Usuario(TxtUsuarioOlv.Text) != true)
            {
                LblMensaje.Text = "Este usuario no esta registrado en el sistema";
                BtnEror.Visible = true;
            }
            else
            {
                string Clave = ClUtilitarios.GenerarPass(6, 10);
                dsDatos = ClUsuario.Datos_Usuario(TxtUsuarioOlv.Text);
                ClUsuario.Actualiza_Clave(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["UsuarioId"]), ClUtilitarios.Encrypt(Clave, true), 1);
                string Nombre = ClPersona.Nombre_Usuario(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]));
                string Asunto = "Notificacion de reseteo de Clave";
                string Mensaje = Mensaje = "<body><table><tr><td>Le informamos que se ha reestablecido su contraseña para poder acceder al Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + dsDatos.Tables["DATOS"].Rows[0]["Usuario"] + ", la contraseña: " + Clave + "</td></tr></table>";
                ClUtilitarios.EnvioCorreo(dsDatos.Tables["DATOS"].Rows[0]["Correo"].ToString(), Nombre, Asunto, Mensaje,0,"","");
                LblMensaje.Text = "Su contraseña fue reestablecida, por favor revise su correo electrónico";
                BtnEror.Visible = true;
            }
        }

        void BtnOlvio_ServerClick(object sender, EventArgs e)
        {
            DivOlvidoClave.Visible = true;
        }

        void BtnIngresar_Click(object sender, EventArgs e)
        {
            BtnEror.Visible = false;
            if (Session["UsuarioId"] == null)
            {
                if (ClUsuario.Existe_Usuario(TxtUsuario.Text) == true)
                {
                    dsDatos = ClUsuario.Datos_Usuario(TxtUsuario.Text);
                    if (Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["Estatus_UsrId"]) == 1)
                    {
                        if (TxtClave.Text == ClUtilitarios.Decrypt(dsDatos.Tables["DATOS"].Rows[0]["Clave"].ToString(), true))
                        {
                            Session["UsuarioId"] = dsDatos.Tables["DATOS"].Rows[0]["UsuarioId"];
                            Session["PersonaId"] = dsDatos.Tables["DATOS"].Rows[0]["PersonaId"];
                            Session["Correo_Usuario"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
                            Session["TipoUsuarioId"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_UsuarioId"];
                            if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Cambio_Pwd"]) == 1)
                            {
                                int CorrIng = ClUsuario.CorrId_IngSistema(Convert.ToInt32(Session["UsuarioId"]));
                                Session["CorrIng"] = CorrIng;
                                ClUsuario.Insertar_Ingreso_Sistema(Convert.ToInt32(Session["UsuarioId"]), CorrIng);
                                Response.Redirect("WebForms/Wfrm_CambioClave.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true) + ""));
                            }
                            else
                            {
                                int CorrIng = ClUsuario.CorrId_IngSistema(Convert.ToInt32(Session["UsuarioId"]));
                                Session["CorrIng"] = CorrIng;
                                ClUsuario.Insertar_Ingreso_Sistema(Convert.ToInt32(Session["UsuarioId"]), CorrIng);
                                if (Convert.ToInt32(Session["TipoUsuarioId"]) == 1)
                                    Response.Redirect("WebForms/Wfrm_SeguimientoUsuario.aspx");
                                else
                                    Response.Redirect("WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "");
                                    //Response.Redirect("WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "");
                            }
                        }
                        else
                        {
                            Session["intentos"] = Convert.ToInt32(Session["intentos"]) + 1;
                            LblMensaje.Text = "Contraseña Incorrecta Intento No. " + Session["intentos"];
                            BtnEror.Visible = true;
                            if (Convert.ToInt32(Session["intentos"]) == 10)
                            {
                                string ClaveNew = ClUtilitarios.GenerarPass(6, 10);
                                ClUsuario.Actualiza_Clave(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["UsuarioId"]), ClUtilitarios.Encrypt(ClaveNew, true), 1);
                                LblMensaje.Text = "Contraseña Incorrecta Intento No. " + Session["intentos"] + ", por seguridad se ha generado una nueva contraseña la cual será enviada su correo electrónico registado";
                                BtnEror.Visible = true;
                                string Asunto = "Notificacion de reseteo de Clave";
                                string Nombre = ClPersona.Nombre_Usuario(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]));
                                string Mensaje = "<body><table><tr><td>Le informamos que se ha reestablecido su contraseña para poder acceder al Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + dsDatos.Tables["DATOS"].Rows[0]["Usuario"] + ", la contraseña: " + ClaveNew + "</td></tr></table>";
                                ClUtilitarios.EnvioCorreo(dsDatos.Tables["DATOS"].Rows[0]["Correo"].ToString(), Nombre, Asunto, Mensaje,0,"","");
                            }
                        }

                    }
                    else
                    {
                        LblMensaje.Text = "Su usuario se encunetra desactivado";
                        BtnEror.Visible = true;
                    }
                }
                else
                {
                    LblMensaje.Text = "Usuario No Existe";
                    BtnEror.Visible = true;
                }
            }
            else
            {
                string UsuarioLog = ClUsuario.Usuario_Get_Login(Convert.ToInt32(Session["UsuarioId"]));
                if (UsuarioLog == TxtUsuario.Text)
                {
                    if (ClUsuario.Existe_Usuario(TxtUsuario.Text) == true)
                    {
                        dsDatos = ClUsuario.Datos_Usuario(TxtUsuario.Text);
                        if (Convert.ToInt64(dsDatos.Tables["DATOS"].Rows[0]["Estatus_UsrId"]) == 1)
                        {
                            if (TxtClave.Text == ClUtilitarios.Decrypt(dsDatos.Tables["DATOS"].Rows[0]["Clave"].ToString(), true))
                            {
                                Session["UsuarioId"] = dsDatos.Tables["DATOS"].Rows[0]["UsuarioId"];
                                Session["PersonaId"] = dsDatos.Tables["DATOS"].Rows[0]["PersonaId"];
                                Session["Correo_Usuario"] = dsDatos.Tables["DATOS"].Rows[0]["Correo"];
                                Session["TipoUsuarioId"] = dsDatos.Tables["DATOS"].Rows[0]["Tipo_UsuarioId"];
                                if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Cambio_Pwd"]) == 1)
                                {
                                    int CorrIng = ClUsuario.CorrId_IngSistema(Convert.ToInt32(Session["UsuarioId"]));
                                    Session["CorrIng"] = CorrIng;
                                    ClUsuario.Insertar_Ingreso_Sistema(Convert.ToInt32(Session["UsuarioId"]), CorrIng);
                                    Response.Redirect("WebForms/Wfrm_CambioClave.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true) + ""));
                                }
                                else
                                {
                                    int CorrIng = ClUsuario.CorrId_IngSistema(Convert.ToInt32(Session["UsuarioId"]));
                                    Session["CorrIng"] = CorrIng;
                                    ClUsuario.Insertar_Ingreso_Sistema(Convert.ToInt32(Session["UsuarioId"]), CorrIng);
                                    Response.Redirect("WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "");
                                }
                            }
                            else
                            {
                                Session["intentos"] = Convert.ToInt32(Session["intentos"]) + 1;
                                LblMensaje.Text = "Contraseña Incorrecta Intento No. " + Session["intentos"];
                                BtnEror.Visible = true;
                                if (Convert.ToInt32(Session["intentos"]) == 10)
                                {
                                    string ClaveNew = ClUtilitarios.GenerarPass(6, 10);
                                    ClUsuario.Actualiza_Clave(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["UsuarioId"]), ClUtilitarios.Encrypt(ClaveNew, true), 1);
                                    LblMensaje.Text = "Contraseña Incorrecta Intento No. " + Session["intentos"] + ", por seguridad se ha generado una nueva contraseña la cual será enviada su correo electrónico registado";
                                    BtnEror.Visible = true;
                                    string Asunto = "Notificacion de reseteo de Clave";
                                    string Nombre = ClPersona.Nombre_Usuario(Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["PersonaId"]));
                                    string Mensaje = "<body><table><tr><td>Le informamos que se ha reestablecido su contraseña para poder acceder al Sistema Electrónico de Gestión Forestal -SEGEFOR- su usuario es: " + dsDatos.Tables["DATOS"].Rows[0]["Usuario"] + ", la contraseña: " + ClaveNew + "</td></tr></table>";
                                    ClUtilitarios.EnvioCorreo(dsDatos.Tables["DATOS"].Rows[0]["Correo"].ToString(),Nombre, Asunto, Mensaje,0,"","");
                                }
                            }

                        }
                        else
                        {
                            LblMensaje.Text = "Su usuario se encunetra desactivado";
                            BtnEror.Visible = true;
                        }
                    }
                    else
                    {
                        LblMensaje.Text = "Usuario No Existe";
                        BtnEror.Visible = true;
                    }
                }
                else
                {
                    LblMensaje.Text = "Ya hay una sesión abierta del sistema SEGEFOR por favor cierre  la sesión abierta o cierre el navegador y vuelva a internarlo";
                    BtnEror.Visible = true;
                }
            }
        }
    }
}