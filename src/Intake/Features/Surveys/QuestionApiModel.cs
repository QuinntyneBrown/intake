using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class QuestionApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromQuestion<TModel>(Question question) where
            TModel : QuestionApiModel, new()
        {
            var model = new TModel();
            model.Id = question.Id;
            return model;
        }

        public static QuestionApiModel FromQuestion(Question question)
            => FromQuestion<QuestionApiModel>(question);

    }
}
