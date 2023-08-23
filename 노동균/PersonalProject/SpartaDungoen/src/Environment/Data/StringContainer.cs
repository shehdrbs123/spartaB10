
public class StringContainer : DataReader
{
    protected Dictionary<string, string> dicStringData = new Dictionary<string, string>();

    public string GetString(string key)
    {
        string value;
        if (!dicStringData.TryGetValue(key, out value))
        {
            Console.WriteLine("{0} : {1}는 존재하지 않는 키 입니다. ", typeof(StringContainer).ToString(), key);
        }

        value = value.Replace("\\n", "\n");

        return value;
    }
    public override void Process(string[] data)
    {
        dicStringData.Add(data[0], data[1]);

    }
}