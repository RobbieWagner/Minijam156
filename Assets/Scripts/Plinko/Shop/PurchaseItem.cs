using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    [CreateAssetMenu(menuName = "Shop Item")]
    public class PurchaseItem : ScriptableObject
    {
        [SerializeField] public string _name;
        [SerializeField] public int cost;
        [SerializeField] public ScoreType currencyType;

        [SerializeReference] public List<GameEffect> effects;

        [ContextMenu(nameof(AddBounceEffect))] void AddBounceEffect(){effects.Add(new AddBounce());}
        [ContextMenu(nameof(AddCurrencyMultiplierEffect))] void AddCurrencyMultiplierEffect(){effects.Add(new AddCurrencyMultiplier());}
        [ContextMenu(nameof(AddCurrency))] void AddCurrency(){effects.Add(new AddCurrency());}
        [ContextMenu(nameof(AddRowEffect))] void AddRowEffect(){effects.Add(new AddRow());}
        [ContextMenu(nameof(AddDropperLength))] void AddDropperLength(){effects.Add(new AddDropperLength());}
        [ContextMenu(nameof(AddMovementUpgrade))] void AddMovementUpgrade(){effects.Add(new UpgradeMovementPower());}
        [ContextMenu(nameof(AddPegStrength))] void AddPegStrength(){effects.Add(new UpgradePegStrength());}

        //[ContextMenu(nameof(AddCurrencyEffect))] void AddCurrencyEffect(){effects.Add(new AddCurrency());}

        [ContextMenu(nameof(AddFloatTimeUpgrade))] void AddFloatTimeUpgrade(){effects.Add(new UpgradeFloatTime());}
        [ContextMenu(nameof(AddFloatSpeedUpgrade))] void AddFloatSpeedUpgrade(){effects.Add(new UpgradeFloatSpeed());}
        [ContextMenu(nameof(AddFlapUpgrade))] void AddFlapUpgrade(){effects.Add(new UpgradeFlapPower());}
        [ContextMenu(nameof(AddEventChance))] void AddEventChance(){effects.Add(new UpgradeEventChance());}
        [ContextMenu(nameof(AddSpecial))] void AddSpecial(){effects.Add(new UpgradeSpecialChance());}
        [ContextMenu(nameof(Clear))] void Clear(){effects.Clear();}

        public bool Buy()
        {
            if(StaticGameStats.GetCurrencyValue(currencyType) >= cost)
            {
                StaticGameStats.EffectScore(currencyType, -cost);
                ApplyEffects();
                return true;
            }
            return false;
        }

        public void ApplyEffects()
        {
            foreach(GameEffect effect in effects)
            {
                Debug.Log("apply effects");
                effect.ApplyPurchaseEffect();
            }
        } 
    }
}