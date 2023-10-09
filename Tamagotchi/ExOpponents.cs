public class ExOpponents
{
    public string name;
    public int ExP;

    public List<ExOpponents> op1 = new();
    public List<ExOpponents> op2 = new();
    public List<ExOpponents> op3 = new();

    public void AddOps1()
    {
        op1.Add(new() { name = "Rose", ExP = 10 });
        op1.Add(new() { name = "Jackson", ExP = 20 });
        op1.Add(new() { name = "Pluto", ExP = 15 });
    }
    public void AddOps2()
    {
        op2.Add(new() { name = "Francis", ExP = 20 });
        op2.Add(new() { name = "Julia", ExP = 40 });
        op2.Add(new() { name = "Mr.Fluffy", ExP = 30 });
    }
    public void AddOps3()
    {
        op3.Add(new() { name = "Tagilla", ExP = 75 });
        op3.Add(new() { name = "Daisy", ExP = 45 });
        op3.Add(new() { name = "Grizzly", ExP = 60 });
    }
}
