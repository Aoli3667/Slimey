using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // �ӵ��ٶ�
    public float lifetime = 3f; // �ӵ����ʱ��
    public TagHandle tag;
    public Vector2 dir;
    void Start()
    {
        

        // ��һ��ʱ��������ӵ�
        Destroy(gameObject, lifetime);
    }

    public void Init(float InSpeed = 5f, float Inlifetime = 3f, string InTagName = "Player", Vector2? InDir = null)
    {
        speed = InSpeed;
        lifetime = Inlifetime;
        tag = TagHandle.GetExistingTag(InTagName);
        dir = InDir ?? Vector2.right;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �ӵ���������ǰ�������ƶ�
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tag.ToString()) return;

        Debug.Log(tag.ToString() + " bullet hit" + other );
        if (other.tag == "Player")
        {
            EventHandler.CallBulletHitPlayerEvent(tag);
            Destroy(gameObject); // �ӵ����к�����
        }
        else if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            EventHandler.CallBulletHitEnemyEvent(tag);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
