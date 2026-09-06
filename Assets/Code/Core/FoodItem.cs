using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private FoodIdentity _foodIdentity;
    [SerializeField] private Collider _hexagonCollider;

    public FoodIdentity FoodItemIdentity => _foodIdentity;

    public void ActivateCollider(bool value)
    {
        _hexagonCollider.enabled = value;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void MoveToLocal(Vector3 targetLocalPos)
    {
        LeanTween.cancel(gameObject);

        float delay =
            transform.GetSiblingIndex() * 0.01f;

        LeanTween.moveLocal(
                gameObject,
                targetLocalPos,
                0.1f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(delay);

        Vector3 direction =
            (targetLocalPos -
             transform.localPosition)
            .With(y: 0)
            .normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            Vector3 rotationAxis =
                Vector3.Cross(
                    Vector3.up,
                    direction);

            LeanTween.rotateAround(
                    gameObject,
                    rotationAxis,
                    360f,
                    0.05f)
                .setEase(
                    LeanTweenType.easeInOutSine)
                .setDelay(delay);
        }
    }

    public void Vanish(float delay)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(
                gameObject,
                Vector3.zero,
                0.2f)
            .setEase(LeanTweenType.easeInBack)
            .setDelay(delay)
            .setOnComplete(
                () => Destroy(gameObject));
    }

    public enum FoodIdentity
    {
        Bread,
        Donut,
        Cake,
        Cupcake,
        Pretzel,
        Baguette
    }
}