// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

// 카드 한 장을 표현하는 클래스
public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }

    public Card(Suit s, Rank r)
    {
        Suit = s;
        Rank = r;
    }

    public int GetValue()
    {
        if ((int)Rank <= 10)
        {
            return (int)Rank;
        }
        else if ((int)Rank <= 13)
        {
            return 10;
        }
        else
        {
            return 11;
        }
    }

		public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

// 덱을 표현하는 클래스
public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        SetDefault();
        Shuffle();
    }

    public void SetDefault()
    {
        int suitCount = Enum.GetNames<Suit>().GetLength(0);
        int Rank = Enum.GetNames<Rank>().GetLength(0);
        
        if (cards != null)
            cards.Clear();
        else
            cards = new List<Card>();
        
        cards.Capacity = suitCount * Rank;
        
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(s, r));
            }
        }
    }

    public void Shuffle()
    {
        Random rand = new Random();

        for (int i = 0; i < cards.Count; i++)
        {
            int j = rand.Next(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public void ReRoll()
    {
        SetDefault();
        Shuffle();
    }

    public Card DrawCard()
    {
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}

// 패를 표현하는 클래스
public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int GetTotalValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
            total += card.GetValue();
        }

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }
}

// 플레이어를 표현하는 클래스
public class Player
{
    private int playerNum;

    public bool isStop
    {
        private set;
        get;
    }
    public Hand Hand { get; private set; }

    public Player(int playerNum)
    {
        this.playerNum = playerNum;
        Hand = new Hand();
        isStop = false;
    }

    public Card DrawCardFromDeck(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        Hand.AddCard(drawnCard);
        return drawnCard;
    }

    public void StopDraw()
    {
        isStop = true;
    }
}

// 여기부터는 학습자가 작성
// 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
public class Dealer : Player
{
    // 코드를 여기에 작성하세요
    public Dealer(int playerNum) : base(playerNum)
    {
    }
    
}

// 블랙잭 게임을 구현하세요. 
public class Blackjack
{
    private Deck deck;

    private Player[] players;

    private const int playerNum=2;

    private const int dealerNum = 0;
    // 코드를 여기에 작성하세요
    public void Play()
    {
        InitBoard();
        OperateGame();
    }

    private void InitBoard()
    {
        deck = new Deck();
        players = new Player[playerNum];
        players[dealerNum] = new Dealer(dealerNum);
        for (int i = 1; i < playerNum; i++)
        {
            players[i] = new Player(i);
        }
        // 2장씩 뽑기
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Card card = deck.DrawCard();
                players[i].Hand.AddCard(card);
            }
        }
    }

    private void OperateGame()
    {
        int currentPlayer = dealerNum;
        Player player = players[currentPlayer];
        Random rand = new Random();
        int stopNum = 0;
        while (true)
        {
            Console.WriteLine($"플레이어 {currentPlayer}의 턴입니다.");
            // 플레이어가 멈췄는지 확인
            if (!player.isStop)
            {
                //딜러일 경우
                //20점 이상일땐 무조건 스톱
                // 17점 이하일땐 무조건 뽑기
                if (currentPlayer == dealerNum)
                {
                    Thread.Sleep(500);
                    int currentScore = player.Hand.GetTotalValue();
                    // 20점 이상이면 무조건 멈춤
                    if (currentScore >= 20)
                    {
                        player.StopDraw();
                        ++stopNum;
                        Console.WriteLine($"플레이어 {currentPlayer}가 멈췄습니다");
                    }
                    // 17점 이하가 아닐때는 즉 20 ~17 사이일때는 랜덤으로 반반
                    else if (!(currentScore <= 17))
                    {
                        int randomSelect = rand.Next(0, 10);
                        if (randomSelect > 4)
                        {
                            player.StopDraw();
                            ++stopNum;
                            Console.WriteLine($"플레이어 {currentPlayer}가 멈췄습니다");
                        }
                    }
                    // 17점 이상일땐 무조건 뽑음
                    else
                    {
                        Console.WriteLine($"플레이어 {currentPlayer}가 뽑습니다");
                    }
                    Thread.Sleep(500);
                }
                // 일반 플레이어일 경우
                else
                {
                    int isDraw = 0;
                    string strDraw = "";
                    Console.Write($"카드를 뽑으시겠습니까?(뽑는다 1, 안뽑는다 2) 현재 점수 : {player.Hand.GetTotalValue()}: ");

                    while (isDraw != 1 && isDraw != 2)
                    {
                        strDraw = Console.ReadLine();
                        if (int.TryParse(strDraw, out isDraw))
                        {
                            if (isDraw == 2)
                            {
                                player.StopDraw();
                                ++stopNum;
                            }
                        }
                    }
                }
            }
            // 멈추지 않은 플레이어는 뽑음
            if (!player.isStop)
            {
                Card card = deck.DrawCard();
                player.Hand.AddCard(card);
            }

            int currentPlayerScore = player.Hand.GetTotalValue();
            // 21점이 넘었는지 체크하고 넘었으면 패배
            if (currentPlayerScore > 21)
            {
                Console.WriteLine($"플레이어 {currentPlayer}이 졌습니다. 점수 {currentPlayerScore}");
                for (int i = 0; i < players.Length; i++)
                {
                    if (currentPlayer == i)
                        continue;
                    Console.WriteLine($"플레이어 {i}의 점수 : {players[i].Hand.GetTotalValue()}");
                }

                break;
            }
            // 모두 멈췄다면 계산 시작
            if (stopNum == players.Length)
            {
                int highestPlayer = 0;
                int highestScore = players[0].Hand.GetTotalValue();
                
                for (int i = 1; i < players.Length; i++)
                {
                    int iScore = players[i].Hand.GetTotalValue();
                    if (highestScore < iScore)
                    {
                        highestPlayer = i;
                        highestScore = iScore;
                    }
                    // 비기는 순간 발생시 그 순간 그냥 비김 판정
                    // 본래는 리스트로 승리자들 추슬러서 해야함
                    else if(highestScore == iScore)
                    {
                        Console.WriteLine($"플레이어 {highestPlayer}와 플레이어 {i}가 비겼습니다. 점수 [{highestScore}]");
                        return;
                    }
                }
                
                Console.WriteLine($"플레이어 {highestPlayer} 가 승리하였습니다 [점수] {highestScore}");
                for (int i = 0; i < players.Length; i++)
                {
                    if (highestPlayer == i)
                        continue;
                    Console.WriteLine($"플레이어 {i}의 점수 : {players[i].Hand.GetTotalValue()}");
                }
                break;
            }

            currentPlayer = (currentPlayer + 1) % players.Length;
            player = players[currentPlayer];
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 블랙잭 게임을 실행하세요
        Blackjack player = new Blackjack();
        player.Play();
    }
}