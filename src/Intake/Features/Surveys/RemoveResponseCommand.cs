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
    public class RemoveResponseCommand
    {
        public class RemoveResponseRequest : IRequest<RemoveResponseResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveResponseResponse { }

        public class RemoveResponseHandler : IAsyncRequestHandler<RemoveResponseRequest, RemoveResponseResponse>
        {
            public RemoveResponseHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveResponseResponse> Handle(RemoveResponseRequest request)
            {
                var response = await _context.Responses.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                response.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveResponseResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}
