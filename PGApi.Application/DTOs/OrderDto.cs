using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PGApi.Application.DTOs
{
    public class OrderDto
    {
        // O atributo JsonPropertyName é utilizado para garantir que as propriedades 
        // do objeto retornado pela API sejam nomeadas exatamente como esperado,
        // correspondendo aos nomes utilizados no banco de dados ou às especificações do cliente.
        // Isso evita problemas de inconsistência de nomenclatura, como a conversão automática
        // para camelCase feita pelo serializador padrão do ASP.NET Core.
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("ProductId")]
        public int ProductId { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
}
