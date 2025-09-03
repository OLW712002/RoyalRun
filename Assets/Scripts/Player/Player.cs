using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected const string runSpeedStringInAnimator = "RunSpeed";
    protected const string hitStringInAnimator = "Hit";

    public string GetRunSpeedString()
    {
        return runSpeedStringInAnimator;
    }

    public string GetHitString()
    {
        return hitStringInAnimator;
    }
}
