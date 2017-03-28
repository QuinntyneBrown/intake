using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetRespondentByIdQuery
    {
        public class GetRespondentByIdRequest : IRequest<GetRespondentByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetRespondentByIdResponse
        {
            public RespondentApiModel Respondent { get; set; } 
        }

        public class GetRespondentByIdHandler : IAsyncRequestHandler<GetRespondentByIdRequest, GetRespondentByIdResponse>
        {
            public GetRespondentByIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetRespondentByIdResponse> Handle(GetRespondentByIdRequest request)
            {                
                return new GetRespondentByIdResponse()
                {
                    Respondent = RespondentApiModel.FromRespondent(await _context.Respondents.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
