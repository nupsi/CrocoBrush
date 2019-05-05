using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CrocoBrush.Tutorial
{
    /// <summary>
    /// Displays how food acts over time.
    /// </summary>
    public class FoodQuality : Tooth
    {
        [SerializeField]
        private GameObject m_prefab;

        [SerializeField]
        private TextMeshPro m_text;

        private Food m_food;

        private Quality m_quality;

        private void Awake()
        {
            var food = Instantiate(m_prefab);
            food.transform.SetParent(transform);
            m_food = food.GetComponent<Food>();
            m_text.SetText(m_food.Quality.ToString());
            StartLoop();
        }

        private void Update()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            if(!m_food.gameObject.activeInHierarchy)
            {
                StartLoop();
            }

            if(m_food.Quality != m_quality)
            {
                m_text.SetText(m_food.Quality.ToString());
                m_quality = m_food.Quality;
            }
        }

        private void StartLoop()
        {
            PlaceFood(m_food, 1);
        }

        public override void Remove() => m_food.Hide();
    }
}