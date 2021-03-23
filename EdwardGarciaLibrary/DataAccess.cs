using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdwardGarciaLibrary
{
    using System.Data.SqlClient;
    using System.Configuration;

    public class DataAccess
    {
        protected static string constring = ConfigurationManager.ConnectionStrings["EdwardGarciaDB"].ConnectionString;
        // <add name="CapstoneDemo" connectionString="Data Source=DESKTOP-50S5SLH\SQLEXPRESS02;Initial Catalog=CapstoneDemo;Integrated Security=True" />
        //Data Source=sql5101.site4now.net;Initial Catalog=DB_A6D740_CapstoneBGSTrial;User ID=DB_A6D740_CapstoneBGSTrial_admin;Password=***********
        //<add name = "CapstoneDemo" connectionString="Data Source=mssql-17982-0.cloudclusters.net,17982;Initial Catalog=CapstoneDemo;User ID=Edward;Password=CapstoneBGS123" />

        protected static SqlConnection con;
        protected static SqlCommand cmd;
        protected static SqlDataAdapter da;
        protected static SqlDataReader dr;
        protected static SqlTransaction trx;
    }
}
