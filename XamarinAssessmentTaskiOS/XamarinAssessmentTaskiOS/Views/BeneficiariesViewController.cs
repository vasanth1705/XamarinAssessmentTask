using System;
using UIKit;

namespace XamarinAssessmentTaskiOS.Views
{
	public partial class BeneficiariesViewController : UIViewController
	{
        
        public BeneficiariesViewController(IntPtr handle) : base (handle)
        {
        }

        public BeneficiariesViewController() { }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            SetupViews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (SegmentControl != null)
            {
                SegmentControl.ValueChanged += SegmentControl_ValueChanged;
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            if (SegmentControl != null)
            {
                SegmentControl.ValueChanged -= SegmentControl_ValueChanged;
            }
        }

        private void SegmentControl_ValueChanged(object sender, EventArgs e)
        {
            var segmentedControl = sender as UISegmentedControl;
            if (segmentedControl == null)
                return;
            if (segmentedControl.SelectedSegment == 0) {
                 LoadChildBeneficiariesViewController();
            } else {
                LoadHistoryViewController();
            }
        }

        private void SetupViews() {
            NavigationItem.SetHidesBackButton(true, false);
            this.Title = "Mobile Recharge";
            NavigationItem.BackButtonTitle = "Back";
            SegmentControl.SelectedSegment = 0;
            SegmentControl.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White }, UIControlState.Selected);
            LoadChildBeneficiariesViewController();
        }

        private void LoadHistoryViewController()
        {
            var historyViewController = Storyboard.InstantiateViewController("HistoryViewController") as HistoryViewController;
            AddChildViewController(historyViewController);
            historyViewController.View.Frame = ContainerView.Bounds;
            ContainerView.AddSubview(historyViewController.View);
            historyViewController.DidMoveToParentViewController(this);
        }

        private void LoadChildBeneficiariesViewController()
        {
            var childBeneficiariesViewController = Storyboard.InstantiateViewController("ChildBeneficiariesViewController") as ChildBeneficiariesViewController;
            AddChildViewController(childBeneficiariesViewController);
            childBeneficiariesViewController.View.Frame = ContainerView.Bounds;
            ContainerView.AddSubview(childBeneficiariesViewController.View);
            childBeneficiariesViewController.DidMoveToParentViewController(this);
        }
    }
}