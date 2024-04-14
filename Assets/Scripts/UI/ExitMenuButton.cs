using System.Collections;
using UnityEngine;

namespace RobbieWagnerGames.Common
{
    public class ExitMenuButton : MenuButton
    {
        public override IEnumerator SelectButton(Menu menu)
        {
            yield return StartCoroutine(base.SelectButton(menu));
        }
    }
}