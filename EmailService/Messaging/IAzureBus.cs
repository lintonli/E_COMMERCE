namespace EmailService.Messaging
{
    public interface IAzureBus
    {
        Task start();
        Task stop();
    }
}
