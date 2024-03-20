using Android.App;
using Android.Graphics;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.ViewPager2.Widget;
using Google.Android.Material.Tabs;
using XamarinAssessmentTask.Adapters;
using XamarinAssessmentTask.Views.Callbacks;

namespace XamarinAssessmentTask.Views
{
    [Activity (Label = "RechargeActivity", Theme = "@style/AppTheme.NoActionBar")]			
	public class RechargeActivity : AppCompatActivity
	{
        private TabLayout tabLayout;
        private ViewPager2 viewPager;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.activity_recharge);
            SetupViews();
		}

        protected override void OnStart()
        {
            base.OnStart();
            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnSubscribeEvents();
        }

        private void SetupViews() {
            tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);
            viewPager = FindViewById<ViewPager2>(Resource.Id.view_pager);
            SetupToolBar();
            if (tabLayout != null)
            {
                tabLayout.SetTabTextColors(Color.White, Color.DeepSkyBlue);
                tabLayout.AddTab(tabLayout.NewTab().SetText(Resources.GetString(Resource.String.recharge_text)));
                tabLayout.AddTab(tabLayout.NewTab().SetText(Resources.GetString(Resource.String.history_text)));
            }
            if (viewPager != null) {
                viewPager.Adapter = new TabsAdapter(this);
                viewPager.RegisterOnPageChangeCallback(new ViewPager2PageChangeCallback(tabLayout));
                viewPager.UserInputEnabled = false;
            }
        }

        private void SetupToolBar()
        {
            var toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            if (SupportActionBar != null)
            {
                SupportActionBar.Title = "Mobile Recharge";
            }
        }

        private void SubscribeEvents() {
            if (tabLayout != null) {
                tabLayout.TabSelected += TabLayout_TabSelected;
            }
        }

        private void UnSubscribeEvents() {
            if (tabLayout != null)
            {
                tabLayout.TabSelected -= TabLayout_TabSelected;
            }
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            viewPager.SetCurrentItem(e.Tab.Position, true);
        }
    }
}