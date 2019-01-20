using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class NormalCube : MonoBehaviour
    {
        [SerializeField]
        public GameObject SmokeParticle;

        /// <summary>
        /// 値を見るためよう,Inspectorでいじることはない
        /// </summary>
        [SerializeField]
        int index;

        public PanelType PanelType;

        /// <summary>
        /// どこにぱねるが　配置されてるかのための変数
        /// </summary>
        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }
    }
}