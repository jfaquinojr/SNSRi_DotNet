namespace SNSRi.Api.Models.HomeSeer
{
    public class ControlPair
    {
        public bool Do_Update { get; set; } 
        public bool SingleRangeEntry { get; set; } 
        public int ControlButtonType { get; set; }
        public string ControlButtonCustom { get; set; }
        public int CCIndex { get; set; }
        public string Range { get; set; }
        public int Ref { get; set; }
        public string Label { get; set; }
        public int ControlType { get; set; }
        public ControlLocation ControlLocation { get; set; }

        public int ControlUse { get; set; }
        public string ControlValue { get; set; }
        public string ControlString { get; set; }
        public string ControlStringList { get; set; }
        public string ControlStringSelected { get; set; }
        public bool ControlFlag { get; set; }

    }
}