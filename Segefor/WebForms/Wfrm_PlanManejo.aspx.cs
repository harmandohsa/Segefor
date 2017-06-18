using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using Telerik.Web.UI;
using System.IO;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_PlanManejo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Persona ClPersona;
        Cl_Manejo ClManejo;
        Cl_Registro ClRegistro;
        Cl_Inmueble ClInmueble;
        Cl_Gestion_Registro Cl_Gestion;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClPersona = new Cl_Persona();
            ClManejo = new Cl_Manejo();
            ClRegistro = new Cl_Registro();
            ClInmueble = new Cl_Inmueble();
            Cl_Gestion = new Cl_Gestion_Registro();

            GrdPlanesSolicitados.NeedDataSource += GrdPlanesSolicitados_NeedDataSource;
            GrdPlanesSolicitadosComoRegente.NeedDataSource += GrdPlanesSolicitadosComoRegente_NeedDataSource;
            GrdPlanesSolicitadosComoRegente.ItemCommand += GrdPlanesSolicitadosComoRegente_ItemCommand;
            GrdPlanesSolicitadosComoRegente.ItemDataBound += GrdPlanesSolicitadosComoRegente_ItemDataBound;
            GrdPlanesSolicitados.ItemDataBound += GrdPlanesSolicitados_ItemDataBound;
            GrdPlanesSolicitados.ItemCommand += GrdPlanesSolicitados_ItemCommand;
            BtnYes.ServerClick += BtnYes_ServerClick;

            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(46, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));


                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 46);
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

                int EsEMPF = ClRegistro.EsElaboradorPM_Activo(Convert.ToInt32(Session["PersonaId"]));
                if (EsEMPF == 0)
                {
                    DivTitPlanProceso.Visible = false;
                    DivPlanProceso.Visible = false;
                }
            }

        }

        void CopiarAnexos(int AsignacionId, int GestionId)
        {
            
            
            string sourceDirectory = Server.MapPath(".") + @"\Archivos\Anexos\MapaPendiente\" + AsignacionId;
            string targetDirectory = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaPendiente\" + GestionId;
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            if (diSource.Exists)
            {
                CopyAll(diSource, diTarget);
                DeleteAll(diSource);
            }

            string sourceDirectoryCro = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId;
            string targetDirectoryCro = Server.MapPath(".") + @"\Archivos\AnexosPM\Croquis\" + GestionId;
            DirectoryInfo diSourceCro = new DirectoryInfo(sourceDirectoryCro);
            DirectoryInfo diTargetCro = new DirectoryInfo(targetDirectoryCro);
            if (diSourceCro.Exists)
            {
                CopyAll(diSourceCro, diTargetCro);
                DeleteAll(diSourceCro);
            }

            string sourceDirectoryRonda = Server.MapPath(".") + @"\Archivos\Anexos\MapaRonda\" + AsignacionId;
            string targetDirectoryRonda = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaRonda\" + GestionId;
            DirectoryInfo diSourceRonda = new DirectoryInfo(sourceDirectoryRonda);
            DirectoryInfo diTargetRonda = new DirectoryInfo(targetDirectoryRonda);
            if (diSourceRonda.Exists)
            {
                CopyAll(diSourceRonda, diTargetRonda);
                DeleteAll(diSourceRonda);
            }

            string sourceDirectoryUbi = Server.MapPath(".") + @"\Archivos\Anexos\MapaUbicacion\" + AsignacionId;
            string targetDirectoryUbi = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUbicacion\" + GestionId;
            DirectoryInfo diSourceUbi = new DirectoryInfo(sourceDirectoryUbi);
            DirectoryInfo diTargetUbi = new DirectoryInfo(targetDirectoryUbi);
            if (diSourceUbi.Exists)
            {
                CopyAll(diSourceUbi, diTargetUbi);
                DeleteAll(diSourceUbi);
            }

            string sourceDirectoryUsoActual = Server.MapPath(".") + @"\Archivos\Anexos\MapaUsoActual\" + AsignacionId;
            string targetDirectoryUsoActual = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUsoActual\" + GestionId;
            DirectoryInfo diSourceUsoActual = new DirectoryInfo(sourceDirectoryUsoActual);
            DirectoryInfo diTargetUsoActual = new DirectoryInfo(targetDirectoryUsoActual);
            if (diSourceUsoActual.Exists)
            {
                CopyAll(diSourceUsoActual, diTargetUsoActual);
                DeleteAll(diSourceUsoActual);
            }

        }

        public static void DeleteAll(DirectoryInfo source)
        {
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.Delete();
            }
            source.Delete();
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

        void BtnYes_ServerClick(object sender, EventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtAsignacionId.Text);
            int GestionId = Cl_Gestion.MaxGestionId(1);
            int Correlativo_Anual = Cl_Gestion.MaxGestionId(2);
            string NUG = "NUG-" + Correlativo_Anual + "-" + Convert.ToDateTime(ClUtilitarios.FechaDB()).Year;
            int PersonaId = ClManejo.Get_PersonaId_AsignacionId(AsignacionId);

            DataSet Inmuebles = ClManejo.Get_Fincas_Completas_Manejo(Convert.ToInt32(Convert.ToInt32(TxtAsignacionId.Text)));
            int InmuebleId = Convert.ToInt32(Inmuebles.Tables["DATOS2"].Rows[0]["InmuebleId"]);
            Inmuebles.Clear();

            DataSet dsRegioSubregionInmueble = ClInmueble.Get_Region_Subregion_Inmueble(InmuebleId);
            string SubRegionId = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString();

            string Region = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["Region"].ToString();
            //TxtSubRegion.Text = dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubRegion"].ToString();
            ClManejo.ActualizaEstatusAsignacionElaborador(Convert.ToInt32(TxtAsignacionId.Text), 5);

            Cl_Gestion.Insertar_Gestion(GestionId, NUG, Convert.ToInt32(PersonaId), Convert.ToInt32(SubRegionId), 13, 2, Correlativo_Anual);
            ClManejo.Traslada_TempManejo(GestionId, AsignacionId);
            ClManejo.Traslada_PlanManejo_ConvierteXml(GestionId, AsignacionId);
            CopiarAnexos(AsignacionId, GestionId);
            ClManejo.Elimina_TempPlanManejo(AsignacionId);
            string Mensaje = "Su solicitud fue enviada exitosamente, con Numero Único de Gestión (NUG): " + NUG + ". Por lo que solicitamos presentarse a la oficina Subregional " + Region + ", con la documentación que deberá presentar para dar trámite a su solicitud.";
            ClUtilitarios.EnvioCorreo(Session["Correo_Usuario"].ToString(), ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"])).ToString(), "Solicitud SEGEFOR", Mensaje, 0, "", "");
            Response.Redirect("~/WebForms/Wfrm_Inicio.aspx?appel=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&traite=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(NUG, true)) + "");
        }

        void GrdPlanesSolicitados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadWindow1.Title = "Plan de Manejo Forestal";
                int SubCategoriaId = ClManejo.Get_SubCategoriaPlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 1,2);
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_PlanManejoForestal.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&source=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&souscategorie=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubCategoriaId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            
            else if (e.CommandName == "CmdAnexos")
            {
                int Id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]);
                string GestionNo = "";
                String js = "window.open('Wfrm_AnexosPlanManejo.aspx?idgestion=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(Id.ToString(), true)) + "&NUG=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(GestionNo.ToString(), true)) + "&typpe=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "', '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
            }
            else if (e.CommandName == "CmdDevElb")
            {
                ClManejo.ActualizaEstatusAsignacionElaborador(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 2);
                GrdPlanesSolicitados.Rebind();
                GrdPlanesSolicitadosComoRegente.Rebind();
            }
            else if (e.CommandName == "CmdPrintSol")
            {
                DataSet Inmuebles = ClManejo.Get_Fincas_Completas_Manejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]));
                int InmuebleId = Convert.ToInt32(Inmuebles.Tables["DATOS2"].Rows[0]["InmuebleId"]);
                Inmuebles.Clear();

                DataSet dsRegioSubregionInmueble = ClInmueble.Get_Region_Subregion_Inmueble(Convert.ToInt32(InmuebleId));
                int RegionId = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["RegionId"].ToString());
                int SubRegionId = Convert.ToInt32(dsRegioSubregionInmueble.Tables["Datos"].Rows[0]["SubregionId"].ToString());
                RadWindow1.Title = "Solicitud Plan de Manejo Forestal";
                RadWindow1.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepSolicitudPlanManejo.aspx?identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&idnoiger=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(RegionId.ToString(), true)) + "&sousnoigerid=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(SubRegionId.ToString(), true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindow1.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            else if (e.CommandName == "CmdEnvInab")
            {
                TxtAsignacionId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString();
                LblTitConfirmacion.Text = "La Gestíon sera enviada al INAB, ¿esta seguro (a)?";
                Label1.Text = "La Gestíon sera enviada al INAB, ¿esta seguro (a)?";
                if (ClManejo.Existe_Representatnes_PlanManejo(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"])) == 0)
                {
                    DocPropietario.Visible = true;
                }
                else
                {
                    DocRepresentante.Visible = true;
                    DocRepresentanteDos.Visible = true;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWindowConfirm.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdPlanesSolicitados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                if (Convert.ToInt32(item.GetDataKeyValue("EstatusId")) == 4)
                {
                    ((ImageButton)item["VerInfo"].FindControl("ImgVerinfo")).Visible = true;
                    ((ImageButton)item["Anexos"].FindControl("ImgAnexos")).Visible = true;
                    ((ImageButton)item["DevElab"].FindControl("ImgDevElab")).Visible = true;
                    ((ImageButton)item["EnvINAB"].FindControl("ImgEnvINAB")).Visible = true;
                    ((ImageButton)item["PrintSol"].FindControl("ImgPrintSol")).Visible = true;
                }
            }
        }

        void GrdPlanesSolicitadosComoRegente_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (dataItem["FechaAcepta"].Text == "&nbsp;")
                    dataItem["Go"].FindControl("ImgIrPlan").Visible = false;
                else
                {
                    dataItem["Ok"].FindControl("ImgAceptar").Visible = false;
                    dataItem["No"].FindControl("ImgRechazar").Visible = false;
                }
            }
        }

        void GrdPlanesSolicitadosComoRegente_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdOk")
            {
                ClManejo.ActualizaEstatusFechaAsignacionElaborador(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 2);
                GrdPlanesSolicitadosComoRegente.Rebind();
            }
            else if (e.CommandName == "CmdNo")
            {
                ClManejo.ActualizaEstatusFechaAsignacionElaborador(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"]), 3);
                DataSet dsUsuario =  ClUsuario.Datos_UsuarioId(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"]));
                string Correo = dsUsuario.Tables["Datos"].Rows[0]["Correo"].ToString();
                int PersonaId = Convert.ToInt32(dsUsuario.Tables["Datos"].Rows[0]["PersonaId"].ToString());
                dsUsuario.Clear();
                string Mensaje = "Se le notifica que No se acepta la realización de su Plan de Manejo Forestal.";
                ClUtilitarios.EnvioCorreo(Correo, ClPersona.Nombre_Usuario(PersonaId), "Rechazo Plan de Manejo", Mensaje, 0, "", "");
                GrdPlanesSolicitadosComoRegente.Rebind();
            }
            else if (e.CommandName == "CmdGo")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubCategoriaId"].ToString() == "")
                    Response.Redirect("~/WebForms/Wfrm_SeleccionPlanMenejo.aspx?typecategoria=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CategoriaId"].ToString(), true)) + "&affectation=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&utilisater=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString(), true)) + "");
                else
                    Response.Redirect("~/WebForms/Wfrm_TipoPlanManejo.aspx?typeplan=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SubCategoriaId"].ToString(), true)) + "&affectation=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AsignacionId"].ToString(), true)) + "&utilisater=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsuarioId"].ToString(), true)) + "");
            }
        }

        void GrdPlanesSolicitadosComoRegente_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_PlanesManejo(2, Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(Session["UsuarioId"])), GrdPlanesSolicitadosComoRegente);
        }

        void GrdPlanesSolicitados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.Get_PlanesManejo(1,Convert.ToInt32(Session["PersonaId"]),Convert.ToInt32(Session["UsuarioId"])),GrdPlanesSolicitados);
        }
    }
}