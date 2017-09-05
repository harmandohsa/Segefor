using SEGEFOR.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Enmiendas : System.Web.UI.Page
    {
        Cl_Usuario ClUsuario;
        Cl_Utilitarios ClUtilitarios;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Manejo ClManejo;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClManejo = new Cl_Manejo();

            GrdSolicitudes.NeedDataSource += GrdSolicitudes_NeedDataSource;
            GrdSolicitudes.ItemDataBound += GrdSolicitudes_ItemDataBound;
            GrdSolicitudes.ItemCommand += GrdSolicitudes_ItemCommand;
            BtnYes.Click += BtnYes_Click;
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(9, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
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
                if (Session["TipoUsuarioId"].ToString() == "11")
                    GrdSolicitudes.Columns[10].Visible = true;
            }
        }

        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Session["TipoUsuarioId"].ToString() == "11")
            {
                int GestionId = Convert.ToInt32(TxtGestionId.Text);
                DataSet dsDatosUsuario = ClGestion.GetPersona_Gestion(GestionId);
                string Nombres = dsDatosUsuario.Tables["Datos"].Rows[0]["Nombres"].ToString() + " " + dsDatosUsuario.Tables["Datos"].Rows[0]["Apellidos"].ToString();
                string Correo = dsDatosUsuario.Tables["Datos"].Rows[0]["Correo"].ToString();
                string MensajeCorreo = "Solicitud de enminedas para su licencia forestal";
                DataSet dsDatosEnmeindaJuridica = ClGestion.GetEnmiendasRegional(GestionId);
                MensajeCorreo = (("<body><table><tr><td colspan='2'><b>Enmiendas del Director Regional</b></td></tr><tr><td colspan='2'><b>Informe No. " + dsDatosEnmeindaJuridica.Tables["Datos"].Rows[0]["NoOficio"].ToString() + "</b></td></tr>"));
                for (int i = 0; i < dsDatosEnmeindaJuridica.Tables["DATOS"].Rows.Count; i++)
                {
                    MensajeCorreo = MensajeCorreo + (("<tr><td>" + (i + 1).ToString() + "</td><td>" + dsDatosEnmeindaJuridica.Tables["Datos"].Rows[i]["Enmienda"].ToString() + " </td></tr>"));
                }
                MensajeCorreo = MensajeCorreo + (("</table></body>"));

                dsDatosEnmeindaJuridica.Clear();
                ClUtilitarios.EnvioCorreo(Correo, Nombres, "Solicitud de enminedas para su licencia forestal", MensajeCorreo, 0, "", "");
                dsDatosUsuario.Clear();
                ClGestion.Manda_Gestion_Usuario(GestionId, 1);
                ClGestion.Cambia_Estatus_Gestion(GestionId, 8);
                int AsignacionId =  ClGestion.RetornaPlanManejoEnmienda(GestionId);
                CopiarAnexos(AsignacionId, GestionId);
                Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "");
            }
            

        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        void CopiarAnexos(int AsignacionId, int GestionId)
        {


            string sourceDirectory = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaPendiente\" + GestionId;
            string targetDirectory = Server.MapPath(".") + @"\Archivos\Anexos\MapaPendiente\" + AsignacionId;
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            if (diSource.Exists)
            {
                CopyAll(diSource, diTarget);
            }

            string sourceDirectoryCro = Server.MapPath(".") + @"\Archivos\AnexosPM\Croquis\" + GestionId;
            string targetDirectoryCro = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId;
            DirectoryInfo diSourceCro = new DirectoryInfo(sourceDirectoryCro);
            DirectoryInfo diTargetCro = new DirectoryInfo(targetDirectoryCro);
            if (diSourceCro.Exists)
            {
                CopyAll(diSourceCro, diTargetCro);
            }

            string sourceDirectoryRonda = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaRonda\" + GestionId;
            string targetDirectoryRonda = Server.MapPath(".") + @"\Archivos\Anexos\MapaRonda\" + AsignacionId;
            DirectoryInfo diSourceRonda = new DirectoryInfo(sourceDirectoryRonda);
            DirectoryInfo diTargetRonda = new DirectoryInfo(targetDirectoryRonda);
            if (diSourceRonda.Exists)
            {
                CopyAll(diSourceRonda, diTargetRonda);
            }

            string sourceDirectoryUbi = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUbicacion\" + GestionId;
            string targetDirectoryUbi = Server.MapPath(".") + @"\Archivos\Anexos\MapaUbicacion\" + AsignacionId;
            DirectoryInfo diSourceUbi = new DirectoryInfo(sourceDirectoryUbi);
            DirectoryInfo diTargetUbi = new DirectoryInfo(targetDirectoryUbi);
            if (diSourceUbi.Exists)
            {
                CopyAll(diSourceUbi, diTargetUbi);
            }

            string sourceDirectoryUsoActual = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUsoActual\" +  GestionId ;
            string targetDirectoryUsoActual = Server.MapPath(".") + @"\Archivos\Anexos\MapaUsoActual\" + AsignacionId;
            DirectoryInfo diSourceUsoActual = new DirectoryInfo(sourceDirectoryUsoActual);
            DirectoryInfo diTargetUsoActual = new DirectoryInfo(targetDirectoryUsoActual);
            if (diSourceUsoActual.Exists)
            {
                CopyAll(diSourceUsoActual, diTargetUsoActual);
            }

        }

        void GrdSolicitudes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdSeg")
            {
                TxtGestionId.Text = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GestionId"].ToString()).ToString();
                LblTitConfirmacion.Text = "Se enviara notificación al usuario de las enmiendas, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
                if (Session["TipoUsuarioId"].ToString() == "11")
                    ClUtilitarios.LlenaGrid(ClGestion.Get_Gestiones(7, Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToInt32(Session["UsuarioId"])), GrdSolicitudes);
        }
    }
}