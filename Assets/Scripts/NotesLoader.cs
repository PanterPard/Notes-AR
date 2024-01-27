using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net.Http.Headers;
using Vuforia;
using UnityEngine.Networking;
using JetBrains.Annotations;

public class NotesLoader : MonoBehaviour
{
    // Образец
    public GameObject note_prefab;

    // CSV-файл
    public TextAsset csv_file;

    // Поля ввода
    public string input_note_name;
    public string input_note_text;
    public string input_note_image_url;

    // Разделители
    private char line_separator = '\n';
    private char field_separator = ',';

    // Изображение
    public Texture2D note_image_file;

    public void ReadData()
    {
        string[] records = csv_file.text.Split(line_separator);
        for (int i = 1; i < records.Length - 1; i++)
        {
            {
                Debug.Log("Строки: " + records);

                string record = records[i];

                string[] fields = record.Split(field_separator);
                foreach (string field in fields)
                {
                    if (fields.Length == 3)
                    {
                        input_note_name = fields[0];
                        input_note_text = fields[1];
                        input_note_image_url = fields[2];
                    }
                }
                LoadNotes();
            }
        }
    }

    public void LoadNotes()
    {
        StartCoroutine(RetrieveTextureFromWeb());
    }

    IEnumerator RetrieveTextureFromWeb()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(input_note_image_url))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                Debug.Log("Link: " + input_note_image_url);
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
            input_note_name);
        // add the Default Observer Event Handler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        Debug.Log("Instant Image Target created " + mTarget.TargetName);

        GameObject note = GameObject.Find(input_note_name);

        Instantiate(note_prefab, note.transform);
    }
}
