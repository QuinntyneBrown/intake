using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetQuizzesQuery
    {
        public class GetQuizzesRequest : IRequest<GetQuizzesResponse> { }

        public class GetQuizzesResponse
        {
            public ICollection<QuizApiModel> Quizzes { get; set; } = new HashSet<QuizApiModel>();
        }

        public class GetQuizzesHandler : IAsyncRequestHandler<GetQuizzesRequest, GetQuizzesResponse>
        {
            public GetQuizzesHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetQuizzesResponse> Handle(GetQuizzesRequest request)
            {
                var quizzes = await _dataContext.Surveys
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetQuizzesResponse()
                {
                    Quizzes = quizzes.Select(x => QuizApiModel.FromQuiz(x)).ToList()
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
