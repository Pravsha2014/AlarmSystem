using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    public UnityAction<bool> StateChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            StateChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            StateChanged?.Invoke(false);
        }
    }
}
