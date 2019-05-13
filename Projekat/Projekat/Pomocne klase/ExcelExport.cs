using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Projekat.Pomocne_klase
{
    public class ExcelExport
    {
        private ExcelWorksheet sheet;
        private double sumaSvihTroskova;// Ovo radim zbog performansi, da bi izbegao visestruko racunanje ove vrednosti pamtim je u privatnom atributu i racunam u konsturktoru;

        public List<Trosak> Troskovi { get; set; }
        public string FilePath { get; set; }
        public double[] Uplate { get; set; }

        public ExcelExport(string path, List<Trosak> troskovi, double[] uplate)
        {
            FilePath = path;
            Troskovi = troskovi;
            Uplate = uplate;
            sumaSvihTroskova = SumaSvihTroskova();
        }

        private void Otvori()
        {
            System.Diagnostics.Process.Start(FilePath);
        }

        public void Exportuj()
        {
            using (var p = new ExcelPackage())
            {
                sheet = p.Workbook.Worksheets.Add("Sheet 1");
                PopuniSheet();
                p.SaveAs(new FileInfo(FilePath));
            }
            Otvori();
        }

        private void PopuniSheet()
        {
            int row = 1;
            UnesiNaslovZaUplate(row);
            row += 2;

            UnesiRed(row++, "Ukupno", sumaSvihTroskova, Color.White, false);
            row++;
            UnesiRed(row++, "EXTENT BRUTO", sumaSvihTroskova, Color.Orange, false);
            UnesiRed(row++, "EXTENT NETO", sumaSvihTroskova * 0.8, Color.Yellow, false);
            row++;
            UnesiNaziveKolona(row++);

            // Zanimljivi deo, grupise troskove na osnovu kategorije a onda na osnovu podkategorije, izracunava sumu troskova kategorije/podkategorije i to stampa
            var kategorije = Troskovi.GroupBy(t => t.Kategorija);
            foreach (IGrouping<string, Trosak> k in kategorije)
            {
                UnesiRed(row++, k.Key, SumaKategorije(k.Key), Color.LightGray);    

                var podkategorije = k.GroupBy(t => t.Podkategorija);
                foreach (IGrouping<string, Trosak> p in podkategorije)
                {
                    UnesiRed(row++, p.Key, SumaPotkategorije(k.Key, p.Key), Color.White);
                }
            }

            // Automatsko odredjivanje sirine kolona cell range-a
            sheet.Cells[1, 1, row, Uplate.Count() + 4].AutoFitColumns();
        }

        private void UnesiNaziveKolona(int row)
        {
            sheet.Cells[row, 1].Value = "Aktivnost"; 
            sheet.Cells[row, 2].Value = "Saradnik"; 
            sheet.Cells[row, 3].Value = "%"; 
            sheet.Cells[row, 4].Value = "Ukupno"; 
        }

        // Ispisuje na vrhu sheeta 1.UPLATA, 2.UPLATA... i ispod toga koliko procenata nosi koja uplata
        private void UnesiNaslovZaUplate(int row)
        {
            for (int i = 0; i < Uplate.Count(); i++)
            {
                sheet.Cells[row, i + 5].Value = $"{i + 1}. UPLATA";
                sheet.Cells[row + 1, i + 5].Value = Uplate[i];
                sheet.Cells[row + 1, i + 5].Style.Numberformat.Format = "#0\\.00%";
            }
        }

        // Ispisuje naziv troska, iznos i deli ga na rate prema Uplatama i to ispisuje
        private void UnesiRed(int row, string text, double iznos, Color color, bool prikaziProcenat = true)
        {
            sheet.Cells[row, 1].Value = text;
            if (prikaziProcenat)
            {
                sheet.Cells[row, 3].Value = iznos * 100 / sumaSvihTroskova;
                sheet.Cells[row, 3].Style.Numberformat.Format = "#0\\.00%";
            }
            sheet.Cells[row, 4].Value = iznos;

            for (int i = 0; i < Uplate.Count(); i++)
            {
                sheet.Cells[row, i + 5].Value = iznos * Uplate[i] / 100;
            }

            // Postavljanje stila ovih celija
            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, Uplate.Count() + 4].Style.Fill.BackgroundColor.SetColor(color);
        }

        // Vraca sumu svih troskova za jednu potkategoriju
        private double SumaPotkategorije(string kategorija, string podKategorija)
        {
            return Troskovi.Where(t => t.Kategorija == kategorija && t.Podkategorija == podKategorija)
                           .Sum(t => t.UkupnaCena); ;
        }

        // Vraca sumu trsokova za jednu kategoriju
        private double SumaKategorije(string kategorija)
        {
            return Troskovi.Where(t => t.Kategorija == kategorija)
                           .Sum(t => t.UkupnaCena); ;
        }

        // Vraca sumu svih troskova
        private double SumaSvihTroskova()
        {
            return Troskovi.Sum(t => t.UkupnaCena);
        }
    }
}
