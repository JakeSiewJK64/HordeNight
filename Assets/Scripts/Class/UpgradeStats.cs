public class UpgradeStats
{
    public UpgradeModule damage { get; set; }
    public UpgradeModule capacity { get; set; }
    public UpgradeModule fireRate { get; set; }
    public UpgradeModule reloadSpeed { get; set; }

    public UpgradeStats()
    {
        damage = new UpgradeModule();
        capacity = new UpgradeModule();
        fireRate = new UpgradeModule();
        reloadSpeed = new UpgradeModule();
    }

    public void ResetUpgrades()
    {
        damage.Reset();
        capacity.Reset();
        fireRate.Reset();
        reloadSpeed.Reset();
    }
}