using ShoppingSQLiteDatabase.Models;
using ShoppingSQLiteDatabase.Service;
using System.Collections.ObjectModel;
using System.Transactions;

namespace ShoppingSQLiteDatabase.Pages;

public partial class ProductPage : ContentPage
{
	private ShoppingDatabase _database;
	private CustomerProfile _currentCustomer;
	private ObservableCollection<ShoppingItems> _items; //{ get; set; }

	public ObservableCollection<ShoppingItems> Items
	{
		get { return _items; }
		set
		{
			_items = value;

			OnPropertyChanged();

		}
	}

	public ProductPage()
	{
		InitializeComponent();
		_database = new ShoppingDatabase();
		BindingContext = this;
		LoadData();
	}



	private void LoadData()
	{
		var shoppingItems = _database.GetAllShoppingItems();

		// _currentCustomer = _database.GetCustomerById(1);
		Items = new ObservableCollection<ShoppingItems>(_database.GetAllShoppingItems());
		//ShoppingItemsListView.BindingContext = Items;
		/*foreach (var item in shoppingItems)
		{
			item.IsSelected = false; // Or set it to whatever initial value you prefer
		}
		Items = new ObservableCollection<ShoppingItems>(shoppingItems);*/

	}

	/*private List<ShoppingItems> GetSelectedShoppingItems()
	{
		// Retrieve the selected items from the ListView
		var selectedShoppingItems = new List<ShoppingItems>();
		foreach (var shoppingItems in ShoppingItemsListView.ItemsSource)
		{
			if (shoppingItems is ShoppingItems selectedShoppingItem && selectedShoppingItems._isSelected)
			{
				selectedShoppingItems.Add(selectedShoppingItem);
			}
		}
		return selectedShoppingItems;
	}

	private async void AddShoppingItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			// Get the selected items
			List<ShoppingItems> selectedShoppingItems = GetSelectedShoppingItems();

			// Prompt the user to enter the selected quantity for each selected item
			foreach (var shoppingItems in selectedShoppingItems)
			{
				// Show an input dialog for selected quantity
				string selectedQuantityText = await DisplayPromptAsync("Enter Selected Quantity", $"Enter selected quantity for {}", "OK", "Cancel", "1", -1, Keyboard.Numeric);

				// Check if the user entered a selected quantity
				if (!string.IsNullOrEmpty(selectedQuantityText))
				{
					// Parse the selected quantity
					if (int.TryParse(selectedQuantityText, out int selectedQuantity))
					{
						// Add the item to the shopping cart with the specified selected quantity
						ShoppingCart item = new ShoppingCart
						{
							CartItemId = shoppingItems.ItemId,
							Quantity = selectedQuantity // Use the selected quantity entered by the user
						};
						_database.UpdateShoppingCart(item);
					}
					else
					{
						// Show an error message if the entered selected quantity is invalid
						await DisplayAlert("Error", "Invalid selected quantity. Please enter a valid number.", "OK");
					}




				}

			}
		}*/
}