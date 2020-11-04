using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class UserAdded
    {
        public Guid Id { get; set; }
        public User User { get; set; }

    }
}
