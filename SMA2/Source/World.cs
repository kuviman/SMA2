using System;
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
			for (int i = 0; i < 1000; i++) {
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
		const double AsteroidDespawnDistance = 25;
		const double minTime = 0.05;
		const double maxTime = 0.2;

		const double camSpeed = 5;

		double dtK = 1;
		const double dtkspeed = 10;
		const double mindtk = 0.05;
		public void Update(double dt) {

			dt *= dtK;
			if (!player.Alive) {
				dtK = Math.Max(dtK - dtkspeed * dt, mindtk);
			}

			const double dist = 25;
			PosGroup<SpaceObject> posGroup = new PosGroup<SpaceObject>(
				player.Position.X - dist, player.Position.Y - dist,
				player.Position.X + dist, player.Position.Y + dist);
			double maxSize = 0;
			foreach (var o in objects) {
				posGroup.Add(o, o.Position);
				maxSize = Math.Max(maxSize, o.Size);
			}

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
				double d2 = a.Size + maxSize;
				foreach (var b in posGroup.Query(a.Position - new Vec2(d2, d2), a.Position + new Vec2(d2, d2))) {
					if (!b.Collideable)
						continue;
					if (a == b)
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

			foreach (var a in objects) {
				if (a is Cloud) {
					double REPL = a.Size;
					double REP = 20;
					double GRAV = 2;
					double GRAVL = 2 * a.Size;
					double MINL = a.Size * 0.1;
					double d2 = Math.Max(GRAVL, REPL * 2);
					foreach (var b in posGroup.Query(a.Position - new Vec2(d2, d2), a.Position + new Vec2(d2, d2))) {
						if (!(b is Cloud))
							continue;
						if (a == b)
							continue;
						Vec2 dv = b.Position - a.Position;
						double d = dv.Length;
						if (d < MINL)
							continue;
						if (d < REPL * 2) {
							Vec2 f = (d - REPL) * REP * dv * Math.Pow(d, -3) * dt;
							a.Velocity += f;
							b.Velocity -= f;
						} else if (d < GRAVL) {
							Vec2 f = GRAV * dv * Math.Pow(d, -3) * dt;
							a.Velocity += f;
							b.Velocity -= f;
						}
					}

					foreach (var b in posGroup.Query(a.Position - new Vec2(d2, d2), a.Position + new Vec2(d2, d2))) {
						if (!(b is Asteroid))
							continue;
						Vec2 dv = b.Position - a.Position;
						double d = dv.SqrLength;
						const double AST = 2;
						const double ASTK = 100;
						if (d < AST * AST) {
							a.Velocity -= ASTK * dv / d;
						}
					}

					{
						Vec2 dv = player.Position - a.Position;
						double d = dv.SqrLength;
						const double PLAST = 2;
						const double PLK = 1000;
						if (d < PLAST * PLAST) {
							a.Velocity -= PLK * dv / d;
						}
					}
				}
			}

			const double CLOUDSPEED = 1;
			foreach (var cloud in objects.Where(o => o is Cloud))
				cloud.Velocity = Vec2.Clamp(cloud.Velocity, CLOUDSPEED);

			objects.RemoveWhere(a => !(a is Cloud) && (a.Position - player.Position).Length > AsteroidDespawnDistance);
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
			Draw.Translate(cam.Position);
			Draw.Scale(30);
			double aspect = (double)back.Width / back.Height;
			Draw.Scale(aspect, 1);
			Draw.Align(0.5, 0.5);
			const double k = 50;
			Draw.Color(0.5, 0.5, 0.5, 1);
			back.SubTexture(cam.Position.X / aspect / k, cam.Position.Y / k, 1, 1).Render();
			Draw.Load();

			foreach (var o in objects) {
				if (o is Cloud)
					continue;
				o.Render();
			}

			Draw.BeginTexture(cloudMap);
			cam.Apply();
			Draw.Clear(1, 1, 1, 0);
			foreach (var o in objects) {
				if (o is Cloud)
					o.Render();
			}
			Draw.EndTexture();

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