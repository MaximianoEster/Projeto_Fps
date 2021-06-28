using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer = default;
    [SerializeField] private int _linePoints = default;
    
    private bool _showLine = false;
    private Vector3 _speed = default;
    private Vector3 _finalSpeed = default;
    private Vector3 _initialPos = default;

    public void CalculateTrajectory(Transform spawnPoint, Transform weaponRotation , float power)
    {
        _speed = new Vector3(weaponRotation.localRotation.y,-weaponRotation.localRotation.x,
            -weaponRotation.localRotation.x) * power;
        
        if (_showLine)
        {
            _lineRenderer.enabled = true;
           ShowTrajectory(spawnPoint.position, _speed);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
    
    private void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[_linePoints];
        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * (time * time) / 2;
        }
        
        _lineRenderer.SetPositions(points);
    }
    
    public Vector3 Speed => _speed;

    public bool ShowLine
    {
        get => _showLine;
        set => _showLine = value;
    }
}
