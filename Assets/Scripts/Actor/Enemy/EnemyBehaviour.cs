using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class EnemyBehaviour : ActorBase {

		private Player m_target;
		
		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

		/// <summary>
		/// 標的を見つけたときに実行される
		/// </summary>
		private void OnFoundTarget() {
			
		}

		/// <summary>
		/// 標的を見失ったときに実行される
		/// </summary>
		private void OnLostTarget() {
			
		}

		/// <summary>
		/// アイテムが近くに落ちたときに実行される
		/// </summary>
		private void OnEncountItem() {
			this.LookBack();
		}

		/// <summary>
		/// 逆方向へ振り返る
		/// </summary>
		public void LookBack() {
			Vector2 direction = transform.localScale;
			direction.x = -direction.x;
			transform.localScale = direction;
		}

		/// <summary>
		/// 標的に対して攻撃を仕掛ける
		/// </summary>
		public void Attack() {

		}
		
	}
}