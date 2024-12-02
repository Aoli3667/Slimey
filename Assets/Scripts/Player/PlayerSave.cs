using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    private JsonData testData;

    public class JsonData
    {
        public PlayerData playerData;
    }

    public class PlayerData
    {
        public Vector2 position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SavePoint"))
        {
            InitJsonData();
            PlayerDataSave(collision.gameObject);
            JsonSave();
        }
        if (collision.gameObject.CompareTag("DeathPoint"))
        {
            PlayerDataLoad();
        }
    }

    private void InitJsonData()
    {
        testData = new JsonData();
        PlayerData data = new PlayerData();
        testData.playerData = data;
    }

    private string JsonPath()
    {
        return Path.Combine(Application.persistentDataPath, "JsonSaveTest.json");
    }

    private void JsonSave()
    {
        string path = JsonPath();

        // ����n�������ڣ������n���K�P�]��
        if (!File.Exists(path))
        {
            Debug.Log("File does not exist, creating...");
            using (File.Create(path))
            {
                // �n�������� using �^�K�Y���r�Ԅ��P�]
            }
        }

        // ���Y���D�Q�� JSON
        string json = JsonUtility.ToJson(testData, true);

        // ����n��
        File.WriteAllText(path, json);

        Debug.Log("Finished save at: " + path);
    }

    private void PlayerDataSave(GameObject savePoint)
    {
        testData.playerData.position = savePoint.transform.position;
    }

    private void PlayerDataLoad()
    {
        this.transform.position = testData.playerData.position;
        Debug.Log("Data load successful, player at position: " + testData.playerData.position);
    }
}
