using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEGEFOR
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static MAPS[] BindMapPoints(string name, string name1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<MAPS> lstFencingCircle = new List<MAPS>();
            try
            {

                dt.Columns.Add("Latitude");
                dt.Columns.Add("Longitude");
                dt.Rows.Add("14.6024595263", "-90.3413091974");
                dt.Rows.Add("14.6034629969", "-90.3414106076");
                dt.Rows.Add("14.6078011922", "-90.3401542519");
                dt.Rows.Add("14.6088475707", "-90.3368858258");
                dt.Rows.Add("14.604717034", "-90.3377615063");
                dt.Rows.Add("14.6037954647", "-90.3384398257");
                dt.Rows.Add("14.6033980053", "-90.3388578318");
                dt.Rows.Add("14.6028021398", "-90.3400000371");
                dt.Rows.Add("14.6025948001", "-90.3408263767");
                dt.Rows.Add("14.6024595263", "-90.3413091974");
                dt.Rows.Add("14.6024595263", "-90.3413091974");
                foreach (DataRow dtrow in dt.Rows)
                {
                    MAPS objMAPS = new MAPS();
                    objMAPS.Latitude = dtrow["Latitude"].ToString();
                    objMAPS.Longitude = dtrow["Longitude"].ToString();
                    lstFencingCircle.Add(objMAPS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return lstFencingCircle.ToArray();
        }

        public class MAPS
        {
            public string Latitude;
            public string Longitude;
        }
    }
}