using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Models
{
    public class PostFreshDesk
    {
        public int TicketID { get; set; }
        //Required
        [Required]
        public int customerID { get; set; }//requester_id 
        [Required]
        public string email { get; set; } //email
        [Required]
        public string IDFacebookProfile { get; set; } //facebook_id
        [Required]
        public string phoneNumberRequester { get; set; }//phone
        [Required]
        public string IDTwitterProfile { get; set; }//twitter_id
        [Required]
        public int status { get; set; }//status
        [Required]
        public int priority { get; set; }//priority
        [Required]
        public int source { get; set; }
        [Required]
        public string externalID { get; set; }//unique_external_id
        //Required

        public string name { get; set; }//name
        public string subject { get; set; }//subject 
        public string description { get; set; }//description
        public string type { get; set; }//type
        public string agentID { get; set; }//responder_id
        public int groupID { get; set; }//group_id
        public int productID { get; set; } //HardCode -- product_id
        public List<string> tags { get; set; }//tags
        public int companyID { get; set; }//company_id
        public List<Attachments> Attachments { get; set; }//attachments
        public List<string> cc_Emails { get; set; }//cc_emails
        public List<object> custom_Fields { get; set; }//custom_fields
        public DateTime? resolvedDate { get; set; } //due_by para mí
        public int email_ConfigID { get; set; } //HardCode 
        public DateTime? expirationDate { get; set; } //fr_due_by para mí

    }
    public class Attachments
    {
        public string id { get; set; }

        public string name { get; set; }

        public string attachment_url { get; set; }
        public DateTime created_at { get; set; }
    }
}
