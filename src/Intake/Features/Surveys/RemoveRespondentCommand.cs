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
            public int? TenantId { get; set; }
        }

        public class RemoveRespondentResponse { }

        public class RemoveRespondentHandler : IAsyncRequestHandler<RemoveRespondentRequest, RemoveRespondentResponse>
        {
            public RemoveRespondentHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveRespondentResponse> Handle(RemoveRespondentRequest request)
            {
                var respondent = await _context.Respondents.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                respondent.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveRespondentResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}
