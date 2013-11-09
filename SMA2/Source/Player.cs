﻿using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Player : SpaceObject {

		const double Speed = 5;
		const double Accel = 20;

		public Player() {
			Position = Vec2.Zero;
			Size = 1;
		}

		public override void Update(double dt) {
			base.Update(dt);
			double vx = 0, vy = 0;
			if (Key.A.Pressed())
				vx -= 1;
			if (Key.D.Pressed())
				vx += 1;
			if (Key.W.Pressed())
				vy += 1;
			if (Key.S.Pressed())
				vy -= 1;
			var targetVel = new Vec2(vx, vy).Unit * Speed;
			Velocity += Vec2.Clamp(targetVel - Velocity, Accel * dt);
		}

		static Texture texture = new Texture("../Data/Player.png");

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

	}

}
