using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingController : MonoBehaviour
{
    public GameObject[] settingOptions; // Array untuk opsi menu
    public RectTransform pointer; // RectTransform dari pointer
    private int currentIndex = 0; // Indeks opsi menu saat ini

    void Start()
    {
        UpdatePointerPosition(); // Memperbarui posisi pointer saat start

        // Menambahkan event listener untuk setiap opsi menu
        for (int i = 0; i < settingOptions.Length; i++)
        {
            int index = i; // Capturing the current value of i
            Button button = settingOptions[i].GetComponent<Button>();
            EventTrigger trigger = settingOptions[i].AddComponent<EventTrigger>();

            // Event saat mouse mengarahkan ke opsi menu
            EventTrigger.Entry entryEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            entryEnter.callback.AddListener((eventData) => { OnPointerEnter(index); });
            trigger.triggers.Add(entryEnter);

            // Event saat mouse mengklik opsi menu
            EventTrigger.Entry entryClick = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            entryClick.callback.AddListener((eventData) => { OnPointerClick(index); });
            trigger.triggers.Add(entryClick);
        }
    }

    void Update()
    {
        // Navigasi menu menggunakan tombol panah
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % settingOptions.Length; // Pindah ke opsi berikutnya
            UpdatePointerPosition(); // Memperbarui posisi pointer
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + settingOptions.Length) % settingOptions.Length; // Pindah ke opsi sebelumnya
            UpdatePointerPosition(); // Memperbarui posisi pointer
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption(); // Pilih opsi menu
        }
    }

    void UpdatePointerPosition()
    {
        // Ambil posisi RectTransform dari opsi menu saat ini
        RectTransform currentOption = settingOptions[currentIndex].GetComponent<RectTransform>();
        // Update posisi pointer berdasarkan posisi opsi menu saat ini
        pointer.position = new Vector3(pointer.position.x, currentOption.position.y, pointer.position.z);
    }

    void OnPointerEnter(int index)
    {
        currentIndex = index; // Perbarui indeks opsi menu saat ini
        UpdatePointerPosition(); // Memperbarui posisi pointer
    }

    void OnPointerClick(int index)
    {
        currentIndex = index; // Perbarui indeks opsi menu saat ini
        SelectOption(); // Pilih opsi menu
    }

    void SelectOption()
    {
        // Aksi yang dilakukan saat opsi dipilih
        switch (settingOptions[currentIndex].name)
        {
            case "Suara":
                Debug.Log("Start Game");
                // SceneManager.LoadScene("MainGameScene"); // Uncomment dan ganti dengan nama scene Anda
                break;
            case "Kontrol":
                Debug.Log("Open Settings");
                break;
        }
    }
}
