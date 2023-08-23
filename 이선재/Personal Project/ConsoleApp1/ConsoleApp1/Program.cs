using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public enum Scene { 
    None, GameIntro, MyInfo, Inventory, ManageInventory, Shop, ShopBuyItem, ShopSellItem, 
    DungeonEntry, Rest
}
internal class Program
{
    private static Scene scene = Scene.None;

    private static Character player;
    private static Equipment equipment1;
    private static Equipment equipment2;
    private static Equipment equipment3;
    private static Equipment equipment4;
    private static Equipment equipment5;
    private static Shop shop;
    private static Dungeon easyDungeon;
    private static Dungeon normalDungeon;
    private static Dungeon hardDungeon;

    static void Main(string[] args)
    {
        GameDataSetting();
        //DisplayGameIntro();
        Update();
    }

    static void Update()
    {
        while (true)
        {
            switch (scene)
            {
                case Scene.None:
                    DisplayGameIntro();
                    break;
                case Scene.GameIntro:
                    DisplayGameIntro();
                    break;
                case Scene.MyInfo:
                    DisplayMyInfo();
                    break;
                case Scene.Inventory:
                    DisplayInventory();
                    break;  
                case Scene.ManageInventory:
                    DisplayManageInventory();
                    break;
                case Scene.Shop:
                    DisplayShop();
                    break;
                case Scene.ShopBuyItem:
                    DisplayShopBuyItem();
                    break;
                case Scene.ShopSellItem:
                    DisplayShopSellItem();
                    break;
                case Scene.DungeonEntry:
                    DisplayDungeonEntry();
                    break;
                case Scene.Rest:
                    DisplayRest();
                    break;
            }
        }
    }

    static void GameDataSetting()
    {
        shop = new Shop();

        // 아이템 정보 세팅
        equipment1 = new Equipment(1, "사나이의 웨폰", 5, 500, "뜨거운 남자의 무기.");
        equipment2 = new Equipment(2, "사나이의 플레이트", 5, 1000, "뜨거운 남자의 방어구.");
        equipment3 = new Equipment(1, "테스트 검", 1, 10, "테스트용 낡은검.");
        equipment4 = new Equipment(2, "테스트 플레이트", 1, 11, "테스트용 낡은 플레이트.");
        equipment5 = new Equipment(1, "매미", 50, 555000, "시끄러움.");

        easyDungeon = new Dungeon("쉬운 던전", 5, 1000, 10);
        normalDungeon = new Dungeon("일반 던전", 11, 1700, 50);
        hardDungeon = new Dungeon("어려운 던전", 17, 2500, 100);

        shop.ShopItemList.Add(equipment1);
        shop.ShopItemList.Add(equipment2);
        shop.ShopItemList.Add(equipment3);
        shop.ShopItemList.Add(equipment4);
        shop.ShopItemList.Add(equipment5);


        // 파일없을때 에러 처리
        try
        {
            string jsonString = File.ReadAllText(@"..\..\..\gamedata.json");

            player = JsonConvert.DeserializeObject<Character>(jsonString);

            
            // 저장 후 다시 실행 시 인벤토리의 객체가 아닌 별개의 객체가 돼버리는 문제 해결용
            if(player.Weapon != null)
                player.Weapon = player.ItemList.Find(x => x.Name == player.Weapon.Name);
            if(player.Armor != null)
                player.Armor = player.ItemList.Find(x => x.Name == player.Armor.Name);
            
        }
        catch (FileNotFoundException e)
        {
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            player.ItemList.Add(equipment1);
            player.ItemList.Add(equipment2);
        }
    }

