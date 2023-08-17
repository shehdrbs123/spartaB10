// See https://aka.ms/new-console-template for more information


using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.InteropServices;
using TextRPGModule;

namespace TextRPGModule
{
    // 게임의 전반적인 로직을 관리하는 클래스
    public class TextRPGCore
    {
        private Stage currentStage;
        private Monster[] monsters;
        private IItem[] Items;
        private Warrior player;
        private Random rand;

        private int selectInt=0;
        // 게임시작
        public void Play()
        {
            Init();
            OperateGame();
        }
        //게임에 필요한 모든 리소스의 초기화, 적용
        public void Init()
        {
            monsters = new Monster[2];
            monsters[0] = new Goblin();
            monsters[0].Init("Goblin");
            monsters[1] = new Dragon();
            monsters[1].Init("Dragon");

            Items = new IItem[2];
            Items[0] = new HealthPotion();
            Items[0].Init("HealthPotion",50);
            Items[1] = new StrenthPotion();
            Items[1].Init("StrengthPotion",20);
            

            player = new Warrior();
            player.Init("Player");

            rand = new Random();
            currentStage = new Stage();
            selectInt = 0;
        }
        
        //다음 스테이지에 나올 몬스터를 고르는 함수
        public Monster SelectMonster()
        {
            // int select = rand.Next(0, monsters.Length);
            // return monsters[select];
            return monsters[selectInt++];
        }

        // 게임의 메인로직
        public void OperateGame()
        {
            bool isPlayerWin = true;
            while (isPlayerWin)
            {
                // 스테이지를 초기화 하고, 스테이지 내 전투 수행
                Monster currentMonster = SelectMonster();
                currentStage.Init(player,currentMonster,Items);
                currentStage.Start(out isPlayerWin);
                
                // 최종적으로 플레이어가 이겼는지를 파악
                if (isPlayerWin)
                {
                    // 이겼을 경우 게임 진행여부를 물어봄
                    char input = ' ';
                    while (!(input == 'y' || input == 'n'))
                    {
                        Console.Write("계속 하시겠습니까? (y,n) : ");
                        input = Console.ReadLine()[0];
                    }
                    // 계속진행이 아닐 때 게임 종료
                    if (input == 'n')
                        break;
                }
                // 모든 몬스터를 해치웠을 경우 게임을 종료
                if (selectInt == monsters.Length)
                {
                    Console.WriteLine("모든 몬스터를 잡으셨습니다");
                    break;
                }
            }
            Console.WriteLine("게임을 종료합니다");
        }
    }
    
