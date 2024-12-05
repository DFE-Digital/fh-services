--
-- The script below can be run against the relevant databases to add the app permissions for Entra authentication
--

-- Service Directory API
USE [s181p02-fh-service-directory-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-sd-api')
BEGIN
	DROP USER [s181p02-as-fh-sd-api]
END
CREATE USER [s181p02-as-fh-sd-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-sd-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-sd-api];
GO

-- Referral API
USE [s181p02-fh-referral-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-referral-api')
BEGIN
	DROP USER [s181p02-as-fh-referral-api]
END
CREATE USER [s181p02-as-fh-referral-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-referral-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-referral-api];
GO

-- IDAM API
USE [s181p02-fh-idam-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-idam-api')
BEGIN
	DROP USER [s181p02-as-fh-idam-api]
END
CREATE USER [s181p02-as-fh-idam-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-idam-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-idam-api];
GO

-- Report API
USE [s181p02-fh-report-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-report-api')
BEGIN
	DROP USER [s181p02-as-fh-report-api]
END
CREATE USER [s181p02-as-fh-report-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-report-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-report-api];
GO

-- Notification API
USE [s181p02-fh-notification-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-notification-api')
BEGIN
	DROP USER [s181p02-as-fh-notification-api]
END
CREATE USER [s181p02-as-fh-notification-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-notification-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-notification-api];
GO

-- Open Referral Mock API
USE [s181p02-fh-open-referral-mock-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-open-referral-mock-api')
BEGIN
	DROP USER [s181p02-as-fh-open-referral-mock-api]
END
CREATE USER [s181p02-as-fh-open-referral-mock-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-open-referral-mock-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-open-referral-mock-api];
GO

-- IDAM Maintenance UI
USE [s181p02-fh-idam-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-idam-maintenance-ui')
BEGIN
	DROP USER [s181p02-as-fh-idam-maintenance-ui]
END
CREATE USER [s181p02-as-fh-idam-maintenance-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-idam-maintenance-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-idam-maintenance-ui];
GO

-- Referral (Connect) UI
USE [s181p02-fh-referral-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-referral-ui')
BEGIN
	DROP USER [s181p02-as-fh-referral-ui]
END
CREATE USER [s181p02-as-fh-referral-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-referral-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-referral-ui];
GO

-- Referral Dashboard (Connect) UI
USE [s181p02-fh-referral-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-ref-dash-ui')
BEGIN
	DROP USER [s181p02-as-fh-ref-dash-ui]
END
CREATE USER [s181p02-as-fh-ref-dash-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-ref-dash-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-ref-dash-ui];
GO

-- SD Admin (Manage) UI
USE [s181p02-fh-idam-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-sd-admin-ui')
BEGIN
	DROP USER [s181p02-as-fh-sd-admin-ui]
END
CREATE USER [s181p02-as-fh-sd-admin-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-sd-admin-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-sd-admin-ui];
GO

-- Open Referral Function
USE [s181p02-fh-service-directory-db]
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-fa-fh-open-referral')
BEGIN
	DROP USER [s181p02-fa-fh-open-referral]
END
DROP USER [s181p02-fa-fh-open-referral]
CREATE USER [s181p02-fa-fh-open-referral] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-fa-fh-open-referral];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-fa-fh-open-referral];
GO