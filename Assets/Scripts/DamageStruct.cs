using System;

[Serializable]
public struct DamageStruct
{
    public float damage;
    public DamageType damageType;
    public bool isCritical;
    public bool isBlockable;

    public DamageStruct(float damage, DamageType damageType, bool isCritical, bool isBlockable)
    {
        this.damage = damage;
        this.damageType = damageType;
        this.isCritical = isCritical;
        this.isBlockable = isBlockable;
    }

    public DamageStruct(float damage)
    {
        this.damage = damage;
        this.damageType = DamageType.Physical;
        this.isCritical = false;
        this.isBlockable = true;
    }
}

public enum DamageType
{
    Physical,
    Magical
}

public enum DamageableTargetType
{
    Player,
    Enemy,
    Destructible
}