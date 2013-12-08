using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Health {

		public double MaxValue;

		double _val;
		public double Value {
			get { return _val; }
			set { _val = Math.Min(Math.Max(value, 0), MaxValue); }
		}

		public double Percentage {
			get { return Value / MaxValue; }
		}

		public Health(double max) {
			Value = MaxValue = max;
		}

	}

	partial class SpaceObject {

		public Health Health = null;

	}

}