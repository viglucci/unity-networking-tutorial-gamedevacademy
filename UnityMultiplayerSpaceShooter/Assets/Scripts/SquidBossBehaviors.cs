using System.Collections;
using UnityEngine;

public class SquidBossBehaviors : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Escape = Animator.StringToHash("Escape");

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void EscapeAfter(int timeSeconds)
    {
        StartCoroutine(CoEscapeAfter(timeSeconds));
    }

    private IEnumerator CoEscapeAfter(int timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
        _animator.SetTrigger(Escape);
    }
}