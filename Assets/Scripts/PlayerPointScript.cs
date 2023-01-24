using UnityEngine;

public class PlayerPointScript : MonoBehaviour
{
    private float points;

    void Start()
    {
        points = 0;    
    }

    public void IncrementPoints(float amount)
    {
        points += amount;
    }

    public float GetPoints()
    {
        return points;
    }
}
