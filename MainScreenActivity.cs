using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using ShikimoriMe.UiHelper;
using System.Collections.Generic;
using System.Linq;

namespace ShikimoriMe
{

    [Activity(Label = "MainScreenActivity")]
    public class MainScreenActivity : Activity
    {
        private RadioGroup radioGroup;
        private RecyclerView recycler;
        private RecyclerViewAdapter animeItemAdapter;
        private ProfilePageAdapter profileItemAdapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<Data> lstData = new List<Data>();
        private List<UserInfo> lstUser = new List<UserInfo>();
        private bool doubleBackToExitPressedOnce;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_mainscreen);
            InitUi();
            InitRadioBar();
        }

        private void InitUi()
        {
            var searchView = FindViewById<SearchView>(Resource.Id.searchAnimeView);

            searchView.Click += (sender, e) => searchView.Iconified = false;
            searchView.QueryTextSubmit += async (sender, e) =>
            {
                Anime[] animes = await Viewer.GetAnimeByName(e.Query);
                InitDataForRecycler(animes.OrderByDescending(anime => anime.Score).ToArray());
                InitRecyclerBase();
                InitRecyclerAnime();
                (sender as SearchView).Iconified = true;
                (sender as SearchView).ClearFocus();
            };
        }

        private void InitRadioBar()
        {
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            radioGroup.CheckedChange += async (sender, e) =>
            { 
                switch (e.CheckedId)
                {
                    case Resource.Id.radioButtonHome:
                        InitDataForRecycler(await Viewer.GetOnGoingAnime());
                        InitRecyclerBase();
                        InitRecyclerAnime();
                        break;
                    case Resource.Id.radioButtonProfile:
                        lstUser = new List<UserInfo> { await Viewer.GetMyInfo() };
                        InitRecyclerBase();
                        InitRecyclerProfile();
                        break;
                }
            };
            FindViewById<RadioButton>(Resource.Id.radioButtonHome).Checked = true;
        }

        private void InitRecyclerBase()
        {
            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycler.SetLayoutManager(layoutManager);
        }
        private void InitRecyclerProfile()
        {
            profileItemAdapter = new ProfilePageAdapter(lstUser);
            recycler.SetAdapter(profileItemAdapter);
        }
        private void InitRecyclerAnime()
        {
            animeItemAdapter = new RecyclerViewAdapter(lstData);
            animeItemAdapter.ItemClick += OnAnimeItemClick;
            recycler.SetAdapter(animeItemAdapter);
        }

        private void InitDataForRecycler(Anime[] animes)
        {
            lstData.Clear();
            foreach (Anime anime in animes)
                lstData.Add(new Data() { description = $"{anime.Russian} ({anime.Name})", imageId = $"{ClientConstants.BaseUrl}{anime.Image.Original}", animeId = anime.Id});
        }

        private void OnAnimeItemClick(object sender, int position)
        {
            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerViewAdapter adapter = recyclerView.GetAdapter() as RecyclerViewAdapter;
            RecyclerViewHolder viewHolder = recyclerView.FindViewHolderForAdapterPosition(position) as RecyclerViewHolder; 

            
            // При клике на элемент, открываем подробное описание аниме
            var intent = new Intent(this, typeof(AnimeDetailInfoActivity));
            intent.PutExtra("AnimePosition", position); // Передаем позицию аниме
            intent.PutExtra("AnimeId", lstData[position].animeId);
            StartActivity(intent);
            OverridePendingTransition(0, 0);
        }


        public override void OnBackPressed()
        {
            // Предлагаем пользователю выйти из приложения при двойном нажатии "Назад"
            if (doubleBackToExitPressedOnce)
            {
                base.OnBackPressed();
                return;
            }

            this.doubleBackToExitPressedOnce = true;
            Toast.MakeText(this, "Нажмите еще раз, чтобы выйти", ToastLength.Short).Show();

            new Handler().PostDelayed(() => { doubleBackToExitPressedOnce = false; }, 2000); // Сброс через 2 секунды
        }

    }
}