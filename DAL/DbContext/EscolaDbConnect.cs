using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DbContext
{
    public partial class Entities : System.Data.Entity.DbContext
    {
        public Entities(string connectionString)
            : base(connectionString)
        {
        }

        public void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly Is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        }
    }
    
    
}
