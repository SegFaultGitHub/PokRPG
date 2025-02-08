
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Audio;
using Code.Cards.Classes;
using Code.Cards.MonoBehaviours;
using Code.Cards.Triggers;
using Code.Jokers.Classes;
using Code.Jokers.MonoBehaviours;
using Code.Utils;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Fight {
    public class MB_FightManager : MonoBehaviour {
        #region Members
        [Foldout("FightManager", true)]
        [SerializeField] private protected int m_HandSize = 6;
        [SerializeField] private protected MB_Hand m_Hand;

        [SerializeField] private protected MB_Card m_CardPrefab;
        [SerializeField] private protected MB_CardMetadata m_CardMetadata;

        [SerializeField][Range(0, 1)] private protected float m_InitialTriggerDelay = .6f;
        [SerializeField][Range(0, 1)] private protected float m_TriggerDelayRatio = .9f;
        [SerializeField][Range(0, 1)] private protected float m_MinTriggerDelay = .05f;
        [SerializeField][Range(0, 2)] private protected float m_DelayBeforeDiscardAfterPlay = 1;

        [Separator("Read only")]
        [ReadOnly][SerializeField] private protected List<C_Card> m_Deck;
        [ReadOnly][SerializeField] private protected List<C_Card> m_Discarded;

        [ReadOnly][SerializeField] private protected MB_AudioManager m_AudioManager;
        #endregion

        #region Getters / Setters
        private int HandSize { get => this.m_HandSize; }
        private MB_Hand Hand { get => this.m_Hand; }

        private MB_Card CardPrefab { get => this.m_CardPrefab; }
        private MB_CardMetadata CardMetadata { get => this.m_CardMetadata; }

        private float InitialTriggerDelay { get => this.m_InitialTriggerDelay; }
        private float TriggerDelayRatio { get => this.m_TriggerDelayRatio; }
        private float MinTriggerDelay { get => this.m_MinTriggerDelay; }
        private float DelayBeforeDiscardAfterPlay { get => this.m_DelayBeforeDiscardAfterPlay; }

        private List<C_Card> Deck { get => this.m_Deck; set => this.m_Deck = value; }
        private List<C_Card> Discarded { get => this.m_Discarded; set => this.m_Discarded = value; }

        private MB_AudioManager AudioManager { get => this.m_AudioManager; set => this.m_AudioManager = value; }

        // TEMP
        [SerializeField] private TMP_Text HandLabel;
        [SerializeField] private TMP_Text TriggerSourceLabel;
        [SerializeField] private GameObject Arrow;
        [SerializeField] private Transform JokerParent;
        [SerializeField] private MB_Joker JokerPrefab;
        private C_ExtraTriggers ExtraTriggers;
        private readonly Dictionary<AC_Joker, MB_Joker> Jokers = new();
        #endregion

        #region Static / Readonly / Const
        #endregion

        #region Unity methods
        private void Awake() {
            List<E_Rank> ranks = new() {
                E_Rank.Two,
                E_Rank.Three,
                E_Rank.Four,
                E_Rank.Five,
                E_Rank.Six,
                E_Rank.Seven,
                E_Rank.Eight,
                E_Rank.Nine,
                E_Rank.Ten,
                E_Rank.Jack,
                E_Rank.Queen,
                E_Rank.King,
                E_Rank.Ace
            };
            List<E_Suit> suits = new() {
                E_Suit.Hearts,
                E_Suit.Clubs,
                E_Suit.Diamonds,
                E_Suit.Spades
            };

            this.Deck = suits.SelectMany(
                    _ => ranks,
                    (suit, rank) => new C_Card {
                        Rank = rank,
                        Suit = suit
                    }
                )
                .ToList();
            this.Discarded = new List<C_Card>();

            this.AudioManager = FindFirstObjectByType<MB_AudioManager>();

            C_JokerTriggerPreviousCard jokerPrevious = new();
            C_JokerTriggerNextCard jokerNext = new();
            C_JokerTriggerRandomCard jokerRandom1 = new();
            C_JokerTriggerRandomCard jokerRandom2 = new();
            this.ExtraTriggers = new C_ExtraTriggers();
            this.ExtraTriggers.AddJoker(jokerPrevious);
            this.ExtraTriggers.AddJoker(jokerNext);
            this.ExtraTriggers.AddJoker(jokerRandom1);
            this.ExtraTriggers.AddJoker(jokerRandom2);

            MB_Joker jokerPreviousObj = Instantiate(this.JokerPrefab,this.JokerParent);
            jokerPreviousObj.SetJoker(jokerPrevious);
            this.Jokers[jokerPrevious] = jokerPreviousObj;

            MB_Joker jokerNextObj = Instantiate(this.JokerPrefab,this.JokerParent);
            jokerNextObj.SetJoker(jokerNext);
            this.Jokers[jokerNext] = jokerNextObj;

            MB_Joker jokerRandom1Obj = Instantiate(this.JokerPrefab,this.JokerParent);
            jokerRandom1Obj.SetJoker(jokerRandom1);
            this.Jokers[jokerRandom1] = jokerRandom1Obj;

            MB_Joker jokerRandom2Obj = Instantiate(this.JokerPrefab,this.JokerParent);
            jokerRandom2Obj.SetJoker(jokerRandom2);
            this.Jokers[jokerRandom2] = jokerRandom2Obj;
        }

        private void Start() {
            this.DrawCards();
        }
        #endregion

        public void DrawCards() {
            IEnumerator _Coroutine() {
                if (this.HandSize > this.Hand.Size) {
                    // Keep drawing
                    if (this.Deck.Count == 0) {
                        this.Deck = new List<C_Card>(this.Discarded);
                        this.Discarded.Clear();
                    }

                    C_Card card = SC_Utils.Sample(this.Deck);
                    this.Deck.Remove(card);

                    MB_Card cardObj = Instantiate(this.CardPrefab);
                    cardObj.SetCard(card, this.CardMetadata);
                    this.Hand.AddCardToHand(cardObj);

                    yield return new WaitForSeconds(this.Hand.DelayBetweenDiscards);
                    this.StartCoroutine(_Coroutine());
                } else {
                    // Stop drawing
                    this.Hand.Unblock();
                }
            }

            this.Hand.Block();
            this.StartCoroutine(_Coroutine());
        }

        public void PlayCards() {
            float _Pitch(float wait) {
                return SC_Utils.MapFrom(this.MinTriggerDelay, this.InitialTriggerDelay, 2, .8f, wait);
            }
            float _AnimationSpeed(float wait) {
                return SC_Utils.MapFrom(this.MinTriggerDelay, this.InitialTriggerDelay, 4, 1, wait);
            }

            IEnumerator _Coroutine(IReadOnlyList<MB_Card> cards, IReadOnlyList<C_CardTrigger> triggers, int current, float wait) {
                cards[triggers[current].Index].transform.SetSiblingIndex(-1);
                cards[triggers[current].Index].Trigger(_AnimationSpeed(wait));
                this.AudioManager.PlayCardScored(volume: .5f, pitch: _Pitch(wait));

                if (triggers[current].Source is C_JokerTriggerSource jokerTriggerSource) {
                    MB_Joker joker = this.Jokers[jokerTriggerSource.Joker];
                    joker.Trigger(_AnimationSpeed(wait));
                    this.AudioManager.PlayJokerTriggered(volume: 1f, pitch: _Pitch(wait));
                }

                this.TriggerSourceLabel.SetText(triggers[current].Source.ToString());
                this.Arrow.transform.position = cards[triggers[current].Parent].transform.position;

                if (current + 1 < triggers.Count) {
                    float nextDelay = Mathf.Lerp(wait, this.MinTriggerDelay, 1 - this.TriggerDelayRatio);
                    yield return new WaitForSeconds(wait);
                    this.StartCoroutine(_Coroutine(cards, triggers, current + 1, nextDelay));
                } else {
                    yield return new WaitForSeconds(this.DelayBeforeDiscardAfterPlay);
                    this.TriggerSourceLabel.SetText("---");
                    this.Arrow.SetActive(false);
                    this.Hand.Unblock();
                    this.DiscardCards();
                }
            }

            List<MB_Card> playedCards = this.Hand.Play();
            this.Discarded.AddRange(playedCards.Select(card => card.Card));

            C_PlayedHand playedHand = SC_Hands.GetPokerHand(playedCards.Select(card => card.Card).ToList());
            this.HandLabel.SetText(playedHand.Hand.ToString());

            List<C_CardTrigger> triggers = SC_Triggers.Compute(playedHand.Hand, playedHand.ScoringCards, this.ExtraTriggers);
            List<MB_Card> scoringCards = playedCards.Where(card => playedHand.ScoringCards.Contains(card.Card)).ToList();

            this.Arrow.SetActive(true);
            this.Hand.Block();
            this.StartCoroutine(_Coroutine(scoringCards, triggers, 0, this.InitialTriggerDelay));
        }

        public void DiscardCards() {
            IEnumerator _Coroutine() {
                List<MB_Card> discardedCards = this.Hand.Discard();
                this.Discarded.AddRange(discardedCards.Select(card => card.Card));

                yield return this.StartCoroutine(this.Hand.SendSelectedToDeckCoroutine());
                // yield return new WaitForSeconds(this.Hand.DelayBetweenDiscards);
                this.Hand.Unblock();
                this.DrawCards();
            }

            this.HandLabel.SetText("---");
            this.Hand.Block();
            this.StartCoroutine(_Coroutine());
        }
    }
}
