﻿namespace SharedModelsInnoClinic
{
    public interface IReceptionistProfileManipulation
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        int OfficeId { get; set; }
        int AccountId { get; set; }
    }
}
