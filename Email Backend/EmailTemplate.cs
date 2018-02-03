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
                + ((remark.Trim().Length > 0) ? "The following Remark has been left for you: " + remark : "") + "<br/>"
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

        public static String GenerateOldRepresentativeRemovedSubject()
        {
            return "You are no longer the Department Representative";
        }

        public static String GenerateOldRepresentativeRemovedEmail()
        {
            return "Your Department Has Relieved you from Department Representative Duties. Have a Nice Day!";
        }

        public static String GenerateNewRepresentativeRemovedSubject()
        {
            return "You are now the Department Representative";
        }

        public static String GenerateNewRepresentativeRemovedEmail()
        {
            return "Your Department Has chosen you as the new Department Representative to collect the next disbursement. Upcoming disbursement details if present can be found when you login. Congrats!";
        }

        public static String GenerateOldDeputyAuthorityRemovedSubject()
        {
            return "You have been removed as Approving Authority";
        }

        public static String GenerateOldDeputyAuthorityRemovedEmail(string empName, string deptName)
        {
            return "Dear " + empName + ",<br/><br/>You have been removed as the approving authority of " + deptName + ".";
        }

        public static String GenerateNewDeputyAuthoritySubject()
        {
            return "You have been appointed as Approving Authority";
        }

        public static String GenerateNewDeputyAuthorityEmail(string empName, string deptName, string startDate, string endDate)
        {
            return "Dear " + empName + ", <br/><br/>You have been appointed as the approving authority of " + deptName + ", starting at " + startDate + " and ending at " + endDate + ".";
        }

        public static String GeneratePendingRequisitionSubject(string requestEmployee)
        {
            return "A new Requisition has been raised by" + requestEmployee + ".";
        }
        public static String GeneratePendingRequisition(string manager, string requestEmployee)
        {
            return
                "Dear " + manager + "<br/>" + "A new requisition has been raised by" + requestEmployee + ",please approve it soon.";
        }
        public static String GenerateRaisedRequisitionSubject(string requestEmployee)
        {
            return "You raised a new requisition.";
        }
        public static String GenerateRaisedRequisition(string employee, string reqNo)
        {
            return
                "Dear " + employee + "<br/>" + "Your new requisiton number is " + reqNo + ".";
        }

        public static string GenerateAdjustmentVoucherSubject()
        {
            return "New Adjustment Vouchers Pending Approval";
        }

        public static string GenerateAdjustmentVoucherEmail(string recipient, int aVNo)
        {
            string message =
                "Dear " + recipient + ","
                + "<br/><br/>Adjustment Voucher #" + aVNo + " has been created and is pending your approval."
                + "<br/>";

            message += "<br/>Thank you."
                + "<br/><br/>[This is an automated Message. Please do not reply.]";

            return message;
        }

        public static String GenerateOrderFormEmailSubject()
        {
            return "New Purchase Orders Pending Approval";
        }

        public static String GenerateOrderFormEmail(string supervisor, List<string> pOList)
        {
            string message =
                "Dear " + supervisor + ","
                + "<br/><br/>The following Purchase Orders have been created and are pending your approval:"
                 + "<br/>";

            int count = 1;
            foreach (string pO in pOList)
            {
                message += String.Format("<br/>{0}. {1}", count, pO);
                count++;
            }

            message += "<br/><br/>Thank you."
                + "<br/><br/>[This is an automated Message. Please do not reply.]";

            return message;
        }

        public static string GenerateCompletedDisbursementSubject(int disbursementNo)
        {
            return "Disbursement #" + disbursementNo + " Success";
        }

        public static string GenerateCompletedDisbursementEmail(string empName, int disbursementNo, DateTime? disbursementDate, IEnumerable<dynamic> dDetailsList)
        {
            string message = "Dear " + empName + ",";
            message += "<br/><br/>";
            message += "Disbursement #" + disbursementNo + " was completed successfully on " + disbursementDate + ". Please refer below for more details:";
            message += "<br/><br/>";
            message += "<table><tr>";
            message += "<th>Item No</th>";
            message += "<th>Description</th>";
            message += "<th>Needed</th>";
            message += "<th>Delivered</th>";
            message += "</tr>";

            foreach (dynamic item in dDetailsList)
            {
                message += "<tr><td>" + item.ItemNo + "</td>";
                message += "<td>" + item.ItemDescription + "</td>";
                message += "<td>" + item.Needed + "</td>";
                message += "<td>" + item.Delivered + "</td></tr>";
            }

            message += "</table>";
            message += "<br/>";
            message += "Thank you.";
            message += "<br/><br/>[This is an automated Message. Please do not reply.]";

            return message;
        }
    }
}
