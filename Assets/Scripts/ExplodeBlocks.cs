using UnityEngine;

/*The control logic that was originally intended to achieve explosive effects has not yet been implemented
 TODO The logic behind the explosion effect
 */
public class ExplodeBlocks : MonoBehaviour
{
    public GameObject block;
    public void Explode()
    {
        Debug.Log("±¬Õ¨£¡");
        this.GetComponent<Renderer>().enabled = true;

    }
}
