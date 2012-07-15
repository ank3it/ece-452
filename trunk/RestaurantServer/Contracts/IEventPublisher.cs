using System;

using System.ServiceModel;

namespace RestaurantServer.Contracts
{
    /// <summary>
    /// Callback interface for pushing events to clients.
    /// </summary>
    public interface IEventCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnPublishedEvent(Event e);

        [OperationContract(IsOneWay = true)]
        void OrderCompletionTimePushed(OrderUpdate update);

        [OperationContract(IsOneWay = true)]
        void NewOrder(Order update, Customer customer);
    }

    /// <summary>
    /// Allows a client to subscribe/unsubscribe to events published by the system.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IEventCallback))]
    public interface IEventPublisher
    {
        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();

        [OperationContract]
        string SendMessage(string message);
    }
}
