using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    class Pos
    {
        public List<Item> Items;
        public Dictionary<int, BoughtItem> BoughtItems;
        public Dictionary<int, int> StockItem = new Dictionary<int, int>();

        public string LoginChoicePrompt = "Enter your choice for login";
        public string LoginChoiceErrorPrompt = "Wrong choice. Please try again for login";

        public void Begin()
        {
            Console.WriteLine("Enter 0 for login as admin or 1 for login as customer");
            int loginChoice = TakeUserInput(LoginChoicePrompt, LoginChoiceErrorPrompt);
            if (loginChoice == 0)
            {
                Console.WriteLine("Login as Admin");
            }
            else if (loginChoice == 1)
            {
                Console.WriteLine("Login as Customer");
            }
            else
            {
                Begin();
            }
        }
        public void DefaultInit()
        {
            Items = new List<Item>()
            {
                new Item(){ Id = 1, ItemName = "Pen", ItemPrice = 5, ItemStock = 10 },
                new Item(){ Id = 2, ItemName = "Book", ItemPrice = 100, ItemStock = 15 },
                new Item(){ Id = 3, ItemName = "Rice", ItemPrice = 50, ItemStock = 20 },
                new Item(){ Id = 4, ItemName = "Cap", ItemPrice = 25, ItemStock = 10 }
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
