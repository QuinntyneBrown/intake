using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Quizzes
{
    public class GetAnswerByIdQuery
    {
        public class GetAnswerByIdRequest : IRequest<GetAnswerByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetAnswerByIdResponse
        {
            public AnswerApiModel Answer { get; set; } 
		}

        public class GetAnswerByIdHandler : IAsyncRequestHandler<GetAnswerByIdRequest, GetAnswerByIdResponse>
        {
            public GetAnswerByIdHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetAnswerByIdResponse> Handle(GetAnswerByIdRequest request)
            {                
                return new GetAnswerByIdResponse()
                {
                    Answer = AnswerApiModel.FromAnswer(await _dataContext.Answers.FindAsync(request.Id))
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
