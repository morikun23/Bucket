using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public partial class Player {

		/// <summary>
		/// プレイヤーの静止状態
		/// </summary>
		public class HideState : IPlayerState {

			/// <summary>プレイヤーへの参照</summary>
			private Player _;

			public HideState(Player arg_player) {
				_ = arg_player;
			}

			/// <summary>
			/// この状態になったときに実行される
			/// </summary>
			void IPlayerState.OnEnter() {
				_.m_searchArea.enabled = false;
				_.m_animator.SetBool("Hide" , true);
			}

			/// <summary>
			/// この状態の間、毎フレーム実行される
			/// </summary>
			void IPlayerState.OnUpdate() {
				if (_.m_jump) {
					_.m_jump = false;
				}
			}

			/// <summary>
			/// この状態ではなくなるときに実行される
			/// </summary>
			void IPlayerState.OnExit() {
				_.m_searchArea.enabled = true;
				_.m_animator.SetBool("Hide" , false);
			}

			/// <summary>
			/// 状態の変更があるか取得する
			/// </summary>
			/// <returns></returns>
			IPlayerState IPlayerState.GetNextState() {
				if (_.m_dead) return new DeadState(_);
				if (!_.m_hide) return new IdleState(_);
				return null;
			}
		}

		public void Hide() {
			m_hide = true;
		}

		public void Show() {
			m_hide = false;
		}
		
	}
}