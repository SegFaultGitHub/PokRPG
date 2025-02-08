using Code.Cards.Triggers;

namespace Code.Jokers.Classes {
    public class C_JokerTriggerPreviousCard : AC_Joker {
        #region Members
        #endregion

        #region Getters / Setters
        #endregion

        #region Static / Readonly / Const
        #endregion

        public C_JokerTriggerPreviousCard() : base() {
            this.Trigger = new C_TriggerPreviousCard(this);
        }
    }
}
