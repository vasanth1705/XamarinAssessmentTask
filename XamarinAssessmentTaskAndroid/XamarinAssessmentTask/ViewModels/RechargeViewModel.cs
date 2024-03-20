using System;
using System.Collections.Generic;
using XamarinAssessmentTask.Database;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.ViewModels
{
	public class RechargeViewModel
	{
        private readonly BeneficiaryDatabaseHelper dbHelper;

        public RechargeViewModel()
		{
            dbHelper = BeneficiaryDatabaseHelper.Instance;
        }

        public bool IsShowBeneficiaries()
        {
            if (GetAllBeneficiaries()?.Count > 0)
            {
                return true;
            }
            else
            {
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