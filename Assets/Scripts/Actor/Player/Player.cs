using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {

	/// <summary>
	/// ゲーム内のプレイヤー
	/// FSMの形式で実装されている
	/// </summary>
	public partial class Player : ActorBase , IHolderCallbackReciever {

		/// <summary>
		/// プレイヤーの状態
		/// ※FSMによる実装
		/// </summary>
		public interface IPlayerState {

			/// <summary>
			/// この状態になったときに実行される
			/// </summary>
			void OnEnter();

			/// <summary>
			/// この状態の間、毎フレーム実行される
			/// </summary>
			void OnUpdate();

			/// <summary>
			/// この状態ではなくなるときに実行される
			/// </summary>
			void OnExit();

			/// <summary>
			/// 状態の変更があるか取得する
			/// </summary>
			/// <returns></returns>
			IPlayerState GetNextState();
		}

		//-----------------------------------------------
			[Header("Unity Component")]
		//-----------------------------------------------

		/// <summary>Rigidbody</summary>
		[SerializeField]
		private Rigidbody2D m_rigidbody;

		/// <summary>SpriteRenderer</summary>
		[SerializeField]
		private SpriteRenderer m_spriteRenderer;

		/// <summary>Animator</summary>
		[SerializeField]
		private Animator m_animator;

		//----------------------------------------------
			[Header("My Parts")]
		//----------------------------------------------

		/// <summary>索敵用コライダー</summary>
		[SerializeField]
		private CircleCollider2D m_searchArea;
		

		//----------------------------------------------
			[Header("Status")]
		//----------------------------------------------

		/// <summary>移動速度</summary>
		[SerializeField]
		private float m_runSpeed;

		/// <summary>ジャンプ力</summary>
		[SerializeField]
		private float m_jumpPower;

		/// <summary>地面に着地している状態である</summary>
		private bool m_isGrounded;

		/// <summary>現在のプレイヤーの状態</summary>
		private IPlayerState m_currentState;

		/// <summary>左移動フラグ</summary>
		private bool m_leftRun;

		/// <summary>右移動フラグ</summary>
		private bool m_rightRun;
		
		/// <summary>隠密フラグ</summary>
		private bool m_hide;

		/// <summary>死亡フラグ</summary>
		private bool m_dead;

		/// <summary>ジャンプフラグ</summary>
		private bool m_jump;

		/// <summary>
		/// Animatorコンポーネント
		/// ※読み取り専用
		/// </summary>
		public Animator AnimatorComponent {
			get {
				return m_animator;
			}
		}

		/// <summary>
		/// Rigidbody2Dコンポーネント
		/// ※読み取り専用
		/// </summary>
		public Rigidbody2D RigidbodyComponent {
			get {
				return m_rigidbody;
			}
		}

		/// <summary>
		/// Start by Unity
		/// </summary>
		void Start() {
			StateTransition(new IdleState(this));
		}

		void OnEnable() {
			m_rightRun = false;
			m_leftRun = false;
			m_jump = false;
			m_dead = false;
		}

		// Update is called once per frame
		void Update() {

			#region FSMによる実装
			IPlayerState nextState = m_currentState.GetNextState();

			if(nextState != null) {
				StateTransition(nextState);
			}

			m_currentState.OnUpdate();
			#endregion

			transform.localScale = new Vector3(
				(int)m_currentDirection,
				transform.localScale.y,
				transform.localScale.z);
		}

		/// <summary>
		/// 現在のステート（状態）を取得する
		/// </summary>
		/// <returns>Player.○○の形式</returns>
		public System.Type GetCurrentState() {
			return m_currentState.GetType();
		}

		public void Jump() {
			m_jump = true;
		}

		/// <summary>
		/// 指定されたジャンプ力でジャンプする
		/// </summary>
		/// <param name="arg_jumpPower">ジャンプ力</param>
		private void Jump(float arg_jumpPower) {

			if (!m_isGrounded) return;

			m_animator.SetTrigger("Jump");
			//気持ちよくジャンプさせるため重力加速度をリセットする
			m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x , 0);
			m_rigidbody.AddForce(Vector2.up * arg_jumpPower);
		}

		/// <summary>
		/// ステートの遷移を行う
		/// </summary>
		/// <param name="arg_nextState">次のステート</param>
		private void StateTransition(IPlayerState arg_nextState) {
			if (m_currentState != null) {
				m_currentState.OnExit();
				m_currentState = null;
			}
			if (arg_nextState != null) {
				m_currentState = arg_nextState;
				m_currentState.OnEnter();
			}
		}

		public bool IsGrounded() {
			return m_isGrounded;
		}

		//---------------------------------------------------
		//　以下、外部からのコールバック
		//　※今後、コールバックなどを追加するときは以下に追加すること
		//---------------------------------------------------

		/// <summary>
		/// 着地時の処理
		/// 地面と接触したときにコールバックとして実行される
		/// </summary>
		private void OnGroundEnter() {
			m_animator.SetBool("OnGround" , true);
			m_isGrounded = true;
		}

		/// <summary>
		/// 離陸時の処理
		/// 地面と接触がなくなったときにコールバックとして実行される
		/// </summary>
		private void OnGroundExit() {
			m_animator.SetBool("OnGround" , false);
			m_isGrounded = false;
		}

		/// <summary>
		/// アイテムを掴む時に実行される
		/// </summary>
		/// <param name="arg_holder"></param>
		void IHolderCallbackReciever.OnItemGrasp(ItemHolder arg_holder) {
			m_animator.SetBool("Grasp" , true);
		}

		/// <summary>
		/// アイテムを放出するときに実行される
		/// </summary>
		/// <param name="arg_holder"></param>
		void IHolderCallbackReciever.OnItemRelease(ItemHolder arg_holder) {
			m_animator.SetBool("Grasp" , false);
		}
	}
}