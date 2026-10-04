using UnityEngine;
using UnityEngine.AI;

public class BaseAI<T> : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;

    protected T _state;

    protected void SetState(T state) => _state = state;
}