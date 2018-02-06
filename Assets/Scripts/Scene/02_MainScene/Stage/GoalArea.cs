using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class GoalArea : MonoBehaviour {

		//コールバック
		private System.Action m_callBack;

		public void Initialize(System.Action arg_callBack){
			m_callBack = arg_callBack;
		}

		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				if (m_callBack != null) m_callBack();
			}
		}
	}
}