using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using System.Collections.Generic;
//Адаптер для видео в recyclerview
namespace ShikimoriMe.UiHelper
{
    public class VideoAdapter : RecyclerView.Adapter
    {
        private List<string> videoUrls;

        public VideoAdapter(List<string> videoUrls)
        {
            this.videoUrls = videoUrls;
        }

        public override int ItemCount => videoUrls.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.video_item, parent, false);
            return new VideoViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is VideoViewHolder videoViewHolder)
            {     
                videoViewHolder.WebViewVideo.LoadDataWithBaseURL(null, videoUrls[position], "text/html", "utf-8", null);
            }
        }
    }

    public class VideoViewHolder : RecyclerView.ViewHolder
    {
        public WebView WebViewVideo { get; }

        public VideoViewHolder(View itemView) : base(itemView)
        {
            WebViewVideo = itemView.FindViewById<WebView>(Resource.Id.webViewVideo);
            WebViewVideo.Settings.JavaScriptEnabled = true;
            WebViewVideo.Settings.MediaPlaybackRequiresUserGesture = true;
        }
    }
}