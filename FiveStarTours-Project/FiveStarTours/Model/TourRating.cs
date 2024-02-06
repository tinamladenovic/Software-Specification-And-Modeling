using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class TourRating : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int GuidesKnowledge { get; set; }
        public int GuidesSpeaking { get; set; }
        public int LevelOfInterest { get; set; }
        public string Photo { get; set; }
        public string Review { get; set; }
        public int UserId { get; set; }
        public bool Reported { get; set; }
        public KeyPoints KeyPoint {get; set;}

        public TourRating() { }

        public TourRating(int tourId, int guidesKnowledge, int guidesSpeaking, int levelOfInterest, string photo, string review, int userId)
        {
            TourId = tourId;
            GuidesKnowledge = guidesKnowledge;
            GuidesSpeaking = guidesSpeaking;
            LevelOfInterest = levelOfInterest;
            Photo = photo;
            Review = review;
            UserId = userId;   
        }
        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), TourId.ToString(), GuidesKnowledge.ToString(), GuidesSpeaking.ToString(),
                LevelOfInterest.ToString(), Photo, Review, UserId.ToString(), Reported.ToString()};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuidesKnowledge = Convert.ToInt32(values[2]);
            GuidesSpeaking = Convert.ToInt32(values[3]);
            LevelOfInterest = Convert.ToInt32(values[4]);
            Photo = Convert.ToString(values[5]);
            Review = Convert.ToString(values[6]);
            UserId = Convert.ToInt32(values[7]);
            Reported = Convert.ToBoolean(values[8]);
        }
    }
}
