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
        }

        public class RemoveOptionResponse { }

        public class RemoveOptionHandler : IAsyncRequestHandler<RemoveOptionRequest, RemoveOptionResponse>
        {
            public RemoveOptionHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveOptionResponse> Handle(RemoveOptionRequest request)
            {
                var option = await _dataContext.Options.FindAsync(request.Id);
                option.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveOptionResponse();
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
