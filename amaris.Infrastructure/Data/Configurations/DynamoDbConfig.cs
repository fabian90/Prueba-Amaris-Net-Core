using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace amaris.Infrastructure.Data.Configurations
{
    public static class DynamoDbConfig
    {
        public static AmazonDynamoDBConfig GetLocalConfig()
        {
            return new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000" // DynamoDB local
            };
        }
    }
}
