using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class User
    {
        public string firstName;
        public string lastName;
        public string university;
        public Company company;
    }

    [System.Serializable]
    public class Company
    {
        public string department;
    }

    [System.Serializable]
    public class ResponseObject
    {
        public List<User> users;
    }
}
