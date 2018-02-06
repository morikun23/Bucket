using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class HitDetector : MonoBehaviour {

		/// <summary>
		/// 敵と当たったときに実行される
		/// </summary>
		/// <param name="arg_collider"></param>
		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if(arg_collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
				EnemyBehaviour enemy = arg_collider.GetComponent<EnemyBehaviour>();
				if (enemy) {
					enemy.SendMessage("OnCollidedItem");
					transform.parent.gameObject.SendMessage("OnCollided");
				}
			}
		}
	}
}