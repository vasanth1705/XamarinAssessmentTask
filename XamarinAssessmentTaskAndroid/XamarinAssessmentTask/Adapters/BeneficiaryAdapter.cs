using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.Adapters
{
    public class BeneficiaryAdapter: RecyclerView.Adapter
	{
        private IReadOnlyList<BeneficiariesModel> _beneficiaryList;
        public event EventHandler<BeneficiariesModel> SelectedItem;

        public BeneficiaryAdapter(IReadOnlyList<BeneficiariesModel> beneficiaryList)
		{
            _beneficiaryList = beneficiaryList;
        }

        public override int ItemCount => _beneficiaryList?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is BeneficiaryViewHolder viewHolder)
            {
                viewHolder?.Bind(_beneficiaryList[position]);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_beneficiary_layout, parent, false);
            int screenWidth = parent.Context.Resources.DisplayMetrics.WidthPixels;
            int itemWidth = (int)(screenWidth / 2.7);
            ViewGroup.LayoutParams layoutParams = itemView.LayoutParameters;
            layoutParams.Width = itemWidth;
            itemView.LayoutParameters = layoutParams;
            var viewHolder = new BeneficiaryViewHolder(itemView, OnClick);
            return viewHolder;
        }

        public void UpdateBeneficiaries(IReadOnlyList<BeneficiariesModel> beneficiaries)
        {
            _beneficiaryList = beneficiaries;
            NotifyDataSetChanged();
        }

        private void OnClick(int position)
        {
            if (SelectedItem != null)
            {
                if (_beneficiaryList.Count > 0) {
                    SelectedItem(this, _beneficiaryList[position]);
                }
            }
        }
    }

    public class BeneficiaryViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView tvBeneficiaryName, tvMobile;

        public BeneficiaryViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            tvBeneficiaryName = itemView?.FindViewById<TextView>(Resource.Id.tv_beneficiary_name);
            tvMobile = itemView?.FindViewById<TextView>(Resource.Id.tv_mobile);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }

        public void Bind(BeneficiariesModel beneficiary) {
            if (beneficiary != null) {
                tvBeneficiaryName.Text = beneficiary?.Name ?? "";
                tvMobile.Text = beneficiary?.MobileNumber ?? "";
            }
        }
    }
}