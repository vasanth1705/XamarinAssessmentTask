using Foundation;

namespace XamarinAssessmentTaskiOS.Utilities
{
    public class UserDefaultsHelper
    {
        public static void SetMyBalanceAmount(string key, int value)
        {
            NSUserDefaults.StandardUserDefaults.SetInt(value, key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        public static int GetmyBalanceAmount(string key)
        {
            return (int)NSUserDefaults.StandardUserDefaults.IntForKey(key);
        }
    }
}

