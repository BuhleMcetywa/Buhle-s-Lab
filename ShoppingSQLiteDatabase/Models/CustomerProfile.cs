using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ShoppingSQLiteDatabase.Models
{
    public class CustomerProfile
    {
        [PrimaryKey,AutoIncrement]
        public int CustomerProfileId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }

        public string CustomerBio { get; set; }

        public string CustomerPracticeNo { get; set; }

        [ForeignKey(typeof(ShoppingCart))]
        public int? CartItemId { get; set; }

        [ManyToOne]
        public ShoppingCart ShoppingCart { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ShoppingItems> ShoppingItems { get; set; }

        public CustomerProfile()
        {
            ShoppingItems = new List<ShoppingItems>();
        }
    }
}
