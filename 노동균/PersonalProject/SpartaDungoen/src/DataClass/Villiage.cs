
public class Villiage : IFunctions
{
    public string NameId{ set; get; }
    public List<string> FunctionIDs { private set; get; }

    public int Count
    {
        get
        {
            return FunctionIDs.Count;
        }
    }

    public Villiage()
    {
        NameId = "";
        FunctionIDs = new List<string>();
    }
}

