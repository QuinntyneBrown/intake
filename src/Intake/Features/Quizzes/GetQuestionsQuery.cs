using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Quizzes
{
    public class GetQuestionsQuery
    {
        public class GetQuestionsRequest : IRequest<GetQuestionsResponse> { }

        public class GetQuestionsResponse
        {
            public ICollection<QuestionApiModel> Questions { get; set; } = new HashSet<QuestionApiModel>();
        }

        public class GetQuestionsHandler : IAsyncRequestHandler<GetQuestionsRequest, GetQuestionsResponse>
        {
            public GetQuestionsHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetQuestionsResponse> Handle(GetQuestionsRequest request)
            {
                var questions = await _dataContext.Questions
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetQuestionsResponse()
                {
                    Questions = questions.Select(x => QuestionApiModel.FromQuestion(x)).ToList()
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
