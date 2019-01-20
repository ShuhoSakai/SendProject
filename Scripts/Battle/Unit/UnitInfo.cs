using UnityEngine;

namespace Battle
{
    [
        System.Serializable,
        CreateAssetMenu(menuName = "Unit/CreateUnitData", fileName = "UnitData")
    ]
    public class UnitInfo : ScriptableObject
    {
        /// <summary>
        /// どのパネルの上にいるかを返す
        /// </summary>
        /// <value>The index of the panel.</value>
        public int PanelIndex;

        /// <summary>
        /// ユニットの名前
        /// </summary>
        public string Name;

        /// <summary>
        /// ユニットID
        /// </summary>
        public int UnitId;

        /// <summary>
        /// プレイヤーのユニットか敵側のユニットの情報
        /// </summary>
        public PlayerType PlayerOrEnemy;

        /// <summary>
        /// 最大Hp
        /// </summary>
        public int MaxHp;

        /// <summary>
        /// 現在のHp
        /// </summary>
        public int NowHp;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Atk;

        /// <summary>
        /// 知力
        /// </summary>
        public int Intellect;

        /// <summary>
        /// パネル間の移動速度
        /// </summary>
        public int Spd;

        /// <summary>
        /// ユニットが生きてるか死んでるか、ダメージ受けてるかなど、のステータスチェック
        /// </summary>
        public UnitState UnitState;
    }
}