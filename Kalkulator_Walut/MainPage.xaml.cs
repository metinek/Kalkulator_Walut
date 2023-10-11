namespace Kalkulator_Walut
{
    public partial class MainPage : ContentPage
    {
        bool wybor = true;
        public MainPage()
        {
            InitializeComponent();
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
            if (wybor==false)
                otrzymaszLbl.Text = ( Math.Round(float.Parse(kwotaEnt.Text) * float.Parse(kursLbl.Text),2)).ToString() + "PLN";
            else
                otrzymaszLbl.Text = (Math.Round(float.Parse(kwotaEnt.Text) / float.Parse(kursLbl.Text),2)).ToString() + "€";
            SemanticScreenReader.Announce(otrzymaszLbl.Text);
            lblJSON.Text = "";
        }
    }
}