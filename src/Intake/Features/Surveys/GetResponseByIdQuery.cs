using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetResponseByIdQuery
    {
        public class GetResponseByIdRequest : IRequest<GetResponseByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetResponseByIdResponse
        {
            public ResponseApiModel Response { get; set; } 
        }

        public class GetResponseByIdHandler : IAsyncRequestHandler<GetResponseByIdRequest, GetResponseByIdResponse>
        {
            public GetResponseByIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetResponseByIdResponse> Handle(GetResponseByIdRequest request)
            {                
                return new GetResponseByIdResponse()
                {
                    Response = ResponseApiModel.FromResponse(await _context.Responses.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
