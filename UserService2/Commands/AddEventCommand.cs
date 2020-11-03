using Common.Events;
using Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService2.Commands
{
    public class AddEventCommand : IRequest<Unit>
    {
        public AddEventCommand(UserAdded command)
        {
            Command = command;
        }

        public UserAdded Command { get; set; }




    }
}
