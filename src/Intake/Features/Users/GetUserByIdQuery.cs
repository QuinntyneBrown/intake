using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Users
{
    public class GetUserByIdQuery
    {
        public class GetUserByIdRequest : IRequest<GetUserByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetUserByIdResponse
        {
            public UserApiModel User { get; set; } 
		}

        public class GetUserByIdHandler : IAsyncRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
        {
            public GetUserByIdHandler(IIntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request)
            {                
                return new GetUserByIdResponse()
                {
                    User = UserApiModel.FromUser(await _context.Users.FindAsync(request.Id))
                };
            }

            private readonly IIntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
