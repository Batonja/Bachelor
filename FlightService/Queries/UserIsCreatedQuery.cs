using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Queries
{
    public class UserIsCreatedQuery : IRequest<int>
    {
        public Guid guidOfEvent;

        public UserIsCreatedQuery(Guid guidOfEvent)
        {
            this.guidOfEvent = guidOfEvent;
        }
    }
}
