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
    private int previousExp;

    private SkinnedMeshRenderer facePrev;
    private SkinnedMeshRenderer bodyPrev;



    // Start is called before the first frame update
    void Start()
    {
        previousExp = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentExp = expDropdown.value;


        if(expDropdown.value == 0)
        {
            if(previousExp != -1)
            {
                face.SetBlendShapeWeight(previousExp, 0);
                body.SetBlendShapeWeight(previousExp, 0);
            }

            face.SetBlendShapeWeight(0, 100);
            body.SetBlendShapeWeight(0, 100);
            previousExp = 0;

        }

        if (expDropdown.value == 1)
        {
            if (previousExp != -1)
            {
                face.SetBlendShapeWeight(previousExp, 0);
                body.SetBlendShapeWeight(previousExp, 0);
            }

            face.SetBlendShapeWeight(1, 100);
            body.SetBlendShapeWeight(1, 100);
            previousExp = 1;
        }

        if (expDropdown.value == 2)
        {
            if (previousExp != -1)
            {
                face.SetBlendShapeWeight(previousExp, 0);
                body.SetBlendShapeWeight(previousExp, 0);
            }

            face.SetBlendShapeWeight(2, 100);
            body.SetBlendShapeWeight(2, 100);
            previousExp = 2;
        }

        if (expDropdown.value == 3)
        {
            if (previousExp != -1)
            {
                face.SetBlendShapeWeight(previousExp, 0);
                body.SetBlendShapeWeight(previousExp, 0);
            }

            face.SetBlendShapeWeight(3, 100);
            body.SetBlendShapeWeight(3, 100);
            previousExp = 3;
        }

        if (expDropdown.value == 4)
        {
            if (previousExp != -1)
            {
                face.SetBlendShapeWeight(previousExp, 0);
                body.SetBlendShapeWeight(previousExp, 0);
            }

            face.SetBlendShapeWeight(4, 100);
            body.SetBlendShapeWeight(4, 100);
            previousExp = 4;

        }

        /*
        if(currentExp != previousExp)
        {

            for (int i = 0; i < 6; i++)
            {
                face.SetBlendShapeWeight(i, 0);
            }
        }
        */
    }
}
