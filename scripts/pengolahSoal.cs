using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pengolahSoal : MonoBehaviour
{
    public TextAsset assetSoal;

    public string[] soal;

    private string[,] soalBag;

    int indexSoal;
    int maxSoal;
    bool ambilSoal;
    public char kunciJawaban;

    bool[] soalSelesai;
    //Komponen UI
    public Text txtSoal, txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;

    bool cekJawaban;
    bool isHasil;
    private float durasi;
    public float durasiPenilaian;
    

    // Start is called before the first frame update
    void Start()
    {
        durasi = durasiPenilaian;
        soal = assetSoal.ToString().Split('#');

        soalSelesai = new bool[soal.Length];

        soalBag = new string[soal.Length, 6];
        maxSoal = soal.Length;
        OlahSoal();

        ambilSoal = true;
        TampilkanSoal();
    }

    // Untuk melakukan pengolahan terhadap soal
    private void OlahSoal()
    {
        for (int i = 0; i < soal.Length; i++)
        {
            string[] tempSoal = soal[i].Split('+');
            for (int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i, j] = tempSoal[j];
                continue;
            }
            continue;
        }
    }

    // Untuk menampilkan soal dan menginisialisasi Soal dan jawaban - jawabannya ( dimasukan ke dalam Aray )
    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                for (int i = 0; i < soal.Length; i++)
                {
                    int randomIndexSoal = Random.Range(0, soal.Length);
                    if (!soalSelesai[randomIndexSoal])
                    {
                        txtSoal.text = soalBag[randomIndexSoal, 0];
                        txtOpsiA.text = soalBag[randomIndexSoal, 1];
                        txtOpsiB.text = soalBag[randomIndexSoal, 2];
                        txtOpsiC.text = soalBag[randomIndexSoal, 3];
                        txtOpsiD.text = soalBag[randomIndexSoal, 4];
                        char v = soalBag[randomIndexSoal, 5][0];
                        kunciJawaban = v;

                        soalSelesai[randomIndexSoal] = true;
                        ambilSoal = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
    }

    public GameObject panel;

    // Untuk menginisialisasi jawaban-jawaban di pilihan Ganda
    public void Opsi(string opsiHuruf)
    {
        checkJawaban(opsiHuruf[0]);

        if (isHasil)
        {
            // Do Nothing
        }
        else
        {
            panel.SetActive(true);
        }
        indexSoal++;
        ambilSoal = true;
    }

    public Text txtPenilaian;

    // Untuk melakukan pengecheckan jawaban
    private void checkJawaban(char huruf)
    {
        string penilaian;
        if (huruf.Equals(kunciJawaban))
        {
            panel.SetActive(false);
            penilaian = "";
            isHasil = true;
            FindObjectOfType<GameControl>().changeState(GameState.BoardGame);
        }
        else
        {
            panel.SetActive(true);
            penilaian = "Jawaban anda salah, Anda tidak dapat mengocok dadu!";
            isHasil = false;
            FindObjectOfType<Dice>().changeTurn();
        }
        txtPenilaian.text = penilaian;
    }

    // Untuk memunculkan panel soal
    void Update()
    {
        if (durasi>0)
        {
            durasiPenilaian -= Time.deltaTime;

            if (durasiPenilaian <= 0)
            {
                panel.SetActive(false);
                durasiPenilaian = durasi;

                TampilkanSoal();
            }
        }
    }
}