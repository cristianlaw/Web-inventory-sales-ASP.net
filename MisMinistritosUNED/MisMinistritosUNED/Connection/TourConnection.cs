using MisMinistritosUNED.Models;
using MisMinistritosUNED.Models.Combo;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace MisMinistritosUNED.Connection
{
    public class TourConnection : ITour
    {
        private string _baseurl;

        public TourConnection()
        {
            _baseurl = "http://localhost:5182";
        }

        public async Task<bool> AgregarTour(TourModel tours)
        {
            bool respuesta = false;
            var tour = new HttpClient();
            tour.BaseAddress = new Uri(_baseurl);


            var contenido = new StringContent(JsonConvert.SerializeObject(tours), Encoding.UTF8, "application/json");
            var response = await tour.PostAsync($"TourApi/Tour", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
        public async Task<object> Documentos(TourModel tour)
        {


            //Añadir imagen
            string RutaImagen = ("wwwroot/Imagen/"); //Ruta para las imagenes

            if (!Directory.Exists(RutaImagen))//si la ruta no existe la crea
                Directory.CreateDirectory(RutaImagen);

            string fileName = tour.id + ".jpg"; //Renombre la imagen con la ID única

            string fileNameWithPath = Path.Combine(RutaImagen, fileName); //Ruta completa con nombre final

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                tour.imagen.CopyTo(stream); //Se añade imagen 
            }
            string filePath = ("wwwroot/Imagen/" + tour.id + ".jpg");
            using var form = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "formFile", filePath);

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_baseurl)
            };

            var response = await httpClient.PostAsync($"TourApi/Tour/Subir", form);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            File.Delete(filePath);
            return (responseContent);

        }

        public async Task<bool> ActualizarTour(TourModel tours)
        {
            bool respuesta = false;
            var tour = new HttpClient();
            tour.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(tours), Encoding.UTF8, "application/json");
            var response = await tour.PutAsync($"TourApi/Tour", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<TourModel> BorrarTour(string id)
        {
            TourModel tourEncontrado = null;

            var tour = new HttpClient();
            tour.BaseAddress = new Uri(_baseurl);

            var respuesta = await tour.DeleteAsync($"TourApi/Tour/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<TourModel>(json_respuesta);
                tourEncontrado = resultado;
            }

            return tourEncontrado;
        }

        public async Task<TourModel> UnTour(string id)
        {
            TourModel tourEncontrado = null;

            var tour = new HttpClient();
            tour.BaseAddress = new Uri(_baseurl);

            var respuesta = await tour.GetAsync($"TourApi/Tour/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<TourModel>(json_respuesta);
                tourEncontrado = resultado;
            }

            return tourEncontrado;
        }

        public async Task<List<TourModel>> TodosTour()
        {
            List<TourModel> listaTemporal = new List<TourModel>();

            var tour = new HttpClient();
            tour.BaseAddress = new Uri(_baseurl);

            var respuesta = await tour.GetAsync("TourApi/Tour");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<TourModel>>(json_respuesta);
                listaTemporal = resultado;
            }

            return listaTemporal;
        }

        public async Task<List<DiasTour>> ComboBoxTour()
        {
            List<DiasTour> referenciaTemporal = new List<DiasTour>();

            var diasTour = new HttpClient();
            diasTour.BaseAddress = new Uri(_baseurl);

            var respuesta = await diasTour.GetAsync("/CBoxTourApi/CBoxTour");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<DiasTour>>(json_respuesta);
                referenciaTemporal = resultado;
            }
            return referenciaTemporal;
        }

        
    }
}
