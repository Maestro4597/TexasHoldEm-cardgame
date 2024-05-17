using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TexasHoldEm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace TexasHoldEm
{
    public class Card
    {
        // Properties
        public string Suit { get; }
        public string Rank { get; }

        // Constructor
        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Override ToString method to return card as string
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    } //Represents a single card.

    public class Deck
    {
        // Properties
        public List<Card> Cards { get; private set; }

        // Constructor
        public Deck()
        {
            Cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            // Add cards to the deck
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    Cards.Add(new Card(suit, rank));
                }
            }
        }

        // Shuffle the deck
        public void Shuffle()
        {
            Random random = new Random();
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card card = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = card;
            }
        }

        // Deal a card from the deck
        public Card Deal()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    } //Represents a deck of playing cards, with methods to shuffle the deck and deal cards.

    public class Player
    {

        // Properties
        public string Name { get; }
        public List<Card> Hand { get; private set; }
        public int Chips { get; private set; }

        // Constructor
        public Player(string name, int chips)
        {
            Name = name;
            Chips = chips;
            Hand = new List<Card>();
        }

        // Add a card to the player's hand
        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        // Remove all cards from the player's hand
        public void ClearHand()
        {
            Hand.Clear();
        }

        // Add chips to the player's stack
        public void AddChips(int amount)
        {
            Chips += amount;
        }

        // Remove chips from the player's stack
        public void RemoveChips(int amount)
        {
            Chips -= amount;
        }

        // Override ToString method to return player as string
        public override string ToString()
        {
            return $"{Name} ({Chips} chips)";
        }
    }  //Deals with Player name, hand of cards, and chips.

    public class HumanPlayer : Player
    {
        // Constructor
        public HumanPlayer(string name, int startingChips) : base(name, startingChips)
        {
        }

        // Ask the player to make a bet
        public int MakeBet(int minimumBet)
        {
            Console.WriteLine($"Minimum bet is {minimumBet}. {Name}, how much do you want to bet?");
            int bet = Convert.ToInt32(Console.ReadLine());

            while (bet < minimumBet || bet > Chips)
            {
                if (bet < minimumBet)
                {
                    Console.WriteLine($"Bet must be at least {minimumBet}. {Name}, how much do you want to bet?");
                }
                else if (bet > Chips)
                {
                    Console.WriteLine($"{Name}, you don't have enough chips for that bet. You have {Chips} chips. How much do you want to bet?");
                }
                bet = Convert.ToInt32(Console.ReadLine());
            }

            return bet;
        }

        // Ask the player if they want to fold, call, or raise
        public string MakeDecision(int minimumBet, int currentBet)
        {
            Console.WriteLine($"Minimum bet is {minimumBet}. {Name}, the current bet is {currentBet}. Do you want to fold, call, or raise?");

            string decision = Console.ReadLine();

            while (decision.ToLower() != "fold" && decision.ToLower() != "call" && decision.ToLower() != "raise")
            {
                Console.WriteLine("Invalid decision. Do you want to fold, call, or raise?");
                decision = Console.ReadLine();
            }

            if (decision.ToLower() == "raise")
            {
                Console.WriteLine($"{Name}, how much do you want to raise?");
                int raiseAmount = Convert.ToInt32(Console.ReadLine());
                while (raiseAmount < minimumBet)
                {
                    Console.WriteLine($"Raise amount must be at least {minimumBet}. {Name}, how much do you want to raise?");
                    raiseAmount = Convert.ToInt32(Console.ReadLine());
                }

                return $"raise {raiseAmount}";

            }

            if(decision.ToLower() == "fold")

            {
                


            }



            return decision.ToLower();
        }
    } //Deals with Decisions interacting with game UI

    public class Table
    {
        // Properties
        public List<Player> Players { get; }
        public Deck Deck { get; }
        public List<Card> CommunityCards { get; }

        // Constructor
        public Table()
        {
            Players = new List<Player>();
            Deck = new Deck();
            CommunityCards = new List<Card>();
        }

        // Add a player to the table
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        // Remove a player from the table
        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        // Deal cards to all players at the table
        public void Deal()
        {
            // Deal two cards to each player
            foreach (Player player in Players)
            {
                player.AddCard(Deck.Deal());
                player.AddCard(Deck.Deal());
            }

            // Deal five community cards
            CommunityCards.Add(Deck.Deal());
            CommunityCards.Add(Deck.Deal());
            CommunityCards.Add(Deck.Deal());
            CommunityCards.Add(Deck.Deal());
            CommunityCards.Add(Deck.Deal());

        }

        // Reset the table for a new round of play
        public void Reset()
        {
            Deck.Shuffle();
            CommunityCards.Clear();
            foreach (Player player in Players)
            {
                player.ClearHand();
            }
        }


    } //A class that represents the game table, with properties such as the community cards and the pot.

    public class Game
    {
        // Properties
        public Table Table { get; }
        public List<Player> Winners { get; private set; }

        // Constructor
        public Game()
        {
            Table = new Table();
            Winners = new List<Player>();
        }

        // Add a player to the table
        public void AddPlayer(Player player)
        {
            Table.AddPlayer(player);
        }

        // Remove a player from the table
        public void RemovePlayer(Player player)
        {
            Table.RemovePlayer(player);
        }

        // Play a round of Texas Hold'em
        public void PlayRound()
        {
            // Deal cards
            Table.Reset();
            Table.Deal();

            // Betting
            // TODO: Implement betting logic

            // Determine winner(s)
            Winners = DetermineWinners(Table.Players);
        }

        // Determine the winner(s) of a round
        private List<Player> DetermineWinners(List<Player> players)
        {
            List<Player> winners = new List<Player>();

            // TODO: Implement logic to determine winners

            return winners;
        }
    } //A class that manages the overall flow of the game, with methods to start a new round, deal cards, manage bets, and determine the winner of the hand.


    public enum PokerHands
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public enum PokerRanks
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }




    //Necessary for Rubric:




    /* public class HandRank : IComparable<HandRank> 
     {

         public Hand Hand { get; }
         public Rank RankValue { get; }

         public HandRank(Hand hand, Rank rankValue = Rank.Two)
         {
             Hand = hand;
             RankValue = rankValue;
         }

         public int CompareTo(HandRank other)
         {
             if (Hand != other.Hand)
                 return Hand.CompareTo(other.Hand);

             return RankValue.CompareTo(other.RankValue);
         }
     }

 */
    /*  public Player DetermineWinner(Player player1, Player player2, List<Card> communityCards)
      {
          HandEvaluator evaluator = new HandEvaluator();
          HandRanking player1Ranking = evaluator.EvaluateHand(player1.Hand.Concat(communityCards));
          HandRanking player2Ranking = evaluator.EvaluateHand(player2.Hand.Concat(communityCards));

          if (player1Ranking > player2Ranking)
          {
              return player1;
          }
          else if (player2Ranking > player1Ranking)
          {
              return player2;
          }
          else // hands have same ranking, need to compare kickers
          {
              List<Card> player1Hand = player1.Hand.Concat(communityCards).OrderByDescending(card => card.Rank).ToList();
              List<Card> player2Hand = player2.Hand.Concat(communityCards).OrderByDescending(card => card.Rank).ToList();
              for (int i = 0; i < player1Hand.Count; i++)
              {
                  if (player1Hand[i].Rank > player2Hand[i].Rank)
                  {
                      return player1;
                  }
                  else if (player2Hand[i].Rank > player1Hand[i].Rank)
                  {
                      return player2;
                  }
              }
              // if all cards are the same, the hand is a tie
              return null;
          }
      }

      */

    //https://chat.openai.com/chat
    // Prompt: Make an IComparable function for a PokerHand class
    public class PokerHand : IComparable<PokerHand>
    {
        public Card[] Cards { get; }




        public PokerHands GetRank()
        {
            bool isFlush = Cards.All(c => c.Suit == Cards[0].Suit);
            bool isStraight = IsStraight(Cards);


            if (isFlush && isStraight && Cards[0].Rank == "Ace")
            {
                return PokerHands.RoyalFlush;
            }
            else if (isFlush && isStraight)
            {
                return PokerHands.StraightFlush;
            }
            else if (HasFourOfAKind(Cards))
            {
                return PokerHands.FourOfAKind;
            }
            else if (HasFullHouse(Cards))
            {
                return PokerHands.FullHouse;
            }
            else if (isFlush)
            {
                return PokerHands.Flush;
            }
            else if (isStraight)
            {
                return PokerHands.Straight;
            }
            else if (HasThreeOfAKind(Cards))
            {
                return PokerHands.ThreeOfAKind;
            }
            else if (HasTwoPair(Cards))
            {
                return PokerHands.TwoPair;
            }
            else if (HasOnePair(Cards))
            {
                return PokerHands.OnePair;
            }
            else
            {
                return PokerHands.HighCard;
            }


        }

        public int CompareTo(PokerHand other)
        {
            // First compare the rank of the hands
            int rankComparison = this.GetRank().CompareTo(other.GetRank());
            if (rankComparison != 0)
            {
                return rankComparison;
            }

            // If the ranks are the same, compare the high card values
            for (int i = 0; i <= 4; i++)
            {
                int cardComparison = this.Cards[i].Rank.CompareTo(other.Cards[i].Rank);
                if (cardComparison != 0)
                {
                    return cardComparison;
                }
            }

            // If the hands have the same rank and high cards, they are equal
            return 0;
        }

        // Helper methods for checking hand ranks
        private bool IsStraight(Card[] cards)
        {
            // Sort the cards in descending order
            Array.Sort(cards);
            Array.Reverse(cards);


            if ((cards[0].Rank == "Six" && cards[1].Rank == "Five" && cards[2].Rank == "Four" && cards[3].Rank == "Three" && cards[4].Rank == "Two") ||
                (cards[0].Rank == "Seven" && cards[1].Rank == "Six" && cards[2].Rank == "Five" && cards[3].Rank == "Four" && cards[4].Rank == "Three") ||
                (cards[0].Rank == "Eight" && cards[1].Rank == "Seven" && cards[2].Rank == "Six" && cards[3].Rank == "Five" && cards[4].Rank == "Four") ||
                (cards[0].Rank == "Nine" && cards[1].Rank == "Eight" && cards[2].Rank == "Seven" && cards[3].Rank == "Six" && cards[4].Rank == "Five") ||
                (cards[0].Rank == "Ten" && cards[1].Rank == "Nine" && cards[2].Rank == "Eight" && cards[3].Rank == "Seven" && cards[4].Rank == "Six") ||
                (cards[0].Rank == "Jack" && cards[1].Rank == "Ten" && cards[2].Rank == "Nine" && cards[3].Rank == "Eight" && cards[4].Rank == "Seven") ||
                (cards[0].Rank == "Queen" && cards[1].Rank == "Jack" && cards[2].Rank == "Ten" && cards[3].Rank == "Nine" && cards[4].Rank == "Eight") ||
                (cards[0].Rank == "King" && cards[1].Rank == "Queen" && cards[2].Rank == "Jack" && cards[3].Rank == "Ten" && cards[4].Rank == "Nine") ||
                (cards[0].Rank == "Ace" && cards[1].Rank == "King" && cards[2].Rank == "Queen" && cards[3].Rank == "Jack" && cards[4].Rank == "Ten")) {

                return true;

            }


            return false;
        }

        private bool HasOnePair(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 2);
        }

        private bool HasTwoPair(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Count(g => g.Count() == 2) == 2;
        }

        private bool HasThreeOfAKind(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 3);
        }

        private bool HasFullHouse(Card[] cards)
        {
            return HasOnePair(cards) && HasThreeOfAKind(cards);
        }

        private bool HasFourOfAKind(Card[] cards)
        {
            return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 4);
        }


    }
}

  public class HoldEmHand : IComparable<HoldEmHand>
    {





    List<Card> SevenCardsP1;



    public int GetBestPokerHand()
    {
        Player One = new Player("Mike", 100);
        Player Two = new Player("John", 100);
        Table table;

        table = new Table();
        table.AddPlayer(One);
        table.AddPlayer(Two);
        table.Deal();


        SevenCardsP1.Add(table.CommunityCards[0]);
        SevenCardsP1.Add(table.CommunityCards[1]);
        SevenCardsP1.Add(table.CommunityCards[2]);
        SevenCardsP1.Add(table.CommunityCards[3]);
        SevenCardsP1.Add(table.CommunityCards[4]);
        SevenCardsP1.Add(One.Hand[0]);
        SevenCardsP1.Add(One.Hand[1]);

        SevenCardsP1.Sort();
        SevenCardsP1.Reverse();










        return 0;





    }

    public int CompareTo(HoldEmHand other)
    {

        int HandComparison = this.GetBestPokerHand().CompareTo(other.GetBestPokerHand());
        
        if (HandComparison != 0)
        {
            return HandComparison;
        }

        

        for (int i = 0; i < 5; i++)
        {
            int cardComparison = this.SevenCardsP1[i].Rank.CompareTo(other.SevenCardsP1[i].Rank);
            if (cardComparison != 0)
            {
                return cardComparison;
            }
        }


        return 0;
    }








    public Player hand { get; set;   }






       


}



