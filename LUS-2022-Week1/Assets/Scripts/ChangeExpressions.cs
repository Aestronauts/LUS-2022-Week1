using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeExpressions : MonoBehaviour
{
    [SerializeField]
    public SkinnedMeshRenderer face;
    [SerializeField]
    public SkinnedMeshRenderer body;
    [SerializeField]
    public TMPro.TMP_Dropdown expDropdown;

    private int currentExp;



    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        currentExp = expDropdown.value;


        if(expDropdown.value == 0)
        {
            face.SetBlendShapeWeight(0, 100);
            body.SetBlendShapeWeight(0, 100);
            currentExp = 0;

        }

        if (expDropdown.value == 1)
        {
            face.SetBlendShapeWeight(1, 100);
            body.SetBlendShapeWeight(1, 100);
            currentExp = 1;
        }

        if (expDropdown.value == 2)
        {
            face.SetBlendShapeWeight(2, 100);
            body.SetBlendShapeWeight(2, 100);
            currentExp = 2;
        }

        if (expDropdown.value == 3)
        {
            face.SetBlendShapeWeight(3, 100);
            body.SetBlendShapeWeight(3, 100);
            currentExp = 3;
        }

        if (expDropdown.value == 4)
        {
            face.SetBlendShapeWeight(4, 100);
            body.SetBlendShapeWeight(4, 100);
            currentExp = 4;

        }



    }
}
