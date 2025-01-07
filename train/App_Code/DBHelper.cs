using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace train.App_Code
{
    public class DBHelper
    {

        private static string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\HP\Documents\Visual Studio 2012\Projects\train\traindb.mdb";

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(connStr);
        }
    }
}