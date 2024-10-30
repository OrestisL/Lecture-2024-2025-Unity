using UnityEngine;

public class SetupClock : MonoBehaviour
{
    public GameObject ClockBase;
    public float Radius;

    public GameObject TimeIndicatorPrefab;

    private void Start()
    {
        //there are 12 time indicators on a clock

        for (int i = 0; i < 12; i++)
        {
            Vector3 currentPos = PointOnCircle(ClockBase.transform.position, Radius, i);
            GameObject currentIdicator = Instantiate(TimeIndicatorPrefab);
            currentIdicator.transform.localPosition = currentPos;

            float angle = 2 * Mathf.PI * Mathf.Rad2Deg * (float)i / 12 - 90;
            currentIdicator.transform.rotation =
                Quaternion.AngleAxis(angle, currentIdicator.transform.forward);
        }
    }

    Vector3 PointOnCircle(Vector3 center, float radius, int index)
    {
        Vector3 position = new();
        position.x = center.x + radius * Mathf.Cos(2 * Mathf.PI * (float)index / 12);
        position.z = 0;
        position.y = center.z + radius * Mathf.Sin(2 * Mathf.PI * (float)index / 12);
        return position;
    }
}
