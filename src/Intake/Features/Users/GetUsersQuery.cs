using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Users
{
    public class GetUsersQuery
    {
        public class GetUsersRequest : IRequest<GetUsersResponse> { }

        public class GetUsersResponse
        {
            public ICollection<UserApiModel> Users { get; set; } = new HashSet<UserApiModel>();
        }

        public class GetUsersHandler : IAsyncRequestHandler<GetUsersRequest, GetUsersResponse>
        {
            public GetUsersHandler(IIntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetUsersResponse> Handle(GetUsersRequest request)
            {
                var users = await _context.Users.ToListAsync();
                return new GetUsersResponse()
                {
                    Users = users.Select(x => UserApiModel.FromUser(x)).ToList()
                };
            }

            private readonly IIntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
