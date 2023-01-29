using System;

public class UpgradeModule
{
    private int level { get; set; }
    private float value { get; set; }
    private float cost { get; set; }
    private int maxLevel { get; set; }

    public UpgradeModule() {
        Reset();
        maxLevel = 10;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public void Reset()
    {
        level = 1;
        value = 1;
        cost = 500;
    }

    public void Upgrade()
    {
        level++;
        value += value * .5f;
        cost *= 2;
    }
 
    public int GetLevel() { return level; }
    public float GetCost () { return cost; }

    public double GetValue() { return Math.Round(value / 10, 2); }
}
