class Tamagotchi
{
    ExOpponents e = new();
    int hunger;
    int boredom;
    int ExScore = 5;
    public int money = 100;
    List<string> words = new();
    public List<store> ownedFood = new();
    public List<store> ownedItems = new();
    public List<store> dressed = new();
    public bool isAlive = true;
    Random gen = new();
    public string name;

    public void Feed()
    {
        Console.WriteLine($"What would you like to feed {name} with?");
        for (int i = 0; i < ownedFood.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ownedFood[i].ItemName}, Hunger Decrease: {ownedFood[i].HungerDecrease}");
        }
        string FeedChoise = Console.ReadLine();
        int FC;
        int.TryParse(FeedChoise, out FC);
        for (int i = 0; i < ownedFood.Count; i++)
        {
            if (FC == i + 1)
            {
                hunger -= ownedFood[i].HungerDecrease;
                ownedFood.RemoveAt(i);
            }
        }
        if (hunger < 0)
        {
            hunger = 0;
        }
    }

    public void Hi()
    {
        int choise = gen.Next(words.Count);
        if (words.Count > 0)
        {
            Console.WriteLine($"{name} said: {words[choise]}");
        }
        else
        {
            Console.WriteLine($"{name} doesn't know any words, you have to teach them some!(press enter to continue)");
            Console.ReadLine();
            Teach();
        }
        ReduceBoredom();
    }

    public void Teach()
    {
        Console.Clear();
        Console.WriteLine($"What would you like to teach {name}?");
        string newWord = Console.ReadLine();
        words.Add(newWord);
        Console.WriteLine($"You taught {name} to say '{newWord}'");
        ReduceBoredom();
    }

    public void Tick()
    {
        boredom += gen.Next(10);
        hunger += gen.Next(7);
        if (hunger >= 100 || boredom >= 200)
        {
            isAlive = false;
        }
        PrintStats();
    }

    public void PrintStats()
    {
        string aliveTrue;
        if (isAlive)
        {
            aliveTrue = "alive";
        }
        else
        {
            aliveTrue = "dead";
        }
        Console.WriteLine($"Hunger: {hunger}/100, Boredom: {boredom}/200, you have ${money}, {name} is {aliveTrue}");
    }

    public bool GetAlive()
    {
        if (isAlive)
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
        boredom = boredom / 2;
    }

    public void DressUp()
    {
        Console.Clear();
        Console.WriteLine($"what would you like to dress {name} in? {name} is currently wearing {dressed.Count} items");
        for (int i = 0; i < ownedItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ownedItems[i].ItemName}, Exhibition Score: {ownedItems[i].ExhibitionScore}");
        }
        string dressChoise = Console.ReadLine();
        int DC;
        int.TryParse(dressChoise, out DC);
        for (int i = 0; i < ownedItems.Count; i++)
        {
            if (DC == i + 1)
            {
                dressed.Add(ownedItems[i]);
                ownedItems.RemoveAt(i);
            }
        }
        if (dressed.Count > 3)
        {
            Console.WriteLine($"{name} is wearing to many things! {dressed[0].ItemName} got taken off! press enter to continue!");
            ownedItems.Add(dressed[0]);
            dressed.RemoveAt(0);
            Console.ReadLine();
        }
        Console.WriteLine($"{name} is currently wearing:");
        for (int i = 0; i < dressed.Count; i++)
        {
            Console.WriteLine($"{dressed[i].ItemName}, Exh.points {dressed[i].ExhibitionScore}");
        }
        ReduceBoredom();
    }

    public void setExScore()
    {
        ExScore = 5;
        for (int i = 0; i < dressed.Count; i++)
        {
            ExScore += dressed[i].ExhibitionScore;
        }
    }

    public void Exhibition()
    {
        Console.Clear();
        setExScore();
        int win;
        int diff;
        Console.WriteLine($"At What Grade Would You Like to Compete In? 1 (Avg. Exh.points: 15, 1st price: $30), 2 (AVg. Exh.points: 30, 1st price: $50), or 3 (Avg. Exh.points: 60, 1st price: $100), {name} has {ExScore} in Exh.ponits");
        string DiffChoise = Console.ReadLine();
        int DiC;
        int.TryParse(DiffChoise, out DiC);
        if (DiC == 1)
        {
            e.AddOps1();
            diff = e.op1[0].ExP + e.op1[1].ExP + e.op1[0].ExP + ExScore;
            win = gen.Next(diff);
            win -= ExScore;
            if (win <= ExScore)
            {
                Console.WriteLine("Congratulations! You won $30");
                money += 30;
            }
            else if (win >= ExScore && win <= ExScore + e.op1[0].ExP)
            {
                Console.WriteLine($"You Lost to {e.op1[0].name} );");
            }
            else if (win >= ExScore + e.op1[0].ExP && win <= ExScore + e.op1[0].ExP + e.op1[1].ExP)
            {
                Console.WriteLine($"You Lost to {e.op1[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {e.op1[2].name} );");
            }
            e.op1.RemoveRange(0, 3);
        }
        else if (DiC == 2)
        {
            e.AddOps2();
            diff = e.op2[0].ExP + e.op2[1].ExP + e.op2[0].ExP + ExScore;
            win = gen.Next(diff);
            win -= ExScore;
            if (win <= ExScore)
            {
                Console.WriteLine("Congratulations! You won $50");
                money += 50;
            }
            else if (win >= ExScore && win <= ExScore + e.op2[0].ExP)
            {
                Console.WriteLine($"You Lost to {e.op2[0].name} );");
            }
            else if (win >= ExScore + e.op2[0].ExP && win <= ExScore + e.op2[0].ExP + e.op2[1].ExP)
            {
                Console.WriteLine($"You Lost to {e.op2[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {e.op2[2].name} );");
            }
            e.op2.RemoveRange(0, 3);
        }
        else if (DiC == 3)
        {
            e.AddOps3();
            diff = e.op3[0].ExP + e.op3[1].ExP + e.op3[0].ExP + ExScore;
            win = gen.Next(diff);
            win -= ExScore;
            if (win <= ExScore)
            {
                Console.WriteLine("Congratulations! You won $100");
                money += 100;
            }
            else if (win >= ExScore && win <= ExScore + e.op3[0].ExP)
            {
                Console.WriteLine($"You Lost to {e.op3[0].name} );");
            }
            else if (win >= ExScore + e.op3[0].ExP && win <= ExScore + e.op3[0].ExP + e.op3[1].ExP)
            {
                Console.WriteLine($"You Lost to {e.op3[1].name} );");
            }
            else
            {
                Console.WriteLine($"You Lost to {e.op3[2].name} );");
            }
            e.op3.RemoveRange(0, 3);
        }
        ReduceBoredom();
    }
}
