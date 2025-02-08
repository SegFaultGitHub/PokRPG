using System.Collections.Generic;
using Code.Cards.Classes;
using Code.Jokers.Classes;

namespace Code.Cards.Triggers {
    public class C_TriggerPreviousCard : AC_JokerTrigger {
        public C_TriggerPreviousCard(AC_Joker joker) : base(joker) { }

        public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
            List<C_CardTrigger> triggers = new();

            if (index - 1 >= 0)
                triggers.Add(
                    new C_CardTrigger {
                        Index = index - 1,
                        Parent = index,
                        Source = this.Source
                    }
                );

            return triggers;
        }
    }
}
