using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PrivateProject
{
    internal class Program
    {
        static Player player;
        static Store store;

        static void Main(string[] args)
        {
            string name = "조범준";
            string job = "전사";
            Item item;
            player = new Player(name, job);
            store = new Store(player);
            
            // 플레이어 아이템 추가
            item = new Item("무쇠갑옷", 9, "방어구", true, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
            player.ItemAdd(item);
            item = new Item("낡은 검", 2, "무기", true, "쉽게 볼 수 있는 낡은 검입니다.", 600);
            player.ItemAdd(item);
            item = new Item("좋은 검", 4, "무기", true, "쉽게 볼 수 없는 좋은 검입니다.", 1000);
            player.ItemAdd(item);
            item = new Item("가죽갑옷", 3, "방어구", true, "가죽으로 만들어져 매끄러운 갑옷입니다.", 800);
            player.ItemAdd(item);

            // 상점 아이템 추가
            item = new Item("수련자 갑옷", 5, "방어구", false, "수련에 도움을 주는 갑옷입니다.", 1000);
            store.StoreItemAdd(item);
            item = new Item("무쇠갑옷", 9, "방어구", false, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
            store.StoreItemAdd(item);
            item = new Item("스파르타의 갑옷", 15, "방어구", false, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            store.StoreItemAdd(item);
            item = new Item("낡은 검", 2, "무기", false, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            store.StoreItemAdd(item);
            item = new Item("청동 도끼", 5, "무기", false, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
            store.StoreItemAdd(item);
            item = new Item("스파르타의 창", 7, "무기", false, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500);
            store.StoreItemAdd(item);


            while (true){
                StartScene();
            }
        }

        public static void StartScene()     //메인 화면 창
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"보유 골드 {player.playerGold}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"현재 체력 {player.playerHp}\n");

            Console.WriteLine("1. 상태 보기");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("2. 인벤토리");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("3. 상점");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4. 던전입장");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("5. 휴식하기 ( 500 G )\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            /*bool선택이유
            만약 입력받는 숫자가 0일때 문자를 입력해도 0으로 취급해 넘어가는 것을 방지하기위함
            행동 번호들이 늘어나면 아래 if문에 계속 추가하여 사용해도 된다
            */

            if (actionNum < 1 || actionNum > 5)
                isNum = false;

            while (!isNum)
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum < 1 || actionNum > 5)
                {
                    isNum = false;
                }
            }

            Console.WriteLine();

            if (actionNum == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================");
                Console.WriteLine("    상태 보기 창으로 이동합니다.");
                Console.WriteLine("====================================");
                Thread.Sleep(1000);
                player.StatusWindow();
            }
            else if(actionNum == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("====================================");
                Console.WriteLine("     인벤토리 창으로 이동합니다.");
                Console.WriteLine("====================================");
                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)       //인벤토리창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = player.inventoryWindow();
                }
            }
            else if( actionNum == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("====================================");
                Console.WriteLine("       상점 창으로 이동합니다.");
                Console.WriteLine("====================================");
                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)       //인벤토리창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = store.ItemStoreWindow();
                }
            }
            else if( actionNum == 4)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================");
                Console.WriteLine("    던전 입장 창으로 이동합니다.");
                Console.WriteLine("====================================");
                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)       //던전입장 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = player.DungeonWindow();
                }
            }
            else if(actionNum == 5)
            {
                if (player.playerHp == 100)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("====================================");
                    Console.WriteLine("       이미 체력이 충분합니다.");
                    Console.WriteLine("====================================");

                    Thread.Sleep(1000);
                }
                else if (player.playerGold >= 500)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("====================================");
                    Console.WriteLine("        휴식을 완료했습니다.");
                    Console.WriteLine("====================================\n");

                    Console.WriteLine("보유 골드 {0} -> {1}", player.playerGold, player.playerGold -= 500);
                    Console.WriteLine("체력 {0} -> 100",player.playerHp);
                    player.playerHp = 100;

                    Thread.Sleep(1000);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("====================================");
                    Console.WriteLine("        Gold 가 부족합니다.");
                    Console.WriteLine("====================================\n");

                    Thread.Sleep(1000);
                }
            }
        }
    }

    
    public class Item
    {
        public string sortation;    //분류
        public string itemName;     //이름
        public string itemExplanation; //설명
        public int itemPerformance; //효과
        public int itemPrice;       //아이템 가격
        public bool mountingStatus; //장착상태

        public Item(string _itemName, int _itemPerformance, string _sortation, bool _mountingStatus, string _itemExplanation , int _itemPrice)      //아이템 이름, 아이템 효과, 아이템 분류, 장착 상태, 아이템 가격
        {
            itemName = _itemName;
            itemPerformance = _itemPerformance;
            ItemNamingCenter(_sortation);
            mountingStatus = _mountingStatus;
            itemExplanation = _itemExplanation;
            itemPrice = _itemPrice;
        }

        public void ItemNamingCenter(string _sortation)     //아이템이 잡동사니인지 아닌지 판별
        {
            if(_sortation != "무기" && _sortation != "방어구")       //입력받은 문자열이 무기나 방어구가 아니면 잡동사니
            {
                sortation = "잡동사니";
                itemPerformance = 0;
            }
            else
            {
                sortation = _sortation;
            }
        }
    }

    public class Store
    {
        Item[] items;
        Player player;

        public Store(Player _player)
        {
            items = new Item[0];
            player = _player;
        }

        public bool ItemStoreWindow()
        {
            int j = 8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.playerGold} G\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in items)
            {
                Console.Write($"- {item.itemName}");

                StoreItemList(item, j++, "구매");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n1. 아이템 구매");
            Console.WriteLine($"2. 아이템 판매");
            Console.WriteLine($"0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum != 1 && actionNum != 2)
                isNum = false;

            Console.WriteLine();

            while (!isNum)      //0, 1 을 누르지 않았다면
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum != 1 && actionNum != 2)
                {
                    isNum = false;
                }
            }

            if (actionNum == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================");
                Console.WriteLine("    아이템 구매 창으로 이동합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //장착 관리 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = itemBuyWindow();   //장착 관리 창으로 이동
                }
                return true;
            }

            if (actionNum == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================");
                Console.WriteLine("    아이템 판매 창으로 이동합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //장착 관리 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = itemSellWindow();   //장착 관리 창으로 이동
                }
                return true;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      메인 화면으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public bool itemBuyWindow()
        {
            int j = 8;
            int i = 1;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.playerGold} G\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in items)
            {
                Console.Write($"- {i++} {item.itemName}");

                StoreItemList(item, j++, "구매");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum > items.Length || actionNum < 0) 
                isNum = false;

            Console.WriteLine();

            while (!isNum)      //0, 1 을 누르지 않았다면
            {

                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum > items.Length || actionNum < 0)
                {
                    isNum = false;
                }
            }

            if (actionNum > 0 && actionNum <= items.Length)
            {
                Purchas(actionNum);
                Thread.Sleep(1000);
                Console.Clear();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("       상점 창으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public bool itemSellWindow()
        {
            int j = 8;
            int i = 1;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.playerGold} G\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in player.items)
            {
                Console.Write($"- {i++} {item.itemName}");

                StoreItemList(item, j++, "판매");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum > player.items.Length || actionNum < 0)
                isNum = false;

            Console.WriteLine();

            while (!isNum)      //0, 1 을 누르지 않았다면
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum > player.items.Length || actionNum < 0)
                {
                    isNum = false;
                }
            }

            if (actionNum > 0 && actionNum <= player.items.Length)
            {
                Sell(actionNum);
                Thread.Sleep(1000);
                Console.Clear();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("       상점 창으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public void Purchas(int num)
        {
            if (player.playerGold >= items[num - 1].itemPrice)
            {
                Console.WriteLine("구매를 완료했습니다.");
                player.playerGold -= items[num - 1].itemPrice;
                player.ItemAdd(items[num - 1]);
                StoreItemDelete(items[num - 1]);
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
        }

        public void Sell(int num)
        {
            Console.WriteLine("판매를 완료했습니다.");
            player.playerGold += (player.items[num - 1].itemPrice / 100 * 85);
            StoreItemAdd(player.ItemDelete(player.items[num - 1]));
        }

        public void StoreItemAdd(Item item)
        {
            Array.Resize(ref items, items.Length + 1);          //items 배열의 정보는 유지하면서 배열의 크기 1증가시켜준다
            item.mountingStatus = false;
            items[items.Length - 1] = item;
        }

        public void StoreItemDelete(Item item)                      //아이템을 구매하면 실행되는 메서드
        {   
            for (int i = 0; i < items.Length; i++)                  
            {
                if (items[i].itemName == item.itemName)
                {
                    items = items.Where(num => num != item).ToArray();          //상점 아이템 배열에서 해당 아이템 삭제
                    return;
                }
            }
        }

        public void StoreItemList(Item item, int j, string str)
        {
            Console.SetCursorPosition(20, j);
            if (item.sortation == "방어구")
            {
                Console.Write("| 방어력 +{0, -3}", item.itemPerformance);
            }
            else if (item.sortation == "무기")
            {
                Console.Write("| 공격력 +{0, -3}", item.itemPerformance);
            }
            else                //무기나 방어구가 아니면 잡동사니 나중에 다른 종류가 생기면 else if 문으로 추가
            {
                Console.Write("| 잡동 사니  ");
            }

            Console.Write(String.Format("|  {0, -30}", item.itemExplanation));

            Console.SetCursorPosition(90, j);
            if(str == "구매")                             //구매 창과 판매 창에 표시되는 가격이 달라서 if문 사용
            {
                Console.WriteLine($"| {item.itemPrice} G");
            }
            else if(str == "판매")
            {
                Console.WriteLine("| {0} G", (item.itemPrice / 100 * 85));
            }
        }
    }

    public class Player
    {
        int playerLevel;     //플레이어 레벨
        float playerAttack;    //플레이어 공격력
        int playerDefense;   //플레이어 방어력
        int playerExp;      //플레이어 경험치
        public int playerHp;        //플레이어 체력
        public int playerGold;      //플레이어 소지 골드
        string playerName;   //플레이어 이름
        string playerJob;    //플레이어 직업
        public Item[] items;    //아이템 저장 배열\
        Item[] mountingItem = new Item[2];      //0번 무기 1번 방어구

        public Player(string name, string job) //생성자
        {
            playerName = name;      //입력받은 이름으로 초기화
            playerJob = job;        //입력받은 직업으로 초기화
            playerExp = 0;
            playerLevel = 1;        
            playerAttack = 10f;      
            playerDefense = 5;
            playerHp = 100;
            playerGold = 1500;
            items = new Item[0];
        }

        public void StatusWindow()      //상태창
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"레벨 {playerLevel}\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"경험치 {playerExp} / {playerLevel * 5}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"직업( {playerJob} )");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"공격력 : {playerAttack}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"방어력 : {playerDefense}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"체 력 : {playerHp}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"보유 골드 : {playerGold} G\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[장착 아이템]");
            if (mountingItem[0] != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"무기   | {mountingItem[0].itemName}");
                Console.SetCursorPosition(25, 13);
                Console.WriteLine($"| 효과 | 공격력 +{mountingItem[0].itemPerformance}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"무기    장착 안함");
            }

            if (mountingItem[1] != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"방어구 | {mountingItem[1].itemName}");
                Console.SetCursorPosition(25, 14);
                Console.WriteLine($"| 효과 | 방어력 +{mountingItem[1].itemPerformance}\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"방어구  장착 안함\n");
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0)
                isNum = false;

            while (!isNum)
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0)
                {
                    isNum = false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      메인 화면으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
        }

        public bool inventoryWindow()
        {
            int j = 5;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in items)
            {
                if (item.mountingStatus)
                {
                    Console.Write($"- [E]{item.itemName}");
                }
                else
                {
                    Console.Write($"- {item.itemName}");
                }

                ItemList(item, j++);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n1. 장착 관리");
            Console.WriteLine($"2. 아이템 정렬");
            Console.WriteLine($"0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum != 1 && actionNum != 2)
                isNum = false;

            while (!isNum)          //0, 1, 2번을 누르지 않았다면
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum != 1 && actionNum != 2)
                {
                    isNum = false;
                }
            }

            Console.WriteLine();
            
            if (actionNum == 1)         //장착관리 창으로 이동
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("====================================");
                Console.WriteLine("     장착 관리 창으로 이동합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //장착 관리 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = MountingWindow();   //장착 관리 창으로 이동
                }
                return true;
            }
            else if(actionNum == 2)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("====================================");
                Console.WriteLine("     아이템 정렬 창으로 이동합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //아이템 정렬 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState =SortingWindow();   //아이템 정렬 창으로 이동
                }
                return true;
            }

            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      메인 화면으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }       //인벤토리 창

        public bool MountingWindow()
        {
            int i = 0;
            int j = 5;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 잇습니다.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in items)        //내가 가지고 있는 아이템 개수만큼 반복
            {
                i++;
                if (item.mountingStatus)
                {
                    Console.Write($"- {i} [E]{item.itemName}");
                }
                else
                {
                    Console.Write($"- {i} {item.itemName}");
                }

                ItemList(item, j++);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;      //어떤 번호를 눌렀는지 체크하기 위한 변수
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum > items.Length || actionNum < 0)     // 0이 아니거나 아이템 배열보다 크면
                isNum = false;

            Console.WriteLine();

            while (!isNum)      //아이템 번호나 0번을 누르지 않았다면 반복
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);

                if (actionNum != 0 && actionNum > items.Length || actionNum < 0)
                    isNum = false;
            }

            if (actionNum > 0 && actionNum <= items.Length)      //아이템 번호를 눌렀다면
            {
                items[actionNum - 1].mountingStatus = !items[actionNum - 1].mountingStatus;
                ItemPerformanceApplication(items[actionNum - 1]);
                Console.Clear();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================");
            Console.WriteLine("      인벤 토리 창으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }       //장착 관리 창
        
        public bool SortingWindow()     //아이템 정렬 창
        {
            int j = 5;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[아이템 목록]");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  아이템이름\t\t효과\t\t  설명");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Item item in items)
            {
                if (item.mountingStatus)
                {
                    Console.Write($"- [E]{item.itemName}");
                }
                else
                {
                    Console.Write($"- {item.itemName}");
                }

                ItemList(item, j++);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"1. 이름");
            Console.WriteLine($"2. 장착순");
            Console.WriteLine($"3. 방어력");
            Console.WriteLine($"4. 공격력");
            Console.WriteLine($"0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum > 4 || actionNum < 0)
                isNum = false;

            Console.WriteLine();

            while (!isNum)      //0에서 4 사이를 누르지 않았다면
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum > 4 || actionNum < 0)
                {
                    isNum = false;
                }
            }

            if (actionNum > 0 && actionNum <= 4)      //0에서 4 사이를 누르지 않았다면
            {
                SortItem(actionNum);
                Console.Clear();
                return true;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================");
            Console.WriteLine("      인벤 토리 창으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public bool DungeonWindow()
        {
            int j = 5;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"체력 {playerHp}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"골드 {playerGold}");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"레벨 {playerLevel}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"경험치 {playerExp} / {playerLevel * 5}\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. 쉬운 던전\t\t| 방어력 5  이상 권장 | 경험치 3");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. 일반 던전\t\t| 방어력 11 이상 권장 | 경험치 7");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. 어려운 던전\t\t| 방어력 17 이상 권장 | 경험치 10");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"0. 나가기\n");
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0 && actionNum != 1 && actionNum != 2 && actionNum != 3)
                isNum = false;

            while (!isNum)          //0, 1, 2번을 누르지 않았다면
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0 && actionNum != 1 && actionNum != 2 && actionNum != 3)
                {
                    isNum = false;
                }
            }

            Console.WriteLine();

            
            if (playerHp < 36 && actionNum != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================");
                Console.WriteLine("    체력이 부족해 입장불가입니다.");
                Console.WriteLine("====================================");
            }
            else if (actionNum == 1)         //장착관리 창으로 이동
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("====================================");
                Console.WriteLine("      쉬운 던전으로 입장합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //던전 결과 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = DungeonResults("쉬운", 5, 1000, 3);   //장착 관리 창으로 이동
                }
                return true;
            }
            else if (actionNum == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("====================================");
                Console.WriteLine("      일반 던전으로 입장합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //던전 결과 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = DungeonResults("일반", 11, 1700, 7);   //아이템 정렬 창으로 이동
                }
                return true;
            }
            else if (actionNum == 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("====================================");
                Console.WriteLine("     어려운 던전으로 입장합니다.");
                Console.WriteLine("====================================");

                Thread.Sleep(1000);

                bool isWindowState = true;
                while (isWindowState)        //던전 결과 창에서 0을 입력하지 않았다면 true 입력했다면 false를 반환
                {
                    isWindowState = DungeonResults("어려운", 17, 2500, 10);   //아이템 정렬 창으로 이동
                }
                return true;
            }

            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      메인 화면으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public bool DungeonResults(string str,int num, int gold, int exp)
        {
            Random ran = new Random();
            int i = 100;
            Console.Clear();
            if (playerDefense < num)
            {
                i = ran.Next(1, 101);
            }

            if (i > 40)
            {
                Console.WriteLine("던전 클리어");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{str} 던전을 클리어 하였습니다.");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[탐험 결과]");

                int damage = ran.Next(20, 36);      //20 ~ 35 사이 데미지
                int getGold = gold + (gold * ran.Next((int)(playerAttack * 10), (int)(playerAttack * 20) + 1) / 1000);  //%값을 구한후 보상에 더함
                playerExp += exp;
                if ((playerLevel * 5) < playerExp)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n====================================");
                    Console.WriteLine("         레벨 업 하셨습니다.");
                    Console.WriteLine("====================================\n");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"레벨 {++playerLevel}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"경험치 {playerExp -= (playerLevel - 1) * 5} / {playerLevel * 5}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"공격력 {playerAttack} -> {playerAttack += 0.5f}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"방어력 {playerDefense} -> {playerDefense += 1}\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"경험치 {playerExp} / {playerLevel * 5}");
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("체력 {0} -> {1}", playerHp, (playerHp -= damage));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Gold {0} G -> {1}",playerGold, (playerGold += getGold));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("던전 실패");
                Console.WriteLine($"{str} 던전을 실패 하였습니다.");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[탐험 결과]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("체력 {0} -> {1}", playerHp, (playerHp /= 2));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n0. 나가기\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            bool isNum = int.TryParse(actionStr, out actionNum);

            //bool선택이유 (메인문 StartScene 사용이유와 같다)

            if (actionNum != 0)
                isNum = false;

            while (!isNum)
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                isNum = int.TryParse(actionStr, out actionNum);
                if (actionNum != 0)
                {
                    isNum = false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("     던전 입장 창으로 이동합니다.");
            Console.WriteLine("====================================");

            Thread.Sleep(1000);
            Console.Clear();
            return false;
        }

        public void SortItem(int num)          //아이템 정렬
        {
            if ( num == 1)
            {
                Array.Sort(items, delegate (Item item1, Item item2)             //긴 문자가 위로
                {
                    return item2.itemName.Replace(" ", "").Length.CompareTo(item1.itemName.Replace(" ", "").Length);    //Replace로 공백을 제거
                });
            }
            else if( num == 2)
            {
                Array.Sort(items, delegate (Item item1, Item item2)             //장착한 장비가 위로
                {
                    return item2.mountingStatus.CompareTo(item1.mountingStatus);
                });
            }
            else
            {
                Array.Sort(items, delegate (Item item1, Item item2)             //능력치가 높은 순서로
                {
                    return item2.itemPerformance.CompareTo(item1.itemPerformance);
                });

                if(num == 3)        //방어구
                {
                    Array.Sort(items, delegate (Item item1, Item item2)             //방어구가 위로
                    {
                        if (item1.sortation == "방어구")
                        {
                            return -1;
                        }
                        return 1;
                    });
                }
                else                //무기
                {
                    Array.Sort(items, delegate (Item item1, Item item2)             //무기가 위로
                    {
                        if (item1.sortation == "무기")
                        {
                            return -1;
                        }
                        return 1;
                    });
                }
            }
        }

        public void ItemAdd(Item item)      //플레이어에 아이템 추가하는 함수
        {
            Array.Resize(ref items, items.Length + 1);          //items 배열의 정보는 유지하면서 배열의 크기 1증가시켜준다
            items[items.Length - 1] = item;
            if (item.mountingStatus)        //추가 받은 아이템이 장착 상태라면
            {
                ItemPerformanceApplication(item);       //아이템 효과 적용 함수로 이동
            }
        }

        public Item ItemDelete(Item item)       //아이템을 팔면 실행되는 메서드
        {
            for(int i = 0; i < items.Length; i++)
            {
                if (items[i].itemName == item.itemName)         //아이템의 이름이 같다면
                {
                    if (items[i].mountingStatus)                //장착상태였다면
                    {
                        if (items[i].sortation == "무기")         //파는 아이템이 무기면
                        {
                            playerAttack -= mountingItem[0].itemPerformance;    //무기배열안에 있는 무기의 효과를 빼고
                            mountingItem[0] = null;                 //무기배열을 비운다
                        }
                        else if (items[i].sortation == "방어구")       //파는 아이템이 방어구라면
                        {
                            playerDefense -= mountingItem[1].itemPerformance;   //방어구배열안에 있는 방어구 효과를 빼고
                            mountingItem[1] = null;                 //방어구배열을 비운다
                        }
                    }
                    items = items.Where(num => num != item).ToArray();  //아이템을 배열에서 삭제
                    return item;    //아이템을 리턴해주어 상점에 추가 다수의 아이템이 삭제되는것을 방지하기 위해 여기서 리턴
                }
            }
            return null;
        }

        public void ItemList(Item item, int j)         //아이템 목록을 띄워주는 창
        {
            Console.SetCursorPosition(20, j);
            if (item.sortation == "방어구")
            {
                Console.Write("| 방어력 +{0, -3}", item.itemPerformance);
            }
            else if (item.sortation == "무기")
            {
                Console.Write("| 공격력 +{0, -3}", item.itemPerformance);
            }
            else                //무기나 방어구가 아니면 잡동사니 나중에 다른 종류가 생기면 else if 문으로 추가
            {
                Console.Write("| 잡동 사니  ");
            }
            Console.WriteLine(String.Format("|  {0, -30}",item.itemExplanation));
        }

        public void ItemPerformanceApplication(Item item)        //아이템이 장착유무에 따른 효과 적용 함수
        {
            if (item.mountingStatus)                //아이템이 장착 상태로 들어왔다면
            {   
                if (item.sortation == "무기")
                {
                    if (mountingItem[0] == null)            //무기넣는 배열에 아무것도 없다면
                    {
                        mountingItem[0] = item;             //무기를 넣는다
                        playerAttack += item.itemPerformance;
                    }
                    else                                    //무기넣는 배열에 무기가 존재한다면
                    {
                        playerAttack -= mountingItem[0].itemPerformance;    //무기배열안에 있는 무기의 효과를 빼고
                        mountingItem[0].mountingStatus = false;         //무기배열의 장착상태를 false로 바꾸고
                        mountingItem[0] = item;                         //무기를 넣는다
                        playerAttack += item.itemPerformance;
                    }
                }
                else if(item.sortation == "방어구")
                {
                    if (mountingItem[1] == null)
                    {
                        mountingItem[1] = item;
                        playerDefense += item.itemPerformance;
                    }
                    else
                    {
                        playerDefense -= mountingItem[1].itemPerformance;
                        mountingItem[1].mountingStatus = false;
                        mountingItem[1] = item;
                        playerDefense += item.itemPerformance;
                    }
                }
            }
        }
    }
}