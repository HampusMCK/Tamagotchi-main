Console.Title = "TamaGucci";

Tamagotchi kid = new();
store ica = new();
bool isinStore = false;
List<Action> eventList = new()
    {
    kid.Teach,
    kid.Hi,
    kid.Feed,
    GoToStore,
    checkInventory,
    kid.DressUp,
    kid.Exhibition
    };

Console.WriteLine("Name Your TamaGucci!");
kid.name = Console.ReadLine();
ica.addItems();
while (kid.isAlive)
{

    while (kid.isAlive && isinStore == false)
    {
        Console.Clear();

        Console.WriteLine($"What do you wish to do? 1.Teach, 2.Talk, 3.Feed, 4.go to the store, 5.Check your Inventory, 6.Dress up {kid.name}, 7.Enter an Exhibition?. Answer with a number!!");
        string answer = Console.ReadLine();
        int answerInt;
        int.TryParse(answer, out answerInt);
        Console.Clear();

        for (int i = 0; i < eventList.Count; i++)
        {
            if (answerInt == i + 1)
            {
                eventList[i]();
            }
        }
        kid.Tick();
        Console.ReadLine();
    }

    while (kid.isAlive && isinStore)
    {
        Console.Clear();
        Console.WriteLine("What do you want to buy? 1. Food or 2. Items");
        string StoreChoise = Console.ReadLine();
        if (StoreChoise == "1" || StoreChoise == "food")
        {
            Console.WriteLine($"What do you wish to buy? You have ${kid.money}");
            ica.PrintFood();
            buyFood();
        }
        if (StoreChoise == "2" || StoreChoise == "items")
        {
            Console.WriteLine($"What do you wish to buy? You have ${kid.money}");
            ica.PrintItem();
            buyItems();
        }
    }

}

Console.WriteLine($"{kid.name} Died");
Console.ReadLine();



void GoToStore()
{
    isinStore = true;
}


void buyFood()
{
    string buyChoise = Console.ReadLine();
    int BC;
    int.TryParse(buyChoise, out BC);
    for (int i = 0; i < ica.StockItems.Count; i++)
    {
        if (BC == i + 1)
        {
            kid.money -= ica.StockItems[i].ItemPrice;
            if (kid.money < 0)
            {
                kid.money += ica.StockItems[i].ItemPrice;
                Console.WriteLine($"That is too expensive for you, you have ${kid.money}");
            }
            else
            {
                kid.ownedFood.Add(ica.StockItems[i]);
                Console.WriteLine($"You just bought 1 {ica.StockItems[i].ItemName}");
                Console.WriteLine("would you like to buy something more? Answer y or n");
                string buyAgain = Console.ReadLine().ToLower();
                if (buyAgain == "y")
                {
                }
                else
                {
                    isinStore = false;
                }
            }
        }
    }
}

void buyItems()
{
    string buyChoise = Console.ReadLine();
    int BC;
    int.TryParse(buyChoise, out BC);
    if (BC > 0)
    {
        BC += 4;
    }
    for (int i = 0; i < ica.StockItems.Count; i++)
    {
        if (BC == i + 1)
        {
            kid.money -= ica.StockItems[i].ItemPrice;
            if (kid.money < 0)
            {
                kid.money += ica.StockItems[i].ItemPrice;
                Console.WriteLine($"That is too expensive for you, you have ${kid.money}");
            }
            else
            {
                kid.ownedItems.Add(ica.StockItems[i]);
                Console.WriteLine($"You just bought 1 {ica.StockItems[i].ItemName}");
                ica.StockItems.RemoveAt(i);
                Console.WriteLine("would you like to buy something more? Answer y or n");
                string buyAgain = Console.ReadLine().ToLower();
                if (buyAgain == "y")
                {
                }
                else
                {
                    isinStore = false;
                }
            }
        }
    }
}



void checkInventory()
{
    Console.WriteLine("You own These Items:");
    for (int i = 0; i < kid.ownedItems.Count; i++)
    {
        Console.WriteLine($"{kid.ownedItems[i].ItemName}");
    }
    for (int i = 0; i < kid.ownedFood.Count; i++)
    {
        Console.WriteLine($"{kid.ownedFood[i].ItemName}");
    }
}