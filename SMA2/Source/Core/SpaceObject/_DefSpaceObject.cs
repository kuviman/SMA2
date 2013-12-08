using System;
using VitPro.Engine;

namespace VitPro.SMA2 {

	abstract partial class SpaceObject : IRenderable, IUpdateable {

		public SpaceObject() {
			OnUpdate += UpdateMovement;
		}

		public event Action<double> OnUpdate;
		public virtual void Update(double dt) {
			OnUpdate.Apply(dt);
		}

		public event Action OnRender;
		public virtual void Render() {
			OnRender.Apply();
		}

		public World World { get; internal set; }

		public double Size = 1;

	}

}