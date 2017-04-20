using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.WebContenedor
{
    public partial class Wfrm_VerAnexo : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUtilitarios = new Cl_Utilitarios();
            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] buffer = client.DownloadData(ClUtilitarios.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Route"].ToString()), true));

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }

        }
    }
}