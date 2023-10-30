using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_ElementSkill : MonoBehaviour
{
    int time=0;
    bool isActive = false;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 100)
        {
            //보스 그로기 애니메이션
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (isActive && gameObject.CompareTag("Water"))
        {
            //데미지 여기에 추가
            //if보스가 패턴중일때
                time++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isActive && gameObject.CompareTag("Fire"))
        {
            //데미지랑 빨라짐
        }
        if(isActive && gameObject.CompareTag("Water"))
        {
            //함정과 닿으면 불 끄기
        }
    }

}
