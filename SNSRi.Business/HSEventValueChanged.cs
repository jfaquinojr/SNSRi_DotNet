namespace SNSRi.Business
{
    public class HSEventValueChanged : HSEventMessage
    {
        public string NewValue
        {
            get
            {
                return Parameters[2];
            }
        }
        public string OldValue
        {
            get
            {
                return Parameters[2];
            }
        }
        public int ReferenceId
        {
            get
            {
                int n = 0;
                int.TryParse(Parameters[3], out n);
                return n;
            }
        }
    }
}
