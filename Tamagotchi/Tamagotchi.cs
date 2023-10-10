class Tamagotchi
{
    public int hunger;
    public int boredom;
    public int money = 100;
    public List<string> words = new();
    public List<Store> ownedFood = new();
    public List<Store> ownedItems = new();
    public List<Store> dressed = new();
    public bool isAlive = true;
    public string name;
    public int ExScore = 5;
    Random gen = new();
    public ExOpponents e = new();
}
