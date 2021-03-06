﻿using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class ScoreEff : SpaceObject {

		public ScoreEff(Vec2 pos, double size) {
			Health = new Health(1);
			Position = pos;
			Size = size;
		}

		const double GrowSpeed = 0.5;

		public override void Update(double dt) {
			Health.Value -= dt;
			base.Update(dt);
			Size *= (1 + GrowSpeed * dt);
		}

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(World.Current.cam.Rotation);
			Draw.Align(0.5, 0.5);
			Draw.Color(1, 1, 0, Math.Pow(Health.Percentage, 0.5));
			Test.font.Render("+1");
			Draw.Load();
		}

	}

}