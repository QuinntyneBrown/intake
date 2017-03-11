using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetSurveysQuery
    {
        public class GetSurveysRequest : IRequest<GetSurveysResponse> { }

        public class GetSurveysResponse
        {
            public ICollection<SurveyApiModel> Surveys { get; set; } = new HashSet<SurveyApiModel>();
        }

        public class GetSurveysHandler : IAsyncRequestHandler<GetSurveysRequest, GetSurveysResponse>
        {
            public GetSurveysHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetSurveysResponse> Handle(GetSurveysRequest request)
            {
                var surveys = await _dataContext.Surveys
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetSurveysResponse()
                {
                    Surveys = surveys.Select(x => SurveyApiModel.FromSurvey(x)).ToList()
                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
