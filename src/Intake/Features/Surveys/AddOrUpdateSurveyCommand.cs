using MediatR;
using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class AddOrUpdateSurveyCommand
    {
        public class AddOrUpdateSurveyRequest : IRequest<AddOrUpdateSurveyResponse>
        {
            public SurveyApiModel Survey { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateSurveyResponse { }

        public class AddOrUpdateSurveyHandler : IAsyncRequestHandler<AddOrUpdateSurveyRequest, AddOrUpdateSurveyResponse>
        {
            public AddOrUpdateSurveyHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateSurveyResponse> Handle(AddOrUpdateSurveyRequest request)
            {
                var entity = await _context.Surveys
                    .SingleOrDefaultAsync(x => x.Id == request.Survey.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Surveys.Add(entity = new Survey());
                entity.Name = request.Survey.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateSurveyResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
