using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Lazer : SpaceObject {

		Vec2 p2;
		const double Speed = 50;

		public Lazer(Vec2 pos, Vec2 p2) {
			Health = new Health(0.2);
			Position = pos;
			this.p2 = p2;
			Collideable = false;
			Velocity = Direction * Speed;
		}

		double swapT = 0;
		const double swapTime = 0.05;

		public override void Update(double dt) {
			Health.Value -= dt;
			base.Update(dt);
			swapT -= dt;
			if (swapT < 0) {
				swapT = swapTime;
				Rebuild();
			}
		}

		const int segs = 20;
		const double seglen = 1;
		const double offset = 0.5;

		Vec2[] ps = new Vec2[segs];

		Vec2 Direction { get { return (p2 - Position).Unit; } }

		void Rebuild() {
			for (int i = 0; i < segs; i++) {
				ps[i] = Position + Direction * seglen * i;
				if (i > 0)
					ps[i] += Vec2.Rotate90(Direction * GRandom.NextDouble(-offset, offset));
			}
		}

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Color(1, 1, 1, Math.Pow(Health.Percentage, 0.5));
			Draw.Line(Position, p2, 0.1);
			for (int i = 0; i < segs - 1; i++) {
				Draw.Line(ps[i], ps[i + 1], 0.075, new Color(0.5, 0.5, 1, 0.7));
			}
			Draw.Load();
		}

	}

}