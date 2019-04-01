namespace CrocoBrush.Animation
{
    public class CrocodileAnimator : StartEndAnimator
    {
        protected override void LevelStart() => m_animator.SetTrigger("Open");

        protected override void LevelEnd() => m_animator.SetTrigger("Close");
    }
}