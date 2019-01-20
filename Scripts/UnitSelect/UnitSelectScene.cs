using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Battle;

namespace UnitSelect
{
    public class UnitSelectScene : MonoBehaviour
    {
        void Start()
        {
            Init();
        }

        void Init()
        {
            var unitInfos = Resources.LoadAll<UnitInfo>("Data");

            AllUnitSelectView.instance.ShowUnits(unitInfos);
        }

        public void OnClickDecideButton()
        {
            SceneManager.LoadScene(SceneConstant.MatchingRoom);
        }
    }
}
