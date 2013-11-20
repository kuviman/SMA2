using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class SpaceObject : IRenderable, IUpdateable {

		public double Health { get; set; }
		public double MaxHealth { get; set; }

		public SpaceObject(double maxHealth) {
			Health = MaxHealth = maxHealth;
		}
		public SpaceObject() : this(100) { }

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