using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Dust : SpaceObject {

		const double initk = 0.5;
		public Dust(Vec2 pos, double size) {
			Health = MaxHealth = 0.2;
			Position = pos;
			Size = size * initk;
			GrowSpeed *= size;
			Collideable = false;
			for (int i = 0; i < 5; i++) {
				particles.Add(Vec2.Rotate(Vec2.OrtX * GRandom.NextDouble(), GRandom.NextDouble(0, 2 * Math.PI)));
				rots.Add(GRandom.NextDouble(0, 2 * Math.PI));
			}
		}

		double Rotation;

		double GrowSpeed = 5;
		const double RotSpeed = 2;

		public override void Update(double dt) {
			Health -= dt;
			base.Update(dt);
			Size += GrowSpeed * dt;
			Rotation += RotSpeed * dt;
		}

		static Texture tex = new Texture("../Data/Explosion.png");

		List<Vec2> particles = new List<Vec2>();
		List<double> rots = new List<double>();

		public override void Render() {
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size);
			Draw.Rotate(Rotation);
			Draw.Color(0.5, 0.5, 0.5, Math.Pow(Health / MaxHealth, 0.5) * 0.7);
			for (int i = 0; i < particles.Count; i++) {
				Draw.Save();
				Draw.Translate(particles[i]);
				Draw.Rotate(rots[i]);
				Draw.Scale(0.5);
				Draw.Align(0.5, 0.5);
				Draw.Quad();
				Draw.Load();
			}
			Draw.Load();
		}

	}

}