using ClassLibrary;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Pomocne_klase
{
    public class ExcelExport
    {
        private Workbook workbook;
        private Worksheet sheet;

        public List<Trosak> Troskovi { get; set; }
        public string FilePath { get; set; }
        public double[] Uplate { get; set; }

        public ExcelExport(string path, List<Trosak> troskovi, double[] uplate)
        {
            FilePath = path;
            Troskovi = troskovi;
            Uplate = uplate;

            workbook = new Workbook();
            sheet = new Worksheet("Sheet 1");
        }

        private void Zapamti()
        {
            workbook.Worksheets.Add(sheet);
            workbook.Save(FilePath);
        }

        private void Otvori()
        {
            System.Diagnostics.Process.Start(FilePath);
        }

        public void Exportuj()
        {
            PopuniSheet();
            Zapamti();
            Otvori();
        }

        private void PopuniSheet()
        {
            int row = 0;
            UnesiNaslovZaUplate(row);
            row += 2;

            var kategorije = Troskovi.GroupBy(t => t.Kategorija);
            foreach (IGrouping<string, Trosak> k in kategorije)
            {
                row++;// prazan red ispred kategorije
                UnesiRed(row++, k.Key, SumaKategorije(k.Key));
                

                var podkategorije = k.GroupBy(t => t.Podkategorija);
                foreach (IGrouping<string, Trosak> p in podkategorije)
                {
                    UnesiRed(row++, p.Key, SumaPotkategorije(k.Key, p.Key));
                }
            }
        }

        // Ispisuje na vrhu sheeta 1.UPLATA, 2.UPLATA... i ispod toga koliko procenata nosi koja uplata
        private void UnesiNaslovZaUplate(int row)
        {
            for (int i = 0; i < Uplate.Count(); i++)
            {
                sheet.Cells[row, i + 3] = new Cell($"{i + 1}. UPLATA");
                sheet.Cells[row + 1, i + 3] = new Cell($"{Uplate[i]}%");
            }
        }

        // Ispisuje naziv troska, iznos i deli ga na rate prema Uplatama i to ispisuje
        private void UnesiRed(int row, string text, double iznos)
        {
            sheet.Cells[row, 0] = new Cell(text);
            sheet.Cells[row, 1] = new Cell(iznos);

            for (int i = 0; i < Uplate.Count(); i++)
            {
                sheet.Cells[row, i + 3] = new Cell(iznos * Uplate[i] / 100);
            }
        }

        // Vraca sumu svih troskova za jednu potkategoriju
        private double SumaPotkategorije(string kategorija, string podKategorija)
        {
            double suma = 0;
            foreach (var item in Troskovi)
                if (item.Kategorija == kategorija && item.Podkategorija == podKategorija)
                    suma += item.UkupnaCena;
            return suma;
        }

        // Vraca sumu trsokova za jednu kategoriju
        private double SumaKategorije(string kategorija)
        {
            double suma = 0;
            foreach (var item in Troskovi)
                if (item.Kategorija == kategorija)
                    suma += item.UkupnaCena;
            return suma;
        }
    }
}
