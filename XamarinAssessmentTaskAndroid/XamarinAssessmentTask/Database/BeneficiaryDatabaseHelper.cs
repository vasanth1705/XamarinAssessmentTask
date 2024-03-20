using System;
using SQLite;
using System.Collections.Generic;
using System.IO;
using XamarinAssessmentTask.Models;

namespace XamarinAssessmentTask.Database
{
    public class BeneficiaryDatabaseHelper
    {
        private readonly SQLiteConnection _sqliteConnection;
        private static BeneficiaryDatabaseHelper _instance;

        public static BeneficiaryDatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BeneficiaryDatabaseHelper();
                }
                return _instance;
            }
        }

        public BeneficiaryDatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "beneficiary_database.db3");
            _sqliteConnection = new SQLiteConnection(dbPath);
            CreateTable();
        }

        private void CreateTable()
        {
            try
            {
                _sqliteConnection.CreateTable<BeneficiariesModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while CreateTable: " + ex.Message);
            }
        }

        public void SaveItem(BeneficiariesModel item)
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

        public List<BeneficiariesModel> GetItems()
        {
            try
            {
                return _sqliteConnection.Table<BeneficiariesModel>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItems: " + ex.Message);
                return new List<BeneficiariesModel>();
            }
        }

        public BeneficiariesModel GetItem(int id)
        {
            try
            {
                return _sqliteConnection.Table<BeneficiariesModel>().FirstOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItem: " + ex.Message);
                return new BeneficiariesModel();
            }
        }

        public void DeleteItem(int id)
        {
            try
            {
                _sqliteConnection.Delete<BeneficiariesModel>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while DeleteItem: " + ex.Message);
            }
        }

        public BeneficiariesModel GetItemByMobileNumber(string mobileNumber)
        {
            try
            {
                return _sqliteConnection.Table<BeneficiariesModel>().FirstOrDefault(t => t.MobileNumber == mobileNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItemByMobileNumber: " + ex.Message);
                return null;
            }
        }
    }
}