﻿namespace NutrifeelCore.Infraestructure.Service
{
    public interface ICustomEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
