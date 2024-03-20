using System;
using System.Collections.Generic;
using XamarinAssessmentTaskiOS.Database;

namespace XamarinAssessmentTaskiOS.ViewModels
{
	public class RechargeViewModel
	{
        private readonly BeneficiaryDatabaseHelper beneficiaryDbHelper;

        public RechargeViewModel()
		{
            beneficiaryDbHelper = BeneficiaryDatabaseHelper.Instance;
        }

		public List<int> FetchTopUpData() {
			return new List<int> { 5, 10, 20, 30, 50, 75, 100 };
        }

        public bool IsVerifiedUser(int beneficairyId)
        {
            try
            {
                var user = beneficiaryDbHelper.GetItem(beneficairyId);
                if (user.IsVerified == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItem: " + ex.Message);
                return false;
            }

        }
    }
}