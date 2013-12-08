using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		void UpdatePhysics(double dt) {
			const double maxSize = 2;
			foreach (var a in objects) {
				if (a.Physics == null)
					continue;
				double d2 = a.Size + maxSize;
				foreach (var b in posGroup.Query(a.Position - new Vec2(d2, d2), a.Position + new Vec2(d2, d2))) {
					if (b.Physics == null)
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

					const double K = 5;
					Add(new Dust(a.Position + dr * a.Size, Math.Min(a.Size, b.Size) * (-dv) / K));

					const double DamageK = 5;
					double damage = 2 * (-dv) * DamageK / (a.Physics.Mass + b.Physics.Mass);
					a.Health.Value -= damage * b.Physics.Mass;
					b.Health.Value -= damage * a.Physics.Mass;

					b.Position += dr * pen / 2;
					a.Position -= dr * pen / 2;
					double E = 2 / (a.Physics.Mass + b.Physics.Mass);
					b.Velocity -= E * a.Physics.Mass * dv * dr;
					a.Velocity += E * b.Physics.Mass * dv * dr;
				}
			}
		}

	}

}