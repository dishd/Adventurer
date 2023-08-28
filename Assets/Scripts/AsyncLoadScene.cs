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
        private UISlider loadingSlider;  //加载场景进度条，B场景中使用UGUI实现

        //public Text loadingText;  //显示加载进度 %

        private float loadingSpeed = 3;  //加载速度，这里是进度条的读取速度

        private float targetValue;  //进度条目标的值/异步加载进度的值

        private AsyncOperation asyncLoad;  //定义异步加载的引用

        // Use this for initialization  
        void Start()
        {
            loadingSlider = transform.FindChildCompentByName<UISlider>("Bar_Back");
            loadingSlider.value = 0.0f;  //初始化进度条

            //if (SceneManager.GetActiveScene().name == "B")  //如果当前活动场景是B
            //{
            //    //启动协程  
            //    StartCoroutine(AsyncLoading());  //开启协程进行异步加载
            //}
            StartCoroutine(AsyncLoading());  //开启协程进行异步加载

        }

        IEnumerator AsyncLoading()
        {

            asyncLoad = SceneManager.LoadSceneAsync(Global.LoadSceneName);
            //阻止当加载完成自动切换
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
        //    //阻止当加载完成自动切换
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
                //progress的值最大为0.9 
                targetValue = 1.0f;
            }
            if (targetValue != loadingSlider.value)
            { //插值运算 
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(loadingSlider.value - targetValue) < 0.1f)
                //如果当前进度条value和目标值接近 设置进度条value为目标值 
                { loadingSlider.value = targetValue; }
            }
            //loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
            if ((int)(loadingSlider.value) == 1)
           // 当进度条读取到百分之百时允许场景切换 
            { //允许异步加载完毕后自动切换场景 

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
            while (op.progress < 0.9f) //此处如果是 <= 0.9f 则会出现死循环所以必须小0.9
            {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    SetLoadingPercentage(displayProgress);
                    yield return new WaitForEndOfFrame();//ui渲染完成之后
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
