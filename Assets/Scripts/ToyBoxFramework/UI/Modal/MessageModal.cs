using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox {
	public class MessageModal : ModalController {
		
		[SerializeField]
		private Text m_text;

		[HideInInspector]
		public string m_message;

		/// <summary>
		/// 起動メソッド
		/// </summary>
		public override void OnActive() {
			m_text.text = m_message;
		}
	}
}