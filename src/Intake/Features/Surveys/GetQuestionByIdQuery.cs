using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetQuestionByIdQuery
    {
        public class GetQuestionByIdRequest : IRequest<GetQuestionByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetQuestionByIdResponse
        {
            public QuestionApiModel Question { get; set; } 
		}

        public class GetQuestionByIdHandler : IAsyncRequestHandler<GetQuestionByIdRequest, GetQuestionByIdResponse>
        {
            public GetQuestionByIdHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetQuestionByIdResponse> Handle(GetQuestionByIdRequest request)
            {                
                return new GetQuestionByIdResponse()
                {
                    Question = QuestionApiModel.FromQuestion(await _dataContext.Questions.FindAsync(request.Id))
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
