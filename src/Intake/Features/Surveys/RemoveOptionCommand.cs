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
    public class RemoveOptionCommand
    {
        public class RemoveOptionRequest : IRequest<RemoveOptionResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveOptionResponse { }

        public class RemoveOptionHandler : IAsyncRequestHandler<RemoveOptionRequest, RemoveOptionResponse>
        {
            public RemoveOptionHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveOptionResponse> Handle(RemoveOptionRequest request)
            {
                var option = await _context.Options.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                option.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveOptionResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}
