# Payroll System

## Project Description
This Payroll System is a Windows Forms application built with .NET 6. It manages employee payroll including attendance tracking, payroll processing, reports generation, and company settings management. The system uses SQL Server Express with integrated security for data storage.

## Features
- Employee management (Add, Edit, Delete)
- Attendance recording and management
- Payroll period management and payroll processing
- Payslip generation in PDF format
- Government report generation in Excel format
- User authentication
- Company settings and government rates configuration

## Prerequisites
- Windows OS
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Setup Instructions

### Database Setup
1. Open SQL Server Management Studio (SSMS) or use `sqlcmd`.
2. Create a new database named `PayrollSystemDB`.
3. Create required tables by executing the following scripts:

