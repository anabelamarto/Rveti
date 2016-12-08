using UnityEngine;
using System.Collections;

namespace Utilities{
	public class UtilitiesScript : MonoBehaviour {

		public enum Direction{Down,Up,Right,Left};

		public static void DirectionToAxisSpeeds(Direction direction, ref float horizontalSpeed, ref float verticalSpeed){
			switch (direction) {
			case Direction.Down:
				horizontalSpeed = 0;
				verticalSpeed = -1;
				break;

			case Direction.Up:
				horizontalSpeed = 0;
				verticalSpeed = 1;
				break;

			case Direction.Right:
				horizontalSpeed = 1;
				verticalSpeed = 0;
				break;

			case Direction.Left:
				horizontalSpeed = -1;
				verticalSpeed = 0;
				break;
			}
		}

		public static Vector2 DirectionToAxisSpeeds(Direction direction){
			Vector2 vector = new Vector2(0,0);
			switch (direction) {
			case Direction.Down:
				vector = new Vector2(0, -1);
				break;

			case Direction.Up:
				vector = new Vector2(0, 1);
				break;

			case Direction.Right:
				vector = new Vector2(1, 0);
				break;

			case Direction.Left:
				vector = new Vector2(-1, 0);
				break;
			}
			return vector;
		}

		public static void AxisSpeedsToDirection(ref Direction direction, float horizontalSpeed, float verticalSpeed){
			if (horizontalSpeed > 0) {
				direction = Direction.Right;
			}

			if (horizontalSpeed < 0) {
				direction = Direction.Left;
			}

			if (verticalSpeed > 0) {
				direction = Direction.Up;
			}

			if (verticalSpeed < 0) {
				direction = Direction.Down;
			}
		}

	}
}
