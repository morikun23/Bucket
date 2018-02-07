using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class Stage : MonoBehaviour {

		[SerializeField]
		private MainScene m_sceneManager;

		[SerializeField]
		private int m_onGoalScore = 5000;

		[SerializeField]
		private Transform m_startPoint;

		[SerializeField]
		private GoalArea m_goalPoint;

		private Player m_player;
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize(Player arg_player) {
			m_player = arg_player;

			PlayerGenerate();
			
			m_goalPoint.Initialize(this.OnPlayerDoesGoaled);
			
		}
		
		/// <summary>
		/// プレイヤーが死亡したときに実行される
		/// </summary>
		/// <param name="arg_callBack"></param>
		/// <returns></returns>
		private IEnumerator OnPlayerDoesDead(System.Action arg_callBack) {
			yield return new WaitForEndOfFrame();
			//プレイヤーが死亡するまで待機
			yield return new WaitWhile(() => m_player.GetCurrentState() != typeof(Player.DeadState));
			
			m_player.gameObject.SetActive(false);

			yield return null;
			
			arg_callBack();
		}

		/// <summary>
		/// プレイヤーを生成する
		/// </summary>
		private void PlayerGenerate(){

			m_player.transform.position = m_startPoint.position;
			
			m_player.gameObject.SetActive(true);
		
			StartCoroutine(OnPlayerDoesDead(this.PlayerGenerate));
		}

		
		private void OnStartPoint() {
			
		}

		private void OnEnemyDestroyed() {

		}

		/// <summary>
		/// プレイヤーがゴールしたときに実行される
		/// </summary>
		private void OnPlayerDoesGoaled() {
			MainScene.m_gameScore.m_goalPoint = m_onGoalScore;
			m_sceneManager.OnPlayerDoesGoaled();
		}

		private void OnPlayerDead() {
			
		}
	}
}