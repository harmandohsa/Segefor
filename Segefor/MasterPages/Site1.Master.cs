using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.MasterPages
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        Cl_Usuario Clusuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Clusuario = new Cl_Usuario();
            LnkSalir.Click += LnkSalir_Click;       
        }

        void LnkSalir_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] != null)
                Clusuario.Actualiza_SalidaSis(Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(Session["CorrIng"]));
            Session["UsuarioId"] = null;
            Session["CorrIng"] = null;
            Session["intentos"] = null;
            Response.Redirect("~/Wfrm_Login.aspx");
        }
    }
}