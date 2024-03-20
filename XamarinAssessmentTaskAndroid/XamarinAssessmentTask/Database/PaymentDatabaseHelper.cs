using System;
using SQLite;
using System.Collections.Generic;
using XamarinAssessmentTask.Models;
using System.IO;

namespace XamarinAssessmentTask.Database
{
    public class PaymentDatabaseHelper
    {
        private readonly SQLiteConnection _sqliteConnection;
        private static PaymentDatabaseHelper _instance;

        public static PaymentDatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaymentDatabaseHelper();
                }
                return _instance;
            }
        }

        public PaymentDatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "payment_database.db3");
            _sqliteConnection = new SQLiteConnection(dbPath);
            CreateTable();
        }

        private void CreateTable()
        {
            try
            {
                _sqliteConnection.CreateTable<PaymentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while CreateTable: " + ex.Message);
            }
        }

        public void SaveItem(PaymentModel item)
        {
            try
            {
                var existingItem = GetItem(item.Id);
                if (existingItem != null)
                {
                    _sqliteConnection.Update(item);
                }
                else
                {
                    _sqliteConnection.Insert(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving Save or Update Item: " + ex.Message);
            }
        }

        public List<PaymentModel> GetItems()
        {
            try
            {
                return _sqliteConnection.Table<PaymentModel>().OrderByDescending(i => i.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItems: " + ex.Message);
                return new List<PaymentModel>();
            }
        }

        public PaymentModel GetItem(int id)
        {
            try
            {
                return _sqliteConnection.Table<PaymentModel>().FirstOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItem: " + ex.Message);
                return new PaymentModel();
            }
        }

        public void DeleteItem(int id)
        {
            try
            {
                _sqliteConnection.Delete<PaymentModel>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while DeleteItem: " + ex.Message);
            }
        }
    }
}