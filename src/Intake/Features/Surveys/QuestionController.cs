using MediatR;
using Intake.Security;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuestionCommand.AddOrUpdateQuestionResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuestionCommand.AddOrUpdateQuestionRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuestionCommand.AddOrUpdateQuestionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuestionCommand.AddOrUpdateQuestionRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuestionsQuery.GetQuestionsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetQuestionsQuery.GetQuestionsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuestionByIdQuery.GetQuestionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuestionByIdQuery.GetQuestionByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuestionCommand.RemoveQuestionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuestionCommand.RemoveQuestionRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
