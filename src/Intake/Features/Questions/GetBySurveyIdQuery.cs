using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Questions
{
    public class GetBySurveyIdQuery
    {
        public class GetBySurveyIdRequest : IRequest<GetBySurveyIdResponse>
        {
            public GetBySurveyIdRequest()
            {

            }
        }

        public class GetBySurveyIdResponse
        {
            public GetBySurveyIdResponse()
            {

            }
        }

        public class GetBySurveyIdHandler : IAsyncRequestHandler<GetBySurveyIdRequest, GetBySurveyIdResponse>
        {
            public GetBySurveyIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetBySurveyIdResponse> Handle(GetBySurveyIdRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
