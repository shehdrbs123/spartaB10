public class FunctionListContainer : DataReader
{
    private Dictionary<string, IFunctions> _functionsLists;

    public FunctionListContainer() : base()
    {
        _functionsLists = new Dictionary<string, IFunctions>();
    }
    public override void Process(string[] data)
    {
        IFunctions functionList = new FunctionList();
        int i = 0;
        int functionCount;
        
        i++;

        // 마을별 기능 리스트 추가
        functionCount = int.Parse(data[i]);
        i++;
        functionList.FunctionIDs.Capacity = functionCount;
        for (int j=0; j < functionCount; ++j)
        {
            functionList.FunctionIDs.Add(data[i+j]);
        }

        _functionsLists.Add(data[0],functionList);
        
    }

    public List<string> GetFunctionList(string Key)
    {
        return _functionsLists[Key].FunctionIDs;
    }
}