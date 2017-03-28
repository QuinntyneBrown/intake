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
    public class AddOrUpdateOptionCommand
    {
        public class AddOrUpdateOptionRequest : IRequest<AddOrUpdateOptionResponse>
        {
            public OptionApiModel Option { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateOptionResponse { }

        public class AddOrUpdateOptionHandler : IAsyncRequestHandler<AddOrUpdateOptionRequest, AddOrUpdateOptionResponse>
        {
            public AddOrUpdateOptionHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateOptionResponse> Handle(AddOrUpdateOptionRequest request)
            {
                var entity = await _context.Options
                    .SingleOrDefaultAsync(x => x.Id == request.Option.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Options.Add(entity = new Option());
                entity.Name = request.Option.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateOptionResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
