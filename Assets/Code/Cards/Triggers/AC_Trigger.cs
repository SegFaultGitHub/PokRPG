using System;
using System.Collections.Generic;
using Code.Cards.Classes;
using Code.Jokers.Classes;

namespace Code.Cards.Triggers {
    public enum E_TriggerSourceType {
        Hand,
        Joker
    }

    public abstract class AC_TriggerSource {
        protected E_TriggerSourceType TriggerSourceType;
    }

    public class C_HandTriggerSource : AC_TriggerSource {
        private readonly E_Hand Hand;

        public C_HandTriggerSource(E_Hand hand) {
            this.TriggerSourceType = E_TriggerSourceType.Hand;
            this.Hand = hand;
        }

        public override string ToString() {
            return $"{this.TriggerSourceType}: {this.Hand}";
        }
    }

    public class C_JokerTriggerSource : AC_TriggerSource {
        public readonly AC_Joker Joker;

        public C_JokerTriggerSource(AC_Joker joker) {
            this.TriggerSourceType = E_TriggerSourceType.Joker;
            this.Joker = joker;
        }

        public override string ToString() {
            return $"{this.TriggerSourceType}: {this.Joker}";
        }
    }

    public abstract class AC_Trigger {
        protected AC_TriggerSource Source;

        public abstract IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index);
    }

    public abstract class AC_JokerTrigger : AC_Trigger {
        protected AC_JokerTrigger(AC_Joker joker) {
            this.Source = new C_JokerTriggerSource(joker);
        }
    }

    public abstract class AC_HandTrigger : AC_Trigger {
        protected AC_HandTrigger(E_Hand hand) {
            this.Source = new C_HandTriggerSource(hand);
        }
    }
}
