using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Health {

		public double MaxValue;

		public event Action OnEmptied;

		double _val;
		public double Value {
			get { return _val; }
			set {
				bool hadHealth = _val > 0;
				_val = Math.Min(Math.Max(value, 0), MaxValue);
				if (hadHealth && _val == 0)
					OnEmptied.Apply();
			}
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