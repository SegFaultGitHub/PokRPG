using System.Collections.Generic;
using Code.Cards.Classes;
using Code.Jokers.Classes;
using UnityEngine;

namespace Code.Cards.Triggers {
    public class C_TriggerRandomCard : AC_JokerTrigger {
        public C_TriggerRandomCard(AC_Joker joker) : base(joker) { }

        public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
            List<C_CardTrigger> triggers = new() {
                new C_CardTrigger {
                    Index = Random.Range(0, cards.Count),
                    Parent = index,
                    Source = this.Source
                }
            };

            return triggers;
        }
    }
}
