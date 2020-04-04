using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCircle : MonoBehaviour
{
    public Image imgCircle; // 사용할 이미지

    public float totalTime = 2.0f; // 게이지가 차는데 걸리는 시간
    bool gvrStatus; // 레티클 반응 여부
    float gvrTimer;

    public int distanceOfRay = 10;
    RaycastHit hit;

    CanvasGroup[] UI;
    SceneManage sm;
    BoxCollider[] bc_0;
    BoxCollider[] bc_1;
    BoxCollider[] bc_2;

    private void Start()
    {
        sm = new SceneManage();

        UI = new CanvasGroup[3] { GameObject.Find("Step1").GetComponent<CanvasGroup>(), 
            GameObject.Find("Step2").GetComponent<CanvasGroup>(),
            GameObject.Find("Step3").GetComponent<CanvasGroup>() };

        bc_0 = UI[0].gameObject.GetComponentsInChildren<BoxCollider>();
        bc_1 = UI[1].gameObject.GetComponentsInChildren<BoxCollider>();
        bc_2 = UI[2].gameObject.GetComponentsInChildren<BoxCollider>();


    }

    void Update()
    {
        // 서클 이미지 채우기
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(ray, out hit, distanceOfRay))
        {
            if(imgCircle.fillAmount == 1)
            {
                if (hit.transform.CompareTag("Btn_Login"))
                {
                    UI[0].alpha = 0;
                    UI[0].blocksRaycasts = false;

                    foreach(BoxCollider collider in bc_0)
                    {
                        collider.enabled = false;
                    }

                    UI[1].alpha = 1;
                    UI[1].blocksRaycasts = true;
                }
                else if (hit.transform.CompareTag("Btn_Main"))
                {
                    UI[1].alpha = 0;
                    UI[1].blocksRaycasts = false;

                    foreach (BoxCollider collider in bc_1)
                    {
                        collider.enabled = false;
                    }

                    UI[2].alpha = 1;
                    UI[2].blocksRaycasts = true;
                }
                else if (hit.transform.CompareTag("Btn_Select"))
                {
                    sm.NextScene();
                }
                else if (hit.transform.CompareTag("Btn_Back"))
                {
                    if (hit.transform.parent.name.Contains("Step2"))
                    {
                        UI[1].alpha = 0;
                        UI[1].blocksRaycasts = false;

                        foreach (BoxCollider collider in bc_1)
                        {
                            collider.enabled = false;
                        }

                        UI[0].alpha = 1;
                        UI[0].blocksRaycasts = true;
                        foreach (BoxCollider collider in bc_0)
                        {
                            collider.enabled = true;
                        }

                        
                    }
                    else if(hit.transform.name.Contains("Step3"))
                    {
                        UI[2].alpha = 0;
                        UI[2].blocksRaycasts = false;

                        foreach (BoxCollider collider in bc_2)
                        {
                            collider.enabled = false;
                        }
                        foreach (BoxCollider collider in bc_1)
                        {
                            collider.enabled = true;
                        }

                        UI[1].alpha = 1;
                        UI[1].blocksRaycasts = true;
                    }
                }
                
                imgCircle.fillAmount = 0;
                gvrTimer = 0f;
            }
        }
        else
        {
            imgCircle.fillAmount = 0;
            gvrTimer = 0f;
        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
    }
}
