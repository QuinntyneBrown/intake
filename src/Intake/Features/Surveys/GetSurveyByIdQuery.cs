using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetSurveyByIdQuery
    {
        public class GetSurveyByIdRequest : IRequest<GetSurveyByIdResponse> { 
			public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetSurveyByIdResponse
        {
            public SurveyApiModel Survey { get; set; } 
		}

        public class GetSurveyByIdHandler : IAsyncRequestHandler<GetSurveyByIdRequest, GetSurveyByIdResponse>
        {
            public GetSurveyByIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveyByIdResponse> Handle(GetSurveyByIdRequest request)
            {                
                return new GetSurveyByIdResponse()
                {
                    Survey = SurveyApiModel.FromSurvey(await _context.Surveys.FindAsync(request.Id))
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
