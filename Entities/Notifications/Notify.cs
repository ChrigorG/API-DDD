using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Notifications
{
    public class Notify
    {
        public Notify() 
        {
            this.Notifications = new();
        }

        [NotMapped]
        public string NameProperties { get; set; } = string.Empty;

        [NotMapped]
        public string Message { get; set; } = string.Empty;

        [NotMapped]
        public List<Notify> Notifications { get; set; }

        public bool ValidatePropertiesString(string value, string nameProperty)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(nameProperty))
            {
                this.Notifications.Add(new Notify
                {
                    Message = "Field Mandatory",
                    NameProperties = nameProperty
                });

                return false;
            }

            return true;
        }
    }
}
