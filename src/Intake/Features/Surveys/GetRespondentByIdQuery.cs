using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetRespondentByIdQuery
    {
        public class GetRespondentByIdRequest : IRequest<GetRespondentByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetRespondentByIdResponse
        {
            public RespondentApiModel Respondent { get; set; } 
		}

        public class GetRespondentByIdHandler : IAsyncRequestHandler<GetRespondentByIdRequest, GetRespondentByIdResponse>
        {
            public GetRespondentByIdHandler(IntakeContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetRespondentByIdResponse> Handle(GetRespondentByIdRequest request)
            {                
                return new GetRespondentByIdResponse()
                {
                    Respondent = RespondentApiModel.FromRespondent(await _dataContext.Respondents.FindAsync(request.Id))
                };
            }

            private readonly IntakeContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
