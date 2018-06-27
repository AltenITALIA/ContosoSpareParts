namespace SpareParts.Cqrs
{
    public static class Queues
    {
        public static readonly VehicleQueues Vehicle = new VehicleQueues();

        public class VehicleQueues
        {
            protected internal VehicleQueues()
            {
                
            }

            public readonly string QueueName = "Catalog";

            public readonly string EventsQueueName = "Catalog-Events";

        }

    }
}
