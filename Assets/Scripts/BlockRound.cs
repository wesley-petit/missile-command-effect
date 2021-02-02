using UnityEngine;

// Block round only in tutorials
public class BlockRound : MonoBehaviour
{
    private void Start() => RoundSystem.Instance.gameObject.SetActive(false);
}
