using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class LazerGun : Weapon {
		public LazerGun() : base(2.5) { }

		static Sound snd = new Sound("../Data/lazer.wav");

		protected override void DoShoot(Vec2 pos) {
			snd.Play();
			pos = Owner.Position + (pos - Owner.Position).Unit * 100500;
			World.Current.Add(new Lazer(Owner.Position + (pos - Owner.Position).Unit * Owner.Size, pos));
			const double damage = 100;
			foreach (var a in World.Current.asteroids) {
				if ((a.Position - Owner.Position) * (pos - Owner.Position) < 0)
					continue;
				if (Math.Abs((a.Position - Owner.Position) ^ (pos - Owner.Position).Unit) < a.Size) {
					a.Health.Value -= damage;
					if (!a.Alive) {
						World.Current.Score++;
						World.Current.Add(new ScoreEff(a.Position, 0.5));
					}
				}
			}
		}
	}

}