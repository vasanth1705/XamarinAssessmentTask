using System;
using System.Threading.Tasks;
using UIKit;
namespace XamarinAssessmentTaskiOS.Core
{
	public class BaseViewController: UIViewController
	{
        public BaseViewController(IntPtr handle) : base(handle) { }
        public BaseViewController() { }
        
        protected void ShowAlert(string title = "", string message = "", Action okAction = null)
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) =>
            {
                okAction?.Invoke();
            }));
            PresentViewController(alertController, true, null);
        }

        protected void PushViewController<T>(Action<T> configureViewController = null) where T : UIViewController
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var viewControllerIdentifier = typeof(T).Name;

            if (storyboard.InstantiateViewController(viewControllerIdentifier) is T viewController)
            {
                configureViewController?.Invoke(viewController);
                NavigationController.PushViewController(viewController, true);
            }
        }
    }
}