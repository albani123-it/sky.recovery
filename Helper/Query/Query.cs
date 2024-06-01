namespace sky.recovery.Helper.Query
{
    public class Query : IQuery
    {

        public string QueryMonitoringList()
        {
            var Query = @"
    SELECT 
        ML.cu_cif, 
        ML.acc_no 
    FROM public.master_loan ML 
    LIMIT 5";
            return Query;
        }

    }
}
