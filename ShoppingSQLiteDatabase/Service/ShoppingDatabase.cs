using ShoppingSQLiteDatabase.Models;
using SQLite;




namespace ShoppingSQLiteDatabase.Service
{
    public class ShoppingDatabase
    {
        private SQLiteConnection _dbConnection;

        public string GetDataBasePath()
        {
            string fileName = "shoppingdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, fileName);

        }
        public ShoppingDatabase()
        {

            _dbConnection = new SQLiteConnection(GetDataBasePath());

			_dbConnection.CreateTable<CustomerProfile>();
            _dbConnection.CreateTable<ShoppingItems>();
            _dbConnection.CreateTable<ShoppingCart>();

            SeedDatabase();

        }
        public void SeedDatabase()
        {
            if (_dbConnection.Table<CustomerProfile>().Count() == 0)
            {
                CustomerProfile customerProfile = new CustomerProfile()
                {
                    CustomerName = "Nic-Quinlynn",
                    CustomerSurname = "Titus",
                    CustomerEmail = "nictitus@gmail.com",
                    CustomerPracticeNo = "010929040708CAF",
                    CustomerBio = " BScHons Chemical Sciences Graduate "
                };
                _dbConnection.Insert(customerProfile);
            }
            if (_dbConnection.Table<ShoppingItems>().Count() == 0)
            {
                List<ShoppingItems> items = new List<ShoppingItems>()
                {
                    new ShoppingItems()
                    {
                        ItemName = "Petridish set",
                        Price = "R 800.00",
                        Quantity = 30,
                        ImagePath = "petridish.jpg"
                    },

                    new ShoppingItems()
                    {
                        ItemName = "Nitrogen gas",
                        Price = "R 2999.99",
                        Quantity = 50,
                        ImagePath = "nitrogen.png"
                    },

                    new ShoppingItems()
                    {
                        ItemName = "MonoEthyleneGlycol",
                        Price = "R 650.00",
                        Quantity = 66,
                        ImagePath = "monoethyleneglycol.jpg"
                    },

                    new ShoppingItems()
                    {
						ItemName = "Funnel",
						Price = "R 65.00",
						Quantity = 60,
						ImagePath = "funnel.jpg"
					},

                    new ShoppingItems()
                    {
						ItemName = "Sulfuric Acid",
						Price = "R 700.00",
						Quantity = 67,
						ImagePath = "sulfuricacid.jpeg"
					},

                    new ShoppingItems()
                    {
						ItemName = "Conical flask set",
						Price = "R 450.00",
						Quantity = 66,
						ImagePath = "conicalflask.jpeg",
					},

                    new ShoppingItems()
                    {
                        ItemName = "Test tube set",
                        Price = "R100",
                        Quantity = 50,
                        ImagePath = "testtube.jpeg"
                    }
                };
                _dbConnection.InsertAll(items);
            }
        }

        public CustomerProfile GetCustomerById(int Id)
        {
            CustomerProfile customerProfile = _dbConnection.Table<CustomerProfile>().Where(x => x.CustomerProfileId == Id).FirstOrDefault();
            return customerProfile;
        }

        public void UpdateCustomer(CustomerProfile customerProfile)
        {
            _dbConnection.Update(customerProfile);
        }

        public List<ShoppingItems> GetAllShoppingItems()
        {

            return _dbConnection.Table<ShoppingItems>().ToList();

        }

        /*public void AddShoppingItemToCustomer(int customerId, int shoppingItemId)
        {
            var customer = _dbConnection.Get<CustomerProfile>(customerId);
            var shoppingItem = _dbConnection.Get<ShoppingItems>(shoppingItemId);

            customer.ShoppingItems.Add(shoppingItem);
            _dbConnection.Update(customer);
        }

        public void RemoveShoppingItemFromCustomer(int customerId, int shoppingItemId)
        {
            var customer = _dbConnection.Get<CustomerProfile>(customerId);
            var shoppingItem = _dbConnection.Get<ShoppingItems>(shoppingItemId);

            customer.ShoppingItems.Remove(shoppingItem);
            _dbConnection.Update(customer);
        }*/
        public List<ShoppingCart> GetCustomerCartItems()
        {

            int customerId = GetCurrentCustomerId(); 

            
            List<ShoppingCart> cartItems = _dbConnection.Table<ShoppingCart>().Where(item => item.CustomerProfileId == customerId).ToList();

            return cartItems;
        }
        private int GetCurrentCustomerId()
        {

            return 1;


        }

        public void UpdateShoppingCart(ShoppingCart cart)
        {
            var existingItem= _dbConnection.Table<ShoppingCart>().FirstOrDefault(c=>c.CartItemId==cart.CartItemId);

            if(existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Quantity = 1;
                _dbConnection.Insert(cart);
            }
        }
		

		
	}
}