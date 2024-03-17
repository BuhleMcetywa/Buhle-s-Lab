﻿using ShoppingSQLiteDatabase.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;
using SQLitePCL;




namespace ShoppingSQLiteDatabase.Service
{
    public class ShoppingDatabase
    {
        private SQLiteConnection _connection;

        public string GetDataBasePath()
        {
            string fileName = "shoppingdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, fileName);

        }
        public ShoppingDatabase()
        {

            _connection = new SQLiteConnection(GetDataBasePath());

            _connection.CreateTable<CustomerProfile>();
            _connection.CreateTable<ShoppingItems>();

            SeedDatabase();

        }
        public void SeedDatabase()
        {
            if (_connection.Table<CustomerProfile>().Count() == 0)
            {
                CustomerProfile customerProfile = new CustomerProfile()
                {
                    CustomerName = "Luke",
                    CustomerSurname = "Padiachy",
                    CustomerEmail = "me@computer.co.za",
                    CustomerBio = "Life is Good"
                };
                _connection.Insert(customerProfile);
            }
            if (_connection.Table<ShoppingItems>().Count() == 0)
            {
                ShoppingItems shoppingItems = new ShoppingItems()
                {
                    ItemName = "Headset",
                    Price = "R 450.00",
                    Quantity = 10,
                    ImagePath = "Images/headset.jpg"

                };
                _connection.Insert(shoppingItems);

                shoppingItems = new ShoppingItems()
                {
                    ItemName = "Studio Setup",
                    Price = "R 7000.99",
                    Quantity = 2,
                    ImagePath = "Images/studio.jpg"

                };
                _connection.Insert(shoppingItems);

                shoppingItems = new ShoppingItems()
                {
                    ItemName = "Flexirolla",
                    Price = "R 900.00",
                    Quantity = 10,
                    ImagePath = "Images/flexirolla.jpg"

                };
                _connection.Insert(shoppingItems);

            }
        }
        public CustomerProfile GetCustomerById(int Id)
        {
            CustomerProfile customerProfile = _connection.Table<CustomerProfile>().Where(x => x.CustomerProfileId == Id).FirstOrDefault();
            return customerProfile;
        }

        public void UpdateCustomer(CustomerProfile customerProfile)
        {
            _connection.Update(customerProfile);
        }

        public List<ShoppingItems> GetAllShoppingItems()
        {

            return _connection.Table<ShoppingItems>().ToList();

        }

        public void AddShoppingItemToCustomer(int customerId, int shoppingItemId)
        {
            var customer = _connection.Get<CustomerProfile>(customerId);
            var shoppingItem = _connection.Get<ShoppingItems>(shoppingItemId);

            customer.ShoppingItems.Add(shoppingItem);
            _connection.Update(customer);
        }

        public void RemoveShoppingItemFromCustomer(int customerId, int shoppingItemId)
        {
            var customer = _connection.Get<CustomerProfile>(customerId);
            var shoppingItem = _connection.Get<ShoppingItems>(shoppingItemId);

            customer.ShoppingItems.Remove(shoppingItem);
            _connection.Update(customer);
        }

    }
}