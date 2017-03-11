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
        public class GetResponsesRequest : IRequest<GetResponsesResponse> { }

        public class GetResponsesResponse
        {
            public ICollection<ResponseApiModel> Responses { get; set; } = new HashSet<ResponseApiModel>();
        }

        public class GetResponsesHandler : IAsyncRequestHandler<GetResponsesRequest, GetResponsesResponse>
        {
            public GetResponsesHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetResponsesResponse> Handle(GetResponsesRequest request)
            {
                var responses = await _dataContext.Responses
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetResponsesResponse()
                {
                    Responses = responses.Select(x => ResponseApiModel.FromResponse(x)).ToList()
                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
