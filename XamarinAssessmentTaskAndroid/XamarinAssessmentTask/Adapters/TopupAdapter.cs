using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace XamarinAssessmentTask.Adapters
{
    public class TopupAdapter: RecyclerView.Adapter
	{
        private readonly IReadOnlyList<int> _amounts;
        public event EventHandler<int> SelectedItem;
        public event EventHandler<int> DeSelectedItem;
        private int selectedPosition = RecyclerView.NoPosition;

        public TopupAdapter(IReadOnlyList<int> amounts)
		{
            _amounts = amounts;
        }

        public override int ItemCount => _amounts?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is TopupViewHolder viewHolder)
            {
                bool isSelected = position == selectedPosition;
                viewHolder.Bind(_amounts[position], isSelected);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_topup, parent, false);
            var viewHolder = new TopupViewHolder(itemView, OnClick);
            return viewHolder;
        }

        private void OnClick(int position)
        {
            if (selectedPosition == position)
            {
                return;
            }

            int previouslySelected = selectedPosition;
            selectedPosition = position;

            NotifyItemChanged(previouslySelected);
            NotifyItemChanged(selectedPosition);

            if (previouslySelected != RecyclerView.NoPosition)
            {
                DeSelectedItem?.Invoke(this, 0);
            }

            SelectedItem?.Invoke(this, _amounts[position]);

        }
    }

    partial class TopupViewHolder : RecyclerView.ViewHolder
    {
        private TextView tvAmount, tvAed;
        private View itemView;

        public TopupViewHolder(View itemView) : base(itemView)
        {

        }

        public TopupViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            this.itemView = itemView;
            tvAmount = itemView?.FindViewById<TextView>(Resource.Id.tv_amount);
            tvAed = itemView?.FindViewById<TextView>(Resource.Id.tv_aed);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

        public void Bind(int amount, bool isSelected)
        {
            tvAmount.Text = amount.ToString() ?? "";
            tvAed.Text = "AED";
            itemView?.SetBackgroundColor(isSelected ? Color.DeepSkyBlue : Color.White);
            tvAmount?.SetTextColor(isSelected ? Color.White : Color.Black);
            tvAed?.SetTextColor(isSelected ? Color.White : Color.Black);
        }
    }
}