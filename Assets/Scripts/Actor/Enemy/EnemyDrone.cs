using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket
{

    public class EnemyDrone : EnemyBehaviour
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

        //プレイヤーを見つけたら、１秒たつまで待機
        IEnumerator DoesFindPlayer()
        {

            yield return new WaitForSeconds(1);
            OnSetTargetPlayer();
        }

        //気絶したら、3秒たつまで待機
        IEnumerator DoesSwoon()
        {

            yield return new WaitForSeconds(3);
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
