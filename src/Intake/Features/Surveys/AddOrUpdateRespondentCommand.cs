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
    public class AddOrUpdateRespondentCommand
    {
        public class AddOrUpdateRespondentRequest : IRequest<AddOrUpdateRespondentResponse>
        {
            public RespondentApiModel Respondent { get; set; }
        }

        public class AddOrUpdateRespondentResponse
        {

        }

        public class AddOrUpdateRespondentHandler : IAsyncRequestHandler<AddOrUpdateRespondentRequest, AddOrUpdateRespondentResponse>
        {
            public AddOrUpdateRespondentHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateRespondentResponse> Handle(AddOrUpdateRespondentRequest request)
            {
                var entity = await _dataContext.Respondents
                    .SingleOrDefaultAsync(x => x.Id == request.Respondent.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Respondents.Add(entity = new Respondent());
                entity.Name = request.Respondent.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateRespondentResponse()
                {

                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
