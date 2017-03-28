using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetResponsesQuery
    {
        public class GetResponsesRequest : IRequest<GetResponsesResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetResponsesResponse
        {
            public ICollection<ResponseApiModel> Responses { get; set; } = new HashSet<ResponseApiModel>();
        }

        public class GetResponsesHandler : IAsyncRequestHandler<GetResponsesRequest, GetResponsesResponse>
        {
            public GetResponsesHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetResponsesResponse> Handle(GetResponsesRequest request)
            {
                var responses = await _context.Responses
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetResponsesResponse()
                {
                    Responses = responses.Select(x => ResponseApiModel.FromResponse(x)).ToList()
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
