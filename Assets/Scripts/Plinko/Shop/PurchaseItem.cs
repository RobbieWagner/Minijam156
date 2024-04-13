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
        [ContextMenu(nameof(Mass))] void Mass(){effects.Add(new AddMass());}
        [ContextMenu(nameof(AddCurrencyMultiplierEffect))] void AddCurrencyMultiplierEffect(){effects.Add(new AddCurrencyMultiplier());}
        [ContextMenu(nameof(AddBasketUpgrade))] void AddBasketUpgrade(){effects.Add(new UpgradeBasketReward());}
        [ContextMenu(nameof(AddRowEffect))] void AddRowEffect(){effects.Add(new AddRow());}

        //[ContextMenu(nameof(AddCurrencyEffect))] void AddCurrencyEffect(){effects.Add(new AddCurrency());}

        [ContextMenu(nameof(AddFloatTimeUpgrade))] void AddFloatTimeUpgrade(){effects.Add(new UpgradeFloatTime());}
        [ContextMenu(nameof(AddFloatSpeedUpgrade))] void AddFloatSpeedUpgrade(){effects.Add(new UpgradeFloatSpeed());}
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
                effect.ApplyPurchaseEffect();
            }
        } 
    }
}