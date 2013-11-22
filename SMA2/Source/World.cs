﻿using System;
using System.Linq;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class World {

		public Camera cam = new Camera(20);
		static Texture back = new Texture("../Data/Back.png");
		public static Texture cloudMap = new Texture(1000, 500, true);

		public Player player = new Player();

		HashSet<SpaceObject> objects = new HashSet<SpaceObject>();

		public static World Current = null;

		public World() {
			Add(player);
			for (int i = 0; i < 100; i++) {
				Add(new Cloud());				
			}
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

		const double camSpeed = 5;
		public void Update(double dt) {
			cam.Position += (player.Position - cam.Position) * Math.Min(dt * camSpeed, 1);
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
			objects.RemoveWhere(a => !(a is Cloud) && (a.Position - player.Position).Length > AsteroidDespawnDistance);
			foreach (var o in new List<SpaceObject>(objects.Where(a => !a.Alive))) {
				if (!o.Collideable)
					continue;
				Add(new Explosion(o.Position, o.Size * 1.5));
			}
			objects.RemoveWhere(a => !a.Alive);
		}

		public void Render() {

			Draw.BeginTexture(cloudMap);
			Draw.Clear(1, 1, 1, 0);
			Draw.EndTexture();

			Draw.Save();
			cam.Apply();

			Draw.Save();
			Draw.Translate(cam.Position);
			Draw.Scale(20);
			double aspect = (double)back.Width / back.Height;
			Draw.Scale(aspect, 1);
			Draw.Align(0.5, 0.5);
			const double k = 50;
			back.SubTexture(cam.Position.X / aspect / k, cam.Position.Y / k, 1, 1).Render();
			Draw.Load();

			objects.Render();

			Draw.Load();

			Draw.Save();
			new Camera(1).Apply();
			Draw.Scale((double)cloudMap.Width / cloudMap.Height, 1);
			Draw.Align(0.5, 0.5);
			Draw.Color(1, 1, 1, 0.3);
			cloudMap.Render();
			Draw.Load();
		}


		internal void Add(SpaceObject o) {
			objects.Add(o);
		}
	}

}