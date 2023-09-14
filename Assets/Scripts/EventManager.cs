using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<int> OnHealthCount = new UnityEvent<int>();
    public static UnityEvent<Ufo> OnInfoPanel = new UnityEvent<Ufo>();

    public static void SendHealthCount(int healthCount)
    {
        OnHealthCount.Invoke(healthCount);
    }

    public static void SendUfoInfo(Ufo ufoData)
    {
        OnInfoPanel.Invoke(ufoData);
    }
}
