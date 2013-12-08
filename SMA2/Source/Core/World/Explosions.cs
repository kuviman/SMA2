using System;
using System.Linq;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		static Sound expSound = new Sound("../Data/explosion.wav");

		void UpdateExplosions(double dt) {
			foreach (var o in objects.Where(a => !a.Alive)) {
				if (o.Physics == null)
					continue;
				Add(new Explosion(o.Position, o.Size * 1.5));
				if (player.Alive) {
					expSound.Play(1 - Math.Pow((o.Position - player.Position).Length / AsteroidDespawnDistance, 0.5));
				}
			}
		}

	}

}