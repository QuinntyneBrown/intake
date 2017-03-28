using Intake.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static Intake.Features.Surveys.AddOrUpdateOptionCommand;
using static Intake.Features.Surveys.GetOptionsQuery;
using static Intake.Features.Surveys.GetOptionByIdQuery;
using static Intake.Features.Surveys.RemoveOptionCommand;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/option")]
    public class OptionController : ApiController
    {
        public OptionController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateOptionResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateOptionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateOptionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateOptionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetOptionsResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetOptionsQuery.GetOptionsRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetOptionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetOptionByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveOptionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveOptionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
