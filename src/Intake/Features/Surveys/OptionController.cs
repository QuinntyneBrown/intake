using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/option")]
    public class OptionController : ApiController
    {
        public OptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateOptionCommand.AddOrUpdateOptionResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateOptionCommand.AddOrUpdateOptionRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateOptionCommand.AddOrUpdateOptionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateOptionCommand.AddOrUpdateOptionRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetOptionsQuery.GetOptionsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetOptionsQuery.GetOptionsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetOptionByIdQuery.GetOptionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetOptionByIdQuery.GetOptionByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveOptionCommand.RemoveOptionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveOptionCommand.RemoveOptionRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
