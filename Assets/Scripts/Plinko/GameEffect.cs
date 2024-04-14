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
public class AddBounce : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        DropperManager.Instance.bounceMat.bounciness = magnitude;
    }
}

[Serializable]
public class AddCurrencyMultiplier : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        StaticGameStats.currencyMultiplier = magnitude;
    }
}

[Serializable]
public class AddCurrency: GameEffect
{
    [SerializeField] public ScoreType currencyType;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        StaticGameStats.EffectScore(currencyType, (int) magnitude);
    }
}

[Serializable]
public class AddRow : GameEffect
{
    [SerializeField] private DropperSegment rowPrefab;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperManager.Instance.unlockedSegments.Add(rowPrefab);
    }
}

[Serializable]
public class AddDropperLength : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperManager.Instance.MiddleSegmentCount++;
    }
}

[Serializable]
public class UpgradeFloatTime : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.maxGlideTime = magnitude;
        DropperBall.Instance.GlideTime = DropperBall.Instance.maxGlideTime;
    }
}

[Serializable]
public class UpgradeFlapPower : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.flapForce = magnitude;
    }
}

[Serializable]
public class UpgradeFloatSpeed : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.glideInputSpeed = magnitude;
    }
}

[Serializable]
public class UpgradeMovementPower : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperBall.Instance.inputSpeed = magnitude;
    }
}

[Serializable]
public class UpgradePegStrength : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        StaticGameStats.bumpLimit = (int)magnitude;
    }
}

[Serializable]
public class UpgradeEventChance : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameManager.Instance.eventChance = (int)magnitude;
    }
}

[Serializable]
public class UpgradeSpecialChance : GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperManager.Instance.specialRowChance = (int)magnitude;
    }
}