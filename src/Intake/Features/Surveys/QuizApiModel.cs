using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class QuizApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromQuiz<TModel>(Survey quiz) where
            TModel : QuizApiModel, new()
        {
            var model = new TModel();
            model.Id = quiz.Id;
            return model;
        }

        public static QuizApiModel FromQuiz(Survey quiz)
            => FromQuiz<QuizApiModel>(quiz);

    }
}
