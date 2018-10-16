using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Weapon", menuName = "ScriptableObjects/weapon", order = 0)]
public class WeaponType : ScriptableObject {

    public FloatReference Damage;
    public int ColliderType;

    public FloatReference AttackRangeX;
    public FloatReference AttackRangeY;
    public FloatReference AttackRadius;

    public float angleOfBox;

    public FloatReference timeBtwAttack;

}

