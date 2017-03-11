using MediatR;
using Intake.Data;
using Intake.Features.Core;
using System.Threading.Tasks;

namespace Intake.Features.Surveys
{
    public class GetQuestionByIdQuery
    {
        public class GetQuestionByIdRequest : IRequest<GetQuestionByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetQuestionByIdResponse
        {
            public QuestionApiModel Question { get; set; } 
		}

        public class GetQuestionByIdHandler : IAsyncRequestHandler<GetQuestionByIdRequest, GetQuestionByIdResponse>
        {
            public GetQuestionByIdHandler(IntakeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuestionByIdResponse> Handle(GetQuestionByIdRequest request)
            {                
                return new GetQuestionByIdResponse()
                {
                    Question = QuestionApiModel.FromQuestion(await _context.Questions.FindAsync(request.Id))
                };
            }

            private readonly IntakeContext _context;
            private readonly ICache _cache;
        }

    }

}
