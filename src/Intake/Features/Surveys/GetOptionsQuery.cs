using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetOptionsQuery
    {
        public class GetOptionsRequest : IRequest<GetOptionsResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetOptionsResponse
        {
            public ICollection<OptionApiModel> Options { get; set; } = new HashSet<OptionApiModel>();
        }

        public class GetOptionsHandler : IAsyncRequestHandler<GetOptionsRequest, GetOptionsResponse>
        {
            public GetOptionsHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetOptionsResponse> Handle(GetOptionsRequest request)
            {
                var options = await _context.Options
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetOptionsResponse()
                {
                    Options = options.Select(x => OptionApiModel.FromOption(x)).ToList()
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
