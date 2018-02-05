using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace Bucket {

	/// <summary>
	/// ゲーム内スコア
	/// </summary>
	public class GameScore {

		/// <summary>残り時間</summary>
		public int m_limitTime;

		/// <summary>死亡回数</summary>
		public int m_deathCount;

		/// <summary>討伐回数</summary>
		public int m_killCount;

		/// <summary>アイテム投棄回数</summary>
		public int m_throwCount;

		/// <summary>
		/// ゴールポイント
		/// ※ゴール時に加算される
		/// </summary>
		public int m_goalPoint;

		/// <summary>ゴール時に獲得できるスコア</summary>
		public int GOAL_POINT = 5000;

		public static int TotalScore {
			get {
				//TODO:スコア計算式をここに記述
				return 0;
			}
		}
	}

	public class MainScene : Scene {

		//-----------------------------------------
		//	プレイヤー
			[Header("Player")]
		//-----------------------------------------

		/// <summary>生成するプレイヤーのプレハブ</summary>
		[SerializeField]
		private GameObject m_playerPrefab;

		[SerializeField]
		private PlayerController m_playerController;

		/// <summary>制御する対象</summary>
		private Player m_player;

		/// <summary>デフォルトのプレイヤー名</summary>
		private const string PLAYER_NAME = "Player";

		[SerializeField]
		private CameraController m_cameraController;

		public readonly GameScore m_gameScore = new GameScore();

		public override IEnumerator OnEnter() {
			
			m_player = Instantiate(m_playerPrefab).GetComponent<Player>();
			m_player.name = PLAYER_NAME;

			m_playerController.Initialize(m_player);

			m_cameraController.SetFocusTarget(m_player.gameObject);

			AppManager.Instance.fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
		}

		public override IEnumerator OnUpdate() {
			while (true) {
				yield return null;
			}
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("SceneResult");
		}
	}
}