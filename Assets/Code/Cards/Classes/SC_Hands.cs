using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Cards.Classes {
    public enum E_Hand {
        HighCard = 0,
        Pair = 1,
        TwoPairs = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        Flush = 5,
        FullHouse = 6,
        FourOfAKind = 7,
        StraightFlush = 8,
        RoyalFlush = 9,
        FiveOfAKind = 10,
        FlushFullHouse = 11,
        FlushFiveOfAKind = 12
    }

    public class C_PlayedHand {
        public List<C_Card> ScoringCards { get; set; }
        public E_Hand Hand { get; set; }
    }

    public static class SC_Hands {
        private static readonly List<E_Rank> ACE_LOW_STRAIGHT = new() {
            E_Rank.Two,
            E_Rank.Three,
            E_Rank.Four,
            E_Rank.Five,
            E_Rank.Ace
        };

        public static C_PlayedHand GetPokerHand(List<C_Card> cards) {
            List<E_Rank> ranks = cards.Select(card => card.Rank).ToList();
            List<E_Suit> suits = cards.Select(card => card.Suit).ToList();

            Dictionary<E_Rank, int> ranksCounts = new();
            foreach (E_Rank rank in ranks) {
                ranksCounts.TryAdd(rank, 0);
                ranksCounts[rank]++;
            }

            Dictionary<E_Suit, int> suitsCounts = new();
            foreach (E_Suit suit in suits) {
                suitsCounts.TryAdd(suit, 0);
                suitsCounts[suit]++;
            }

            List<E_Rank> sortedRanks = ranksCounts.Keys.ToList();
            sortedRanks.Sort();

            bool isFlush = suitsCounts.Values.Count == 1 && cards.Count == 5;
            bool isStraight = sortedRanks.SequenceEqual(ACE_LOW_STRAIGHT) || sortedRanks.Count == 5 && sortedRanks[^1] - sortedRanks[0] == 4;

            E_Hand eHand;
            List<C_Card> scoringCards;

            if (ranksCounts.Values.Contains(5) && isFlush) {
                eHand = E_Hand.FlushFiveOfAKind;
                scoringCards = cards;
            } else if (ranksCounts.Values.Contains(3) && ranksCounts.Values.Contains(2) && isFlush) {
                eHand = E_Hand.FlushFullHouse;
                scoringCards = cards;
            } else if (ranksCounts.Values.Contains(5)) {
                eHand = E_Hand.FiveOfAKind;
                scoringCards = cards;
            } else if (sortedRanks == ACE_LOW_STRAIGHT && isFlush) {
                eHand = E_Hand.RoyalFlush;
                scoringCards = cards;
            } else if (isStraight && isFlush) {
                eHand = E_Hand.StraightFlush;
                scoringCards = cards;
            } else if (ranksCounts.Values.Contains(4)) {
                eHand = E_Hand.FourOfAKind;
                scoringCards = cards.Where(card => ranksCounts[card.Rank] == 4).ToList();
            } else if (ranksCounts.Values.Contains(3) && ranksCounts.Values.Contains(2)) {
                eHand = E_Hand.FullHouse;
                scoringCards = cards;
            } else if (isFlush) {
                eHand = E_Hand.Flush;
                scoringCards = cards;
            } else if (isStraight) {
                eHand = E_Hand.Straight;
                scoringCards = cards;
            } else if (ranksCounts.Values.Contains(3)) {
                eHand = E_Hand.ThreeOfAKind;
                scoringCards = cards.Where(card => ranksCounts[card.Rank] == 3).ToList();
            } else if (ranksCounts.Values.Count(count => count == 2) == 2) {
                eHand = E_Hand.TwoPairs;
                scoringCards = cards.Where(card => ranksCounts[card.Rank] == 2).ToList();
            } else if (ranksCounts.Values.Contains(2)) {
                eHand = E_Hand.Pair;
                scoringCards = cards.Where(card => ranksCounts[card.Rank] == 2).ToList();
            } else {
                eHand = E_Hand.HighCard;
                scoringCards = cards.Where(card => card.Rank == sortedRanks[^1]).ToList();
            }

            return new C_PlayedHand {
                Hand = eHand,
                ScoringCards = scoringCards
            };
        }
    }
}
