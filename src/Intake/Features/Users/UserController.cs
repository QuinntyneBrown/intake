using Intake.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Users
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateUserCommand.AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateUserCommand.AddOrUpdateUserRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateUserCommand.AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateUserCommand.AddOrUpdateUserRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetUsersQuery.GetUsersResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetUsersQuery.GetUsersRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetUserByIdQuery.GetUserByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetUserByIdQuery.GetUserByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveUserCommand.RemoveUserResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveUserCommand.RemoveUserRequest request)
            => Ok(await _mediator.Send(request));

        [Route("current")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetUserByUsernameQuery.GetUserByUsernameResponse))]
        public async Task<IHttpActionResult> Current()
        {
            if (!User.Identity.IsAuthenticated)
                return Ok();

            var request = new GetUserByUsernameQuery.GetUserByUsernameRequest() { Username = User.Identity.Name };
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
