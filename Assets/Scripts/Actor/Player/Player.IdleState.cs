using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public partial class Player {

		/// <summary>
		/// プレイヤーの静止状態
		/// </summary>
		public class IdleState : IPlayerState {

			/// <summary>プレイヤーへの参照</summary>
			private Player _;

			public IdleState(Player arg_player) {
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
				if (_.m_jump) {
					_.Jump(_.m_jumpPower);
					_.m_jump = false;
				}
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
				if (_.m_dead) return new DeadState(_);
				if (_.m_hide) return new HideState(_);
				if (_.m_leftRun || _.m_rightRun) return new RunState(_);
				return null;
			}
		}

	}
}