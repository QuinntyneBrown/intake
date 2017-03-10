using Intake.Data.Model;

namespace Intake.Features.Surveys
{
    public class ResponseApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromResponse<TModel>(Response response) where
            TModel : ResponseApiModel, new()
        {
            var model = new TModel();
            model.Id = response.Id;
            return model;
        }

        public static ResponseApiModel FromResponse(Response response)
            => FromResponse<ResponseApiModel>(response);

    }
}
