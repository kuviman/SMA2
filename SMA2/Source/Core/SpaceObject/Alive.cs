using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class SpaceObject {

		public bool Alive {
			get {
				if (Health == null)
					return true;
				return Health.Value > 0;
			}
		}

	}

}