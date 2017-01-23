using UnityEngine;
using System.Collections;

namespace Utilities{
	public class UtilitiesScript : MonoBehaviour {

		public enum Direction{South,North,East,West,NorthEast,NorthWest,SouthEast,SouthWest, NoDirection};

		public static void DirectionToAxisSpeeds(Direction direction, ref float horizontalSpeed, ref float verticalSpeed){
			switch (direction) {
			case Direction.South:
				horizontalSpeed = 0;
				verticalSpeed = -1;
				break;

			case Direction.North:
				horizontalSpeed = 0;
				verticalSpeed = 1;
				break;

			case Direction.East:
				horizontalSpeed = 1;
				verticalSpeed = 0;
				break;

			case Direction.West:
				horizontalSpeed = -1;
				verticalSpeed = 0;
				break;

			case Direction.NorthEast:
				horizontalSpeed = 1;
				verticalSpeed = 1;
				break;

			case Direction.NorthWest:
				horizontalSpeed = -1;
				verticalSpeed = 1;
				break;

			case Direction.SouthEast:
				horizontalSpeed = 1;
				verticalSpeed = -1;
				break;

			case Direction.SouthWest:
				horizontalSpeed = -1;
				verticalSpeed = -1;
				break;

			case Direction.NoDirection:
				horizontalSpeed = 0;
				verticalSpeed = 0;
				break;

			}
		}

		public static Vector2 DirectionToVectorSpeeds(Direction direction){
			Vector2 vector = new Vector2(0,0);
			switch (direction) {
			case Direction.South:
				vector = new Vector2(0, -1);
				break;

			case Direction.North:
				vector = new Vector2(0, 1);
				break;

			case Direction.East:
				vector = new Vector2(1, 0);
				break;

			case Direction.West:
				vector = new Vector2(-1, 0);
				break;

			case Direction.NorthEast:
				vector = new Vector2(1, 1);
				break;

			case Direction.NorthWest:
				vector = new Vector2(-1, 1);
				break;

			case Direction.SouthEast:
				vector = new Vector2(1, -1);
				break;

			case Direction.SouthWest:
				vector = new Vector2(-1, -1);
				break;

			case Direction.NoDirection:
				vector = new Vector2(0, 0);
				break;

			}
			return vector;
		}

		public static void AxisSpeedsToDirection(ref Direction direction, float horizontalSpeed, float verticalSpeed){
			if (horizontalSpeed > 0) {
				if (verticalSpeed == 0) {
					direction = Direction.East;
				} else if (verticalSpeed > 0) {
					direction = Direction.NorthEast;
				} else if (verticalSpeed < 0) {
					direction = Direction.SouthEast;
				}
			} else if (horizontalSpeed < 0) {
				if (verticalSpeed == 0) {
					direction = Direction.West;
				} else if (verticalSpeed > 0) {
					direction = Direction.NorthWest;
				} else if (verticalSpeed < 0) {
					direction = Direction.SouthWest;
				}
			} else {
				if (verticalSpeed > 0) {
					direction = Direction.North;
				}

				if (verticalSpeed < 0) {
					direction = Direction.South;
				}

				if (verticalSpeed == 0) {
					direction = Direction.NoDirection;
				}
			}
		}

	}
}
