namespace Domain.Data
{
    public class WordDictionary
    {
        public string Id { get; set; }

        public string Salt { get; set; }

        public string Word { get; set; }

        public int Count { get; set; }
    }
}
