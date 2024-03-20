using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.Adapters
{
    public class HistoryAdapter: RecyclerView.Adapter
	{
        private IReadOnlyList<PaymentModel> _paymentList;

        public HistoryAdapter(IReadOnlyList<PaymentModel> paymentList)
		{
            _paymentList = paymentList;
        }

        public override int ItemCount => _paymentList?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is HistoryViewHolder viewHolder)
            {
                viewHolder?.Bind(_paymentList[position]);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_history, parent, false);
            var viewHolder = new HistoryViewHolder(itemView);
            return viewHolder;
        }

        public void UpdateHistory(IReadOnlyList<PaymentModel> paymentList)
        {
            _paymentList = paymentList;
            NotifyDataSetChanged();
        }
    }

    partial class HistoryViewHolder : RecyclerView.ViewHolder
    {
        private TextView tvBeneficiaryName, tvMobile, tvTopupAmount, tvRechargeFee, tvTotalAmount;
        public HistoryViewHolder(View itemView) : base(itemView)
        {
            tvBeneficiaryName = itemView?.FindViewById<TextView>(Resource.Id.tv_beneficiary_name);
            tvMobile = itemView?.FindViewById<TextView>(Resource.Id.tv_mobile);
            tvRechargeFee = itemView?.FindViewById<TextView>(Resource.Id.tv_recharge_fee);
            tvTopupAmount = itemView?.FindViewById<TextView>(Resource.Id.tv_topup_amount);
            tvTotalAmount = itemView?.FindViewById<TextView>(Resource.Id.tv_total_amount);
        }

        public void Bind(PaymentModel payment) {
            if (payment != null) {
                tvBeneficiaryName.Text = "Beneficiary Name : " + payment?.Username ?? "";
                tvMobile.Text = "Mobile Number : " + payment?.MobileNumber ?? "";
                tvRechargeFee.Text = "Recharge Fee : " + payment?.RechargeFee.ToString() + " AED";
                tvTotalAmount.Text = "Total Amount : " + (payment?.TopUpAmount + payment?.RechargeFee).ToString() + " AED";
                tvTopupAmount.Text = "Topup Amount : " + payment?.TopUpAmount.ToString() + " AED";
            }
        }
    }
}