    static void GameClose()
    {
        // Json 포멧으로 시리얼라이징
        string jsonString = JsonConvert.SerializeObject(player);
        File.WriteAllText(@"..\..\..\gamedata.json", jsonString);
    }

#region Display
    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전입장");
        Console.WriteLine("5. 휴식하기");
        Console.WriteLine("6. 종료하기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 6);
        switch (input)
        {
            case 1:
                scene = Scene.MyInfo;
                break;

            case 2:
                scene = Scene.Inventory;
                break;
            case 3:
                scene = Scene.Shop;
                break;
            case 4:
                scene = Scene.DungeonEntry;
                break;
            case 5:
                scene = Scene.Rest;
                break;
            case 6:
                GameClose();
                Environment.Exit(0);
                break;
        }
    }

    static void DisplayRest()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("휴식하기");
        Console.ResetColor();
        Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold})");
        Console.WriteLine();
        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                scene = Scene.GameIntro;
                break;
            case 1:
                player.TryRest();
                break;
        }
    }

    static void DisplayDungeonClear(Dungeon dungeon)
    {
        float damage = dungeon.CalculateDamage(true, player);
        float reward = dungeon.CalculateReward(true, player);

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("던전 클리어");
        Console.ResetColor();
        Console.WriteLine("축하합니다!!");
        Console.WriteLine($"{dungeon.Name}을 클리어 하였습니다.");
        Console.WriteLine();
        Console.WriteLine("[탐험 결과]");
        Console.WriteLine($"체력 {player.Hp} -> {player.Hp - damage}");
        Console.WriteLine($"Gold {player.Gold} -> {player.Gold + reward} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        // 던전 결과 후처리
        player.GetDamage(damage);
        player.Gold += (int)reward;

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                scene = Scene.DungeonEntry;
                break;
        }
    }
    static void DisplayDungeonFail(Dungeon dungeon)
    {
        float damage = dungeon.CalculateDamage(false, player);
        float reward = dungeon.CalculateReward(false, player);

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("던전 실패");
        Console.ResetColor();
        Console.WriteLine("ㅠㅠ");
        Console.WriteLine($"{dungeon.Name}을 실패 하였습니다.");
        Console.WriteLine();
        Console.WriteLine("[탐험 결과]");
        Console.WriteLine($"체력 {player.Hp} -> {player.Hp - damage}");
        Console.WriteLine($"Gold {player.Gold} -> {player.Gold + reward} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        // 던전 결과 후처리
        player.GetDamage(damage);
        player.Gold += (int)reward;

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                scene = Scene.DungeonEntry;
                break;
        }
    }

    static void DisplayDungeonEntry()
    {
        // 플레이어의 체력이 없을 때 던전 입장을 막음
        if(player.Hp == 0)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("플레이어의 HP가 없습니다. 체력을 회복하고 입장 해 주세요!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int exitInput = CheckValidInput(0, 0);
            if (exitInput == 0)
                scene = Scene.GameIntro;

            return;
        }

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("던전입장");
        Console.ResetColor();
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 쉬운 던전");
        Console.WriteLine("2. 일반 던전");
        Console.WriteLine("3. 어려운 던전");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                scene = Scene.GameIntro;
                break;
            case 1:
                if (easyDungeon.TryDungeon(player))
                {
                    DisplayDungeonClear(easyDungeon);
                }
                else
                    DisplayDungeonFail(easyDungeon);
                break;
            case 2:
                if (normalDungeon.TryDungeon(player))
                {
                    DisplayDungeonClear(normalDungeon);
                }
                else
                    DisplayDungeonFail(normalDungeon);
                break;
            case 3:
                if (hardDungeon.TryDungeon(player))
                {
                    DisplayDungeonClear(hardDungeon);
                }
                else
                    DisplayDungeonFail(hardDungeon);
                break;
        }
    }

    static void DisplayShop()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상점");
        Console.ResetColor();
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();

        // 람다식으로 문자열길이를 내림차순 정렬, 같으면 사전 순
        shop.ShopItemList.Sort((a, b) => {
            if (a.Name.Length > b.Name.Length)
                return -1;
            else if (a.Name.Length < b.Name.Length)
                return 1;
            else
                return String.Compare(a.Name, b.Name);
        });

        for (int i = 0; i < shop.ShopItemList.Count; i++)
        {
            Equipment item = shop.ShopItemList[i];
            string itemName = item.Name.PadRight(20, ' ');
            string itemPower = (item.Type == 1) ? $"공격력 : {item.ItemPower}" : $"방어력 : {item.ItemPower}";
            string itemPrice = (player.ItemList.Find(x => x.Name == item.Name) != null) ? "구매완료" : $"{item.Price} G";

            Console.WriteLine(string.Concat($"- {itemName} | {itemPower} | {item.Explanation}".PadRight(50, ' ')," | ", itemPrice));
        }

        Console.WriteLine();
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("2. 아이템 판매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                scene = Scene.GameIntro;
                break;
            case 1:
                scene = Scene.ShopBuyItem;
                break;
            case 2:
                scene = Scene.ShopSellItem;
                break;
        }
    }
    static void DisplayShopBuyItem()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상점 - 아이템 구매");
        Console.ResetColor();
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();

        // 람다식으로 문자열길이를 내림차순 정렬, 같으면 사전 순
        shop.ShopItemList.Sort((a, b) => {
            if (a.Name.Length > b.Name.Length)
                return -1;
            else if (a.Name.Length < b.Name.Length)
                return 1;
            else
                return String.Compare(a.Name, b.Name);
        });

        for (int i = 0; i < shop.ShopItemList.Count; i++)
        {
            Equipment item = shop.ShopItemList[i];
            string itemName = item.Name.PadRight(20, ' ');
            string itemPower = (item.Type == 1) ? $"공격력 : {item.ItemPower}" : $"방어력 : {item.ItemPower}";
            string itemPrice = (player.ItemList.Find(x => x.Name == item.Name) != null) ? "구매완료" : $"{item.Price}";

            Console.WriteLine(string.Concat($"- {i+1} {itemName} | {itemPower} | {item.Explanation}".PadRight(30, ' '), " | ", itemPrice));
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, shop.ShopItemList.Count);

            if (input == 0)
            {
                scene = Scene.Shop;

                break;
            }
            else
            {
                shop.BuyItem(player, input - 1);

                Thread.Sleep(1000);

                break;
            }
        }
    }
    static void DisplayShopSellItem()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상점 - 아이템 판매");
        Console.ResetColor();
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();

        // 람다식으로 문자열길이를 내림차순 정렬, 같으면 사전 순
        player.ItemList.Sort((a, b) => {
            if (a.Name.Length > b.Name.Length)
                return -1;
            else if (a.Name.Length < b.Name.Length)
                return 1;
            else
                return String.Compare(a.Name, b.Name);
        });

        for (int i = 0; i < player.ItemList.Count; i++)
        {
            Equipment item = player.ItemList[i];
            string itemName = item.Name.PadRight(20, ' ');
            string itemPower = (item.Type == 1) ? $"공격력 : {item.ItemPower}" : $"방어력 : {item.ItemPower}";

            Console.WriteLine(string.Concat($"- {i + 1} {itemName} | {itemPower} | {item.Explanation}".PadRight(30, ' '), " | ", item.Price));
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, player.ItemList.Count);

            if (input == 0)
            {
                scene = Scene.Shop;

                break;
            }
            else
            {
                shop.SellItem(player, input - 1);

                Thread.Sleep(1000);

                break;
            }
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        
        int equipAtk = 0;
        int equipDef = 0;

        if (player.Weapon != null)
            equipAtk = player.Weapon.ItemPower;
        if (player.Armor != null)
            equipDef = player.Armor.ItemPower;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상태보기");
        Console.ResetColor();
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.GetCharacterAtk()} (+{equipAtk})");
        Console.WriteLine($"방어력 : {player.GetCharacterDef()} (+{equipDef})");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                scene = Scene.GameIntro;
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("인벤토리");
        Console.ResetColor();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        // 람다식으로 문자열길이를 내림차순 정렬, 같으면 사전 순
        player.ItemList.Sort((a,b) => {
            if (a.Name.Length > b.Name.Length)
                return -1;
            else if (a.Name.Length < b.Name.Length)
                return 1;
            else
                return String.Compare(a.Name, b.Name);
        });
        
        for(int i = 0; i < player.ItemList.Count; i++)
        {
            Equipment item = player.ItemList[i];
            string itemName = item.Name.PadRight(20, ' ');
            string equip = item.IsEquip ? "[E]" : "";
            string itemPower = (item.Type == 1) ? $"공격력 : {item.ItemPower}" : $"방어력 : {item.ItemPower}";

            Console.WriteLine($"- {equip}{itemName} | {itemPower} | {item.Explanation}");
        }

        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                scene = Scene.GameIntro;
                break;
            case 1:
                scene = Scene.ManageInventory;
                break;
        }
    }

    static void DisplayManageInventory()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.ResetColor();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < player.ItemList.Count; i++)
        {
            Equipment item = player.ItemList[i];
            string itemName = item.Name.PadRight(20, ' ');
            string equip = item.IsEquip ? $" {i + 1} [E] " : $" {i + 1} ";
            string itemPower = (item.Type == 1) ? $"공격력 : {item.ItemPower}" : $"방어력 : {item.ItemPower}";

            Console.WriteLine($"- {equip}{itemName} | {itemPower} | {item.Explanation}");
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, player.ItemList.Count);
        if (input == 0)
        {
            scene = Scene.GameIntro;

            //break;
        }
        else
        {
            Equipment equipment = player.ItemList[input - 1];

            player.ChangeEquipment(equipment);
        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}
