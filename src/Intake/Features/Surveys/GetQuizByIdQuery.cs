using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetQuizByIdQuery
    {
        public class GetQuizByIdRequest : IRequest<GetQuizByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetQuizByIdResponse
        {
            public QuizApiModel Quiz { get; set; } 
		}

        public class GetQuizByIdHandler : IAsyncRequestHandler<GetQuizByIdRequest, GetQuizByIdResponse>
        {
            public GetQuizByIdHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetQuizByIdResponse> Handle(GetQuizByIdRequest request)
            {                
                return new GetQuizByIdResponse()
                {
                    Quiz = QuizApiModel.FromQuiz(await _dataContext.Surveys.FindAsync(request.Id))
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
