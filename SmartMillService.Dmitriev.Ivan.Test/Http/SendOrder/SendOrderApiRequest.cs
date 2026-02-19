using SmartMillService.Dmitriev.Ivan.Test.Http.Abstract;

namespace SmartMillService.Dmitriev.Ivan.Test.Http.SendOrder
{
    public class SendOrderApiRequest : BaseApiRequest<SendOrderCommandParameters>
    {
        public override string Command => "SendOrder";
    }
}
