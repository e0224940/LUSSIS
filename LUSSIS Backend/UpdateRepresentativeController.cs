﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class UpdateRepresentativeController
    {
        public static Employee GetCurrentRepresentativeOf(int employeeNo)
        {
            LussisEntities context = new LussisEntities();
            Employee result = null;
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Department currDepartment = null;

            if (currEmployee != null)
            {
                currDepartment = context.Departments
                    .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                    .FirstOrDefault();

                if (currDepartment != null)
                {
                    result = currDepartment.EmployeeRepresentative;
                }
            }

            return result;
        }

        public static List<Employee> GetAlternativeRepresentativesFor(int employeeNo)
        {
            LussisEntities context = new LussisEntities();
            List<Employee> result = new List<Employee>();
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Department currDepartment = null;

            if (currEmployee != null)
            {
                currDepartment = context.Departments
                    .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                    .FirstOrDefault();

                if (currDepartment != null)
                {
                    result = currDepartment.Employees
                        .Where(emp => emp.EmpNo != currDepartment.EmployeeRepresentative.EmpNo)
                        .ToList();
                }
            }

            return result;
        }

        public static bool SetNewDepartmentRepresentativeTo(int employeeNo, int newRepresentativeNo)
        {
            LussisEntities context = new LussisEntities();
            bool result = false;
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Employee newRepresentative = context.Employees
                .Where(emp => emp.EmpNo == newRepresentativeNo)
                .FirstOrDefault();
            Department currDepartment = null;

            try
            {
                if (currEmployee != null && newRepresentative != null)
                {
                    currDepartment = context.Departments
                        .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                        .FirstOrDefault();

                    if (currDepartment != null)
                    {
                        currDepartment.RepEmpNo = newRepresentative.EmpNo;
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
