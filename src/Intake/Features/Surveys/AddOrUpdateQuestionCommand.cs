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
    public class AddOrUpdateQuestionCommand
    {
        public class AddOrUpdateQuestionRequest : IRequest<AddOrUpdateQuestionResponse>
        {
            public QuestionApiModel Question { get; set; }
        }

        public class AddOrUpdateQuestionResponse
        {

        }

        public class AddOrUpdateQuestionHandler : IAsyncRequestHandler<AddOrUpdateQuestionRequest, AddOrUpdateQuestionResponse>
        {
            public AddOrUpdateQuestionHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuestionResponse> Handle(AddOrUpdateQuestionRequest request)
            {
                var entity = await _dataContext.Questions
                    .SingleOrDefaultAsync(x => x.Id == request.Question.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Questions.Add(entity = new Question());
                entity.Name = request.Question.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateQuestionResponse()
                {

                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
