using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class GroundDetector : MonoBehaviour {

		private enum GroundState {
			GROUND,
			AIR
		}

		private GroundState m_currentGroundState = GroundState.AIR;

		[SerializeField]
		private GameObject m_noticeTarget;

		[SerializeField]
		private LayerMask m_targetLayer;

		private BoxCollider2D m_collider;

		private void Awake() {
			m_collider = GetComponent<BoxCollider2D>();
		}

		private void FixedUpdate() {

			RaycastHit2D hitInfo = Physics2D.BoxCast(
				transform.position , m_collider.bounds.size ,
				0 , Vector2.zero , 0 , m_targetLayer);

			GroundState prevState;

			prevState = (hitInfo) ? GroundState.GROUND : GroundState.AIR;	

			if(m_currentGroundState != prevState) {
				m_currentGroundState = prevState;
				switch (m_currentGroundState) {
					case GroundState.GROUND: m_noticeTarget.SendMessage("OnGroundEnter",hitInfo.collider); break;
					case GroundState.AIR: m_noticeTarget.SendMessage("OnGroundExit", hitInfo.collider); break;
				}
			}
			

		}
	}
}