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
    public class AddOrUpdateResponseCommand
    {
        public class AddOrUpdateResponseRequest : IRequest<AddOrUpdateResponseResponse>
        {
            public ResponseApiModel Response { get; set; }
        }

        public class AddOrUpdateResponseResponse
        {

        }

        public class AddOrUpdateResponseHandler : IAsyncRequestHandler<AddOrUpdateResponseRequest, AddOrUpdateResponseResponse>
        {
            public AddOrUpdateResponseHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateResponseResponse> Handle(AddOrUpdateResponseRequest request)
            {
                var entity = await _dataContext.Responses
                    .SingleOrDefaultAsync(x => x.Id == request.Response.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Responses.Add(entity = new Response());
                entity.Name = request.Response.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateResponseResponse()
                {

                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
