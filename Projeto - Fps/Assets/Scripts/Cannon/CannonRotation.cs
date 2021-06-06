using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
   [SerializeField] private InteractiveItem _interactiveItem = default;
   [SerializeField] private RotationData _cannonRotationSettings = default;
   
   private float _verticalSpeedRotation = default;
   private float _horizontalSpeedRotation = default;

   private float _xAngleRotationMin = default;
   private float _xAngleRotationMax = default;
   
   private float _yAngleRotationMin = default;
   private float _yAngleRotationMax = default;
   
   private Vector2 _startingRotation = default;

   public static Quaternion finalPosCannon = default;

   private void Awake()
   {
      InitializeSettings();
   }
   
   private void Start()
   {
      GameManager.Instance.InputManager.OnAnyKeyPressed += RotateCannon;
   }

   private void OnDisable()
   {
      GameManager.Instance.InputManager.OnAnyKeyPressed -= RotateCannon;
   }

   private void InitializeSettings()
   {
      _verticalSpeedRotation = _cannonRotationSettings.VerticalSpeed;
      _horizontalSpeedRotation = _cannonRotationSettings.HorizontalSpeed;

      _xAngleRotationMin = _cannonRotationSettings.HorizontalAngleMin;
      _xAngleRotationMax = _cannonRotationSettings.HorizontalAngleMax;
   
      _yAngleRotationMin = _cannonRotationSettings.VerticalAngleMin;
      _yAngleRotationMax =  _cannonRotationSettings.VerticalAngleMax;
   }

   private void RotateCannon(InputsData inputsData)
   {
      if (_interactiveItem.OnCannon)
      {
         Vector2 _mouseDirection = inputsData.MouseDirection;
         
         _startingRotation.x += _mouseDirection.x * _verticalSpeedRotation * Time.deltaTime;
         _startingRotation.y += _mouseDirection.y * _horizontalSpeedRotation * Time.deltaTime;
         
         _startingRotation.y = Mathf.Clamp(_startingRotation.y,
            -_yAngleRotationMin, _yAngleRotationMax);
                
                
         _startingRotation.x = Mathf.Clamp(_startingRotation.x,
            -_xAngleRotationMin, _xAngleRotationMax);
        
         if (_mouseDirection != Vector2.zero)
         {
            
            transform.localRotation = Quaternion.Euler(-_startingRotation.y, 
               _startingRotation.x, 0f);
            
            finalPosCannon = transform.localRotation;
            
         }
      }
   }
}
