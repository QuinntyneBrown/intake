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
            public RemoveSurveyHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSurveyResponse> Handle(RemoveSurveyRequest request)
            {
                var survey = await _context.Surveys.FindAsync(request.Id);
                survey.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSurveyResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}
