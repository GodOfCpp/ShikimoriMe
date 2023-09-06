using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using ShikimoriMe.UiHelper;
using Square.Picasso;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//Activity страницы с детальным описанием аниме
namespace ShikimoriMe
{
    [Activity(Label = "AnimeDetailInfoActivity")]
    public class AnimeDetailInfoActivity : Activity
    {
        private RecyclerView recyclerView;
        private VideoAdapter videoAdapter;
        private List<string> video_urls;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
       
            SetContentView(Resource.Layout.activity_anime_detailed_info);

            await InitUi();
        }

        private async Task InitUi()
        {
            int AnimeId = Intent.GetIntExtra("AnimeId", -1);
            AnimeDetailedInfo animeDetailedInfo = await Viewer.GetAnimeInfoById(AnimeId);

            Picasso.Get().Load($"{ClientConstants.BaseUrl}{animeDetailedInfo.Image.Original}").Into(FindViewById<ImageView>(Resource.Id.imageViewPoster));

            FindViewById<TextView>(Resource.Id.textViewTitle).Text = animeDetailedInfo.Russian;
            FindViewById<TextView>(Resource.Id.textViewRating).Text = $"Оценка: {animeDetailedInfo.Score}/10";
            FindViewById<TextView>(Resource.Id.textViewGenre).Text = $"Жанры: {string.Join(", ", animeDetailedInfo.Genres.Select(genre => genre.Russian))}";
            FindViewById<TextView>(Resource.Id.textViewDescription).Text = Regex.Replace(animeDetailedInfo.Description, @"\[[^\]]*\]", "");

            //Видео по аниме
            video_urls = new List<string>();
            foreach (var video in animeDetailedInfo.Videos)
                video_urls.Add(VideoUrlConstructor.Construct(video.Url));

            //Инициализация адаптера для видео
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            LinearLayoutManager layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            recyclerView.SetLayoutManager(layoutManager);
            videoAdapter = new VideoAdapter(video_urls);
            recyclerView.SetAdapter(videoAdapter);
        }

        private void Space() { }
  
    }
}