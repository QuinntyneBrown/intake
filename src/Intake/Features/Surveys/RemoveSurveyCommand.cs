using MediatR;
using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class RemoveSurveyCommand
    {
        public class RemoveSurveyRequest : IRequest<RemoveSurveyResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveSurveyResponse { }

        public class RemoveSurveyHandler : IAsyncRequestHandler<RemoveSurveyRequest, RemoveSurveyResponse>
        {
            public RemoveSurveyHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveSurveyResponse> Handle(RemoveSurveyRequest request)
            {
                var survey = await _dataContext.Surveys.FindAsync(request.Id);
                survey.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveSurveyResponse();
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
