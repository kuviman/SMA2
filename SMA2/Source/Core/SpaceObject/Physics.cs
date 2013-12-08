using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Physics {

		public double Size;
		public double Mass;

		public Physics(double size, double mass) {
			Size = size;
			Mass = mass;
		}

		public static Physics SolidSphere(double size, double density) {
			return new Physics(size, (4.0 / 3.0) * Math.PI * Math.Pow(size, 3));
		}

	}

	partial class SpaceObject {

		public Physics Physics = null;

	}

}