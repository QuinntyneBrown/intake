using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class AnswerApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromAnswer<TModel>(Answer answer) where
            TModel : AnswerApiModel, new()
        {
            var model = new TModel();
            model.Id = answer.Id;
            return model;
        }

        public static AnswerApiModel FromAnswer(Answer answer)
            => FromAnswer<AnswerApiModel>(answer);

    }
}
