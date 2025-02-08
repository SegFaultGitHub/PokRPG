using System.Collections.Generic;
using Code.Cards.Classes;
using Code.Jokers.Classes;

namespace Code.Cards.Triggers {
    public class C_ExtraTriggers {
        #region Members
        #endregion

        #region Getters / Setters
        private readonly Dictionary<E_Rank, int> Ranks = new();
        private readonly Dictionary<E_Suit, int> Suits = new();
        private readonly Dictionary<int, int> Indexes = new();
        private readonly Dictionary<(E_Rank, E_Suit), int> Cards = new();

        private readonly List<AC_Trigger> Triggers = new();
        #endregion

        #region Static / Readonly / Const
        #endregion

        public void AddRank(E_Rank rank, int count) {
            this.Ranks.TryAdd(rank, 0);
            this.Ranks[rank] += count;
        }

        public void AddSuit(E_Suit suit, int count) {
            this.Suits.TryAdd(suit, 0);
            this.Suits[suit] += count;
        }

        public void AddIndex(int index, int count) {
            this.Indexes.TryAdd(index, 0);
            this.Indexes[index] += count;
        }

        public void AddCard(E_Rank rank, E_Suit suit, int count) {
            this.Cards.TryAdd((rank, suit), 0);
            this.Cards[(rank, suit)] += count;
        }

        public void AddJoker(AC_Joker joker) {
            this.AddTrigger(joker.Trigger);
        }

        private void AddTrigger(AC_Trigger trigger) {
            this.Triggers.Add(trigger);
        }

        public int GetRank(E_Rank rank) {
            this.Ranks.TryGetValue(rank, out int count);
            return count;
        }

        public int GetSuit(E_Suit suit) {
            this.Suits.TryGetValue(suit, out int count);
            return count;
        }

        public int GetIndex(int index) {
            this.Indexes.TryGetValue(index, out int count);
            return count;
        }

        public int GetCard(E_Rank rank, E_Suit suit) {
            this.Cards.TryGetValue((rank, suit), out int count);
            return count;
        }

        public List<AC_Trigger> GetTriggers() {
            return this.Triggers;
        }
    }
}
