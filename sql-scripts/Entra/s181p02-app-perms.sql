--
-- The script below can be run against the relevant databases to add the app permissions for Entra authentication
--

--
-- Database s181p02-fh-report-db
--

-- Report API 
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-report-api')
BEGIN
	DROP USER [s181p02-as-fh-report-api]
END
CREATE USER [s181p02-as-fh-report-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-report-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-report-api];


--
-- Database s181p02-fh-notification-db
--

-- Notification API
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-notification-api')
BEGIN
	DROP USER [s181p02-as-fh-notification-api]
END
CREATE USER [s181p02-as-fh-notification-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-notification-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-notification-api];


--
-- Database s181p02-fh-open-referral-mock-db
--

-- Open Referral Mock API
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-open-referral-mock-api')
BEGIN
	DROP USER [s181p02-as-fh-open-referral-mock-api]
END
CREATE USER [s181p02-as-fh-open-referral-mock-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-open-referral-mock-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-open-referral-mock-api];


--
-- Database s181p02-fh-idam-db
--

-- IDAM Maintenance UI
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-idam-maintenance-ui')
BEGIN
	DROP USER [s181p02-as-fh-idam-maintenance-ui]
END
CREATE USER [s181p02-as-fh-idam-maintenance-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-idam-maintenance-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-idam-maintenance-ui];

-- SD Admin (Manage) UI
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-sd-admin-ui')
BEGIN
	DROP USER [s181p02-as-fh-sd-admin-ui]
END
CREATE USER [s181p02-as-fh-sd-admin-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-sd-admin-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-sd-admin-ui];

-- IDAM API
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-idam-api')
BEGIN
	DROP USER [s181p02-as-fh-idam-api]
END
CREATE USER [s181p02-as-fh-idam-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-idam-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-idam-api];


--
-- Database s181p02-fh-referral-db
--

-- Referral API 
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-referral-api')
BEGIN
	DROP USER [s181p02-as-fh-referral-api]
END
CREATE USER [s181p02-as-fh-referral-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-referral-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-referral-api];
      
-- Referral (Connect) UI
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-referral-ui')
BEGIN
	DROP USER [s181p02-as-fh-referral-ui]
END
CREATE USER [s181p02-as-fh-referral-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-referral-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-referral-ui];

-- Referral Dashboard (Connect) UI
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-ref-dash-ui')
BEGIN
	DROP USER [s181p02-as-fh-ref-dash-ui]
END
CREATE USER [s181p02-as-fh-ref-dash-ui] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-ref-dash-ui];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-ref-dash-ui];


--
-- Database s181p02-fh-service-directory-db
--

-- Service Directory API
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-as-fh-sd-api')
BEGIN
	DROP USER [s181p02-as-fh-sd-api]
END
CREATE USER [s181p02-as-fh-sd-api] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-as-fh-sd-api];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-as-fh-sd-api];

-- Open Referral Function
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N's181p02-fa-fh-open-referral')
BEGIN
	DROP USER [s181p02-fa-fh-open-referral]
END
CREATE USER [s181p02-fa-fh-open-referral] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [s181p02-fa-fh-open-referral];
ALTER ROLE db_datawriter ADD MEMBER [s181p02-fa-fh-open-referral];
