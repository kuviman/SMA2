using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class MachineGun : Weapon {
		public MachineGun() : base(0.1) { }
		protected override void DoShoot(Vec2 pos) {
			var dust = new Dust(Owner.Position + (pos - Owner.Position).Unit * Owner.Size * 1.5, 0.5);
			const double minspeed = 3, maxspeed = 5;
			dust.Velocity = (pos - Owner.Position).Unit * GRandom.NextDouble(minspeed, maxspeed);
			World.Current.Add(dust);
			World.Current.Add(new Bullet(dust.Position, pos));
		}
	}

}