#endregion

#region Class
public class Shop
{
    public List<Equipment> ShopItemList { get; set; }
    public Shop()
    {
        ShopItemList = new List<Equipment>();
    }

    public void BuyItem(Character player, int itemNum)
    {
        // 상점 물품을 복제해서 생성
        Equipment item = ShopItemList[itemNum].CreateClone();

        if (player.Gold < item.Price)
        {
            Console.WriteLine("Gold가 부족합니다.");
        }
        else
        {
            if (!player.ItemList.Exists(x => x.Name == item.Name))
            {
                player.ItemList.Add(item);
                player.Gold -= item.Price;
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
                Console.WriteLine("이미 구입한 아이템입니다.");
        }
    }
    
    public void SellItem(Character player, int itemNum)
    {
        Equipment item = player.ItemList[itemNum];

        if (player.ItemList.Exists(x => x.Name == item.Name))
        {
            // 장착중이라면 해제
            player.ChangeEquipment(item);

            player.ItemList.Remove(item);
            player.Gold += (int)(item.Price * 0.85f);
            Console.WriteLine("판매를 완료했습니다.");
        }
        else
            Console.WriteLine("판매할 수 없는 아이템입니다.");
    }
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public float Atk { get; set; }
    public float Def { get; set; }
    public int Hp { get; set; }
    public int Gold { get; set; }
    public int Exp { get; set; }

    public List<Equipment> ItemList { get; set; }

    // null 허용 형식
    public Equipment? Weapon { get; set; }
    public Equipment? Armor { get; set; }
    
    // 파츠별로 변경
    //public List<Equipment> EquipItemList { get; set; }

    public Character(string name, string job, int level, float atk, float def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        Exp = 0;

        ItemList = new List<Equipment>(); 
    }

    public void LevelUp()
    {
        Atk += 0.5f;
        Def += 1f;
    }


    // 휴식
    public void TryRest()
    {
        if (Hp == 100)
        {
            Console.WriteLine("이미 체력이 가득 찼습니다.");
        }
        else
        {
            if (Gold >= 500)
            {
                Hp = 100;
                Gold -= 500;
                Console.WriteLine("체력이 회복 되었습니다!");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다!");
            }
        }

        Thread.Sleep(1000);
    }

    public void GetDamage(float damage)
    {
        Hp -= (int)damage;

        if (Hp < 0)
            Hp = 0;
    }

    // 캐릭터의 공격력을 리턴
    public float GetCharacterAtk()
    {
        if (Weapon != null)
        {
            return Atk + Weapon.ItemPower;
        }
        else
            return Atk;
    }

    // 캐릭터의 방어력을 리턴
    public float GetCharacterDef()
    {
        if (Armor != null)
        {
            return Def + Armor.ItemPower;
        }
        else
            return Def;
    }

    public void ChangeEquipment(Equipment equip)
    {
        // 무기타입 장비
        if (equip.Type == 1)
        {
            // 미착용시 바로 착용
            if(Weapon == null)
            {
                Weapon = equip;
                Weapon.IsEquip = true;
            }
            else
            {
                // 이미 착용시 해제
                if(Weapon.Name == equip.Name)
                {
                    Weapon.IsEquip = false;
                    Weapon = null;
                }
                // 다른 장비 착용시 기존장비 해제 후 착용
                else
                {
                    Weapon.IsEquip = false;
                    Weapon = equip;
                    Weapon.IsEquip = true;
                }
            }
        }
        //방어구 타입 장비
        else if (equip.Type == 2)
        {
            if (Armor == null)
            {
                Armor = equip;
                Armor.IsEquip = true;
            }
            else
            {
                if (Armor.Name == equip.Name)
                {
                    Armor.IsEquip = false;
                    Armor = null;
                }
                else
                {
                    Armor.IsEquip = false;
                    Armor = equip;
                    Armor.IsEquip = true;
                }
            }
        }
    }
}

public class Equipment
{
    // Type 1: 무기 2: 방어구
    public int Type { get;}
    public string Name { get;}
    public string Explanation { get;}
    public int ItemPower;
    public int Price { get; }
    public bool IsEquip { get; set; }

