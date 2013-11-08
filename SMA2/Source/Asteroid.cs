using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Asteroid : IRenderable, IUpdateable {

		static Texture texture = new Texture("../Data/Asteroid.png");

		public Vec2 Position;
		public Vec2 Velocity;
		public double Size = 0.7;

		const double InitDistance = 5;
		const double Speed = 4;

		public Asteroid() {
			Position = Vec2.Rotate(Vec2.OrtX, GRandom.NextDouble(0, 2 * Math.PI)) * InitDistance;
			Velocity = Vec2.Rotate(Vec2.OrtX, GRandom.NextDouble(0, 2 * Math.PI)) * Speed;
		}

		public void Render() {
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

		public void Update(double dt) {
			Position += Velocity * dt;
		}

	}

}