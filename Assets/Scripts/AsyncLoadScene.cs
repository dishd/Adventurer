using Common;
using ns;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

namespace LoadScene
{
    /// <summary>
    /// 
    /// </summary>
    public class AsyncLoadScene : MonoBehaviour
    {
        private UISlider loadingSlider;  //���س�����������B������ʹ��UGUIʵ��

        //public Text loadingText;  //��ʾ���ؽ��� %

        private float loadingSpeed = 3;  //�����ٶȣ������ǽ������Ķ�ȡ�ٶ�

        private float targetValue;  //������Ŀ���ֵ/�첽���ؽ��ȵ�ֵ

        private AsyncOperation asyncLoad;  //�����첽���ص�����

        // Use this for initialization  
        void Start()
        {
            loadingSlider = transform.FindChildCompentByName<UISlider>("Bar_Back");
            loadingSlider.value = 0.0f;  //��ʼ��������

            //if (SceneManager.GetActiveScene().name == "B")  //�����ǰ�������B
            //{
            //    //����Э��  
            //    StartCoroutine(AsyncLoading());  //����Э�̽����첽����
            //}
            StartCoroutine(AsyncLoading());  //����Э�̽����첽����

        }

        IEnumerator AsyncLoading()
        {

            asyncLoad = SceneManager.LoadSceneAsync(Global.LoadSceneName);
            //��ֹ����������Զ��л�
            asyncLoad.allowSceneActivation = false;

            if (Global.Contain3DScene == true)
            {
                 SceneManager.LoadSceneAsync(Global.LoadUIName, LoadSceneMode.Additive);
            }
            yield return asyncLoad;
        } // Update is called once per frame 


        //IEnumerator AsyncLoading()
        //{

        //    //asyncLoad = SceneManager.LoadSceneAsync(Global.LoadSceneName);
        //    StartCoroutine(Global.LoadSceneName);
        //    //��ֹ����������Զ��л�
        //    //asyncLoad.allowSceneActivation = false;

        //    if (Global.Contain3DScene == true)
        //    {
        //        asyncLoad = SceneManager.LoadSceneAsync(Global.LoadUIName, LoadSceneMode.Additive);
        //    }
        //    yield return asyncLoad;
        //} // Update is called once per frame 



        private void Update()
        {
            targetValue = asyncLoad.progress;
            if (asyncLoad.progress >= 0.9f)
            {
                //progress��ֵ���Ϊ0.9 
                targetValue = 1.0f;
            }
            if (targetValue != loadingSlider.value)
            { //��ֵ���� 
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(loadingSlider.value - targetValue) < 0.1f)
                //�����ǰ������value��Ŀ��ֵ�ӽ� ���ý�����valueΪĿ��ֵ 
                { loadingSlider.value = targetValue; }
            }
            //loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
            if ((int)(loadingSlider.value) == 1)
           // ����������ȡ���ٷ�֮��ʱ�������л� 
            { //�����첽������Ϻ��Զ��л����� 

                asyncLoad.allowSceneActivation = true;

                if (Global.hideEtc)
                {
                    GameObject.FindGameObjectWithTag("EasyTouch").transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("EasyTouchControlsCanvas").SetActive(true);
                    Global.hideEtc = false;
                }

            }
        }


        IEnumerator loginMy()
        {
            int displayProgress = 0;
            int toProgress = 0;
            AsyncOperation op = SceneManager.LoadSceneAsync(Global.LoadSceneName);
            if (Global.Contain3DScene == true)
            {
                op = SceneManager.LoadSceneAsync(Global.LoadUIName, LoadSceneMode.Additive);
            }
            op.allowSceneActivation = false;
            while (op.progress < 0.9f) //�˴������ <= 0.9f ��������ѭ�����Ա���С0.9
            {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    SetLoadingPercentage(displayProgress);
                    yield return new WaitForEndOfFrame();//ui��Ⱦ���֮��
                }
            }
            toProgress = 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            op.allowSceneActivation = true;

        }

        private void SetLoadingPercentage(int displayProgress)
        {
            loadingSlider.value = displayProgress;
            //text.text = displayProgress.ToString() + "%";
        }
    }

}
