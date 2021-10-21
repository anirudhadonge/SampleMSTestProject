using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class DatabaseGetters
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DataTable GetDataTable(string query , string connectionStringName)
        {
            SqlConnection sqlConnection = new SqlConnection(new DataBaseConnection().GetConfigurationValue(connectionStringName));

            try
            {
                sqlConnection.Open();
                log.Info("Db Query " + query);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandTimeout = 60;
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                return dataTable;

            } catch(Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
            return null;
        }
    }
}
