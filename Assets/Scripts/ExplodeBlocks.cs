using UnityEngine;

/*ԭ��������ըЧ���Ŀ����߼� ��ûʵ��
 TODO ��ըЧ�����߼�
 */
public class ExplodeBlocks : MonoBehaviour
{
    public GameObject block;
    // ������ըЧ��
    public void Explode()
    {
        Debug.Log("��ը��");
        this.GetComponent<Renderer>().enabled = true;

    }
}
