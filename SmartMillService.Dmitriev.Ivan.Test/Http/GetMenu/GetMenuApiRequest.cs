using SmartMillService.Dmitriev.Ivan.Test.Http.Abstract;

namespace SmartMillService.Dmitriev.Ivan.Test.Http.GetMenu
{
    public class GetMenuApiRequest : BaseApiRequest<GetMenuCommandParameters>
    {
        public override string Command { get => "GetMenu"; }
    }
}
