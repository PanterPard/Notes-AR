using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net.Http.Headers;

public class NotesSaver : MonoBehaviour
{
    // CSV-����
    public TextAsset csv_file;

    // ���� �����
    public Text input_note_name;
    public Text input_note_text;
    public Text input_note_image_url;

    // �����������
    private char line_separator = '\n';
    private char field_separator = ',';

    public void SaveNote()
    {
        File.AppendAllText(getPath() + "/Data/NotesData.csv", line_separator + input_note_name.text + field_separator + input_note_text.text + field_separator + input_note_image_url.text);
        Debug.Log("������ ��������� � CSV-����.");
    }

    private static string getPath()
    {
        #if UNITY_EDITOR
            Debug.Log("��������: ���� �������.");
            return Application.dataPath;
        #elif UNITY_ANDROID
            Debug.Log("�������: ���� �������.");
            return Application.persistentDataPath;
        #else
            return Application.dataPath;
        #endif
    }
}
