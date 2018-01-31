using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class ModalController : MonoBehaviour {

		/// <summary>デフォルトのバックボタン</summary>
		[SerializeField]
		private UIButton m_exitButton;

		/// <summary>バックボタンを押したときのコールバック</summary>
		private System.Action m_exitCallback;

		[SerializeField]
		protected GameObject m_modalObject;

		[SerializeField]
		public GameObject ModalObject {
			get {
				return m_modalObject;
			}
		}

		/// <summary>Start関数は継承禁止</summary>
		protected void Start() { }

		/// <summary>Update関数は継承禁止</summary>
		protected void Update() { }

		/// <summary>
		/// モーダルを表示する
		/// </summary>
		public void Show() {
			this.Show(null);
		}
		
		/// <summary>
		/// モーダルを表示する
		/// （コールバックも同時に設定する）
		/// </summary>
		/// <param name="arg_callBack"></param>
		public void Show(System.Action arg_callBack) {

			ModalObject.SetActive(true);
			this.transform.localScale = Vector3.zero;
			this.m_exitCallback = arg_callBack;

			iTween.ScaleTo(ModalObject , iTween.Hash(
				"scale" , Vector3.one ,
				"time" , 0.25f ,
				"oncomplete" , "OnActive" ,
				"oncompletetarget" , this.gameObject
				));

			if (m_exitButton != null) {
				m_exitButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress,this.Hide));
			}
		}

		/// <summary>
		/// 起動メソッド
		/// </summary>
		public abstract void OnActive();

		/// <summary>
		/// モーダルを閉じる
		/// </summary>
		public void Hide() {
			iTween.ScaleTo(ModalObject , iTween.Hash(
				"scale" , Vector3.zero ,
				"time" , 0.25f ,
				"oncomplete" , "OnRemoved",
				"oncompletetarget", this.gameObject
				));
		}

		/// <summary>
		/// モーダルが閉じたときに実行する
		/// コールバックを通知する
		/// </summary>
		private void OnRemoved() {
			if(m_exitCallback != null) {
				m_exitCallback();
			}
			Destroy(this.gameObject);
		}
		
	}
}