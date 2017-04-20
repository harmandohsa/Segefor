using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;

namespace SEGEFOR.Mapas
{
    public partial class MenuMapas : System.Web.UI.Page
    {
        public int poligonoNo;
        public int Noproyecto;
        Cl_Poligono Clpoligono;
        protected void Page_Load(object sender, EventArgs e)
        {
            //verpol.Click +=new EventHandler(verpol_Click);
            Clpoligono = new Cl_Poligono();






            lstpoligonos.SelectedIndexChanged += new EventHandler(lstpoligonos_SelectedIndexChanged);
            BtnVerPuntos.Click += new EventHandler(BtnVerPuntos_Click);
            BtnCargaExcel.Click += new EventHandler(BtnCargaExcel_Click);
            this.btnelimina.Click += new EventHandler(btnelimina_Click);

            if (!IsPostBack)
            {
                this.txtPoligono.Value = Request.QueryString["Id"];
                ver_mapas();
                mapas();
                informacion();
                puntos_poligono();
            }
        }

        protected void ver_mapas()
        {
            String llamado = "pagina();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "menu9", llamado, true);

        }

        //	Procedimiento que elimina polígonos uno por uno de acuerdo como se seleccionen

        protected void btnelimina_Click(object sender, EventArgs e)
        {
            if (this.txtPoligono.Value == "" || this.Txtnopoligono.Value == "")
            {
                pMensaje("Debe Elegir un poligono para eliminar");
            }
            if (Txtnopoligono.Value != "")
            {

                //    iProyecto.Id = clsBanco.intNoNull(txtPoligono.Value);
                //    iProyecto.NoPoligonos = clsBanco.intNoNull(Txtnopoligono.Value);
                if (Clpoligono.EliminarPoligonos(int.Parse(txtPoligono.Value), int.Parse(Txtnopoligono.Value)))
                {
                    //String llamados = "eliminaPol();";
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "elimina", llamados, true);
                    pMensaje("Poligono Eliminado Satisfactoriamente " + this.txtPoligono.Value + ", " + this.Txtnopoligono.Value);
                }
                else
                {
                    pMensaje("No Se pudo Eliminar El Poligono " + this.txtPoligono.Value + ", " + this.Txtnopoligono.Value);
                    //Response.Write("{\"Mensaje\":\"" + iProyecto.Error.Descripcion + "\"}");
                }
                //}
                //else
                //{
                //    String funcion5 = "respuesta2();";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "respuesta", funcion5, true);                                              
            }
            Txtnopoligono.Value = "";
            mapas();
            informacion();
            puntos_poligono();
        }


