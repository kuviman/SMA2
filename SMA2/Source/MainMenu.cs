using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class MainMenu : State {

		World world = new World();

		public override void Update(double dt) {
			base.Update(dt);
			world.Update(dt);
		}

		public override void Render() {
			var tex = new Texture(Draw.Width, Draw.Height);
			Draw.BeginTexture(tex);
			world.Render();
			Draw.EndTexture();
			Draw.Save();
			Draw.Scale(2);
			Draw.Align(0.5, 0.5);
			Draw.Color(0.5, 0.5, 0.5);
			tex.Render();
			Draw.Load();
			base.Render();

			Draw.Save();
			new Camera(10).Apply();
			Draw.Translate(-5, 0);
			font.Render("1 for new game");
			Draw.Translate(0, -1);
			font.Render("2 for exit");
			Draw.Load();
		}

		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (key == Key.Number2)
				Close();
			else if (key == Key.Number1)
				App.PushState(new Test());
		}

		static SystemFont font = new SystemFont("Courier New", 64, FontStyle.Bold);

	}

}