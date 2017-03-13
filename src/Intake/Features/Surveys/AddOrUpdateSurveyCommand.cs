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
    public class AddOrUpdateSurveyCommand
    {
        public class AddOrUpdateSurveyRequest : IRequest<AddOrUpdateSurveyResponse>
        {
            public SurveyApiModel Survey { get; set; }
        }

        public class AddOrUpdateSurveyResponse
        {

        }

        public class AddOrUpdateSurveyHandler : IAsyncRequestHandler<AddOrUpdateSurveyRequest, AddOrUpdateSurveyResponse>
        {
            public AddOrUpdateSurveyHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateSurveyResponse> Handle(AddOrUpdateSurveyRequest request)
            {
                var entity = await _dataContext.Surveys
                    .SingleOrDefaultAsync(x => x.Id == request.Survey.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Surveys.Add(entity = new Survey());
                entity.Name = request.Survey.Name;
                await _dataContext.SaveChangesAsync().ContinueWith((e) =>
                {
                    var a = e;
                });

                return new AddOrUpdateSurveyResponse()
                {

                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
