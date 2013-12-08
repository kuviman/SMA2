using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	class Cloud : SpaceObject {

		static Texture texture = new Texture("../Data/Cloud.png");

		public double Rotation = GRandom.NextDouble(0, 2 * Math.PI);
		public double RotSpeed = GRandom.NextDouble(-Math.PI, Math.PI) / 10;

		const double dist = 25;

		public Cloud() {
			Position = new Vec2(GRandom.NextDouble(-dist, dist), GRandom.NextDouble(-dist, dist));
			Size = 0.7;
			Collideable = false;
		}

		public override void Render() {
			if (World.Current == null || World.Current.player == null)
				return;
			if ((Position - World.Current.player.Position).Length > World.Current.cam.FOV * 2)
				return;
			base.Render();
			Draw.Save();
			Draw.Translate(Position);
			Draw.Scale(Size * 2);
			Draw.Rotate(Rotation);
			Draw.Align(0.5, 0.5);
			texture.Render();
			Draw.Load();
		}

		public override void Update(double dt) {
			base.Update(dt);
			Rotation += RotSpeed * dt;
			var p = World.Current.player.Position - new Vec2(dist, dist);
			Position = new Vec2(
				p.X + GMath.Mod(Position.X - p.X, 2 * dist),
				p.Y + GMath.Mod(Position.Y - p.Y, 2 * dist));
		}

	}

}