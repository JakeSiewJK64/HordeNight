public class Player : CharacterClass
{
    public float stamina { get; set; }
    public bool immune { get; set; }
    public bool dead { get; set; }
    public float healthRegeneration { get; set; }


    public Player(float health, float stamina, float damage, float healthRegeneration) : base(health, damage)
    {
        this.stamina = stamina;
        this.healthRegeneration = healthRegeneration;
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
        if(!immune)
        {
            health -= damage;
        }
    }

    public void GainHealth(float amount)
    {
        health += amount;
    }
}
