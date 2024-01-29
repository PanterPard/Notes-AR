using UnityEngine;
using System.IO;
using TMPro;

public class NotesSaver : MonoBehaviour
{
    // CSV-файл
    public TextAsset csv_file;

    // Поля ввода
    public TMP_InputField input_note_name;
    public TMP_InputField input_note_text;
    public TMP_InputField input_note_image_url;

    // Разделители
    private char line_separator = '\n';
    private char field_separator = ',';

    public void SaveNote()
    {
        File.AppendAllText(getPath() + "/Data/NotesData.csv", line_separator + input_note_name.text + field_separator + input_note_text.text + field_separator + input_note_image_url.text);
        Debug.Log("Данные сохранены в CSV-файл.");
    }

    private static string getPath()
    {
        #if UNITY_EDITOR
            Debug.Log("Редактор: путь получен.");
            return Application.dataPath;
        #elif UNITY_ANDROID
            Debug.Log("Андроид: путь получен.");
            return Application.persistentDataPath;
        #else
            return Application.dataPath;
        #endif
    }
}