    public Equipment(int type, string name, int power, int price, string explanation)
    {
        Type = type;
        Name = name;
        ItemPower = power;
        Price = price;
        Explanation = explanation;
        IsEquip = false;
    }

    public Equipment CreateClone()
    {
        return (Equipment)MemberwiseClone();
    }
}

public class Dungeon
{
    public string Name { get; }
    private int RecommendDef;
    private int RewardGold;
    private int RewardExp;

    public Dungeon(string name, int recommendDef, int rewardGold, int rewardExp)
    {
        Name = name;
        RecommendDef = recommendDef;
        RewardGold = rewardGold;
        RewardExp = rewardExp;
    }

    public bool TryDungeon(Character player)
    {
        // 권장 방어력보다 떨어질시
        if(player.GetCharacterDef() < RecommendDef)
        {
            // 40% 확률로 던전실패
            if (new Random().Next(1, 101) <= 40)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // 권장 방어력 이상 무조건 성공
        else
        {
            return true;
        }
    }

    public float CalculateDamage(bool clear, Character player)
    {
        float damage;

        if (clear)
        {
            // 권장방어력과 플레이어의 방어력 차이에 따라 데미지
            float defGap = player.GetCharacterDef() - RecommendDef;
            damage = new Random().Next(20, 36) - defGap;
        }
        else
            damage = player.Hp / 2;

        // 데미지가 플레이어의 Hp를 넘어서지 않게 처리
        return (damage < player.Hp) ? damage : player.Hp;
    } 
    public float CalculateReward(bool clear, Character player)
    {
        if (clear)
        {
            // 공격력에 따른 보너스골드
            float bonusGold = new Random().Next((int)player.GetCharacterAtk(), (int)player.GetCharacterAtk()*2) * 0.01f;
            bonusGold *= RewardGold;


            // 던전 경험치 보상
            player.Exp += RewardExp;

            if (player.Exp >= 100)
                player.LevelUp();

            return bonusGold + RewardGold;
        }
        else
            return 0;
    }
}
#endregion
