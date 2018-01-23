using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email_Backend
{
    public class EmailTemplate
    {
        public static String GenerateRequisitionStatusChangedEmailSubject(string requestNumber, string result)
        {
            return "[Requisition] " + requestNumber + " has been " + result;
        }

        public static String GenerateRequisitonStatusChangedEmail(string applicant, string requestNumber, string approver, string result, string remark)
        {
            return
                "Dear " + applicant + "<br/>"
                + "Your Stationery Request numbered " + requestNumber + " has been " + result + " by " + approver + "." + "<br/>"
                + ((remark.Trim().Length > 0 ) ? "The following Remark has been left for you: " + remark: "") + "<br/>"
                + "[This is an automated Message, do no reply]";
        }

        public static String GenerateUpdateCollectionPointEmailSubject(string collectionpoint, string result)
        {
            return "New Collection Point Changed";
        }

        public static String GenerateCollectionPointStatusChangedEmail(string applicant, string newPoint)
        {
            return
                "Dear " + applicant + "<br/>" + "There is a new Collection Point Change. Now New Location is " 
                + newPoint + "[This is an automated Message, do no reply]";
        }
    }
}
