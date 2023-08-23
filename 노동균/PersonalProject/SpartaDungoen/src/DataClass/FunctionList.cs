public class FunctionList : IFunctions
{
    public List<string> FunctionIDs { private set; get; }
    
    public FunctionList()
    {
        FunctionIDs = new List<string>();
    }
}