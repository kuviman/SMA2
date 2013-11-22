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

		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (key == Key.Space)
				world.Add(new Asteroid());
		}


		static SystemFont font = new SystemFont("Courier New", 32, FontStyle.Bold);
		public override void Render() {
			base.Render();
			Draw.Clear(Color.Black);
			world.Render();

			Draw.Save();
			new Camera(10).Apply();
			Draw.Translate(-5, -5);
			var text = string.Format("HEALTH : {0:000}", (int)world.player.Health);
			Draw.Rect(0, 0, font.Measure(text), 1, new Color(1, 1, 1, 0.5));
			Draw.Color(Color.Red);
			font.Render(text);
			Draw.Load();
		}

		public override void MouseDown(MouseButton button, Vec2 pos) {
			base.MouseDown(button, pos);
			world.player.Shoot(world.cam.FromWH(pos, App.Width, App.Height));
		}

	}

	class Program {
		static void Main(string[] args) {
			App.Title = "SMA2";
			App.Run(new Test());
		}
	}

}