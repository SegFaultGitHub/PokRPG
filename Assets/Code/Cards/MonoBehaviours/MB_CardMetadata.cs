using System;
using System.Collections.Generic;
using Code.Cards.Classes;
using MyBox;
using UnityEngine;

namespace Code.Cards.MonoBehaviours {
    public class MB_CardMetadata : MonoBehaviour {
        #region Members
        [Foldout("CardMetadata", true)]
        [SerializeField] private protected Sprite[] m_Hearts;
        [SerializeField] private protected Sprite[] m_Clubs;
        [SerializeField] private protected Sprite[] m_Diamonds;
        [SerializeField] private protected Sprite[] m_Spades;
        #endregion

        #region Getters / Setters
        private Sprite[] Hearts { get => this.m_Hearts; }
        private Sprite[] Clubs { get => this.m_Clubs; }
        private Sprite[] Diamonds { get => this.m_Diamonds; }
        private Sprite[] Spades { get => this.m_Spades; }
        #endregion

        #region Static / Readonly / Const
        private static readonly Dictionary<E_Rank, int> RANK_TO_INDEX_MAPPING = new() {
            { E_Rank.Two, 0 },
            { E_Rank.Three, 1 },
            { E_Rank.Four, 2 },
            { E_Rank.Five, 3 },
            { E_Rank.Six, 4 },
            { E_Rank.Seven, 5 },
            { E_Rank.Eight, 6 },
            { E_Rank.Nine, 7 },
            { E_Rank.Ten, 8 },
            { E_Rank.Jack, 9 },
            { E_Rank.Queen, 10 },
            { E_Rank.King, 11 },
            { E_Rank.Ace, 12 }
        };
        #endregion

        #region Unity methods
        #endregion

        public Sprite GetSprite(C_Card card) {
            return card.Suit switch {
                E_Suit.Hearts => this.Hearts[RANK_TO_INDEX_MAPPING[card.Rank]],
                E_Suit.Clubs => this.Clubs[RANK_TO_INDEX_MAPPING[card.Rank]],
                E_Suit.Diamonds => this.Diamonds[RANK_TO_INDEX_MAPPING[card.Rank]],
                E_Suit.Spades => this.Spades[RANK_TO_INDEX_MAPPING[card.Rank]],
                _ => throw new ArgumentOutOfRangeException(nameof(card.Suit), card.Suit, null)
            };
        }
    }
}
