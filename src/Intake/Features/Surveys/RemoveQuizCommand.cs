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
    public class RemoveQuizCommand
    {
        public class RemoveQuizRequest : IRequest<RemoveQuizResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveQuizResponse { }

        public class RemoveQuizHandler : IAsyncRequestHandler<RemoveQuizRequest, RemoveQuizResponse>
        {
            public RemoveQuizHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveQuizResponse> Handle(RemoveQuizRequest request)
            {
                var quiz = await _dataContext.Surveys.FindAsync(request.Id);
                quiz.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveQuizResponse();
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
