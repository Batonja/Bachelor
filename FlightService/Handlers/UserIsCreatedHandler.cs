using Business;
using FlightService.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightService.Handlers
{
    public class UserIsCreatedHandler : IRequestHandler<UserIsCreatedQuery,int>
    {
        ILuggageLocationBusiness _luggageLocationBusiness; 

        public UserIsCreatedHandler(ILuggageLocationBusiness luggageLocationBusiness)
        {
            _luggageLocationBusiness = luggageLocationBusiness;
        }

        
        public async Task<int> Handle(UserIsCreatedQuery request, CancellationToken cancellationToken)
        {
            return await _luggageLocationBusiness.UserIsCreated(request.guidOfEvent);
        }

        
    }
}
