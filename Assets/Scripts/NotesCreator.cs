using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.Networking;

public class NotesCreator : MonoBehaviour
{
    // Образец
    public GameObject note_prefab;

    // Поля ввода
    public Text input_note_name;
    public Text input_note_text;
    public Text input_note_image_url;

    // Изображение
    public Texture2D note_image_file;

    // Создание заметки
    public void CreateNote()
    {
        StartCoroutine(RetrieveTextureFromWeb());
    }

    IEnumerator RetrieveTextureFromWeb()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(input_note_image_url.text))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                Debug.Log("Link: " + input_note_image_url.text);    
            }
            else
            {
                // Get downloaded texture once the web request completes
                var texture = DownloadHandlerTexture.GetContent(uwr);
                note_image_file = texture;
                Debug.Log("Image downloaded " + uwr);
                CreateImageTargetFromDownloadedTexture();
            }
        }
    }

    void CreateImageTargetFromDownloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            note_image_file,
            1f,
            input_note_name.text);
        // add the Default Observer Event Handler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        Debug.Log("Instant Image Target created " + mTarget.TargetName);

        GameObject note = GameObject.Find(input_note_name.text);

        Instantiate(note_prefab, note.transform);
    }
}
