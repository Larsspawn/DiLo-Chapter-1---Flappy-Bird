using UnityEngine;

public class SetBoolWrapper : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetBoolGoingDown(bool value)
    {
        anim.SetBool("GoingDown", value);
    }
}
