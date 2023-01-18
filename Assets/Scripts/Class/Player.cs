public class Player : CharacterClass
{
    public float Stamina { get; set; }

    public Player(float health, float stamina, float damage) : base(health, damage)
    {
        Stamina = stamina;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
