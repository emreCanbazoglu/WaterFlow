using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Grid))]
[ExecuteInEditMode]
public class GridLevelController : MonoBehaviour
{
    private Grid _grid;
    public Grid Grid
    {
        get
        {
            if (_grid == null)
                _grid = GetComponent<Grid>();

            return _grid;
        }
    }

    [SerializeField]
    [HideInInspector]
    private int _level = 1;
    public int Level
    {
        get
        {
            return _level;
        }
    }

    private int _targetLevel;

    [SerializeField]
    private float _levelChangeDuration;

    private Tween _levelChangeTween;

    public bool IsLevelChanging { get; private set; }

    #region
    public Action<GridLevelController> OnLevelUpdated { get; set; }
    #endregion

    private void Awake()
    {
        _targetLevel = _level;

        UpdateLevel();
    }

    public void TryIncreaseLevel(int level)
    {
        if (IsLevelChanging)
            return;

        _targetLevel = _level + level;

        if (_targetLevel < 1)
            _targetLevel = 1;

        UpdateLevel();

        OnLevelUpdated?.Invoke(this);
    }

    private void UpdateLevel()
    {
        Vector3 newScale = transform.localScale;

        newScale.y = _targetLevel;

        IsLevelChanging = true;

        if (Application.isPlaying)
        {
            if (_levelChangeTween != null)
                _levelChangeTween.Kill();

            _levelChangeTween = transform.DOScale(
                newScale,
                _levelChangeDuration)
                .SetEase(Ease.OutExpo)
                .SetUpdate(UpdateType.Normal)
                .OnUpdate(OnLevelUpdating)
                .OnComplete(OnLevelUpdateCompleted);
        }
        else
        {
            OnLevelUpdateCompleted();
            transform.localScale = newScale;
        }
    }

    private void OnLevelUpdating()
    {
        
    }

    private void OnLevelUpdateCompleted()
    {
        _level = _targetLevel;

        IsLevelChanging = false;
    }
}
