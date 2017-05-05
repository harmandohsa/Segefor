using SEGEFOR.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_AnexosPlanManejo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Manejo ClManejo;
        Cl_Poligono ClPoligono;
        Cl_Manejo_Impresion ClManejoImpresion;

        DataSet DsAnexos = new DataSet("Anexos");

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            ClManejo = new Cl_Manejo();
            ClPoligono = new Cl_Poligono();
            ClManejoImpresion = new Cl_Manejo_Impresion();

            DataTable DtAnexosCroquis = DsAnexos.Tables.Add("AnexoCroquis");
            DataColumn NombreAnexoCroquis = DtAnexosCroquis.Columns.Add("NombreAnexoCroquis", typeof(string));
            DataColumn PathAnexoCroquis = DtAnexosCroquis.Columns.Add("PathAnexoCroquis", typeof(string));


            DataTable DtAnexosMapaUsoActual = DsAnexos.Tables.Add("AnexoUsoActual");
            DataColumn NombreAnexoMapaUsoActual = DtAnexosMapaUsoActual.Columns.Add("NombreAnexoMapaUsoActual", typeof(string));
            DataColumn PathAnexoMapaUsoActual = DtAnexosMapaUsoActual.Columns.Add("PathAnexoMapaUsoActual", typeof(string));

            DataTable DtAnexosMapaPendintes = DsAnexos.Tables.Add("AnexoMapaPendiente");
            DataColumn NombreAnexoMapaPendiente = DtAnexosMapaPendintes.Columns.Add("NombreAnexoMapaPendiente", typeof(string));
            DataColumn PathAnexoMapaPendiente = DtAnexosMapaPendintes.Columns.Add("PathAnexoMapaPendiente", typeof(string));

            DataTable DtAnexosMapaUbicacion = DsAnexos.Tables.Add("AnexoMapaUbicacion");
            DataColumn NombreAnexoMapaUbicacion = DtAnexosMapaUbicacion.Columns.Add("NombreAnexoMapaUbicacion", typeof(string));
            DataColumn PathAnexoMapaUbicacion = DtAnexosMapaUbicacion.Columns.Add("PathAnexoMapaUbicacion", typeof(string));

            DataTable DtAnexosMapaRonda = DsAnexos.Tables.Add("AnexoMapaRonda");
            DataColumn NombreAnexoMapaRonda = DtAnexosMapaRonda.Columns.Add("NombreAnexoMapaRonda", typeof(string));
            DataColumn PathAnexoMapaRonda = DtAnexosMapaRonda.Columns.Add("PathAnexoMapaRonda", typeof(string));

            GrdAnexoCroquia.NeedDataSource += GrdAnexoCroquia_NeedDataSource;
            GrdAnexoMapaUsoActual.NeedDataSource += GrdAnexoMapaUsoActual_NeedDataSource;
            GrdAnexoMapaPendiente.NeedDataSource += GrdAnexoMapaPendiente_NeedDataSource;
            GrdAnexoMapaUbicacion.NeedDataSource += GrdAnexoMapaUbicacion_NeedDataSource;
            GrdInmueblePol.NeedDataSource += GrdInmueblePol_NeedDataSource;
            GrdAnexoMapaRonda.NeedDataSource += GrdAnexoMapaRonda_NeedDataSource;
            GrdAnexoCroquia.ItemCommand += GrdAnexoCroquia_ItemCommand;
            GrdAnexoMapaUsoActual.ItemCommand += GrdAnexoMapaUsoActual_ItemCommand;
            GrdAnexoMapaPendiente.ItemCommand += GrdAnexoMapaPendiente_ItemCommand;
            GrdAnexoMapaUbicacion.ItemCommand += GrdAnexoMapaUbicacion_ItemCommand;
            GrdAnexoMapaRonda.ItemCommand += GrdAnexoMapaRonda_ItemCommand;
            GrdInmueblePol.ItemCommand += GrdInmueblePol_ItemCommand;
            TxtId.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["idgestion"].ToString()), true);
            LblNug.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["NUG"].ToString()), true);
            ImgPolRepoblacion.Click += ImgPolRepoblacion_Click;
            ImgPrintCenso.Click += ImgPrintCenso_Click;
        }

        void ImgPrintCenso_Click(object sender, ImageClickEventArgs e)
        {
            DataSet DsBoleta = ClManejoImpresion.Boleta(Convert.ToInt32(TxtId.Text), 2);
            Session["Boleta"] = DsBoleta;
            RadWinCenso.Title = "Censo / Muestro";
            RadWinCenso.NavigateUrl = "~/WeForms_Reportes/Wfrm_RepCenso.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadWinCenso.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
        }

        void ImgPolRepoblacion_Click(object sender, ImageClickEventArgs e)
        {
            if (ClPoligono.Existe_Poligono_Repoblacion(Convert.ToInt32(TxtId.Text), 2) == 0)
            {
                DivErrPoligonoPrint.Visible = true;
                LblErrPoligono.Text = "Aún no se ha cargado el poligono repoblación";
            }
            else
            {
                String js = "window.open('Wfrm_PoligonoMapa.aspx?ImmobilienId=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("0", true)) + "&identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtId.Text, true)) + "&typbericht=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&processus=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("5", true)) + "', '_blank');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
            }
        }

        void GrdInmueblePol_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DivErrPoligonoPrint.Visible = false;
            if (e.CommandName == "CmdPolFinca")
            {
                if (ClPoligono.Existe_Poligono_Inmueble(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"])) == 0)
                {
                    DivErrPoligonoPrint.Visible = true;
                    LblErrPoligono.Text = "Este finca aún no se ha cargado su poligono";
                }
                else
                {
                    String js = "window.open('Wfrm_PoligonoMapa.aspx?ImmobilienId=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString(), true)) + "&typbericht=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "&processus=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("1", true)) + "', '_blank');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
                }
            }
            else if (e.CommandName == "PolFincaBosque")
            {
                if (ClPoligono.Existe_Poligono_AreaBosque(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]), Convert.ToInt32(TxtId.Text), 2) == 0)
                {
                    DivErrPoligonoPrint.Visible = true;
                    LblErrPoligono.Text = "Este finca aún no se ha cargado su poligono de área de bosque";
                }
                else
                {
                    String js = "window.open('Wfrm_PoligonoMapa.aspx?ImmobilienId=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString(), true)) + "&identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtId.Text, true)) + "&typbericht=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&processus=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "', '_blank');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
                }
            }
            else if (e.CommandName == "PolFincaIntervenir")
            {
                if (ClPoligono.Existe_Poligono_AreaIntervenir(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]), Convert.ToInt32(TxtId.Text), 2) == 0)
                {
                    DivErrPoligonoPrint.Visible = true;
                    LblErrPoligono.Text = "Este finca aún no se ha cargado su poligono de área a intervenir";
                }
                else
                {
                    String js = "window.open('Wfrm_PoligonoMapa.aspx?ImmobilienId=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString(), true)) + "&identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtId.Text, true)) + "&typbericht=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&processus=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("3", true)) + "', '_blank');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
                }
            }
            else if (e.CommandName == "PolFincaProteccion")
            {
                if (ClPoligono.Existe_Poligono_AreaProteccion(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"]), Convert.ToInt32(TxtId.Text), 2) == 0)
                {
                    DivErrPoligonoPrint.Visible = true;
                    LblErrPoligono.Text = "Este finca aún no se ha cargado su poligono de área de protección";
                }
                else
                {
                    String js = "window.open('Wfrm_PoligonoMapa.aspx?ImmobilienId=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["InmuebleId"].ToString(), true)) + "&identificateur=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(TxtId.Text, true)) + "&typbericht=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("2", true)) + "&processus=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt("4", true)) + "', '_blank');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Open Signature.aspx", js, true);
                }
            }
        }

        void GrdAnexoMapaRonda_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de rondas corta fuegos perimetrales e intermedias dentro del area de compromiso de repoblacion forestal";
                int AsignacionId = Convert.ToInt32(TxtId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaRonda"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaUbicacion_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de ubicación del área a aprovechar, caminos.";
                int AsignacionId = Convert.ToInt32(TxtId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaUbicacion"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaPendiente_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de pendientes.";
                int AsignacionId = Convert.ToInt32(TxtId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaPendiente"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoMapaUsoActual_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Mapa de uso actual y recursos hidricos";
                int AsignacionId = Convert.ToInt32(TxtId.Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoMapaUsoActual"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdAnexoCroquia_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "CmdVer")
            {
                RadVerAnexo.Title = "Croquis de acceso a la finca desde el casco municipal";
                int AsignacionId = Convert.ToInt32(TxtId    .Text);
                //string PathArchivo = Server.MapPath(".") + @"\Archivos\Anexos\Croquis\" + AsignacionId + @"\" + f.FileName;
                string PathArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PathAnexoCroquis"].ToString();
                RadVerAnexo.NavigateUrl = "~/WebContenedor/Wfrm_VerAnexo.aspx?route=" + HttpUtility.UrlEncode(ClUtilitarios.Encrypt(PathArchivo, true)) + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "function f(){$find('" + RadVerAnexo.ClientID + "').show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
        }

        void GrdInmueblePol_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClManejo.GetFincaPlanManejoPol(Convert.ToInt32(TxtId.Text)), GrdInmueblePol);
        }

        void GrdAnexoMapaRonda_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaRonda\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaRonda"].NewRow();
                    item["NombreAnexoMapaRonda"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaRonda"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaRonda"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaRonda"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaRonda, "AnexoMapaRonda");
            }
        }

        void GrdAnexoMapaUbicacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUbicacion\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaUbicacion"].NewRow();
                    item["NombreAnexoMapaUbicacion"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaUbicacion"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaUbicacion"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaUbicacion"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaUbicacion, "AnexoMapaUbicacion");
            }
        }

        void GrdAnexoMapaPendiente_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaPendiente\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoMapaPendiente"].NewRow();
                    item["NombreAnexoMapaPendiente"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaPendiente"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoMapaPendiente"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoMapaPendiente"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaPendiente, "AnexoMapaPendiente");
            }
        }

        void GrdAnexoMapaUsoActual_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\AnexosPM\MapaUsoActual\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoUsoActual"].NewRow();
                    item["NombreAnexoMapaUsoActual"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoMapaUsoActual"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoUsoActual"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoUsoActual"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoMapaUsoActual, "AnexoUsoActual");
            }
        }

        void GrdAnexoCroquia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int AsignacionId = Convert.ToInt32(TxtId.Text);
            string PathArchivo = Server.MapPath(".") + @"\Archivos\AnexosPM\Croquis\" + AsignacionId;
            if (Directory.Exists(PathArchivo))
            {
                DirectoryInfo directory = new DirectoryInfo(PathArchivo);
                FileInfo[] files = directory.GetFiles("*.*");
                DirectoryInfo[] directories = directory.GetDirectories();
                for (int i = 0; i < files.Length; i++)
                {
                    DataRow item = DsAnexos.Tables["AnexoCroquis"].NewRow();
                    item["NombreAnexoCroquis"] = ((FileInfo)files[i]).Name;
                    item["PathAnexoCroquis"] = ((FileInfo)files[i]).FullName;
                    DsAnexos.Tables["AnexoCroquis"].Rows.Add(item);
                }
                if (DsAnexos.Tables["AnexoCroquis"].Rows.Count > 0)
                    ClUtilitarios.LlenaGridDataSet(DsAnexos, GrdAnexoCroquia, "AnexoCroquis");
            }
        }
    }
}