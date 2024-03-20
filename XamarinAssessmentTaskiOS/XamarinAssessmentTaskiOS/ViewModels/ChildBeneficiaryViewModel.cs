using System;
using System.Collections.Generic;
using XamarinAssessmentTaskiOS.Database;
using XamarinAssessmentTaskiOS.Models;

namespace XamarinAssessmentTaskiOS.ViewModels
{
	public class ChildBeneficiaryViewModel
	{
        private readonly BeneficiaryDatabaseHelper dbHelper;

        public ChildBeneficiaryViewModel()
		{
            dbHelper = BeneficiaryDatabaseHelper.Instance;
        }

        public bool IsShowBeneficiaries() {
            if (GetAllBeneficiaries()?.Count > 0) {
                return true;
            } else {
                return false;
            }
        }

        public List<BeneficiariesModel> GetAllBeneficiaries()
        {
            try
            {
                return dbHelper.GetItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving beneficiary items: " + ex.Message);
                return new List<BeneficiariesModel>();
            }
        }
    }
}