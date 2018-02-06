using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public partial class Player {

		/// <summary>
		/// プレイヤーの死亡状態
		/// </summary>
		public class DeadState : IPlayerState {

			/// <summary>プレイヤーへの参照</summary>
			private Player _;

			public DeadState(Player arg_player) {
				_ = arg_player;
			}

			/// <summary>
			/// この状態になったときに実行される
			/// </summary>
			void IPlayerState.OnEnter() {

			}

			/// <summary>
			/// この状態の間、毎フレーム実行される
			/// </summary>
			void IPlayerState.OnUpdate() {

			}

			/// <summary>
			/// この状態ではなくなるときに実行される
			/// </summary>
			void IPlayerState.OnExit() {

			}

			/// <summary>
			/// 状態の変更があるか取得する
			/// </summary>
			/// <returns></returns>
			IPlayerState IPlayerState.GetNextState() {
				if (!_.m_dead) return new IdleState(_);
				return null;
			}
		}

		/// <summary>
		/// プレイヤーを死亡させる
		/// </summary>
		public void Dead() {
			m_dead = true;
		}
	}
}