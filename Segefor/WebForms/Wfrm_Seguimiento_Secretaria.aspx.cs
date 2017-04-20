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
    public partial class Wfrm_Seguimiento_Secretaria : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Gestion_Registro ClGestion;
        Cl_Xml ClXml;
        Cl_Manejo ClManejo;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClGestion = new Cl_Gestion_Registro();
            ClXml = new Cl_Xml();
            ClManejo = new Cl_Manejo();

            BtnProcesar.Click += BtnProcesar_Click;
            ImgVerinfo.Click += ImgVerinfo_Click;
            BtnYes.Click += BtnYes_Click;
            BtnEnviaGestion.Click += BtnEnviaGestion_Click;
            IngVerAnexos.Click += IngVerAnexos_Click;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(30, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 30);
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
                LblNug.Text = "Gestión No.: " +  ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gun"].ToString()), true);
                LblSolicitante.Text = "Solicitante: " +  ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                int CategoriaId = 0;
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    CategoriaId = ClGestion.Get_CategoriaRNFId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["souscategorie"].ToString()), true)));
                    if (CategoriaId == 7)
                        SetIdentificacionRequisitos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["profession"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["categorieprofessionnelle"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["originepersonne"].ToString()), true)),CategoriaId);
                    else if ((CategoriaId == 2) || (CategoriaId == 3) || (CategoriaId == 4) || (CategoriaId == 6) || (CategoriaId == 5) || (CategoriaId == 8) || (CategoriaId == 9) || (CategoriaId == 1))
                       SetIdentificacionRequisitos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true)), 0, 0, 0,CategoriaId);
                    if ((CategoriaId == 2) || (CategoriaId == 3) || (CategoriaId == 4) || (CategoriaId == 6))
                    {
                        int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                        if ((ClGestion.Tiene_Anexos_Inventerio(GestionId) == 0) && (ClGestion.Tiene_Anexos_Poligono(GestionId) == 0))
                        {
                            IngVerAnexos.Visible = false;
                            LblAnexos.Visible = false;
                        }
                    }
                    else
                    {
                        IngVerAnexos.Visible = false;
                        LblAnexos.Visible = false;
                    }
                }
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                {
                    SetIdentificacionRequisitos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true)), 0, 0, 0, CategoriaId);
                }
                
                
                    
            }

        }

        void IngVerAnexos_Click(object sender, ImageClickEventArgs e)
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
        

        void BtnEnviaGestion_Click(object sender, EventArgs e)
        {
            if (ClGestion.Existe_Factura_Gestion(TxtSerie.Text, Convert.ToInt32(Txtfactura.Text)) == true)
            {
                DivError.Visible = true;
                LblError.Text = "Ya existe es número de factura";
                
            }
            else
            {
                LblTitConfirmacion.Text = "La Gestíon será enviada el siguiente paso, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            
        }


        void BtnYes_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["Valida"]) == true)
            {
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                    {
                        string Mensaje = "";
                        string Doc_Presentados = "";
                        int NumeroDoc_Presntados = 1;
                        string No_Expediente = "";

                        if(ChkTitulo.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkTitulo.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkTitulo.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkColegiado.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkColegiado.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkColegiado.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkRtu.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkRtu.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkRtu.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkId.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkId.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkId.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkPosgrado.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkPosgrado.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkPosgrado.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDiploma.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDiploma.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDiploma.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                        int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                        No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                        string NombreCategoria = ClGestion.NombreCategoriaRNF(SubCategoriaId);
                        Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción en el Registro Nacional Forestal (RNF) en la categoría de “ " +  NombreCategoria + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                        if (SubCategoriaId == 1)
                            No_Expediente = No_Expediente + "-3.7.3";

                        else if (SubCategoriaId == 2)
                            No_Expediente = No_Expediente + "-3.7.2";
                        else if (SubCategoriaId == 3)
                            No_Expediente = No_Expediente + "-3.7.1";
                        else if (SubCategoriaId == 16)
                            No_Expediente = No_Expediente + "-3.7.4";
                        No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                        ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]),Correlativo_Anual_SubRegion);
                        ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                        ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                        DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                        ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                        dsDatosSubRegional.Clear();
                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21) || (SubCategoriaId == 18) || (SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43) || (SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33) || (SubCategoriaId == 14))
                    {
                        string Mensaje = "";
                        string Doc_Presentados = "";
                        int NumeroDoc_Presntados = 1;
                        string No_Expediente = "";

                        if (ChkCertificacionPV.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCertificacionPV.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCertificacionPV.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkIdNoRepresentantePV.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkIdNoRepresentantePV.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkIdNoRepresentantePV.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkIdSiRepresentantePV.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkIdSiRepresentantePV.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkIdSiRepresentantePV.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ÇhkCopiaNombramientoPV.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ÇhkCopiaNombramientoPV.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ÇhkCopiaNombramientoPV.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkPoliginoPV.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkPoliginoPV.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkPoliginoPV.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                        int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                        No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                        string NombreCategoria = ClGestion.NombreCategoriaRNF(SubCategoriaId);
                        Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción en el Registro Nacional Forestal (RNF) en la categoría de “" + NombreCategoria + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                        if (SubCategoriaId == 4)
                            No_Expediente = No_Expediente + "-3.2.2";
                        else if (SubCategoriaId == 5)
                            No_Expediente = No_Expediente + "-3.2.1.1";
                        else if (SubCategoriaId == 19)
                            No_Expediente = No_Expediente + "-3.2.1.2";
                        else if (SubCategoriaId == 20)
                            No_Expediente = No_Expediente + "-3.2.1.3";
                        else if (SubCategoriaId == 21)
                            No_Expediente = No_Expediente + "-3.2.1.4";
                        else if (SubCategoriaId == 18)
                            No_Expediente = No_Expediente + "-3.3";
                        else if (SubCategoriaId == 34)
                            No_Expediente = No_Expediente + "-3.4.3";
                        else if (SubCategoriaId == 35)
                            No_Expediente = No_Expediente + "-3.4.4";
                        else if (SubCategoriaId == 36)
                            No_Expediente = No_Expediente + "-3.4.1.1";
                        else if (SubCategoriaId == 37)
                            No_Expediente = No_Expediente + "-3.4.1.2";
                        else if (SubCategoriaId == 38)
                            No_Expediente = No_Expediente + "-3.4.1.3";
                        else if (SubCategoriaId == 39)
                            No_Expediente = No_Expediente + "-3.4.1.4";
                        else if (SubCategoriaId == 40)
                            No_Expediente = No_Expediente + "-3.4.2.1";
                        else if (SubCategoriaId == 41)
                            No_Expediente = No_Expediente + "-3.4.2.2";
                        else if (SubCategoriaId == 42)
                            No_Expediente = No_Expediente + "-3.4.2.3";
                        else if (SubCategoriaId == 43)
                            No_Expediente = No_Expediente + "-3.4.1.5";
                        else if (SubCategoriaId == 13)
                            No_Expediente = No_Expediente + "-3.6.1.2";
                        else if (SubCategoriaId == 29)
                            No_Expediente = No_Expediente + "-3.6.1.1";
                        else if (SubCategoriaId == 30)
                            No_Expediente = No_Expediente + "-3.6.2";
                        else if (SubCategoriaId == 31)
                            No_Expediente = No_Expediente + "-3.6.3.1";
                        else if (SubCategoriaId == 32)
                            No_Expediente = No_Expediente + "-3.6.3.2";
                        else if (SubCategoriaId == 33)
                            No_Expediente = No_Expediente + "-3.6.4.1";
                        else if (SubCategoriaId == 14)
                            No_Expediente = No_Expediente + "-3.1.3.1";

                        No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                        ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]), Correlativo_Anual_SubRegion);
                        ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                        ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                        DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                        ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                        dsDatosSubRegional.Clear();
                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24))
                    {
                        string Mensaje = "";
                        string Doc_Presentados = "";
                        int NumeroDoc_Presntados = 1;
                        string No_Expediente = "";

                        if (ChkPatente.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkPatente.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkPatente.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkRtuEmpresa.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkRtuEmpresa.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkRtuEmpresa.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocPropietario.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropietario.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropietario.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocRepresentante.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocRepresentante.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocRepresentante.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkNomRepresentante.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkNomRepresentante.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkNomRepresentante.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                        int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                        No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                        string NombreCategoria = ClGestion.NombreCategoriaRNF(SubCategoriaId);
                        Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción en el Registro Nacional Forestal (RNF) en la categoría de “" + NombreCategoria + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                        if (SubCategoriaId == 7)
                            No_Expediente = No_Expediente + "-3.5.2";
                        else if (SubCategoriaId == 9)
                            No_Expediente = No_Expediente + "-3.5.5";
                        else if (SubCategoriaId == 10)
                        {
                            int Det_SubCategoria = ClGestion.Get_Det_RegistroRnfId(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                            if (Det_SubCategoria == 1)
                                No_Expediente = No_Expediente + "-3.5.1.1";
                            else if (Det_SubCategoria == 2)
                                No_Expediente = No_Expediente + "-3.5.1.2";
                            else if (Det_SubCategoria == 3)
                                No_Expediente = No_Expediente + "-3.5.1.3";
                            else if (Det_SubCategoria == 4)
                                No_Expediente = No_Expediente + "-3.5.1.4";
                            else if (Det_SubCategoria == 5)
                                No_Expediente = No_Expediente + "-3.5.1.5";
                            else if (Det_SubCategoria == 6)
                                No_Expediente = No_Expediente + "-3.5.1.6";
                        }
                        else if (SubCategoriaId == 12)
                            No_Expediente = No_Expediente + "-3.5.4";
                        else if (SubCategoriaId == 17)
                            No_Expediente = No_Expediente + "-3.5.3";
                        else if (SubCategoriaId == 22)
                            No_Expediente = No_Expediente + "-3.5.6";
                        else if (SubCategoriaId == 23)
                            No_Expediente = No_Expediente + "-3.5.7";
                        else if (SubCategoriaId == 24)
                            No_Expediente = No_Expediente + "-3.5.8";
                        No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                        ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]), Correlativo_Anual_SubRegion);
                        ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                        ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                        DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                        ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                        dsDatosSubRegional.Clear();
                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28))
                    {
                        string Mensaje = "";
                        string Doc_Presentados = "";
                        int NumeroDoc_Presntados = 1;
                        string No_Expediente = "";

                        if (ChkDocConstitucion.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocConstitucion.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocConstitucion.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkCarneSat.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCarneSat.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCarneSat.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkRtuEntidad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkRtuEntidad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkRtuEntidad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocPropietarioEntidad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropietarioEntidad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropietarioEntidad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocRepresentanteEntidad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocRepresentanteEntidad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocRepresentanteEntidad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkNomRepresentanteEntidad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkNomRepresentanteEntidad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkNomRepresentanteEntidad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkCopiaActa.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCopiaActa.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCopiaActa.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                        int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                        No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                        string NombreCategoria = ClGestion.NombreCategoriaRNF(SubCategoriaId);
                        Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción en el Registro Nacional Forestal (RNF) en la categoría de “" + NombreCategoria + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                        if (SubCategoriaId == 25)
                            No_Expediente = No_Expediente + "-3.8.1";
                        else if (SubCategoriaId == 26)
                            No_Expediente = No_Expediente + "-3.8.2";
                        else if (SubCategoriaId == 27)
                            No_Expediente = No_Expediente + "-3.8.3";
                        else if (SubCategoriaId == 28)
                            No_Expediente = No_Expediente + "-3.8.4";
                        No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                        ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]), Correlativo_Anual_SubRegion);
                        ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                        ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                        DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                        ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                        dsDatosSubRegional.Clear();
                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 11) || (SubCategoriaId == 15))
                    {
                        string Mensaje = "";
                        string Doc_Presentados = "";
                        int NumeroDoc_Presntados = 1;
                        string No_Expediente = "";

                        if (ChkPatenteMoto.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkPatenteMoto.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkPatenteMoto.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkRtuMoto.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkRtuMoto.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkRtuMoto.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocPropiedad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropiedad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropiedad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkCopiaPropiedad.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCopiaPropiedad.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCopiaPropiedad.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocPropietarioMoto.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropietarioMoto.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropietarioMoto.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkDocRepresentanteMoto.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocRepresentanteMoto.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocRepresentanteMoto.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        if (ChkCopiaLegalMoto.Visible == true)
                        {
                            if (Doc_Presentados == "")
                                Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCopiaLegalMoto.Text;
                            else
                                Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCopiaLegalMoto.Text;
                            NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                        }
                        int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                        int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                        No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                        string NombreCategoria = ClGestion.NombreCategoriaRNF(SubCategoriaId);
                        Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción en el Registro Nacional Forestal (RNF) en la categoría de “" + NombreCategoria + "” como " + ClGestion.Get_Nombre_SubCategoriaRNF(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                        if (SubCategoriaId == 11)
                            No_Expediente = No_Expediente + "-3.9.2";
                        else if (SubCategoriaId == 15)
                            No_Expediente = No_Expediente + "-3.9.1";
                        No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                        ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]), Correlativo_Anual_SubRegion);
                        ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                        ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                        DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                        string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                        ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                        dsDatosSubRegional.Clear();
                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                }
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    string Mensaje = "";
                    string Doc_Presentados = "";
                    int NumeroDoc_Presntados = 1;
                    string No_Expediente = "";

                    if (ChkSolicitud.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkSolicitud.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkSolicitud.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    if (ChkDocPropiedadbien.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropiedadbien.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropiedadbien.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    if (ChkDocPropietarioAprovechamiento.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkDocPropietarioAprovechamiento.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkDocPropietarioAprovechamiento.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    if (ChkCopiaDocRepresentanteAprovechamiento.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCopiaDocRepresentanteAprovechamiento.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCopiaDocRepresentanteAprovechamiento.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    if (ChkCopiaNombramientoAprovechamiento.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkCopiaNombramientoAprovechamiento.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkCopiaNombramientoAprovechamiento.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    if (ChkPlanManejo.Visible == true)
                    {
                        if (Doc_Presentados == "")
                            Doc_Presentados = NumeroDoc_Presntados + ") " + ChkPlanManejo.Text;
                        else
                            Doc_Presentados = Doc_Presentados + "\n" + NumeroDoc_Presntados + ") " + ChkPlanManejo.Text;
                        NumeroDoc_Presntados = NumeroDoc_Presntados + 1;
                    }
                    int No_Admision_Gestion = ClGestion.Max_Adminision_Gestion();
                    int Correlativo_Anual_SubRegion = ClGestion.Max_No_Expediente(ClGestion.Get_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))));
                    No_Expediente = ClGestion.Get_Region_SubRegion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + "-" + Correlativo_Anual_SubRegion.ToString("000000");
                    string NombreCategoria = ClManejo.Get_NomCategoria_Manejo(SubCategoriaId);
                    Mensaje = "Por este medio se hace constar que de acuerdo a la gestión identificada con el Número " + ClGestion.Get_Nug(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true))) + " se ha recibido completa la papelería para la gestión de: Inscripción del Plan de Manejo Forestal en la categoría de “" + NombreCategoria + "” como " + ClManejo.Get_SubCategoria_Manejo(SubCategoriaId) + ".Para tal efecto se recibió la siguiente documentación:";
                    No_Expediente = No_Expediente + "-1.1.1";
                    No_Expediente = No_Expediente + "-" + DateTime.Now.Year;
                    ClGestion.Insert_Aceptacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), No_Expediente, Mensaje, Doc_Presentados, Convert.ToInt32(Session["UsuarioId"]), Correlativo_Anual_SubRegion);
                    ClGestion.Manda_Gestion_Usuario(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 11);
                    //ClGestion.Insert_Admision_Gestion_Profesional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), TxtSerie.Text, Convert.ToInt32(Txtfactura.Text), Convert.ToInt32(Session["UsuarioId"]));
                    DataSet dsDatosSubRegional = ClGestion.Get_SubRegional(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    string MensajeCorreo = "Se ha enviado a su despacho la gestión del señor (a): " + ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["nom"].ToString()), true);
                    ClUtilitarios.EnvioCorreo(dsDatosSubRegional.Tables["Datos"].Rows[0]["Correo"].ToString(), dsDatosSubRegional.Tables["Datos"].Rows[0]["Nombre"].ToString(), "Envío de gestión", MensajeCorreo, 0, "", "");
                    dsDatosSubRegional.Clear();
                    Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&admissiongestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(No_Admision_Gestion.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                }
            }
            else
            {
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                    {
                        XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                        XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                        //1 Entrego, 0 No Entrego, Null NoAplica 
                        string Titulo = "NULL";
                        string Colegiado = "NULL";
                        string Rtu = "NULL";
                        string Id = "NULL";
                        string PostGrado = "NULL";
                        string Diploma = "NULL";
                        string Pendientes = "";
                        int NumeroPendiente = 1;
                        if (ChkTitulo.Visible == true)
                            if (ChkTitulo.Checked != true)
                            {
                                Titulo = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkTitulo.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkTitulo.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                Titulo = "1";
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("Titulo", Titulo, iElementoDetalle);

                        if (ChkColegiado.Visible == true)
                            if (ChkColegiado.Checked != true)
                            {
                                Colegiado = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkColegiado.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkTitulo.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                                
                            else
                                Colegiado = "1";
                        ClXml.AgregarAtributo("Colegiado", Colegiado, iElementoDetalle);
                        if (ChkRtu.Visible == true)
                            if (ChkRtu.Checked != true)
                            {
                                Rtu = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkRtu.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkRtu.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                                
                            else
                                Rtu = "1";
                        ClXml.AgregarAtributo("Rtu", Rtu, iElementoDetalle);
                        if (ChkId.Visible == true)
                            if (ChkId.Checked != true)
                            {
                                Id = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkId.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkId.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                                
                            else
                                Id = "1";
                        ClXml.AgregarAtributo("DocumentoIdentificacion", Id, iElementoDetalle);
                        if (ChkPosgrado.Visible == true)
                            if (ChkPosgrado.Checked != true)
                            {
                                PostGrado = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkPosgrado.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkPosgrado.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                PostGrado = "1";
                        ClXml.AgregarAtributo("PostGrado", PostGrado, iElementoDetalle);
                        if (ChkDiploma.Visible == true)
                            if (ChkDiploma.Checked != true)
                            {
                                Diploma = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDiploma.Text;
                                else
                                     Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDiploma.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                                
                            else
                                Diploma = "1";
                        ClXml.AgregarAtributo("Diploma", Diploma, iElementoDetalle);
                        iElementoEspecies.AppendChild(iElementoDetalle);
                        iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                        int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                        ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21) || (SubCategoriaId == 18) || (SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43) || (SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33) || (SubCategoriaId == 14))
                    {
                        XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                        XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                        //1 Entrego, 0 No Entrego, Null NoAplica 
                        string CertificacionPV = "NULL";
                        string NoRepresentante = "NULL";
                        string SiRepresentante = "NULL";
                        string NombramientoPV = "NULL";
                        string PoligonoPV = "NULL";
                        string Pendientes = "";
                        int NumeroPendiente = 1;
                        if (ChkCertificacionPV.Visible == true)
                        {
                            if (ChkCertificacionPV.Checked != true)
                            {
                                CertificacionPV = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkCertificacionPV.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCertificacionPV.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                CertificacionPV = "1";
                           
                        }
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("CertificacionPV", CertificacionPV, iElementoDetalle);
                        if (ChkIdNoRepresentantePV.Visible == true)
                        {
                            if (ChkIdNoRepresentantePV.Checked != true)
                            {
                                NoRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkIdNoRepresentantePV.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkIdNoRepresentantePV.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                NoRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("NoRepresentante", NoRepresentante, iElementoDetalle);
                        if (ChkIdSiRepresentantePV.Visible == true)
                        {
                            if (ChkIdSiRepresentantePV.Checked != true)
                            {
                                NoRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkIdSiRepresentantePV.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkIdSiRepresentantePV.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                NoRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("SiRepresentante", SiRepresentante, iElementoDetalle);
                        if (ÇhkCopiaNombramientoPV.Visible == true)
                        {
                            if (ÇhkCopiaNombramientoPV.Checked != true)
                            {
                                NombramientoPV = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ÇhkCopiaNombramientoPV.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ÇhkCopiaNombramientoPV.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                NombramientoPV = "1";
                        }
                        ClXml.AgregarAtributo("NombramientoPV", NombramientoPV, iElementoDetalle);
                        if (ChkPoliginoPV.Visible == true)
                        {
                            if (ChkPoliginoPV.Checked != true)
                            {
                                PoligonoPV = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkPoliginoPV.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkPoliginoPV.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                PoligonoPV = "1";
                        }
                        ClXml.AgregarAtributo("PoligonoPV", PoligonoPV, iElementoDetalle);
                        iElementoEspecies.AppendChild(iElementoDetalle);
                        iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                        int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                        ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24))
                    {
                        XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                        XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                        //1 Entrego, 0 No Entrego, Null NoAplica 
                        string Patente = "NULL";
                        string RtuEmpresa = "NULL";
                        string DocPropietario = "NULL";
                        string DocRepresentante = "NULL";
                        string DocNomRepresentante = "NULL";
                        string Pendientes = "";
                        int NumeroPendiente = 1;
                        if (ChkPatente.Visible == true)
                        {
                            if (ChkPatente.Checked != true)
                            {
                                Patente = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkPatente.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkPatente.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                Patente = "1";

                        }
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("Patente", Patente, iElementoDetalle);
                        if (ChkRtuEmpresa.Visible == true)
                        {
                            if (ChkRtuEmpresa.Checked != true)
                            {
                                RtuEmpresa = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkRtuEmpresa.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkRtuEmpresa.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                RtuEmpresa = "1";
                        }
                        ClXml.AgregarAtributo("RtuEmpresa", RtuEmpresa, iElementoDetalle);
                        if (ChkDocPropietario.Visible == true)
                        {
                            if (ChkDocPropietario.Checked != true)
                            {
                                DocPropietario = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocPropietario.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropietario.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocPropietario = "1";
                        }
                        ClXml.AgregarAtributo("DocPropietario", DocPropietario, iElementoDetalle);
                        if (ChkDocRepresentante.Visible == true)
                        {
                            if (ChkDocRepresentante.Checked != true)
                            {
                                DocRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocRepresentante.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocRepresentante.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocRepresentante", DocRepresentante, iElementoDetalle);
                        if (ChkNomRepresentante.Visible == true)
                        {
                            if (ChkNomRepresentante.Checked != true)
                            {
                                DocNomRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkNomRepresentante.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkNomRepresentante.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                DocNomRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocNomRepresentante", DocNomRepresentante, iElementoDetalle);
                        iElementoEspecies.AppendChild(iElementoDetalle);
                        iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                        int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                        ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28))
                    {
                        XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                        XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                        //1 Entrego, 0 No Entrego, Null NoAplica 
                        string DocConstitucion = "NULL";
                        string CarneSat = "NULL";
                        string RtuEntidad = "NULL";
                        string DocPropietario = "NULL";
                        string DocRepresentante = "NULL";
                        string DocNomRepresentante = "NULL";
                        string CopiaActa = "NULL";
                        string Pendientes = "";
                        int NumeroPendiente = 1;
                        if (ChkDocConstitucion.Visible == true)
                        {
                            if (ChkDocConstitucion.Checked != true)
                            {
                                DocConstitucion = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocConstitucion.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocConstitucion.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                DocConstitucion = "1";

                        }
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("DocConstitucion", DocConstitucion, iElementoDetalle);
                        if (ChkCarneSat.Visible == true)
                        {
                            if (ChkCarneSat.Checked != true)
                            {
                                CarneSat = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocConstitucion.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCarneSat.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                CarneSat = "1";

                        }
                        ClXml.AgregarAtributo("CarneSat", CarneSat, iElementoDetalle);

                        if (ChkRtuEntidad.Visible == true)
                        {
                            if (ChkRtuEntidad.Checked != true)
                            {
                                RtuEntidad = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkRtuEntidad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkRtuEntidad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                RtuEntidad = "1";
                        }
                        ClXml.AgregarAtributo("RtuEntidad", RtuEntidad, iElementoDetalle);

                        if (ChkDocPropietarioEntidad.Visible == true)
                        {
                            if (ChkDocPropietarioEntidad.Checked != true)
                            {
                                DocPropietario = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocPropietarioEntidad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropietarioEntidad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocPropietario = "1";
                        }
                        ClXml.AgregarAtributo("DocPropietario", DocPropietario, iElementoDetalle);

                        if (ChkDocRepresentanteEntidad.Visible == true)
                        {
                            if (ChkDocRepresentanteEntidad.Checked != true)
                            {
                                DocRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocRepresentanteEntidad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocRepresentanteEntidad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocRepresentante", DocRepresentante, iElementoDetalle);

                        if (ChkNomRepresentanteEntidad.Visible == true)
                        {
                            if (ChkNomRepresentanteEntidad.Checked != true)
                            {
                                DocNomRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkNomRepresentanteEntidad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkNomRepresentanteEntidad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                DocNomRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocNomRepresentante", DocNomRepresentante, iElementoDetalle);

                        if (ChkCopiaActa.Visible == true)
                        {
                            if (ChkCopiaActa.Checked != true)
                            {
                                CopiaActa = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkCopiaActa.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCopiaActa.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                CopiaActa = "1";
                        }
                        ClXml.AgregarAtributo("CopiaActa", CopiaActa, iElementoDetalle);
                        iElementoEspecies.AppendChild(iElementoDetalle);
                        iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                        int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                        ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                    else if ((SubCategoriaId == 11) || (SubCategoriaId == 15))
                    {
                        XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                        XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                        //1 Entrego, 0 No Entrego, Null NoAplica 
                        string PatenteMoto = "NULL";
                        string RtuMoto = "NULL";
                        string DocPropiedad = "NULL";
                        string CopiaPropiedad = "NULL";
                        string DocPropietario = "NULL";
                        string DocRepresentante = "NULL";
                        string DocNomRepresentante = "NULL";
                        string Pendientes = "";
                        int NumeroPendiente = 1;
                        if (ChkPatenteMoto.Visible == true)
                        {
                            if (ChkPatenteMoto.Checked != true)
                            {
                                PatenteMoto = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkPatenteMoto.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkPatenteMoto.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                PatenteMoto = "1";

                        }
                        XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                        ClXml.AgregarAtributo("PatenteMoto", PatenteMoto, iElementoDetalle);
                        if (ChkRtuMoto.Visible == true)
                        {
                            if (ChkRtuMoto.Checked != true)
                            {
                                RtuMoto = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkRtuMoto.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkRtuMoto.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                RtuMoto = "1";

                        }
                        ClXml.AgregarAtributo("RtuMoto", RtuMoto, iElementoDetalle);

                        if (ChkDocPropiedad.Visible == true)
                        {
                            if (ChkDocPropiedad.Checked != true)
                            {
                                DocPropiedad = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocPropiedad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropiedad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocPropiedad = "1";
                        }
                        ClXml.AgregarAtributo("DocPropiedad", DocPropiedad, iElementoDetalle);

                        if (ChkCopiaPropiedad.Visible == true)
                        {
                            if (ChkCopiaPropiedad.Checked != true)
                            {
                                CopiaPropiedad = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkCopiaPropiedad.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCopiaPropiedad.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                CopiaPropiedad = "1";
                        }
                        ClXml.AgregarAtributo("CopiaPropiedad", CopiaPropiedad, iElementoDetalle);

                        if (ChkDocPropietarioMoto.Visible == true)
                        {
                            if (ChkDocPropietarioMoto.Checked != true)
                            {
                                DocPropietario = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocPropietarioMoto.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropietarioMoto.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocPropietario = "1";
                        }
                        ClXml.AgregarAtributo("DocPropietario", DocPropietario, iElementoDetalle);

                        if (ChkDocRepresentanteMoto.Visible == true)
                        {
                            if (ChkDocRepresentanteMoto.Checked != true)
                            {
                                DocRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkDocRepresentanteMoto.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocRepresentanteMoto.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }

                            else
                                DocRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocRepresentante", DocRepresentante, iElementoDetalle);

                        if (ChkCopiaLegalMoto.Visible == true)
                        {
                            if (ChkCopiaLegalMoto.Checked != true)
                            {
                                DocNomRepresentante = "0";
                                if (Pendientes == "")
                                    Pendientes = NumeroPendiente + ") " + ChkCopiaLegalMoto.Text;
                                else
                                    Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCopiaLegalMoto.Text;
                                NumeroPendiente = NumeroPendiente + 1;
                            }
                            else
                                DocNomRepresentante = "1";
                        }
                        ClXml.AgregarAtributo("DocNomRepresentante", DocNomRepresentante, iElementoDetalle);

                        iElementoEspecies.AppendChild(iElementoDetalle);
                        iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                        int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                        ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                        Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                    }
                }
                else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
                {
                    int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                    XmlDocument iInformacion = ClXml.CrearDocumentoXML("Informacion");
                    XmlNode iElementoEspecies = iInformacion.CreateElement("Requisitos");
                    //1 Entrego, 0 No Entrego, Null NoAplica 
                    string Solicitud = "NULL";
                    string DocPropiedadbien = "NULL";
                    string DocPropietarioAprovechamiento = "NULL";
                    string CopiaDocRepresentanteAprovechamiento = "NULL";
                    string CopiaNombramientoAprovechamiento = "NULL";
                    string PlanManejo = "NULL";
                    string Pendientes = "";
                    int NumeroPendiente = 1;
                    if (ChkSolicitud.Visible == true)
                    {
                        if (ChkSolicitud.Checked != true)
                        {
                            Solicitud = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkSolicitud.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkSolicitud.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }
                        else
                            Solicitud = "1";

                    }
                    XmlNode iElementoDetalle = iInformacion.CreateElement("Item");
                    ClXml.AgregarAtributo("Solicitud", Solicitud, iElementoDetalle);
                    if (ChkDocPropiedadbien.Visible == true)
                    {
                        if (ChkDocPropiedadbien.Checked != true)
                        {
                            DocPropiedadbien = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkDocPropiedadbien.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropiedadbien.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }
                        else
                            DocPropiedadbien = "1";

                    }
                    ClXml.AgregarAtributo("DocPropiedadbien", DocPropiedadbien, iElementoDetalle);



                    if (ChkDocPropietarioAprovechamiento.Visible == true)
                    {
                        if (ChkDocPropietarioAprovechamiento.Checked != true)
                        {
                            DocPropietarioAprovechamiento = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkDocPropietarioAprovechamiento.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkDocPropietarioAprovechamiento.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }

                        else
                            DocPropietarioAprovechamiento = "1";
                    }
                    ClXml.AgregarAtributo("DocPropietarioAprovechamiento", DocPropietarioAprovechamiento, iElementoDetalle);



                    if (ChkCopiaDocRepresentanteAprovechamiento.Visible == true)
                    {
                        if (ChkCopiaDocRepresentanteAprovechamiento.Checked != true)
                        {
                            CopiaDocRepresentanteAprovechamiento = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkCopiaDocRepresentanteAprovechamiento.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCopiaDocRepresentanteAprovechamiento.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }

                        else
                            CopiaDocRepresentanteAprovechamiento = "1";
                    }
                    ClXml.AgregarAtributo("CopiaDocRepresentanteAprovechamiento", CopiaDocRepresentanteAprovechamiento, iElementoDetalle);

                    if (ChkCopiaNombramientoAprovechamiento.Visible == true)
                    {
                        if (ChkCopiaNombramientoAprovechamiento.Checked != true)
                        {
                            CopiaNombramientoAprovechamiento = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkCopiaNombramientoAprovechamiento.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkCopiaNombramientoAprovechamiento.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }

                        else
                            CopiaNombramientoAprovechamiento = "1";
                    }
                    ClXml.AgregarAtributo("CopiaNombramientoAprovechamiento", CopiaNombramientoAprovechamiento, iElementoDetalle);

                    if (ChkPlanManejo.Visible == true)
                    {
                        if (ChkPlanManejo.Checked != true)
                        {
                            PlanManejo = "0";
                            if (Pendientes == "")
                                Pendientes = NumeroPendiente + ") " + ChkPlanManejo.Text;
                            else
                                Pendientes = Pendientes + "\n" + NumeroPendiente + ") " + ChkPlanManejo.Text;
                            NumeroPendiente = NumeroPendiente + 1;
                        }
                        else
                            PlanManejo = "1";
                    }
                    ClXml.AgregarAtributo("PlanManejo", PlanManejo, iElementoDetalle);

                    iElementoEspecies.AppendChild(iElementoDetalle);
                    iInformacion.ChildNodes[1].AppendChild(iElementoEspecies);
                    int Max_GestionInCompletaId = ClGestion.Max_GestionInCompletaId();
                    ClGestion.Insert_Solicitud_Completacion_Gestion(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), iInformacion, Convert.ToInt32(Session["UsuarioId"]), Pendientes);

                    Response.Redirect("~/WebForms/Wfrm_GestionNueva.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&gestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Max_GestionInCompletaId.ToString(), true)) + "&subcategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "");
                }
            }
        }

        void ImgVerinfo_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
            {
                int SubCategoriaId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true));
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
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
                else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21))
                {
                    
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),1);
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (SubCategoriaId == 18)
                {
                    Session["Ds_Formulario_Plantacion_Voluntaria"] = ClGestion.ImpresionFormularioPlantacionVoluntaria(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)), 2);
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioPlantacion.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_SistemaAgroforestal"] = ClGestion.ImpresionFormularioSistemaAgroforestal(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioSistemasAgroforestales.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33))
                {
                    Session["TipoReporte"] = "2";
                    Session["Ds_Formulario_FuenteSemillera"] = ClGestion.ImpresionFormularioFuenteSemillera(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioFuenteSemillera.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "&traiteid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24))
                {
                    Session["Ds_Empresas"] = ClGestion.ImpresionFormularioEmpresas(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEmpresas.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&traite=" + HttpUtility.UrlEncode(Request.QueryString["traite"]) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28))
                {
                    Session["Dt_Entidad"] = ClGestion.ImpresionFormularioEntidad(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Vista Previa Insripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioEntidad.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if ((SubCategoriaId == 11) || (SubCategoriaId == 15))
                {
                    Session["Ds_MotoSierra"] = ClGestion.ImpresionFormularioMotoSierra(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Vista Previa Insripción";
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioMotoSierra.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else if (SubCategoriaId == 14)
                {
                    Session["Ds_Formulario_BosqueNatural"] = ClGestion.ImpresionFormularioBosqueNatural(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)));
                    RadWindow1.Title = "Formulario de Inscripción";
                    Session["TipoReporte"] = 2;
                    RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepFormularioBosqueNatural.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
            }
            else if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 2)
            {
                RadWindow1.Title = "Plan de Manejo Forestal";
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true)),2);
                int GestionId = Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["gestion"].ToString()), true));
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionId.ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void BtnProcesar_Click(object sender, EventArgs e)
        {
            if (ValidarRequisitos(Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)), Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["subcategoria"].ToString()), true))) == true)
            {
                Session["Valida"] = true;
                

                
                BtnProcesar.Visible = false;
                ChkTitulo.Enabled = false;
                ChkColegiado.Enabled = false;
                ChkRtu.Enabled = false;
                ChkId.Enabled = false;
                ChkPosgrado.Enabled = false;
                ChkDiploma.Enabled = false;
                
                if (Convert.ToInt32(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["modulo"].ToString()), true)) == 3)
                    DivDatosFac.Visible = true;
                else
                {
                    LblTitConfirmacion.Text = "La Gestíon será enviada el siguiente paso, ¿esta seguro (a)?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }


            }
            else
            {
                Session["Valida"] = false;
                LblTitConfirmacion.Text = "La Gestíon generara una solicitud de completación, ¿esta seguro (a)?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            
        }

        bool ValidarRequisitos(int ModuloId, int SubCategoriaId)
        {
            bool Valida = true;
            if (ModuloId == 3)
            {
                if ((SubCategoriaId == 1) || (SubCategoriaId == 2) || (SubCategoriaId == 3) || (SubCategoriaId == 16))
                {
                    if (ChkTitulo.Visible == true)
                        if (ChkTitulo.Checked != true)
                            Valida = false;
                    if (ChkColegiado.Visible == true)
                        if (ChkColegiado.Checked != true)
                            Valida = false;
                    if (ChkRtu.Visible == true)
                        if (ChkRtu.Checked != true)
                            Valida = false;
                    if (ChkId.Visible == true)
                        if (ChkId.Checked != true)
                            Valida = false;
                    if (ChkPosgrado.Visible == true)
                        if (ChkPosgrado.Checked != true)
                            Valida = false;
                    if (ChkDiploma.Visible == true)
                        if (ChkDiploma.Checked != true)
                            Valida = false;
                }
                else if ((SubCategoriaId == 4) || (SubCategoriaId == 5) || (SubCategoriaId == 19) || (SubCategoriaId == 20) || (SubCategoriaId == 21) || (SubCategoriaId == 18) || (SubCategoriaId == 34) || (SubCategoriaId == 35) || (SubCategoriaId == 36) || (SubCategoriaId == 37) || (SubCategoriaId == 38) || (SubCategoriaId == 39) || (SubCategoriaId == 40) || (SubCategoriaId == 41) || (SubCategoriaId == 42) || (SubCategoriaId == 43) || (SubCategoriaId == 13) || (SubCategoriaId == 29) || (SubCategoriaId == 30) || (SubCategoriaId == 31) || (SubCategoriaId == 32) || (SubCategoriaId == 33) || (SubCategoriaId == 14))
                {
                    if (ChkCertificacionPV.Visible == true)
                        if (ChkCertificacionPV.Checked != true)
                            Valida = false;
                    if (ChkIdNoRepresentantePV.Visible == true)
                        if (ChkIdNoRepresentantePV.Checked != true)
                            Valida = false;
                    if (ChkIdSiRepresentantePV.Visible == true)
                        if (ChkIdSiRepresentantePV.Checked != true)
                            Valida = false;
                    if (ÇhkCopiaNombramientoPV.Visible == true)
                        if (ÇhkCopiaNombramientoPV.Checked != true)
                            Valida = false;
                    if (ChkPoliginoPV.Visible == true)
                        if (ChkPoliginoPV.Checked != true)
                            Valida = false;
                }
                else if ((SubCategoriaId == 7) || (SubCategoriaId == 9) || (SubCategoriaId == 10) || (SubCategoriaId == 12) || (SubCategoriaId == 17) || (SubCategoriaId == 22) || (SubCategoriaId == 23) || (SubCategoriaId == 24))
                {
                    if (ChkPatente.Visible == true)
                        if (ChkPatente.Checked != true)
                            Valida = false;
                    if (ChkRtuEmpresa.Visible == true)
                        if (ChkRtuEmpresa.Checked != true)
                            Valida = false;
                    if (ChkDocPropietario.Visible == true)
                        if (ChkDocPropietario.Checked != true)
                            Valida = false;
                    if (ChkDocRepresentante.Visible == true)
                        if (ChkDocRepresentante.Checked != true)
                            Valida = false;
                    if (ChkNomRepresentante.Visible == true)
                        if (ChkNomRepresentante.Checked != true)
                            Valida = false;
                }
                else if ((SubCategoriaId == 25) || (SubCategoriaId == 26) || (SubCategoriaId == 27) || (SubCategoriaId == 28))
                {
                    if (ChkDocConstitucion.Visible == true)
                        if (ChkDocConstitucion.Checked != true)
                            Valida = false;
                    if (ChkCarneSat.Visible == true)
                        if (ChkCarneSat.Checked != true)
                            Valida = false;
                    if (ChkRtuEntidad.Visible == true)
                        if (ChkRtuEntidad.Checked != true)
                            Valida = false;
                    if (ChkDocPropietarioEntidad.Visible == true)
                        if (ChkDocPropietarioEntidad.Checked != true)
                            Valida = false;
                    if (ChkDocRepresentanteEntidad.Visible == true)
                        if (ChkDocRepresentanteEntidad.Checked != true)
                            Valida = false;
                    if (ChkNomRepresentanteEntidad.Visible == true)
                        if (ChkNomRepresentanteEntidad.Checked != true)
                            Valida = false;
                    if (ChkCopiaActa.Visible == true)
                        if (ChkCopiaActa.Checked != true)
                            Valida = false;
                }
                else if ((SubCategoriaId == 11) || (SubCategoriaId == 15))
                {
                    if (ChkPatenteMoto.Visible == true)
                        if (ChkPatenteMoto.Checked != true)
                            Valida = false;
                    if (ChkRtuMoto.Visible == true)
                        if (ChkRtuMoto.Checked != true)
                            Valida = false;
                    if (ChkDocPropiedad.Visible == true)
                        if (ChkDocPropiedad.Checked != true)
                            Valida = false;
                    if (ChkCopiaPropiedad.Visible == true)
                        if (ChkCopiaPropiedad.Checked != true)
                            Valida = false;
                    if (ChkDocPropietarioMoto.Visible == true)
                        if (ChkDocPropietarioMoto.Checked != true)
                            Valida = false;
                    if (ChkDocRepresentanteMoto.Visible == true)
                        if (ChkDocRepresentanteMoto.Checked != true)
                            Valida = false;
                    if (ChkCopiaLegalMoto.Visible == true)
                        if (ChkCopiaLegalMoto.Checked != true)
                            Valida = false;
                }
            }
            else if (ModuloId == 2)
            {
                if (ChkSolicitud.Visible == true)
                    if (ChkSolicitud.Checked != true)
                        Valida = false;
                if (ChkDocPropiedadbien.Visible == true)
                    if (ChkDocPropiedadbien.Checked != true)
                        Valida = false;
                if (ChkDocPropietarioAprovechamiento.Visible == true)
                    if (ChkDocPropietarioAprovechamiento.Checked != true)
                        Valida = false;
                if (ChkCopiaDocRepresentanteAprovechamiento.Visible == true)
                    if (ChkCopiaDocRepresentanteAprovechamiento.Checked != true)
                        Valida = false;
                if (ChkCopiaNombramientoAprovechamiento.Visible == true)
                    if (ChkCopiaNombramientoAprovechamiento.Checked != true)
                        Valida = false;
                if (ChkPlanManejo.Visible == true)
                    if (ChkPlanManejo.Checked != true)
                        Valida = false;

            }
            if (Valida == false)
                return false;
            else
                return true;
        }

        void SetIdentificacionRequisitos(int GestionId, int ModuloId, int SubCategoriaId, int ProfesionId, int CategoriaProfesionId, int OrigenPersonaId, int CategoriaId)
        {
            if (ModuloId == 3)
            {
                if (CategoriaId == 7)
                {
                    Requisitos_Profesional.Visible = true;
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(GestionId);
                    if (CategoriaProfesionId == 1)
                        ChkTitulo.Visible = true;
                    else
                        ChkColegiado.Visible = true;
                    if (ProfesionId == 5)
                        ChkPosgrado.Visible = true;
                    if ((SubCategoriaId == 16) || (SubCategoriaId == 1))
                        ChkDiploma.Visible = true;
                    if (OrigenPersonaId == 2)
                        ChkId.Text = "Copia de documento personal de identificación (Pasaporte)";
                }
                else if ((CategoriaId == 2) || (CategoriaId == 3) || (CategoriaId == 4) || (CategoriaId == 6) || (CategoriaId == 1))
                {
                    Div_Requisitos_Plantacion.Visible = true;
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(GestionId);
                    DataSet dsDatos = ClGestion.Datos_Formulario_Plantacion(GestionId);
                    int TipoPersona = ClGestion.Get_Tipo_Persona_Fincas(2, GestionId);
                    if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Representantes"]) > 0) || (TipoPersona == 2))
                    {
                        ChkIdSiRepresentantePV.Visible = true;
                        ÇhkCopiaNombramientoPV.Visible = true;
                    }
                    if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Propietarios"]) > 0)
                        ChkIdNoRepresentantePV.Visible = true;
                    dsDatos.Clear();
                }
                else if (CategoriaId == 5)
                {
                    Div_Requisitos_Empresas.Visible = true;
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(GestionId);
                    DataSet dsDatos = ClGestion.Datos_Formulario_Empresas(GestionId);
                    if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0)
                    {
                        ChkDocPropietario.Visible = true;
                        ChkDocRepresentante.Visible = true;
                        ChkNomRepresentante.Visible = true;
                    }
                    else
                    {
                        ChkDocRepresentante.Visible = true;
                        ChkNomRepresentante.Visible = true;
                    }

                    dsDatos.Clear();
                }
                else if (CategoriaId == 8)
                {
                    Div_Requisitos_Entidades.Visible = true;
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(GestionId);
                    DataSet dsDatos = ClGestion.Datos_Formulario_Entidad(GestionId);
                    if (Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["Representantes"]) == 0)
                        ChkDocPropietarioEntidad.Visible = true;
                    else
                    {
                        ChkDocRepresentanteEntidad.Visible = true;
                        ChkNomRepresentanteEntidad.Visible = true;
                    }
                    if (SubCategoriaId == 28)
                        ChkCopiaActa.Visible = true;

                    dsDatos.Clear();
                }
                else if (CategoriaId == 9)
                {
                    Div_Requisitos_MotoSierras.Visible = true;
                    LblIdentificacion.Text = ClGestion.Get_Identificacion_Gestion(GestionId);
                    DataSet dsDatos = ClGestion.Get_Datos_Formulario_MotoSierra(GestionId);
                    if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0)))
                        ChkDocPropietarioMoto.Visible = true;
                    if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) > 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) > 0)))
                    {
                        ChkDocPropietarioMoto.Visible = true;
                        ChkDocRepresentanteMoto.Visible = true;
                        ChkCopiaLegalMoto.Visible = true;
                    }
                    if ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosProp"]) == 0) && ((Convert.ToInt32(dsDatos.Tables["DATOS"].Rows[0]["CuantosRep"]) == 0)))
                    {
                        ChkDocRepresentanteMoto.Visible = true;
                        ChkNomRepresentanteEntidad.Visible = true;
                    }

                    dsDatos.Clear();
                    if (SubCategoriaId == 15)
                    {
                        ChkPatenteMoto.Visible = true;
                        ChkRtuMoto.Visible = true;
                    }
                    else if (SubCategoriaId == 11)
                    {
                        ChkDocPropiedad.Visible = true;
                        ChkCopiaPropiedad.Visible = true;
                    }
                }
            }
            else if (ModuloId == 2)
            {
                Div_Requisitos_Aprovechamiento.Visible = true;
                LblIdentificacion.Text = ClManejo.Get_Identificacion_Gestion_Manejo(GestionId);
                //if (ClManejo.Existe_Representatnes_PlanManejo(AsignacionId) == 0)  //Cambiarlo a las tablas reales
                //{

                //}
                ChkDocPropietarioAprovechamiento.Visible = true;

            }
            
        }

    }
}