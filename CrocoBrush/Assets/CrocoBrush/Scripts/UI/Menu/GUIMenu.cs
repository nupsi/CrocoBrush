namespace CrocoBrush.UI.Menu
{
    public abstract class GUIMenu : RegisteredBehaviour
    {
        protected override string EventName => "ResetSettings";

        protected override abstract void UpdateComponent();
    }
}