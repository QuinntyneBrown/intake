using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetOptionByIdQuery
    {
        public class GetOptionByIdRequest : IRequest<GetOptionByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetOptionByIdResponse
        {
            public OptionApiModel Option { get; set; } 
        }

        public class GetOptionByIdHandler : IAsyncRequestHandler<GetOptionByIdRequest, GetOptionByIdResponse>
        {
            public GetOptionByIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetOptionByIdResponse> Handle(GetOptionByIdRequest request)
            {                
                return new GetOptionByIdResponse()
                {
                    Option = OptionApiModel.FromOption(await _context.Options.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
