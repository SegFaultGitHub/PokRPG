using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Utils {
    public static class SC_Utils {
        public static bool Rate(float rate) {
            return Random.Range(0, 1f) < rate;
        }

        public static (int, bool) RatePlus(float rate) {
            int result = Mathf.FloorToInt(rate);
            bool b = Rate(rate - result);
            return (result + (b ? 1 : 0), b);
        }

        public static float RatioFrom(float from, float to, float current) {
            return Mathf.Clamp(from < to ? (current - from) / (to - from) : 1 - (current - to) / (from - to), 0f, 1f);
        }

        public static float GetMidPoint(float origin, float end, float ratio) {
            return origin < end ? (end - origin) * ratio + origin : (origin - end) * (1 - ratio) + end;
        }

        public static float MapFrom(float fromMin, float fromMax, float toMin, float toMax, float current) {
            if (fromMin == fromMax) return toMin;
            float ratio = RatioFrom(fromMin, fromMax, current);
            Debug.Log(ratio);
            return GetMidPoint(toMin, toMax, ratio);
        }

        // Return a sin version of the ratio from 0 to 1
        public static float SinRatio(float x) {
            x = Mathf.Clamp(x, 0, 1);
            return .5f * (Mathf.Cos(x * Mathf.PI + Mathf.PI) + 1);
        }

        #region Simple sample
        public static T Sample<T>(List<T> list) {
            if (list.Count != 0) return list[Random.Range(0, list.Count)];
            Debug.LogError("Trying to sample an empty list");
            return default;
        }

        public static T Sample<T>(ICollection<T> list) {
            if (list.Count == 0) {
                Debug.LogError("Trying to sample an empty list");
                return default;
            }

            List<T> clone = new();
            clone.AddRange(list);
            return clone[Random.Range(0, list.Count)];
        }

        public static List<T> Shuffle<T>(ICollection<T> list) {
            return Sample(list, list.Count);
        }

        public static List<T> Sample<T>(IEnumerable<T> list, int n) {
            List<T> clone = new();
            clone.AddRange(list);

            List<T> result = new();
            while (result.Count < n && clone.Count > 0) {
                T element = Sample(clone);
                clone.Remove(element);
                result.Add(element);
            }

            return result;
        }
        #endregion

        #region Weighted sample
        private static C_WeightedObject<T> GetRandomItemInDistribution<T>(List<C_WeightedObject<T>> list) {
            float total = list.Sum(weightDistribution => weightDistribution.Weight);
            float choice = Random.Range(0, total);
            float index = 0;
            foreach (C_WeightedObject<T> weightDistribution in list) {
                index += weightDistribution.Weight;
                if (choice <= index) {
                    return weightDistribution;
                }
            }

            return list[^1];
        }

        public static C_WeightedObject<T> Sample<T>(List<C_WeightedObject<T>> list) {
            return GetRandomItemInDistribution(list);
        }

        public static List<C_WeightedObject<T>> Sample<T>(List<C_WeightedObject<T>> list, int n) {
            if (list.Count == 0) {
                Debug.LogError("Trying to sample an empty list");
                return default;
            }

            List<C_WeightedObject<T>> clone = new();
            clone.AddRange(list);

            List<C_WeightedObject<T>> result = new();
            while (result.Count < n && clone.Count > 0) {
                C_WeightedObject<T> element = GetRandomItemInDistribution(clone);
                clone.Remove(element);
                result.Add(element);
            }

            return result;
        }

        public static C_WeightedObject<T> Sample<T>(List<C_WeightedObject<T>> list, List<T> allowedValues) {
            if (list.Count == 0 || allowedValues.Count == 0) {
                Debug.LogError("Trying to sample an empty list");
                return default;
            }

            List<C_WeightedObject<T>> clone = new();
            clone.AddRange(list.Where(item => allowedValues.Contains(item.Obj)));

            return Sample(clone);
        }

        public static List<C_WeightedObject<T>> Sample<T>(List<C_WeightedObject<T>> list, List<T> allowedValues, int n) {
            if (list.Count == 0 || allowedValues.Count == 0) {
                Debug.LogError("Trying to sample an empty list");
                return default;
            }

            List<C_WeightedObject<T>> clone = new();
            clone.AddRange(list.Where(item => allowedValues.Contains(item.Obj)));

            return Sample(clone, n);
        }
        #endregion

        public static string FormatNumber(float value, int decimals, bool prefix = true) {
            return
                $"{(prefix && value >= 0 ? "+" : "")}{Math.Round(value, decimals).ToString(CultureInfo.InvariantCulture)}";
        }

        public static Vector3 LerpAngle(Vector3 a, Vector3 b, float t) {
            float x = Mathf.LerpAngle(a.x, b.x, t);
            float y = Mathf.LerpAngle(a.y, b.y, t);
            float z = Mathf.LerpAngle(a.z, b.z, t);
            return new Vector3(x, y, z);
        }
    }
}
