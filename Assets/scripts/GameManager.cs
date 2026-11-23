using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum SessionState { NotStarted, Running, Won, Lost }

    public static GameManager Instance { get; private set; }

    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private PuzzleProgress puzzleProgress;
    [SerializeField] private UnityEvent onGameStarted = new UnityEvent();
    [SerializeField] private UnityEvent onGameWon = new UnityEvent();
    [SerializeField] private UnityEvent onGameLost = new UnityEvent();

    public SessionState State { get; private set; } = SessionState.NotStarted;
    public PuzzleProgress Progress => puzzleProgress;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Only one GameManager should be active in a scene.", this);
            enabled = false;
            return;
        }

        Instance = this;

        if (countdownTimer == null)
            countdownTimer = FindFirstObjectByType<CountdownTimer>();

        if (puzzleProgress == null)
            puzzleProgress = FindFirstObjectByType<PuzzleProgress>();

        if (countdownTimer == null)
            Debug.LogWarning("GameManager has no CountdownTimer assigned.", this);

        if (puzzleProgress == null)
            Debug.LogWarning("GameManager has no PuzzleProgress assigned.", this);
    }

    private void OnEnable()
    {
        if (countdownTimer != null)
            countdownTimer.Finished += LoseGame;
    }

    private void OnDisable()
    {
        if (countdownTimer != null)
            countdownTimer.Finished -= LoseGame;

        if (Instance == this)
            Instance = null;
    }

    public void StartGame()
    {
        if (State == SessionState.Running)
            return;

        State = SessionState.Running;
        countdownTimer?.StartTimer();
        onGameStarted.Invoke();
    }

    public void WinGame()
    {
        if (State != SessionState.Running)
            return;

        State = SessionState.Won;
        countdownTimer?.StopTimer();
        onGameWon.Invoke();
    }

    public void LoseGame()
    {
        if (State != SessionState.Running)
            return;

        State = SessionState.Lost;
        countdownTimer?.StopTimer();
        onGameLost.Invoke();
    }
}
