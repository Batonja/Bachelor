using Common.Events;
using DatabaseLayer.DataAccess;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    
    public class EventsDatabase : IEventsDatabase
    {
        DocumentStore store = DataStore.getStore();

        public void TestEntry(UserAdded ua)
        {
            using (var session = store.OpenSession())
            {
                session.Events.Append(new Guid(), ua);
                session.SaveChanges();
            }


        }



    }
}
