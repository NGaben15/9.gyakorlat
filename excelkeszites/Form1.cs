using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace excelkeszites
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB;     // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void CreateTable()
        {
            xlSheet.Cells[1, 1] = "Kérdés";
            xlSheet.Cells[1, 2] = "Válasz1";
            xlSheet.Cells[1, 3] = "Válasz2";
            xlSheet.Cells[1, 4] = "Válasz3";
            xlSheet.Cells[1, 1] = "Helyes válasz";

           Models.HajosContext context = new Models.HajosContext();
            var adatok = context.Questions.ToList();

            object[,] adatTömb = new object[mindenKérdés.Count(), fejlécek.Count()];

            for (int i = 0; i < mindenKérdés.Count(); i++)
            {
                adatTömb[i, 0] = mindenKérdés[i].Question1;
                adatTömb[i, 1] = mindenKérdés[i].Answer1;
                adatTömb[i, 2] = mindenKérdés[i].Answer2;
                adatTömb[i, 3] = mindenKérdés[i].Answer3;
                adatTömb[i, 4] = mindenKérdés[i].CorrectAnswer;
                adatTömb[i, 5] = mindenKérdés[i].Image;
            }

            int sorokSzáma = adatTömb.GetLength(0);
            int oszlopokSzáma = adatTömb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);
            adatRange.Value2 = adatTömb;

            adatRange.Columns.AutoFit();

            Excel.Range fejllécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejllécRange.Font.Bold = true;
            fejllécRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejllécRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejllécRange.EntireColumn.AutoFit();
            fejllécRange.RowHeight = 40;
            fejllécRange.Interior.Color = Color.Fuchsia;
            fejllécRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }
        



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következõ feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
        
    }
}