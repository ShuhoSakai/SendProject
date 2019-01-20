using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using Unit;
using UnityEngine.UI;

namespace Battle.Unit
{
    public class UnitManager : Utility.SingletonMonoBehavior<UnitManager>
    {
        [SerializeField]
        Transform parent3D;

        [SerializeField]
        Night night;

        [SerializeField, Range(0.1f, 3f)]
        float moveSpeed;

        [SerializeField]
        Button skillButton;

        /// <summary>
        /// UIに表示するプレイヤーのユニット情報
        /// </summary>
        [SerializeField]
        UI_Unit[] uiUnits;

        List<UnitInfo> allUnitList = new List<UnitInfo>();

        IUnit moveUnitInfo;

        public Dictionary<int, UI_Unit> uiUnitDic = new Dictionary<int, UI_Unit>();

        public Dictionary<int, IUnit> playerUnitList = new Dictionary<int, IUnit>();

        public Dictionary<int, IUnit> enemyUnitList = new Dictionary<int, IUnit>();

        public void Initialize(List<UnitInfo> unitList)
        {
            this.allUnitList = unitList;

            this.SetUnitData();
        }

        public void SetSkillButtonEvent(System.Action callBack)
        {
            skillButton.onClick.RemoveAllListeners();
            skillButton.onClick.AddListener(() => 
            {
                callBack();
            });
        }

        /// <summary>
        /// ユニット死亡時の処理
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="index">Index.</param>
        public void DaethUnit(int id, int index, PlayerType type)
        {
            if (type == PlayerType.Player)
            {
                //// 仮
                this.playerUnitList[id].Transform.gameObject.SetActive(false);
                this.playerUnitList[id].UnitInfo.UnitState = UnitState.Death;
                FieldManager.instance.panelList[index].PlayerUnitInfo = null;
                uiUnitDic[id].SetButtonEvent();

            }
            else
            {
                //// 仮
                this.enemyUnitList[id].Transform.gameObject.SetActive(false);
                this.enemyUnitList[id].UnitInfo.UnitState = UnitState.Death;
                FieldManager.instance.panelList[index].EnemyUnitInfo = null;
            }
        }

        /// <summary>
        /// ユニットデータ作成
        /// プレイヤーユニットか敵ユニットか仕分ける
        /// </summary>
        void SetUnitData()
        {
            var counter = 0;
            foreach (var unit in this.allUnitList)
            {
                switch (unit.PlayerOrEnemy)
                {
                    case PlayerType.Player:
                        var player = Instantiate(this.night, this.parent3D);
                        player.UnitInfo = unit;
                        player.transform.localPosition = FieldManager.instance.cubeList[player.UnitInfo.PanelIndex].transform.localPosition;
                        player.Transform = player.transform;
                        this.playerUnitList.Add(unit.UnitId, player);

                        //// UIユニットに情報渡す
                        uiUnitDic.Add(unit.UnitId, uiUnits[counter]);
                        uiUnitDic[unit.UnitId].Show(unit);
                        counter++;

                        //// TODO 仮 これはデータ読み込ませてくる必要がある
                        FieldManager.instance.panelList[player.UnitInfo.PanelIndex].PlayerUnitInfo = player;
                        break;
                    case PlayerType.Enemy:

                        var enemy = Instantiate(this.night, this.parent3D);
                        enemy.UnitInfo = unit;
                        enemy.transform.localPosition = FieldManager.instance.cubeList[enemy.UnitInfo.PanelIndex].transform.localPosition;
                        enemy.Transform = enemy.transform;
                        this.enemyUnitList.Add(unit.UnitId, enemy);

                        //// TODO 仮 これはデータ読み込ませてくる必要がある
                        FieldManager.instance.panelList[enemy.UnitInfo.PanelIndex].EnemyUnitInfo = enemy;

                        break;
                }
            }
        }


        /// <summary>
        /// ユニットを作成
        /// タイミングは3Dパネルが複製された後にしないと、パネルにアクセスできずエラーが返ってくる
        /// </summary>
        public void CreateUnit()
        {
            foreach (var player in this.playerUnitList.Values)
            {
                var unit = Instantiate(this.night, this.parent3D);
                unit.transform.localPosition = FieldManager.instance.cubeList[player.UnitInfo.PanelIndex].transform.localPosition;
            }

            foreach (var enemy in this.enemyUnitList.Values)
            {
                var unit = Instantiate(this.night, this.parent3D);
                unit.transform.localPosition = FieldManager.instance.cubeList[enemy.UnitInfo.PanelIndex].transform.localPosition;
            }
        }

        /// <summary>
        /// UIのユニット情報を更新
        /// </summary>
        public void UpdateUI_Units(int key)
        {
            uiUnitDic[key].Show(playerUnitList[key].UnitInfo);
        }
    }
}