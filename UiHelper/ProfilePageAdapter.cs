using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using System.Collections.Generic;

namespace ShikimoriMe.UiHelper
{
    class ProfilePageViewHolder : RecyclerView.ViewHolder
    {
        public ImageView userAvatar;
        public TextView userDescription;
        public TextView other;
        public ProfilePageViewHolder(View itemView) : base(itemView)
        {
            userAvatar = itemView.FindViewById<ImageView>(Resource.Id.userAvatar);
            userDescription = itemView.FindViewById<TextView>(Resource.Id.userDesc);
            other = itemView.FindViewById<TextView>(Resource.Id.other);
        }
    }

    class ProfilePageAdapter : RecyclerView.Adapter
    {
        List<UserInfo> lstData = new List<UserInfo>();
        public override int ItemCount => this.lstData.Count;

        public ProfilePageAdapter(List<UserInfo> lstData)
        {
            this.lstData = lstData;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ProfilePageViewHolder viewHolder = holder as ProfilePageViewHolder;
            Picasso.Get().Load(lstData[position].Image.x160).Into(viewHolder.userAvatar);
            viewHolder.userDescription.Text = lstData[position].Nickname;
            viewHolder.other.Text = lstData[position].Website;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.profilepage_item, parent, false);
            return new ProfilePageViewHolder(itemView);
        }
    }
}