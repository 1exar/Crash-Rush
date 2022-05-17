using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Fire();

    public abstract void StartAimation();

    public abstract void StopAnimation();
}