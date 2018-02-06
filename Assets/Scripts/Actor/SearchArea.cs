using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class SearchArea : MonoBehaviour {

		[SerializeField]
		private GameObject m_noticeTarget;
		
		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if(m_noticeTarget != null) {
				m_noticeTarget.SendMessage("OnFoundTarget",arg_collider);
			}	
		}

		private void OnTriggerExit2D(Collider2D arg_collider) {
			if (m_noticeTarget != null) {
				m_noticeTarget.SendMessage("OnLostTarget",arg_collider);
			}
		}

		public void OnEncountItem(Item arg_item) {
			if(m_noticeTarget != null) {
				m_noticeTarget.SendMessage("OnEncountItem");
			}
		}
	}
}