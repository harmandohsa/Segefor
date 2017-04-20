using Excel;
using SEGEFOR.Clases;
using SEGEFOR.Data_Set;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_Poligno_Region : System.Web.UI.Page
    {
        DataSet resultXls = new DataSet();
        Ds_Temporales Ds_Temporal = new Ds_Temporales();
        Cl_Utilitarios ClUtilitarios;
        Cl_Xml ClXml;
        Cl_Poligono ClPoligono;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            ClXml = new Cl_Xml();
            ClPoligono = new Cl_Poligono();

            BtnCargar.Click += BtnCargar_Click;
            BtnGrabar.Click += BtnGrabar_Click;
            GrdPoligono.NeedDataSource += GrdPoligono_NeedDataSource;
            LblTitConfirmacion.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["region"].ToString()), true);
            LblRegionId.Text = ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["regionid"].ToString()), true);
        }

        void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (GrdPoligono.Items.Count > 0)
            {
                XmlDocument iInformacionPol = ClXml.CrearDocumentoXML("Poligonos");
                XmlNode iElementoPoligono = iInformacionPol.CreateElement("Puntos");

                for (int i = 0; i < GrdPoligono.Items.Count; i++)
                {
                    XmlNode iElementoDetalle = iInformacionPol.CreateElement("Item");
                    ClXml.AgregarAtributo("Id", 1, iElementoDetalle);
                    ClXml.AgregarAtributo("X", GrdPoligono.Items[i].OwnerTableView.DataKeyValues[i]["X"], iElementoDetalle);
                    ClXml.AgregarAtributo("Y", GrdPoligono.Items[i].OwnerTableView.DataKeyValues[i]["Y"], iElementoDetalle);
                    iElementoPoligono.AppendChild(iElementoDetalle);
                }
                iInformacionPol.ChildNodes[1].AppendChild(iElementoPoligono);
                //String iPoligonoGML = "";
                //string ErrorMapa = "";
                int RegionId = Convert.ToInt32(LblRegionId.Text);
                //if (ClPoligono.actualizar_poligonos_region(iInformacionPol, ref RegionId, ref iPoligonoGML, Convert.ToInt32(Session["UsuarioId"]), ref ErrorMapa))
                //{

                //}
                //else
                //{
                //}
            }
        }

        void BtnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Stream stream = UploadPolFinca.UploadedFiles[0].InputStream;
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.IsFirstRowAsColumnNames = true;
                resultXls = excelReader.AsDataSet();

                Ds_Temporal.Tables["Dt_Poligono_Region"].Clear();
                foreach (DataRow iDtRow in resultXls.Tables[0].Rows)
                {
                    if (iDtRow["X"].ToString() != "")
                    {
                        DataRow rowNew = Ds_Temporal.Tables["Dt_Poligono_Region"].NewRow();
                        rowNew["X"] = iDtRow["X"];
                        rowNew["Y"] = iDtRow["Y"];
                        Ds_Temporal.Tables["Dt_Poligono_Region"].Rows.Add(rowNew);
                    }
                }
                GrdPoligono.Rebind();
            }
            catch (Exception ex)
            {
                String iM = ex.Message;
            }
        }

        void GrdPoligono_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Ds_Temporal.Tables["Dt_Poligono_Region"].Rows.Count > 0)
            {
                ClUtilitarios.LlenaGridDt(Ds_Temporal, GrdPoligono, "Dt_Poligono_Region");
            }
        }
    }
}