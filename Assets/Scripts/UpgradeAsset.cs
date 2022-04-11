using UnityEngine;

[CreateAssetMenu]
public class UpgradeAsset : ScriptableObject
{
    [Header("Внешний вид")] 
    public Sprite Sprite;
    
    public int[] CostByLevel = {3};
    public int MaxLevel = 9;
}