using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class SpaceObject : IRenderable, IUpdateable {

		public World World { get; set; }

		public double Size = 1;
		public Vec2 Position = Vec2.Zero;
		public Vec2 Velocity = Vec2.Zero;

		public virtual void Render() { }
		public virtual void Update(double dt) {
			Position += Velocity * dt;
		}
	}

}