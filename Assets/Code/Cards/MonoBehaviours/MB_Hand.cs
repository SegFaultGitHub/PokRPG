using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Audio;
using Code.Cards.Classes;
using Code.Utils;
using External.LeanTween.Framework;
using MyBox;
using UnityEngine;

namespace Code.Cards.MonoBehaviours {
    public enum E_SortPredicate {
        None,
        Rank,
        Suit
    }

    public class MB_Hand : MonoBehaviour {
        #region Members
        [Foldout("HandBehaviour", true)]
        [SerializeField] private protected float m_HandCardAngleOffset = 4;
        [SerializeField] private protected float m_HandCardDistance = 90;
        [SerializeField] private protected Transform m_HandPosition;

        [SerializeField] private protected float m_SelectedCardDistance = 105;
        [SerializeField] private protected Transform m_SelectedPosition;

        [SerializeField] private protected Transform m_CardsParent;
        [SerializeField] private protected Transform m_Deck;

        [SerializeField][Range(0, 1)] private protected float m_HandOnOverCompressionRatio = .45f;
        [SerializeField][Range(0, 1)] private protected float m_SelectedOnOverCompressionRatio = .75f;

        [SerializeField][Range(0, 1)] private protected float m_DelayBetweenDiscards = .125f;

        [Separator("Read only")]
        [ReadOnly][SerializeField] private protected E_SortPredicate m_SortPredicate;
        
        [ReadOnly][SerializeField] private protected MB_Card m_Over;
        [ReadOnly][SerializeField] private protected List<MB_Card> m_Selected;
        [ReadOnly][SerializeField] private protected List<MB_Card> m_Hand;

        [ReadOnly][SerializeField] private protected int m_Blocks;

        [ReadOnly][SerializeField] private protected MB_AudioManager m_AudioManager;
        #endregion

        #region Getters / Setters
        private float HandCardAngleOffset { get => this.m_HandCardAngleOffset; }
        private float HandCardDistance { get => this.m_HandCardDistance; }
        private Transform HandPosition { get => this.m_HandPosition; }

        private float SelectedCardDistance { get => this.m_SelectedCardDistance; }
        private Transform SelectedPosition { get => this.m_SelectedPosition; }

        private Transform CardsParent { get => this.m_CardsParent; }
        private Transform Deck { get => this.m_Deck; }

        private float HandOnOverCompressionRatio { get => this.m_HandOnOverCompressionRatio; }
        private float SelectedOnOverCompressionRatio { get => this.m_SelectedOnOverCompressionRatio; }

        public float DelayBetweenDiscards { get => this.m_DelayBetweenDiscards; }

        private E_SortPredicate SortPredicate { get => this.m_SortPredicate; set => this.m_SortPredicate = value; }

        private MB_Card Over { get => this.m_Over; set => this.m_Over = value; }
        private List<MB_Card> Selected { get => this.m_Selected; }
        private List<MB_Card> Hand { get => this.m_Hand; }

        private int Blocks { get => this.m_Blocks; set => this.m_Blocks = value; }
        public bool Blocked { get => this.Blocks > 0; }

        private MB_AudioManager AudioManager { get => this.m_AudioManager; set => this.m_AudioManager = value; }

        public int Size { get => this.Hand.Count; }
        private readonly Dictionary<MB_Card, Vector3> CardPositions = new();
        private readonly Dictionary<MB_Card, Vector3> CardRotations = new();
        #endregion

        #region Static / Readonly / Const
        #endregion

        #region Unity methods
        private void Awake() {
            this.AudioManager = FindFirstObjectByType<MB_AudioManager>();
        }

        private void FixedUpdate() {
            void _AdjustCardPosition(MB_Card card) {
                if (!this.CardPositions.ContainsKey(card) || !this.CardRotations.ContainsKey(card)) return;

                card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, this.CardPositions[card], .15f);
                card.transform.localEulerAngles = SC_Utils.LerpAngle(card.transform.localEulerAngles, this.CardRotations[card], .1f);
            }

            this.Hand.ForEach(_AdjustCardPosition);
            this.Selected.ForEach(_AdjustCardPosition);
        }

        private void OnValidate() => this.RecomputeCardPositions();
        #endregion

