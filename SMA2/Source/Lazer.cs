using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Lazer : SpaceObject {

		Vec2 p2;

		public Lazer(Vec2 pos, Vec2 p2) : base(0.2) {
			Position = pos;
			this.p2 = p2;
			Collideable = false;
		}

		public override void Update(double dt) {
			Health -= dt;
			base.Update(dt);
		}

		public override void Render() {
			base.Render();
			Draw.Line(Position, p2, 0.1);
		}

	}

}