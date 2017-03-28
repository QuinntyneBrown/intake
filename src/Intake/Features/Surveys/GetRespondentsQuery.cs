using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetRespondentsQuery
    {
        public class GetRespondentsRequest : IRequest<GetRespondentsResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetRespondentsResponse
        {
            public ICollection<RespondentApiModel> Respondents { get; set; } = new HashSet<RespondentApiModel>();
        }

        public class GetRespondentsHandler : IAsyncRequestHandler<GetRespondentsRequest, GetRespondentsResponse>
        {
            public GetRespondentsHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetRespondentsResponse> Handle(GetRespondentsRequest request)
            {
                var respondents = await _context.Respondents
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetRespondentsResponse()
                {
                    Respondents = respondents.Select(x => RespondentApiModel.FromRespondent(x)).ToList()
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
