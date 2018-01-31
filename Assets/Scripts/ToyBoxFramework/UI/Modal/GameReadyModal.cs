using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox {
	public class GameReadyModal : ModalController {

		/// <summary>Textコンポーネント</summary>
		[SerializeField]
		private Text m_text;

		/// <summary>表示テキスト</summary>
		[HideInInspector]
		public string m_message;

		/// <summary>ゲーム開始ボタン</summary>
		[SerializeField]
		private UIButton m_playButton;
		
		/// <summary>ゲーム開始ボタンが押されたときのコールバック</summary>
		public System.Action m_playAction;

		/// <summary>
		/// 起動メソッド
		/// </summary>
		public override void OnActive() {
			m_text.text = m_message;
			m_playButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease,this.OnPlayButton));
		}

		/// <summary>
		/// ゲーム開始ボタンが押されたときの処理
		/// </summary>
		private void OnPlayButton() {
			m_playAction();
			this.Hide();
		}
	}
}