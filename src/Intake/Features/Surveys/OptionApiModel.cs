using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class OptionApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromOption<TModel>(Option option) where
            TModel : OptionApiModel, new()
        {
            var model = new TModel();
            model.Id = option.Id;
            model.TenantId = option.TenantId;
            return model;
        }

        public static OptionApiModel FromOption(Option option)
            => FromOption<OptionApiModel>(option);

    }
}
