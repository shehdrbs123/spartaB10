// See https://aka.ms/new-console-template for more information


using System.ComponentModel;
using System.IO.Compression;
using System.Runtime.InteropServices;
using TextRPGModule;

namespace TextRPGModule
{
    public class TextRPGCore
    {
        public void Play()
        {
            Init();
            OperateGame();
        }

        public void Init()
        {
            
        }

        public void OperateGame()
        {
            while (true)
            {
                
            }
        }
    }
    
    // 캐릭터 관련 클래스 
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; }
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
            if (Health > damage)
            {
                Health -= damage;
                return damage;
            }
            else
            {
                Health = 0;
                IsDead = true;
                Die();
            }

            return damage;
        }

        private void Die()
        {
            
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
        public int CurrentHealth { get; private set; }
        
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
                Die();
            }

            return damage;
        }

        public void Die()
        {
            
        }

        public void AddHealth(int value)
        {
            CurrentHealth = Math.Clamp(CurrentHealth + value, 0, Health);
        }
    }

    public class Goblin : Monster
    {
        public override void Init(string Name)
        {
            base.Init(Name);
            Name = "Goblin";
            Attack = 10;
            Health = 50;
        }
    }

    public class Dragon : Monster
    {
        public override void Init(string Name)
        {
            base.Init(Name);
            Name = "Dragon";
            Attack = 15;
            Health = 200;
        }
    }
    
    // 아이템 관련 클래스
    public interface IItem
    {
        string Name { get;}
        void Use(Warrior warrior);
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
        public void Use(Warrior warrior)
        {
            warrior.AddPower(Strength);
        }
    }

    public class Stage
    {
        private ICharacter[] fighters;
        private IItem[] rewards;
        
        public Stage()
        {
            fighters = new ICharacter[2];
        }
        public void Init(Warrior currentPlayer, Monster monster, IItem[] rewards)
        {
            fighters[0] = currentPlayer;
            fighters[1] = monster;
            this.rewards = rewards;
        }
        
        public void Start()
        {
            while (true)
            {
                ICharacter hitter = fighters[0];
                ICharacter damager = fighters[1];
                bool isDead = false;
                
                Battle(hitter,damager,out isDead);
                if (isDead)
                {
                    Win();
                    break;
                }

                SwapCharacter();
            }
        }

        public void SwapCharacter()
        {
            ICharacter tmp = fighters[0];
            fighters[0] = fighters[1];
            fighters[1] = tmp;
        }

        public void Battle(ICharacter hitter, ICharacter damager, out bool isDead)
        {
            isDead = false;
            damager.TakeDamage(hitter.Attack);
            isDead = damager.IsDead;
        }
        

        public void Win()
        {
            
        }

        public void Exit()
        {
            
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