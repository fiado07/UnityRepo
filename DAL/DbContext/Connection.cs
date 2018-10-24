using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL.DbContext
{
   public  class Connection
    {

        private  static string GetConnection()
        {

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            //' Declaration the EntityConnectionStringBuilder. 
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            //' Set the properties for the data source. 
            sqlBuilder.DataSource = @".\sqlexpress";
            sqlBuilder.InitialCatalog = "escola";


            //' Turn off integrated security.
            //'sqlBuilder.UserID = ""
            //'sqlBuilder.Password = ""
            sqlBuilder.IntegratedSecurity = true;


            //' Build the SqlConnection connection string. 
            String providerString = sqlBuilder.ToString();

            //'Set the provider name. 
            entityBuilder.Provider = "System.Data.SqlClient";

            //' Set the provider-specific connection string. 
            entityBuilder.ProviderConnectionString = providerString;

            //' Set the Metadata location. 
            entityBuilder.Metadata = "res://*/DbContext.EscolaDB.csdl|res://*/DbContext.EscolaDB.ssdl|res://*/DbContext.EscolaDB.msl";

            return entityBuilder.ToString();

        }


        public Entities GetEntity()
        {

            return new Entities(GetConnection()); 


        }



    }
}
