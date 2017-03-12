namespace SNSRi.Entities
{
    public class DeviceControl
	{
		public int Id { get; set; }
        public int DeviceId { get; set; }
        public bool DoUpdate { get; set; }
        public bool SingleRangeEntry { get; set; }
        public int ButtonType { get; set; }
        public string ButtonCustom { get; set; }
        public int CCIndex { get; set; }
        public string Range { get; set; }
        public string Label { get; set; }
        public int ControlType { get; set; }
        public string ControlValue { get; set; }
        public string ControlString { get; set; }
        public string ControlStringList { get; set; }
        public string ControlStringSelected { get; set; }
        public bool ControlFlag { get; set; }
    }
}
