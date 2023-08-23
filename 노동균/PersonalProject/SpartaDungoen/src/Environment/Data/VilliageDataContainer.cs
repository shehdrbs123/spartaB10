
public class VilliageDataContainer : DataReader
{
    private Dictionary<string, Villiage> _villiages;

    public VilliageDataContainer() : base()
    {
        _villiages = new Dictionary<string, Villiage>();
    }
    public override void Process(string[] data)
    {
        Villiage newVil = new Villiage();
        int i = 0;
        int functionCount;

        newVil.NameId = data[i];
        i++;

        // 마을별 기능 리스트 추가
        functionCount = int.Parse(data[i]);
        i++;
        newVil.FunctionIDs.Capacity = functionCount;
        for (int j=0; j < functionCount; ++j)
        {
            newVil.FunctionIDs.Add(data[i+j]);
        }
        _villiages.Add(newVil.NameId,newVil);
    }

    public Villiage GetVilliage(string Key)
    {
        return _villiages[Key];
    }
}