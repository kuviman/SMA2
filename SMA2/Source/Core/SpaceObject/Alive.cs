using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class SpaceObject {

		bool _alive = true;
		public bool Alive {
			get { return _alive; }
			private set { _alive = value; }
		}

		public void Kill() {
			Alive = false;
		}

	}

}