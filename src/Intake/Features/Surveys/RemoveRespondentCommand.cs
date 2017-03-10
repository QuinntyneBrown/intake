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
    public class RemoveRespondentCommand
    {
        public class RemoveRespondentRequest : IRequest<RemoveRespondentResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveRespondentResponse { }

        public class RemoveRespondentHandler : IAsyncRequestHandler<RemoveRespondentRequest, RemoveRespondentResponse>
        {
            public RemoveRespondentHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveRespondentResponse> Handle(RemoveRespondentRequest request)
            {
                var respondent = await _dataContext.Respondents.FindAsync(request.Id);
                respondent.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveRespondentResponse();
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
