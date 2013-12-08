using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Player : SpaceObject {

		const double Speed = 5;
		const double Accel = 20;

		const double SwingAngle = Math.PI / 40;
		const double SwingSpeed = 10;
		const double MoveSwing = Math.PI / 10;

		double t = 0;

		public Player() {
			Health = new Health(100);
			Position = Vec2.Zero;
			Size = 1;
			Physics = Physics.SolidSphere(Size, 1);
		}

		public override void Update(double dt) {
			Weapon1.Owner = this;
			Weapon2.Owner = this;
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
			if (MouseButton.Left.Pressed())
				Weapon1.Shoot(World.Current.cam.FromWH(Mouse.Position, App.Width, App.Height));
			if (MouseButton.Right.Pressed())
				Weapon2.Shoot(World.Current.cam.FromWH(Mouse.Position, App.Width, App.Height));
			var targetVel = new Vec2(vx, vy).Unit * Speed;
			Velocity += Vec2.Clamp(targetVel - Velocity, Accel * dt);
			Weapon2.Update(dt);
			Weapon1.Update(dt);
		}

		public Weapon Weapon1 = new MachineGun();
		public Weapon Weapon2 = new LazerGun();
		Texture gun = new Texture("../Data/Gun.png");

		static Texture texture = new Texture("../Data/Player.png");

		public override void Render() {
			Weapon2.Owner = this;
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);

			Draw.Save();
			Draw.Scale(0.3);
			Draw.Scale(1, 2);
			Draw.Align(0.5, 0.7);
			body.Render();
			Draw.Load();

			Draw.Save();
			var mpos = World.Current.cam.FromWH(Mouse.Position, App.Width, App.Height);
			double ang = (mpos - Position).Arg;
			Draw.Translate(0, 0.2);
			Draw.Scale(0.5);
			if (mpos.X > Position.X) {
				Draw.Rotate(ang / 2);
				Draw.Align(0.5, 0.5);
				head.Render();
			} else {
				if (ang < 0)
					ang += 2 * Math.PI;
				Draw.Rotate(ang / 2 - Math.PI / 2);
				Draw.Scale(-1, 1);
				Draw.Align(0.5, 0.5);
				head.Render();
			}
			Draw.Load();

			Draw.Save();
			Draw.Rotate((mpos - Position).Arg);
			Draw.Scale(0.2);
			Draw.Align(-2.5, 0.5);
			gun.Render();
			var k = Math.Max(Weapon2.RemainingReloadTime / Weapon2.ReloadTime, 0);
			k = Math.Pow(k, 0.5) / 2;
			var color = new Color(0.8, 0.8, 1, k);
			Draw.Circle(0.25, 0.5, 0.75, color);
			Draw.Load();

			Draw.Rotate(Math.Sin(t) * SwingAngle - Velocity.X / Speed * MoveSwing);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

		Texture body = new Texture("../Data/Body.png");
		Texture head = new Texture("../Data/Head.png");
	}

}