/*
 public static List<string> GetBestPokerHand(List<string> playerCards, List<string> communityCards)
{
    // Combine player and community cards into one list
    List<string> allCards = new List<string>(playerCards);
    allCards.AddRange(communityCards);

    // Generate all possible 5-card combinations from the cards
    List<List<string>> combinations = GetAllCombinations(allCards, 5);

    // Evaluate each combination and determine the best hand
    List<string> bestHand = null;
    foreach (List<string> combination in combinations)
    {
        List<string> currentHand = EvaluateHand(combination);
        if (bestHand == null || CompareHands(currentHand, bestHand) > 0)
        {
            bestHand = currentHand;
        }
    }

    return bestHand;
}

// Helper function to generate all possible combinations of a list of cards
private static List<List<string>> GetAllCombinations(List<string> cards, int size)
{
    List<List<string>> result = new List<List<string>>();
    if (size == 0)
    {
        result.Add(new List<string>());
        return result;
    }
    foreach (string card in cards)
    {
        List<string> subList = new List<string>(cards);
        subList.Remove(card);
        List<List<string>> subCombinations = GetAllCombinations(subList, size - 1);
        foreach (List<string> subCombination in subCombinations)
        {
            subCombination.Add(card);
            result.Add(subCombination);
        }
    }
    return result;
}

// Helper function to evaluate a hand and return its rank and name
private static List<string> EvaluateHand(List<string> cards)
{
    // TODO: Implement hand evaluation logic here
    return cards; // Placeholder implementation
}

// Helper function to compare two hands and determine which is better
private static int CompareHands(List<string> hand1, List<string> hand2)
{
    // TODO: Implement hand comparison logic here
    return 0; // Placeholder implementation
}




public static List<string> GetBestPokerHand(List<string> playerCards, List<string> communityCards)
{
    // Combine player and community cards into one list
    List<string> allCards = new List<string>(playerCards);
    allCards.AddRange(communityCards);

    // Generate all possible 5-card combinations from the cards
    List<List<string>> combinations = GetAllCombinations(allCards, 5);

    // Evaluate each combination and determine the best hand
    List<string> bestHand = null;
    foreach (List<string> combination in combinations)
    {
        List<string> currentHand = EvaluateHand(combination);
        if (bestHand == null || currentHand.CompareTo(bestHand) > 0)
        {
            bestHand = currentHand;
        }
    }

    return bestHand;
}

// Helper function to generate all possible combinations of a list of cards
private static List<List<string>> GetAllCombinations(List<string> cards, int size)
{
    List<List<string>> result = new List<List<string>>();
    if (size == 0)
    {
        result.Add(new List<string>());
        return result;
    }
    foreach (string card in cards)
    {
        List<string> subList = new List<string>(cards);
        subList.Remove(card);
        List<List<string>> subCombinations = GetAllCombinations(subList, size - 1);
        foreach (List<string> subCombination in subCombinations)
        {
            subCombination.Add(card);
            result.Add(subCombination);
        }
    }
    return result;
}

// Helper function to evaluate a hand and return its rank and name
private static List<string> EvaluateHand(List<string> cards)
{
    // TODO: Implement hand evaluation logic here
    return cards; // Placeholder implementation
}

// Helper class representing a poker hand
private class PokerHand : IComparable<PokerHand>
{
    public List<string> Cards { get; private set; }

    public PokerHand(List<string> cards)
    {
        Cards = cards;
    }

    // Compare two poker hands based on their rank and card values
    public int CompareTo(PokerHand other)
    {
        // TODO: Implement hand comparison logic here
        return 0; // Placeholder implementation
    }

*/

//Using C#, create a basic class framework for a 1v1 poker game.
