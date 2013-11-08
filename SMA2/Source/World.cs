using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class World {

		Camera cam = new Camera(20);
		Texture back = new Texture("../Data/Back.png");

		public Player player = new Player();

		public void Update(double dt) {
			player.Update(dt);
		}

		public void Render() {
			Draw.Save();
			cam.Apply();

			Draw.Save();
			Draw.Scale(20);
			Draw.Scale((double)back.Width / back.Height, 1);
			Draw.Align(0.5, 0.5);
			back.Render();
			Draw.Load();

			player.Render();

			Draw.Load();
		}

	}

}