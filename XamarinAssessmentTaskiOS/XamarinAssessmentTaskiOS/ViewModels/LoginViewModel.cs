using XamarinAssessmentTaskiOS.Enums;

namespace XamarinAssessmentTaskiOS.ViewModels
{
	public class LoginViewModel
	{
		public LoginViewModel()
		{
		}

		public LoginValidator DoLogin(string username, string password) {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return LoginValidator.EnterAllFields;
            }
            if (username == "test" && password == "test123")
            {
                return LoginValidator.Success;
            }
            else
            {
                return LoginValidator.InvalidUsernameOrPassword;
            }
        }
	}
}

