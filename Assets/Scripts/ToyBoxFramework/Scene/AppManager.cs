using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public sealed class AppManager : MonoBehaviour {

		#region Singleton
		private static AppManager m_instance;

		public static AppManager Instance {
			get {
				if (m_instance == null) {
					m_instance = FindObjectOfType<AppManager>();
				}
				return m_instance;
			}
		}

		private AppManager() { }
		#endregion

		//オーディオ環境
		[SerializeField]
		private AudioManager m_audioManager;
		public AudioManager audioManager { get { return m_audioManager; } }

		//フェード環境
		[SerializeField]
		private Fade m_fade;
		public Fade fade { get { return m_fade; } }

		//時間環境
		[SerializeField]
		private TimeManager m_timeManager;
		public TimeManager timeManager { get { return m_timeManager; } }

		//UI環境
		[SerializeField]
		private UIManager m_uiManager;
		public UIManager uiManager { get { return m_uiManager; } }

		/// <summary>
		/// 初期起動
		/// </summary>
		private void Awake() {
			if (Instance != this) {
				Destroy(this.gameObject);
				return;
			}

			#region NULLチェック
			if (m_audioManager == null) {
				Debug.LogError("[ToyBox]<color=red>AudioManager</color>が設定されていません");
			}

			if (m_fade == null) {
				Debug.LogError("[ToyBox]<color=red>Fade</color>が設定されていません");
			}

			if (m_timeManager == null) {
				Debug.LogError("[ToyBox]<color=red>TimeManager</color>が設定されていません");
			}
			#endregion

			DontDestroyOnLoad(this.gameObject);
		}
	}
}