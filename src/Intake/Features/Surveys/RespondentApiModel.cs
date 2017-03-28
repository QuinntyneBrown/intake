using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class RespondentApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromRespondent<TModel>(Respondent respondent) where
            TModel : RespondentApiModel, new()
        {
            var model = new TModel();
            model.Id = respondent.Id;
            model.TenantId = respondent.TenantId;
            return model;
        }

        public static RespondentApiModel FromRespondent(Respondent respondent)
            => FromRespondent<RespondentApiModel>(respondent);

    }
}
