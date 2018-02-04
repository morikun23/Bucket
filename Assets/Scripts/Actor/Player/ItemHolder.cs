using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {

	/// <summary>
	/// アイテムを扱っているときのコールバック
	/// </summary>
	public interface IHolderCallbackReciever {

		/// <summary>アイテムを掴む時に実行される</summary>
		void OnItemGrasp(ItemHolder arg_holder);

		/// <summary></summary>
		void OnItemRelease(ItemHolder arg_holder);
		
	}

	/// <summary>
	/// アイテムを扱うためのクラス
	/// </summary>
	public class ItemHolder : MonoBehaviour {

		/// <summary>プレイヤー</summary>
		[SerializeField]
		private Player m_player;

		/// <summary>アイテム感知範囲</summary>
		[SerializeField]
		private BoxCollider2D m_searchArea;

		/// <summary>掴んでいるアイテム</summary>
		private Item m_holdingitem;

		/// <summary>アイテム放出角度</summary>
		[SerializeField,Range(0,89)]
		private float m_throwingAngle = 85;

		/// <summary>アイテム放出力</summary>
		[SerializeField,Range(1,10)]
		private float m_throwingPower = 1;

		/// <summary>アイテム放出距離</summary>
		[SerializeField,Range(0,10)]
		private float m_throwingDistance = 1.5f;

		/// <summary>コールバック対象</summary>
		[SerializeField]
		private readonly List<IHolderCallbackReciever> m_callBackRecievers = new List<IHolderCallbackReciever>(); 

		/// <summary>
		/// 掴んでいるアイテム
		/// </summary>
		public Item HoldingItem {
			get {
				return m_holdingitem;
			}
		}

		/// <summary>
		/// コールバックを受け取る相手を追加する
		/// </summary>
		/// <param name="arg_callBackReceiver"></param>
		public void AddCallBackReceiver(IHolderCallbackReciever arg_callBackReceiver) {
			m_callBackRecievers.Add(arg_callBackReceiver);
		}

		/// <summary>
		/// 近くにあるアイテムを掴む
		/// </summary>
		public void Grasp() {
			
			foreach(IHolderCallbackReciever reciever in m_callBackRecievers) {
				reciever.OnItemGrasp(this);
			}

			RaycastHit2D hitInfo = Physics2D.BoxCast(
				m_searchArea.transform.position,m_searchArea.bounds.size , 0 , Vector2.zero , 0,
				1 << LayerMask.NameToLayer("Item"));

			if (hitInfo) {
				Item item = hitInfo.transform.GetComponent<Item>();
				if (item) {
					m_holdingitem = item.GetGraspableItem();
				}
			}

			if (!m_holdingitem) return;

			m_holdingitem.transform.SetParent(this.transform);
			m_holdingitem.transform.localPosition = Vector3.up * 0.5f;

		}

		/// <summary>
		/// 現在アイテムを掴んでいるか取得
		/// </summary>
		/// <returns></returns>
		public bool IsHolding() {
			return m_holdingitem != null;
		}


		#region 放物線状に移動させるロジック(webから引用)

		/// <summary>
		/// 所持しているアイテムを射出する
		/// </summary>
		public void Throw() {

			if (m_holdingitem == null) return;

			foreach (IHolderCallbackReciever reciever in m_callBackRecievers) {
				reciever.OnItemRelease(this);
			}
			
			// 射出速度を算出
			Vector3 velocity = CalculateVelocity(
				m_holdingitem.transform.position,
				transform.position + Vector3.right * (int)m_player.GetCurrentDirection() * m_throwingDistance,
				m_throwingAngle);

			m_holdingitem.GetComponent<Rigidbody2D>().isKinematic = false;

			m_holdingitem.Throw(velocity * m_throwingPower);

			m_holdingitem.transform.SetParent(null);

			m_holdingitem = null;

		}

		/// <summary>
		/// ボールを射出する
		/// </summary>
		public void Throw(Vector2 arg_destination) {

			if (m_holdingitem == null || arg_destination == null) return;

			// 射出速度を算出
			Vector3 velocity = CalculateVelocity(m_holdingitem.transform.position, arg_destination, m_throwingAngle);

			m_holdingitem.GetComponent<Rigidbody2D>().isKinematic = false;

			m_holdingitem.Throw(velocity*1.5f);

			m_holdingitem.transform.SetParent(null);

			m_holdingitem = null;

		}

		/// <summary>
		/// 標的に命中する射出速度の計算
		/// </summary>
		/// <param name="pointA">射出開始座標</param>
		/// <param name="pointB">標的の座標</param>
		/// <returns>射出速度</returns>
		private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle) {
			// 射出角をラジアンに変換
			float rad = angle * Mathf.PI / 180;

			// 水平方向の距離x
			float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

			// 垂直方向の距離y
			float y = pointA.y - pointB.y;

			// 斜方投射の公式を初速度について解く
			float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

			if (float.IsNaN(speed)) {
				// 条件を満たす初速を算出できなければVector3.zeroを返す
				return Vector3.zero;
			}
			else {
				return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
			}
		}
#endregion
	}
}