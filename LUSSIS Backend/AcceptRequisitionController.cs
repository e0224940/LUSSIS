using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class AcceptRequisitionController
    {
        public static bool AcceptRequisition(int employeeID, int requisitionID, String remarks)
        {
            bool result = false;

            LussisEntities context = new LussisEntities();
            Employee approver = context.Employees.Where(emp => emp.EmpNo == employeeID).FirstOrDefault();
            Requisition requisition = context.Requisitions.Where(req => req.ReqNo == requisitionID).FirstOrDefault();

            // Process only if all values are present in the database
            if (approver != null && requisition != null)
            {
                requisition.Remarks = remarks;
                requisition.ApprovedBy = approver.EmpNo;
                requisition.DateReviewed = DateTime.Now.Date;
                requisition.Status = "Approved";

                context.SaveChanges();

                // Send Email
                EmailBackend.sendEmailStep(
                    requisition.EmployeeWhoIssued.Email,
                    EmailTemplate.GenerateRequisitionStatusChangedEmailSubject(
                        requisition.ReqNo.ToString(),
                        requisition.Status),
                    EmailTemplate.GenerateRequisitonStatusChangedEmail(
                        requisition.EmployeeWhoIssued.EmpName,
                        requisition.ReqNo.ToString(),
                        approver.EmpName,
                        requisition.Status,
                        requisition.Remarks)
                    );

                // Mark as sucessful
                result = true;
            }

            // Return operation result
            return result;
        }
    }
}
