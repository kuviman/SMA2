using System;
using System.Linq;
using VitPro.Engine;

namespace VitPro.SMA2 {

	partial class World {

		public Camera cam = new Camera(20);

		static Texture back = new Texture("../Data/Back.png");
		public static Texture cloudMap = new Texture(1000, 500, true);

		public void Render() {

			Draw.Save();
			cam.Apply();

			Draw.Save();
			Draw.Translate(cam.Position);
			Draw.Scale(30);
			double aspect = (double)back.Width / back.Height;
			Draw.Scale(2);
			Draw.Scale(aspect, 1);
			Draw.Align(0.5, 0.5);
			const double k = 50;
			Draw.Color(0.5, 0.5, 0.5, 1);
			back.SubTexture(cam.Position.X / aspect / k, cam.Position.Y / k, 2, 2).Render();
			Draw.Load();

			foreach (var o in objects) {
				if (o is Cloud)
					continue;
				o.Render();
			}

			Draw.BeginTexture(cloudMap);
			cam.Apply();
			Draw.Clear(1, 1, 1, 0);
			foreach (var o in objects) {
				if (o is Cloud)
					o.Render();
			}
			Draw.EndTexture();

			Draw.Load();

			Draw.Save();
			new Camera(1).Apply();
			Draw.Scale((double)cloudMap.Width / cloudMap.Height, 1);
			Draw.Align(0.5, 0.5);
			Draw.Color(1, 1, 1, 0.3);
			cloudMap.Render();
			Draw.Load();
		}

	}

}