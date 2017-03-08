using MediatR;
using Intake.Security;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/quiz")]
    public class QuizController : ApiController
    {
        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuizCommand.AddOrUpdateQuizResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuizCommand.AddOrUpdateQuizRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuizCommand.AddOrUpdateQuizResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuizCommand.AddOrUpdateQuizRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuizzesQuery.GetQuizzesResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetQuizzesQuery.GetQuizzesRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuizByIdQuery.GetQuizByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuizByIdQuery.GetQuizByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuizCommand.RemoveQuizResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuizCommand.RemoveQuizRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
