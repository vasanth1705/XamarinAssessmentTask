using System;
using XamarinAssessmentTask.Database;
using XamarinAssessmentTask.Enums;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.ViewModels
{
    public class AddBeneficiaryViewModel
    {
        private readonly BeneficiaryDatabaseHelper dbHelper;

        public AddBeneficiaryViewModel()
        {
            dbHelper = BeneficiaryDatabaseHelper.Instance;
        }

        public SaveBeneficiaryValidator SaveBeneficiaryInDB(SaveBeneficiaryModel saveBeneficiary)
        {
            try
            {
                if (!string.IsNullOrEmpty(saveBeneficiary.UserName) && !string.IsNullOrEmpty(saveBeneficiary.MobileNumber))
                {
                    if (dbHelper?.GetItems()?.Count < 5)
                    {
                        if (saveBeneficiary.MobileNumber.Length < 8)
                        {
                            return SaveBeneficiaryValidator.InvalidMobileNumber;
                        }
                        else
                        {
                            var mobileNumberWithCountryCode = saveBeneficiary.CountryCode + saveBeneficiary.MobileNumber;
                            var existingItem = dbHelper.GetItemByMobileNumber(mobileNumberWithCountryCode);
                            if (existingItem != null)
                            {
                                return SaveBeneficiaryValidator.MobileNumberExists;
                            }
                            else
                            {
                                dbHelper?.SaveItem(new BeneficiariesModel
                                {
                                    Name = saveBeneficiary.UserName,
                                    MobileNumber = mobileNumberWithCountryCode,
                                    IsVerified = saveBeneficiary.IsVerified,
                                    TotalTopUpAllowance = 0
                                });
                                return SaveBeneficiaryValidator.Success;
                            }
                        }
                    }
                    else
                    {
                        return SaveBeneficiaryValidator.ReachedMaxBeneficiary;
                    }
                }
                else
                {
                    return SaveBeneficiaryValidator.Error;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving SaveItem: " + ex.Message);
                return SaveBeneficiaryValidator.UnKnownError;
            }

        }
    }
}