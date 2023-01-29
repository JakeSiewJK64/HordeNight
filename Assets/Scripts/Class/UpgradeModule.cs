public class UpgradeModule
{
    public int level { get; set; }
    public float value { get; set; }
    public float cost { get; set; }

    public UpgradeModule() {
        level = 1;
        value = 1;
        cost = 500;
    }  

    public void Upgrade()
    {
        level++;
        value += value * .25f;
        cost *= 2;
    }
 
    public int GetLevel() { return level; }
    public int GetCost () { return level; }

    public float GetValue() { return value; }
}
