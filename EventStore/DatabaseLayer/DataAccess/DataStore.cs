using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.DataAccess
{
    public static class DataStore
    {
        public static DocumentStore getStore()
        {
            DocumentStore store = DocumentStore.
                For(_connectionString => { _connectionString
                 .Connection("Server=127.0.0.1; Port=5432 ;Database=eventsDatabase;User Id=postgres;Password=atotarho12");
                    _connectionString.DatabaseSchemaName = "events"; } );
            return store;
        }
    }
}
