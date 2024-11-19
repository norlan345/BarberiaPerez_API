using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BarberiaPerez_API.Models
{
    public class AgendarCitaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("NombreCliente")]
        public string? NombreCliente { get; set; }

        [BsonElement("Servicio")]
        public string? Servicio { get; set; }

        [BsonElement("FechaCita")]
        public DateTime FechaCita { get; set; }

        [BsonElement("Total")]
        public decimal Total
        {
            get; set;

        }

       
    }
}
