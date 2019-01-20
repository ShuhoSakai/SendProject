using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using UnityEngine.UI;

namespace UnitSelect
{
    public class UnitNode : MonoBehaviour
    {
        [SerializeField]
        Text atk;

        [SerializeField]
        Text intellect;

        /// <summary>
        /// キャラ画像
        /// </summary>
        [SerializeField]
        Image chrImg;

        public UnitInfo Info { get; set; }

        public SelectState State;

        public void Show(UnitInfo info, System.Action buttonEvent = null)
        {
            atk.text = info.Atk.ToString();
            intellect.text = info.Intellect.ToString();

            Info = info;

            //// ボタンイベントの設定
            var button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (buttonEvent != null)
                {
                    buttonEvent();
                }
            });

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}