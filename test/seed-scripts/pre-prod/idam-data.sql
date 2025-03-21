BEGIN TRY
    BEGIN TRANSACTION
    
    DECLARE @Today DATETIME2
    SET @Today = GETDATE()

    DELETE FROM [dbo].[Accounts]
    
    SET IDENTITY_INSERT [dbo].[Accounts] ON 
    
    INSERT INTO [dbo].[Accounts] ([Id], [OpenId], [Name], [Email], [PhoneNumber], [Status], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (2, NULL, N'WYVScIk4Me7hu+Aklw/JcQ==', N'/kEM25CT72Fi0QpIx872HoZLKlYPT3P3pfyOxGWySl4=', NULL, N'Active', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (3, NULL, N'b8CNmp/LbVmBYgW2qf34Cw==', N'2S+LroqABmPf878k6vhei0aoi3gd8av0DGzpXUOmsVE=', NULL, N'Active', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU='),
            (4, NULL, N'HER0DIRs3WkydxHCm3M0/Nqn8Wtk29pZxgshb8EI0i8=', N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', NULL, N'Active', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (5, NULL, N'S6bU8V058xsOHXKDRZjj2w==', N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (6, NULL, N'lgrAijh93TpblDUqv1quLw==', N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', NULL, N'Active', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (7, NULL, N'1iipFjEUXQPmOdQmO2PV8g==', N'5NqRw8gPk7HhhN4+115iKox/iMdtuRJbUZ41IGi9NqgqvXodbnzDj3tDtuaDOK4Y', NULL, N'Active', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (8, NULL, N'YLuVZGGbADpZxZ8gz1+kTw==', N'L5I8oOx6iFSXtXuC/T1H7GJCVvVnGfCwbmd/soW9lMo=', NULL, N'Active', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (9, NULL, N'rYkBRoak0fREt/MXqyp0hzqflovgrCiP0j/WuYXEfpM=', N'm7e2/yhcJyV7agoHTyBmbKCMGFUzsmdXRDxn2edQQfM=', NULL, N'Active', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (10, NULL, N'B7vDfu9uR6j1OAkiamu+0Ct4s05WPdj7o8lToRhkAsA=', N'lEBPjPcG7zYuxA9t4WD8GR3Hlqld3i22WQoCdEsmybschHH+eBHL3BbUzbebHQXM', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (11, NULL, N'TnwFj27u5OIz+gvJo2Svtsl0lSAAGXZhZiQEOq5nGuY=', N'iP2aEugMzwobOTl/6uEhwl52gBhuTaw7Mo241mB7Okfauen10HKNt4jPqqFmXq81', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (12, NULL, N'KE7ffh8jBTjqZF4Z07wOSBYnPvmWq585QFVkMw5T1Xs=', N'DcCQ3RDGc8Cln8zNWwfx60W1EPs9QyWojsP/QV1Yj4nMaeaJRrniFyPvJb4bek99', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (13, NULL, N'OWPQdZRPa4XNt+ApxstTlsXIzxLaR8yLuV9Ikiy+AnM=', N'1gF3h9vQXVN/IGZgqsq6tFx4PAhDNZqJhK8uR99RhcpKKbfdvfVvv5FDOQdr25jo', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (14, NULL, N'a6Aa7NK/1oONvPmZNzr/ONgo0xZAoyLyENo4QjqR9uc=', N'p7h/NBZ+0GfVcrOVJtIc3TAKzyDdsCrHYHL45RK4dghGAvuskFv+3QDVWJMJMiGF', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (15, NULL, N'a6Aa7NK/1oONvPmZNzr/OLJ9jQ9wMe5WSuPilBGjO4s=', N'4P8GaJEHpFZTn2mKNxaqk5RbEXsrawRldnowCfYG1K0q0wMKU6WrJmhm9G+djG/V', NULL, N'Active', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx');
    
    SET IDENTITY_INSERT [dbo].[Accounts] OFF
    
    DELETE FROM [dbo].[AccountClaims]
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] ON 
    
    INSERT INTO [dbo].[AccountClaims] ([Id], [AccountId], [Name], [Value], [Created], [CreatedBy], [LastModified], [LastModifiedBy])
    VALUES  (3, 2, N'role', N'DfeAdmin', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (4, 2, N'OrganisationId', N'-1', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (5, 2, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/24/2024 3:44:32 PM', @Today, N'/kEM25CT72Fi0QpIx872HoZLKlYPT3P3pfyOxGWySl4=', @Today, N'/kEM25CT72Fi0QpIx872HoZLKlYPT3P3pfyOxGWySl4='),
            (7, 3, N'role', N'VcsDualRole', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU='),
            (8, 3, N'OrganisationId', N'11', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU='),
            (10, 3, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/25/2024 12:42:59 PM', @Today, N'2S+LroqABmPf878k6vhei0aoi3gd8av0DGzpXUOmsVE=', @Today, N'2S+LroqABmPf878k6vhei0aoi3gd8av0DGzpXUOmsVE='),
            (11, 3, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 12:53:25 PM', @Today, N'2S+LroqABmPf878k6vhei0aoi3gd8av0DGzpXUOmsVE=', @Today, N'2S+LroqABmPf878k6vhei0aoi3gd8av0DGzpXUOmsVE='),
            (12, 4, N'role', N'DfeAdmin', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (13, 4, N'OrganisationId', N'-1', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (14, 4, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/25/2024 3:32:10 PM', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (15, 5, N'role', N'LaDualRole', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (16, 5, N'OrganisationId', N'6', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (17, 5, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/28/2024 10:14:14 AM', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU='),
            (18, 6, N'role', N'DfeAdmin', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (19, 6, N'OrganisationId', N'-1', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (20, 7, N'role', N'DfeAdmin', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (21, 7, N'OrganisationId', N'-1', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g==', @Today, N'7ho4Gj4/1kybu2RUX9Lt8g=='),
            (22, 5, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/28/2024 4:25:35 PM', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU=', @Today, N'kiZeItndl8OnjIW1C0KvXjUE2TiLzcJcCi8he3/BKbU='),
            (23, 6, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 12:53:29 PM', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (24, 8, N'role', N'LaDualRole', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (25, 8, N'OrganisationId', N'6', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (26, 9, N'role', N'VcsDualRole', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (27, 9, N'OrganisationId', N'11', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD', @Today, N'5NqRw8gPk7HhhN4+115iKgo7Ot/QF7wFzaaHo/8s0GrUeCSNyAL2ez45QhoEqaYD'),
            (28, 9, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'10/31/2024 6:25:19 PM', @Today, N'm7e2/yhcJyV7agoHTyBmbKCMGFUzsmdXRDxn2edQQfM=', @Today, N'm7e2/yhcJyV7agoHTyBmbKCMGFUzsmdXRDxn2edQQfM='),
            (29, 9, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:25:56 PM', @Today, N'm7e2/yhcJyV7agoHTyBmbKCMGFUzsmdXRDxn2edQQfM=', @Today, N'm7e2/yhcJyV7agoHTyBmbKCMGFUzsmdXRDxn2edQQfM='),
            (30, 8, N'TermsAndConditionsAccepted-https://preprod.manage-family-support-services-and-accounts.education.gov.uk', N'10/31/2024 6:28:02 PM', @Today, N'L5I8oOx6iFSXtXuC/T1H7GJCVvVnGfCwbmd/soW9lMo=', @Today, N'L5I8oOx6iFSXtXuC/T1H7GJCVvVnGfCwbmd/soW9lMo='),
            (31, 8, N'TermsAndConditionsAccepted-https://preprod.connect-families-to-support.education.gov.uk', N'11/1/2024 8:31:25 AM', @Today, N'L5I8oOx6iFSXtXuC/T1H7GJCVvVnGfCwbmd/soW9lMo=', @Today, N'L5I8oOx6iFSXtXuC/T1H7GJCVvVnGfCwbmd/soW9lMo='),
            (32, 10, N'role', N'LaManager', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (33, 10, N'OrganisationId', N'6', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (34, 11, N'role', N'LaDualRole', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (35, 11, N'OrganisationId', N'6', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (36, 12, N'role', N'LaProfessional', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (37, 12, N'OrganisationId', N'6', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (38, 13, N'role', N'VcsManager', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (39, 13, N'OrganisationId', N'11', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (40, 14, N'role', N'VcsDualRole', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (41, 14, N'OrganisationId', N'11', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (42, 15, N'role', N'VcsProfessional', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx'),
            (43, 15, N'OrganisationId', N'11', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx', @Today, N'OoD3XNERbLkKNqc0VS1w7LJs9NhZtibQfW7/AOhDAMKxSyNOVt5kZCcnoL5KCNfx');
    
    SET IDENTITY_INSERT [dbo].[AccountClaims] OFF
    
    DELETE FROM [dbo].[UserSessions]
    
    SET IDENTITY_INSERT [dbo].[UserSessions] ON

    -- No user session data which does make sense
    SET IDENTITY_INSERT [dbo].[UserSessions] OFF
    
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage
    
    DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
    
    SELECT
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE(),
        @ErrorMessage = ERROR_MESSAGE()

    ROLLBACK TRANSACTION
    
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH
