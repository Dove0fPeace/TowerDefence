using UnityEngine;

public enum TargetLayer
{
    Air,
    Earth,
    Both
}

[CreateAssetMenu]
public class TowerAsset : ScriptableObject
{
        public TargetLayer Type;
        public int GoldCost = 15; 
        public Sprite GUISprite;
        public Sprite TowerSprite;
        public TurretProperties TurretProperties;
        public TowerAsset[] UpgradeTo;
}
