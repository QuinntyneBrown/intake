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
		}

        public class GetSurveyByIdResponse
        {
            public SurveyApiModel Survey { get; set; } 
		}

        public class GetSurveyByIdHandler : IAsyncRequestHandler<GetSurveyByIdRequest, GetSurveyByIdResponse>
        {
            public GetSurveyByIdHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetSurveyByIdResponse> Handle(GetSurveyByIdRequest request)
            {                
                return new GetSurveyByIdResponse()
                {
                    Survey = SurveyApiModel.FromSurvey(await _dataContext.Surveys.FindAsync(request.Id))
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
