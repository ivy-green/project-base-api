using ProjectBase.Insfracstructure.DTOs.Message;

namespace ProjectBase.Insfracstructure.Services.Message.SQS
{
    public interface ISqsMessage
    {
        Task SendSqsMessage(string queueUrl, MessageDTO content);
        Task DeleteSqsMessage(string queueUrl, string messageRecieptHandle);
        Task<List<Amazon.SQS.Model.Message>> ReceiveSQSMessage(string queueUrl);
    }
}