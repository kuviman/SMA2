using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Asteroid : SpaceObject {

		static Texture texture = new Texture("../Data/Asteroid.png");

		public double Rotation = GRandom.NextDouble(0, 2 * Math.PI);
		public double RotSpeed = GRandom.NextDouble(-Math.PI, Math.PI);

		const double InitDistance = 20;
		const double Speed = 4;

		const double minSize = 0.7, maxSize = 1.5;

		public Asteroid() : base(50) {
			Position = Vec2.Rotate(Vec2.OrtX, GRandom.NextDouble(0, 2 * Math.PI)) * InitDistance;
			const double spot = Math.PI / 6;
			Velocity = Vec2.Rotate(Vec2.OrtX, Math.PI + Position.Arg + GRandom.NextDouble(-spot, spot)) * Speed;
			Size = minSize + (maxSize - minSize) * Math.Pow(GRandom.NextDouble(), 10);
			Position += World.Current.player.Position;
			Velocity += World.Current.player.Velocity / 2;
		}

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(Rotation);
			Draw.Align(0.5, 0.5);
			double k = (1 - Health / MaxHealth) / 2;
			Draw.Color(1, 1 - k, 1 - k);
			texture.Render();
			Draw.Load();
		}

		public override void Update(double dt) {
			base.Update(dt);
			Rotation += RotSpeed * dt;
		}

	}

}