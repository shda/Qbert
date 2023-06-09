using UnityEngine;

namespace Assets.Qbert.Scripts.Utils {
	public abstract class TouchData {
		public abstract int id { get; }
		public abstract Vector2 position { get; }
		public abstract GameObject capturedObject { get; }
	}
}