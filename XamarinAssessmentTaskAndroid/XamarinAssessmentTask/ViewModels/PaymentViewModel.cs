using System;
using System.Linq;
using Android.Content;
using XamarinAssessmentTask.Database;
using XamarinAssessmentTask.Enums;
using XamarinAssessmentTask.Models;
using XamarinAssessmentTask.Utilities;

namespace XamarinAssessmentTask.ViewModels
{
    public class PaymentViewModel
    {
        private readonly PaymentDatabaseHelper paymentDbHelper;
        private readonly BeneficiaryDatabaseHelper beneficiaryDbHelper;

        public PaymentViewModel()
        {
            paymentDbHelper = PaymentDatabaseHelper.Instance;
            beneficiaryDbHelper = BeneficiaryDatabaseHelper.Instance;
        }

        public int FetchMyBalance(Context context)
        {
            return SharedPrefHelper.GetMyBalanceAmount(context);
        }

        public void UpdateMyBalance(Context context, int amount)
        {
            SharedPrefHelper.SetMyBalanceAmount(context, amount);
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

        public TopUpProcessValidator TopUpAction(PaymentModel payment, Context context)
        {
            try
            {
                var user = beneficiaryDbHelper.GetItem(payment.BeneficiaryId);
                int topUpLimit = user.IsVerified ? 500 : 100;
                if (!CheckMultiTopUpLimitReached())
                {
                    if (user.TotalTopUpAllowance + payment.TopUpAmount <= topUpLimit)
                    {
                        int currentBalance = FetchMyBalance(context);
                        var totalAmount = payment.TopUpAmount + payment.RechargeFee;
                        if (currentBalance >= totalAmount)
                        {
                            UpdateMyBalance(context, currentBalance - totalAmount);
                            user.TotalTopUpAllowance += payment.TopUpAmount;
                            beneficiaryDbHelper.SaveItem(user);
                            var newPayment = new PaymentModel()
                            {
                                Username = payment.Username,
                                TopUpAmount = payment.TopUpAmount,
                                BeneficiaryId = payment.BeneficiaryId,
                                MobileNumber = payment.MobileNumber,
                                RechargeFee = payment.RechargeFee
                            };
                            paymentDbHelper.SaveItem(newPayment);
                        }
                        else
                        {
                            return TopUpProcessValidator.InsufficientBalance;
                        }
                    }
                    else
                    {
                        return TopUpProcessValidator.MaxTopUpLimit;
                    }
                    return TopUpProcessValidator.Success;
                }
                else
                {
                    return TopUpProcessValidator.MaxMultiTopUpLimit;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"===> An error occurred: {ex.Message}");
                return TopUpProcessValidator.Error;
            }

        }

        public bool CheckMultiTopUpLimitReached()
        {
            try
            {
                var payments = paymentDbHelper.GetItems();
                if (payments == null || !payments.Any())
                {
                    Console.WriteLine("No payments found in the database.");
                    return false;
                }

                var totalTopUp = payments.Sum(payment => payment.TopUpAmount);
                return totalTopUp >= 3000;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching payments: {ex.Message}");
                return false;
            }
        }
    }
}