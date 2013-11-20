using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Explosion : SpaceObject {

		public Explosion(Vec2 pos, double size)
			: base(0.3) {
				Position = pos;
				Size = size;
		}

		public override void Update(double dt) {
			Health -= dt;
			base.Update(dt);
		}

		static Texture tex = new Texture("../Data/Explosion.png");

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Align(0.5, 0.5);
			tex.Render();
			Draw.Load();
		}

	}

}