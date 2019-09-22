using System;
using System.Collections.Generic;

namespace Landkreuzer {
	public enum StatisticType {
		Damage,
		Time,
		Shot,
		Hit,
		Spawn,
		Death
	}

	public interface IStatisticEntry<T> {
		string Caption { get; }
		T Value { get; set; }
		T AddValue(T value);
	}

	public class FloatEntry : IStatisticEntry<float>, IStatisticEntry<object> {
		public FloatEntry(string caption) {
			Caption = caption;
		}

		public string Caption { get; }

		public float AddValue(float value) {
			Value += value;
			return Value;
		}

		public float Value { get; set; }

		object IStatisticEntry<object>.Value {
			get => Value;
			set => Value = Convert.ToSingle(value);
		}

		public object AddValue(object value) {
			Value += Convert.ToSingle(value);
			return Value;
		}
	}

	public class IntEntry : IStatisticEntry<int>, IStatisticEntry<object> {
		public IntEntry(string caption) {
			Caption = caption;
		}

		public string Caption { get; }

		public int AddValue(int value) {
			Value += value;
			return Value;
		}

		public int Value { get; set; }

		object IStatisticEntry<object>.Value {
			get => Value;
			set => Value = Convert.ToInt32(value);
		}

		public object AddValue(object value) {
			Value += Convert.ToInt32(value);
			return Value;
		}
	}

	public static class Statistics {
		private static readonly Dictionary<StatisticType, IStatisticEntry<object>> _stats =
			new Dictionary<StatisticType, IStatisticEntry<object>> {
				{StatisticType.Damage, new FloatEntry("Damage dealt")},
				{StatisticType.Time, new FloatEntry("Time survived")},
				{StatisticType.Shot, new IntEntry("Shots made")},
				{StatisticType.Hit, new IntEntry("Bullets hit")},
				{StatisticType.Spawn, new IntEntry("Enemies spawned")},
				{StatisticType.Death, new IntEntry("Enemies died")}
			};

		public static IReadOnlyDictionary<StatisticType, IStatisticEntry<object>> Stats => _stats;

		public static void StatisticsEvent(StatisticType t, object value) {
			_stats[t].AddValue(value);
		}
	}
}