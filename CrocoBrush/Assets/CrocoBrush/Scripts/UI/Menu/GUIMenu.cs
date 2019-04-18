using System.Collections.Generic;

namespace CrocoBrush.UI.Menu
{
    /// <summary>
    /// Base class for registered menu elements, that require reset to default function.
    /// </summary>
    public abstract class GUIMenu : RegisteredBehaviour
    {
        /// <summary>
        /// Reset to the default value of the component.
        /// </summary>
        protected abstract void ResetSettings();

        protected override Dictionary<string, Action> Actions =>
            m_actions ??
            (m_actions = new Dictionary<string, Action>
            {
                { "ResetSettings", ResetSettings }
            });
    }
}