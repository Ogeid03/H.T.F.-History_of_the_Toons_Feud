using UnityEngine;

public class ChildComponent : MonoBehaviour
{
    private int livello;

    public void SetLevel(int level)
    {
        livello = level;
        Debug.Log("Livello impostato a: " + livello);
    }
}