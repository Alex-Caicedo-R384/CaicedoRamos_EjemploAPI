using CaicedoRamos_EjemploAPI.Modelo;
using Newtonsoft.Json;

namespace CaicedoRamos_EjemploAPI.Views;

public partial class ClimaActual : ContentPage
{
	public ClimaActual()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string latitud = Lat.Text;
        string longitud = Lon.Text;

        if (string.IsNullOrWhiteSpace(latitud) || string.IsNullOrWhiteSpace(longitud))
        {
            await DisplayAlert("Advertencia", "Por favor, ingrese valores v�lidos para Latitud y Longitud.", "OK");
            return;
        }

        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitud}&lon={longitud}&appid=b286e406c5e0e3739500ccec54a67709";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var clima = JsonConvert.DeserializeObject<Rootobject>(json);

                    weatherLabel.Text = clima.weather[0].main;
                    cityLabel.Text = clima.name;
                    countryLabel.Text = clima.sys.country;
                    temperatureLabel.Text = clima.main.temp+"";
                    windSpeedLabel.Text = string.Format("{0} m/s", clima.wind.speed);
                }
            }
        }
        else
        {
            await DisplayAlert("Advertencia", "Se requiere conexi�n a Internet para consultar el clima.", "OK");
        }
    }
}