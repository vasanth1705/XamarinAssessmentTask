using UIKit;

namespace XamarinAssessmentTaskiOS.Utilities
{
    public class CustomTextFieldDelegate : UITextFieldDelegate
    {
        public override bool ShouldReturn(UITextField textField)
        {
            textField.ResignFirstResponder();
            return true;
        }
    }
}