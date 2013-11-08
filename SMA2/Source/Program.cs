using System;
using VitPro;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Test : State {
		World world = new World();

		public override void Update(double dt) {
			base.Update(dt);
			world.Update(dt);
		}

		public override void Render() {
			base.Render();
			world.Render();
		}

	}

	class Program {
		static void Main(string[] args) {
			App.Title = "SMA2";
			App.Run(new Test());
		}
	}

}