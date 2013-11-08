using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Player : IRenderable, IUpdateable {

		public Vec2 Position = Vec2.Zero;
		public double Size = 1;

		public void Update(double dt) {
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
