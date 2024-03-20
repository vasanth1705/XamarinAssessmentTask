using AndroidX.ViewPager2.Widget;
using Google.Android.Material.Tabs;

namespace XamarinAssessmentTask.Views.Callbacks
{
    partial class ViewPager2PageChangeCallback : ViewPager2.OnPageChangeCallback
    {
        private readonly TabLayout _tabLayout;

        public ViewPager2PageChangeCallback(TabLayout tabLayout)
        {
            _tabLayout = tabLayout;
        }

        public override void OnPageSelected(int position)
        {
            base.OnPageSelected(position);
            _tabLayout.GetTabAt(position)?.Select();
        }

    }
}