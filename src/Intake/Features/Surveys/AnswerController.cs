using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/answer")]
    public class AnswerController : ApiController
    {
        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateAnswerCommand.AddOrUpdateAnswerResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateAnswerCommand.AddOrUpdateAnswerRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateAnswerCommand.AddOrUpdateAnswerResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateAnswerCommand.AddOrUpdateAnswerRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetAnswersQuery.GetAnswersResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetAnswersQuery.GetAnswersRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetAnswerByIdQuery.GetAnswerByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetAnswerByIdQuery.GetAnswerByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveAnswerCommand.RemoveAnswerResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveAnswerCommand.RemoveAnswerRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
