using BusinessLayer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService2.Commands;

namespace UserService2.Handlers
{
    public class AddEventHandler : IRequestHandler<AddEventCommand,Unit>
    {
        IUserBusiness _userBusiness;

        public AddEventHandler(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public async Task<Unit> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            await _userBusiness.AddEvent(request.Command);

            return Unit.Value;
            
        }
    }
}
