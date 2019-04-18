using UnityEngine;

namespace CrocoBrush
{
    public class SpacebarInput : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                if(Spacebar.Instance.Visible)
                {
                    Spacebar.Instance.ClearSpace();
                }
                else
                {
                    Crocodile.Instance.AddScore(Quality.Bad);
                }
            }
        }
    }
}