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
    private Vector3 _speedFinal = default;

    public void CalculateTrajectory(Transform spawnPoint, Transform weaponRotation , float power)
    {
        _speedFinal = new Vector3(weaponRotation.localRotation.y,-weaponRotation.localRotation.x,
                          -weaponRotation.localRotation.x) * power;
        
        if (_showLine)
        {
            _lineRenderer.enabled = _showLine;
           ShowTrajectory(spawnPoint.position, _speedFinal);
        }
        else
        {
            _lineRenderer.enabled = _showLine;
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
    
    public Vector3 SpeedFinal => _speedFinal;

    public bool ShowLine
    {
        get => _showLine;
        set => _showLine = value;
    }
}
