using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/respondent")]
    public class RespondentController : ApiController
    {
        public RespondentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateRespondentCommand.AddOrUpdateRespondentResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateRespondentCommand.AddOrUpdateRespondentRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateRespondentCommand.AddOrUpdateRespondentResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateRespondentCommand.AddOrUpdateRespondentRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetRespondentsQuery.GetRespondentsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetRespondentsQuery.GetRespondentsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetRespondentByIdQuery.GetRespondentByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetRespondentByIdQuery.GetRespondentByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveRespondentCommand.RemoveRespondentResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveRespondentCommand.RemoveRespondentRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
