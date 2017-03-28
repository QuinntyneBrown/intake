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
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateRespondentResponse { }

        public class AddOrUpdateRespondentHandler : IAsyncRequestHandler<AddOrUpdateRespondentRequest, AddOrUpdateRespondentResponse>
        {
            public AddOrUpdateRespondentHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateRespondentResponse> Handle(AddOrUpdateRespondentRequest request)
            {
                var entity = await _context.Respondents
                    .SingleOrDefaultAsync(x => x.Id == request.Respondent.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Respondents.Add(entity = new Respondent());
                
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateRespondentResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
