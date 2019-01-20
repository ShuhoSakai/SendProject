using Battle;
using Battle.Unit;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Unit
{
    public interface IUnit
    {
        UnitInfo UnitInfo { get; set; }

        Transform Transform { get; set; }

        /// <summary>
        /// ダメージ受けた時に走るメソッド
        /// </summary>
        /// <param name="damage">Damage.</param>
        /// <param name="id">ユニットID.</param>
        /// <param name="index">パネルIndex.</param>
        void ReceiveDamage(int damage, int id, int index, PlayerType type);

        /// <summary>
        /// 死亡時に走るメソッド
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="index">Index.</param>
        void Death(int id, int index, PlayerType type);

        /// <summary>
        /// 城にダメージを与える時に走らせるメソッド
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="type">Type.</param>
        void CastleDamage(int value, PlayerType type);

        /// <summary>
        /// 移動メソッド
        /// </summary>
        /// <param name="list">List.</param>
        void ExecuteMove(List<NormalCube> list);

        /// <summary>
        /// 各ユニットのスキルセット
        /// </summary>
        void ExecuteSkill();
    }
}