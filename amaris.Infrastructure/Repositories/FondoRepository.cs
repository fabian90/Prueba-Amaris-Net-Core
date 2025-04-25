using amaris.Core.Entities;
using amaris.Core.Interfaces.Repositories;
using Amazon.DynamoDBv2;
using Commons.Repository.Entities;
using Commons.Repository.Interfaces;

namespace Commons.Repository.Repository
{
    public class FondoRepository : GenericRepository<Fondo>, IFondoRepository
    {
        public FondoRepository(IAmazonDynamoDB client)
            : base(client, "Fondos") // nombre de la tabla en DynamoDB
        {
        }
    }
}
