using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class Upgrades : SingletonBase<Upgrades>
{
    public const string filename = "upgrade.dat";
    [Serializable]
    private class UpgradeSave
    {
        public UpgradeAsset Asset;
        public int Level = 0;
    }

    [SerializeField] private UpgradeSave[] _saves;

    protected override void Awake()
    {
        base.Awake();
        Saver<UpgradeSave[]>.TryLoad(filename, ref _saves);
    }

    public static void BuyUpgrade(UpgradeAsset asset)
    {
        foreach (var upgrade in Instance._saves)
        {
            if (upgrade.Asset == asset && GetUpgradeLevel(asset) < asset.MaxLevel)
            {
                upgrade.Level += 1;
                Saver<UpgradeSave[]>.Save(filename, Instance._saves);
            }
        }
    }

    public static int GetUpgradeLevel(UpgradeAsset asset)
    {
        print("Get upgrade level");
        foreach (var upgrade in Instance._saves)
        {
            if (upgrade.Asset == asset)
            {
                return upgrade.Level;
            }
        }

        return 0;
    }

    public static int GetTotalCost()
    {
        int result = 0;
        foreach (var upgrade in Instance._saves)
        {
            for (int i = 0; i < upgrade.Level; i++)
            {
                result += upgrade.Asset.CostByLevel[i];
            }
        }

        return result;
    }
}
