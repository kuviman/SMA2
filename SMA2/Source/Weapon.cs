using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Weapon : IUpdateable {

		public Player Owner;

		public double RemainingReloadTime = 0;
		public double ReloadTime = 0.5;

		public void Update(double dt) {
			RemainingReloadTime -= dt;
		}

		public void Shoot(Vec2 pos) {
			if (RemainingReloadTime > 0)
				return;
			pos = Owner.Position + (pos - Owner.Position).Unit * 100500;
			World.Current.Add(new Lazer(Owner.Position + (pos - Owner.Position).Unit * Owner.Size, pos));
			RemainingReloadTime = ReloadTime;
			const double damage = 100;
			foreach (var a in World.Current.asteroids) {
				if ((a.Position - Owner.Position) * (pos - Owner.Position) < 0)
					continue;
				if (Math.Abs((a.Position - Owner.Position) ^ (pos - Owner.Position).Unit) < a.Size) {
					a.Health -= damage;
					if (!a.Alive)
						World.Current.Score++;
				}
			}
		}

	}

}