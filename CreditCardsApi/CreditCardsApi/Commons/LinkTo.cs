namespace CreditCardsApi.Commons
{
    public sealed class LinkTo
    {
        public string rel { get; set; }
        public string method { get; set; }
        public string href { get; set; }

        public static LinkTo To( string rel, string method, string href, string id = "")
        {
            return new LinkTo()
            {
                rel = rel,
                href = href + $"/{id}",
                method = method
            };
        }

    }
}
