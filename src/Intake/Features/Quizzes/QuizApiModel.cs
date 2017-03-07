using Intake.Data.Model;

namespace Intake.Features.Quizzes
{
    public class QuizApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromQuiz<TModel>(Quiz quiz) where
            TModel : QuizApiModel, new()
        {
            var model = new TModel();
            model.Id = quiz.Id;
            return model;
        }

        public static QuizApiModel FromQuiz(Quiz quiz)
            => FromQuiz<QuizApiModel>(quiz);

    }
}
