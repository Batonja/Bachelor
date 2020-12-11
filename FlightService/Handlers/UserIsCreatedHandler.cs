﻿using Business;
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
        IFlightBusiness _flightBusiness; 

        public UserIsCreatedHandler(IFlightBusiness flightBusiness)
        {
            _flightBusiness = flightBusiness;
        }

        
        public async Task<int> Handle(UserIsCreatedQuery request, CancellationToken cancellationToken)
        {
            return await _flightBusiness.UserIsCreated(request.guidOfEvent);
        }

        
    }
}
