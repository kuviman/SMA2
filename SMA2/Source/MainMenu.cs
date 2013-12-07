using System;
using System.Collections.Generic;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class MainMenu : State {

		class Button : IRenderable, IUpdateable {

			string Text;
			double y;
			public Action Action;

			public Button(string text, double y) {
				this.Text = text;
				this.y = y;
			}

			public void Update(double dt) {
				dt *= 5;
				if (Selected)
					t = Math.Min(t + dt, 1);
				else
					t = Math.Max(t - dt, 0);
			}

			double t = 0;

			public void Render() {
				Draw.Save();
				Draw.Translate(0, y);
				Draw.Align(font.Measure(Text) / 2, 0);
				Draw.Save();
				Draw.Scale(font.Measure(Text), 1);
				var backCol = new Color(0.6, 0.6, 1, t / 1.5);
				Draw.Color(backCol);
				Draw.Quad();
				Draw.Load();
				Draw.Color(0, 0, 0);
				font.Render(Text);
				Draw.Load();
			}

			public bool Selected = false;

			public bool Inside(Vec2 pos) {
				double sx = font.Measure(Text);
				return Math.Abs(pos.X) < sx / 2 && pos.Y > y && pos.Y < y + 1;
			}

		}

		World world = new World();

		public MainMenu() {
			world.curTime = 100500;
			world.player.Health -= 100500;
			for (int i = 0; i < 300; i++) {
				world.Update(0.2);
			}
			var b = new Button("PLAY", 0);
			b.Action = () => StateManager.PushState(new Test());
			buttons.Add(b);
			b = new Button("exit", -3);
			b.Action = () => StateManager.Close();
			buttons.Add(b);
		}

		List<Button> buttons = new List<Button>();

		public override void MouseMove(Vec2 pos) {
			base.MouseMove(pos);
			pos = new Camera(10).FromWH(pos, App.Width, App.Height);
			foreach (var b in buttons)
				b.Selected = b.Inside(pos);
		}

		public override void MouseDown(MouseButton button, Vec2 pos) {
			base.MouseDown(button, pos);
			foreach (var b in buttons)
				if (b.Selected)
					b.Action.Apply();
		}

		public override void Update(double dt) {
			base.Update(dt);
			world.Update(dt);
			buttons.Update(dt);
		}

		public override void Render() {
			var tex = new Texture(Draw.Width, Draw.Height);
			Draw.BeginTexture(tex);
			world.Render();
			Draw.EndTexture();
			Draw.Save();
			Draw.Scale(2);
			Draw.Align(0.5, 0.5);
			//Draw.Color(0.5, 0.5, 0.5);
			tex.Render();
			Draw.Color(1, 1, 1, 0.5);
			Draw.Quad();
			Draw.Load();
			base.Render();

			Draw.Save();
			new Camera(10).Apply();
			buttons.Render();

			Draw.Translate(new Vec2(0, 2) + Vec2.Rotate(Vec2.OrtX, App.Time) / 10);
			Draw.Scale(8, 1.5);
			Draw.Align(0.5, 0);
			logo.Render();

			Draw.Load();
		}

		Texture logo = new Texture("../Data/Logo.png");

		public override void KeyDown(Key key) {
			base.KeyDown(key);
		}

		static Font font = new Font("../Data/font.TTF", 32);
		static MainMenu() {
			font.Smooth = false;
		}

	}

}