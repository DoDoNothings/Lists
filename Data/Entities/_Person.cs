using System;

namespace Lists.Data.Entities
{
    //класс пользователь
    [Serializable]
    public class OnePerson
    {            
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string Name { get; set; }
            public string Patronymic { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FillPerson =>FirstName + " " + Name + " " + Patronymic + " " + Email + " " + Phone;
         
            public override string ToString()
            {
                return FillPerson;
            }        
    }
}
