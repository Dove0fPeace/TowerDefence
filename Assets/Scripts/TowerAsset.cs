using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class TowerAsset : ScriptableObject
    {
        public int GoldCost = 15; 
        public Sprite GUISprite;
        public Sprite TowerSprite;
        public TurretProperties TurretProperties;
    }
