using Intake.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Intake.Features.Surveys.AddOrUpdateResponseCommand;
using static Intake.Features.Surveys.GetResponsesQuery;
using static Intake.Features.Surveys.GetResponseByIdQuery;
using static Intake.Features.Surveys.RemoveResponseCommand;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/response")]
    public class ResponseController : ApiController
    {
        public ResponseController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateResponseResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateResponseRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateResponseResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateResponseRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetResponsesResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetResponsesRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetResponseByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetResponseByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveResponseResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveResponseRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
