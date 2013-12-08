using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class SpaceObject {

		public Vec2 Position = Vec2.Zero;
		public Vec2 Velocity = Vec2.Zero;

		void UpdateMovement(double dt) {
			Position += Velocity * dt;
		}

	}

}