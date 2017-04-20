using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Globalization;
using System.Web;

namespace SEGEFOR.Clases
{
    public class Cl_Utilitarios
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();
        Cl_Usuario ClUsuario;

        public void LlenaCombo(DataSet ds, Telerik.Web.UI.RadComboBox Combo, string Llave, string Descripcion)
        {
            Combo.DataSource = ds;
            Combo.DataMember = "DATOS";
            Combo.DataTextField = Descripcion;
            Combo.DataValueField = Llave;
            Combo.DataBind();
            ds.Tables.Remove("DATOS");
        }

        public void AgregarSeleccioneCombo(Telerik.Web.UI.RadComboBox Combo, string Descripcion)
        {
            string Texto = "Seleccione " + Descripcion;
            Combo.Items.Insert(0,Texto);
        }

        public bool email_bien_escrito(string strMailAddress)
        {
            return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public DateTime FechaDB()
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_FechaDb", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Resul", OleDbType.Date).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDateTime(cmd.Parameters["@Resul"].Value);

            }
            catch (Exception ex)
            {
                cn.Close();
                return Convert.ToDateTime("01/01/2000");
            }
        }

        public bool EsMayor(DateTime FecNac)
        {
            DateTime time = new DateTime(1, 1, 1);
            DateTime time2 = new DateTime(Convert.ToInt32(FecNac.Year), Convert.ToInt32(FecNac.Month), Convert.ToInt32(FecNac.Day));
            DateTime time3 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan span = (TimeSpan)(time3 - time2);
            DateTime time4 = time + span;
            int num = time4.Year - 1;
            return (num >= 0x12);
        }

        public string GenerarPass(int LongPassMin, int LongPassMax)
        {
            char[] ValueAfanumeric = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '!', '#', '$', '%', '&', '?', '¿' };
            Random ram = new Random();
            int LogitudPass = ram.Next(LongPassMin, LongPassMax);
            string Password = String.Empty;

            for (int i = 0; i < LogitudPass; i++)
            {
                int rm = ram.Next(0, 2);

                if (rm == 0)
                {
                    Password += ram.Next(0, 10);
                }
                else
                {
                    Password += ValueAfanumeric[ram.Next(0, 59)];
                }
            }

            return Password;

        }

        public void EnvioCorreo(string Mail, string Nombre, string Asunto, string Mensaje, int ConAdjunto, string RutaAdjunto, string NombreArchivo)
        {
            try
            {
                string Sitio = System.Configuration.ConfigurationManager.AppSettings["Sitio"].ToString();
                System.Net.Mail.MailMessage Correo = new System.Net.Mail.MailMessage();
                Correo.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["Cuenta"], "INAB Administrador");
                Correo.To.Add(Mail);
                //Correo.CC.Add("hhernandez@query.com.gt");
                Correo.Subject = Asunto;
                string Saludo = "<table><tr><td>Estimado(a): " + Nombre + "</td></tr></table>";
                string Notificacion = "<table><tr><td><b>NOTIFICACIÓ ELECTRÓNICA, DEL ADMINISTRADOR DEL SISTEMA:</b></td></tr></table>";
                Mensaje = Notificacion + Saludo + Mensaje + "<table><tr><td>Ingrese al sistema por medio del siguiente enlace: " + Sitio + " para revisar la información</td></tr><tr><td></td></tr><tr><td><b>Instituto Nacional de Bosques</b></td></tr><tr><td><b>Más bosques, más vida…</b></td></tr><tr><td><b>Tel: 23212626</b></td></tr><tr><td></td></tr><tr><td> <font color=#FF0000>Por favor no responda este correo.</font></td></tr></table>";
                Mensaje = Mensaje + "<table><tr><td></td></tr><tr><td>Este correo electrónico fue enviado a: " + Nombre + ", a través del Sistema Electrónico de Gestión Forestal -SEGEFOR- del Instituto Nacional de Bosques –INAB–.</td></tr></table></body>";
                AlternateView HTMLConImagenes = default(AlternateView);
                HTMLConImagenes = AlternateView.CreateAlternateViewFromString(Mensaje, null, "text/html");
                Correo.AlternateViews.Add(HTMLConImagenes);
                Correo.IsBodyHtml = true;
                if (ConAdjunto == 1)
                {
                    Attachment File  = new Attachment(RutaAdjunto);
                    File.Name = NombreArchivo;
                    Correo.Attachments.Add(File);
                }
                Correo.Priority = System.Net.Mail.MailPriority.High;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["Host"].ToString(), Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Puerto"]));
                //smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Cuenta"], System.Configuration.ConfigurationManager.AppSettings["Clave"]);

                smtp.Send(Correo);
            }
            catch (Exception ex)
            {
                string Err = ex.Message;
            }

        }

        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "INAB";
            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                                 typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            string Encriptado =   Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return Encriptado.Replace("+", "!");
        }

        public string Decrypt(string cipherString, bool useHashing)
        {
            cipherString = cipherString.Replace("!", "+");
            byte[] keyArray;

            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "INAB";
            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                             typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT

            return UTF8Encoding.UTF8.GetString(resultArray);
        }



        public bool TieneEspecial(string clave)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(clave, @"^(?=.*[¡,@,#,$,%,^,&,*,(,),_,-,+,=,{,},\,|,:,;,<,>,.,?,/]).{8,20}$");
        }

        public bool TieneGuion(string texto)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(texto, @"[0-9]-[0123456789k]{1}$");
        }

        public bool TieneNumero(string clave)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(clave, @"^(?=.*\d).{8,20}$");
        }

        public bool TieneMayus(string clave)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(clave, @"^(?=.*[A-Z]).{8,20}$");
        }

        public bool TieneMinus(string clave)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(clave, @"^(?=.*[a-z]).{8,20}$");
        }

        public void Permisos(MasterPage MasPag, int UsuarioId)
        {
            ClUsuario = new Cl_Usuario();
            ds = ClUsuario.Permisos_Usuario(UsuarioId);

            for (int i = 0; i <= ds.Tables["DATOS"].Rows.Count - 1; i++)
            {
                switch (Convert.ToInt32(ds.Tables["DATOS"].Rows[i]["FormaId"]))
                {
                    case 1: //Gestiones
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGest;
                        MnuGest = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGest");
                        MnuGest.Visible = true;
                        break;
                    case 2: //Notificaciones
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuNot;
                        MnuNot = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuNot");
                        MnuNot.Visible = true;
                        break;
                    case 3: //Consultas
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuConsulta;
                        MnuConsulta = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmin");
                        MnuConsulta.Visible = true;
                        break;
                    case 4: //Reportes
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuRep;
                        MnuRep = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuRep");
                        MnuRep.Visible = true;
                        break;
                    case 5: //Adm. Plan de Manejo
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmPlanManejo;
                        MnuAdmPlanManejo = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmPlanManejo");
                        MnuAdmPlanManejo.Visible = true;
                        break;
                    case 6: //Ayuda
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAyda;
                        MnuAyda = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAyda");
                        MnuAyda.Visible = true;
                        break;
                    case 7: //Administrar
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmin;
                        MnuAdmin = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmin");
                        MnuAdmin.Visible = true;
                        break;
                    case 8: //Gestiones Nuevas
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGestNeuva;
                        MnuGestNeuva = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGestNeuva");
                        MnuGestNeuva.Visible = true;
                        break;
                    case 9: //Enmiendas
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGestEnmiendas;
                        MnuGestEnmiendas = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGestEnmiendas");
                        MnuGestEnmiendas.Visible = true;
                        break;
                    case 10: //Modificaciones
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGestMod;
                        MnuGestMod = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGestMod");
                        MnuGestMod.Visible = true;
                        break;
                    case 11: //Gestiones en curso (historial)
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGestCurso;
                        MnuGestCurso = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGestCurso");
                        MnuGestCurso.Visible = true;
                        break;
                    case 12: //Emisión de notas de bosque
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmPlanEmisionNotas;
                        MnuAdmPlanEmisionNotas = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmPlanEmisionNotas");
                        MnuAdmPlanEmisionNotas.Visible = true;
                        break;
                    case 13: //Informe de avances
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmPlanInformeAv;
                        MnuAdmPlanInformeAv = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmPlanInformeAv");
                        MnuAdmPlanInformeAv.Visible = true;
                        break;
                    case 14: //Perfil de Usuario
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmPerfil;
                        MnuAdmPerfil = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmPerfil");
                        MnuAdmPerfil.Visible = true;
                        break;
                    case 15: //Contraseña
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmClave;
                        MnuAdmClave = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmClave");
                        MnuAdmClave.Visible = true;
                        break;
                    case 16: //Inmuebles
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdmInmu;
                        MnuAdmInmu = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdmInmu");
                        MnuAdmInmu.Visible = true;
                        break;
                    case 18: //Administración Aprovechamiento Forestal
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminAproFores;
                        MnuAdminAproFores = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminAproFores");
                        MnuAdminAproFores.Visible = true;
                        break;
                    case 19: //Administración Incentivos Forestales
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminIncentivos;
                        MnuAdminIncentivos = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminIncentivos");
                        MnuAdminIncentivos.Visible = true;
                        break;
                    case 20: //Administración Registro Nacional Forestal
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminRnf;
                        MnuAdminRnf = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminRnf");
                        MnuAdminRnf.Visible = true;
                        break;
                    case 21: //Catalogos Registro Nacional Forestal
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuCatalogoRnf;
                        MnuCatalogoRnf = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuCatalogoRnf");
                        MnuCatalogoRnf.Visible = true;
                        break;
                    case 22: //Catalogos Registro Nacional Forestal.Profesiones
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuCatalogoRnfProfesion;
                        MnuCatalogoRnfProfesion = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuCatalogoRnfProfesion");
                        MnuCatalogoRnfProfesion.Visible = true;
                        break;
                    case 24: //Catalogos Registro Nacional Forestal.Profesiones x actividad profesionale
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuCatalogoRnfProfesionActividadProfesional;
                        MnuCatalogoRnfProfesionActividadProfesional = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuCatalogoRnfProfesionActividadProfesional");
                        MnuCatalogoRnfProfesionActividadProfesional.Visible = true;
                        break;
                    case 25: //Administracion General
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminGen;
                        MnuAdminGen = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminGen");
                        MnuAdminGen.Visible = true;
                        break;
                    case 26: //Catalogos Administración General
                        System.Web.UI.HtmlControls.HtmlGenericControl ManCatalogoAdminGen;
                        ManCatalogoAdminGen = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("ManCatalogoAdminGen");
                        ManCatalogoAdminGen.Visible = true;
                        break;
                    case 27: //Catalogos Administración General -> Perfiles
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuCatAdminGenPerfil;
                        MnuCatAdminGenPerfil = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuCatAdminGenPerfil");
                        MnuCatAdminGenPerfil.Visible = true;
                        break;
                    case 28: //Catalogos Administración General -> Usuarios
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuUsuarios;
                        MnuUsuarios = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuUsuarios");
                        MnuUsuarios.Visible = true;
                        break;
                    case 29: //Catalogos Administración General -> Permisos
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuPermisosAdmin;
                        MnuPermisosAdmin = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuPermisosAdmin");
                        MnuPermisosAdmin.Visible = true;
                        break;
                    case 33: //Notificaciones de Juridico
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuNotJuridico;
                        MnuNotJuridico = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuNotJuridico");
                        MnuNotJuridico.Visible = true;
                        break;
                    case 36: //Gestiones -> Historial
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuGestHistorial;
                        MnuGestHistorial = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuGestHistorial");
                        MnuGestHistorial.Visible = true;
                        break;
                    case 46: //Gestiones -> Plan de Manejo
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuPlanManejo;
                        MnuPlanManejo = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuPlanManejo");
                        MnuPlanManejo.Visible = true;
                        break;
                    case 51: //Catalogos Administración General -> RegionesSubregiones
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminRegionesSubRegiones;
                        MnuAdminRegionesSubRegiones = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminRegionesSubRegiones");
                        MnuAdminRegionesSubRegiones.Visible = true;
                        break;
                    case 52: //Catalogos Registro Nacional Forestal. Imagenes bosque
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuImagenesBosquesNaturales;
                        MnuImagenesBosquesNaturales = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuImagenesBosquesNaturales");
                        MnuImagenesBosquesNaturales.Visible = true;
                        break;
                    case 53: //Catalogos Admin General.Espese
                        System.Web.UI.HtmlControls.HtmlGenericControl MnuAdminEspecies;
                        MnuAdminEspecies = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuAdminEspecies");
                        MnuAdminEspecies.Visible = true;
                        break;
                }
            }
            OverridePermisos(MasPag, 1);

        }
        
        //Override Permisos
        public void OverridePermisos(MasterPage MasPag, int Tipo)
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();
            DataSet TieneSolicitudes = ClManejo.Get_PlanesManejo(Tipo, Convert.ToInt32(HttpContext.Current.Session["PersonaId"]), Convert.ToInt32(HttpContext.Current.Session["UsuarioId"]));
            if (TieneSolicitudes.Tables["Datos"].Rows.Count == 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl MnuPlanManejoDos;
                MnuPlanManejoDos = (System.Web.UI.HtmlControls.HtmlGenericControl)MasPag.FindControl("MnuPlanManejo");
                MnuPlanManejoDos.Visible = false;
            }
            TieneSolicitudes.Clear();
        }

        public void LlenaGrid(DataSet ds, Telerik.Web.UI.RadGrid Grid)
        {
            Grid.Culture = new CultureInfo("es-PE");
            Grid.DataSource = ds;
        }

        public void LlenaGridDt(DataSet ds, Telerik.Web.UI.RadGrid Grid, string Table)
        {
            Grid.Culture = new CultureInfo("es-PE");
            Grid.DataSource = ds.Tables[Table];
        }

        public void LlenaGridDataView(DataView dv, Telerik.Web.UI.RadGrid Grid, string Table)
        {
            Grid.Culture = new CultureInfo("es-PE");
            Grid.DataSource = dv;
        }

        public void LlenaRadTree(DataSet ds, Telerik.Web.UI.RadTreeList TreeList)
        {
            TreeList.Culture = new CultureInfo("es-PE");
            TreeList.DataSource = ds;
        }

        int Days(DateTime start, DateTime end)
        {
            TimeSpan span = start.Subtract(end);
            return (int)span.TotalDays;
        }

        public bool EsMayorDays(int Dias, DateTime Fecha)
        {
            DateTime fecha1 = Convert.ToDateTime(DateTime.Now);
            DateTime fecha2 = Convert.ToDateTime(Fecha);
            int CntDays = Days(fecha1, fecha2);
            if (CntDays > 120)
                return true;
            else
                return false;
        }

        public object IIf(bool Expression, object TruePart, object FalsePart)
        {
            object ReturnValue = Expression == true ? TruePart : FalsePart;
            return ReturnValue;
        }

        public void LlenaGridDataSet(DataSet Ds, Telerik.Web.UI.RadGrid Grid, string Tabla)
        {
            //Grid.Culture = new CultureInfo("es-PE");
            Grid.DataSource = Ds.Tables[Tabla];
        }

        public bool EsInstitucional(string Correo)
        {
            string searchWithinThis = Correo;
            string searchForThis = "inab.gob.gt";
            int firstCharacter = searchWithinThis.IndexOf(searchForThis);
            if (firstCharacter > 0)
                return true;
            else
                return false;
        }

        public string DevuelveRomano(int Numero)
        {
            switch (Numero)
            {
                case 1:
                    return "I.";
                case 2:
                    return "II.";
                case 3:
                    return "III.";
                case 4:
                    return "IV.";
                default:
                    return "Err";
            }
        }


       public string enletras(string num)
       {
           string res, dec = "";
           Int64 entero;
           int decimales;
           double nro;

           try

           {
               nro = Convert.ToDouble(num);
           }
           catch
           {
               return "";
           }

           entero = Convert.ToInt64(Math.Truncate(nro));
           decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
           if (decimales > 0)
           {
               dec = " CON " + decimales.ToString() + "/100";
           }

           res = toText(Convert.ToDouble(entero)) + dec;
           return res;
       }

       private string toText(double value)
       {
           string Num2Text = "";
           value = Math.Truncate(value);
           if (value == 0) Num2Text = "CERO";
           else if (value == 1) Num2Text = "UNO";
           else if (value == 2) Num2Text = "DOS";
           else if (value == 3) Num2Text = "TRES";
           else if (value == 4) Num2Text = "CUATRO";
           else if (value == 5) Num2Text = "CINCO";
           else if (value == 6) Num2Text = "SEIS";
           else if (value == 7) Num2Text = "SIETE";
           else if (value == 8) Num2Text = "OCHO";
           else if (value == 9) Num2Text = "NUEVE";
           else if (value == 10) Num2Text = "DIEZ";
           else if (value == 11) Num2Text = "ONCE";
           else if (value == 12) Num2Text = "DOCE";
           else if (value == 13) Num2Text = "TRECE";
           else if (value == 14) Num2Text = "CATORCE";
           else if (value == 15) Num2Text = "QUINCE";
           else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
           else if (value == 20) Num2Text = "VEINTE";
           else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
           else if (value == 30) Num2Text = "TREINTA";
           else if (value == 40) Num2Text = "CUARENTA";
           else if (value == 50) Num2Text = "CINCUENTA";
           else if (value == 60) Num2Text = "SESENTA";
           else if (value == 70) Num2Text = "SETENTA";
           else if (value == 80) Num2Text = "OCHENTA";
           else if (value == 90) Num2Text = "NOVENTA";
           else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
           else if (value == 100) Num2Text = "CIEN";
           else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
           else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
           else if (value == 500) Num2Text = "QUINIENTOS";
           else if (value == 700) Num2Text = "SETECIENTOS";
           else if (value == 900) Num2Text = "NOVECIENTOS";
           else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
           else if (value == 1000) Num2Text = "MIL";
           else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
           else if (value < 1000000)
           {
               Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
               if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
           }

           else if (value == 1000000) Num2Text = "UN MILLON";
           else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
           else if (value < 1000000000000)
           {
               Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
               if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
           }

           else if (value == 1000000000000) Num2Text = "UN BILLON";
           else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

           else
           {
               Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
               if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
           }
           return Num2Text;

       }

      


    }
}