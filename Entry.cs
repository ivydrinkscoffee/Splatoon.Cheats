namespace Splatoon.Cheats
{
    class Entry
    {
        #region Constants

        private string ENTRY_HEADER = "[Entry]";
        private string DESCRPTION_PREFIX = "description=";
        private string ADDRESS_PREFIX = "address";
        private string TYPE_PREFIX = "type=";
        private string VALUE_PREFIX = "value=";

        private string Description;
        private string Address;
        private string Type;
        private uint Value;

        #endregion

        #region Constuctors And Desctructors

        public Entry(string description, string address, string type, uint value)
        {
            Description = description;
            Address = address;
            Type = type;
            Value = value;
        }

        #endregion

        #region Methods

        public string GetFullEntry()
        {
            return $"{ENTRY_HEADER}\n" +
                $"{DESCRPTION_PREFIX}{Description}\n" +
                $"{ADDRESS_PREFIX}{Address}\n" +
                $"{TYPE_PREFIX}{Type}\n" +
                $"{VALUE_PREFIX}{Value}\n\n";
        }

        #endregion
    }
}
