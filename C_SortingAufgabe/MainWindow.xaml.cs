using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading;
using System.Diagnostics;

namespace C_SortingAufgabe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Liste von Zufallszahlen
        List<int> zahlenBS = new List<int>();
        List<int> zahlenQS = new List<int>();

        // Anzahl des Austausch
        int changeBS = 0;
        int changeQS = 0;

        // Helper
        int gesamt = 1000;
        bool sorted = false;

        // Stopwatch für Bubblesort und Quicksort
        Stopwatch stopwatchBS = new Stopwatch();
        Stopwatch stopwatchQS = new Stopwatch();
        public static readonly long frequency = Stopwatch.Frequency;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Event: Generate Button clicked
        /// Zufallszahlen generieren und in der Liste hinzufügen
        /// Die Zahlenliste in der RichTextBox anzuzeigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenerate_Click(object sender, RoutedEventArgs e)
        {
            zahlenBS.Clear();
            zahlenQS.Clear();

            changeBS = 0;
            changeQS = 0;

            rtbBS.SelectAll();
            rtbBS.Selection.Text = "";
            rtbQS.SelectAll();
            rtbQS.Selection.Text = "";

            lbBS.Content = "";
            lbQS.Content = "";

            sorted = false;

            var rand = new Random();  

            for (int i = 0; i < gesamt - 1; i++)
            {
                int randNum = rand.Next(1, 1000);
                zahlenQS.Add(randNum);
                zahlenBS.Add(randNum);
            }

            ausgabe();

        }

        /// <summary>
        /// Event: Sort Button clicked
        /// Bubblesort und Quicksort Methode aufzurufen 
        /// und misst die Sortierung anhand der Stopwatch
        /// Die sortierte Zahlenliste in der RichTextBox und
        /// die Anzahl des Austausch sowie die Dauer in Label anzeigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSort_Click(object sender, RoutedEventArgs e)
        {
            if (sorted)
            {
                MessageBox.Show("Sorted!");
            } else {
                rtbBS.SelectAll();
                rtbBS.Selection.Text = "";
                rtbQS.SelectAll();
                rtbQS.Selection.Text = "";

                stopwatchBS.Start();
                bubbleSort();
                stopwatchBS.Stop();
                double tsBS = stopwatchBS.ElapsedMilliseconds;

                stopwatchQS.Start();
                quickSort(zahlenQS, 0, zahlenQS.Count - 1);
                stopwatchQS.Stop();
                double tsQS = stopwatchQS.ElapsedMilliseconds;

                ausgabe();

                lbBS.Content = $"{changeBS.ToString()} swaps - Time: {tsBS.ToString()} Milliseconds";
                lbQS.Content = $"{changeQS.ToString()} swaps - Time: {tsQS.ToString()} Milliseconds";
                sorted = true;
            }

            
        }

        /// <summary>
        /// Bubblesort Methode
        /// </summary>
        private void bubbleSort()
        {
            int temp = 0;

            for (int anzahlRichtig = 0; anzahlRichtig < zahlenBS.Count - 1; anzahlRichtig++)
            {
                for (int j = 0; j < zahlenQS.Count - anzahlRichtig - 1; j++)
                {
                    if (zahlenBS[j] > zahlenBS[j + 1])
                    {
                        temp = zahlenBS[j];
                        zahlenBS[j] = zahlenBS[j + 1];
                        zahlenBS[(j + 1)] = temp;
                        changeBS++;
                    }
                }
            }

        }

        /// <summary>
        /// Bubblesort Methode
        /// </summary>
        /// <param name="arr">Die Zufallszahlenliste</param>
        /// <param name="indexLow">die niedrigste Position/Index in der Liste</param>
        /// <param name="indexHi"> die höchste Position/Index in der Liste</param>
        private void quickSort(List<int> arr, int indexLow, int indexHi)
        {
            int pivot, tmpSwap, tmpLow, tmpHi;

            tmpLow = indexLow;
            tmpHi = indexHi;

            pivot = arr[(indexLow + indexHi) / 2];

            while (tmpLow <= tmpHi)
            {
                //increment
                while (arr[tmpLow] < pivot && tmpLow < indexHi)
                {
                    tmpLow++;
                }

                //decrement
                while (pivot < arr[tmpHi] && tmpHi > indexLow)
                {
                    tmpHi--;
                }

                //swap
                if (tmpLow <= tmpHi)
                {
                    tmpSwap = arr[tmpLow];
                    arr[tmpLow] = arr[tmpHi];
                    arr[tmpHi] = tmpSwap;

                    tmpLow++;
                    tmpHi--;
                    changeQS++; 
                }
            }

            if (indexLow < indexHi)
            {
                quickSort(arr, indexLow, tmpHi);
            }

            if(tmpLow < indexHi)
            {
                quickSort(arr, tmpLow, indexHi);
            }
        }

        /// <summary>
        /// Die Zahlenliste wird in der RichTextBox angezeigt
        /// </summary>
        private void ausgabe()
        {
            foreach (int num in zahlenBS)
            {
                rtbBS.AppendText(num.ToString() + " ");

            }

            foreach (int num in zahlenQS)
            { 
                rtbQS.AppendText(num.ToString() + " ");
            }
        }
    }
}
