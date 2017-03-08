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
        }

        public class AddOrUpdateOptionResponse
        {

        }

        public class AddOrUpdateOptionHandler : IAsyncRequestHandler<AddOrUpdateOptionRequest, AddOrUpdateOptionResponse>
        {
            public AddOrUpdateOptionHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateOptionResponse> Handle(AddOrUpdateOptionRequest request)
            {
                var entity = await _dataContext.Options
                    .SingleOrDefaultAsync(x => x.Id == request.Option.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Options.Add(entity = new Option());
                entity.Name = request.Option.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateOptionResponse()
                {

                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
