public class Zombie : CharacterClass
{
    public float speed { get; set; }
    public Zombie(float speed, float health, float damage) : base(health, damage)
    {
        this.speed = speed;
    }
}
