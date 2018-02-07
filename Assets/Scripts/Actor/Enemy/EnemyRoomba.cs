using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket
{

    public class EnemyRoomba : EnemyBehaviour
    {

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (m_isPlayerFound)
            {
                StartCoroutine(DoesFindPlayer());
            }

            if (m_isSwoon)
            {
                StartCoroutine(DoesSwoon());
            }

            if (m_isItemFound)
            {
                StartCoroutine(DoesFindItem());
            }
        }

        //プレイヤーを見つけたら、Findのアニメが終わるまで待機
        IEnumerator DoesFindPlayer()
        {
            m_anim.SetBool("Find", true);
            m_anim.Update(0);
            yield return new WaitForSeconds(1);
			m_anim.SetBool("Find", false);
            OnSetTargetPlayer();
        }

        //気絶したら、気絶のアニメが終わるまで待機
        IEnumerator DoesSwoon()
        {
            m_anim.SetBool("Stun", true);
            m_anim.Update(0);
			yield return new WaitForSeconds(3);

            m_anim.SetBool("Stun", false);
            m_isSwoon = false;
        }

        //アイテムを見つけたら１秒待機
        IEnumerator DoesFindItem()
        {

            yield return new WaitForSeconds(1);
            m_isItemFound = false;
        }
    }

}