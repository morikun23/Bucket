using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public partial class Player {

		/// <summary>
		/// プレイヤーの移動状態
		/// </summary>
		public class RunState : IPlayerState {

			/// <summary>プレイヤーへの参照</summary>
			private Player _;

			public RunState(Player arg_player) {
				_ = arg_player;
			}

			/// <summary>
			/// この状態になったときに実行される
			/// </summary>
			void IPlayerState.OnEnter() {
				_.m_animator.SetBool("Run" , true);
			}

			/// <summary>
			/// この状態の間、毎フレーム実行される
			/// </summary>
			void IPlayerState.OnUpdate() {

				if (_.m_jump) {
					_.Jump(_.m_jumpPower);
					_.m_jump = false;
				}

				if (_.m_leftRun && _.m_rightRun) {
					_.m_rigidbody.velocity = new Vector3() {
						x = 0 ,
						y = _.m_rigidbody.velocity.y ,
						z = 0
					};
					return;
				}

				//行き止まりである
				if (IsDeadEnd()) {
					return;
				}

				_.m_rigidbody.velocity = new Vector3() {
					x = _.m_runSpeed * (int)_.m_currentDirection ,
					y = _.m_rigidbody.velocity.y ,
					z = 0
				};
			}

			/// <summary>
			/// この状態ではなくなるときに実行される
			/// </summary>
			void IPlayerState.OnExit() {
				_.m_animator.SetBool("Run" , false);
				_.m_rigidbody.velocity = new Vector3() {
					x = 0 ,
					y = _.m_rigidbody.velocity.y ,
					z = 0
				};
			}

			/// <summary>
			/// 状態の変更があるか取得する
			/// </summary>
			/// <returns></returns>
			IPlayerState IPlayerState.GetNextState() {
				if (_.m_dead) return new DeadState(_);
				if (_.m_hide) return new HideState(_);
				if (!(_.m_leftRun || _.m_rightRun)) return new IdleState(_);
				return null;
			}

			/// <summary>
			/// 進行方向に壁がないか調べる
			/// </summary>
			/// <returns></returns>
			private bool IsDeadEnd() {
				if (Physics2D.BoxCast(_.transform.position ,
					Vector2.one , 0 , Vector2.right * (int)_.m_currentDirection ,
					0.1f , 1 << LayerMask.NameToLayer("Ground"))) {
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// 指定方向へ移動を開始する
		/// </summary>
		/// <param name="arg_direction">移動方向</param>
		public void Run(Direction arg_direction) {
			m_currentDirection = arg_direction;
			if (arg_direction == Direction.LEFT) m_leftRun = true;
			else if (arg_direction == Direction.RIGHT) m_rightRun = true;
		}

		/// <summary>
		/// 指定方向への移動を終了する
		/// </summary>
		/// <param name="arg_direction">終了させる移動方向</param>
		public void Stop(Direction arg_direction) {
			if (arg_direction == Direction.LEFT) m_leftRun = false;
			else if (arg_direction == Direction.RIGHT) m_rightRun = false;
		}
	}
}