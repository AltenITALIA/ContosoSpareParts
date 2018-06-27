namespace SpareParts.Cqrs
{
    public static class Queues
    {
        public static readonly VehicleQueues Vehicle = new VehicleQueues();

        public static readonly PartQueues Part = new PartQueues();

        public class VehicleQueues
        {
            protected internal VehicleQueues()
            {
                
            }

            public readonly string QueueName = "Vehicle";

            public readonly string EventsQueueName = "Vehicle-Events";

        }

        public class PartQueues
        {
            protected internal PartQueues()
            {

            }

            public readonly string QueueName = "Part";

            public readonly string EventsQueueName = "Part-Events";

        }
    }
}
