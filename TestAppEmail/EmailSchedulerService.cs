using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using TestAppEmail.Data;
using TestAppEmail.Models;

public class EmailSchedulerService : BackgroundService
{
    private readonly IServiceProvider _services;

    public EmailSchedulerService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DateDbContext>(); 

                
                var currentDateTime = DateTime.UtcNow;
                var schedulesDue = dbContext.SystemParamaterTest
                    .ToList();
                var emailIsSent = dbContext.SystemParamaterTestEmail.ToList();


                // Send the emails for the due schedules
                foreach (var schedule in schedulesDue)
                {

                    DateTime sdateTime = DateTime.Parse(schedule.DateValue);
                    DateTime nowWithoutSeconds = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
                    int esid = emailIsSent[0].EmailId;

                    if (sdateTime == nowWithoutSeconds)
                    {
                        await SendEmail(esid);
                    }
                    //schedule.IsSent = true; // Mark the schedule as sent to avoid duplicate sending
                    emailIsSent[0].IsSent = 1;
                }

                await dbContext.SaveChangesAsync();
            }

            // Wait for some time before checking again (e.g., every minute)
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task SendEmail(int emailModel)
    {
      
        try
        {
            //email details
            string sender = "prarthana.wickramarathne@synapsys.lk";
            string receiver = "sendertestapp@gmail.com";
            string subject = "New test email - 2023-07-19";
            string name = "prarthana";
            string time = DateTime.Now.ToString();
            string eid = emailModel.ToString();

            // Create a new MailMessage object
            MailMessage message = new MailMessage();
            message.From = new MailAddress(sender);
            message.To.Add(receiver);
            message.Subject = subject;

            // Read the email body from the HTML file
            // Read the HTML template file
            string templatePath = "EmailTemplates/EmailTemplate.html";
            string htmlContent = System.IO.File.ReadAllText(templatePath);

            htmlContent = htmlContent.Replace("{name}", name);
            htmlContent = htmlContent.Replace("{time}", time);
            htmlContent = htmlContent.Replace("{eid}", eid);

            message.Body = htmlContent;
            message.IsBodyHtml = true; // Set the IsBodyHtml property to true

            // Create a new SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("prarthana.wickramarathne@synapsys.lk", "Prarthana1994");

            // Send the email
            smtpClient.Send(message);

            return;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error sending email: {ex.Message}");
        }
    }
}
