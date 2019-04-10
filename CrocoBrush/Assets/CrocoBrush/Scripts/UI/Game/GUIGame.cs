namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// Base class for in game UI components that require updated when the event manager receives a UpdateGameUI request.
    /// </summary>
    public abstract class GUIGame : RegisteredBehaviour
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Instance.StartListening(ResetEvent, ResetComponent);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.Instance.StopListening(ResetEvent, ResetComponent);
        }

        protected override string EventName => "UpdateGameUI";
        protected string ResetEvent => "ResetGame";

        protected override abstract void UpdateComponent();
        protected abstract void ResetComponent();
    }
}