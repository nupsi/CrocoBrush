using System.Collections.Generic;

namespace CrocoBrush.UI.Game
{
    /// <summary>
    /// Base class for in game UI components that require updated when the event manager receives a UpdateGameUI request.
    /// </summary>
    public abstract class GUIGame : RegisteredBehaviour
    {
        /// <summary>
        /// Update the component.
        /// This should be called when something in the game updates.
        /// </summary>
        protected abstract void UpdateComponent();

        /// <summary>
        /// Reset the component.
        /// This shoul be called when the game start or is restarted.
        /// </summary>
        protected abstract void ResetComponent();

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "UpdateGameUI", UpdateComponent },
                { "ResetGame", ResetComponent }
            });
    }
}