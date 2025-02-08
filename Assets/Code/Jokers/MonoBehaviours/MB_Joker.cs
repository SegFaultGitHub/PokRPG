using Code.Jokers.Classes;
using MyBox;
using UnityEngine;

namespace Code.Jokers.MonoBehaviours {
    public class MB_Joker : MonoBehaviour {
        #region Members
        [Foldout("Joker", true)]

        [Separator("Read only")]
        [ReadOnly][SerializeField] private protected Animator m_Animator;
        #endregion

        #region Getters / Setters
        private Animator Animator { get => this.m_Animator; set => this.m_Animator = value; }

        private AC_Joker Joker { get; set; }
        #endregion

        #region Static / Readonly / Const
        private static readonly int TRIGGER = Animator.StringToHash("trigger");
        private static readonly int RANDOM_TRIGGER = Animator.StringToHash("random-trigger");
        private static readonly int TRIGGER_SPEED = Animator.StringToHash("trigger-speed");
        #endregion

        #region Unity methods
        private void Awake() {
            this.Animator = this.GetComponent<Animator>();
        }

        private void Start() {
            // this.transform.localScale = Vector3.one;
        }
        #endregion

        public void SetJoker(AC_Joker joker) {
            this.Joker = joker;
        }

        public void Trigger(float triggerSpeed) {
            this.Animator.SetInteger(RANDOM_TRIGGER, Random.Range(0, 2));
            this.Animator.SetTrigger(TRIGGER);
            this.Animator.SetFloat(TRIGGER_SPEED, triggerSpeed);
        }
    }
}
