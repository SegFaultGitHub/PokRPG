using Code.Cards.Triggers;

namespace Code.Jokers.Classes {
    public class C_JokerTriggerNextCard : AC_Joker {
        #region Members
        #endregion

        #region Getters / Setters
        #endregion

        #region Static / Readonly / Const
        #endregion

        public C_JokerTriggerNextCard() : base() {
            this.Trigger = new C_TriggerNextCard(this);
        }
    }
}
