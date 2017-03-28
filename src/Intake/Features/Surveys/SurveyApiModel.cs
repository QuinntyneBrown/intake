using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class SurveyApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TenantId { get; set; }

        public static TModel FromSurvey<TModel>(Survey survey) where
            TModel : SurveyApiModel, new()
        {
            var model = new TModel();
            model.Id = survey.Id;
            model.Name = survey.Name;
            model.TenantId = survey.TenantId;
            return model;
        }

        public static SurveyApiModel FromSurvey(Survey survey)
            => FromSurvey<SurveyApiModel>(survey);

    }
}
