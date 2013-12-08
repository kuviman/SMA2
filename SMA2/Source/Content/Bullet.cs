using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Bullet : SpaceObject {

		Vec2 p2;
		const double Speed = 50;

		public Bullet(Vec2 pos, Vec2 p2) {
			Health = new Health(0.5);
			Position = pos;
			this.p2 = p2;
			Collideable = false;
			Velocity = Direction * Speed;
		}

		public override void Update(double dt) {
			Health.Value -= dt;
			base.Update(dt);
		}

		Vec2 Direction { get { return (p2 - Position).Unit; } }

		public override void Render() {
			base.Render();
			const double length = 0.5;
			const double width = 0.1;
			Draw.Line(Position, Position + Direction * length, width, Color.Red);
		}

	}

}