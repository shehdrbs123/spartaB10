public enum EStatus
{
    Level, Attack, Defense,Health,Gold
}
public interface IStatus
{
    public int Level { get; set; }
    public string Job { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    
    public int ExtraAttack { get; set; }
    public int ExtraDefense { get; set; }
    public int ExtraHealth { get; set; }
}

public class Status : IStatus
{
    public Status()
    {
        Level = 1;
        Job = "전사";
        Attack = 10;
        Defense = 5;
        Health = 100;
        Gold = 1500;
        ExtraAttack = 0;
        ExtraDefense = 0;
        ExtraHealth = 0;
    }

    public void AddExtra(EStatus type, int value)
    {
        switch (type)
        {
            case EStatus.Attack :
                ExtraAttack += value;
                break;
            case EStatus.Defense :
                ExtraDefense += value;
                break;
            case EStatus.Health :
                ExtraHealth += value;
                break;
        }
    }
    
    public int Level { get; set; }
    public string Job { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int ExtraAttack { get; set; }
    public int ExtraDefense { get; set; }
    public int ExtraHealth { get; set; }
}