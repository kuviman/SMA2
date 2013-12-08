using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	abstract class Weapon : IUpdateable {

		public Player Owner;

		public double RemainingReloadTime = 0;
		public double ReloadTime;

		public Weapon(double reloadTime) {
			this.ReloadTime = reloadTime;
		}

		public virtual void Update(double dt) {
			RemainingReloadTime -= dt;
		}

		public void Shoot(Vec2 pos) {
			if (RemainingReloadTime > 0)
				return;
			RemainingReloadTime = ReloadTime;
			DoShoot(pos);
		}

		protected abstract void DoShoot(Vec2 pos);

	}

}