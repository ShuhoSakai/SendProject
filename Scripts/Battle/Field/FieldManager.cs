using Const;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;
using Battle.Unit;

namespace Battle
{
    public class FieldManager : Utility.SingletonMonoBehavior<FieldManager>
    {
        /// <summary>
        /// パネル TODO インターフェース継承させるよー
        /// </summary>
        [SerializeField]
        TouchPanel touchPanel;

        /// <summary>
        /// 縦横にずらす量
        /// </summary>
        [SerializeField]
        float slideValue = 0.25f;

        [SerializeField]
        Vector3 fieldSliedeValue;

        [SerializeField]
        Vector3 fieldStartPos;

        [SerializeField]
        Vector2 startPos;

        [SerializeField]
        Transform panelParent_3D;

        [SerializeField]
        Transform touchPanelParent;

        [SerializeField]
        NormalCube normalCube;

        public bool IsSummon = false;

        public int SummonUnitId = -1;

        /// <summary>
        /// キャラの進むルート  TODO インターフェース継承させるよー
        /// </summary>
        List<TouchPanel> draggingList = new List<TouchPanel>();

        /// <summary>
        /// パネルにインデクスを指定してアクセスするためのDictionary
        /// TODO:inteface継承したパネル
        /// </summary>
        public Dictionary<int, TouchPanel> panelList = new Dictionary<int, TouchPanel>();

        /// <summary>
        /// 3DのブロックのDictionary TODO ここもIBlockてきなインターフェース継承させる予定
        /// </summary>
        /// TODO Inspectorで見るため用
        public Dictionary<int, NormalCube> cubeList = new Dictionary<int, NormalCube>();

        enum TouchState
        {
            Start,
            Dragging,
            End,

            Wait,
        }

        TouchState touchState = TouchState.Wait;

        public void Initialize()
        {
            this.FieldCreate();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (this.touchState == TouchState.Wait)
                {
                    this.OnDragStart();
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (this.touchState == TouchState.Start || this.touchState == TouchState.Dragging)
                {
                    this.OnDragging();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (this.touchState == TouchState.Dragging)
                {
                    this.OnDragEnd();
                }
            }
        }

        void FieldCreate()
        {
            //// フィールド作成
            var index = 0;
            for (var i = 0; i < FieldLength.vertical; i++)
            {
                for (var j = 0; j < FieldLength.horizontal; j++)
                {

                    //// タッチングパネル
                    var panel = Instantiate(this.touchPanel, this.touchPanelParent);
                    panel.transform.localPosition = new Vector3(startPos.x + (j * this.slideValue), startPos.y + (i * this.slideValue), 0.0f);
                    panel.Index = index;
                    panel.PlayerUnitInfo = null;
                    panel.EnemyUnitInfo = null;
                    panel.gameObject.SetActive(true);
                    panel.PanelType = j == FieldLength.horizontal - 1 ? PanelType.NearCastle : PanelType.Normal;

                    //// 3Dパネル
                    var cube = Instantiate(this.normalCube, this.panelParent_3D);
                    cube.transform.localPosition = new Vector3(this.fieldStartPos.x + (j * this.fieldSliedeValue.x), 0.0f, this.fieldStartPos.z + (i * this.fieldSliedeValue.z));
                    cube.Index = index;
                    cube.gameObject.name = index.ToString();
                    cube.PanelType = j == FieldLength.horizontal - 1 ? PanelType.NearCastle : PanelType.Normal;
                    this.cubeList.Add(index, cube);

                    //// 各GameObjectの名前にそのパネルのIndexをつけて行く
                    panel.gameObject.name = index.ToString();
                    if (this.panelList.ContainsKey(index) == false)
                    {
                        this.panelList.Add(index, panel);
                    }

                    //// 最後にindexをカウントアップ
                    index++;
                }
            }
        }

        /// <summary>
        /// タッチ開始
        /// </summary>
        void OnDragStart()
        {
            Debug.Log("===== OnDragStart =====");

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider == null)
            {
                return;
            }

            var index = Int32.Parse(hit.collider.gameObject.name);

            if(IsSummon)
            {
                SummonUnit(SummonUnitId, index);
            }

            //// ユニットがパネルの上に乗っていない状態なら処理を行わない
            if (this.panelList[index].PlayerUnitInfo == null)
            {
                return;
            }

            /// タッチの状態をスタートに
            this.touchState = TouchState.Start;

            //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
            if (hit.collider != null)
            {
                this.draggingList.Clear();
                this.draggingList.Add(this.panelList[index]);
            }
        }

        void OnDragging()
        {
            //// 状態をタッチ中に
            this.touchState = TouchState.Dragging;

            //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                var index = Int32.Parse(hit.collider.gameObject.name);

                //// まだなぞってないパネルだったらリストに追加
                if (this.draggingList.Contains(this.panelList[index]) == false)
                {
                    this.draggingList.Add(this.panelList[index]);
                }
            }
        }

        void OnDragEnd()
        {
            Debug.Log("===== OnDragEnd =====");

            //// エンド状態に
            this.touchState = TouchState.End;

            //// 移動できるパネルが2つ以上あるのであれば移動開始
            if (this.draggingList.Count() > 1)
            {
                //// タッチでなぞったリストから同じIndexの3Dパネルのリストを抽出して送る
                var moveList = new List<NormalCube>();

                for (var i = 0; i < this.draggingList.Count;i++)
                {
                    moveList.Add(this.cubeList[this.draggingList[i].Index]);
                }
                this.draggingList.First().PlayerUnitInfo.ExecuteMove(moveList);
                this.touchState = TouchState.Wait;
            }
            else
            {
                this.touchState = TouchState.Wait;
            }
        }

        /// <summary>
        /// 召喚メソッド
        /// </summary>
        /// <param name="unitId">どのキャラクターを召喚するか.</param>
        /// <param name="panelIndex">どこに召喚するか.</param>
        void SummonUnit(int unitId,int panelIndex)
        {
            UnitManager.instance.playerUnitList[unitId].Transform.localPosition = cubeList[panelIndex].transform.localPosition;
            UnitManager.instance.playerUnitList[unitId].Transform.gameObject.SetActive(true);

            //// ユニットの状態を生きている状態に
            UnitManager.instance.playerUnitList[unitId].UnitInfo.UnitState = UnitState.Alive;

            //// 召喚するパネルにユニットを召喚する
            FieldManager.instance.panelList[panelIndex].PlayerUnitInfo = UnitManager.instance.playerUnitList[unitId];

            IsSummon = false;
            SummonUnitId = -1;
        }
    }
}
