using Common.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public interface IEventsDatabase
    {
        void TestEntry(UserAdded ua);
    }
}
