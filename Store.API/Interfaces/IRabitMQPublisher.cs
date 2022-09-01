namespace Store.API.Interfaces
{
    public interface IRabitMQPublisher
    {
        public void SendProductMessage<T>(T message);
    }
}
