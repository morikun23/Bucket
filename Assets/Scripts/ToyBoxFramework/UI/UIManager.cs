using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {

	/// <summary>
	/// レイヤーの種類
	/// 数字が大きいものほど前に表示させる
	/// </summary>
	public enum UILayer : int {
		DEFAULT = 0,
		MAIN = 1,
		FRONT = 2,
		MODAL = 3,
		SYSTEM = 4
	}

	public partial class ResourcePath {

		/// <summary>モーダル全般の親パス</summary>
		public const string MODAL_PATH = "Contents/UI/Modal/Prefabs/";

	}
	
	public class UIManager : MonoBehaviour {

		private static UIManager m_instance;

		public static UIManager Instance {
			get {
				if(m_instance == null) {
					m_instance = FindObjectOfType<UIManager>();
				}
				return m_instance;
			}
		}

		private Camera m_uiCamera;

		public Camera UICamera {
			get {
				if(m_uiCamera == null) {
					m_uiCamera = System.Array.Find(FindObjectsOfType<Camera>() ,
						_ => _.gameObject.layer == LayerMask.NameToLayer("UI"));
					if(m_uiCamera == null) {
						Debug.LogError("[ToyBox]UICameraが見つかりません");
					}
				}
				return m_uiCamera;
			}
		}

		/// <summary>モーダルプレハブのキャッシュ</summary>
		private static readonly Dictionary<string , GameObject> m_modalContainer = new Dictionary<string , GameObject>();
		
		private void Awake() {
			if(Instance != this) {
				Destroy(this.gameObject);
			}


			#region Modalフォルダの中身をすべて取得
			GameObject[] modalList = Resources.LoadAll<GameObject>(ResourcePath.MODAL_PATH);
			foreach (GameObject modal in modalList) {
				m_modalContainer[modal.name] = modal;
			}
			#endregion
		
		}

		/// <summary>
		/// メッセージモーダルを表示する
		/// </summary>
		/// <param name="arg_message">表示内容</param>
		/// <param name="arg_callBack">モーダル表示終了後コールバック</param>
		public void PopupMessageModal(string arg_message , System.Action arg_callBack = null) {

			MessageModal modal = InstantiateModal(m_modalContainer["MessageModal"]) as MessageModal;

			modal.m_message = arg_message;
			modal.Show(arg_callBack);
			
		}

		/// <summary>
		/// ゲーム開始確認モーダルを表示する
		/// </summary>
		/// <param name="arg_message">表示内容</param>
		/// <param name="arg_onGamePlay">ゲーム開始ボタン</param>
		/// <param name="arg_callBack">モーダル表示終了後コールバック</param>
		public void PopupGameReadyModal(string arg_message ,System.Action arg_onGamePlay, System.Action arg_callBack = null) {

			GameReadyModal modal = InstantiateModal(m_modalContainer["GameReadyModal"]) as GameReadyModal;

			modal.m_message = arg_message;
			modal.m_playAction = arg_onGamePlay;
			modal.Show(arg_callBack);

		}

		/// <summary>
		/// モーダルを生成する
		/// 生成時にモーダルのデフォルト設定を適用させる
		/// </summary>
		/// <param name="arg_prefab"></param>
		/// <returns></returns>
		private ModalController InstantiateModal(GameObject arg_prefab) {
			Canvas canvas = Instantiate(arg_prefab).GetComponent<Canvas>();

			canvas.renderMode = RenderMode.ScreenSpaceCamera;
			canvas.worldCamera = UICamera;
			return canvas.GetComponentInChildren<ModalController>();
		}

	}
}