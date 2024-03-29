﻿namespace Solitare
{
    public class Deck
    {
        public static List<Card> cards;
        public int size;
        public Deck(String[] ranks, String[] suits)
        {
            cards = new List<Card>();
            for (int j = 0; j < ranks.Length; j++)
            {
                foreach (String suitString in suits)
                {
                    cards.Add(new Card(ranks[j], suitString));
                }
            }
            size = cards.Count();
            shuffle();
        }
        public Deck()
        {
            size = cards.Count();
        }

        public Boolean isEmpty()
        {
            return size == 0;
        }
        public int getSize()
        {
            return size;
        }

        public List<Card> getDeck()
        {
            return cards;
        }

        public void shuffle()
        {
            Random rand = new Random();
            for (int k = cards.Count() - 1; k > 0; k--)
            {
                int howMany = k + 1;
                int start = 0;
                int randPos = (int)(rand.Next(start, howMany + start));    //check this?
                Card temp = cards[k];
                cards[k] = cards[randPos];
                cards[randPos] = temp;
            }
            size = cards.Count();
        }
        public Card deal()
        {
            if (isEmpty())
            {
                return null;
            }
            size--;
            Card c = cards[size];
            return c;
        }
    }

}
