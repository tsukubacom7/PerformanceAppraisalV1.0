update EmployeeMaster set HRStaff = null, AppraisalYear = null, AppraisalDate = null
where EmployeeID in ('00001253','00001256','00001582','00001554')

delete from AppraisalDocHeader 
where EmployeeID in (select EmployeeID from EmployeeMaster where EmployeeID in ('00001253','00001256','00001582','00001554'));

delete from AppraisalDocLine where AppraisalDocNo in (
select AppraisalDocNo from AppraisalDocHeader 
where EmployeeID in (select EmployeeID from EmployeeMaster where EmployeeID in ('00001253','00001256','00001582','00001554')))

delete from Attachments where EmployeeID in ('00001253','00001256','00001582','00001554')

delete from ActionHistory where EmployeeID in ('00001253','00001256','00001582','00001554')

delete from ApprovalHistory where AppraisalDocNo in (
select AppraisalDocNo from AppraisalDocHeader 
where EmployeeID in (select EmployeeID from EmployeeMaster where EmployeeID in ('00001253','00001256','00001582','00001554')))

select * from EmployeeMaster where EmployeeID in ('00001253','00001256','00001582','00001554');





