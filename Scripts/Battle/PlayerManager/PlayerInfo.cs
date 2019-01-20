using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class PlayerInfo
    {
        /// <summary>
        /// 城のHP
        /// </summary>
        public int CastleHP;

        /// <summary>
        /// 城の最大HP
        /// </summary>
        public int CastleMaxHP;

        /// <summary>
        /// ユーザー情報
        /// </summary>
        public UserInfo UserInfo;
    }

}