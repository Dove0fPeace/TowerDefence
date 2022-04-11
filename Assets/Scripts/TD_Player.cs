using System;
using UnityEngine;

public class TD_Player : Player
{
    public static new TD_Player Instance
    { get
        {
            return Player.Instance as TD_Player;
        }
    }

    private static event Action<int> OnGoldUpdate;

    public static void GoldUpdateSubscribe(Action<int> act)
    {
        OnGoldUpdate += act;
        act(Instance.CurrentGold);
    }

    public static void GOldUpdate_Unsubscribe(Action<int> act)
    {
        OnGoldUpdate -= act;
    }
    public static event Action<int> OnLifeUpdate;
    public static void HealthUpdateSubscribe(Action<int> act)
    {
        OnLifeUpdate += act;
        act(Instance.NumLives);
    }

    [Header("TD")]
    [SerializeField] private int m_Gold;

    public int CurrentGold => m_Gold;

    [SerializeField] private Tower m_TowerPrefab;

    [Space(2)] 
    [SerializeField] private UpgradeAsset m_HealthUpgrade;

    [SerializeField] private UpgradeAsset m_GoldUpgrade;

    protected override void Awake()
    {
        base.Awake();
        ConfirmUpgrades();
    }

    public void ChangeGold(int gold)
    {
        m_Gold += gold;
        OnGoldUpdate(CurrentGold);
    }

    public void ChangeLife(int change)
    {
        TakeDamage(change);
        OnLifeUpdate(NumLives);
    }

    public void TryBuild(TowerAsset towerAsset, Transform buildSite)
    {
        ChangeGold(-towerAsset.GoldCost);
        var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
        tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.TowerSprite;
        tower.SetTurret(towerAsset.TurretProperties);
        tower.SetType(towerAsset.Type);
        Destroy(buildSite.gameObject);
    }

    private void ConfirmUpgrades()
    {
        m_NumLives += Upgrades.GetUpgradeLevel(m_HealthUpgrade) * 5;
        m_Gold += Upgrades.GetUpgradeLevel(m_GoldUpgrade) * 10;
    }
    
}