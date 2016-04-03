using System;
using System.Runtime.Serialization;

namespace MegProject.Dto.CustomDto.ResultModels
{
    [DataContract]
    [Serializable]
    public class WebResultModel
    {
        [DataMember]
        public string resultType { get; set; }
        [DataMember]
        public  string header { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}