namespace sky.recovery.Helper.Config
{
    public class AydaHelper
    {
        public bool IsRequested(int? statusid)
        {
            if(statusid==8 || statusid==13 || statusid==11 || statusid==12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsDraft(int? statusid)
        {
            if (statusid == 3 || statusid == 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
