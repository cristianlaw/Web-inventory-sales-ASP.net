using MisMinistritosUNED.Models;
using MisMinistritosUNED.Models.Combo;

namespace MisMinistritosUNED.Connection
{
    public interface ITour
    {
        public Task<bool> AgregarTour(TourModel tour);

        public Task<TourModel> UnTour(string id);

        public Task<List<TourModel>> TodosTour();

        public Task<bool> ActualizarTour(TourModel tour);

        public Task<TourModel> BorrarTour(string id);

        public Task<List<DiasTour>> ComboBoxTour();

        public Task<object> Documentos(TourModel tour);
    }
}
