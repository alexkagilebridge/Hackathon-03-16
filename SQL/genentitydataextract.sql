
  select top (100) ps.PayslipID
		

	  ,EG.[GenEntityID]
      ,EG.[FirstName]
      ,EG.[DoNotReEmploy]
      ,EG.[BirthDate]
      ,EG.[Gender]
      ,EG.[NationalityCountryCode]
      ,EG.[MaritalStatusTypeID]
      ,EG.[LanguageTypeID]
      ,EG.[ForeignInd]
      ,EG.[PassportCountryCode]
      ,EG.[RacialGroup]
      ,EG.[Disabled]
      ,EG.[Status]
      ,EG.[CountryOfBirth]
      ,EG.[BloodGroup]
      ,EG.[OptOutfromIncomeVerification]



  
  
  
  
  
  FROM Employee.Employee E 
  left join entity.GenEntity eg on e.GenEntityID=eg.GenEntityID
  INNER JOIN Employee.EmployeeRule ER ON ER.EmployeeID = E.EmployeeID
    INNER JOIN Employee.EmployeePayPeriod EPP on EPP.EmployeeRuleID = ER.EmployeeRuleID
    INNER JOIN Company.CompanyRule cr on ER.CompanyRuleID = cr.CompanyRuleID
    LEFT JOIN Company.PayPeriodGen AS pg ON pg.PayPeriodGenID = EPP.PayPeriodGenID
    INNER JOIN Payroll.Payslip AS ps ON ps.EmployeePayPeriodID = EPP.EmployeePayPeriodID  

	order by PayslipID
