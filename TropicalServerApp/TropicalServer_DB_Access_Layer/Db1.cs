using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TropicalServerApp.TropicalServer_DB_Access_Layer
{
    public class Db1
    {
        SqlConnection Model1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString);

        public DataSet Show_data() {
            SqlCommand com = new SqlCommand("SELECT [TO].OrderTrackingNumber, [TO].OrderDate,"+
            "[TC].CustID, [TC].CustName,[TC].CustOfficeAddress1,"+
            "[TC].CustRouteNumber" +
            "FROM tblOrder[TO]" +
            "INNER JOIN tblCustomer[TC]" +
            "ON[TO].OrderCustomerNumber =[TC].CustNumber" +
            "WHERE[TO].OrderTrackingNumber IS NOT NULL; ",Model1);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds); //fill the data adapter with this dataset
            return ds;
        }
    }
}