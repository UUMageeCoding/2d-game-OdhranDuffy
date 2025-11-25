using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;   // Assign in Inspector (must NOT be children of the platform)
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _checkDistance = 0.05f;

    private int _currentWaypointIndex = 0;
    private int _direction = 1; // 1 = forward, -1 = backward

    void Start()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            Debug.LogError($"{name} has no waypoints assigned!");
        }
    }

    void Update()
    {
        if (_waypoints == null || _waypoints.Length == 0) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        // Move toward target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWaypoint.position,
            _speed * Time.deltaTime
        );

        // Switch waypoint when close enough
        if (Vector3.Distance(transform.position, targetWaypoint.position) < _checkDistance)
        {
            _currentWaypointIndex += _direction;

            // Reverse direction at ends
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = _waypoints.Length - 2;
                _direction = -1;
            }
            else if (_currentWaypointIndex < 0)
            {
                _currentWaypointIndex = 1;
                _direction = 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Stick player/object to platform
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Detach when leaving
        other.transform.SetParent(null);
    }
}



