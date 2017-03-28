using MediatR;
using Intake.Data;
using Intake.Data.Model;
using Intake.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Intake.Features.Surveys
{
    public class AddOrUpdateQuestionCommand
    {
        public class AddOrUpdateQuestionRequest : IRequest<AddOrUpdateQuestionResponse>
        {
            public QuestionApiModel Question { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateQuestionResponse { }

        public class AddOrUpdateQuestionHandler : IAsyncRequestHandler<AddOrUpdateQuestionRequest, AddOrUpdateQuestionResponse>
        {
            public AddOrUpdateQuestionHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuestionResponse> Handle(AddOrUpdateQuestionRequest request)
            {
                var entity = await _context.Questions
                    .SingleOrDefaultAsync(x => x.Id == request.Question.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Questions.Add(entity = new Question());
                entity.Name = request.Question.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateQuestionResponse();
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }
    }
}