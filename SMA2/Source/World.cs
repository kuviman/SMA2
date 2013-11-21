using System;
using System.Linq;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class World {

		public Camera cam = new Camera(20);
		Texture back = new Texture("../Data/Back.png");

		public Player player = new Player();

		HashSet<SpaceObject> objects = new HashSet<SpaceObject>();

		public static World Current = null;

		public World() {
			Add(player);
		}

		public IEnumerable<Asteroid> asteroids {
			get {
				foreach (var o in objects) {
					var a = o as Asteroid;
					if (a != null)
						yield return a;
				}
			}
		}

		double timeTillNextAsteroid = 0;
		const double AsteroidDespawnDistance = 20;
		const double minTime = 0.1;
		const double maxTime = 1;

		public void Update(double dt) {
			Current = this;
			timeTillNextAsteroid -= dt;
			if (timeTillNextAsteroid < 0) {
				timeTillNextAsteroid = GRandom.NextDouble(minTime, maxTime);
				Add(new Asteroid());
			}
			objects.Update(dt);
			foreach (var a in objects) {
				if (!a.Collideable)
					continue;
				foreach (var b in objects) {
					if (!b.Collideable)
						continue;
					Vec2 dr = b.Position - a.Position;
					if (dr.Length > b.Size + a.Size)
						continue;
					double pen = b.Size + a.Size - dr.Length;
					dr = dr.Unit;
					double dv = (b.Velocity - a.Velocity) * dr;
					if (dv > 0)
						continue;

					const double DamageK = 5;
					double damage = (-dv) * DamageK;
					a.Health -= damage;
					b.Health -= damage;

					b.Position += dr * pen / 2;
					a.Position -= dr * pen / 2;
					const double E = 1;
					b.Velocity -= E * dv * dr;
					a.Velocity += E * dv * dr;
				}
			}
			objects.RemoveWhere(a => a.Position.Length > AsteroidDespawnDistance);
			foreach (var o in new List<SpaceObject>(objects.Where(a => !a.Alive))) {
				if (!o.Collideable)
					continue;
				Add(new Explosion(o.Position, o.Size * 1.5));
			}
			objects.RemoveWhere(a => !a.Alive);
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

			objects.Render();

			Draw.Load();
		}


		internal void Add(SpaceObject o) {
			objects.Add(o);
		}
	}

}