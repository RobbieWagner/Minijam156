using System.Collections;
using System.Collections.Generic;
using RobbieWagnerGames.Managers;
using UnityEngine;
using RobbieWagnerGames.Plinko;

namespace RobbieWagnerGames.Common
{
    public class ResumeGameButton : MenuButton
    {
        public override IEnumerator SelectButton(Menu menu)
        {
            yield return new WaitForSecondsRealtime(.01f);
            GameManager.Instance.ResumeGame();
        }
    }
}

