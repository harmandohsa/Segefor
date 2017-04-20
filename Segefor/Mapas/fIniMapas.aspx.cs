using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR.Mapas
{
    public partial class fIniMapas : System.Web.UI.Page
    {
        public int poligonoNo;
        public int Noproyecto;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnelimina_Click(object sender, EventArgs e)
        {
            //if (Txtnopoligono.Value != "")
            //{
            //    clsProyecto iProyecto = new clsProyecto();
            //    iProyecto.Id = clsBanco.intNoNull(txtPoligono.Value);
            //    iProyecto.NoPoligonos = clsBanco.intNoNull(Txtnopoligono.Value);
            //    if (iProyecto.EliminarPoligonos())
            //    {
            //        String llamados = "eliminaPol();";
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), "elimina", llamados, true);                    
            //    }
            //    else
            //    {
            //        //Response.Write("{\"Mensaje\":\"" + iProyecto.Error.Descripcion + "\"}");
            //    }                
            //}
            //else
            //{
            //    String funcion5 = "respuesta2();";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "respuesta", funcion5, true);                                              
            // }            
            //Txtnopoligono.Value = "";
            // mapas();
            // informacion();
            // puntos_poligono();            
        }
    }
}