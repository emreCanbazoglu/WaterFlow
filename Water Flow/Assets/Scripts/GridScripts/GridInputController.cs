using UnityEngine;

[RequireComponent(typeof(PressableBehaviour))]
public class GridInputController : MonoBehaviour
{
    private PressableBehaviour _pressableBehaviour;
    public PressableBehaviour PressableBehaviour
    {
        get
        {
            if (_pressableBehaviour == null)
                _pressableBehaviour = GetComponent<PressableBehaviour>();

            return _pressableBehaviour;
        }
    }

    private GridLevelController _levelController;
    public GridLevelController LevelController
    {
        get
        {
            if (_levelController == null)
                _levelController = GetComponent<GridLevelController>();

            return _levelController;
        }
    }

    private void Awake()
    {
        RegisterToPressableBehaviour();

        PressableBehaviour.StartCheckingPressed();
    }

    private void OnDestroy()
    {
        PressableBehaviour.StopCheckingPressed();

        UnregisterFromPressableBehaviour();
    }

    private void RegisterToPressableBehaviour()
    {
        PressableBehaviour.OnPressed += OnPressed;
    }

    private void UnregisterFromPressableBehaviour()
    {
        PressableBehaviour.OnPressed -= OnPressed;
    }

    private void OnPressed(PressableBehaviour obj)
    {
        Debug.Log("Buraya mi geliyor");
        LevelController.TryIncreaseLevel(-1);
    }
}
