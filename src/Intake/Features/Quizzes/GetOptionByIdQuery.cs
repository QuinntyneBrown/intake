using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Quizzes
{
    public class GetOptionByIdQuery
    {
        public class GetOptionByIdRequest : IRequest<GetOptionByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetOptionByIdResponse
        {
            public OptionApiModel Option { get; set; } 
		}

        public class GetOptionByIdHandler : IAsyncRequestHandler<GetOptionByIdRequest, GetOptionByIdResponse>
        {
            public GetOptionByIdHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetOptionByIdResponse> Handle(GetOptionByIdRequest request)
            {                
                return new GetOptionByIdResponse()
                {
                    Option = OptionApiModel.FromOption(await _dataContext.Options.FindAsync(request.Id))
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
