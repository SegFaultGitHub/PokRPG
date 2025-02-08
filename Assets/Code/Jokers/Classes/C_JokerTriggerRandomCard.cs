using Code.Cards.Triggers;

namespace Code.Jokers.Classes {
    public class C_JokerTriggerRandomCard : AC_Joker {
        #region Members
        #endregion

        #region Getters / Setters
        #endregion

        #region Static / Readonly / Const
        #endregion

        public C_JokerTriggerRandomCard() : base() {
            this.Trigger = new C_TriggerRandomCard(this);
        }
    }
}
