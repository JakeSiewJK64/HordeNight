public class Player : CharacterClass
{
    public float stamina { get; set; }

    public Player(float health, float stamina, float damage) : base(health, damage)
    {
        this.stamina = stamina;
    }

    public void ReduceStamina(float amount)
    {
        stamina -= amount;
    }
    
    public void GainStamina(float amount)
    {
        stamina += amount;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
