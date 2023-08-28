using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// ²Î¿¼
    /// https://blog.csdn.net/lzhq1982/article/details/18793479
    /// </summary>
    public class TestNPC : MonoBehaviour
    {
        [HideInInspector]
        public GameObject _heroPanel;
        public UISlider _bloodSlider;
        public float height = 1.4f;
        public float x = -0.3f;
        private void Start()
        {
            GameObject heroPanel = Resources.Load("MonstarHP") as GameObject;
            _heroPanel = Instantiate(heroPanel, transform.position, transform.rotation) as GameObject;
            _heroPanel.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
            _bloodSlider = _heroPanel.GetComponentInChildren<UISlider>();
       
        }

        private void Update()
        {
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + height, transform.position.z);
            _heroPanel.transform.position = pos;
            _heroPanel.transform.rotation = Camera.main.transform.rotation;
        }

        public void SetSLider(float t)
        {
            _bloodSlider.value = t;
        }
    }

    

}
