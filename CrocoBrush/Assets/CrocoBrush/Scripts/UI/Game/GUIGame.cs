namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// Base class for in game UI components that require updated when the event manager receives a UpdateGameUI request.
    /// </summary>
    public abstract class GUIGame : RegisteredBehaviour
    {
        /// <summary>
        /// Event name to listen to
        /// </summary>
        /// <value>Event name to listen to.</value>
        protected override string EventName => "UpdateGameUI";

        protected override abstract void UpdateComponent();
    }
}