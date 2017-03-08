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
    public class AddOrUpdateAnswerCommand
    {
        public class AddOrUpdateAnswerRequest : IRequest<AddOrUpdateAnswerResponse>
        {
            public AnswerApiModel Answer { get; set; }
        }

        public class AddOrUpdateAnswerResponse
        {

        }

        public class AddOrUpdateAnswerHandler : IAsyncRequestHandler<AddOrUpdateAnswerRequest, AddOrUpdateAnswerResponse>
        {
            public AddOrUpdateAnswerHandler(DataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateAnswerResponse> Handle(AddOrUpdateAnswerRequest request)
            {
                var entity = await _dataContext.Answers
                    .SingleOrDefaultAsync(x => x.Id == request.Answer.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Answers.Add(entity = new Answer());
                entity.Name = request.Answer.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateAnswerResponse()
                {

                };
            }

            private readonly DataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
