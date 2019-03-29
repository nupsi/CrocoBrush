namespace CrocoBrush.UI.Game
{
    public abstract class GUIGame : RegisteredBehaviour
    {
        protected override string EventName => "UpdateGameUI";

        protected override abstract void UpdateComponent();
    }
}