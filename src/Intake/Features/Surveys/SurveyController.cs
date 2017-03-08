using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Intake.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/survey")]
    public class SurveyController : ApiController
    {
        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateSurveyCommand.AddOrUpdateSurveyResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSurveyCommand.AddOrUpdateSurveyRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSurveyCommand.AddOrUpdateSurveyResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSurveyCommand.AddOrUpdateSurveyRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSurveysQuery.GetSurveysResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetSurveysQuery.GetSurveysRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSurveyByIdQuery.GetSurveyByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSurveyByIdQuery.GetSurveyByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSurveyCommand.RemoveSurveyResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSurveyCommand.RemoveSurveyRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
