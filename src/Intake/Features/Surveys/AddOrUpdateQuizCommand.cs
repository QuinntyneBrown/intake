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
    public class AddOrUpdateQuizCommand
    {
        public class AddOrUpdateQuizRequest : IRequest<AddOrUpdateQuizResponse>
        {
            public QuizApiModel Quiz { get; set; }
        }

        public class AddOrUpdateQuizResponse
        {

        }

        public class AddOrUpdateQuizHandler : IAsyncRequestHandler<AddOrUpdateQuizRequest, AddOrUpdateQuizResponse>
        {
            public AddOrUpdateQuizHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuizResponse> Handle(AddOrUpdateQuizRequest request)
            {
                var entity = await _dataContext.Surveys
                    .SingleOrDefaultAsync(x => x.Id == request.Quiz.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Surveys.Add(entity = new Survey());
                entity.Name = request.Quiz.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateQuizResponse()
                {

                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
