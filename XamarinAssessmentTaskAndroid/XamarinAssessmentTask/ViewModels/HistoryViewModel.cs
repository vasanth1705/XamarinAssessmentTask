using System;
using System.Collections.Generic;
using XamarinAssessmentTask.Database;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.ViewModels
{
    public class HistoryViewModel
    {
        private readonly PaymentDatabaseHelper paymentDbHelper;
        public HistoryViewModel()
        {
            paymentDbHelper = PaymentDatabaseHelper.Instance;
        }

        public bool IsShowPaymentHistory()
        {
            if (GetPaymentList()?.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PaymentModel> GetPaymentList()
        {
            try
            {
                return paymentDbHelper.GetItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving payment items: " + ex.Message);
                return new List<PaymentModel>();
            }
        }
    }
}