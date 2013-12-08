using System;
using System.Linq;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		void UpdateBullets(double dt) {

			const double maxSize = 2;
			foreach (var bb in objects) {
				var b = bb as Bullet;
				if (b == null)
					continue;
				double d2 = maxSize;
				foreach (var a in posGroup.Query(b.Position - new Vec2(d2, d2), b.Position + new Vec2(d2, d2))
					.Where(o => o.Physics != null)) {
					if (a is Player)
						continue;
					if ((a.Position - b.Position).SqrLength > GMath.Sqr(a.Size))
						continue;
					const double speedK = 0.05;
					const double damage = 10;
					a.Velocity += speedK * b.Velocity / a.Physics.Mass;
					a.Health.Value -= damage;
					b.Health.Value -= 100500;
					if (!a.Alive) {
						World.Current.Score++;
						World.Current.Add(new ScoreEff(a.Position, 0.5));
					}
				}
			}
		}

	}

}