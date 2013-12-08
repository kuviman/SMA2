using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Explosion : SpaceObject {

		public Explosion(Vec2 pos, double size) {
			Health = new Health(0.15);
			Position = pos;
			Size = size;
			Collideable = false;
		}

		double swapT = 0;
		double Rotation;

		const double GrowSpeed = 2;
		const double SwapTime = 0.05;

		public override void Update(double dt) {
			Health.Value -= dt;
			base.Update(dt);
			Size *= (1 + GrowSpeed * dt);
			swapT -= dt;
			if (swapT < 0) {
				Rotation = GRandom.NextDouble(0, 2 * Math.PI);
				swapT = SwapTime;
			}
		}

		static Texture tex = new Texture("../Data/Explosion.png");

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(Rotation);
			Draw.Align(0.5, 0.5);
			Draw.Color(1, 1, 1, Health.Percentage);
			tex.Render();
			Draw.Load();
		}

	}

}