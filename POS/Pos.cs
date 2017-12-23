using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public enum AdminOperation
    {
        AddItem = 1,
        UpdateItem = 2,
        DisplayItem = 3,
        Logout = 4
    }
    class Pos
    {
        public List<Item> Items;
        public Dictionary<int, BoughtItem> BoughtItems;
        public Dictionary<int, int> StockItem = new Dictionary<int, int>();
        public int Sum = 0;

        public string LoginChoicePrompt = "Enter your choice for login";
        public string LoginChoiceErrorPrompt = "Wrong choice. Please try again for login";
        public string AdminChoiceErrorPrompt = "Wrong choice! Try again";
        public string AdminChoicePrompt = "Enter your choice";
        public string QuantityInputPrompt = "Enter quantity";
        public string QuantityErrorPrompt = "Wrong choice! Try again";
        public string BuyPrompt = "What you want to buy";
        public string BuyErrorPrompt = "Wrong choice! Try again";
        public string ViewCartPrompt = "Enter 0 for view cart";
        public string ItemNotFound = "Item not found! Try again";

        public void Begin()
        {
            Console.WriteLine("Enter 0 for login as admin or 1 for login as customer");
            int loginChoice = TakeUserInput(LoginChoicePrompt, LoginChoiceErrorPrompt);
            switch (loginChoice)
            {
                case 0:
                    Console.WriteLine("Login as Admin");
                    DisplayItem();
                    AdminOperation();
                    break;
                case 1:
                    Console.WriteLine("Login as Customer");
                    CustomerOperation();
                    break;
                default:
                    Begin();
                    break;
            }
        }
        public void CustomerOperation()
        {
            DisplayItem();
            Console.WriteLine(ViewCartPrompt);
            int choice = TakeUserInput(BuyPrompt, BuyErrorPrompt);

            switch (choice)
            {
                case 0:
                    DisplayCart(BoughtItems);
                    break;
                default:
                    Item getItem = GetItem(choice);
                    if (getItem == null)
                    {
                        Console.WriteLine(ItemNotFound);
                        CustomerOperation();
                    }
                    else
                    {
                        AddToCart(getItem);
                        DisplayItem();
                    }
                    break;
            }
        }
        public Item GetItem(int choice)
        {
            foreach (Item t in Items)
            {
                if (choice == t.Id)
                    return t;
            }
            return null;
        }
        public void AddToCart(Item item)
        {
            string itemName = item.ItemName;
            Console.Write("Item {0} found. ", itemName);
            int quantity = TakeUserInput(QuantityInputPrompt, QuantityErrorPrompt);

            if (item.ItemStock >= quantity)
            {
                Console.WriteLine("Product buy");
                StockCheck(item, quantity);
                item.ItemStock -= quantity;
                CustomerOperation();
            }
            else
            {
                Console.WriteLine("{0} {1}", quantity, itemName + " is not in stock");
                CustomerOperation();
            }
        }
        public void StockCheck(Item item, int quantity)
        {
            if (!BoughtItems.ContainsKey(item.Id))
            {
                BoughtItems.Add(item.Id, new BoughtItem { Id = item.Id, Quantity = quantity, Item = item });
            }
            else
            {
                BoughtItems[item.Id].Quantity += quantity;
            }
        }
        public void DisplayCart(Dictionary<int, BoughtItem> boughtItemList)
        {
            int total = 0;
            Console.WriteLine("\n-----------------------------------------Display Cart---------------------------------------\n");
            Console.WriteLine("Item\t\tQuantity\t\tUnit Price\t\tSum");
            foreach (var pair in boughtItemList)
            {
                Sum += pair.Value.Quantity;
                int price = pair.Value.Quantity * pair.Value.Item.ItemPrice;
                Console.WriteLine(pair.Value.Item.ItemName + "\t\t" + pair.Value.Quantity + "\t\t\t" + pair.Value.Item.ItemPrice + "\t\t\t" + price);
                total += price;
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("Total Payment\t\t\t\t\t\t\t{0}", total);

            Console.WriteLine("\nFor shop again enter 0 or logout enter 1");
            int choice = TakeUserInput("Enter your choice", "Wrong input");
            if (choice == 0)
            {
                DisplayItem();
                CustomerOperation();
            }
            else
            {
                Begin();
            }
        }
        public void AdminOperation()
        {
            Console.WriteLine("Enter 1 for Add new item 2 for update existing stock 3 for display item list 4 for logout");
            int adminChoice = TakeUserInput(AdminChoicePrompt, AdminChoiceErrorPrompt);
            switch (adminChoice)
            {
                case (int)POS.AdminOperation.AddItem:
                    AddItem();
                    break;
                case (int)POS.AdminOperation.UpdateItem:
                    UpdateItem();
                    break;
                case (int)POS.AdminOperation.DisplayItem:
                    DisplayItem();
                    AdminOperation();
                    break;
                case (int)POS.AdminOperation.Logout:
                    Begin();
                    DisplayItem();
                    break;
                default:
                    Console.WriteLine(AdminChoiceErrorPrompt);
                    AdminOperation();
                    break;
            }
        }
        public void AddItem()
        {
            Console.WriteLine("Enter item name: ");
            string name = Console.ReadLine();
            int price = TakeUserInput("Enter price", "Wrong! Enter correct price");
            int quantity = TakeUserInput("Enter quantity", "Wrong! Enter correct quantity");

            Items.Add(new Item { Id = Items.Count + 1, ItemName = name, ItemPrice = price, ItemStock = quantity });
            Console.WriteLine("Item added successfully");
            AdminOperation();
        }
        public void UpdateItem()
        {
            var input = TakeUserInput("Select item to add stock", AdminChoicePrompt);
            if (input != 4)
                if (input <= Items.Count)
                {
                    int quantity = TakeUserInput(QuantityInputPrompt, QuantityErrorPrompt);
                    if (quantity > 0)
                        if (Items != null) Items[input - 1].ItemStock += quantity;
                    DisplayItem();
                    AdminOperation();
                    return;
                }
                else
                {
                    Console.WriteLine(AdminChoiceErrorPrompt);
                    AdminOperation();
                }
            else
                DisplayItem();
            AdminOperation();
        }
        public void DisplayItem()
        {
            Console.WriteLine("Products");
            Console.WriteLine("===========================");
            Console.WriteLine("No\tItem\t\tPrice\t InStock");
            Console.WriteLine("---------------------------------------------");
            foreach (var item in Items)
            {
                Console.WriteLine(item.Id + "\t" + item.ItemName + "\t\t" + item.ItemPrice + "\t" + item.ItemStock);
            }
        }
        public void DefaultInit()
        {
            Items = new List<Item>
            {
                new Item{ Id = 1, ItemName = "Pen", ItemPrice = 5, ItemStock = 10 },
                new Item{ Id = 2, ItemName = "Book", ItemPrice = 100, ItemStock = 15 },
                new Item{ Id = 3, ItemName = "Rice", ItemPrice = 50, ItemStock = 20 },
                new Item{ Id = 4, ItemName = "Cap", ItemPrice = 25, ItemStock = 10 }
            };
            BoughtItems = new Dictionary<int, BoughtItem>();
        }
        public int TakeUserInput(string inputPrompt, string errorPrompt)
        {
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                Console.WriteLine(errorPrompt);
                return TakeUserInput(inputPrompt, errorPrompt);
            }
        }
    }
}
