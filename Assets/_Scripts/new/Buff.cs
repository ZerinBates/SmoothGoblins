using UnityEngine;

// Base class for buffs
public abstract class Buff : MonoBehaviour
{
    // A flag to track whether the buff has expired
    protected TriggerEvent trigger {get;}
    private bool hasExpired;
    int rounds = 1;
    public TriggerEvent GetTrigger()
    {
        return trigger;
    }
    // Called when the buff is applied
    public virtual void OnApply()
    {
        // Reset the expired flag
        hasExpired = false;
    }

    // Called when the buff is updated
    public virtual void OnUpdate(TriggerEvent t,UnitBasic u)
    {
        if (t == trigger)
        {
            rounds--;
            if (rounds == 0)
            {
                Expire();
            }
        }
        // Do something here when the buff is updated
    }

    // Called when the buff expires
    public virtual void OnExpire()
    {
        // Do something here when the buff expires
    }

    // Expire the buff
    public void Expire()
    {
        if (!hasExpired)
        {
            hasExpired = true;
            OnExpire();
            Destroy(gameObject);
        }
    }
}

