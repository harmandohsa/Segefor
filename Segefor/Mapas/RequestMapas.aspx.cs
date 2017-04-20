using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using System.Text;
using System.IO;
using LibreriaPinpep;

namespace SEGEFOR.Mapas
{
    public partial class RequestMapas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsProyecto iProyecto = new clsProyecto();
            iProyecto.Id = Request["id"];
            DataSet iDtDatos = iProyecto.PuntosPoligonos;
            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {
                LiteralControl iTablaDatos = Crear_Tabla_Puntos(iDtDatos.Tables[0]);

                Response.Write(RenderControl(iTablaDatos));
            }
        }

        private string RenderControl(Control ctrl)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            ctrl.RenderControl(hw);
            return sb.ToString();
        }

        LiteralControl Crear_Tabla_Puntos(DataTable pDatos)
        {
            StringBuilder iSb = new StringBuilder();

            /* Etiqueta de inicio */
            iSb.Append("<table cellpadding='0' cellspacing='0' class='tPuntos' >");

            /* Generacion de columnas. Se omite siempre las primeras dos, una para el RID y otra PARA el MIDX */
            iSb.Append("<thead><tr>");
            int iColCount = 0;
            foreach (DataColumn iCol in pDatos.Columns)
            {
                if (iColCount > 1)
                {
                    iSb.Append("<th>");
                    iSb.Append(iCol.ColumnName.ToString());
                    iSb.Append("</th>");
                }
                iColCount++;
            }
            iSb.Append("</tr></thead>");

            /* Genera las filas */
            iSb.Append("<tbody>");
            int id = 0;
            int iNumPuntos = 0;
            foreach (DataRow iRow in pDatos.Rows)
            {
                iSb.Append("<tr>");
                iColCount = 0;
                foreach (Object iCelda in iRow.ItemArray)
                {
                    if (iColCount > 2)
                    {
                        iSb.AppendFormat("<td>{0}</td>", iCelda);
                    }
                    else if (iColCount == 1)
                    {
                        iNumPuntos = clsBase.intNoNull(iCelda);
                    }
                    else if (iColCount == 2)            /* Coloca un merge para la columna del ID del polígono */
                    {
                        if ((id == 0) || (id != clsBase.intNoNull(iCelda)))
                        {
                            id = clsBase.intNoNull(iCelda);
                            iSb.AppendFormat("<td rowspan='{0}'>{1}</td>", iNumPuntos, iCelda);
                        }
                    }
                    iColCount++;
                }
                iSb.Append("</tr>");
            }

            iSb.Append("</tbody></table>");

            LiteralControl iLC = new LiteralControl(iSb.ToString());
            return iLC;
        }
    }
}