using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class GetAnswersQuery
    {
        public class GetAnswersRequest : IRequest<GetAnswersResponse> { }

        public class GetAnswersResponse
        {
            public ICollection<AnswerApiModel> Answers { get; set; } = new HashSet<AnswerApiModel>();
        }

        public class GetAnswersHandler : IAsyncRequestHandler<GetAnswersRequest, GetAnswersResponse>
        {
            public GetAnswersHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetAnswersResponse> Handle(GetAnswersRequest request)
            {
                var answers = await _dataContext.Answers
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                return new GetAnswersResponse()
                {
                    Answers = answers.Select(x => AnswerApiModel.FromAnswer(x)).ToList()
                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
