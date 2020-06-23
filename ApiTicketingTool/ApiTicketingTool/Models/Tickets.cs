using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ApiTicketingTool.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string customerID { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string phoneNumberRequester { get; set; }
        public string IDFacebookProfile { get; set; }
        public string agentID { get; set; }
        public string groupID { get; set; }

        public DateTime? creationDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public DateTime? resolvedDate { get; set; }
        public DateTime? closedDate { get; set; }
        public DateTime? lastUpdateDate { get; set; }
        public DateTime? fistResponseRequestDate { get; set; }
        public string agentInteractions { get; set; }
        public string customerIntearction { get; set; }
        public string resolutionStatus { get; set; }
        public string firstResponseStatus { get; set; }
        public string tags { get; set; }
        public string surveysResult { get; set; }
        public string companyID { get; set; }
        public string customerCompany { get; set; }
        public string projectNumber { get; set; }
        public string sharePointID { get; set; }
        public string quotationID { get; set; }
        public decimal? customerEstimatedHours { get; set; }
        public decimal? tmHoursWeek1 { get; set; }
        public decimal? tmHoursWeek2 { get; set; }
        public decimal? tmHoursWeek3 { get; set; }
        public decimal? tmHoursWeek4 { get; set; }
        public decimal? progressWeek1 { get; set; }
        public decimal? progressWeek2 { get; set; }
        public decimal? progressWeek3 { get; set; }
        public decimal? progressWeek4 { get; set; }
        public string billingMonth { get; set; }
        public decimal? totalBillingHours { get; set; }
        public decimal? totalProgress { get; set; }
        public DateTime? estimatedStartDate { get; set; }
        public DateTime? estimatedEndDate { get; set; }
        public DateTime? realStartDate { get; set; }
        public DateTime? realEndDate { get; set; }
        public decimal? estimatedHourAgent { get; set; }
        public decimal? guaranteeHours { get; set; }

    }

    [JsonConverter(typeof(JsonPathConverter))]
    public class DocumentsResults
    {


        [JsonProperty("attachments")]
        public List<Attachments> Attachments { get; set; }
    }

    //public class Attachments
    //{
    //    public string id { get; set; }

    //    public string name { get; set; }

    //    public string attachment_url { get; set; }
    //    public DateTime created_at { get; set; }


    //}

    class JsonPathConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);
            foreach (PropertyInfo prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true).OfType<JsonPropertyAttribute>().FirstOrDefault();
                string jsonPath = (att != null ? att.PropertyName : prop.Name);
                JToken token = jo.SelectToken(jsonPath);
                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value, null);
                }
            }
            return targetObj;
        }

        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
