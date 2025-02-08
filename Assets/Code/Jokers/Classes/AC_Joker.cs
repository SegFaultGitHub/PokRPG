using System;
using Code.Cards.Triggers;

namespace Code.Jokers.Classes {
    [Serializable]
    public abstract class AC_Joker {
        #region Members
        #endregion

        #region Getters / Setters
        public AC_JokerTrigger Trigger { get; protected set; }
        private Guid ID;
        #endregion

        #region Static / Readonly / Const
        #endregion

        protected AC_Joker() {
            this.ID = Guid.NewGuid();
        }
    }
}
