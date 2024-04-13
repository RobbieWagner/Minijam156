using System.Collections;
using System.Collections.Generic;
using RobbieWagnerGames.Managers;
using UnityEngine;

namespace RobbieWagnerGames.Common
{
    public class ResumeGameButton : MenuButton
    {
        public override IEnumerator SelectButton(Menu menu)
        {
            yield return new WaitForSecondsRealtime(.01f);
            UIManager.Instance.ResumeGame();
        }
    }
}

