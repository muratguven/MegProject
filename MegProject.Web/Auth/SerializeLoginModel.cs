using System;
namespace MegProject.Web.Auth
{
    [Serializable]
    public class SerializeLoginModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}