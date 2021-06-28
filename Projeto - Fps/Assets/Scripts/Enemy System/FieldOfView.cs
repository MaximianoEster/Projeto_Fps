using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;


public class FieldOfView : MonoBehaviour
{
   public delegate void FovHandler();
   public event FovHandler OnObjDetectable;
   public event FovHandler OnObjUndetectable;
   
   [SerializeField] private float _radius = default;
   [SerializeField] private float _angle = default;
   [SerializeField] private float _height = default;
   [SerializeField] private int _scanFrequency = default;
   [SerializeField] private LayerMask _targetMask = default;
   [SerializeField] private LayerMask _obstructionMask = default;
   [SerializeField] private LayerMask _layers = default;
   [SerializeField] private Transform _sensorPosition = default;

   
   private List<GameObject> _objects = new List<GameObject>();
   private Collider[] _colliders = new Collider[1];
   private int _count = default;
   private float _scanInterval = default;
   private float _scanTimer = default;
   private bool _detectable = false;
   private int _maxCollider = 1;
   
   
   private void Start()
   {
      Initialize();
   }
   
   public void Scan()
   {
      Collider[] hitColliders = new Collider[_maxCollider];
      int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _radius, hitColliders, _targetMask);

      if (numColliders != 0)
      {
         GameObject col = hitColliders[0].gameObject;

         if (IsInSight(col))
         {
            if (_detectable == false)
            {
               OnObjDetectable?.Invoke();
               _detectable = true;
            }
         }
         else
         {
            if (_detectable)
            {
               OnObjUndetectable?.Invoke();
               _detectable = false;
            }
         }
      }
   }
   
   private void Initialize()
   {
      _scanInterval = 1f / _scanFrequency;
   }
   
   public void Timer()
   {
      _scanTimer -= Time.deltaTime;
      if (_scanTimer < 0)
      {
         _scanTimer += _scanInterval;
         Scan();
      }
   }
   
   private bool IsInSight(GameObject obj)
   {
      Vector3 origin = transform.position;
      Vector3 dest = obj.transform.position;
      Vector3 direction = dest - origin;
      
      direction.y = 0;
      float deltaAngle = Vector3.Angle(direction, transform.forward);

      float distance = Vector3.Distance(dest, transform.position);
      if (distance > _radius)
      {
         return false;
      }
      
      if (deltaAngle > _angle)
      {
         return false;
      }

      origin.y += _height / 2;
      dest.y = origin.y;

      if (Physics.Linecast(origin, dest, _obstructionMask))
      {
         return false;
      }
      return true;
   }

   //Remove latter
   private void OnDrawGizmos()
   {
      Gizmos.DrawWireSphere(transform.position,_radius );
      
      Gizmos.color = Color.green;
      foreach (var obj in _objects)
      {
         Gizmos.DrawSphere(obj.transform.position, 2);
      }
   }
}
