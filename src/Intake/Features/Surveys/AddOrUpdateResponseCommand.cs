using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class AddOrUpdateResponseCommand
    {
        public class AddOrUpdateResponseRequest : IRequest<AddOrUpdateResponseResponse>
        {
            public ResponseApiModel Response { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateResponseResponse { }

        public class AddOrUpdateResponseHandler : IAsyncRequestHandler<AddOrUpdateResponseRequest, AddOrUpdateResponseResponse>
        {
            public AddOrUpdateResponseHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateResponseResponse> Handle(AddOrUpdateResponseRequest request)
            {
                var entity = await _context.Responses
                    .SingleOrDefaultAsync(x => x.Id == request.Response.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Responses.Add(entity = new Response());
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateResponseResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}
