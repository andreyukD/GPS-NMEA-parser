using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] txtNumbers = richTextBox1.Text.Split(','); //pobieram wartosci z sekwencji, jako separator wystepuje ,

            string gpr = "$GPRMC"; //szukam tej sekwencji (podstawowa)
            string gpgsa = "$GPGSA"; //sekwencja o satelitach na podstawie ktorych zostala znalieziona lokalizacja
            string gsv = "$GPGSV"; //sekwencja o satelitach (id itp)
            int tylkorazsatelita = 0; label15.Text = "";

            for (int i = 0; i < txtNumbers.Length; i++) //ide po komorkam z danymi i szukam sekwencji
            {
                if (txtNumbers[i].Contains(gpr))//jesli znalazlem sekwencje $GPRMC (podstawowa)
                {
                    label5.Text = "" + txtNumbers[i + 1].Substring(0, 2) + ":" + txtNumbers[i + 1].Substring(2, 2) + ":" + txtNumbers[i + 1].Substring(4, 2)+" UTC"; //wypisuje date
                    if (txtNumbers[i + 2].Equals("A")) { //jesli A - to status aktywny
                        label6.Text = "Status: Aktywny (A)";
                    }
                    else if (txtNumbers[i + 2].Equals("V"))  { label6.Text = "Status: Nieaktywny (V)"; } //jesli v to nieaktywny
                    else { label6.Text = ""; } //jesli nic to nic

                    if (txtNumbers[i + 3].Substring(0, 1).Equals("0")) //dalej okreslam szerokosc. - jesli na poczatku jest zero, to wycinam pierwsze 3 liczby. jesli nie ma zera, to pierwsze 2. i tam dalej zgodnie ze wzorem (w pdfie spojrz)
                    {
                        label7.Text = "Szerokość geograficzna: " + txtNumbers[i + 3].Substring(0, 3) + " deg " + txtNumbers[i + 3].Substring(3, txtNumbers[i + 3].Length - 3) + "'" + txtNumbers[i + 4];
                    }
                    else { label7.Text = "Szerokość geograficzna: " + txtNumbers[i + 3].Substring(0, 2) + " deg " + txtNumbers[i + 3].Substring(2, txtNumbers[i + 3].Length - 2) + "'" + txtNumbers[i + 4]; }
                    
                    if (txtNumbers[i + 5].Substring(0, 1).Equals("0")) {//tak samo jak i z szerokoscia
                        label8.Text = "Długość geograficzna: " + txtNumbers[i + 5].Substring(0, 3) + " deg " + txtNumbers[i + 5].Substring(3, txtNumbers[i + 5].Length - 3) + "'" + txtNumbers[i + 6];
                    }
                    else
                    {
                        label8.Text = "Długość geograficzna: " + txtNumbers[i + 5].Substring(0, 2) + " deg " + txtNumbers[i + 5].Substring(2, txtNumbers[i + 5].Length - 2) + "'" + txtNumbers[i + 6];
                    }
                    label9.Text = "Prędkość obiektu: " + txtNumbers[i + 7] + " (węzły)";
                    label10.Text = "Kąt poruszania się obiektu: " + txtNumbers[i + 8] + " (stopni)";
                    label11.Text = "Data: " + txtNumbers[i + 9].Substring(0, 2) + " " + txtNumbers[i + 9].Substring(2, 2) + " ";
                    int n = Convert.ToInt32(txtNumbers[i + 9].Substring(4, 2));//tu jak mowilem - jak ostatnie dwie liczby < 20 to bedzie rok 20xx. w innym przypadku 19xx
                    if (n < 20) { 
                    label11.Text += "20" + txtNumbers[i + 9].Substring(4, 2);
                    }
                    else {label11.Text += "19" + txtNumbers[i + 9].Substring(4, 2);}
                    label12.Text = "Odchylenie magnetyczne ziemi: " + txtNumbers[i + 10] + "," + txtNumbers[i + 11].Substring(0,1);

                }//$GPRMC - pozycja uzytkownika

                if (txtNumbers[i].Contains(gpgsa)) //teraz tak samo szukam innej sekwencji
                {
                    label1.Text = "" + txtNumbers[i + 3] + " " + txtNumbers[i + 4] + " " + txtNumbers[i + 5] + " " + txtNumbers[i + 6] + " " + txtNumbers[i + 7] + " " + txtNumbers[i + 8] + " " + txtNumbers[i + 9] + " " + txtNumbers[i + 10] + " " + txtNumbers[i + 11] + " " + txtNumbers[i + 12] + " ";
                }//$GSA

                
                
                if (txtNumbers[i].Contains(gsv))//teraz sekwencja - informacja o kazdym satelicie
                {
                    int t=0;
                    if (tylkorazsatelita == 0)//to znaczy jak i mowilem zeby szukac tej gwiazdki. na pewno jeden rekord bedzie zapisany i ten warunek przejdzie
                    { label15.Text += "Liczba aktualnie widocznych satelitów: " + txtNumbers[i + 3] + "\n\n"; }
                    tylkorazsatelita++;

                        //z kazdej linijki wypisuje jeden identyfikator
                        label15.Text += "\nIdentyfikator PRN satelity: " + txtNumbers[i + 4] + "\nWyniesienie satelity nad poziom równika (stopnie): " + txtNumbers[i + 5] + "\nAzymut satelity (stopnie): " + txtNumbers[i + 6] + "\nSNR poziom odbieranego sygnału: " + txtNumbers[i+7].Substring(0,2) + "\n" ;
                        
                       //jesli w pierwszym byla gwiazdka (suma kontrolna) - to nie wypisuje kolejne. w wrzeciwnym przypadku wypisuje
                        if (!txtNumbers[i + 7].Contains("*"))
                        {
                            t = i + 4;
                            label15.Text += "\nIdentyfikator PRN satelity: " + txtNumbers[t + 4] + "\nWyniesienie satelity nad poziom równika (stopnie): " + txtNumbers[t + 5] + "\nAzymut satelity (stopnie): " + txtNumbers[t + 6] + "\nSNR poziom odbieranego sygnału: " + txtNumbers[t + 7].Substring(0, 2) + "\n";


                            //to samo tylko zagniezdzone
                            if (!txtNumbers[i + 11].Contains("*"))
                            {
                                t += 4;
                                label15.Text += "\nIdentyfikator PRN satelity: " + txtNumbers[t + 4] + "\nWyniesienie satelity nad poziom równika (stopnie): " + txtNumbers[t + 5] + "\nAzymut satelity (stopnie): " + txtNumbers[t + 6] + "\nSNR poziom odbieranego sygnału: " + txtNumbers[t + 7].Substring(0, 2) + "\n";

                                //to samo
                                if (!txtNumbers[i + 15].Contains("*"))
                                {
                                    t += 4;
                                    label15.Text += "\nIdentyfikator PRN satelity: " + txtNumbers[t + 4] + "\nWyniesienie satelity nad poziom równika (stopnie): " + txtNumbers[t + 5] + "\nAzymut satelity (stopnie): " + txtNumbers[t + 6] + "\nSNR poziom odbieranego sygnału: " + txtNumbers[t + 7].Substring(0, 2) + "\n";
                                }
                            }
                        }

      
                            //8 12 16 20
                            //i+4 i+5 i+6 i+7
                }
            } //for

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "GPS";

            label1.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label15.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
