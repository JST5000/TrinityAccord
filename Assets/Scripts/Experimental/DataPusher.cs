using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPusher : MonoBehaviour
{
    public static bool RecordStats { get; set; } = true;

    private bool EnableExceptionsToPropagate = false;

    private void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="runId">Should be stored by the caller via GetNewRunId at the start of each run</param>
    /// <param name="enemies">The current encounter's enemy list in string format (Comma separated)</param>
    /// <param name="startHP">HP before any fighting happened</param>
    /// <param name="endHP"></param>
    /// <param name="deck"></param>
    public void PushEncounterHistory(string runId, string enemies, int startHP, int endHP, List<CardData> deck)
    {
        StartCoroutine(checkInternetConnection((isConnected) =>
        {
            //If there is internet connection, try to push
            if (isConnected && RecordStats)
            {
                List<string> deckCardNames = new List<string>();
                foreach (CardData card in deck)
                {
                    deckCardNames.Add(card.GetName());
                }

                Debug.Log(runId + "\n" + enemies + "\n" + startHP + "\n" + endHP + "\n" + deck);

                try
                {
                    EncounterHistory history = new EncounterHistory
                    {
                        Enemies = enemies,
                        RunId = runId,
                        StartingHP = startHP,
                        EndingHP = endHP,
                        DamageTaken = startHP - endHP,
                        Deck = deckCardNames
                    };

                    AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

                    // Initialize the Amazon Cognito credentials provider
                    CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                        "us-east-2:e36bedfb-19e2-49dc-95ad-3c4636fdb918", // Identity pool ID
                        RegionEndpoint.USEast2 // Region
                    );

                    AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

                    //V2 context allows for duplicate string entries (Such as when uploading the deck list)
                    DynamoDBContext context = new DynamoDBContext(client, new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });


                    // Save the encounter history.
                    context.SaveAsync(history, (result) =>
                    {
                        if (result.Exception == null)
                            Debug.Log(@"encounter data saved");
                        else
                        {
                            Debug.Log(result.Exception);
                        }
                    });

                }
                catch (Exception e)
                {
                    //TODO selectively mute so that players can play offline without getting a flood of errors!
                    if (EnableExceptionsToPropagate)
                    {
                        throw e;
                    }
                    else
                    {
                        Debug.Log("Unhandled Exception:");
                        Debug.Log(e);
                    }
                }
            }
        }));
    }

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }


    public static string GetNewRunId()
    {
        return System.DateTime.UtcNow.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [DynamoDBTable("encounter_history")]
    public class EncounterHistory
    {
        [DynamoDBHashKey("enemies")]   // Hash key.
        public string Enemies { get; set; }
        [DynamoDBRangeKey("run_id")]
        public string RunId { get; set; }
        [DynamoDBProperty("Deck list")]
        public List<string> Deck { get; set; }
        [DynamoDBProperty]
        public int StartingHP { get; set; }
        [DynamoDBProperty]
        public int EndingHP { get; set; }
        [DynamoDBProperty]
        public int DamageTaken { get; set; }
    }
}
