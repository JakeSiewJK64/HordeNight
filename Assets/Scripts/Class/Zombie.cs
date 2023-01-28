public class Zombie : CharacterClass
{
    public float speed { get; set; }
    public ZombieType zombieType { get; set; }
    public Zombie(float speed, float health, float damage, ZombieType zombieType) : base(health, damage)
    {
        this.speed = speed;
        this.zombieType = zombieType;
    }
}
