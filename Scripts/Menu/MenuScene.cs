using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class MenuScene : MonoBehaviour
    {
        public void OnClickLeagueButton()
        {
            SceneManager.LoadScene(SceneConstant.UnitSelect);
        }
    }
}
