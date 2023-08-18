namespace Week3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Blackjack blackjack = new Blackjack();

            blackjack.Game();
        }
    }

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace }

    public class Card
    {
        public Suit suit;
        public Rank rank;
        public Card(Suit s, Rank r)
        {
            suit = s;
            rank = r;
        }

        public int GetValue()
        {
            if ((int)rank <= 10)
            {
                return (int)rank;
            }
            else if ((int)rank <= 13)
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
            return $"{(int)rank} {suit}를 뽑았습니다";
        }
    }

    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            Suit suit = new Suit();
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }
            Shuffle();
        }

        public void Shuffle ()
        {
            Random ran = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = ran.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

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
            int aceCount = 0;
            int total = 0;

            foreach (Card card in cards)
            {
                if (card.rank == Rank.Ace)
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

    public class Player
    {
        public Hand hand;

        public Player()
        {
            hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawCard = deck.DrawCard();
            hand.AddCard(drawCard);
            return drawCard;
        }
    }

    public class Dealer : Player
    {
        public void asdf()
        {

        }
    }

    public class Blackjack
    {
        Player player;
        Player dealer;
        Deck deck;
        Card card;
        public Blackjack()
        {
            player = new Player();
            dealer = new Player();
            deck = new Deck();
        }

        public void Game()
        {
            bool drawcard = true;
            deck.Shuffle();
            int playerscore;
            int dealerscore;

            for(int i = 0; i < 2; i++)
            {
                card = player.DrawCardFromDeck(deck);
                Console.WriteLine("플레이어가 뽑은 카드 : {0}", card.ToString());
                card = dealer.DrawCardFromDeck(deck);
            }

            playerscore = player.hand.GetTotalValue();
            dealerscore = dealer.hand.GetTotalValue();

            Console.WriteLine("오픈된 딜러의 카드 : {0}", card.ToString());
            Console.WriteLine("플레이어 현재까지 점수 : {0}",playerscore);

            while (drawcard)
            {
                if (playerscore <= 21)
                {
                    Console.Write("카드를 더 받으시겠습니까(Y/N) :");
                    string str = Console.ReadLine();
                    if (str == "Y")
                    {
                        card = player.DrawCardFromDeck(deck);
                        Console.WriteLine("플레이어 : {0}", card.ToString());
                        playerscore = player.hand.GetTotalValue();
                        Console.WriteLine("플레이어 현재까지 점수 : {0}", playerscore);
                    }
                    else if (str == "N")
                    {
                        drawcard = false;
                    }
                }
                else
                {
                    Console.WriteLine("플레이어 점수가 21점을 넘어 더이상 뽑지 못합니다", playerscore);
                    drawcard = false;
                }
            }

            dealerscore = dealer.hand.GetTotalValue();
            Console.WriteLine("\n플레이어 최종 점수 : {0}", playerscore);
            Console.WriteLine("딜러 현재 점수 : {0}\n", dealerscore);

            if (playerscore <= 21)
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (dealerscore < 17)
                    {
                        Console.WriteLine("딜러의 카드합이 17을 넘지못해 카드를 뽑습니다.");
                        card = dealer.DrawCardFromDeck(deck);
                        Console.WriteLine("딜러가 뽑은 카드 : {0}", card.ToString());
                        dealerscore = dealer.hand.GetTotalValue();
                        Console.WriteLine("딜러 현재 점수 : {0}\n", dealerscore);
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("딜러 최종 점수 : {0}\n", dealerscore);

                
                if (playerscore > dealerscore || dealerscore > 21)
                {
                    Console.WriteLine("플레이어 승");
                }
                else if(playerscore < dealerscore)
                {
                    Console.WriteLine("딜러 승");
                }
                else
                {
                    Console.WriteLine("비겼습니다");
                }
            }
            else
            {
                Console.WriteLine("플레이어 점수가 21점이 넘어 딜러의 점수와 관계없이 플레이어의 패배입니다");
            }
        }
    }
}