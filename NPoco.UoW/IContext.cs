namespace NPoco.UoW
{
    public interface IContext
    {
        ISession OpenSession();
    }
}
