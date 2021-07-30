using System.Collections.Generic;
using Cars.Acl.DataMuse.Results;
using Cars.Application.Query.Results;

namespace Cars.Api.Responses
{
    public class GetCarResponse
    {
        public GetCarResponse(CarResult car, SoundsLikeWordResults soundsLikeWords)
        {
            Make = car.Make;
            Model = car.Model;
            Colour = car.Colour;
            Year = car.Year;
            if (soundsLikeWords.Success)
            {
                WordsThatSoundLikeModel = soundsLikeWords.Words;
            }
            else
            {
                WordsThatSoundLikeModel = new List<string>();
            }
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Colour { get; private set; }
        public int Year { get; private set; }
        public List<string> WordsThatSoundLikeModel { get; private set; }
    }
}
