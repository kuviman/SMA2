using System;
using System.Linq;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		public static World Current = null;

		public World() {
			Add(player);
			for (int i = 0; i < 1000; i++) {
				Add(new Cloud());
			}
		}

		const double camSpeed = 5;

		double dtK = 1;
		const double dtkspeed = 10;
		const double mindtk = 0.05;
		double rSpeed = 0;

		public double curTime = 0;

		PosGroup<SpaceObject> posGroup;

		public void Update(double dt) {
			Current = this;
			objects.Refresh();
			if (!player.Alive) {
				rSpeed = Math.Min(rSpeed + 0.01 * dt, 0.1);
				cam.Rotation += rSpeed * dt;
			}

			dt *= dtK;
			if (!player.Alive) {
				dtK = Math.Max(dtK - dtkspeed * dt, mindtk);
			}

			curTime += dt;

			cam.Position += (player.Position - cam.Position) * Math.Min(dt * camSpeed, 1);

			const double dist = 25;
			posGroup = new PosGroup<SpaceObject>(
				player.Position.X - dist, player.Position.Y - dist,
				player.Position.X + dist, player.Position.Y + dist);
			foreach (var o in objects) {
				posGroup.Add(o, o.Position);
			}
			objects.Update(dt);

			UpdateAsteroids(dt);
			UpdateBullets(dt);
			UpdateClouds(dt);
			UpdateExplosions(dt);
			UpdatePhysics(dt);

			foreach (var o in objects.Where(a =>
				(!(a is Cloud) && (a.Position - player.Position).Length > AsteroidDespawnDistance) || !a.Alive))
				objects.Remove(o);
		}

		public int Score = 0;
	}

}