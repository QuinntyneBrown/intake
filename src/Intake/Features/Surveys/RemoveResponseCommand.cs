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
        }

        public class RemoveResponseResponse { }

        public class RemoveResponseHandler : IAsyncRequestHandler<RemoveResponseRequest, RemoveResponseResponse>
        {
            public RemoveResponseHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveResponseResponse> Handle(RemoveResponseRequest request)
            {
                var response = await _dataContext.Responses.FindAsync(request.Id);
                response.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveResponseResponse();
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
