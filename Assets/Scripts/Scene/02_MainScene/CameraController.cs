using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class CameraController : MonoBehaviour {

		[SerializeField]
		private Camera m_targetCamera;

		[SerializeField]
		private Transform m_focusTarget;

		[SerializeField]
		private bool m_freezeX, m_freezeY;

		[SerializeField]
		private Transform m_rightEdge, m_leftEdge;

		private float CameraRightEdgePosition {
			get {
				if (m_targetCamera == null) return 0;
				return m_targetCamera.ViewportToWorldPoint(Vector2.one).x;
			}
		}

		private float CameraLeftEdgePosition {
			get {
				if (m_targetCamera == null) return 0;
				return m_targetCamera.ViewportToWorldPoint(Vector2.zero).x;
			}
		}

		private float CameraWidth {
			get {
				return CameraRightEdgePosition - CameraLeftEdgePosition;
			}
		}

		private void Update() {
			
			if (m_focusTarget == null) return;
			
			Vector3 pos = this.transform.position;
			
			if (!m_freezeX) pos.x = m_focusTarget.position.x;
			if (!m_freezeY) pos.y = m_focusTarget.position.y;

			if (m_leftEdge != null) {
				if (pos.x < m_leftEdge.position.x + CameraWidth / 2) {
					pos.x = m_leftEdge.position.x + CameraWidth / 2;
				}
			}

			if (m_rightEdge != null) {
				if (pos.x > m_rightEdge.position.x - CameraWidth / 2) {
					pos.x = m_rightEdge.position.x - CameraWidth / 2;
				}
			}

			this.transform.position = pos;
		}

		public void SetFocusTarget(GameObject arg_target) {
			m_focusTarget = arg_target.transform;
		}
		
	}
}