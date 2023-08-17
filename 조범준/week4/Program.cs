using System.Security.Cryptography.X509Certificates;
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
            if (health < 0)
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
        public Monster(string str, int num)
        {
            name = str;
            health = num;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
                Console.WriteLine($"{name}이(가) 죽었습니다");
            else
                Console.WriteLine($"{name}이(가) {damage}의 데미지를 입었습니다. 남은 체력 : {health}");
        }
    }

    public class Goblin : Monster
    {
        public Goblin(string name) : base(name, 50) { } //부모 클래스의 생성자를 호출
    }

    public class Dragon : Monster
    {
        public Dragon(string name) : base(name, 50) { } 
    }

    public interface IItem
    {
        string name { get;}
        void Use(Warrior warrior);
    }

    public class HealthPotion : IItem
    {
        public string name => "체력포션";

        public void Use(Warrior warrior)
        {
            int num = 50;
            warrior.health += 50;

            if(warrior.health >= 100)
            {
                num = 150 - warrior.health;     //얼마나 체력이 회복했는지 넣어보려고
                warrior.health = 100;
            }

            Console.WriteLine($"체력 포션을 사용합니다. 체력이 {num} 증가합니다. 현재 체력 : {warrior.health}");
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
        private ICharacter player;
        private ICharacter Monster;
        private List<IItem> rewards;


    }

    internal class Program
    {
        static void Main(string[] args)
        {
             
        }
    }
}