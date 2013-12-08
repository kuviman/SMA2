using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class SpaceObject {

		double _health;
		public double Health { get { return _health; } set { _health = Math.Max(value, 0); } }

		public double MaxHealth { get; set; }

	}

}