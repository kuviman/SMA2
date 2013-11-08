using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class World {

		Camera cam = new Camera(20);
		Texture back = new Texture("../Data/Back.png");

		public Player player = new Player();
		public HashSet<Asteroid> asteroids = new HashSet<Asteroid>();

		double timeTillNextAsteroid = 0;
		const double AsteroidDespawnDistance = 20;
		const double minTime = 0.1;
		const double maxTime = 1;

		public void Update(double dt) {
			player.Update(dt);
			timeTillNextAsteroid -= dt;
			if (timeTillNextAsteroid < 0) {
				timeTillNextAsteroid = GRandom.NextDouble(minTime, maxTime);
				asteroids.Add(new Asteroid());
			}
			asteroids.Update(dt);
			foreach(var a in asteroids)
				foreach (var b in asteroids) {
					Vec2 dr = b.Position - a.Position;
					if (dr.Length > b.Size + a.Size)
						continue;
					double pen = b.Size + a.Size - dr.Length;
					dr = dr.Unit;
					double dv = (b.Velocity - a.Velocity) * dr;
					if (dv > 0)
						continue;
					b.Position += dr * pen / 2;
					a.Position -= dr * pen / 2;
					const double E = 1;
					b.Velocity -= E * dv * dr;
					a.Velocity += E * dv * dr;
				}
			asteroids.RemoveWhere(a => a.Position.Length > AsteroidDespawnDistance);
		}

		public void Render() {
			Draw.Save();
			cam.Apply();

			Draw.Save();
			Draw.Scale(20);
			Draw.Scale((double)back.Width / back.Height, 1);
			Draw.Align(0.5, 0.5);
			back.Render();
			Draw.Load();

			player.Render();
			asteroids.Render();

			Draw.Load();
		}

	}

}