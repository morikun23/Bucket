using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public abstract class ActorBase : MonoBehaviour {

		public enum Direction {
			LEFT = -1,
			RIGHT = 1
		}

		[SerializeField]
		protected Direction m_currentDirection = Direction.RIGHT;

		protected float m_moveSpeed;

		/// <summary>
		/// 現在の向きを取得する
		/// </summary>
		/// <returns></returns>
		public Direction GetCurrentDirection() {
			return m_currentDirection;
		}
	}
}