using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Microsoft.Owin;

namespace Intake.Features.Surveys
{
    public class GetSurveysQuery
    {
        public class GetSurveysRequest : IRequest<GetSurveysResponse> {
            public int? TenantId { get; set; }
        }

        public class GetSurveysResponse
        {
            public ICollection<SurveyApiModel> Surveys { get; set; } = new HashSet<SurveyApiModel>();            
        }

        public class GetSurveysHandler : IAsyncRequestHandler<GetSurveysRequest, GetSurveysResponse>
        {
            public GetSurveysHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveysResponse> Handle(GetSurveysRequest request)
            {
               var surveys = await _context.Surveys
                    .Where(x=>x.TenantId == request.TenantId)
                    .ToListAsync();

                return new GetSurveysResponse()
                {
                    Surveys = surveys.Select(x => SurveyApiModel.FromSurvey(x)).ToList()
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}