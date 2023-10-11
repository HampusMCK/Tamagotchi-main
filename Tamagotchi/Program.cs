Console.Title = "TamaGucci";

Random gen = new();
Store ica = new Store();
Tamagotchi kid = new();
bool isinStore = false;
string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string filePath = Path.Combine(path, "saveFile.txt");
List<Action> eventList = new()
    {
    Teach,
    Hi,
    Feed,
    GoToStore,
    checkInventory,
    DressUp,
    Exhibition
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
                saveGame();
            }
        }
        Tick();
        saveGame();
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
        else if (StoreChoise == "2" || StoreChoise == "items")
        {
            Console.WriteLine($"What do you wish to buy? You have ${kid.money}");
            ica.PrintItem();
            buyItems();
        }
        else
        {
            isinStore = false;
        }
        saveGame();
    }
    saveGame();
}

Console.WriteLine($"{kid.name} Died");
Console.ReadLine();




void saveGame()
{
    var JsonSetting = new Newtonsoft.Json.JsonSerializerSettings
    {
        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
    };
    var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(kid, Newtonsoft.Json.Formatting.Indented, JsonSetting);


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
    var JsonSetting = new Newtonsoft.Json.JsonSerializerSettings
    {
        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
    };
    string content = null;
    using (StreamReader sr = new StreamReader(filePath))
    {
        content = sr.ReadToEnd();
    }

    var kidReturned = Newtonsoft.Json.JsonConvert.DeserializeObject<Tamagotchi>(content, JsonSetting);
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
        else
        {
            isinStore = false;
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
        else
        {
            isinStore = false;
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


 void Feed()
    {
        Console.WriteLine($"What would you like to feed {kid.name} with?");
        for (int i = 0; i < kid.ownedFood.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {kid.ownedFood[i].ItemName}, Hunger Decrease: {kid.ownedFood[i].HungerDecrease}");
        }
        string FeedChoise = Console.ReadLine();
        int FC;
        int.TryParse(FeedChoise, out FC);
        for (int i = 0; i < kid.ownedFood.Count; i++)
        {
            if (FC == i + 1)
            {
                kid.hunger -= kid.ownedFood[i].HungerDecrease;
                kid.ownedFood.RemoveAt(i);
            }
        }
        if (kid.hunger < 0)
        {
            kid.hunger = 0;
        }
    }

     void Hi()
    {
        int choise = gen.Next(kid.words.Count);
        if (kid.words.Count > 0)
        {
            Console.WriteLine($"{kid.name} said: {kid.words[choise]}");
        }
        else
        {
            Console.WriteLine($"{kid.name} doesn't know any words, you have to teach them some!(press enter to continue)");
            Console.ReadLine();
            Teach();
        }
        ReduceBoredom();
    }

    void Teach()
    {
        Console.Clear();
        Console.WriteLine($"What would you like to teach {kid.name}?");
        string newWord = Console.ReadLine();
        kid.words.Add(newWord);
        Console.WriteLine($"You taught {kid.name} to say '{newWord}'");
        ReduceBoredom();
    }

   void Tick()
    {
        kid.boredom += gen.Next(10);
        kid.hunger += gen.Next(7);
        if (kid.hunger >= 100 || kid.boredom >= 200)
        {
            kid.isAlive = false;
        }
        PrintStats();
    }

    void PrintStats()
    {
        string aliveTrue;
        if (kid.isAlive)
        {
            aliveTrue = "alive";
        }
        else
        {
            aliveTrue = "dead";
        }
        Console.WriteLine($"Hunger: {kid.hunger}/100, Boredom: {kid.boredom}/200, you have ${kid.money}, {kid.name} is {aliveTrue}");
    }

    bool GetAlive()
    {
        if (kid.isAlive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ReduceBoredom()
    {
        kid.boredom = kid.boredom / 2;
    }

    void DressUp()
    {
        Console.Clear();
        Console.WriteLine($"what would you like to dress {kid.name} in? {kid.name} is currently wearing {kid.dressed.Count} items");
        for (int i = 0; i < kid.ownedItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {kid.ownedItems[i].ItemName}, Exhibition Score: {kid.ownedItems[i].ExhibitionScore}");
        }
        string dressChoise = Console.ReadLine();
        int DC;
        int.TryParse(dressChoise, out DC);
        for (int i = 0; i < kid.ownedItems.Count; i++)
        {
            if (DC == i + 1)
            {
                kid.dressed.Add(kid.ownedItems[i]);
                kid.ownedItems.RemoveAt(i);
            }
        }
        if (kid.dressed.Count > 3)
        {
            Console.WriteLine($"{kid.name} is wearing to many things! {kid.dressed[0].ItemName} got taken off! press enter to continue!");
            kid.ownedItems.Add(kid.dressed[0]);
            kid.dressed.RemoveAt(0);
            Console.ReadLine();
        }
        Console.WriteLine($"{kid.name} is currently wearing:");
        for (int i = 0; i < kid.dressed.Count; i++)
        {
            Console.WriteLine($"{kid.dressed[i].ItemName}, Exh.points {kid.dressed[i].ExhibitionScore}");
        }
        ReduceBoredom();
    }

    void setExScore()
    {
        kid.ExScore = 5;
        for (int i = 0; i < kid.dressed.Count; i++)
        {
            kid.ExScore += kid.dressed[i].ExhibitionScore;
        }
    }

    void Exhibition()
    {
        Console.Clear();
        setExScore();
        int win;
        int diff;
        Console.WriteLine($"At What Grade Would You Like to Compete In? 1 (Avg. Exh.points: 15, 1st price: $30),\n2 (AVg. Exh.points: 30, 1st price: $50),\nor 3 (Avg. Exh.points: 60, 1st price: $100),\n{kid.name} has {kid.ExScore} in Exh.ponits");
        string DiffChoise = Console.ReadLine();
        int DiC;
        int.TryParse(DiffChoise, out DiC);
        if (DiC == 1)
        {
            kid.e.AddOps1();
            diff = kid.e.op1[0].ExP + kid.e.op1[1].ExP + kid.e.op1[0].ExP + kid.ExScore;
            win = gen.Next(diff);
            win -= kid.ExScore;
            if (win <= kid.ExScore)
            {
                Console.WriteLine("Congratulations! You won $30");
                kid.money += 30;
            }
            else if (win >= kid.ExScore && win <= kid.ExScore + kid.e.op1[0].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op1[0].name} );");
            }
            else if (win >= kid.ExScore + kid.e.op1[0].ExP && win <= kid.ExScore + kid.e.op1[0].ExP + kid.e.op1[1].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op1[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {kid.e.op1[2].name} );");
            }
            kid.e.op1.RemoveRange(0, 3);
        }
        else if (DiC == 2)
        {
            kid.e.AddOps2();
            diff = kid.e.op2[0].ExP + kid.e.op2[1].ExP + kid.e.op2[0].ExP + kid.ExScore;
            win = gen.Next(diff);
            win -= kid.ExScore;
            if (win <= kid.ExScore)
            {
                Console.WriteLine("Congratulations! You won $50");
                kid.money += 50;
            }
            else if (win >= kid.ExScore && win <= kid.ExScore + kid.e.op2[0].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op2[0].name} );");
            }
            else if (win >= kid.ExScore + kid.e.op2[0].ExP && win <= kid.ExScore + kid.e.op2[0].ExP + kid.e.op2[1].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op2[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {kid.e.op2[2].name} );");
            }
            kid.e.op2.RemoveRange(0, 3);
        }
        else if (DiC == 3)
        {
            kid.e.AddOps3();
            diff = kid.e.op3[0].ExP + kid.e.op3[1].ExP + kid.e.op3[0].ExP + kid.ExScore;
            win = gen.Next(diff);
            win -= kid.ExScore;
            if (win <= kid.ExScore)
            {
                Console.WriteLine("Congratulations! You won $100");
                kid.money += 100;
            }
            else if (win >= kid.ExScore && win <= kid.ExScore + kid.e.op3[0].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op3[0].name} );");
            }
            else if (win >= kid.ExScore + kid.e.op3[0].ExP && win <= kid.ExScore + kid.e.op3[0].ExP + kid.e.op3[1].ExP)
            {
                Console.WriteLine($"You Lost to {kid.e.op3[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {kid.e.op3[2].name} );");
            }
            kid.e.op3.RemoveRange(0, 3);
        }
        ReduceBoredom();
    }