    // 캐릭터 관련 클래스 
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; }
        int CurrentHealth { get; }
        int Attack { get; }
        bool IsDead { get; }
        int TakeDamage(int damage);
    }

    public class Warrior : ICharacter
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public bool IsDead { get; private set; }
        public int CurrentHealth { get; private set; }
       
        // 생성자 대신에 쓸 Init
        // 생성자의 사용시 자식의 생성자의 매개변수의 수 증가 등의 문제를 방지하기 위해서 생성자 대신 Init을 사용
        public void Init(string Name)
        {
            this.Name = Name;
            Health = 100;
            CurrentHealth = Health;
            IsDead = false;
            Attack = 10;
        }
        public int TakeDamage(int damage)
        {
            if (CurrentHealth > damage)
            {
                AddHealth(-damage);
                return damage;
            }
            else
            {
                AddHealth(-damage);
                IsDead = true;
            }
            return damage;
        }

        public void AddHealth(int value)
        {
            CurrentHealth = Math.Clamp(CurrentHealth + value, 0, Health);
        }

        public void AddPower(int value)
        {
            Attack = Math.Clamp(Attack + value, 0, int.MaxValue);
        }
    }
    
    public class Monster : ICharacter
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Attack { get; protected set; }
        public bool IsDead { get; private set; }
        public int CurrentHealth { get; protected set; }
        
        public virtual void Init(string Name)
        {
            this.Name = Name;
            Health = 100;
            CurrentHealth = Health;
            Attack = 3;
            IsDead = false;
        }
        
        public int TakeDamage(int damage)
        {
            if (CurrentHealth > damage)
            {
                AddHealth(-damage);
                return damage;
            }
            else
            {
                AddHealth(-damage);
                IsDead = true;
            }

            return damage;
        }

        public void AddHealth(int value)
        {
            CurrentHealth = Math.Clamp(CurrentHealth + value, 0, Health);
        }
    }

    public class Goblin : Monster
    {
        // Init을 통해 Goblin의 능력치를 지정
        public override void Init(string Name)
        {
            base.Init(Name);
            Name = "Goblin";
            Attack = 10;
            Health = 50;
            CurrentHealth = Health;
        }
    }

    public class Dragon : Monster
    {
        public override void Init(string Name)
        {
            base.Init(Name);
            Name = "Dragon";
            Attack = 15;
            Health = 100;
            CurrentHealth = Health;
        }
    }
    
    // 아이템 관련 클래스
    public interface IItem
    {
        string Name { get;}
        void Use(Warrior warrior);
        // 이름과 능력치를 외부에서 지정
        void Init(string Name, int amount);
        // 능력치의 수치를 알기 위해서 만듬
        int GetAbillity();
    }

    public class HealthPotion : IItem
    {
        public string Name { get; protected set; }
        public int healAmount { get; private set; }
        
        public void Init(string Name, int healAmount)
        {
            this.Name = Name;
            this.healAmount = healAmount;
        }

        public int GetAbillity()
        {
            return healAmount;
        }

        public void Use(Warrior warrior)
        {
            warrior.AddHealth(healAmount);
        }
    }

    public class StrenthPotion : IItem
    {
        public string Name { get; protected set; }
        public int Strength { get; private set; }

        public void Init(string Name, int strength)
        {
            this.Name = Name;
            this.Strength = strength;
        }

        public int GetAbillity()
        {
            return Strength;
        }

        public void Use(Warrior warrior)
        {
            warrior.AddPower(Strength);
        }
    }
    // 스테이지 내의 전투를 담당하는 클래스
    public class Stage
    {
        private ICharacter[] fighters;
        private IItem[] rewards;

        private const int playerIdx=0;
        private const int monsterIdx=1;
        public Stage()
        {
            fighters = new ICharacter[2];
        }
        // 플레이어와 몬스터, 그리고 rewards 리스트를 설정한다.
        public void Init(Warrior currentPlayer, Monster monster, IItem[] rewards)
        {
            fighters[playerIdx] = currentPlayer;
            fighters[monsterIdx] = monster;
            this.rewards = rewards;
        }
        
        // 게임 진행
        public void Start(out bool isPlayerWin)
        {
            isPlayerWin = false;
            int nHitterIdx = playerIdx;
            int nDamagerIdx = monsterIdx;
            while (true)
            {
                Console.Clear();
                ShowCurrentPlayerStatus();
                ICharacter hitter = fighters[nHitterIdx];
                ICharacter damager = fighters[nDamagerIdx];
                bool isDead = false;
                
                Console.WriteLine($"{hitter.Name}이 공격합니다.");
                
                Battle(hitter,damager,out isDead);
                
                if (isDead)
                {
                    if (hitter.GetType() == typeof(Warrior))
                    {
                        isPlayerWin = true;
                        TryReward(isPlayerWin);
                    }
                    else
                    {
                        Console.WriteLine($"패배하였습니다");
                        break;
                    }
                }

                SwapCharacter(ref nHitterIdx,ref nDamagerIdx);
                Thread.Sleep(1000);
            }
        }

        public void SwapCharacter(ref int nHitterIdx, ref int nDamagerIdx)
        {
            int tmp = nHitterIdx;
            nHitterIdx = nDamagerIdx;
            nDamagerIdx = tmp;
        }

        public void Battle(ICharacter hitter, ICharacter damager, out bool isDead)
        {
            isDead = false;
            int damaged =damager.TakeDamage(hitter.Attack); 
            Console.WriteLine($"{damager.Name}이 {damaged,4}만큼의 데미지를 입었습니다. 남은 체력 {damager.Name,10}:{damager.CurrentHealth,3}");
            isDead = damager.IsDead;
        }

        public void ShowCurrentPlayerStatus()
        {
            ICharacter showTarget = fighters[playerIdx];
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("이름 : {0,10} 체력 : {1,4}/{2,4}  공격력 : {3,3}",showTarget.Name,showTarget.CurrentHealth,showTarget.Health,showTarget.Attack);
            Console.WriteLine("-----------------------------------------------------");
            
            showTarget = fighters[monsterIdx];
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("이름 : {0,10} 체력 : {1,4}/{2,4}  공격력 : {3,3}",showTarget.Name,showTarget.CurrentHealth,showTarget.Health,showTarget.Attack);
            Console.WriteLine("-----------------------------------------------------");
        }
        // 입력 예외처리포함 reward를 먹을때 까지 반복
        public void TryReward(bool isPlayerWin)
        {
            if (isPlayerWin)
            {
                Console.WriteLine($"{fighters[playerIdx].Name}가 이겼습니다.");
                
                while (true)
                {
                    int selectNum;
                    Console.WriteLine("아이템을 고르세요");
                    for (int i = 0; i < rewards.Length; i++)
                    {
                        Console.WriteLine($"{i} : {rewards[i].Name,20} {rewards[i].GetAbillity(),4}");
                    }
                    Console.Write("어떤 것을 고르시겠습니까? : ");

                    if (int.TryParse(Console.ReadLine(), out selectNum))
                    {
                        if (!(0 <= selectNum && selectNum <= rewards.Length))
                        {
                            Console.WriteLine("알맞는 값을 넣어주세요");
                        }
                        else
                        {
                            // 리워드 획득!
                            rewards[selectNum].Use((Warrior)fighters[playerIdx]);
                            break;
                        }
                    }
                }
            }
        }
    }
}


public class Program
{
    public static void Main(String[] args)
    {
        TextRPGCore core = new TextRPGCore();
        core.Play();
    }
}