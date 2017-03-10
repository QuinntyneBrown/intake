using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/response")]
    public class ResponseController : ApiController
    {
        public ResponseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateResponseCommand.AddOrUpdateResponseResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateResponseCommand.AddOrUpdateResponseRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateResponseCommand.AddOrUpdateResponseResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateResponseCommand.AddOrUpdateResponseRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetResponsesQuery.GetResponsesResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetResponsesQuery.GetResponsesRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetResponseByIdQuery.GetResponseByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetResponseByIdQuery.GetResponseByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveResponseCommand.RemoveResponseResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveResponseCommand.RemoveResponseRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
