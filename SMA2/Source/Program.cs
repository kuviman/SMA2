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
#if !DEBUG
			App.Fullscreen = true;
#endif
			Mouse.Visible = false;
			App.Title = "SMA2";
			App.Run(new MyManager(new MainMenu()));
		}
	}

}