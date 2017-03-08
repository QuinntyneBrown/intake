using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetOptionsQuery
    {
        public class GetOptionsRequest : IRequest<GetOptionsResponse> { }

        public class GetOptionsResponse
        {
            public ICollection<OptionApiModel> Options { get; set; } = new HashSet<OptionApiModel>();
        }

        public class GetOptionsHandler : IAsyncRequestHandler<GetOptionsRequest, GetOptionsResponse>
        {
            public GetOptionsHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetOptionsResponse> Handle(GetOptionsRequest request)
            {
                var options = await _dataContext.Options
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetOptionsResponse()
                {
                    Options = options.Select(x => OptionApiModel.FromOption(x)).ToList()
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
