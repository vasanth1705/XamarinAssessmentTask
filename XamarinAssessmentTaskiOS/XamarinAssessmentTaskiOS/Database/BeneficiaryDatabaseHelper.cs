using System;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinIOS;
using System.Collections.Generic;
using XamarinAssessmentTaskiOS.Models;
using System.Linq;

namespace XamarinAssessmentTaskiOS.Database
{
	public class BeneficiaryDatabaseHelper
	{
        private readonly SQLiteConnection sqliteConnection;
        private static BeneficiaryDatabaseHelper instance;

        public static BeneficiaryDatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BeneficiaryDatabaseHelper();
                }
                return instance;
            }
        }

        public BeneficiaryDatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "beneficiary_database.db3");
            var platform = new SQLitePlatformIOS();
            sqliteConnection = new SQLiteConnection(platform, dbPath);
            CreateTable();
        }

        private void CreateTable()
        {
            try
            {
                sqliteConnection.CreateTable<BeneficiariesModel>();
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
                    sqliteConnection.Update(item);
                }
                else
                {
                    sqliteConnection.Insert(item);
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
                return sqliteConnection.Table<BeneficiariesModel>().ToList();
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
                return sqliteConnection.Table<BeneficiariesModel>().FirstOrDefault(t => t.Id == id);
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
                sqliteConnection.Delete<BeneficiariesModel>(id);
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
                return sqliteConnection.Table<BeneficiariesModel>().FirstOrDefault(t => t.MobileNumber == mobileNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving GetItemByMobileNumber: " + ex.Message);
                return null;
            }
        }
    }
}