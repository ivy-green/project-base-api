using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using ProjectBase.Domain.Configuration;
using ProjectBase.Insfracstructure.DTOs.Message;

namespace ProjectBase.Insfracstructure.Services.Message.SQS
{
    public class SqsMessage : ISqsMessage
    {
        private readonly IAmazonSQS _sqs;
        AppSettingConfiguration _setting;
        public SqsMessage(IAmazonSQS sqs, AppSettingConfiguration setting)
        {
            _sqs = sqs;
            _setting = setting;
        }

        private async Task PrintListSqsQueue()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Getting Started with Amazon SQS");
            Console.WriteLine("===========================================\n");

            // confirming queue exists
            ListQueuesRequest listQueuesRequest = new ListQueuesRequest();
            ListQueuesResponse listQueuesResponse = await _sqs.ListQueuesAsync(listQueuesRequest);

            Console.WriteLine("Printing list of Amazon SQS queues.\n");
            foreach (String queueUrl in listQueuesResponse.QueueUrls)
            {
                Console.WriteLine("  QueueUrl: {0}", queueUrl);
            }
            Console.WriteLine();
        }

        public async Task SendSqsMessage(string queueUrl, MessageDTO content)
        {
            try
            {
                // send message
                Console.WriteLine("Sending a message to My first queue.\n");
                string json = JsonConvert.SerializeObject(content);

                SendMessageRequest sendMessageRequest = new SendMessageRequest()
                {
                    QueueUrl = queueUrl,
                    MessageBody = json
                };

                await _sqs.SendMessageAsync(sendMessageRequest);
            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
            }
            return;
        }

        public async Task DeleteSqsMessage(string queueUrl, string messageRecieptHandle)
        {
            Console.WriteLine("Delete queue My first queue.\n");
            DeleteMessageRequest deleteSqsQueueRequest = new DeleteMessageRequest() 
            { 
                QueueUrl= queueUrl,
                ReceiptHandle = messageRecieptHandle
            };
            await _sqs.DeleteMessageAsync(deleteSqsQueueRequest);
        }

        public async Task<List<Amazon.SQS.Model.Message>> ReceiveSQSMessage(string queueUrl)
        {
            ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest()
            { 
                QueueUrl = queueUrl
            };

            try
            {
                ReceiveMessageResponse receiveMessageResponse = 
                    await _sqs.ReceiveMessageAsync(receiveMessageRequest);

                if(receiveMessageResponse.Messages.Any())
                {
                    return receiveMessageResponse.Messages;
                }
                return null;
            }
            catch (AmazonSQSException)
            {
                return null;
            }
        }

    }
}
