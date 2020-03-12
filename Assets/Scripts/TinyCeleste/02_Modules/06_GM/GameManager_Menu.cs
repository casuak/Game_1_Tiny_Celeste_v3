using System;
using TinyCeleste._01_Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyCeleste._02_Modules._06_GM
{
    public class GameManager_Menu : EntitySingleton<GameManager_Menu>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Enter Game");
                SceneManager.LoadScene("Scenes/Main");
            }
        }
    }
}