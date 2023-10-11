using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json;



namespace Kalkulator_Walut
{




    public partial class MainPage : ContentPage
    {
        private string kodWaluty = "eur";
        private string data = "";
        public double skup = 0;
        public double sprzedaz = 0;



        bool wybor = true;
        public MainPage()
        {
            InitializeComponent();
            pobierzDane();
        }


        private void pobierzDane()
        {
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/" + kodWaluty + "/2023-10-11/?format=json";
            string wynik;
            using (var webClient = new WebClient())
            {
                wynik = webClient.DownloadString(url);
            }
            using JsonDocument j1 = JsonDocument.Parse(wynik);
            JsonElement json = j1.RootElement;
            var rates = json.GetProperty("rates");
            var rate = rates[0];
            string bid = rate.GetProperty("bid").ToString();
            bid = bid.Replace('.', ',');

            string ask = rate.GetProperty("ask").ToString();
            ask = ask.Replace('.', ',');

            data = rate.GetProperty("effectiveDate").ToString();
            skup = double.Parse(bid);
            sprzedaz = double.Parse(ask);
            kursLbl.Text = "\nSkup: "+ Math.Round(skup, 2).ToString()+"\n";
            kursLbl.Text += "Sprzedaż: "+ Math.Round(sprzedaz, 2).ToString();
        }

        private void OnEuroClicked(object sender, EventArgs e)
        {
            euroNaPlnBtn.IsEnabled = false;
            plnNaEuroBtn.IsEnabled = true;
            wybor = false;
        }

        private void OnPlnClicked(object sender, EventArgs e)
        {
            plnNaEuroBtn.IsEnabled = false;
            euroNaPlnBtn.IsEnabled = true;
            wybor = true;
        }

        private void OnPrzeliczClicked(object sender, EventArgs e)
        {
            if (wybor == false)
            {
                otrzymaszLbl.Text = (Math.Round(double.Parse(kwotaEnt.Text) * sprzedaz, 2)).ToString();
                walutaLbl.Text = "PLN";
            }
            else
            {
                otrzymaszLbl.Text = (Math.Round(double.Parse(kwotaEnt.Text) / skup, 2)).ToString();
                walutaLbl.Text = "€";
            }
            SemanticScreenReader.Announce(otrzymaszLbl.Text);
        }
    }
}