using System;
using System.Collections;
using System.Collections.Generic;
using RobbieWagnerGames.Plinko;
using UnityEngine;

[Serializable]
public class GameEffect
{
    [SerializeField] protected float magnitude;
    [SerializeField] protected Color associatedColor = Color.black;
    [SerializeField] public Sprite icon;

    public virtual void ApplyPurchaseEffect()
    {
        
    }

    public Color GetColor()
    {
        return associatedColor;
    }
}

[Serializable]
public class AddBounce: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        //TODO: StaticGameStats.mutantMaterial.bounciness = magnitude;
    }
}

[Serializable]
public class AddCurrencyMultiplier: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        StaticGameStats.currencyMultiplier = magnitude;
    }
}

// [Serializable]
// public class AddCurrency: GameEffect
// {
//     [SerializeField] public ScoreType currencyType;

//     public override void ApplyPurchaseEffect()
//     {
//         base.ApplyPurchaseEffect();
//         StaticGameStats.EffectScore(currencyType);
//     }
// }

[Serializable]
public class AddRow: GameEffect
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private float height;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        //DropperGame.Instance.AddRow(rowPrefab, height);
    }
}

[Serializable]
public class UpgradeFloatTime: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.maxGlideTime = magnitude;
        DropperBall.Instance.GlideTime = DropperBall.Instance.maxGlideTime;
    }
}

[Serializable]
public class UpgradeFloatSpeed: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.glideInputSpeed = magnitude;
    }
}

[Serializable]
public class UpgradeMovementPower: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.inputSpeed = magnitude;
    }
}

[Serializable]
public class UpgradeBasketReward: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        //GameStats.Instance.currencyAddOnEffect = (int) magnitude;
    }
}

[Serializable]
public class AddMass: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        //GameManager.Instance.ball.rb2d.mass = magnitude;
    }
}
