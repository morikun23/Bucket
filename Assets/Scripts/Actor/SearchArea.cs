using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class SearchArea : MonoBehaviour {

		[SerializeField]
		private GameObject m_noticeTarget;
		
		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if(m_noticeTarget != null) {
				m_noticeTarget.SendMessage("OnFoundTarget");
			}	
		}

		private void OnTriggerExit2D(Collider2D arg_collider) {
			if (m_noticeTarget != null) {
				m_noticeTarget.SendMessage("OnLostTarget");
			}
		}
	}
}