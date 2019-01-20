using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchingRoom
{
    public class MatchingRoomScene : MonoBehaviour
    {
        public void OnClickDecideButton()
        {
            SceneManager.LoadScene(SceneConstant.SetPosition);
        }
    }
}