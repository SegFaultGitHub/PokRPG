using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Cards.Classes {
    public enum E_Rank {
        Two = 0,
        Three = 1,
        Four = 2,
        Five = 3,
        Six = 4,
        Seven = 5,
        Eight = 6,
        Nine = 7,
        Ten = 8,
        Jack = 9,
        Queen = 10,
        King = 11,
        Ace = 12
    }

    public enum E_Suit {
        Hearts = 0,
        Clubs = 1,
        Diamonds = 2,
        Spades = 3
    }

    [Serializable]
    public class C_Card {
        #region Members
        [SerializeField] private protected E_Rank m_Rank;
        [SerializeField] private protected E_Suit m_Suit;
        #endregion

        #region Getters / Setters
        public E_Rank Rank { get => this.m_Rank; set => this.m_Rank = value; }
        public E_Suit Suit { get => this.m_Suit; set => this.m_Suit = value; }

        private Guid ID;
        #endregion

        #region Static / Readonly / Const
        public static readonly Dictionary<E_Rank, int> VALUES = new() {
            { E_Rank.Two, 2 },
            { E_Rank.Three, 3 },
            { E_Rank.Four, 4 },
            { E_Rank.Five, 5 },
            { E_Rank.Six, 6 },
            { E_Rank.Seven, 7 },
            { E_Rank.Eight, 8 },
            { E_Rank.Nine, 9 },
            { E_Rank.Ten, 10 },
            { E_Rank.Jack, 11 },
            { E_Rank.Queen, 12 },
            { E_Rank.King, 13 },
            { E_Rank.Ace, 15 },
        };
        #endregion

        public C_Card() {
            this.ID = Guid.NewGuid();
        }

        public override string ToString() {
            return $"{this.Rank} of {this.Suit}";
        }
    }
}
