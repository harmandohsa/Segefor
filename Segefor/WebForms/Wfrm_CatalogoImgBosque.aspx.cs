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
    public partial class Wfrm_CatalogoImgBosque : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Registro ClRegistro;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClRegistro = new Cl_Registro();

            BtnSubirFoto.Click += BtnSubirFoto_Click;
            BtnUploadMapaVocacion.Click += BtnUploadMapaVocacion_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(52, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 52);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    BtnSubirFoto.Visible = false;
                    BtnUploadMapaVocacion.Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnSubirFoto.Visible = false;
                    BtnUploadMapaVocacion.Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Eliminar"]) == 0)
                {
                    BtnSubirFoto.Visible = false;
                    BtnUploadMapaVocacion.Visible = false;
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
                CargarImagenes();
            }

        }

        void BtnUploadMapaVocacion_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (UploadedFile f in UploadMapaVocacion.UploadedFiles)
                {
                    string PathArchivo = Server.MapPath(".") + @"\FilesMap\Vocacion";
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivNoErrMapaVocacion.Visible = true;
                    LblMensajeNoErrMapaVocacion.Text = "Mapa cargado con exito";
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
            }
            CargarImagenes();
        }

        void BtnSubirFoto_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (UploadedFile f in UploadMapaBosque.UploadedFiles)
                {
                    string PathArchivo = Server.MapPath(".") + @"\FilesMap\BosquesNaturales";
                    if (Directory.Exists(PathArchivo))
                    {
                        DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                        FileInfo[] files = directory.GetFiles("*.*");
                        DirectoryInfo[] directories = directory.GetDirectories();
                        for (int i = 0; i < files.Length; i++)
                        {
                            ((FileInfo)files[i]).Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(PathArchivo);
                    f.SaveAs(PathArchivo + @"\" + f.FileName);
                    DivNoErrMapaBosque.Visible = true;
                    LblMensajeNoErrMapaBosque.Text = "Mapa cargado con exito";
                }
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
            }
            CargarImagenes();
        }

        void CargarImagenes()
        {
            string PathArchivo = Server.MapPath(".") + @"\FilesMap\Vocacion";
            string FileNameVocacion = "";
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    FileNameVocacion = ((FileInfo)files[i]).Name;
                }
                PathArchivo = PathArchivo + @"\" + FileNameVocacion; 
            }
            ImgMapaVoca.ImageUrl = "~/WebForms/FilesMap/Vocacion/" + FileNameVocacion ;


            string PathArchivoBosque = Server.MapPath(".") + @"\FilesMap\BosquesNaturales";
            string FileNameBosque = "";
            if (Directory.Exists(PathArchivoBosque))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivoBosque);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    FileNameBosque = ((FileInfo)files[i]).Name;
                }
            }
            ImgMapaBosque.ImageUrl = "~/WebForms/FilesMap/BosquesNaturales/" + FileNameBosque;
            
               
        }
    }
}