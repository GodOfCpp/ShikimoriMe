namespace ShikimoriMe
{
    public abstract class ApiRequestsBase
    {

    }
    public class AnimeRequestSettings : ApiRequestsBase
    {
        public int? limit;
        public Order? order;
        public Kind? kind;
        public Duration? duration;
        public Rating? rating;
        public Censored? censored;
        public Mylist? mylist;
        public string search;
    }
}