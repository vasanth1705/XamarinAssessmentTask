using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using XamarinAssessmentTask.Adapters;
using XamarinAssessmentTask.ViewModels;

namespace XamarinAssessmentTask.Views.Fragments
{
    public class HistoryFragment : Fragment
    {
		private TextView tvNoHistory;
		private RecyclerView rvHistory;
		private HistoryViewModel viewModel;
        private HistoryAdapter adapter;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

        public override void OnStart()
        {
            base.OnStart();
            UpdateViews();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_history, container, false);
			SetupViews(view);
            return view;
		}

		private void SetupViews(View view) {
			if (view != null) {
				tvNoHistory = view.FindViewById<TextView>(Resource.Id.tv_no_history_found);
				rvHistory = view.FindViewById<RecyclerView>(Resource.Id.rv_history);
				viewModel = new HistoryViewModel();
				adapter = new HistoryAdapter(viewModel?.GetPaymentList());
                if (adapter != null)
                {
                    rvHistory.SetAdapter(adapter);
                }
                var layoutManager = new LinearLayoutManager(view.Context);
                rvHistory.SetLayoutManager(layoutManager);
            }
        }

        private void UpdateViews()
        {
            if (viewModel.IsShowPaymentHistory())
            {
                rvHistory.Visibility = ViewStates.Visible;
                tvNoHistory.Visibility = ViewStates.Gone;
            }
            else
            {
                rvHistory.Visibility = ViewStates.Gone;
                tvNoHistory.Visibility = ViewStates.Visible;
            }
            if (adapter != null) {
                adapter.UpdateHistory(viewModel?.GetPaymentList());
            }
        }
    }
}