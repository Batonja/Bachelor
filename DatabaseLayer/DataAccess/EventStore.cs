using Common.Events;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.DataAccess
{
    public class EventStore
    {
        public static DocumentStore getStore()
        {
            DocumentStore store = DocumentStore.
                For(_ =>
                {
                    _.Connection("Server=127.0.0.1; Port=5432 ;Database=eventsDatabase;User Id=postgres;Password=atotarho12");
                    _.DatabaseSchemaName = "events";
                    _.Events.AddEventType(typeof(UserAdded));
                });
            return store;
        }
    }
}
