using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class MachineGun : Weapon {
		public MachineGun() : base(0.1) { }
		protected override void DoShoot(Vec2 pos) {
			const double acc = 0.1;
			var dir = Vec2.Rotate((pos - Owner.Position).Unit, GRandom.NextDouble(-acc, acc));
			pos = Owner.Position + dir * 100500;
			var dust = new Dust(Owner.Position + dir * Owner.Size * 1.5, 0.5);
			const double minspeed = 3, maxspeed = 5;
			dust.Velocity = (pos - Owner.Position).Unit * GRandom.NextDouble(minspeed, maxspeed);
			World.Current.Add(dust);
			World.Current.Add(new Bullet(dust.Position, pos));
		}
	}

}