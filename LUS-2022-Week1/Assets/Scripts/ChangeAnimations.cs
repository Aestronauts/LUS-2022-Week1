using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAnimations : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public TMPro.TMP_Dropdown animDropdown;

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {

        if (animDropdown.value == 0)
        {
            animator.Play("cancan");

        }

        if (animDropdown.value == 1)
        {
            animator.Play("chickendance");

        }

        if (animDropdown.value == 2)
        {
            animator.Play("sillydance");

        }

        if (animDropdown.value == 3)
        {
            animator.Play("tpose");

        }


    }
}
