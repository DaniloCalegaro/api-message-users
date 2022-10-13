using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string nameProperty { get; set; }

        [NotMapped]
        public string message { get; set; }

        [NotMapped]
        public List<Notifies> Notifications { get; set; }

        public bool validatePropertyString(string value, string propertyName)
        {
            if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    message = "Campo Obrigatório",
                    nameProperty = propertyName
                });
                return false;
            }
            return true;
        }

        public bool validatePropertyInt(int value, string propertyName)
        {
            if (value <= 0 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    message = "Campo Obrigatório",
                    nameProperty = propertyName
                });
                return false;
            }
            return true;
        }
    }
}
