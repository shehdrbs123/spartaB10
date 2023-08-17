using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;

namespace week4
{
    public interface ICharacter
    {
        string name { get;}
        int health { get; set; }
        int attack { get;}
        bool isDead { get;}
        void TakeDamage(int damage);
    }

    public class Warrior : ICharacter
    {
        public string name { get; }
        public int health { get; set; }
        public int attack { get; set; }
        public bool isDead => health <= 0;

        public Warrior(string str)
        {
            name = str;
            health = 100;
            attack = 20;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                Console.WriteLine($"{name}이(가) 죽었습니다");
            else
                Console.WriteLine($"{name}이(가) {damage}의 데미지를 입었습니다. 남은 체력 : {health}");
        }
    }

    public class Monster : ICharacter
    {
        public string name { get; }
        public int health { get; set;}
        public int attack { get; }
        public bool isDead => health <= 0;
        public Monster(string str, int num, int min, int max)
        {
            name = str;
            health = num;
            attack = new Random().Next(min, max);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                Console.WriteLine($"{name}이(가) 죽었습니다");
            else
                Console.WriteLine($"{name}이(가) {damage}의 데미지를 입었습니다. 남은 체력 : {health}");
        }
    }

    public class Goblin : Monster
    {
        public Goblin(string name) : base(name, 50, 10, 20) { } //부모 클래스의 생성자를 호출
    }

    public class Dragon : Monster
    {
        public Dragon(string name) : base(name, new Random().Next(500, 1000), 50, 100) { } 
    }

    public interface IItem
    {
        string name { get;}
        void Use(Warrior warrior);
    }

    public class HealthPotion : IItem
    {
        public string name => "체력 포션";

        public void Use(Warrior warrior)
        {
            warrior.health += 50;
        }
    }

    public class StrengthPotion : IItem
    {
        public string name => "힘 포션";

        public void Use(Warrior warrior)
        {
            warrior.attack += 5;
        }
    }

    public class Stage
    {
        private Warrior player;
        private Monster monster;
        private List<IItem> rewards;
        private int stageNum;
        private int maxHp;

        public Stage(Warrior w, Monster m,List<IItem> r, int i)
        {
            player = w;
            monster = m;
            rewards = r;
            stageNum = i;
            maxHp = player.health;
        }

        public void Start()
        {
            Console.WriteLine($"{stageNum}스테이지 시작\n\n플레이어 체력 : {player.health}\n플레이어공격력 : {player.attack}\n");
            Console.WriteLine($"몬스터 이름 : {monster.name}\n몬스터 체력 : {monster.health}\n몬스터 공격력 : {monster.attack}\n");

            while (!player.isDead && !monster.isDead)
            {
                Console.WriteLine($"\n{player.name}의 턴");
                monster.TakeDamage(player.attack);
                Thread.Sleep(500);

                if (monster.isDead)
                    break;

                Console.WriteLine($"\n{monster.name}의 턴");
                player.TakeDamage(monster.attack);
                Thread.Sleep(500);
            }

            if (player.isDead)
            {
                StageClear(player);
            }
            else
            {
                StageClear(monster);
            }
        }

        public void StageClear(ICharacter i)
        {
            if (i is Monster)
            {
                Console.WriteLine("\n============================");
                Console.WriteLine("         Stage Clear");
                Console.WriteLine("============================\n");

                if (rewards != null)
                {
                    Console.WriteLine("아래의 보상중 한가지를 선택하여 사용하실수 있습니다.");

                    foreach (IItem item in rewards)
                    {
                        Console.WriteLine(item.name);
                    }
                    player.health = maxHp;

                    Console.WriteLine("사용할 아이템 이름을 입력하세요 : ");
                    string input = Console.ReadLine();
                    IItem selectedItem = rewards.Find(item => item.name == input);
                    
                    if(selectedItem == null)
                    {
                        while (selectedItem == null)
                        {
                            Console.WriteLine("잘못 입력하셨습니다");
                            Console.WriteLine("사용할 아이템 이름을 입력하세요 : ");
                            input = Console.ReadLine();
                            selectedItem = rewards.Find(item => item.name == input);
                        }
                    }
                    selectedItem.Use(player);
                }
                Console.Clear();
            }
            else
            {
                Console.WriteLine("플레이어가 사망하였습니다\n\n\n\n");
                Console.WriteLine("============================");
                Console.WriteLine("         GAME OVER");
                Console.WriteLine("============================");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int stagenum = 1;
            Warrior warrior = new Warrior("조범준");
            Goblin goblin = new Goblin("고블린");
            Dragon dragon = new Dragon("드래곤");

            List<IItem> stageRewards = new List<IItem> { new HealthPotion(), new StrengthPotion() };

            Random ran = new Random();

            while (!warrior.isDead && !dragon.isDead)
            {
                if (goblin.isDead)
                    goblin = new Goblin("고블린");

                int i = ran.Next(0, 100);

                if (i > 0)
                {
                    Stage stage = new Stage(warrior, goblin, stageRewards, stagenum);
                    stage.Start();
                }
                else if(i == 0)
                {
                    Stage stage = new Stage(warrior, dragon, stageRewards, stagenum);
                    stage.Start();
                }
                stagenum++;
            }
            if(dragon.isDead)
            {

                Console.WriteLine("============================");
                Console.WriteLine("      클리어 하셨습니다");
                Console.WriteLine("============================");
            }
        }
    }
}