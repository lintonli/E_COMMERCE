using EmailService.Models.Dtos;
using MailKit.Net.Smtp;
using MimeKit;


namespace EmailService.Service
{
    public class MailsService
    {
        private readonly string _email;
        private readonly string _password;
        private readonly IConfiguration _configuration;

        public MailsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = _configuration.GetValue<string>("EmailSettings:Email");
            _password = _configuration.GetValue<string>("EmailSettings:Password");
        }

        public async Task sendEmail(UserMessageDto user, string Message)
        {

            var message1 = new MimeMessage();

            message1.From.Add(new MailboxAddress("E_COMMERCE", _email));

            message1.To.Add(new MailboxAddress(user.Name, user.Email));

            message1.Subject = "WELCOME TO EDD en EDDY COLLECTION";

            var body = new TextPart("html")
            {
                Text = Message.ToString()
            };

            message1.Body = body;


            ///

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate(_email, _password);

            await client.SendAsync(message1);

            await client.DisconnectAsync(true);
        }
    }
}
