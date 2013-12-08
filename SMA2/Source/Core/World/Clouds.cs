using System;
using System.Linq;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		void UpdateClouds(double dt) {
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

			const double CLOUDSPEED = 2;
			foreach (var cloud in objects.Where(o => o is Cloud)) {
				const double frict = 1;
				cloud.Velocity -= cloud.Velocity * frict * dt;
				cloud.Velocity = Vec2.Clamp(cloud.Velocity, CLOUDSPEED);
			}
		}

	}

}