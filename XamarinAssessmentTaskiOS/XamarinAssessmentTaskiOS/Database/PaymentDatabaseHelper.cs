using System;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using System.Collections.Generic;
using System.IO;
using XamarinAssessmentTaskiOS.Models;
using System.Linq;

namespace XamarinAssessmentTaskiOS.Database
{
    public class PaymentDatabaseHelper
    {
        private readonly SQLiteConnection sqliteConnection;
        private static PaymentDatabaseHelper instance;

        public static PaymentDatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaymentDatabaseHelper();
                }
                return instance;
            }
        }

        public PaymentDatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "payment_database.db3");
            var platform = new SQLitePlatformIOS();
            sqliteConnection = new SQLiteConnection(platform, dbPath);
            CreateTable();
        }

        private void CreateTable()
        {
            try
            {
                sqliteConnection.CreateTable<PaymentModel>();
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

        public List<PaymentModel> GetItems()
        {
            try
            {
                return sqliteConnection.Table<PaymentModel>().OrderByDescending(i => i.Id).ToList();
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
                return sqliteConnection.Table<PaymentModel>().FirstOrDefault(t => t.Id == id);
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
                sqliteConnection.Delete<PaymentModel>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while DeleteItem: " + ex.Message);
            }
        }
    }
}