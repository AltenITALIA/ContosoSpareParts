namespace SpareParts.Cqrs
{
    public static class Queues
    {
        public static readonly CatalogQueues Catalog = new CatalogQueues();

        public static readonly TasteQueues Taste = new TasteQueues();

        public class CatalogQueues
        {
            protected internal CatalogQueues()
            {
                
            }

            public readonly string QueueName = "Catalog";

            public readonly string EventsQueueName = "Catalog-Events";

        }
        public class TasteQueues
        {
            protected internal TasteQueues()
            {

            }

            public readonly string QueueName = "Taste";

            public readonly string EventsQueueName = "Taste-Events";

        }
    }
}
