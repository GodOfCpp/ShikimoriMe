using ShikimoriMe.UiHelper;
using System;
using System.Collections.Generic;

//Сущности для парсинга из json ответа от api
namespace ShikimoriMe
{
    [Serializable]
    public enum Order
    {
        id,
        id_desc,
        ranked,
        kind,
        popularity,
        name,
        aired_on,
        episodes,
        status,
        random,
        created_at,
        created_at_desc
    }
    [Serializable]
    public enum Duration
    {
        S,
        D,
        F
    }
    [Serializable]
    public enum Rating
    {
        none,
        g,
        pg,
        pg_13,
        r,
        r_plus,
        rx
    }
    [Serializable]
    public enum Censored
    {
        _true,
        _false
    }
    [Serializable]
    public enum Mylist
    {
        planned,
        watching,
        rewatching,
        completed,
        on_hold,
        dropped
    }
    [Serializable]
    public enum Kind
    {
        tv,
        movie,
        ova,
        ona,
        special,
        music,
        tv_13,
        tv_24,
        tv_48
    }
    [Serializable]
    public class ImageInfo
    {
        public string Original { get; set; }
        public string Preview { get; set; }
        public string X96 { get; set; }
        public string X48 { get; set; }
    }
    public class ImageDetailedInfo 
    {
        public string x160 { get; set; }
        public string x148 { get; set; }
        public string x80 { get; set; }
        public string x64 { get; set; }
        public string x48 { get; set; }
        public string x32 { get; set; }
        public string x16 { get; set; }
    }
    [Serializable]
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Russian { get; set; }
        public ImageInfo Image { get; set; }
        public string Url { get; set; }
        public string Kind { get; set; }
        public string Score { get; set; }
        public string Status { get; set; }
        public int Episodes { get; set; }
        public int EpisodesAired { get; set; }
        public string AiredOn { get; set; }
        public string ReleasedOn { get; set; }
    }
    [Serializable]
    public class AnimeDetailedInfo : Anime
    {
        public string Rating { get; set; }
        public List<string> English { get; set; }
        public List<string> Japanese { get; set; }
        public List<string> Synonyms { get; set; }
        public string LicenseNameRu { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string DescriptionHtml { get; set; }
        public string DescriptionSource { get; set; }
        public string Franchise { get; set; }
        public bool Favoured { get; set; }
        public bool Anons { get; set; }
        public bool Ongoing { get; set; }
        public int ThreadId { get; set; }
        public int TopicId { get; set; }
        public int MyAnimeListId { get; set; }
        public List<RateScoreStats> RatesScoresStats { get; set; }
        public List<RatesStatusStats> RatesStatusesStats { get; set; }
        public string UpdatedAt { get; set; }
        public string NextEpisodeAt { get; set; }
        public List<string> Fansubbers { get; set; }
        public List<string> Fandubbers { get; set; }
        public List<string> Licensors { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Studio> Studios { get; set; }
        public List<Video> Videos { get; set; }
        public List<Screenshot> Screenshots { get; set; }
        public object UserRate { get; set; }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public ImageDetailedInfo Image { get; set; }
        public DateTime LastOnlineAt { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Website { get; set; }
        public DateTime? BirthOn { get; set; }
        public int FullYears { get; set; }
        public string Locale { get; set; }
    }
}