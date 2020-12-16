using Common.Events;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class EventsBusiness : IEventsBusiness
    {
        IEventsDatabase _eventsDatabase;

        public EventsBusiness(IEventsDatabase eventsDatabase)
        {
            _eventsDatabase = eventsDatabase;
        }

        public void TestEntry(UserAdded ua)
        {
            _eventsDatabase.TestEntry(ua);
        }
    }
}
