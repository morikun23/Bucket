using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class EnemyBehaviour : ActorBase {

        [SerializeField]
		private GameObject m_player;
        
        [SerializeField,Range(1,3)]
        private float m_sideTargetNum = 2;

        [SerializeField, Range(0.1f, 0.3f)]
        private float m_speed = 0.1f;
        [SerializeField, Range(0.5f,1)]
        private float m_attackSpeed;

        private Vector2 m_targetPos;
        private Vector2 m_startPos;

        private bool m_isPlayerFound = false;
        private bool m_isItemFound = false;
        private bool m_isSwoon = false;

        // ラジアン変数
        private float m_rad;

        // Use this for initialization
        void Start() {

            //m_player = FindObjectOfType<Player>().gameObject;

            m_moveSpeed = m_speed;

            m_startPos = transform.position;

            m_targetPos = new Vector2(m_startPos.x - m_sideTargetNum, transform.position.y);


            m_rad = Mathf.Atan2(
            m_targetPos.y - transform.position.y,
            m_targetPos.x - transform.position.x);
        }

		// Update is called once per frame
		void Update() {

             if (Input.GetKeyDown(KeyCode.Space)) m_isItemFound = false;

             Vector2 position = transform.position;

            //色々処理したい
            if (m_isSwoon)//気絶中
            {
                position = transform.position;
            }
            else if (m_isPlayerFound)//プレイヤー発見中
            {
                position.x += m_attackSpeed * Mathf.Cos(m_rad);

                if (Vector2.Distance(position, m_targetPos) < 0.3f)
                {
                    position = m_targetPos;
                }

            }
            else if (m_isItemFound)//アイテムに気を取られ中
            {
                position = transform.position;
            }
            else//通常時
            {
                position.x += m_moveSpeed * Mathf.Cos(m_rad);

                if (Vector2.Distance(position, m_targetPos) < 0.1f)
                {
                    
                    MoveDirectionChange();
                    LookBack();
                }
            }
            
            transform.position = position;
        }

		/// <summary>
		/// 標的を見つけたときに実行される
		/// </summary>
		private void OnFoundTarget() {

            if(m_isPlayerFound)return;

            m_targetPos = m_player.transform.position;
            m_rad = Mathf.Atan2(
                m_targetPos.y - transform.position.y,
                m_targetPos.x - transform.position.x);

            m_isPlayerFound = true;
            Invoke("OnLostTarget", 3);
        }

		/// <summary>
		/// 標的を見失ったときに実行される
		/// </summary>
		private void OnLostTarget() {

            m_startPos = transform.position;

            if(transform.localScale.x < 0)
            m_targetPos = new Vector2(m_startPos.x - m_sideTargetNum, transform.position.y);
            else
            m_targetPos = new Vector2(m_startPos.x + m_sideTargetNum, transform.position.y);

            m_rad = Mathf.Atan2(
            m_targetPos.y - transform.position.y,
            m_targetPos.x - transform.position.x);

            m_isPlayerFound = false;
        }

        /// <summary>
		/// 移動する方向を反転
		/// </summary>
		private void MoveDirectionChange()
        {
            if(m_targetPos.x == m_startPos.x + m_sideTargetNum)
            m_targetPos = new Vector2(m_startPos.x - m_sideTargetNum, transform.position.y);
            else
                m_targetPos = new Vector2(m_startPos.x + m_sideTargetNum, transform.position.y);

            m_rad = Mathf.Atan2(
                m_targetPos.y - transform.position.y,
                m_targetPos.x - transform.position.x);

        }

        /// <summary>
        /// アイテムが近くに落ちたときに実行される
        /// </summary>
        private void OnEncountItem(GameObject arg_item) {

            if (m_isPlayerFound || m_isItemFound) return;

            //位置と向きから、反転すべきかどうかを判断
            if((arg_item.transform.position.x < transform.position.x && transform.localScale.x > 0) ||
                (arg_item.transform.position.x > transform.position.x && transform.localScale.x < 0))
			this.LookBack();

            m_isItemFound = true;
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
		/// 気絶させる
		/// </summary>
		public void OnSwoon()
        {
            m_isSwoon = true;
        }

    }
}