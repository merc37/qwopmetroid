using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New ConsumeItem", menuName = "Scriptable Objects/Lists/ConsumeItems")]
public class ItemsToConsumeList : ScriptableObject
{
   //public List<ConsummableItem> itemsToConsume = new List<ConsummableItem>();

    public FloatReference playerHealth;
    public FloatReference playerSpeed;

    public bool ConsumeItem(ConsummableItem itemToConsume)
    {
        if(itemToConsume.effectType == EffectType.InstantEffect)
        {
            if(itemToConsume.comsumableType == ConsumableTypes.healthRelated)
            {
                if(playerHealth.Variable.Value < 10)
                {
                    playerHealth.Variable.Value += itemToConsume.bonousHealth;
                    Debug.Log(playerHealth.Variable.Value);
                    return true;
                }
            }
        }
        return false;
    }


}
