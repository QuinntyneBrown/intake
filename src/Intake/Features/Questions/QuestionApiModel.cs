using Intake.Data.Model;

namespace Intake.Features.Questions
{
    public class QuestionApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromQuestion<TModel>(Question question) where
            TModel : QuestionApiModel, new()
        {
            var model = new TModel();
            model.Id = question.Id;
            model.TenantId = question.TenantId;
            model.Name = question.Name;
            return model;
        }

        public static QuestionApiModel FromQuestion(Question question)
            => FromQuestion<QuestionApiModel>(question);

    }
}
