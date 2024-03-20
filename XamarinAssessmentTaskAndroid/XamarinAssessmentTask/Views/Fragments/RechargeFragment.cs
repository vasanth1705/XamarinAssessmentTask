using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using XamarinAssessmentTask.Adapters;
using XamarinAssessmentTask.ViewModels;
using XamarinAssessmentTask.Views.Activities;

namespace XamarinAssessmentTask.Views.Fragments
{
    public class RechargeFragment : Fragment
    {
        private TextView tvNoBeneficiary;
        private Button btnAddBeneficairy;
        private RecyclerView rvBeneficiaries;
        private RechargeViewModel viewModel;
        private BeneficiaryAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_recharge, container, false);
            SetupViews(view);
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            UpdateViews();
            SubscribeEvents();
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            UnSubscribeEvents();
        }

        private void SetupViews(View view) {
            if (view != null) {
                tvNoBeneficiary = view.FindViewById<TextView>(Resource.Id.tv_no_beneficiary_found);
                btnAddBeneficairy = view.FindViewById<Button>(Resource.Id.btn_add_beneficiary);
                rvBeneficiaries = view.FindViewById<RecyclerView>(Resource.Id.rv_beneficiary);
            }
            viewModel = new RechargeViewModel();
            adapter = new BeneficiaryAdapter(viewModel.GetAllBeneficiaries());
            if (adapter != null) {
                rvBeneficiaries.SetAdapter(adapter);
            }
            var layoutManager = new LinearLayoutManager(view.Context, LinearLayoutManager.Horizontal, false);
            rvBeneficiaries.SetLayoutManager(layoutManager);
        }

        private void SubscribeEvents()
        {
            if (btnAddBeneficairy != null) {
                btnAddBeneficairy.Click += BtnAddBeneficairy_Click;
            }
            if (adapter != null)
            {
                adapter.SelectedItem += Adapter_SelectedItem;
            }
        }

        private void UnSubscribeEvents()
        {
            if (btnAddBeneficairy != null)
            {
                btnAddBeneficairy.Click -= BtnAddBeneficairy_Click;
            }
        }

        private void BtnAddBeneficairy_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(AddBeneficiaryActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        private void Adapter_SelectedItem(object sender, Models.BeneficiariesModel beneficiary)
        {
            Intent intent = new Intent(Activity, typeof(BeneficiaryRechargeActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var json = JsonConvert.SerializeObject(beneficiary);
            intent.PutExtra("beneficiary", json);
            StartActivity(intent);
        }

        private void UpdateViews() {
            if (viewModel.IsShowBeneficiaries()) {
                tvNoBeneficiary.Visibility = ViewStates.Gone;
            } else {
                tvNoBeneficiary.Visibility = ViewStates.Visible;
            }
            adapter.UpdateBeneficiaries(viewModel.GetAllBeneficiaries());
        }
    }
}