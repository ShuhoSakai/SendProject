using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Const;
using Battle;

namespace SetPosition
{
    public class FieldManager : SingletonMonoBehavior<FieldManager>
    {
        /// <summary>
        /// パネル TODO インターフェース継承させるよー
        /// </summary>
        [SerializeField]
        GameObject touchPanel;

        /// <summary>
        /// 縦横にずらす量
        /// </summary>
        [SerializeField]
        float slideValue = 0.25f;

        [SerializeField]
        Vector3 fieldStartPos;

        [SerializeField]
        Vector3 fieldSliedeValue;

        [SerializeField]
        Vector2 startPos;

        [SerializeField]
        Transform panelParent_3D;

        [SerializeField]
        Transform touchPanelParent;

        [SerializeField]
        NormalCube normalCube;

        public void CreateField()
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
                    panel.gameObject.SetActive(true);

                    //// 3Dパネル
                    var cube = Instantiate(this.normalCube, this.panelParent_3D);
                    cube.transform.localPosition = new Vector3(this.fieldStartPos.x + (j * this.fieldSliedeValue.x), 0.0f, this.fieldStartPos.z + (i * this.fieldSliedeValue.z));
                    cube.Index = index;
                    cube.gameObject.name = index.ToString();
                    cube.PanelType = j == FieldLength.horizontal - 1 ? PanelType.NearCastle : PanelType.Normal;

                    //// 最後にindexをカウントアップ
                    index++;
                }
            }
        }
    }
}