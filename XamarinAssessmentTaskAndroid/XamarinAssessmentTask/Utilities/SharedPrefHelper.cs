using Android.Content;

namespace XamarinAssessmentTask.Utilities
{
    public class SharedPrefHelper
	{
        private const string PreferencesFileName = "MyPreferences";
        private const string BalanceKey = "MyBalance";

        public static void EnsureMyBalanceInitialized(Context context)
        {
            var prefs = context.GetSharedPreferences(PreferencesFileName, FileCreationMode.Private);
            if (!prefs.Contains(BalanceKey))
            {
                SetMyBalanceAmount(context, 5000);
            }
        }

        public static void SetMyBalanceAmount(Context context, int value)
        {
            var prefs = context.GetSharedPreferences(PreferencesFileName, FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.PutInt(BalanceKey, value);
            editor.Apply();
        }

        public static int GetMyBalanceAmount(Context context)
        {
            var prefs = context.GetSharedPreferences(PreferencesFileName, FileCreationMode.Private);
            return prefs.GetInt(BalanceKey, 0);
        }
    }
}