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
            var config = DynamoDbConfig.GetLocalConfig(); // Usa configuración local
            Client = new AmazonDynamoDBClient(config);
            Context = new DynamoDBContext(Client);
        }
    }
}