        protected void lstpoligonos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                this.Txtnopoligono.Value = lstpoligonos.SelectedItem.Value.ToString();
                mapas();

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }


            finally
            {

            }
        }

        protected void mapas()
        {


            String funcion = "btnPoligono_click();";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "menu", funcion, true);

            //ClientScript.RegisterStartupScript(GetType(), "menu", funcion, true);

        }

        protected void BtnVerPuntos_Click(object sender, EventArgs e)
        {
            // ver_mapas();
            mapas();
            informacion();
            puntos_poligono();
        }

        protected void verpol_Click(object sender, EventArgs e)
        {
            mapas();
            informacion();
            puntos_poligono();
        }

        protected void btnimprimir_Click(object sender, EventArgs e)
        {
            Imprimir_poligono();
        }


        private void Imprimir_poligono()
        {
            if (Txtnopoligono.Value != "0")
            {
                Noproyecto = Convert.ToInt32(txtPoligono.Value);
                poligonoNo = Convert.ToInt32(Txtnopoligono.Value);

                //-	Manda a llamar el formulario de impresión del polígono
                String strScript;
                strScript = "window.open('ImpresionPoligono.aspx?dato1=" + Noproyecto + "&dato2=" + poligonoNo + "','_blank')";
                strScript += "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Poligonos", strScript, true);
                Response.Write("</script>");
                mapas();
            }
            else
            {

                String funcion1 = "respuesta();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "menu1", funcion1, true);
                mapas();
            }
        }




        protected void btnimprimirpuntos_Click(object sender, EventArgs e)
        {
            if (Txtnopoligono.Value != "")
            {
                Noproyecto = Convert.ToInt32(txtPoligono.Value);
                poligonoNo = Convert.ToInt32(Txtnopoligono.Value);

                //	Manda a llamar el formulario de impresión de los puntos del polígono

                String strScript;
                strScript = "window.open('WfrmimpresionPuntos.aspx?dato1=" + Noproyecto + "&dato2=" + poligonoNo + "','_blank')";
                strScript += "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Poligonos", strScript, true);
                Response.Write("</script>");
                //mapas();
            }
            else
            {

                String funcion1 = "respuesta();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "menu1", funcion1, true);
                // mapas();
            }
        }



        void pMensaje(string Mensaje)
        {
            if (Mensaje != "")
            {
                RadWindowManager1.RadAlert(Mensaje, 330, 180, "Mapas", null);
                return;


            }

        }



        private void informacion()
        {
            int Id = Convert.ToInt32(this.txtPoligono.Value);

            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_Lee_poligonos", cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando.Parameters["@GestionId"].Value = Id;

            SqlDataAdapter da = new SqlDataAdapter(Comando);
            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);




            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {
                //string propietario = iDtDatos.Tables[0].Rows[0]["propietario"].ToString();
                //string expediente = iDtDatos.Tables[0].Rows[0]["expendiente"].ToString();
                //string faseproyecto = iDtDatos.Tables[0].Rows[0]["fase"].ToString();
                //string depto = iDtDatos.Tables[0].Rows[0]["depto"].ToString();
                //string muni = iDtDatos.Tables[0].Rows[0]["municipio"].ToString();
                //string totalarea = iDtDatos.Tables[1].Rows[0]["total"].ToString();

                //Lblexpediente.Text = "Expediente: " + expediente;
                //lblfase.Text = "Fase de " + faseproyecto;
                //Lbltitular.Text = "Titular del Proyecto: " + propietario;
                //lbldepto.Text = "Departamento: " + depto;
                //lblmuni.Text = "Municipio: " + muni;
                //lbltotalarea.Text = "Area Total del Proyecto: " + totalarea + " ha";

                //lstpoligonos.DataTextField = "area";
                //lstpoligonos.DataValueField = "id";


            }

            cn.Close();

            SqlConnection cn2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn2.Open();
            SqlCommand Comando2 = new SqlCommand("sp_Lee_poligonos2", cn);
            Comando2.CommandType = CommandType.StoredProcedure;
            Comando2.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando2.Parameters["@GestionId"].Value = Id;

            SqlDataAdapter da2 = new SqlDataAdapter(Comando2);
            DataSet iDtDatos2 = new DataSet();

            // Por último, rellenamos el DataSet
            da2.Fill(iDtDatos2);

            if ((iDtDatos2 != null) && (iDtDatos2.Tables.Count > 0) && (iDtDatos2.Tables[0].Rows.Count > 0))
            {
                lstpoligonos.DataTextField = "area";
                lstpoligonos.DataValueField = "id";
                lstpoligonos.DataSource = iDtDatos2;
                lstpoligonos.DataBind();
            }
            cn2.Close();
        }



        private void puntos_poligono()
        {


            int Id = Convert.ToInt32(this.txtPoligono.Value);

            /* Debe obtenerse una cadena de puntos x y separados cada punto por un espacio en blanco */
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnMapas"]);
            cn.Open();
            SqlCommand Comando = new SqlCommand("sp_obtener_puntos_poligonos", cn);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@GestionId", SqlDbType.Int, 1);
            Comando.Parameters["@GestionId"].Value = Id;

            SqlDataAdapter da = new SqlDataAdapter(Comando);
            DataSet iDtDatos = new DataSet();

            // Por último, rellenamos el DataSet
            da.Fill(iDtDatos);

            if ((iDtDatos != null) && (iDtDatos.Tables.Count > 0) && (iDtDatos.Tables[0].Rows.Count > 0))
            {
                GvistaPuntos.DataSource = iDtDatos;
                GvistaPuntos.DataBind();

                ///GvistaPuntos.Columns[0].Visible = false;
                GvistaPuntos.HeaderRow.Cells[0].Text = "Poligono No.";
                GvistaPuntos.HeaderRow.Cells[1].Text = "No.de Poligono";
                GvistaPuntos.HeaderRow.Cells[2].Text = "Punto";
                GvistaPuntos.HeaderRow.Cells[3].Text = "Coordenada X";
                GvistaPuntos.HeaderRow.Cells[4].Text = "Coordenada Y";
            }
        }



        protected void Btninformacion_Click(object sender, EventArgs e)
        {
            informacion();
        }

        protected void BtnPuntos_Click(object sender, EventArgs e)
        {
            puntos_poligono();
        }



        protected void BtnCargaExcel_Click(object sender, EventArgs e)
        {
            string Id = "";
            if (this.txtPoligono.Value == "")
            {

                pMensaje("Debe Grabar el Compromiso ");
                return;
            }
            Id = txtPoligono.Value;

            string url = "";
            url = "/CargaPuntosMapas.aspx?Id=" + Id;
            //string popupScript = "window.open('" + url + "', 'popup_window', 'left=100,top=100,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", popupScript, true);

            string popupScriptt = "<script languaje='javascript'>" + "window.open('" + url + "', 'left=100,top=100,resizable=yes');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopUpWindow", popupScriptt, false);

        }
    }
}