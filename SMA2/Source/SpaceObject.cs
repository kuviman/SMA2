using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class SpaceObject : IRenderable, IUpdateable {

		public World World { get; set; }

		public double Size;
		public Vec2 Position;

		public virtual void Render() { }
		public virtual void Update(double dt) { }
	}

}