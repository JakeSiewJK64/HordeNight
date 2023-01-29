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

    public void IncrementLevel()
    {
        level++;
    }

    public void IncrementCost()
    {
        cost *= 2;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public int GetLevel() { return level; }
    public int GetCost () { return level; }

    public float GetValue() { return value; }
}
