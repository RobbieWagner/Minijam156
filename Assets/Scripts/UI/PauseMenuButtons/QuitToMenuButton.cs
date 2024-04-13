using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RobbieWagnerGames.Common
{
    public class QuitToMenuButton : MenuButton
    {
        public override IEnumerator SelectButton(Menu menu)
        {
            yield return new WaitForSecondsRealtime(.01f);
            SceneManager.LoadScene("MainMenu");
        }
    }
}