using Code.Cards.Classes;
using MyBox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Code.Cards.MonoBehaviours {
    public class MB_Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        #region Members
        [Foldout("Card", true)]
        [SerializeField] private protected Image m_Recto;
        [SerializeField] private protected Image m_Verso;

        [SerializeField] private protected GameObject m_RaycastTargetInactive;
        [SerializeField] private protected GameObject m_RaycastTargetSelected;
        [SerializeField] private protected GameObject m_RaycastTargetInHand;

        [Separator("Read only")]
        [ReadOnly][SerializeField] private protected C_Card m_Card;
        [ReadOnly][SerializeField] private protected Animator m_Animator;
        [ReadOnly][SerializeField] private protected MB_Hand m_Hand;
        #endregion

        #region Getters / Setters
        private Image Recto { get => this.m_Recto; }
        private Image Verso { get => this.m_Verso; }

        private GameObject RaycastTargetInactive { get => this.m_RaycastTargetInactive; }
        private GameObject RaycastTargetSelected { get => this.m_RaycastTargetSelected; }
        private GameObject RaycastTargetInHand { get => this.m_RaycastTargetInHand; }

        public C_Card Card { get => this.m_Card; private set => this.m_Card = value; }
        private Animator Animator { get => this.m_Animator; set => this.m_Animator = value; }
        private MB_Hand Hand { get => this.m_Hand; set => this.m_Hand = value; }
        #endregion

        #region Static / Readonly / Const
        private static readonly int TRIGGER = Animator.StringToHash("trigger");
        private static readonly int RANDOM_TRIGGER = Animator.StringToHash("random-trigger");
        private static readonly int ON_ENTER = Animator.StringToHash("on-enter");
        private static readonly int ON_EXIT = Animator.StringToHash("on-exit");
        private static readonly int SCORING = Animator.StringToHash("scoring");
        private static readonly int TRIGGER_SPEED = Animator.StringToHash("trigger-speed");
        #endregion

        #region Unity methods
        private void Awake() {
            this.Animator = this.GetComponent<Animator>();
        }

        private void Start() {
            this.transform.localScale = Vector3.one;
            this.Hand = this.GetComponentInParent<MB_Hand>();
        }

        private void FixedUpdate() {
            float dot = Vector3.Dot(this.transform.forward, Vector3.forward);
            this.Recto.gameObject.SetActive(dot >= 0);
            this.Verso.gameObject.SetActive(dot < 0);
        }

        public void OnPointerEnter(PointerEventData _) {
            if (this.Hand.Blocked) return;

            this.Hand.OnEnterCard(this);
            this.Animator.SetTrigger(ON_ENTER);
            this.RaycastTargetInactive.SetActive(false);
            bool selected = this.Hand.IsSelected(this);
            this.RaycastTargetSelected.SetActive(selected);
            this.RaycastTargetInHand.SetActive(!selected);
        }

        public void OnPointerExit(PointerEventData _) {
            if (this.Hand.Blocked) return;

            this.Hand.OnExitCard(this);
            this.Animator.SetTrigger(ON_EXIT);
            this.RaycastTargetInactive.SetActive(true);
            this.RaycastTargetSelected.SetActive(false);
            this.RaycastTargetInHand.SetActive(false);
        }

        public void OnPointerClick(PointerEventData _) {
            if (this.Hand.Blocked) return;

            this.Hand.OnClickCard(this);
            this.OnPointerExit(null);
        }
        #endregion

        public void SetCard(C_Card card, MB_CardMetadata cardMetadata) {
            this.Card = card;
            this.name = card.ToString();
            this.Recto.sprite = cardMetadata.GetSprite(this.Card);
        }

        public void Trigger(float triggerSpeed) {
            this.Animator.SetInteger(RANDOM_TRIGGER, Random.Range(0, 2));
            this.Animator.SetTrigger(TRIGGER);
            this.Animator.SetFloat(TRIGGER_SPEED, triggerSpeed);
        }

        public void SetScoring(bool scoring) {
            this.Animator.SetBool(SCORING, scoring);
        }
    }
}
