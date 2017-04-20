using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;

namespace SEGEFOR.WebContenedor
{
    public partial class Wfrm_Foto_Perfil : System.Web.UI.Page
    {
        Cl_Persona ClPersona;
        protected void Page_Load(object sender, EventArgs e)
        {



                ClPersona = new Cl_Persona();
                DataSet DsArchivo = new DataSet();
                DsArchivo = ClPersona.Foto_Perfil(Convert.ToInt32(Session["PersonaId"]));
                if (DsArchivo.Tables["DATOS"].Rows.Count != 0)
                {
                    Response.ContentType = DsArchivo.Tables["DATOS"].Rows[0]["ContentType"].ToString();
                    if (DsArchivo.Tables["DATOS"].Rows[0]["Foto"].ToString() != "")
                        Response.BinaryWrite((byte[])DsArchivo.Tables["DATOS"].Rows[0]["Foto"]);
                }
                
            
        }
    }
}