using AndroidX.Fragment.App;
using AndroidX.ViewPager2.Adapter;
using XamarinAssessmentTask.Views.Fragments;

namespace XamarinAssessmentTask.Adapters
{
    public class TabsAdapter : FragmentStateAdapter
    {
        private readonly Fragment[] _fragments = { new RechargeFragment(), new HistoryFragment() };

        public TabsAdapter(FragmentActivity fragmentActivity) : base(fragmentActivity)
        {
        }

        public override int ItemCount => _fragments?.Length ?? 0;

        public override Fragment CreateFragment(int position)
        {
            return _fragments[position];
        }
    }
}