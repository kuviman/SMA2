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
			if (key == Key.F2)
				world = new World();
			if (key == Key.Escape)
				Close();
		}


		public static Font font = new Font("../Data/font.TTF", 32);
		public override void Render() {
			base.Render();
			Draw.Clear(Color.Black);
			world.Render();

			Draw.Save();
			new Camera(10).Apply();

			RenderScore();
			RenderHealth();

			Draw.Load();
            if (!world.player.Alive)
            {
                new Camera(12).Apply();
                Draw.Save();
                Draw.Scale(1);
                Draw.Translate(new Vec2(-5, 0));
                Draw.Align(0.5, 0.5);
                font.Render("YOUR PENIS FELL OFF");
                Draw.Load();
            }
		}

		void RenderScore() {
			Draw.Save();

			Draw.Translate(-4, 4);
			Draw.Scale(0.5);

			var text = string.Format("SCORE : {0}", world.Score);

			Draw.Rect(0, 0, font.Measure(text), 1, new Color(0, 0, 0, 0.5));
			font.Render(text);

			Draw.Load();
		}

		void RenderHealth() {
			Draw.Save();
			Draw.Translate(0, -4);
			Draw.Scale(0.5);
			const double w = 5;
			const double h = 0.6;
			const double width = 0.1;
			Draw.Rect(-w, -h, w, h, new Color(1, 1, 1, 0.3));
			Draw.Line(new Vec2(-w, -h), new Vec2(-w, h), width);
			Draw.Line(new Vec2(w, -h), new Vec2(w, h), width);
			Draw.Line(new Vec2(-w, -h), new Vec2(w, -h), width);
			Draw.Line(new Vec2(-w, h), new Vec2(w, h), width);

			const double cnt = 20;
			const double h2 = 0.4;
			const double width2 = 0.3;
			for (int i = 0; i < cnt; i++) {
				Draw.Save();
				Draw.Translate(-w + (i + 1) * (2 * w / (cnt + 1)), 0);
				if (world.player.Health < (i + 1) * world.player.MaxHealth / cnt)
					Draw.Color(1, 0, 0);
				else
					Draw.Color(0, 1, 0);
				Draw.Line(new Vec2(0, -h2), new Vec2(0, h2), width2);
				Draw.Load();
			}
			Draw.Load();
		}

	}

	class MyManager : StateManager {
		public MyManager(State a)
			: base(a) {
		}
		double t = 1;

		Texture cursor = new Texture("../Data/Cursor.png");

		public override void Update(double dt) {
			base.Update(dt);
			t -= 5 * dt;
			if (t < 0)
				t = 0;
		}

		public override void StateChanged() {
			base.StateChanged();
			t = 1;
			if (tex != null) {
				back = tex.Copy();
			}
		}

		Texture tex = null, back = null;

		public override void Render() {
			if (tex == null || tex.Width != Draw.Width || tex.Height != Draw.Height)
				tex = new Texture(Draw.Width, Draw.Height);

			Draw.BeginTexture(tex);

			base.Render();

			Draw.EndTexture();
			tex.RemoveAlpha();

			Draw.Save();
			Draw.Scale(2);
			Draw.Align(0.5, 0.5);

			tex.Render();
			if (back != null) {
				Draw.Color(1, 1, 1, t);
				back.Render();
			}

			Draw.Load();

			var cam = new Camera(20);
			Draw.Save();
			cam.Apply();
			Draw.Translate(cam.FromWH(Mouse.Position, Draw.Width, Draw.Height));
			Draw.Align(0.5, 0.5);
			cursor.Render();
			Draw.Load();
		}
	}

	class Program {
		static void Main(string[] args) {
			new Sound("../Data/music.wav").Play(volume: 0.3, looped: true);
#if !DEBUG
			App.Fullscreen = true;
#endif
			Mouse.Visible = false;
			App.Title = "SMA2";
			App.Run(new MyManager(new MainMenu()));
		}
	}

}