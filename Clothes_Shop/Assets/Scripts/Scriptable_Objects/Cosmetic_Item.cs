using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cosmetic", menuName = "ScriptableObjects/CosmeticScriptableObject", order = 1)]
public class Cosmetic_Item : ScriptableObject
{
    public string nameItem;
    public Sprite icon;
    public AnimatorOverrideController animator;
    public int price;
    public string type;
}
