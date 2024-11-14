using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BarberiaPerez_API.Models
{
    public class ServicioDisponibleModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Precio")]
        public decimal Precio { get; set; }


        [BsonElement("Servicio")]
        public string? Servicio { get; set; }

       



    }
}
