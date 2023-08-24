using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using System;
using System.Collections.Generic;

//Адаптер для аниме в recyclerview
namespace ShikimoriMe.UiHelper
{
    class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageView { get; set; }
        public TextView txtDescription { get; set; }
        public RecyclerViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            imageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            txtDescription = itemView.FindViewById<TextView>(Resource.Id.txtDescription);

            itemView.Click += (sender, e) =>  listener(AdapterPosition);
        }
    }
    class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Data> lstData = new List<Data>();
        public event EventHandler<int> ItemClick;

        public RecyclerViewAdapter(List<Data> lstData) => this.lstData = lstData;
        public override int ItemCount
        {
            get => lstData.Count;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            Picasso.Get().Load(lstData[position].imageId).Into(viewHolder.imageView);
            viewHolder.txtDescription.Text = lstData[position].description;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.Item, parent, false);
            return new RecyclerViewHolder(itemView, OnClick);
        }

        private void OnClick(int position) => ItemClick?.Invoke(this, position);
    }
}