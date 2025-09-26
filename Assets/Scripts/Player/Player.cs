using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected const string runSpeedStringInAnimator = "RunSpeed";
    protected const string hitStringInAnimator = "Hit";

    protected const string defaultLayerString = "Default";
    protected const string invisibleLayerString = "Invisible";

    public string GetRunSpeedString()
    {
        return runSpeedStringInAnimator;
    }

    public string GetHitString()
    {
        return hitStringInAnimator;
    }
}
