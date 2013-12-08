using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		public Player player = new Player();
		Group<SpaceObject> objects = new Group<SpaceObject>();

		public IEnumerable<Asteroid> asteroids {
			get {
				foreach (var o in objects) {
					var a = o as Asteroid;
					if (a != null)
						yield return a;
				}
			}
		}

		public void Add(SpaceObject o) {
			objects.Add(o);
		}

	}

}