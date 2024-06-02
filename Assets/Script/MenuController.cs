using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public RectTransform pointer; // Referensi ke pointer
    public Button[] buttons; // Array tombol
    private Text[] buttonTexts; // Array teks tombol

    private int currentIndex = 0; // Indeks tombol yang sedang dipilih

    void Start()
    {
        // Inisialisasi array teks tombol
        buttonTexts = new Text[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonTexts[i] = buttons[i].GetComponentInChildren<Text>();

            // Menambahkan event trigger untuk pointer enter dan exit
            AddEventTriggers(buttons[i], i);
        }

        // Inisialisasi pointer ke posisi tombol pertama
        UpdatePointerPosition();
    }

    void Update()
    {
        // Deteksi input tombol panah atas dan bawah
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePointerDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePointerUp();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Panggil fungsi onClick tombol saat tombol Enter ditekan
            buttons[currentIndex].onClick.Invoke();
        }
    }

    void MovePointerDown()
    {
        currentIndex++;
        if (currentIndex >= buttons.Length)
        {
            currentIndex = 0;
        }
        UpdatePointerPosition();
    }

    void MovePointerUp()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = buttons.Length - 1;
        }
        UpdatePointerPosition();
        
    }

    void UpdatePointerPosition()
    {
        // Pindahkan pointer ke posisi tombol yang sedang dipilih
        pointer.position = buttons[currentIndex].transform.position;
    }


    // Fungsi untuk menambahkan event trigger pada tombol
    void AddEventTriggers(Button button, int index)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Event trigger untuk pointer enter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnPointerEnter(index); });
        trigger.triggers.Add(entryEnter);

        // Event trigger untuk pointer exit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnPointerExit(index); });
        trigger.triggers.Add(entryExit);
    }

    // Fungsi yang dipanggil saat pointer masuk
    public void OnPointerEnter(int index)
    {
        currentIndex = index;
        UpdatePointerPosition();
    }

    // Fungsi yang dipanggil saat pointer keluar (jika diperlukan)
    public void OnPointerExit(int index)
    {
        // Tidak melakukan apa-apa saat pointer keluar
    }
}
