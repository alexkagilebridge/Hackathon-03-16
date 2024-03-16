SELECT top(100)  [PayslipID]
  
      ,[GrossEarnings]
      ,[Deductions]
      ,[NetPay]
      ,[Note]
      ,[LeavePeriodsRemain]
      ,[AnnualisationFactor]
      ,[TaxCalculation]

      ,[EarlyPay]
 
      ,[ForcedPay]
      ,[BCEAIncPeriods]
      ,[ForceAverageYTDCalc]
      ,[BCEATakeonTotal]
      ,[SITETotalYTDP]
      ,[PAYETotalYTDP]
      ,[RFIAdjustment]

      ,[PayPeriodGenID]
      ,[PayslipType]
  
  FROM [Sage300].[Payroll].[Payslip]
  order by PayslipID
