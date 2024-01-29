using UnityEngine;
using System.IO;
using TMPro;

public class NotesSaver : MonoBehaviour
{
    // CSV-����
    public TextAsset csv_file;

    // ���� �����
    public TMP_InputField input_note_name;
    public TMP_InputField input_note_text;
    public TMP_InputField input_note_image_url;

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
