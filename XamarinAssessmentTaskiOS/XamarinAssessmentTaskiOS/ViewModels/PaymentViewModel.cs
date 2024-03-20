﻿using System;
using System.Linq;
using XamarinAssessmentTaskiOS.Database;
using XamarinAssessmentTaskiOS.Enums;
using XamarinAssessmentTaskiOS.Models;
using XamarinAssessmentTaskiOS.Utilities;

namespace XamarinAssessmentTaskiOS.ViewModels
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

        public int FetchMyBalance() {
            return UserDefaultsHelper.GetmyBalanceAmount("MyBalance");
        }

        public void UpdateMyBalance(int amount) {
            UserDefaultsHelper.SetMyBalanceAmount("MyBalance", amount);
        }

        public bool IsVerifiedUser(int beneficairyId) {
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

        public TopUpProcessValidator TopUpAction(PaymentModel payment)
        {
            try
            {
                var user = beneficiaryDbHelper.GetItem(payment.BeneficiaryId);
                int topUpLimit = user.IsVerified ? 500 : 100;
                if (!CheckMultiTopUpLimitReached()) {
                    if (user.TotalTopUpAllowance + payment.TopUpAmount <= topUpLimit)
                    {
                        int currentBalance = FetchMyBalance();
                        var totalAmount = payment.TopUpAmount + payment.RechargeFee;
                        if (currentBalance >= totalAmount)
                        {
                            UpdateMyBalance(currentBalance - totalAmount);
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
                } else {
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