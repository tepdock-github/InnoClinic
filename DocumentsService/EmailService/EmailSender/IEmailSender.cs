﻿namespace EmailService.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
