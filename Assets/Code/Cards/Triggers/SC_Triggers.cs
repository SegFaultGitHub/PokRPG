using System.Collections.Generic;
using System.Linq;
using Code.Cards.Classes;

namespace Code.Cards.Triggers {
    public class C_CardTrigger {
        public int Index { get; set; }
        public int Parent { get; set; }
        public AC_TriggerSource Source { get; set; }
    }

    public static class SC_Triggers {
        #region Hand triggers
        private class C_HighCardTrigger : AC_HandTrigger {
            public C_HighCardTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new() {
                    new C_CardTrigger {
                        Parent = index,
                        Index = index,
                        Source = this.Source
                    }
                };
                return triggers;
            }
        }

        private class C_PairTrigger : AC_HandTrigger {
            public C_PairTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        3 + index
                    )
                );
                return triggers;
            }
        }
        private class C_TwoPairsTrigger : AC_HandTrigger {
            public C_TwoPairsTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        2 + index
                    )
                );
                return triggers;
            }
        }
        private class C_ThreeOfAKindTrigger : AC_HandTrigger {
            public C_ThreeOfAKindTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        4 + index
                    )
                );
                return triggers;
            }
        }
        private class C_StraightTrigger : AC_HandTrigger {
            public C_StraightTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new() {
                    new C_CardTrigger {
                        Parent = index,
                        Index = index,
                        Source = this.Source
                    }
                };
                for (int j = index; j < cards.Count; j++)
                    triggers.Add(
                        new C_CardTrigger {
                            Parent = index,
                            Index = j,
                            Source = this.Source
                        }
                    );
                return triggers;
            }
        }
        private class C_FlushTrigger : AC_HandTrigger {
            public C_FlushTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        2
                    )
                );
                for (int j = -1; j <= 1; j++)
                    if (index + j >= 0 && index + j < cards.Count)
                        triggers.Add(
                            new C_CardTrigger {
                                Parent = index,
                                Index = index + j,
                                Source = this.Source
                            }
                        );
                return triggers;
            }
        }
        private class C_FullHouseTrigger : AC_HandTrigger {
            public C_FullHouseTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                int count = cards[index].Rank switch {
                    E_Rank.Ace => 5,
                    E_Rank.Jack => 4,
                    E_Rank.Queen => 4,
                    E_Rank.King => 4,
                    _ => 3
                };
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        count
                    )
                );
                return triggers;
            }
        }
        private class C_FourOfAKindTrigger : AC_HandTrigger {
            public C_FourOfAKindTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        5 + index
                    )
                );
                return triggers;
            }
        }
        private class C_FiveOfAKindTrigger : AC_HandTrigger {
            public C_FiveOfAKindTrigger(E_Hand hand) : base(hand) { }

            public override IEnumerable<C_CardTrigger> GetTriggers(List<C_Card> cards, int index) {
                List<C_CardTrigger> triggers = new();
                triggers.AddRange(
                    Enumerable.Repeat(
                        new C_CardTrigger {
                            Parent = index,
                            Index = index,
                            Source = this.Source
                        },
                        6 + index
                    )
                );
                return triggers;
            }
        }
        #endregion

        private static readonly Dictionary<E_Hand, List<AC_Trigger>> TRIGGERS = new() {
            {
                E_Hand.HighCard, new List<AC_Trigger> {
                    new C_HighCardTrigger(E_Hand.HighCard)
                }
            }, {
                E_Hand.Pair, new List<AC_Trigger> {
                    new C_PairTrigger(E_Hand.Pair)
                }
            }, {
                E_Hand.TwoPairs, new List<AC_Trigger> {
                    new C_TwoPairsTrigger(E_Hand.TwoPairs)
                }
            }, {
                E_Hand.ThreeOfAKind, new List<AC_Trigger> {
                    new C_ThreeOfAKindTrigger(E_Hand.ThreeOfAKind)
                }
            }, {
                E_Hand.Straight, new List<AC_Trigger> {
                    new C_StraightTrigger(E_Hand.Straight)
                }
            }, {
                E_Hand.Flush, new List<AC_Trigger> {
                    new C_FlushTrigger(E_Hand.Flush)
                }
            }, {
                E_Hand.FullHouse, new List<AC_Trigger> {
                    new C_FullHouseTrigger(E_Hand.FullHouse)
                }
            }, {
                E_Hand.FourOfAKind, new List<AC_Trigger> {
                    new C_FourOfAKindTrigger(E_Hand.FourOfAKind)
                }
            }, {
                E_Hand.StraightFlush, new List<AC_Trigger> {
                    new C_StraightTrigger(E_Hand.StraightFlush), new C_FlushTrigger(E_Hand.StraightFlush)
                }
            }, {
                E_Hand.RoyalFlush, new List<AC_Trigger> {
                    new C_StraightTrigger(E_Hand.RoyalFlush), new C_FlushTrigger(E_Hand.RoyalFlush)
                }
            }, {
                E_Hand.FiveOfAKind, new List<AC_Trigger> {
                    new C_FiveOfAKindTrigger(E_Hand.FiveOfAKind)
                }
            }, {
                E_Hand.FlushFullHouse, new List<AC_Trigger> {
                    new C_FullHouseTrigger(E_Hand.FlushFullHouse), new C_FlushTrigger(E_Hand.FlushFullHouse)
                }
            }, {
                E_Hand.FlushFiveOfAKind, new List<AC_Trigger> {
                    new C_FiveOfAKindTrigger(E_Hand.FlushFiveOfAKind), new C_FlushTrigger(E_Hand.FlushFiveOfAKind)
                }
            }
        };

        public static List<C_CardTrigger> Compute(E_Hand hand, List<C_Card> cards, C_ExtraTriggers extraTriggers) {
            List<C_CardTrigger> triggers = new();

            for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++) {
                int extraTriggerCount = extraTriggers.GetRank(cards[cardIndex].Rank)
                                        + extraTriggers.GetSuit(cards[cardIndex].Suit)
                                        + extraTriggers.GetIndex(cardIndex)
                                        + extraTriggers.GetCard(cards[cardIndex].Rank, cards[cardIndex].Suit);
                for (int triggerCount = 0; triggerCount < 1 + extraTriggerCount; triggerCount++) {
                    foreach (AC_Trigger trigger in TRIGGERS[hand]) {
                        triggers.AddRange(trigger.GetTriggers(cards, cardIndex));
                    }

                    foreach (AC_Trigger trigger in extraTriggers.GetTriggers()) {
                        triggers.AddRange(trigger.GetTriggers(cards, cardIndex));
                    }
                }
            }

            return triggers;
        }
    }
}
