using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class SpaceObject {

		public bool Collideable = true;
		public double Size = 1;
		public double Mass { get { return Math.PI * GMath.Sqr(Size); } }

	}

}