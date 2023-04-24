using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    public SphereCollider AttackRangeCollider;
    [SerializeField] private LineRenderer LineRenderer;
    private Tower _tower;

    // Start is called before the first frame update
    void Start()
    {
        AttackRangeCollider = GetComponent<SphereCollider>();
        LineRenderer = GetComponent<LineRenderer>();
        _tower = GetComponentInParent<Tower>();

        // Draw a circle surrounding the tower
        DrawCircle(32, AttackRangeCollider.radius);
    }

    // Draw a circle surrounding the tower
    private void DrawCircle(int steps, float radius)
    {
        LineRenderer.positionCount = steps + 1;
        LineRenderer.widthMultiplier = 0.1f;
        LineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        LineRenderer.startColor = Color.red;
        LineRenderer.endColor = Color.red;
        LineRenderer.loop = false;
        
        Vector3[] positions = new Vector3[steps + 1];

        for (int i = 0; i <= steps; i++)
        {
            float angle = 2f * Mathf.PI * i / steps;
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;
            positions[i] = new Vector3(x, y, x);
        }

        LineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _tower.EnemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _tower.EnemiesInRange.Remove(other.gameObject);
        }
    }
}