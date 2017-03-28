using Intake.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Intake.Features.Surveys.AddOrUpdateRespondentCommand;
using static Intake.Features.Surveys.GetRespondentsQuery;
using static Intake.Features.Surveys.GetRespondentByIdQuery;
using static Intake.Features.Surveys.RemoveRespondentCommand;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/respondent")]
    public class RespondentController : ApiController
    {
        public RespondentController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateRespondentResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateRespondentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateRespondentResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateRespondentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetRespondentsResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetRespondentsQuery.GetRespondentsRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetRespondentByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetRespondentByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveRespondentResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveRespondentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
