Console.Title = "TamaGucci";

Store ica = new Store();
List<Tamagotchi> kidList = new();
Tamagotchi kid = new();
bool isinStore = false;
string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string filePath = Path.Combine(path, "saveFile.txt");
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

if (File.Exists(filePath))
{
    Console.WriteLine("New Game or Load Game? answer 1 or 2");
    string NoL = Console.ReadLine();
    if (NoL == "2")
    {
        loadGame();
    }
}

if (kid.name == null)
{
    Console.Clear();
    Console.WriteLine("Name Your TamaGucci!");
    kid.name = Console.ReadLine();
}
ica.addItems();
while (kid.isAlive)
{

    while (kid.isAlive && isinStore == false)
    {
        Console.Clear();

        Console.WriteLine($"What do you wish to do? 1.Teach, 2.Talk, 3.Feed, 4.go to the Store,\n5.Check your Inventory, 6.Dress up {kid.name}, 7.Enter an Exhibition?. Answer with a number!!");
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
        saveGame();
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
        saveGame();
    }
    saveGame();
}

Console.WriteLine($"{kid.name} Died");
Console.ReadLine();





void saveGame()
{
    var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(kid, Newtonsoft.Json.Formatting.Indented);


    using (StreamWriter sw = new StreamWriter(filePath))
    {
        sw.Write(serializedObject);
    }


    string content = null;
    using (StreamReader sr = new StreamReader(filePath))
    {
        content = sr.ReadToEnd();
    }

    var kidReturned = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
}

void loadGame()
{
    string content = null;
    using (StreamReader sr = new StreamReader(filePath))
    {
        content = sr.ReadToEnd();
    }

    var kidReturned = Newtonsoft.Json.JsonConvert.DeserializeObject<Tamagotchi>(content);
    kid = kidReturned;
}

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