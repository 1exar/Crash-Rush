using UnityEngine;

public class HealEntity : Entity
{

    [SerializeField] private int _healEffect;
    public int HealEffect
    {
        get { return _healEffect; }
        private set{}
    }

}