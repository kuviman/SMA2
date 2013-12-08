using System;
using System.Linq;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		const int AsteroidsCap = 100;
		double timeTillNextAsteroid = 0;
		const double AsteroidDespawnDistance = 25;
		double minTime { get { return 1 / (curTime + 1); } }
		double maxTime { get { return minTime * 2; } }

		void UpdateAsteroids(double dt) {
			timeTillNextAsteroid -= dt;
			if (timeTillNextAsteroid < 0 && objects.Count(o => o is Asteroid) < AsteroidsCap) {
				timeTillNextAsteroid = GRandom.NextDouble(minTime, maxTime);
				Add(new Asteroid());
			}

			foreach (var o in objects.Where(a =>
				!(a is Asteroid) && (a.Position - player.Position).Length > AsteroidDespawnDistance))
				objects.Remove(o);
		}

	}

}