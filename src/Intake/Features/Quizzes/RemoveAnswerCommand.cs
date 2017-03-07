using MediatR;
using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Intake.Features.Quizzes
{
    public class RemoveAnswerCommand
    {
        public class RemoveAnswerRequest : IRequest<RemoveAnswerResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveAnswerResponse { }

        public class RemoveAnswerHandler : IAsyncRequestHandler<RemoveAnswerRequest, RemoveAnswerResponse>
        {
            public RemoveAnswerHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveAnswerResponse> Handle(RemoveAnswerRequest request)
            {
                var answer = await _dataContext.Answers.FindAsync(request.Id);
                answer.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveAnswerResponse();
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
