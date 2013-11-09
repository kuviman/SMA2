using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Asteroid : SpaceObject {

		static Texture texture = new Texture("../Data/Asteroid.png");

		public Vec2 Velocity;

		public double Rotation = GRandom.NextDouble(0, 2 * Math.PI);
		public double RotSpeed = GRandom.NextDouble(-Math.PI, Math.PI);

		const double InitDistance = 15;
		const double Speed = 4;

		public Asteroid() {
			Position = Vec2.Rotate(Vec2.OrtX, GRandom.NextDouble(0, 2 * Math.PI)) * InitDistance;
			const double spot = Math.PI / 6;
			Velocity = Vec2.Rotate(Vec2.OrtX, Math.PI + Position.Arg + GRandom.NextDouble(-spot, spot)) * Speed;
			Size = 0.7;
		}

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(Rotation);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

		public override void Update(double dt) {
			base.Update(dt);
			Position += Velocity * dt;
			Rotation += RotSpeed * dt;
		}

	}

}