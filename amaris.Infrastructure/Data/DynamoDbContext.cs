using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using amaris.Infrastructure.Data.Configurations;

namespace amaris.Infrastructure.Data
{
    public class DynamoDbContext
    {
        public AmazonDynamoDBClient Client { get; private set; }
        public DynamoDBContext Context { get; private set; }

        public DynamoDbContext()
        {
            // Usar la configuración local de DynamoDB
            var config = DynamoDbConfig.GetLocalConfig(); // Usa configuración local

            // Crear el cliente con la configuración proporcionada
            Client = new AmazonDynamoDBClient(config);

            // Crear el contexto de DynamoDB para realizar operaciones
            Context = new DynamoDBContext(Client);
        }
    }
}
