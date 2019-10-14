using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoDBTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);

        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        // Initialize the Amazon Cognito credentials provider
        CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "us-east-2:e36bedfb-19e2-49dc-95ad-3c4636fdb918", // Identity pool ID
            RegionEndpoint.USEast2 // Region
        );

        AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

        DynamoDBContext context = new DynamoDBContext(client);

        Book myBook = new Book
        {
            Id = 1001,
            Title = "object persistence-AWS SDK for.NET SDK-Book 1001",
            ISBN = "111-1111111001",
            BookAuthors = new List<string> { "Author 1", "Author 2" },
        };

        // Save the book.
        context.SaveAsync(myBook, (result) => {
            if (result.Exception == null)
                Debug.Log(@"book saved");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [DynamoDBTable("ProductCatalog")]
    public class Book
    {
        [DynamoDBHashKey]   // Hash key.
        public int Id { get; set; }
        [DynamoDBProperty]
        public string Title { get; set; }
        [DynamoDBProperty]
        public string ISBN { get; set; }
        [DynamoDBProperty("Authors")]    // Multi-valued (set type) attribute. 
        public List<string> BookAuthors { get; set; }
    }
}
