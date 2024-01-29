using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net.Http.Headers;
using Vuforia;
using UnityEngine.Networking;
using System.Threading;
using System;

public class NotesLoader : MonoBehaviour
{
    // Образец
    public GameObject note_prefab;

    // CSV-файл
    public TextAsset csv_file;

    // Поля заметки
    private string input_note_name;
    private string input_note_text;
    private string input_note_image_url;

    // Разделители
    private char line_separator = '\n';
    private char field_separator = ',';

    // Изображение
    public Texture2D note_image_file;

    // Визуализация загрузки
    private float load_counter;
    private float len;
    public Text loading_bar;
    public GameObject loading_bar_image;

    public void StartReadingData()
    {
        StartCoroutine(ReadData());
    }

    IEnumerator ReadData()
    {
        string[] records = csv_file.text.Split(line_separator);
        len = records.Length - 1;
        foreach (string record in records)
        {
            string[] fields = record.Split(field_separator);

            input_note_name = fields[0];
            input_note_text = fields[1];
            input_note_image_url = fields[2];

            yield return StartCoroutine(RetrieveTextureFromWeb());
        }
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

        note.GetComponentInChildren<NoteDataBuffer>().trigger = true;
        note.GetComponentInChildren<NoteDataBuffer>().note_name = input_note_name;
        note.GetComponentInChildren<NoteDataBuffer>().note_text = input_note_text;

        load_counter += 1;

        loading_bar.text = "Загрузка... (" + load_counter + "/" + len + ")";
        loading_bar_image.GetComponent<UnityEngine.UI.Image>().fillAmount = load_counter / len;
        
        if (load_counter >= len)
        {
            Invoke("HideLoadingBar", 1);
        }
    }

    private void HideLoadingBar()
    {
        GameObject.Find("Load Note - Panel").SetActive(false);
    }
}
