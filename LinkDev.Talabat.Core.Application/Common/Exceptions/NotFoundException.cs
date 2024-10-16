namespace LinkDev.Talabat.Core.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
            :base("not found")
        {
            
        }
    }
}