        public void AddCardToHand(MB_Card card) {
            this.Hand.Add(card);
            card.transform.SetParent(this.CardsParent);
            card.transform.position = this.Deck.position;
            card.transform.eulerAngles = this.Deck.eulerAngles;
            this.AudioManager.PlayCardDrawn();
            this.RecomputeCardPositions();
        }

        public void OnEnterCard(MB_Card card) {
            if (this.Blocked) return;

            this.Over = card;
            this.RecomputeCardPositions();
        }

        public void OnExitCard(MB_Card card) {
            if (this.Blocked) return;

            this.Over = null;
            this.RecomputeCardPositions();
        }

        public void OnClickCard(MB_Card card) {
            if (this.Blocked) return;

            if (this.Selected.Contains(card)) this.Deselect(card);
            else this.Select(card);
        }

        public bool IsSelected(MB_Card card) {
            return this.Selected.Contains(card);
        }

        private void Select(MB_Card card) {
            if (this.Selected.Count >= 5) return;
            if (!this.Hand.Contains(card)) return;

            this.Selected.Add(card);
            this.Hand.Remove(card);
            this.AudioManager.PlayCardDrawn();
            this.RecomputeCardPositions();
            this.ComputeHand();
        }

        private void Deselect(MB_Card card) {
            if (!this.Selected.Contains(card)) return;

            this.Selected.Remove(card);
            this.Hand.Add(card);
            this.AudioManager.PlayCardDrawn();
            this.RecomputeCardPositions();
            this.ComputeHand();
        }

        private void ComputeHand() {
            this.Hand.ForEach(card => card.SetScoring(true));
            this.Selected.ForEach(card => card.SetScoring(true));

            if (this.Selected.Count == 0) return;
            C_PlayedHand playedHand = SC_Hands.GetPokerHand(this.Selected.Select(card => card.Card).ToList());
            foreach (MB_Card card in this.Selected) {
                card.SetScoring(playedHand.ScoringCards.Contains(card.Card));
            }
        }

        public List<MB_Card> Play() {
            foreach (MB_Card card in this.Selected) {
                this.CardPositions.Remove(card);
                this.CardRotations.Remove(card);
            }
            this.RecomputeCardPositions();
            return this.Selected;
        }

        public List<MB_Card> Discard() {
            foreach (MB_Card card in this.Selected) {
                this.CardPositions.Remove(card);
                this.CardRotations.Remove(card);
            }
            this.RecomputeCardPositions();
            return this.Selected;
        }

