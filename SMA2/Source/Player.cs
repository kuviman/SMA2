using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Player : SpaceObject {

		const double Speed = 5;
		const double Accel = 20;

		const double SwingAngle = Math.PI / 40;
		const double SwingSpeed = 4;
		const double MoveSwing = Math.PI / 20;

		double t = 0;

		public Player() {
			Position = Vec2.Zero;
			Size = 1;
		}

		public override void Update(double dt) {
			base.Update(dt);
			t += SwingSpeed * dt;
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
			RemainingReloadTime -= dt;
		}

		static Texture texture = new Texture("../Data/Player.png");

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(Math.Sin(t) * SwingAngle + Velocity.X / Speed * MoveSwing);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

		public double RemainingReloadTime = 0;
		public double ReloadTime = 0.5;

		public void Shoot(Vec2 pos) {
			if (RemainingReloadTime > 0)
				return;
			pos = Position + (pos - Position).Unit * 100500;
			World.Current.Add(new Lazer(Position, pos));
			RemainingReloadTime = ReloadTime;
			const double damage = 100;
			foreach (var a in World.Current.asteroids) {
				if ((a.Position - this.Position) * (pos - Position) < 0)
					continue;
				if (Math.Abs((a.Position - this.Position) ^ (pos - Position).Unit) < a.Size)
					a.Health -= damage;
			}
		}
	}

}
