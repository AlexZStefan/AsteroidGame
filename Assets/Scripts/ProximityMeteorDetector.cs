using UnityEngine;

public class ProximityMeteorDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask asteroidLayer;
    [SerializeField] private SuperMisile superMisile;

    private Transform targetAsteroid;
    private bool detected = false;
    private float timerUntilSelfDetonate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            detected = true;
        }
    }

    private void Update()
    {
        timerUntilSelfDetonate += Time.deltaTime;

        if (detected) DetectClosestAsteroid();
        else if (timerUntilSelfDetonate > 10)
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }

    // this can probably be improved 
    private void DetectClosestAsteroid()
    {
        // check targe not destroyed 
        if (targetAsteroid != null && !targetAsteroid.gameObject.activeInHierarchy) {
            targetAsteroid = null;
        }

        // check distance and go towards asteroid 
        if (targetAsteroid != null && Vector2.Distance(targetAsteroid.position, transform.parent.parent.TransformPoint(transform.localPosition)) < detectionRadius * 4)
        {
            superMisile.ApplyForce(targetAsteroid.position);
            superMisile.AdjustDamping();
        }
        else if (targetAsteroid == null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, asteroidLayer);
            float closestDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (Collider2D hit in hits)
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = hit.transform;
                }
            }
            targetAsteroid = closest;
        }
        else Destroy(transform.parent.parent.gameObject);

        // destroy if no asteroid detected by physics
        if (targetAsteroid == null) Destroy(transform.parent.parent.gameObject);
    }
}