        [ButtonMethod]
        private void RecomputeCardPositions() {
            this.SortHand();

            float totalSelectedWidth = (this.Selected.Count - 1) * this.SelectedCardDistance;
            float startX = -(this.Selected.Count - 1) * this.SelectedCardDistance / 2;
            float stepX;
            if (this.Selected.Contains(this.Over)) {
                if (this.Over == this.Selected[0] || this.Over == this.Selected[^1])
                    stepX = totalSelectedWidth / (this.Selected.Count - 1 + this.SelectedOnOverCompressionRatio);
                else
                    stepX = totalSelectedWidth / (this.Selected.Count - 1 + this.SelectedOnOverCompressionRatio * 2);
            } else {
                stepX = totalSelectedWidth / (this.Selected.Count - 1);
            }
            foreach (MB_Card card in this.Selected) {
                if (card == this.Over) {
                    if (card != this.Selected[0]) startX += stepX * this.SelectedOnOverCompressionRatio;
                    this.CardPositions[card] = this.SelectedPosition.localPosition + new Vector3(startX, 0);
                    this.CardRotations[card] = new Vector3(0, 0, 0);
                    startX += stepX * this.SelectedOnOverCompressionRatio;
                } else {
                    this.CardPositions[card] = this.SelectedPosition.localPosition + new Vector3(startX, 0);
                    this.CardRotations[card] = new Vector3(0, 0, 0);
                }
                startX += stepX;
            }

            float radius = 180f * this.HandCardDistance / (Mathf.PI * this.HandCardAngleOffset);
            Vector3 innerCirclePosition = this.HandPosition.localPosition + new Vector3(0, -radius, 0);
            float totalFanAngle = (this.Hand.Count - 1) * this.HandCardAngleOffset;
            // Place the first card at the top of the circle (90 deg), + a calculated offset based on the card count
            float startAngle = (this.Hand.Count - 1) * this.HandCardAngleOffset / 2 + 90;;
            float stepAngle;
            if (this.Hand.Contains(this.Over)) {
                if (this.Over == this.Hand[0] || this.Over == this.Hand[^1])
                    stepAngle = totalFanAngle / (this.Hand.Count - 1 + this.HandOnOverCompressionRatio);
                else
                    stepAngle = totalFanAngle / (this.Hand.Count - 1 + this.HandOnOverCompressionRatio * 2);
            } else {
                stepAngle = totalFanAngle / (this.Hand.Count - 1);
            }
            foreach (MB_Card card in this.Hand) {
                if (card == this.Over) {
                    if (card != this.Hand[0]) startAngle -= stepAngle * this.HandOnOverCompressionRatio;
                    float x = Mathf.Cos(Mathf.Deg2Rad * startAngle) * radius;
                    float y = Mathf.Sin(Mathf.Deg2Rad * startAngle) * radius;

                    this.CardPositions[card] = innerCirclePosition + new Vector3(x, y + 75);
                    this.CardRotations[card] = new Vector3(0, 0, 0);
                    startAngle -= stepAngle * this.HandOnOverCompressionRatio;
                } else {
                    float x = Mathf.Cos(Mathf.Deg2Rad * startAngle) * radius;
                    float y = Mathf.Sin(Mathf.Deg2Rad * startAngle) * radius;

                    this.CardPositions[card] = innerCirclePosition + new Vector3(x, y);
                    this.CardRotations[card] = new Vector3(0, 0, startAngle - 90);
                }

                startAngle -= stepAngle;
            }

            if (this.Over != null) this.Over.transform.SetSiblingIndex(-1);

            // Sort card in order for hand, reverse order for selected (for display purposes)
            int childIndex = 0;
            for (int i = this.Selected.Count - 1; i >= 0; i--) {
                this.Selected[i].transform.SetSiblingIndex(childIndex);
                childIndex++;
            }
            foreach (MB_Card card in this.Hand) {
                card.transform.SetSiblingIndex(childIndex);
                childIndex++;
            }

            if (this.Over != null) this.Over.transform.SetSiblingIndex(-1);
        }

        private void SortHand() {
            void _SortByRank() => this.Hand.Sort((card1, card2) => -card1.Card.Rank.CompareTo(card2.Card.Rank));
            void _SortBySuit() => this.Hand.Sort((card1, card2) => card1.Card.Suit.CompareTo(card2.Card.Suit));

            switch (this.SortPredicate) {
                case E_SortPredicate.Rank:
                    _SortBySuit();
                    _SortByRank();
                    break;
                case E_SortPredicate.Suit:
                    _SortByRank();
                    _SortBySuit();
                    break;
                case E_SortPredicate.None:
                default:
                    break;
            }
        }

        public void SortByRanks() {
            this.SortPredicate = E_SortPredicate.Rank;
            this.RecomputeCardPositions();
        }

        public void SortBySuits() {
            this.SortPredicate = E_SortPredicate.Suit;
            this.RecomputeCardPositions();
        }

        public void StopSorting() {
            this.SortPredicate = E_SortPredicate.None;
        }

        public void Block() {
            this.Blocks++;
            this.Over = null;
            this.RecomputeCardPositions();
        }

        public void Unblock() {
            this.Blocks--;
            this.Over = null;
            this.RecomputeCardPositions();
        }

        public IEnumerator SendSelectedToDeckCoroutine() {
            void _SendCardToDeck(MB_Card card) {
                this.AudioManager.PlayCardDrawn();
                LeanTween.move(card.gameObject, this.Deck.position, 0.3f).setEaseInSine();
                LeanTween.rotate(card.gameObject, this.Deck.eulerAngles, 0.3f)
                    .setOnComplete(() => {
                            this.Selected.Remove(card);
                            Destroy(card.gameObject);
                        }
                    );
            }

            for (int i = 0; i < this.Selected.Count; i++) {
                MB_Card card = this.Selected[i];
                this.InSeconds(i * this.DelayBetweenDiscards, () => _SendCardToDeck(card));
            }

            yield return new WaitUntil(() => this.Selected.Count == 0);
        }
    }
}
