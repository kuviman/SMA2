using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Player : IRenderable, IUpdateable {

		public Vec2 Position = Vec2.Zero;
		public double Size = 1;

		const double Speed = 5;

		public void Update(double dt) {
			double vx = 0, vy = 0;
			if (Key.A.Pressed())
				vx -= 1;
			if (Key.D.Pressed())
				vx += 1;
			if (Key.W.Pressed())
				vy += 1;
			if (Key.S.Pressed())
				vy -= 1;
			Position += new Vec2(vx, vy).Unit * Speed * dt;
		}

		static Texture texture = new Texture("../Data/Player.png");

		public void Render() {
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

	}

}
