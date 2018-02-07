using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;
using UnityEngine.UI;

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
		
		public static int TotalScore {
			get {
				//TODO:スコア計算式をここに記述
				return 0;
			}
		}
	}

	/// <summary>
	/// ゲーム内プレイ時間
	/// </summary>
	public class GameTimer {

		/// <summary>総合計時間</summary>
		private float m_totalTime;

		/// <summary>総合計時間</summary>
		public float TotalTime {
			get {
				return m_totalTime;
			}
		}

		/// <summary>分</summary>
		public float Minutes {
			get {
				return m_totalTime / 60;
			}
		}

		/// <summary>秒</summary>
		public float Seconds {
			get {
				return m_totalTime % 60;
			}
		}

		/// <summary>
		/// タイマーのカウントをリセットする
		/// </summary>
		public void Reset() {
			m_totalTime = 0;
		}

		/// <summary>
		/// タイマーの時間を追加する
		/// </summary>
		public void AddTime() {
			m_totalTime += Time.deltaTime;
		}

		/// <summary>
		/// タイマーの時間を追加する
		/// </summary>
		/// <param name="arg_seconds">秒</param>
		public void AddTime(float arg_seconds) {
			m_totalTime += arg_seconds;
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

		//スコア
		public static readonly GameScore m_gameScore = new GameScore();

		//タイマー
		public static readonly GameTimer m_gameTimer = new GameTimer();

		[SerializeField]
		private Stage m_stage;

		//-----------------------------------------
		//	UI
			[Header("UI")]
		//-----------------------------------------
		[SerializeField]
		private Text m_minuteTimeText;

		[SerializeField]
		private Text m_secondTimeText;

		private bool m_isGamePlaying;


		public override IEnumerator OnEnter() {

			#region プレイヤーの生成
			m_player = Instantiate(m_playerPrefab).GetComponent<Player>();
			m_player.name = PLAYER_NAME;
			
			m_playerController.Initialize(m_player);

			#endregion

			//カメラ起動
			m_cameraController.SetFocusTarget(m_player.gameObject);

			#region ステージの起動
			m_stage.Initialize(m_player);

			#endregion

			AppManager.Instance.fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);

			//タイマー始動
			m_gameTimer.Reset();
			
			
			m_isGamePlaying = true;
		}

		public override IEnumerator OnUpdate() {
			while (m_isGamePlaying) {

				#region タイマー更新
				m_minuteTimeText.text = ((int)m_gameTimer.Minutes).ToString().PadLeft(2,'0');
				m_secondTimeText.text = ((int)m_gameTimer.Seconds).ToString().PadLeft(2,'0');

				//タイマー更新
				m_gameTimer.AddTime();
				#endregion
				
				yield return null;
			}
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("SceneResult");
		}

		/// <summary>
		/// プレイヤーがゴールしたときに実行される
		/// </summary>
		public void OnPlayerDoesGoaled(){
			m_isGamePlaying = false;
		}
	}
}