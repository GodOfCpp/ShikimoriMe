//Сущности объектов, получаемых в json через api
namespace ShikimoriMe.UiHelper
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string PlayerUrl { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Hosting { get; set; }
    }

    public class RateScoreStats
    {
        public int Name { get; set; }
        public int Value { get; set; }
    }

    public class RatesStatusStats
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Russian { get; set; }
        public string Kind { get; set; }
        public string EntryType { get; set; }
    }

    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilteredName { get; set; }
        public bool Real { get; set; }
        public string Image { get; set; }
    }

    public class Screenshot
    {
        public string Original { get; set; }
        public string Preview { get; set; }
    }
}