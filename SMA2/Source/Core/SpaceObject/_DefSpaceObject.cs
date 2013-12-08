using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	abstract partial class SpaceObject : IRenderable, IUpdateable {

		public SpaceObject() { }

		public virtual void Update(double dt) {
			UpdateMovement(dt);
		}

		public virtual void Render() { }

		public World World { get; internal set; }

		public Vec2 Position = Vec2.Zero;

	}

}