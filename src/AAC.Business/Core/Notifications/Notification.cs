﻿namespace AAC.Business.Core.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }

        public string Message { get; set; } 
    }
}