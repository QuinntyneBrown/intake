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
    public class RemoveQuestionCommand
    {
        public class RemoveQuestionRequest : IRequest<RemoveQuestionResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveQuestionResponse { }

        public class RemoveQuestionHandler : IAsyncRequestHandler<RemoveQuestionRequest, RemoveQuestionResponse>
        {
            public RemoveQuestionHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveQuestionResponse> Handle(RemoveQuestionRequest request)
            {
                var question = await _dataContext.Questions.FindAsync(request.Id);
                question.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveQuestionResponse();
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
