using System;
using System.Collections.Generic;
using com.ethnicthv.Script.Input;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.ethnicthv.Script.Menu
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelSelectionController : MonoBehaviour
    {
        public float levelSelectionWidth = 100f;

        private DragMenu _dragMenu;

        private List<RectTransform> _levelSelections;

        private RectTransform _rectTransform;
        private bool _isDragging;

        private float _currentDrag;
        private float _dragMax;
        private float _dragMin;
        private int CurrentSelectionIndex => Mathf.RoundToInt(_currentDrag);

        private void Awake()
        {
            _dragMenu = new DragMenu();

            _rectTransform = (RectTransform)transform;

            _levelSelections = new List<RectTransform>();
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = (RectTransform)transform.GetChild(i);
                _levelSelections.Add(child);
                child.anchoredPosition = new Vector2(i * levelSelectionWidth, 0);
                if (i != 0 && !PlayerPrefs.HasKey("Level" + i))
                {
                    SetLevelDisable(child);
                }
            }
            
            _dragMax = -(_levelSelections.Count - 1) - 0.5f;
            _dragMin = 0.5f;
        }

        private void Start()
        {
            _dragMenu.Input.Pressed.started += OnDragStart;
            _dragMenu.Input.Pressed.canceled += OnDragEnd;
            _dragMenu.Input.Enable();
        }

        private void Update()
        {
            if (_isDragging)
            {
                var dragDelta = _dragMenu.Input.DragDelta.ReadValue<Vector2>().x;
                SetCurrentDrag(Mathf.Clamp(_currentDrag + dragDelta / levelSelectionWidth, _dragMax, _dragMin));
                _rectTransform.anchoredPosition = new Vector2(_currentDrag * levelSelectionWidth, 0);
            }
        }

        private void OnDestroy()
        {
            _dragMenu.Input.Pressed.started -= OnDragStart;
            _dragMenu.Input.Pressed.canceled -= OnDragEnd;
            _dragMenu.Input.Disable();
            _rectTransform.DOKill();
        }

        private void OnDragStart(InputAction.CallbackContext context)
        {
            Debug.Log("Drag start");
            _isDragging = true;
            _rectTransform.DOKill();
        }

        private void OnDragEnd(InputAction.CallbackContext context)
        {
            Debug.Log("Drag end");
            _isDragging = false;
            var targetPosition = CurrentSelectionIndex * levelSelectionWidth;
            _rectTransform.DOAnchorPosX(targetPosition, 0.5f).SetEase(Ease.OutCirc)
                .OnUpdate(() => SetCurrentDrag(_rectTransform.anchoredPosition.x / levelSelectionWidth));
        }
        
        private void OnCurrentSelectionChanged()
        {
            
        }

        public void OnSelectLevel(int level)
        {
            Debug.Log("Selected level: " + level);
            SceneManager.LoadScene(level);
        }
        
        private void SetCurrentDrag(float value)
        {
            _currentDrag = value;
            OnCurrentSelectionChanged();
        }

        private static void SetLevelDisable(Transform level)
        {
            var button = level.GetComponent<Button>();
            var image1 = level.GetChild(0).GetComponent<Image>();
            var image2 = level.GetChild(0).GetChild(0).GetComponent<Image>();
            button.interactable = false;
            image1.color = Color.gray;
            image2.color = Color.gray;
        }
    }
}