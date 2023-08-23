public class DungoenDataContainer : DataReader
{
    private Dictionary<string, Dungoen> _dungoens;

    public DungoenDataContainer()
    {
        _dungoens = new Dictionary<string, Dungoen>();
    }
    public override void Process(string[] data)
    {
        Dungoen dungoen = new Dungoen();
        dungoen.NameID = data[0];
        dungoen.BaseDefense = int.Parse(data[1]);
        dungoen.Gold = int.Parse(data[2]);
        _dungoens.Add(dungoen.NameID,dungoen);
    }

    public Dungoen GetDungoen(string NameID)
    {
        return _dungoens[NameID];
    }
}