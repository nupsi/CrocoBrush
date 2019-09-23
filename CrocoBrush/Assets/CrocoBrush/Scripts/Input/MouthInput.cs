namespace CrocoBrush
{
    /// <summary>
    /// Turns User input to Mouth Press Directions.
    /// </summary>
    public class MouthInput : DirectionInputReader
    {
        protected override void PressDirection(Direction direction) => Mouth.Instance.PressDirection(direction);
    }
}